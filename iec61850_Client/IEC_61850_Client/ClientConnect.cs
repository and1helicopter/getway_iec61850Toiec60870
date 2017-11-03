using IEC61850.Client;
using  IEC61850.Common;
using System.Collections.Generic;
using Logger;

namespace IEC_61850
{
	public class ClientConnect
	{
		private IedConnection _connection = new IedConnection();

		public string GetConnetionHostPort()
		{
			return $"{_hostname}:{_port}";
		}

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
			try
			{
				_connection.Connect(_hostname, _port);
			}
			catch (IedConnectionException e)
			{
				Log.Write(e, Log.Code.ERROR);
			}
		}

		public void StopConnection()
		{
			_connection.Abort();
			_connection.Dispose();
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

		public class PathDA
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

		public List<PathDA> GetListPathDA()
		{
			return listPath;
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
		}

		public dynamic GetValue(PathDA item)
		{
			dynamic val;
			switch (item.TypeMms)
			{
				case MmsType.MMS_BIT_STRING:
					val = _connection.ReadBitStringValue(item.Path, item.FC);
					break;
				case MmsType.MMS_BOOLEAN:
					val = _connection.ReadBooleanValue(item.Path, item.FC);
					break;
				case MmsType.MMS_FLOAT:
					val = _connection.ReadFloatValue(item.Path, item.FC);
					break;
				case MmsType.MMS_INTEGER:
					val = _connection.ReadIntegerValue(item.Path, item.FC);
					break;
				case MmsType.MMS_UNSIGNED:
					val = _connection.ReadValue(item.Path, item.FC);
					break;
				case MmsType.MMS_STRING:
					val = _connection.ReadStringValue(item.Path, item.FC);
					break;
				case MmsType.MMS_UTC_TIME:
					val = _connection.ReadTimestampValue(item.Path, item.FC);
					break;
				default:
					val = _connection.ReadValue(item.Path, item.FC);
					break;
			}

			return val;
		}
	}
}