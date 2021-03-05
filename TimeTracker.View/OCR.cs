using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace TimeTracker.View
{

    //NOTE: Windows seems to have its own OCR api. This is making HTTP calls to a third-party api and returning 403s. We need to do client-side processing for
    //Faster results and not depend on other peoples' APIs. See: https://medium.com/dataseries/using-windows-10-built-in-ocr-with-c-b5ca8665a14e
    //https://docs.microsoft.com/en-us/samples/microsoft/windows-universal-samples/ocr/
    class OCR
    {
        public static string Get(string uri,string image)
        {
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            var postData = "apikey=helloworld";
            postData += "&base64Image=" + image;
            var data = Encoding.ASCII.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }
            var response = (HttpWebResponse)request.GetResponse();

            //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            //using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }

        public static string OCRSpace_API_Call(string image)           // OCR_Space  API  (Image to Text)
        {
            
            string uri = $"https://api.ocr.space/parse/image";           // {imageUri}";
            string responseString = Get(uri,image);             //       WebUtilities.DoGetRequest(uri);

            //OcrSpaceResult result = JsonConvert.DeserializeObject<OcrSpaceResult>(responseString);      //  This returns the JSON object (In Theory)

            //if ((!Result.IsErroredOnProcessing) && !String.IsNullOrEmpty(result.ParsedResults[0].ParsedText))
            // return result.ParsedResults[0].ParsedText;
            //System.IO.File.WriteAllLines(@"C:\Users\Public\TestFolder\WriteLines.txt", lines);     //  Output to text file (For Testing Purposes)
            return responseString;     //  Returns the OCR Text results as a string value
        }
    }
}

/******************************************************************************************************************************************

namespace ShareX.UploadersLib.OtherServices
{
    public enum OCRSpaceLanguages
    {
        [Description("Czech")]
        ce,
        [Description("Danish")]
        dan,
        [Description("Dutch")]
        dut,
        [Description("English")]
        eng,
        [Description("Finnish")]
        fin,
        [Description("French")]
        fre,
        [Description("German")]
        ger,
        [Description("Hungarian")]
        hun,
        [Description("Italian")]
        ita,
        [Description("Norwegian")]
        nor,
        [Description("Polish")]
        pol,
        [Description("Portuguese")]
        por,
        [Description("Spanish")]
        spa,
        [Description("Swedish")]
        swe,
        [Description("Chinese Simplified")]
        chs,
        [Description("Greek")]
        gre,
        [Description("Japanese")]
        jpn,
        [Description("Russian")]
        rus,
        [Description("Turkish")]
        tur,
        [Description("Chinese Traditional")]
        cht,
        [Description("Korean")]
        kor
    }

    public class OCRSpace : OCR        //  Inherited from the Uploading Class
    {
        private const string APIURLFree = "https://api.ocr.space/parse/image";
        private const string APIURLUSA = "?";
        private const string APIURLEurope = "https://apipro3.ocr.space/parse/image"; // Frankfurt
        private const string APIURLAsia = "https://apipro8.ocr.space/parse/image"; // Tokyo

        public OCRSpaceLanguages Language { get; set; } = OCRSpaceLanguages.eng;
        public bool Overlay { get; set; }

        public OCRSpace(OCRSpaceLanguages language = OCRSpaceLanguages.eng, bool overlay = false)
        {
            Language = language;
            Overlay = overlay;
        }

        public OCRSpaceResponse DoOCR(Stream stream, string fileName)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();
            arguments.Add("apikey", APIKeys.OCRSpaceAPIKey);
            //arguments.Add("url", "");
            arguments.Add("language", Language.ToString());
            arguments.Add("isOverlayRequired", Overlay.ToString());

            UploadResult ur = UploadData(stream, APIURLEurope, fileName, "file", arguments);

            if (ur.IsSuccess)
            {
                return JsonConvert.DeserializeObject<OCRSpaceResponse>(ur.Response);
            }

            return null;
        }
    }

    public class OCRSpaceResponse
    {
        public List<OCRSpaceParsedResult> ParsedResults { get; set; }
        public int OCRExitCode { get; set; }
        public bool IsErroredOnProcessing { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
        public string ProcessingTimeInMilliseconds { get; set; }
    }

    public class OCRSpaceParsedResult
    {
        public OCRSpaceTextOverlay TextOverlay { get; set; }
        public int FileParseExitCode { get; set; }
        public string ParsedText { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorDetails { get; set; }
    }

    public class OCRSpaceTextOverlay
    {
        public List<OCRSpaceLine> Lines { get; set; }
        public bool HasOverlay { get; set; }
        public string Message { get; set; }
    }

    public class OCRSpaceLine
    {
        public List<OCRSpaceWord> Words { get; set; }
        public int MaxHeight { get; set; }
        public int MinTop { get; set; }
    }

    public class OCRSpaceWord
    {
        public string WordText { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}

 */
