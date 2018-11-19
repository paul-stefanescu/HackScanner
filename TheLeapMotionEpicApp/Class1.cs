using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TheLeapMotionEpicApp
{
    public class Hackathon
    {
        public string Hackathon_date { get; set; }
        public string Hackathon_name { get; set; }
        public string Hackathon_location { get; set; }
    }
    public class Flight
    {
        public string DeeplinkUrl { get; set; }
        public string Price { get; set; }
    }
    public class Parser
    {
        public static Hackathon Start(int i)
        {
            //try
            //{
                string read = File.ReadAllText(@"D:\Visual Studio Repository\itworks.json");
                //string read = @"{""hackathon_date"": ""Nov 11 \u2013 12, 2017"", ""hackathon_name"": ""Hack Infinity"", ""hackathon_location"": ""Gandhinagar, GJ, IN""}";
                var JSONObjList = JsonConvert.DeserializeObject<List<Hackathon>>(read);
            return JSONObjList[i];
                //var currentObj = JSONObjList[0];
                /*Console.WriteLine(currentObj.Hackathon_date);
                Console.WriteLine(currentObj.Hackathon_name);
                Console.WriteLine(currentObj.Hackathon_location);

                string read2 = File.ReadAllText(@"D:\Visual Studio Repository\skyscanner-test\api_output.json");
                var JSONObjList2 = JsonConvert.DeserializeObject<List<Flight>>(read);
                var currentFlight = JSONObjList2[0];
                Console.WriteLine(currentFlight.DeeplinkUrl);
                Console.WriteLine(currentFlight.Price);
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
            Console.Read();*/
            }
    }
}
