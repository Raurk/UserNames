using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UserNames.Lib.Models;

namespace UserNames.Lib
{
    public class JsonImporter<T> : DataImporter<T> where T : User
    {
        private const string FileName = "example_data.json";
        string _json;

        protected override void ReadData()
        {
            using (StreamReader r = new StreamReader(FileName))
            {
                _json = r.ReadToEnd();
            }
        }

        protected override void FormatData()
        {
            var jsonReader = new JsonTextReader(new StringReader(_json))
            {
                SupportMultipleContent = true
            };

            var jsonSerializer = new JsonSerializer();
            while (jsonReader.Read())
            {
                Items.Add(jsonSerializer.Deserialize<T>(jsonReader));
            }
        }

        protected override IEnumerable<T> ValidateData()
        {
            foreach (var item in Items)
            {
                if (item.id < 1)
                {
                    throw new ApplicationException("Invalid Id");
                }

                if (item.age < 0 || item.age > 127)
                {
                    throw new ApplicationException("Invalid Age");
                }

                if (string.IsNullOrWhiteSpace(item.first) || item.first.Length < 2 || item.first.Length > 127)
                {
                    throw new ApplicationException("Invalid First Name");
                }

                if (string.IsNullOrWhiteSpace(item.last) || item.last.Length < 2 || item.last.Length > 127)
                {
                    throw new ApplicationException("Invalid Last Name");
                }

                switch (item.gender)
                {
                    case "M":
                    case "F":
                        {
                            break;
                        }
                    default:
                        {
                            throw new ApplicationException("Invalid Gender");
                        }
                }

                yield return item;
            }
        }
    }
}
