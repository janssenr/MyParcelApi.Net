using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace MyParcelApi.Net.Helpers
{
    /// <summary>
    /// Summary description for JsonHelper
    /// </summary>
    public class JsonHelper
    {
        public static string Serialize<T>(T obj, string dateTimeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            string retVal;
            var serializer = new DataContractJsonSerializer(obj.GetType(), new DataContractJsonSerializerSettings { DateTimeFormat = new DateTimeFormat(dateTimeFormat) });
            using (var ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                retVal = Encoding.UTF8.GetString(ms.ToArray());
            }
            return retVal;
        }

        public static T Deserialize<T>(string json, string dateTimeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            var obj = Activator.CreateInstance<T>();
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(obj.GetType(), new DataContractJsonSerializerSettings { DateTimeFormat = new DateTimeFormat(dateTimeFormat) });
                obj = (T)serializer.ReadObject(ms);
                ms.Close();
            }
            return obj;
        }

        public static Dictionary<string, T> DeserializeAsDictionary<T>(string json, string dateTimeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            Dictionary<string, T> obj;
            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(typeof(Dictionary<string, T>), new DataContractJsonSerializerSettings { DateTimeFormat = new DateTimeFormat(dateTimeFormat), UseSimpleDictionaryFormat = true });
                obj = (Dictionary<string, T>)serializer.ReadObject(ms);
                ms.Close();
            }
            return obj;
        }
    }
}