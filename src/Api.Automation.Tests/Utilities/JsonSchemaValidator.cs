
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using NLog;

namespace Api.Automation.Tests.Utilities
{
    public static class JsonSchemaValidator
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Validates the JSON response against a given JSON schema file.
        /// Returns true if valid; false otherwise.
        /// </summary>
        public static bool IsSchemaValid(string jsonResponse, string schemaPath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(jsonResponse))
                {
                    logger.Error("JSON response is empty or null.");
                    return false;
                }

                if (!File.Exists(schemaPath))
                {
                    logger.Error($"Schema file not found at: {schemaPath}");
                    return false;
                }

                var schemaJson = File.ReadAllText(schemaPath);
                var schema = JSchema.Parse(schemaJson);
                var jObject = JObject.Parse(jsonResponse);

                bool isValid = jObject.IsValid(schema, out IList<string> errors);

                if (!isValid)
                {
                    logger.Error($"Schema validation failed:\n{string.Join("\n", errors)}");
                }
                else
                {
                    logger.Info("Schema validation passed successfully.");
                }

                return isValid;
            }
            catch (Exception ex)
            {
                logger.Error($"Schema validation error: {ex.Message}");
                return false;
            }
        }
    }
}