using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace Time.View
{
    class Analysis
    {
        List<Analysis> items = new List<Analysis>();
        //string path = "/Users/salwahaider/Desktop/AutomatedBusinessProcessTrackingAnalysis-GDrive_upload/Output2020_11_13.json";
        public string TimeStamp { get; set; }
        public string Id { get; set; }
        public string OS { get; set; }
        public string Process { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Duration { get; set; }
        public string Idle { get; set; }
        public string Active { get; set; }
        public string ScreenShot { get; set; }

        static void Main(string[] args)
        {
            LoadJson();
        }

        public static void LoadJson()
        {
            var userpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var logPath = userpath + "/Analysis/";

            using (StreamReader r = new StreamReader("/Users/salwahaider/Desktop/AutomatedBusinessProcessTrackingAnalysis-GDrive_upload/Output2020_11_13.json"))
            {
                var json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<Analysis>>(json);
                int total_duration = 0;
                int count = 0;
                int idle_time = 0;
                int active_time = 0;
                int average_duration = 0;
                int average_idle = 0;
                int average_active = 0;

                foreach (var item in items)
                {
                    using (StreamWriter outputFile = new StreamWriter(Path.Combine(logPath, "AnalysisOutput.txt")))
                    {
                        foreach (var lines in items)
                        {
                            outputFile.WriteLine("{0} {1}", item.TimeStamp, item.Duration);
                            total_duration += int.Parse(item.Duration);
                            active_time += int.Parse(item.Active);
                            idle_time += int.Parse(item.Idle);
                            count += 1;
                            outputFile.WriteLine("Total Duration: ", total_duration, " Number of users: ", count);
                            average_duration = total_duration / count;
                            average_idle = idle_time / count;
                            average_active = active_time / count;
                            outputFile.WriteLine("Average Duration: ", average_duration);
                            outputFile.WriteLine("Active Time: ", item.Active, " Idle Time: ", item.Idle);
                            outputFile.WriteLine("Average Active Time: ", average_active, " Average Idle Time: ", average_idle);

                            if (average_active < average_idle)
                            {
                                outputFile.WriteLine("Average Active Time is greater than Average Idle time.");
                            }
                        }
                    }
                }
            }
        }
    }
}


/*public class SimpleAnalysis
{
    [ColumnName("Timestamp"), LoadColumn(0)]
    public string TimeStamp { get; set; }

    [ColumnName("Duration"), LoadColumn(1)]
    public string Duration { get; set; }

    [ColumnName("Active"), LoadColumn(2)]
    public string Active { get; set; }

    [ColumnName("Idle"), LoadColumn(3)]
    public string Idle { get; set; }

}

public class DataPrediction
{
    [ColumnName("Predicted Table")]
    public bool Prediction { get; set; }

}
*/

/*using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.ML.Data;
using Microsoft.ML;
using XPlot.Plotly;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using System.Windows.Forms.VisualStyles;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using MongoDB.Bson;

namespace TimeTracker.View
{
    class Analysis
    {
        List<Analyze> items = new List<Analyze>();
        //string path = "/Users/salwahaider/Desktop/AutomatedBusinessProcessTrackingAnalysis-GDrive_upload/Output2020_11_13.json";

        static void Main(string[] args)
        {
            LoadJson();
        }

        public class Analyze
        {
            public string TimeStamp { get; set; }
            public string Id { get; set; }
            public string OS { get; set; }
            public string Process { get; set; }
            public string Url { get; set; }
            public string Title { get; set; }
            public string Duration { get; set; }
            public string Idle { get; set; }
            public string Active { get; set; }
            public string ScreenShot { get; set; }

            //public override string ToString()
            //{
            //   return $"{TimeStamp}, {Id}, {Duration}, {Active}";
            //}
        }

        public static void LoadJson()
        {
            var client = new MongoClient("mongodb+srv://admin:admin@cluster0.oqexz.mongodb.net/group1db?retryWrites=true&w=majority");
            var database = client.GetDatabase("group1db");
            var fs = new GridFSBucket(database);
            var collecFiles = database.GetCollection<BsonDocument>("fs.files");
            var filter = Builders<GridFSFileInfo>.Filter.And(Builders<GridFSFileInfo>.Filter.Regex(x => x.Filename, "json"));
            var list = fs.Find(filter).ToList(); //all json files stored
            var userpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var analysisPath = userpath + "/Analysis/";
            //var files = Directory.GetFiles(analysisPath);

            foreach (String filePath in Directory.GetFiles(analysisPath))
            {
                JsonTextReader reader = new JsonTextReader(new StreamReader(filePath));
                using (var stream = File.OpenRead(filePath))
                {
                    int index = filePath.IndexOf("/Analysis/");
                    string filename = filePath.Substring(index + 10);
                    var content = reader.Read();
                    var items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Analysis>>(content);

                    foreach (var item in items)
                    {

                    }
            }


        /*public static void LoadJson()
        {
            var client = new MongoClient("mongodb+srv://admin:admin@cluster0.oqexz.mongodb.net/group1db?retryWrites=true&w=majority");//personal mongoDB account made to test after issues with provided one
            var database = client.GetDatabase("group1db");
            var fs = new GridFSBucket(database);
            var userpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var logPath = userpath + "/Analysis/";

            foreach (String filePath in Directory.GetFiles(logPath))
            {
                using (StreamReader r = new StreamReader(filePath))
                {
                    var json = r.ReadToEnd();
                    var items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Analysis>>(json);
                }
            }

            var capPath = userpath + "/Analysis/";

            using (var stream = File.OpenRead(filePath))
            Console.WriteLine("{0} {1}", "TimeStamp", "Duration");
                foreach (var item in items)
                {
                    Console.WriteLine("{0} {1}", item.TimeStamp, item.Duration);
                }
            }
        }
    }
    /*
}

public class SimpleAnalysis
{
    [ColumnName("Timestamp"), LoadColumn(0)]
    public string TimeStamp { get; set; }

    [ColumnName("Duration"), LoadColumn(1)]
    public string Duration { get; set; }

    [ColumnName("Active"), LoadColumn(2)]
    public string Active { get; set; }

    [ColumnName("Idle"), LoadColumn(3)]
    public string Idle { get; set; }

}

public class DataPrediction
{
    [ColumnName("Predicted Table")]
    public bool Prediction { get; set; }

}
*/
