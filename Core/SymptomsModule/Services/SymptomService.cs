using Common.Exceptions;
using Common.Extentions;
using Core.DAL;
using Core.SymptomsModule.Abstractions.Dtos;
using Core.SymptomsModule.Abstractions.Enums;
using Core.SymptomsModule.Abstractions.Services;
using Core.SymptomsModule.Entities;
using Core.SymptomsModule.MapperServices;
using Core.SymptomsModule.ValidationServices;
using Core.UserManagement.Abstractions.Exceptions;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Core.SymptomsModule.Services;

public class SymptomService(
    IUnitOfWork unitOfWork,
    ISymptomValidationService validation,
    ISymptomMapperService mapper) : ISymptomService
{
    public async Task AddOrUpdateAsync(SymptomAddOrUpdateDto addModel)
    {
        validation.IsValidAndThrowException(addModel);


        var user = await unitOfWork.UserRepository.GetsQueryableNoTracker()
            .SingleOrDefaultAsync(x => x.UserName == addModel.UserName);
        if (user == null)
            throw new UserNameNotExistException(addModel.UserName ?? "Unknown");

        var entities = await unitOfWork.SymptomRepository
            .GetsQueryableTracker()
            .Where(x => x.UserId == user.Id)
            .OrderByDescending(x => x.DateTimeOfCreation)
            .ToListAsync();


        var lastItemTotal = entities.FirstOrDefault();
        var lastItemSpecial = entities.FirstOrDefault(x => x.TypeOfSymptom == addModel.TypeOfSymptom);


        if (lastItemTotal is null)
        {
            var entity = mapper.Map(addModel);
            entity.UserId = user.Id;
            await unitOfWork.SymptomRepository.CreateAsync(entity);
            await unitOfWork.SaveChangeAsync();
        }
        else
        {
            if (lastItemSpecial is null)
            {
                var entity = mapper.Map(addModel);
                entity.UserId = user.Id;
                entity.DateTimeOfUpdate = lastItemTotal.DateTimeOfUpdate;
                await unitOfWork.SymptomRepository.CreateAsync(entity);
                await unitOfWork.SaveChangeAsync();
            }
            else
            {
                // بیشتر باشه میشه هفته جدید
                if ((DateTime.Now.Date - lastItemSpecial.DateTimeOfUpdate.Date).Days >= 7)
                {
                    var entity = mapper.Map(addModel);
                    entity.UserId = user.Id;
                    entity.DateTimeOfUpdate = lastItemSpecial.DateTimeOfUpdate.Date.AddDays(7);
                    await unitOfWork.SymptomRepository.CreateAsync(entity);
                    await unitOfWork.SaveChangeAsync();
                }
                // در غیر اینصورت باید آپدیت بشه همین
                else
                {
                    if (addModel.Value.HasValue)
                        lastItemSpecial.Value = addModel.Value.Value;
                    await unitOfWork.SaveChangeAsync();
                }
            }

        }
    }

    public async Task<IEnumerable<SymptomEntity>> GetAllAsync()
        => await unitOfWork.SymptomRepository.GetsQueryableNoTracker().ToListAsync();

    public async Task<List<SymptomChartDataDto>> GetChartAsync(string? userName)
    {

        if (string.IsNullOrWhiteSpace(userName))
            throw new NotValidDataException("Please enter userName");

        var user = await unitOfWork.UserRepository.GetsQueryableNoTracker()
            .SingleOrDefaultAsync(x => x.UserName == userName);

        if (user is null)
            throw new UserNameNotExistException(userName);

        var result = new List<SymptomChartDataDto>();

        var types = EnumExtensions.ToDictionaryWithNameAndType<ETypeOfSymptoms>();
        foreach (var typeOfSymptoms in types)
        {
            result.Add(new SymptomChartDataDto
            {
                TypeOfSymptoms = typeOfSymptoms.Value,
                Spots = await GetSpotsAsync(user.Id, typeOfSymptoms.Value)
            });
        }

        return result;
    }

    public async Task<string> GetReportAsync()
    {
        try
        {
            // Set EPPlus license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            var filePath = Path.Combine("wwwroot", "GetReportSymptoms.xlsx");
            var fullPath = Path.GetFullPath(filePath);

            // Get all patients with their symptoms
            var symptomsData = await unitOfWork.SymptomRepository
                .GetsQueryableNoTracker()
                .Include(x => x.User)
                .OrderBy(x => x.User.UserName)
                .ThenBy(x => x.TypeOfSymptom)
                .ThenBy(x => x.DateTimeOfCreation)
                .ToListAsync();

            // Group data by patient
            var patientGroups = symptomsData
                .GroupBy(x => new { x.UserId, x.User.UserName, x.User.Age, x.User.Sex })
                .ToList();

            using var package = new ExcelPackage();

            // Create summary worksheet
            var summaryWorksheet = package.Workbook.Worksheets.Add("Summary");
            CreateSummarySheet(summaryWorksheet, patientGroups.Cast<IGrouping<dynamic, SymptomEntity>>().ToList());

            // Create detailed worksheet for each patient
            foreach (var patientGroup in patientGroups)
            {
                var worksheetName = $"Patient_{patientGroup.Key.UserName}".Substring(0, Math.Min(31, $"Patient_{patientGroup.Key.UserName}".Length));
                var worksheet = package.Workbook.Worksheets.Add(worksheetName);
                CreatePatientDetailSheet(worksheet, patientGroup.Key.UserName, patientGroup.ToList());
            }

            // Create overview by symptom type
            var overviewWorksheet = package.Workbook.Worksheets.Add("Symptoms Overview");
            CreateSymptomsOverviewSheet(overviewWorksheet, symptomsData);


            // Create matrix sheet
            var matrixWorksheet = package.Workbook.Worksheets.Add("Symptom Matrix");
            CreateSymptomMatrixSheet(matrixWorksheet, symptomsData);


            matrixWorksheet = package.Workbook.Worksheets.Add("Symptom Week Matrix");
            CreateSymptomWeekMatrixSheet(matrixWorksheet, symptomsData);


            // Save the file
            var fileInfo = new FileInfo(fullPath);
            await package.SaveAsAsync(fileInfo);

            return fullPath;
        }
        catch (Exception ex)
        {
            throw new NotHandleException($"Error generating report: {ex.Message}", ex);
        }
    }


    private void CreateSymptomWeekMatrixSheet(ExcelWorksheet worksheet, List<SymptomEntity> symptomsData)
    {
        // Get distinct patient names and symptom types
        var patientNames = symptomsData
            .Select(s => s.User.UserName)
            .Distinct()
            .OrderBy(n => n)
            .ToList();

        var symptomTypes = Enum.GetValues(typeof(ETypeOfSymptoms))
            .Cast<ETypeOfSymptoms>()
            .ToList();

        int weekCount = 7;

        // Header row
        worksheet.Cells[1, 1].Value = "Symptom Name";
        worksheet.Cells[1, 2].Value = "Week";
        for (int p = 0; p < patientNames.Count; p++)
        {
            worksheet.Cells[1, 3 + p].Value = patientNames[p];
            worksheet.Cells[1, 3 + p].Style.Font.Bold = true;
        }

        int currentRow = 2;
        foreach (var symptomType in symptomTypes)
        {
            int startRow = currentRow;
            for (int w = 0; w < weekCount; w++)
            {
                worksheet.Cells[currentRow, 2].Value = $"Week {w + 1}";

                for (int p = 0; p < patientNames.Count; p++)
                {
                    var patientName = patientNames[p];
                    var patientSymptoms = symptomsData
                        .Where(x => x.User.UserName == patientName && x.TypeOfSymptom == symptomType)
                        .OrderBy(x => x.DateTimeOfCreation)
                        .ToList();

                    var value = patientSymptoms.ElementAtOrDefault(w)?.Value;
                    worksheet.Cells[currentRow, 3 + p].Value = value;
                }
                currentRow++;
            }
            // Merge the symptom name cell vertically for this symptom
            worksheet.Cells[startRow, 1, currentRow - 1, 1].Merge = true;
            worksheet.Cells[startRow, 1].Value = GetSymptomDisplayName(symptomType);
            worksheet.Cells[startRow, 1].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            worksheet.Cells[startRow, 1].Style.Font.Bold = true;
        }

        worksheet.Cells.AutoFitColumns();
    }
    private void CreateSymptomMatrixSheet(ExcelWorksheet worksheet, List<SymptomEntity> symptomsData)
    {
        // Get distinct patient names and symptom types
        var patientNames = symptomsData
            .Select(s => s.User.UserName)
            .Distinct()
            .OrderBy(n => n)
            .ToList();

        var symptomTypes = Enum.GetValues(typeof(ETypeOfSymptoms))
            .Cast<ETypeOfSymptoms>()
            .ToList();

        // Header row: first cell is empty, then patient names
        worksheet.Cells[1, 1].Value = "Symptom \\ Patient";
        for (int col = 0; col < patientNames.Count; col++)
        {
            worksheet.Cells[1, col + 2].Value = patientNames[col];
            worksheet.Cells[1, col + 2].Style.Font.Bold = true;
        }

        // Fill rows: each row is a symptom type
        for (int row = 0; row < symptomTypes.Count; row++)
        {
            var symptomType = symptomTypes[row];
            worksheet.Cells[row + 2, 1].Value = GetSymptomDisplayName(symptomType);
            worksheet.Cells[row + 2, 1].Style.Font.Bold = true;

            for (int col = 0; col < patientNames.Count; col++)
            {
                var patientName = patientNames[col];
                var patientSymptoms = symptomsData
                    .Where(s => s.User.UserName == patientName && s.TypeOfSymptom == symptomType)
                    .OrderBy(s => s.DateTimeOfCreation)
                    .ToList();

                // Example: list week numbers (or you can use values, dates, etc.)
                var weekNumbers = Enumerable.Range(1, patientSymptoms.Count)
                    .Select(w => $"week{w}");

                worksheet.Cells[row + 2, col + 2].Value = string.Join(", ", weekNumbers);
            }
        }

        worksheet.Cells.AutoFitColumns();
    }
    private async Task<List<SymptomChartDataSpotDto>> GetSpotsAsync(int userId, ETypeOfSymptoms typeOfSymptom)
    {
        var result = new List<SymptomChartDataSpotDto>();

        var data = await unitOfWork.SymptomRepository
            .GetsQueryableNoTracker()
            .Where(x => x.UserId == userId)
            .Where(x => x.TypeOfSymptom == typeOfSymptom).ToListAsync();

        int week = 1;
        foreach (var spot in data)
        {
            result.Add(new SymptomChartDataSpotDto
            {
                Week = week++,
                Degree = spot.Value
            });
        }

        return result;
    }

    private void CreateSummarySheet(ExcelWorksheet worksheet, List<IGrouping<dynamic, SymptomEntity>> patientGroups)
    {
        // Set headers
        worksheet.Cells[1, 1].Value = "Patient Summary Report";
        worksheet.Cells[1, 1, 1, 6].Merge = true;
        worksheet.Cells[1, 1].Style.Font.Bold = true;
        worksheet.Cells[1, 1].Style.Font.Size = 16;
        worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

        worksheet.Cells[3, 1].Value = "Patient Name";
        worksheet.Cells[3, 2].Value = "Age";
        worksheet.Cells[3, 3].Value = "Sex";
        worksheet.Cells[3, 4].Value = "Total Symptoms Recorded";
        worksheet.Cells[3, 5].Value = "Unique Symptom Types";
        worksheet.Cells[3, 6].Value = "Last Record Date";

        // Style headers
        worksheet.Cells[3, 1, 3, 6].Style.Font.Bold = true;

        // Fill data
        int row = 4;
        foreach (var patientGroup in patientGroups)
        {
            var symptoms = patientGroup.ToList();
            worksheet.Cells[row, 1].Value = patientGroup.Key.UserName;
            worksheet.Cells[row, 2].Value = patientGroup.Key.Age;
            worksheet.Cells[row, 3].Value = patientGroup.Key.Sex ? "Male" : "Female";
            worksheet.Cells[row, 4].Value = symptoms.Count;
            worksheet.Cells[row, 5].Value = symptoms.Select(s => s.TypeOfSymptom).Distinct().Count();
            worksheet.Cells[row, 6].Value = symptoms.Max(s => s.DateTimeOfCreation).ToString("yyyy-MM-dd");
            row++;
        }

        // Auto-fit columns
        worksheet.Cells.AutoFitColumns();
    }

    private void CreatePatientDetailSheet(ExcelWorksheet worksheet, string patientName, List<SymptomEntity> symptoms)
    {
        // Set title
        worksheet.Cells[1, 1].Value = $"Symptoms Detail - {patientName}";
        worksheet.Cells[1, 1, 1, 5].Merge = true;
        worksheet.Cells[1, 1].Style.Font.Bold = true;
        worksheet.Cells[1, 1].Style.Font.Size = 14;
        worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

        // Group symptoms by type
        var symptomGroups = symptoms.GroupBy(s => s.TypeOfSymptom).ToList();

        int currentRow = 3;

        foreach (var symptomGroup in symptomGroups)
        {
            // Symptom type header
            worksheet.Cells[currentRow, 1].Value = GetSymptomDisplayName(symptomGroup.Key);
            worksheet.Cells[currentRow, 1, currentRow, 5].Merge = true;
            worksheet.Cells[currentRow, 1].Style.Font.Bold = true;
            currentRow++;

            // Headers for this symptom type
            worksheet.Cells[currentRow, 1].Value = "Week";
            worksheet.Cells[currentRow, 2].Value = "Degree";
            worksheet.Cells[currentRow, 3].Value = "Record Date";
            worksheet.Cells[currentRow, 4].Value = "Update Date";
            worksheet.Cells[currentRow, 5].Value = "Days Since Last Update";

            worksheet.Cells[currentRow, 1, currentRow, 5].Style.Font.Bold = true;
            currentRow++;

            // Data for this symptom type
            var orderedSymptoms = symptomGroup.OrderBy(s => s.DateTimeOfCreation).ToList();
            int week = 1;

            foreach (var symptom in orderedSymptoms)
            {
                worksheet.Cells[currentRow, 1].Value = week++;
                worksheet.Cells[currentRow, 2].Value = symptom.Value;
                worksheet.Cells[currentRow, 3].Value = symptom.DateTimeOfCreation.ToString("yyyy-MM-dd HH:mm");
                worksheet.Cells[currentRow, 4].Value = symptom.DateTimeOfUpdate.ToString("yyyy-MM-dd HH:mm");
                worksheet.Cells[currentRow, 5].Value = (DateTime.Now - symptom.DateTimeOfUpdate).Days;
                currentRow++;
            }

            currentRow++; // Space between symptom types
        }

        // Auto-fit columns
        worksheet.Cells.AutoFitColumns();
    }

    private void CreateSymptomsOverviewSheet(ExcelWorksheet worksheet, List<SymptomEntity> symptomsData)
    {
        // Set title
        worksheet.Cells[1, 1].Value = "Symptoms Overview by Type";
        worksheet.Cells[1, 1, 1, 6].Merge = true;
        worksheet.Cells[1, 1].Style.Font.Bold = true;
        worksheet.Cells[1, 1].Style.Font.Size = 16;
        worksheet.Cells[1, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

        // Headers
        worksheet.Cells[3, 1].Value = "Symptom Type";
        worksheet.Cells[3, 2].Value = "Total Records";
        worksheet.Cells[3, 3].Value = "Unique Patients";
        worksheet.Cells[3, 4].Value = "Average Degree";
        worksheet.Cells[3, 5].Value = "Max Degree";
        worksheet.Cells[3, 6].Value = "Min Degree";

        // Style headers
        worksheet.Cells[3, 1, 3, 6].Style.Font.Bold = true;

        // Group by symptom type
        var symptomTypeGroups = symptomsData.GroupBy(s => s.TypeOfSymptom).ToList();

        int row = 4;
        foreach (var group in symptomTypeGroups)
        {
            var symptoms = group.ToList();
            worksheet.Cells[row, 1].Value = GetSymptomDisplayName(group.Key);
            worksheet.Cells[row, 2].Value = symptoms.Count;
            worksheet.Cells[row, 3].Value = symptoms.Select(s => s.UserId).Distinct().Count();
            worksheet.Cells[row, 4].Value = Math.Round(symptoms.Average(s => s.Value), 2);
            worksheet.Cells[row, 5].Value = symptoms.Max(s => s.Value);
            worksheet.Cells[row, 6].Value = symptoms.Min(s => s.Value);
            row++;
        }

        // Auto-fit columns
        worksheet.Cells.AutoFitColumns();
    }

    private string GetSymptomDisplayName(ETypeOfSymptoms symptomType)
    {
        return symptomType switch
        {
            ETypeOfSymptoms.AbdominalPain => "Abdominal Pain (درد شکم)",
            ETypeOfSymptoms.AbdominalSwelling => "Abdominal Swelling (تورم و نفخ شکم)",
            ETypeOfSymptoms.Constipation => "Constipation (یبوست)",
            ETypeOfSymptoms.Diarrhea => "Diarrhea (اسهال)",
            ETypeOfSymptoms.MucusInTheStool => "Mucus In The Stool (مخاط در مدفوع)",
            ETypeOfSymptoms.AbdominalCramps => "Abdominal Cramps (گرفتگی شکم)",
            ETypeOfSymptoms.IncompleteExcretion => "Incomplete Excretion (دفع ناقص)",
            ETypeOfSymptoms.DiarrheaAndConstipationAtTheSameTime => "Diarrhea And Constipation At The Same Time (اسهال و یبوست همزمان)",
            _ => symptomType.ToString()
        };
    }
}
