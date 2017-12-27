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
		private bool _runConnect;

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

		public bool StartConnection()
		{
			try
			{
				_connection.Connect(_hostname, _port);
				_runConnect = true;
				return true;
			}
			catch (IedConnectionException e)
			{
				Log.Write(e, Log.Code.ERROR);
				return false;
			}
		}

		public void StopConnection()
		{
			_connection.Abort();
			_runConnect = false;
			_connection.Dispose();
		}

		public bool RunConnect()
		{
			return _runConnect;
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
			public string path { get; }
			public FunctionalConstraint typeFC { get; }
			public MmsType typeMMS { get; }

			public PathDA(string pathStr, FunctionalConstraint typeFc, MmsType typeMms)
			{
				path = pathStr;
				typeFC = typeFc;
				typeMMS = typeMms;
			}
		}

		private PathDA GetPathDA(string path)
		{
			return _listPath.Find(x => x.path == path);
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
			switch (item.typeMMS)
			{
				case MmsType.MMS_BIT_STRING:
					val = _connection.ReadBitStringValue(item.path, item.typeFC);
					break;
				case MmsType.MMS_BOOLEAN:
					val = _connection.ReadBooleanValue(item.path, item.typeFC);
					break;
				case MmsType.MMS_FLOAT:
					val = _connection.ReadFloatValue(item.path, item.typeFC);
					break;
				case MmsType.MMS_INTEGER:
					val = _connection.ReadIntegerValue(item.path, item.typeFC);
					break;
				case MmsType.MMS_UNSIGNED:
					val = _connection.ReadValue(item.path, item.typeFC);
					break;
				case MmsType.MMS_STRING:
					val = _connection.ReadStringValue(item.path, item.typeFC);
					break;
				case MmsType.MMS_UTC_TIME:
					val = _connection.ReadTimestampValue(item.path, item.typeFC);
					break;
				default:
					val = _connection.ReadValue(item.path, item.typeFC);
					break;
			}

			return val;
		}

		public void SetValue(dynamic newValue, dynamic oldValue, ulong operTm, PathDA item, bool test, bool cheakInterlock, bool cheakSynchro, string originator, OrCat orCat)
		{
			if (item.path.Contains("Oper.ctlVal"))
			{
				var path = item.path.Replace(".Oper.ctlVal", "");

				var pathDA = new PathDA($"{path}.ctlModel", FunctionalConstraint.CF, MmsType.MMS_INTEGER);

				try
				{
					var ctlModelValue = GetValue(pathDA);

					if (ctlModelValue == 1)
						DirectWithNormalSecurity(newValue, operTm, path, test, cheakInterlock, cheakSynchro, originator, orCat);
					else if (ctlModelValue == 2)
					{
						WithNormalSecuritySbo(newValue, operTm, path, test, cheakInterlock, cheakSynchro, originator, orCat);
					}
					else if (ctlModelValue == 3)
					{
						DirectWithEnhancedSecurity(newValue, oldValue, operTm, path, test, cheakInterlock, cheakSynchro, originator, orCat);
					}
					else if (ctlModelValue == 4)
					{
						WithEnhancedSecuritySbo(newValue, oldValue, operTm, path, test, cheakInterlock, cheakSynchro, originator, orCat);
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

		private void DirectWithEnhancedSecurity(dynamic newValue, dynamic oldValue, ulong operTm, string path, bool test, bool cheakInterlock, bool cheakSynchro, string originator, OrCat orCat)
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
				
			controlObject.SetCommandTerminationHandler((parameter, o) =>
			{

			}, null);
				
			controlObject.Operate(newValue, operTm);
		}
		
		private void WithEnhancedSecuritySbo(dynamic newValue, dynamic oldValue, ulong operTm, string path, bool test, bool cheakInterlock, bool cheakSynchro, string originator, OrCat orCat)
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
			
			controlObject.SetCommandTerminationHandler((parameter, o) =>
			{
				
			}, null);
			
			if (controlObject.SelectWithValue(oldValue))
				controlObject.Operate(newValue, operTm);
			else
				controlObject.Cancel();
		}
	}
}