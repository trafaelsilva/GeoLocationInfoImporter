// See https://aka.ms/new-console-template for more information

using System.Globalization;
using CsvHelper;
using GeoLocationInfoImporter;

Console.WriteLine("Hello, Importer!");

var countryList = new List<Country>();
var cityList = new List<City>();

using (var countriesReader = new StreamReader(@"..\..\..\Data\countries.csv"))
using (var citiesReader = new StreamReader(@"..\..\..\Data\cities.csv"))
using (StreamWriter countriesWriter = new StreamWriter("countries_output.txt"))
using (StreamWriter citiesWriter = new StreamWriter("cities_output.txt"))
using (var csvCountries = new CsvReader(countriesReader, CultureInfo.InvariantCulture))
using (var csvCities = new CsvReader(citiesReader, CultureInfo.InvariantCulture))
{
    countryList = csvCountries.GetRecords<Country>().ToList();
    Console.WriteLine($"We read {countryList.Count()} countries; - Done");

    cityList = csvCities.GetRecords<City>().ToList();
    cityList = cityList.Distinct(new CityComparer()).ToList();
    Console.WriteLine($"We read {cityList.Count()} cities; - Done");

    foreach (var country in countryList)
    {
        countriesWriter.WriteLine($"INSERT INTO Country(Name, Code, IsoCode2Character)VALUES('{country.Name}', '{country.Code}', '{country.IsoCode2Character}');");
    }

    foreach (var city in cityList)
    {
        citiesWriter.WriteLine($"INSERT INTO City(Name, CountryId)VALUES('{city.Name}', select Id from Country where IsoCode2Character='{city.CountryCode}');");
    }

}
Console.ReadLine();