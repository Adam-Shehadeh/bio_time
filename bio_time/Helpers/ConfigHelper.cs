using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using bio_time.Models;
using Newtonsoft.Json;

namespace bio_time.Helpers {
    public static class ConfigHelper {
        public static bool ConfigExists() {
            return File.Exists("config.json");
        }
        public static ConfigModel GetConfigProperties() {
            if (ConfigExists()) {
                string fileContents = File.ReadAllText("config.json");
                return JsonConvert.DeserializeObject<ConfigModel>(fileContents);
            } else {
                throw new FileNotFoundException("Could not find configuration file.");
            }
        }
        public static void UpdateConfig(ConfigModel configModel)
        {
            if (ConfigExists())
            {
                string fileContent = JsonConvert.SerializeObject(configModel,Formatting.Indented);
                using(StreamWriter writer = new StreamWriter("config.json", false))
                {
                    writer.Write(fileContent);
                }
            } else
            {
                throw new FileNotFoundException("Could not find configuration file.");
            }
        }
    }
}
