using System;

namespace TimeTracker.View.ScreenshotProcessing
{
    public struct ScreenshotStruct
    {
        public ScreenshotStruct(String screenshotFileName, String screenshotBase64String)
        {
            ScreenshotFileName = screenshotFileName;
            ScreenshotBase64String = screenshotBase64String;
        }

        public String ScreenshotFileName { get; }
        public String ScreenshotBase64String { get; }
    }
}