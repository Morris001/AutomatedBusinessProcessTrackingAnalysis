using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
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
        public String readFromImage(string imagePath) { // OCR_Space  API  (Image to Text)
            try {
                TesseractEngine tesseractEngineInstance = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
                Pix img = Pix.LoadFromFile(imagePath); //Change to var, maybe?
                Page page = tesseractEngineInstance.Process(img);
                String text = page.GetText();
                return text;
            } catch (Exception e) {
                return ("Unable to read " + imagePath + ". Reason: " + e.ToString());
            }
    }

        public void writeToFile(String filepath, String text) {
            //Open file from given filepath, insert text in it
            //System.IO.File.WriteAllLines(@"C:\Users\Public\TestFolder\WriteLines.txt", lines);     //  Output to text file (For Testing Purposes)
            using (StreamWriter stream = new StreamWriter(outputPath + path, true)) {
                stream.WriteLine(output);
            }
        }
    }
}