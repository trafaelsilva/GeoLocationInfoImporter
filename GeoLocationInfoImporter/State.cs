using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoLocationInfoImporter
{
    public  class State
    {
        [Index(1)]
        public string Name { get; set; }

        [Index(5)]
        public string Code { get; set; }

        [Index(3)]
        public string CountryCode { get; set; }
    }
}
