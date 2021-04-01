using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using Tesseract;

namespace TimeTracker.View
{
    //Wrapper used: https://github.com/charlesw/tesseract/
    //Sample code: https://github.com/charlesw/tesseract-samples
    class OcrEngine
    {
        public static string OCRSpace_API_Call(string image) // OCR_Space  API  (Image to Text)
        {
            //OcrSpaceResult result = JsonConvert.DeserializeObject<OcrSpaceResult>(responseString);      //  This returns the JSON object (In Theory)

            //if ((!Result.IsErroredOnProcessing) && !String.IsNullOrEmpty(result.ParsedResults[0].ParsedText))
            // return result.ParsedResults[0].ParsedText;
            //System.IO.File.WriteAllLines(@"C:\Users\Public\TestFolder\WriteLines.txt", lines);     //  Output to text file (For Testing Purposes)
            return responseString;     //  Returns the OCR Text results as a string value
        }
    }
}