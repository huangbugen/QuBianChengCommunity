using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace BBSSystemManagement.Domain.Shared.Cto
{
    public class SensitiveWordCto
    {
        public string Words { get; set; }
        public DateTime UpdateTime { get; set; }

        public SensitiveWordCto()
        {

        }

        public SensitiveWordCto(List<string> words)
        {
            UpdateTime = DateTime.Now;
            Words = System.Text.Json.JsonSerializer.Serialize(words, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            });
        }
    }
}