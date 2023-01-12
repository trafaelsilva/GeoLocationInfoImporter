using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLocationInfoImporter
{
    public class Country
    {
        [Index(1)]
        public string Name { get; set; }

        [Index(2)]
        public string Code { get; set; }

        [Index(3)]
        public string IsoCode2Character { get; set; }
    }
}
