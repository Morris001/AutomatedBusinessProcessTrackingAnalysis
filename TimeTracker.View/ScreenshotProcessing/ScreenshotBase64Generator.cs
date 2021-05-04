using System;
using System.IO;

namespace TimeTracker.View.ScreenshotProcessing
{
    public class ScreenshotBase64Generator
    {
        public static String JpegToBase64(String filepath)
        {
            return Convert.ToBase64String(File.ReadAllBytes(filepath));;
        }
    }
}