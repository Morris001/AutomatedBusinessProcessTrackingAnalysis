using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;

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
		public string ScreenShot { get; set; }


		public Report()
		{

		}

		public Report(Event e, EventValues idt, string title, string screenShot)
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

			// get first IPv4 address found
			IPAddress[] addresses = Dns.GetHostAddresses(IPHostName);
			foreach(IPAddress ip4 in addresses.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork))
            {
				IPAddress = ip4.ToString();
				break;
            }
			OS = "Windows";
			Process = e.process ?? "";
			Url = e.url ?? "";
			Title = title ?? "";
			Duration = $"{idt.ts.Hours:00}:{idt.ts.Minutes:00}:{idt.ts.Seconds:00}";
			Idle = $"{idt.idle.Hours:00}:{idt.idle.Minutes:00}:{idt.idle.Seconds:00}";
			Active = $"{idt.active.Hours:00}:{idt.active.Minutes:00}:{idt.active.Seconds:00}";
			ScreenShot = screenShot ?? "";
		}
	}
}
