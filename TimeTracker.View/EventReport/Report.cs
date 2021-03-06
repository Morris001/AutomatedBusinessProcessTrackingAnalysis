﻿using Newtonsoft.Json;
using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using TimeTracker.View.ScreenshotProcessing;

namespace TimeTracker.View
{
	public class Report
	{
		public string TimeStamp { get; set; }
		public string UserName { get; set; }
		public string MacAddress { get; set; }
		public string IPHostName { get; set; }
        public string IPAddress { get; set; }
		public string Id { get; set; }
		public string OS { get; set; }
		public string Process { get; set; }
		public string Url { get; set; }
		public string Title { get; set; }
		public string Duration { get; set; }
		public string Idle { get; set; }
		public string Active { get; set; }
		public string ScreenShotFileName { get; set; }
		public string ScreenShotBase64String { get; set; }
		public string ScreenShotOcrResult { get; set; }


		public Report()
		{

		}

		public Report(Event e, EventValues idt, string title, ScreenshotStruct screenshotStruct)
		{
			// todo: dynamic OS

			TimeStamp = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            Id = idt.entryId ?? "";
			UserName = Environment.UserName;
			MacAddress = 
				(
					from nic in NetworkInterface.GetAllNetworkInterfaces()
					where nic.OperationalStatus == OperationalStatus.Up
					select nic.GetPhysicalAddress().ToString()
				).FirstOrDefault();
            IPHostName = Dns.GetHostName();

			// get all ip addresses
			IPAddress[] addresses = Dns.GetHostAddresses(IPHostName);
			ArrayList addressStrings = new ArrayList();
			foreach(IPAddress ip in addresses)
            {
				addressStrings.Add(ip.ToString());
            }

			var ocrResult = Task<string>.Run( () => OcrEngine.asyncReadFromImage(screenshotStruct.ScreenshotFilePath));
			var base64Screenshot = Task<string>.Run(() => ScreenshotBase64Generator.JpegToBase64(screenshotStruct.ScreenshotFilePath));
			screenshotStruct.ScreenshotBase64String = base64Screenshot.Result;
			
			OS = "Windows";
			Process = e.process ?? "";
			IPAddress = JsonConvert.SerializeObject(addressStrings);
			Url = e.url ?? "";
			Title = title ?? "";
			Duration = $"{idt.ts.Hours:00}:{idt.ts.Minutes:00}:{idt.ts.Seconds:00}";
			Idle = $"{idt.idle.Hours:00}:{idt.idle.Minutes:00}:{idt.idle.Seconds:00}";
			Active = $"{idt.active.Hours:00}:{idt.active.Minutes:00}:{idt.active.Seconds:00}";
			ScreenShotFileName = screenshotStruct.ScreenshotFileName ?? "";
			ScreenShotBase64String = screenshotStruct.ScreenshotBase64String ?? "";
			ScreenShotOcrResult = ocrResult.Result ?? "";
		}
	}
}
