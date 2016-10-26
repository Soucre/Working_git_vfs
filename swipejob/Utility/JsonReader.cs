using Newtonsoft.Json.Linq;

namespace SwipeJob.Utility
{
    public static class JsonReader
    {
        public static T Get<T>(this JObject obj, string name)
        {
            JToken token = obj.GetValue(name, System.StringComparison.OrdinalIgnoreCase);
            if (token != null)
            {
                return token.Value<T>();
            }

            return default(T);
        }
    }
}
