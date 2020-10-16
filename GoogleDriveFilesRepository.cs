using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Drive.v2.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;
using System.Web;

namespace GoogleDriveRestAPI_v3.Models
{
    public class GoogleDriveFilesRepository
    {
        public static string[] Scopes = { Google.Apis.Drive.v3.DriveService.Scope.Drive };
        public static Google.Apis.Drive.v3.DriveService GetService_v3()
        {
            UserCredential credential;
            using (var stream = new FileStream(@"F:\Git Repos\TimeTracker.View\client_secret.json", FileMode.Open, FileAccess.Read))//CHANGE SO IT GETS LOCAL FILE PATH
            {
                String FolderPath = @"F:\Git Repos\TimeTracker.View\";
                String FilePath = Path.Combine(FolderPath, "DriveServiceCredentials.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(FilePath, true)).Result;
            }

            //Create Drive API service.
            Google.Apis.Drive.v3.DriveService service = new Google.Apis.Drive.v3.DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "GoogleDriveRestAPI-v3",
            });

            return service;
        }

        public static Google.Apis.Drive.v2.DriveService GetService_v2()
        {
            UserCredential credential;
            using (var stream = new FileStream(@"F:\Git Repos\TimeTracker.View\client_secret.json", FileMode.Open, FileAccess.Read))//CHANGE SO IT GETS LOCAL FILE PATH
            {
                String FolderPath = @"F:\Git Repos\TimeTracker.View\";
                String FilePath = Path.Combine(FolderPath, "DriveServiceCredentials.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(FilePath, true)).Result;
            }

            //Create Drive API service.
            Google.Apis.Drive.v2.DriveService service = new Google.Apis.Drive.v2.DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "GoogleDriveRestAPI-v2",
            });
            return service;
        }
        
        public static List<GoogleDriveFiles> GetContainsInFolder(String folderId)
        {
            List<string> ChildList = new List<string>();
            Google.Apis.Drive.v2.DriveService ServiceV2 = GetService_v2();
            ChildrenResource.ListRequest ChildrenIDsRequest = ServiceV2.Children.List(folderId);
            do
            {
                ChildList children = ChildrenIDsRequest.Execute();

                if (children.Items != null && children.Items.Count > 0)
                {
                    foreach (var file in children.Items)
                    {
                        ChildList.Add(file.Id);
                    }
                }
                ChildrenIDsRequest.PageToken = children.NextPageToken;

            } while (!String.IsNullOrEmpty(ChildrenIDsRequest.PageToken));

            //Get All File List
            List<GoogleDriveFiles> AllFileList = GetDriveFiles();
            List<GoogleDriveFiles> Filter_FileList = new List<GoogleDriveFiles>();

            foreach (string Id in ChildList)
            {
                Filter_FileList.Add(AllFileList.Where(x => x.Id == Id).FirstOrDefault());
            }
            return Filter_FileList;
        }
        
        
        public static void CreateFolder(string FolderName)
        {
            Google.Apis.Drive.v3.DriveService service = GetService_v3();

            var FileMetaData = new Google.Apis.Drive.v3.Data.File();
            FileMetaData.Name = FolderName;
            FileMetaData.MimeType = "application/vnd.google-apps.folder";

            Google.Apis.Drive.v3.FilesResource.CreateRequest request;

            request = service.Files.Create(FileMetaData);
            request.Fields = "id";
            var file = request.Execute();
            Console.WriteLine("Folder ID: " + file.Id);
        }
        
        
        public static List<GoogleDriveFiles> GetDriveFiles()
        {
            Google.Apis.Drive.v3.DriveService service = GetService_v3();

            // Define parameters of request.
            Google.Apis.Drive.v3.FilesResource.ListRequest FileListRequest = service.Files.List();
            FileListRequest.Fields = "nextPageToken, files(createdTime, id, name, size, version, trashed, parents)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = FileListRequest.Execute().Files;
            List<GoogleDriveFiles> FileList = new List<GoogleDriveFiles>();

            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    GoogleDriveFiles File = new GoogleDriveFiles
                    {
                        Id = file.Id,
                        Name = file.Name,
                        Size = file.Size,
                        Version = file.Version,
                        CreatedTime = file.CreatedTime,
                        Parents = file.Parents
                    };
                    FileList.Add(File);
                }
            }
            return FileList;
        }

        public static void FileUpload(string file)
        {
            using (System.IO.StreamWriter logging =
            new System.IO.StreamWriter(@"C:\Users\Johan Terblanche\Desktop\logger.txt"))
            {
                logging.WriteLine("Alive?\n");
            }

            if (file != null)
            {
                Google.Apis.Drive.v3.DriveService service = GetService_v3();

                string path = "C:\\Users\\Johan Terblanche\\AppData\\Roaming\\Logs\\" + file;
                var FileMetaData = new Google.Apis.Drive.v3.Data.File();
                FileMetaData.Name = Path.GetFileName(file);
                FileMetaData.MimeType = MimeMapping.GetMimeMapping(path);


                using (System.IO.StreamWriter logging =
                new System.IO.StreamWriter(@"C:\Users\Johan Terblanche\Desktop\logger1.txt"))
                {
                    logging.WriteLine("uploading?\n");
                }

                Google.Apis.Drive.v3.FilesResource.CreateMediaUpload request;

                using (var stream = new System.IO.FileStream("C:\\Users\\Johan Terblanche\\AppData\\Roaming\\Logs\\Output2020_09_03.csv", System.IO.FileMode.Open))
                {
                    request = service.Files.Create(FileMetaData, stream, FileMetaData.MimeType);
                    request.Fields = "id";
                    request.Upload();
                }
                using (System.IO.StreamWriter logging =
                new System.IO.StreamWriter(@"C:\Users\Johan Terblanche\Desktop\logger2.txt"))
                    {
                        logging.WriteLine("success??\n");
                    }
            }
        }
    }
}