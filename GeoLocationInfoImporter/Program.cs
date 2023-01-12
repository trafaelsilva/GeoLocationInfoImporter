// See https://aka.ms/new-console-template for more information

using System.Globalization;
using CsvHelper;
using GeoLocationInfoImporter;

Console.WriteLine("Hello, Importer!");

using (var reader = new StreamReader(@"..\..\..\Data\countries.csv"))
using (StreamWriter writer = new StreamWriter("countries_output.txt"))
using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
{
    var records = csv.GetRecords<Country>().ToList();
    foreach (var country in records)
    {
        writer.WriteLine($"INSERT INTO Country(Name, Code, IsoCode2Character)VALUES('{country.Name}', '{country.Code}', '{country.IsoCode2Character}');");
    }
    Console.WriteLine($"We wrote {records.Count()} countries;");
}
Console.ReadLine();