using System.Text.Json;

namespace SME.SERAp.Prova.Item.Infra.Extensions
{
    public static class JsonSerializerExtensions
    {
        private static JsonSerializerOptions ObterConfigSerializer()
        {
            return new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                IgnoreNullValues = true
            };            
        }        
        
        public static T ConverterObjectStringPraObjeto<T>(this string objectString)
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return JsonSerializer.Deserialize<T>(objectString, jsonSerializerOptions);
        }
        
        public static string ConverterObjectParaJson(this object obj)
        {
            return obj == null ? string.Empty : JsonSerializer.Serialize(obj, ObterConfigSerializer());
        }        
    }
}
