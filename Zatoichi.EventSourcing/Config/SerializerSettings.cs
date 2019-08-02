namespace Zatoichi.EventSourcing.Config
{
    using System;
    using System.Reflection;
    using ExpressionSerializer;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class ExpressionSettings
    {
        public static void SetupExpressionSerialization(Type[] types)
        {
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Objects,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            foreach (var t in types)
            {
                settings.Converters.Add(new ExpressionJsonConverter(t.Assembly));
            }

            JsonConvert.DefaultSettings = () => settings;
        }
    }
}