using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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

        public bool IsNumericState => int.TryParse(StateCode, out int i);
    }

    public class CityComparer : IEqualityComparer<City>
    {

        public bool Equals(City x, City y)
        {
            if (x.IsNumericState && y.IsNumericState)
            {
                return x.Name.Equals(y.Name) && x.CountryCode.Equals(y.CountryCode);
            }
            else if (!x.IsNumericState && !y.IsNumericState)
            {
                return x.Name.Equals(y.Name) && x.CountryCode.Equals(y.CountryCode) && x.StateCode.Equals(y.StateCode);
            }
            else return false;


        }

        public int GetHashCode(City obj)
        {
            if (obj.IsNumericState)
            {
                return (obj.Name == null ? 0 : obj.Name.GetHashCode()) ^ (obj.CountryCode == null ? 0 : obj.CountryCode.GetHashCode());
            }
            else
            {
                return (obj.Name == null ? 0 : obj.Name.GetHashCode()) ^ (obj.CountryCode == null ? 0 : obj.CountryCode.GetHashCode()) ^ (obj.StateCode == null ? 0 : obj.StateCode.GetHashCode());
            }

        }
    }
}
