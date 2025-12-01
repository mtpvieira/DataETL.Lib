
namespace DataETL.Abstractions
{
    public abstract class DefaultExtractor
    {
        protected static string FindColumnName(string propertyName, IEnumerable<string> propertyList)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(propertyName))
                    return string.Empty;

                var normalizePropertyName = new string(propertyName
                    .ToLower()
                    .Where(c => char.IsLetterOrDigit(c))
                    .ToArray());

                foreach (var prop in propertyList)
                {
                    var normalizeProp = new string(prop
                        .ToLower()
                        .Where(c => char.IsLetterOrDigit(c))
                        .ToArray());

                    if (normalizeProp == normalizePropertyName)
                        return prop;
                }
            }
            catch
            {
                Console.WriteLine($"Erro ao encontrar o nome da coluna: {propertyName}.");
            }

            return string.Empty;

        }
    }
}
