using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using Newtonsoft.Json;
using Microsoft.ML.Data;
using Microsoft.ML;
using XPlot.Plotly;

namespace TimeTracker.View.DataModels
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
            using (StreamReader r = new StreamReader("/Users/salwahaider/Desktop/AutomatedBusinessProcessTrackingAnalysis-GDrive_upload/Output2020_11_13.json"))
            {
                var json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<Analysis>>(json);

                Console.WriteLine("{0} {1}", "TimeStamp", "Duration");
                foreach (var item in items)
                {
                    Console.WriteLine("{0} {1}", item.TimeStamp, item.Duration);
                }
            }
        }
    }
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
