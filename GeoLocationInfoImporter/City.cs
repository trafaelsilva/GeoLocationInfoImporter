using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLocationInfoImporter
{
    public class City
    {
        [Index(1)]
        public string Name { get; set; }

        [Index(3)]
        public string StateCode { get; set; }

        [Index(6)]
        public string CountryCode { get; set; }
    }

    public class CityComparer : IEqualityComparer<City>
    {

        public bool Equals(City x, City y)
        {
            return x.Name.Equals(y.Name)  && x.CountryCode.Equals(y.CountryCode);
        }

        public int GetHashCode(City obj)
        {
            return (obj.Name == null ? 0 : obj.Name.GetHashCode()) ^ (obj.CountryCode == null ? 0 : obj.CountryCode.GetHashCode());
        }
    }
}
