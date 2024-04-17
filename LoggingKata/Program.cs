using System;
using System.Linq;
using System.IO;
using GeoCoordinatePortable;

namespace LoggingKata
{
    class Program
    {
        static readonly ILog logger = new TacoLogger();
        const string csvPath = "TacoBell-US-AL.csv";

        static void Main(string[] args)
        {
            // Objective: Find the two Taco Bells that are the farthest apart from one another.

            logger.LogInfo("Log initialized");

            // Use File.ReadAllLines(path) to grab all the lines from your csv file. 
            var lines = File.ReadAllLines(csvPath);

            // Log an error if you get 0 lines and a warning if you get 1 line
            if (lines.Length == 0)
            {
                logger.LogError("Zero line from the csv file");
            }
            if (lines.Length == 1)
            {
                logger.LogWarning("Only one line from the csv file");
            }

            // This will display the first item in your lines array
            logger.LogInfo($"Lines: {lines[0]}");

            // Create a new instance of your TacoParser class
            TacoParser parser = new TacoParser();

            // Use the Select LINQ method to parse every line in lines collection
            ITrackable[] locations = lines.Select(parser.Parse).ToArray();
            //Console.WriteLine("Array of locations");
            //foreach ( var loc in locations)
            //{
            //    Console.WriteLine(loc.Name);
            //}
  
            // Store two Taco Bells that are the farthest from each other.
            ITrackable tacoBell1 = null;
            ITrackable tacoBell2 = null;

            //  Store the distance between two TacoBell until the largest distance in meters is found
            double distanceInMeters = 0;



            // First loop to go through each item in the collection of locations
            // This loop will let you select one location at a time to act as the "starting point" or "origin" location.
            for (int i = 0; i < locations.Length; i++)
            {
                ITrackable locA = locations[i];
                // Create a new Coordinate object called `corA` with your locA's latitude and longitude.
                GeoCoordinate corA = new GeoCoordinate(locA.Location.Latitude, locA.Location.Longitude);

                // Second loop to iterate through locations again.
                // This allows you to pick a "destination" 
                for (int j = 0; j < locations.Length; j++)
                {
                    // Create a new Coordinate object called `corB` with your locB's latitude and longitude.
                    ITrackable locB = locations[j];
                    GeoCoordinate corB = new GeoCoordinate(locB.Location.Latitude, locB.Location.Longitude);

                    // Compare the two locations using `.GetDistanceTo()` method, which returns a double.
                    double distanceChecker = corA.GetDistanceTo(corB);

                    // If the distance is greater than the currently saved distance, update the distance variable and the two `ITrackable` variables above.
                    if (distanceChecker > distanceInMeters)
                    {
                        distanceInMeters = distanceChecker;
                        tacoBell1 = locA;
                        tacoBell2 = locB;
                    }
                }
            }
            
            // Display to the console 
            Console.WriteLine("\nTwo Taco Bells farthest away from each other:");
            Console.WriteLine(tacoBell1.Name);
            Console.WriteLine(tacoBell2.Name);
            Console.WriteLine($"They are {Math.Round(distanceInMeters/1609.34, 2)} miles apart."); // 1 mile = 1609.34 meters
        }
    }
}
