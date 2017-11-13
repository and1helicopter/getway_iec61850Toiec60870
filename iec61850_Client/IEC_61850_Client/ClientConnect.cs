using System;
using IEC61850.Client;
using  IEC61850.Common;
using System.Collections.Generic;
using Logger;

namespace IEC_61850
{
	public class ClientConnect
	{
		private readonly IedConnection _connection = new IedConnection();

		private string _hostname;
		private int _port;

		private IsoConnectionParameters _parameters;

		public string GetConnetionHostPort()
		{
			return $"{_hostname}:{_port}";
		}

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

		private readonly List<PathDA> _listPath = new List<PathDA>();

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

		private PathDA GetPathDA(string path)
		{
			return _listPath.Find(x => x.Path == path);
		}

		public List<PathDA> GetListPathDA()
		{
			return _listPath;
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
				_listPath.Add(new PathDA(itemPath, typeFC, typeMMS));
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

		public void SetValue(dynamic value, ulong operTm, PathDA item, bool test, bool cheakInterlock, bool cheakSynchro, string originator, OrCat orCat)
		{
			if (item.Path.Contains("Oper.ctlVal"))
			{
				var path = item.Path.Replace(".Oper.ctlVal", "");

				ControlObject control = _connection.CreateControlObject(path);
				ControlModel controlModel = control.GetControlModel();
				Console.WriteLine(controlModel);

				var pathDA = new PathDA($"{path}.ctlModel", FunctionalConstraint.CF, MmsType.MMS_INTEGER);

				try
				{
					var ctlModelValue = GetValue(pathDA);

					if (ctlModelValue == 1)
					{
						DirectWithNormalSecurity(value, operTm, path, test, cheakInterlock, cheakSynchro, originator, orCat);
					}
					else if (ctlModelValue == 2)
					{
						WithNormalSecuritySbo(value, operTm, path, test, cheakInterlock, cheakSynchro, originator, orCat);
					}
					else if (ctlModelValue == 3)
					{
						DirectWithEnhancedSecurity(value, path);
					}
					else if (ctlModelValue == 4)
					{
						
					}
				}
				catch (Exception e)
				{
					Log.Write(e, Log.Code.WARNING);
				}
			}
		}

		private void DirectWithNormalSecurity(dynamic value, ulong operTm, string path, bool test, bool cheakInterlock, bool cheakSynchro, string originator, OrCat orCat)
		{
			if (GetPathDA($"{path}.Oper.ctlVal") == null) return;
			var controlObject = _connection.CreateControlObject(path);
			
			controlObject.EnableInterlockCheck();
			controlObject.SetInterlockCheck(cheakInterlock);
			
			controlObject.EnableSynchroCheck();
			controlObject.SetSynchroCheck(cheakSynchro);
			
			controlObject.SetTestMode(test);
			
			if(originator != null)
				controlObject.SetOrigin(originator, orCat);
			
			controlObject.SetTestMode(false);
			controlObject.Operate(value, operTm);
		}

		private void WithNormalSecuritySbo(dynamic value, ulong operTm, string path, bool test, bool cheakInterlock, bool cheakSynchro, string originator, OrCat orCat)
		{
			if (GetPathDA($"{path}.Oper.ctlVal") == null) return;
			var controlObject = _connection.CreateControlObject(path);
			
			controlObject.EnableInterlockCheck();
			controlObject.SetInterlockCheck(cheakInterlock);
			
			controlObject.EnableSynchroCheck();
			controlObject.SetSynchroCheck(cheakSynchro);
			
			controlObject.SetTestMode(test);
			
			if(originator != null)
				controlObject.SetOrigin(originator, orCat);
			
			if (controlObject.Select())
				controlObject.Operate(value, operTm);
			else
				controlObject.Cancel();
		}

		private void DirectWithEnhancedSecurity(dynamic value, string path)
		{
			if (GetPathDA($"{path}.Oper.ctlVal") != null)
			{
				var controlObject = _connection.CreateControlObject(path);
				controlObject.SelectWithValue(value);
				controlObject.Operate(value);
			}
		}
	}
}