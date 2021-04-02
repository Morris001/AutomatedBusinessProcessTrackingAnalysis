using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using System;
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
     * The writeToFile function will 
     * 
     */
    class OcrEngine
    {
        public void readFromImage(string imagePath) // OCR_Space  API  (Image to Text)
        {
            //OcrSpaceResult result = JsonConvert.DeserializeObject<OcrSpaceResult>(responseString);      //  This returns the JSON object (In Theory)

            //if ((!Result.IsErroredOnProcessing) && !String.IsNullOrEmpty(result.ParsedResults[0].ParsedText))
            // return result.ParsedResults[0].ParsedText;
            //System.IO.File.WriteAllLines(@"C:\Users\Public\TestFolder\WriteLines.txt", lines);     //  Output to text file (For Testing Purposes)
        }

        public void writeToFile() {
            //Insert stuff here
        }
    }
}