using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Akips_Relay.Config
{
    internal class ConfigManager
    {
        private readonly Logger _logger;
        private readonly string _fileName;

        public ConfigManager(Logger logger, string fileName = "config.yml")
        {
            _logger = logger;
            _fileName = fileName;
        }
        
        public ConfigSchema? GetConfig()
        {
            if (!File.Exists(_fileName))
            {
                _logger.Info($"Nie znaleziono pliku konfiguracyjnego {_fileName}, generuje nowy");
                var newSchema = CreateConfigFile();
                return newSchema;
            }

            var yamlDeserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            string text = File.ReadAllText(_fileName);
            var schema = new ConfigSchema();
            try
            {
                schema = yamlDeserializer.Deserialize<ConfigSchema>(text);
            } catch(Exception e)
            {
                _logger.Error($"Plik konfiguracyjny zawiera nieprawidłowe wartości");
                return null;
            }

            var schemaValidator = new ConfigSchemaValidator();
            var validationResult = schemaValidator.Validate(schema);
            if (!validationResult.IsValid)
            {
                _logger.Error($"Plik konfiguracyjny zawiera nieprawidłowe wartości");
                return null;
            }
            return schema;
        }

        private ConfigSchema CreateConfigFile()
        {
            var yamlSerializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            var schema = new ConfigSchema();
            var yaml = yamlSerializer.Serialize(schema);
            File.WriteAllText(_fileName, yaml);
            return schema;
        }
    }
}
