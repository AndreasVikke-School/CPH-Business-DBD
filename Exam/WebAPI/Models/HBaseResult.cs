using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace WebAPI.Models
{
    public class HBaseResult
    {
        [JsonProperty(PropertyName = "Row")]
        public List<HbaseRow> rows { get; set; }

        public class HbaseRow {
            [JsonIgnore]
            public string _key;
            [JsonProperty(PropertyName = "key")]
            public string key {
                get {
                    return Encoding.UTF8.GetString(Convert.FromBase64String(_key));;
                } 
                private set {
                    _key = value;
                } 
            }

            [JsonProperty(PropertyName = "Cell")]
            public List<HbaseCell> cells { get; set; }
        }

        public class HbaseCell {
            [JsonIgnore]
            public string _column;
            [JsonProperty(PropertyName = "column")]
            public string column {
                get {
                    return Encoding.UTF8.GetString(Convert.FromBase64String(_column));;
                } 
                private set {
                    _column = value;
                } 
            }

            public string timestamp { get; set; }

            [JsonIgnore]
            public string _data;
            [JsonProperty(PropertyName = "$")]
            public string data {
                get {
                    return Encoding.UTF8.GetString(Convert.FromBase64String(_data));;
                } 
                private set {
                    _data = value;
                } 
            }
        }
    }
}