using System;
using System.Runtime.Serialization;

namespace MyParcelApi.Net.Models
{
    [DataContract]
    public class Error
    {
        [DataMember(Name = "code", EmitDefaultValue = false)]
        public int Code { get; set; }

        [DataMember(Name = "message", EmitDefaultValue = false)]
        public string Message { get; set; }

        //[DataMember(Name = "human", EmitDefaultValue = false)]
        //public string Human { get; set; }

        [DataMember(Name = "fields", EmitDefaultValue = false)]
        public string[] Fields { get; set; }

        //[DataMember(Name = "human", EmitDefaultValue = false)]
        //public string[] Human { get; set; }

        [DataMember(Name = "human", EmitDefaultValue = false, IsRequired = false)]
        private dynamic _human;

        public string[] Human
        {
            get
            {
                var obj = _human;
                if (obj is string)
                {
                    return new[] { (string)obj };
                }
                if (obj is Array)
                {
                    int length = ((object[])obj).Length;
                    var openinghours = new string[length];
                    for (int i = 0; i < length; i++)
                    {
                        openinghours[i] = obj[i].ToString();
                    }
                    return openinghours;
                }
                return new string[0];
            }
            set { _human = value; }
        }
    }
}
