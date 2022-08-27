using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akips_Relay
{
    internal class Gamedata
    {
        public Gamedata() { }
        public Gamedata(string map, double x, double y, double z)
        {
            Map = map;
            X = x;
            Y = y;
            Z = z;
        }

        public string? Map { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public double? Z { get; set; }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Gamedata? FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Gamedata>(json);
        }

        public bool IsAnyNullOrEmpty()
        {
            return GetType().GetProperties()
                .Where(pi => pi.PropertyType == typeof(string))
                .Select(pi => (string?)pi.GetValue(this))
                .Any(value => string.IsNullOrEmpty(value));
        }
    }
}
