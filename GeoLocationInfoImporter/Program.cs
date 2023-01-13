// See https://aka.ms/new-console-template for more information

using System.Globalization;
using CsvHelper;
using GeoLocationInfoImporter;

Console.WriteLine("Hello, Importer!");

var countryList = new List<Country>();
var cityList = new List<City>();
var statesList = new List<State>();

using (var countriesReader = new StreamReader(@"..\..\..\Data\countries.csv"))
using (var citiesReader = new StreamReader(@"..\..\..\Data\cities.csv"))
using (var statesReader = new StreamReader(@"..\..\..\Data\states.csv"))
using (StreamWriter countriesWriter = new StreamWriter("countries_output.sql"))
using (StreamWriter citiesWriter = new StreamWriter("cities_output.sql"))
using (StreamWriter statesWriter = new StreamWriter("states_output.sql"))
using (var csvCountries = new CsvReader(countriesReader, CultureInfo.InvariantCulture))
using (var csvCities = new CsvReader(citiesReader, CultureInfo.InvariantCulture))
using (var csvStates = new CsvReader(statesReader, CultureInfo.InvariantCulture))
{
    countryList = csvCountries.GetRecords<Country>().ToList();
    Console.WriteLine($"We read {countryList.Count()} countries; - Done");

    cityList = csvCities.GetRecords<City>().ToList();
    cityList = cityList.Distinct(new CityComparer()).ToList();
    Console.WriteLine($"We read {cityList.Count()} cities; - Done");

    statesList = csvStates.GetRecords<State>().ToList();
    Console.WriteLine($"We read {statesList.Count()} states; - Done");

    foreach (var country in countryList)
    {
        countriesWriter.WriteLine($"INSERT INTO Country(Name, Code, IsoCode2Character )VALUES('{country.Name.Replace("'","''")}', '{country.Code}', '{country.IsoCode2Character}');");
    }

    foreach (var city in cityList)
    {
        if (city.IsNumericState)
        {
            citiesWriter.WriteLine($"INSERT INTO City(Name, CountryId )VALUES('{city.Name.Replace("'", @"''")}', ( select Id from Country where IsoCode2Character='{city.CountryCode}'));");
        }

        else
        {
            citiesWriter.WriteLine($"INSERT INTO City(Name, CountryId, StateProvinceId )VALUES('{city.Name.Replace("'", @"''")}', ( select Id from Country where IsoCode2Character='{city.CountryCode}') , (select Id from StateProvince where Code='{city.StateCode}'));");
        }

        
    }

    foreach (var state in statesList)
    {
        statesWriter.WriteLine($"INSERT INTO StateProvince(Name, Code, CountryId )VALUES('{state.Name.Replace("'", @"''")}', '{state.Code}', (select Id from Country where IsoCode2Character='{state.CountryCode}'));");
    }

}
Console.ReadLine();