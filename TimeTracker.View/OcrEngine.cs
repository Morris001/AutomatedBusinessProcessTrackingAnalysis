using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace TimeTracker.View
{
    /*
     * Wrapper used: https://github.com/charlesw/tesseract/
     * Sample code for seeing how to use library: https://github.com/charlesw/tesseract-samples
     * 
     * Workflow explained in order --
     * Constructor's contents and explanation:
     * This class will, upon creation, create a directory for output files and contain 2 path variables.
     * One path variable is for screencaps and the other is for the text files this class makes (which is the created directory).
     * These will be set in the constructor. 
     * 
     *      Tangent: There shouldn't even be a need for supplying arguments to the constructor, we should get 
     *      values from a project-wide constants file. We need to make that though (so for now we have a constructor which requires arguments).
     * 
     * Functions, their contents and explanation: 
     * There will be 2 functions other than basic getters/setters as applicable. A read function and a writeToFile function.
     * 
     * The read function will load an image using the filepath as an argument and perform the OCR process on it. 
     * It will then return a string, which will be passed into the writetoFile function.
     * 
     * The writeToFile function will write the contents to a .txt file which shares the name of the image Tesseract read the text from.
     * 
     */
    class OcrEngine
    {
        private String folderWithScreenshotsPath;
        private String outputDirectoryPath;

        public OcrEngine(String folderWithScreenshotsPath, String outputDirectoryPath) {
            this.folderWithScreenshotsPath = folderWithScreenshotsPath;
            this.outputDirectoryPath = outputDirectoryPath;
        }
        
        public static String asyncReadFromImage(string imagePath) {
            //var TESSDATA_PREFIX = ;
            try {
                //NOTE: This filepath needs to be altered for it to work on your machine, change it to where your .tessdata folder (which contains the eng.traineddata file in this project's directory) is
                //We need to have this installed in ProgramFiles via the .msi so there can be a fixed location.
                FileSystemWatcher fileCreationWatcher = new FileSystemWatcher();
                fileCreationWatcher.Path = Path.GetDirectoryName(imagePath);
                fileCreationWatcher.Filter = Path.GetFileName(imagePath);
                TesseractEngine tesseractEngineInstance = new TesseractEngine(@"C:\DEV\AutomatedBusinessProcessTrackingAnalysis\TimeTracker.View\tessdata", "eng", EngineMode.Default);
                Pix img = Pix.LoadFromFile(imagePath); //Change to var, maybe?
                Page page = tesseractEngineInstance.Process(img);
                String text = page.GetText();
                return text;
            } catch (Exception e) {
                return ("Unable to read " + imagePath + ". Reason: " + e.ToString());
            }
        }
        
        public void readScreenshots() {
            foreach (String imageFilePath in Directory.GetFiles(this.folderWithScreenshotsPath)) //iterate over every file in captures folder and generate a corresponding text file in the /OCR folder
            {
                string output = this.readFromImage(imageFilePath);
                int index = imageFilePath.IndexOf("/Captures/") + 10;
                int index2 = imageFilePath.IndexOf(".jpeg");
                int length = index2 - index;
                string textOutputPath = this.outputDirectoryPath + imageFilePath.Substring(index, length - 1) + ".txt";
                this.writeToFile(textOutputPath, output);
            }
        }

        public String readFromImage(string imagePath) { // Make this asynchronous
            //var TESSDATA_PREFIX = ;
            try {
                //NOTE: This filepath needs to be altered for it to work on your machine, change it to where your .tessdata folder (which contains the eng.traineddata file in this project's directory) is
                //We need to have this installed in ProgramFiles via the .msi so there can be a fixed location.
                TesseractEngine tesseractEngineInstance = new TesseractEngine(@"C:\DEV\AutomatedBusinessProcessTrackingAnalysis\TimeTracker.View\tessdata", "eng", EngineMode.Default);
                Pix img = Pix.LoadFromFile(imagePath); //Change to var, maybe?
                Page page = tesseractEngineInstance.Process(img);
                String text = page.GetText();
                return text;
            } catch (Exception e) {
                return ("Unable to read " + imagePath + ". Reason: " + e.ToString());
            }
    }

        public void writeToFile(String filepath, String text) { //Should we make this asynchronous?

            try {
                if (!File.Exists(filepath)) {
                    File.WriteAllText(filepath, text); //Shouldn't be writing to the same file more than once
                }
            } catch (Exception e) {
                return; //Come up with something to do if writing to a file fails later
            }
        }
    }
}