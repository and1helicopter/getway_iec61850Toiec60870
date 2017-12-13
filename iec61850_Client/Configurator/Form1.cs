using System;
using System.IO;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;

namespace Configurator
{
	public partial class Form1 : Form
	{
		private ChromiumWebBrowser _chromeBrowser;

		public Form1()
		{
			InitializeComponent();

			InitializeChromium();
		}

		public void InitializeChromium()
		{
			CefSettings settings = new CefSettings();
			settings.SetOffScreenRenderingBestPerformanceArgs();
			DoubleBuffered = true;

			// Note that if you get an error or a white screen, you may be doing something wrong !
			// Try to load a local file that you're sure that exists and give the complete path instead to test
			// for example, replace page with a direct path instead :
			// String page = @"C:\Users\SDkCarlos\Desktop\afolder\index.html";

			string _page = string.Format($"{ Application.StartupPath}\\view\\index.html");
			//String page = @"C:\Users\SDkCarlos\Desktop\artyom-HOMEPAGE\index.html";

			if (!File.Exists(_page))
			{
				MessageBox.Show(@"Error The html file doesn't exists : " + _page);
			}

			// Initialize cef with the provided settings
			Cef.Initialize(settings);
			// Create a browser component
			_chromeBrowser = new ChromiumWebBrowser(_page);
			
			// Add it to the form and fill it to the form window.
			Panel.Controls.Add(_chromeBrowser);
			_chromeBrowser.Dock = DockStyle.Fill;

			// Allow the use of local resources in the browser
			BrowserSettings browserSettings = new BrowserSettings
			{
				FileAccessFromFileUrls = CefState.Enabled,
				UniversalAccessFromFileUrls = CefState.Enabled
			};
			_chromeBrowser.BrowserSettings = browserSettings;
			
			_chromeBrowser.RegisterJsObject("cefCustomObject", new CefCustomObject(_chromeBrowser, this));
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			//_chromeBrowser.ShowDevTools();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			Cef.Shutdown();
		}
	}
}
