using ClosedXML.Excel;
using DataETL.Abstractions;
using DataETL.Interfaces;
using System.Reflection;

namespace DataETL.Business.Extractors
{
    public class ExcelExtractor<T> : DefaultExtractor, IExtractor<T> where T : new()
    {
        private readonly string _filePath;
        private readonly string _sheetName;

        public ExcelExtractor(string filePath, string sheetName = null)
        {
            _filePath = filePath;
            _sheetName = sheetName;
        }

        public Task<IEnumerable<T>> ExtractAsync()
        {
            var workbook = new XLWorkbook(_filePath);
            var sheet = _sheetName == null
                ? workbook.Worksheets.First()
                : workbook.Worksheet(_sheetName);

            var rows = sheet.RangeUsed().RowsUsed().Skip(1); // pula cabeçalho
            var header = sheet.Row(1).Cells().Select(c => c.GetString()).ToList();

            var properties = typeof(T)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .ToDictionary(p => p.Name, p => p);

            var result = new List<T>();

            foreach (var row in rows)
            {
                var obj = new T();

                for (int i = 0; i < header.Count; i++)
                {
                    var columnName = FindColumnName(header[i], properties.Select(x => x.Key));

                    if (columnName == null)
                        continue;

                    var prop = properties[columnName];

                    var cellValue = row.Cell(i + 1).Value;

                    if (string.IsNullOrEmpty(cellValue.ToString()))
                        continue;

                    object convertedValue = Convert.ChangeType(cellValue, prop.PropertyType);
                    prop.SetValue(obj, convertedValue);
                }

                result.Add(obj);
            }

            return Task.FromResult(result.AsEnumerable());
        }
    }
}
