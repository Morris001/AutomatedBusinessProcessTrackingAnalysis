using System;

namespace TimeTracker.View.ScreenshotProcessing
{
    public struct ScreenshotStruct
    {
        public ScreenshotStruct(String screenshotFileName, String filepath, String screenshotBase64String)
        {
            ScreenshotFileName = screenshotFileName;
            ScreenshotFilePath = filepath;
            ScreenshotBase64String = screenshotBase64String;
        }

        public String ScreenshotFileName { get; }
        public String ScreenshotFilePath { get;  }
        public String ScreenshotBase64String { get; set; }
    }
}