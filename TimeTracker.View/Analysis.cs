using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using Newtonsoft.Json;


namespace TimeTracker.View
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


        public static void LoadJson()
        {
            var userpath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var logPath = userpath + "/Analysis/";

            using (StreamReader r = new StreamReader(Directory.GetFiles(logPath)[1]))
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