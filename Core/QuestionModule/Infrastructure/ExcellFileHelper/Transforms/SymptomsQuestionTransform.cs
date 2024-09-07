//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Common.Extentions;
using Core.QuestionModule.Abstractions.Enumerations;
using Core.QuestionModule.Entities;
using OfficeOpenXml;

namespace Core.QuestionModule.Infrastructure.ExcellFileHelper.Transforms;

internal class SymptomsQuestionTransform : ITransformExcellFileToQuestionList
{
    public List<QuestionEntity> Gets(string filePath)
    {
        var listOfQuestion = new List<QuestionEntity>();

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var excelPack = new ExcelPackage())
        {
            using (var stream = File.OpenRead(filePath))
            {
                excelPack.Load(stream);
            }

            var ews = excelPack.Workbook.Worksheets[0];

            for (int rowNum = 1; rowNum <= ews.Dimension.End.Row; rowNum++)
            {
                try
                {
                    listOfQuestion.Add(Get(ews, rowNum));
                }
                catch (Exception ex)
                {
                    throw new Exception($"ROW : {rowNum}", ex);
                }
            }
        }

        return listOfQuestion;
    }

    private QuestionEntity Get(ExcelWorksheet ws, int row)
    {
        int col = 1;
        var question = new QuestionEntity
        {
            TypeOfQuestion = ETypeOfQuestion.Symptoms,
            Title = ws.GetStringValue(row, col++)
        };

        question.QuestionOptions =
        [
            new QuestionOption
                {
                    Name = ws.GetStringValue(row,col++),
                    Value = ws.GetIntValue(row,col++)
                },
            new QuestionOption
                {
                    Name = ws.GetStringValue(row,col++),
                    Value = ws.GetIntValue(row,col++)
                },
            new QuestionOption
                {
                    Name = ws.GetStringValue(row,col++),
                    Value = ws.GetIntValue(row,col++)
                },
            new QuestionOption
                {
                    Name = ws.GetStringValue(row,col++),
                    Value = ws.GetIntValue(row,col++)
                },
            new QuestionOption
                {
                    Name = ws.GetStringValue(row,col++),
                    Value = ws.GetIntValue(row,col++)
                },
            new QuestionOption
                {
                    Name = ws.GetStringValue(row,col++),
                    Value = ws.GetIntValue(row,col++)
                },
            ];

        return question;
    }
}
