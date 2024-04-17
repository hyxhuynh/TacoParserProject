namespace LoggingKata
{
    /// <summary>
    /// Parses a POI file to locate all the Taco Bells
    /// </summary>
    public class TacoParser
    {
        readonly ILog logger = new TacoLogger();
        
        public ITrackable Parse(string line)
        {
            logger.LogInfo("Begin parsing");

            // Take your line and use line.Split(',') to split it up into an array of strings, separated by the char ','
            var cells = line.Split(',');

            // If your array's Length is less than 3, something went wrong
            if (cells.Length < 3)
            {
                // Log error message and return null
                return null; 
            }

            // Latitude from the array at index 0
            double latitude = double.Parse(cells[0]);
            
            // Longitude from the array at index 1
            double longtitude = double.Parse(cells[1]);
            
            // The store name from the array at index 2
            string name = cells[2];

            // Create an instance of the Point Struct
            // Set the values of the point (Latitude and Longitude) 
            Point navPoint = new Point();
            navPoint.Latitude = latitude;
            navPoint.Longitude = longtitude;

            // Create an instance of the TacoBell class
            // Set the values of the class (Name and Location)
            TacoBell store = new TacoBell();
            store.Name = name;
            store.Location = navPoint;

            // Return the instance of your TacoBell class
            return store;
        }
    }
}
