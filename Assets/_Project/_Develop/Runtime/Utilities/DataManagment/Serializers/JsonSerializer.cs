using Newtonsoft.Json;

namespace Assets.Project._Develop.Runtime.Utilities.DataManagment.Serializers
{
    public class JsonSerializer : IDataSerializer
    {
        public TData Desirialize<TData>(string serializedData)
        {
            return JsonConvert.DeserializeObject<TData>(serializedData, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto,
            });
        }

        public string Serialize<TData>(TData data)
        {
            return JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Auto,
            });
        }
    }
}
