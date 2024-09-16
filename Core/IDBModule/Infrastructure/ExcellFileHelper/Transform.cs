//----------------------------------
//-- Creator : MrMohande3 Khademi --
//----------------------------------

using Common.Extentions;
using Core.IDBModule.Abstractions.Extentions;
using Core.IDBModule.Entities;
using Core.QuestionModule.Abstractions.Enumerations;
using Core.QuestionModule.Entities;
using OfficeOpenXml;

namespace Core.IDBModule.Infrastructure.ExcellFileHelper;

internal class Transform
{
    public List<IDBEntity> Gets(string filePath)
    {
        var listOfQuestion = new List<IDBEntity>();

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

    private IDBEntity Get(ExcelWorksheet ws, int row)
    {
        int col = 1;
        var idb = new IDBEntity
        {
            TypeOfIDB = ws.GetStringValue(row, col++).GetIDB(),
            Title = ws.GetStringValue(row, col++),
            Content = ws.GetStringValue(row, col++),
        };

        return idb;
    }
}
