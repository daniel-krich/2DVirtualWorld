using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace My2DWorldServer.Extensions
{
    public static class JsonSerializerExtensions
    {
        public static string SerializeUnicode(object target)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Hebrew, UnicodeRanges.Cyrillic)
            };
            return JsonSerializer.Serialize(target, options);
        }
    }
}
