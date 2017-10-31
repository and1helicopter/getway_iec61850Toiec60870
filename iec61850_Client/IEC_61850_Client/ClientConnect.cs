using IEC61850.Client;
using  IEC61850.Common;
using System.Collections.Generic;

namespace IEC_61850
{
	public class ClientConnect
	{


		private IedConnection _connection = new IedConnection();

		private string _hostname;
		private int _port;

		private IsoConnectionParameters _parameters;

		public void DefineConnection(string hostname, int port)
		{
			_hostname = hostname;
			_port = port;
		}

		public void StartConnection()
		{
			_connection.Connect(_hostname, _port);
		}

		public void DefinePassword(string password)
		{
			_parameters = _connection.GetConnectionParameters();
			_parameters.UsePasswordAuthentication(password);
		}

		public void DefineLocalAddresses(uint pSelector, byte[] sSelector, byte[] tSelector)
		{
			_parameters = _connection.GetConnectionParameters();
			_parameters.SetLocalAddresses(pSelector, sSelector, tSelector);
		}

		public void DefineLocalApTitle(string apTitle, int aeQualifier)
		{
			_parameters = _connection.GetConnectionParameters();
			_parameters.SetLocalApTitle(apTitle, aeQualifier);
		}

		public void DefineRemoteAddresses(uint pSelector, byte[] sSelector, byte[] tSelector)
		{
			_parameters = _connection.GetConnectionParameters();
			_parameters.SetRemoteAddresses(pSelector, sSelector, tSelector);
		}

		public void DefineRemoteApTitle(string apTitle, int aeQualifier)
		{
			_parameters = _connection.GetConnectionParameters();
			_parameters.SetRemoteApTitle(apTitle, aeQualifier);
		}

		private List<PathDA> listPath = new List<PathDA>();

		private class PathDA
		{
			public string Path { get; }
			public FunctionalConstraint FC { get; }
			public MmsType TypeMms { get; }

			public PathDA(string path, FunctionalConstraint fc, MmsType typeMms)
			{
				Path = path;
				FC = fc;
				TypeMms = typeMms;
			}
		}

		public void FillPathDA()
		{
			var nodeLD = _connection.GetServerDirectory();

			foreach (var ld in nodeLD)
			{
				var nodeLN = _connection.GetLogicalDeviceDirectory(ld);
				foreach (var ln in nodeLN)
				{
					FillDataObject(ld, ln);


				}
			}
		}

		private void FillDataObject(string ld, string ln)
		{
			var nodeDO = _connection.GetLogicalNodeDirectory($"{ld}/{ln}", ACSIClass.ACSI_CLASS_DATA_OBJECT);
			foreach (var _do in nodeDO)
			{
				var nodeDAwithFC = _connection.GetDataDirectoryFC($"{ld}/{ln}.{_do}");
				foreach (var dafc in nodeDAwithFC)
				{
					var path = $"{ld}/{ln}.{_do}";


					FillDataObjectBDA(path, dafc);
				}
			}
		}

		private void FillDataObjectBDA(string path, string dafc)
		{
			var typeFC = ObjectReference.getFC($"{path}.{dafc}");
			var itemPath = ObjectReference.getElementName($"{path}.{dafc}");

			var bda  = _connection.GetDataDirectoryFC(itemPath);
			if (bda.Count != 0)
			{
				foreach (var bdafc in bda)
				{
					FillDataObjectBDA(itemPath, bdafc);
				}
			}
			else
			{
				var typeMMS = _connection.GetVariableSpecification(itemPath, typeFC).GetType();
				listPath.Add(new PathDA(itemPath, typeFC, typeMMS));
			}

			//foreach (var path in itemPath)
			//{
			//	

			//}
		}
	}
}