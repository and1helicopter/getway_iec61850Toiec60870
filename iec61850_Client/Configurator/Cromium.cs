using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CefSharp;
using CefSharp.WinForms;
using Gateway;

namespace Configurator
{
	class CefCustomObject
	{
		// Declare a local instance of chromium and the main form in order to execute things from here in the main thread
		private static ChromiumWebBrowser _instanceBrowser = null;
		// The form class needs to be changed according to yours
		private static Form1 _instanceMainForm = null;


		public CefCustomObject(ChromiumWebBrowser originalBrowser, Form1 mainForm)
		{
			_instanceBrowser = originalBrowser;
			_instanceMainForm = mainForm;
		}

		public void showDevTools()
		{
			_instanceBrowser.ShowDevTools();
		}

		public dynamic startServer61850(dynamic obj, dynamic status, dynamic index)
		{
			if (status)
			{
				if (!GatewayAPI_Class.Start_Server61850(obj, index))
				{
					//Соединение не может быть установлено
					return false;
				}
				//Получение списка 
				var listItems = GatewayAPI_Class.Get_Items_Server61850(index);
				//Отправка даннных в форму
				return listItems;
			}
			else
			{
				return GatewayAPI_Class.Stop_Server61850(index);
			}
		}

		public dynamic addServer61850(dynamic obj)
		{
			return GatewayAPI_Class.Add_Server61850(obj);
		}

		public dynamic removeServer61850(dynamic index)
		{
			return GatewayAPI_Class.Remove_Server61850(index);
		}

		public dynamic saveToFile(dynamic file)
		{
		    SaveFileDialog sfd = new SaveFileDialog
		    {
		        DefaultExt = @".json",
		    };
		    if (sfd.ShowDialog() == DialogResult.OK)
		    {
		        FileStream fs = new FileStream(sfd.FileName, FileMode.Create);

            }
		    return false;
		}
		
		//public void opencmd()
		//{
		//	ProcessStartInfo start = new ProcessStartInfo("cmd.exe", "/c pause");
		//	Process.Start(start);
		//}
	}
}
