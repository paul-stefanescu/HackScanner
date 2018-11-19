using Newtonsoft.Json;
using NodaTime;
using SkyScanner.Services;
using SkyScanner.Settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLeapMotionEpicApp
{
    public class api
    {
        public static async Task<SkyScanner.Data.Itinerary> QueryFlights(string from, string to)
        {
            var scanner = new Scanner(ConfigurationManager.AppSettings["apiKey"]);
            var fromPlace = (await scanner.QueryLocation(from)).First();
            var toPlace = (await scanner.QueryLocation(to)).First();

            //Query flights
            var itineraries = await scanner.QueryFlight(
              new FlightQuerySettings(
                new FlightRequestSettings(
                  fromPlace, toPlace,
                  new LocalDate(2017, 11, 16), new LocalDate(2017, 11, 20)),
                new FlightResponseSettings(SortType.Price, SortOrder.Ascending)));

            var itinerary = itineraries.First();
            return itinerary;

        }

        /*public static void Main(string[] args)
        {
            try
            {
                var thingum = QueryFlights("London", "Toronto").Result;
                string json = JsonConvert.SerializeObject(thingum, Formatting.Indented);
                //var json = JavaScriptSerializer.Serialize(data);
                System.IO.File.WriteAllText(@"D:\Visual Studio Repository\skyscanner-test\api_output.txt", json);
                //Console.WriteLine(thingum);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }*/
    }
}
