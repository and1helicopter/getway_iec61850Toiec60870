using System.Collections.Generic;
using IEC61850.Common;
using IEC_60870;
using lib60870;

 namespace Gateway
 {
	 public class Servers61850
	 {
		 public string host { get; set; }
		 public int port { get; set; }
		 public string apTitleR { get; set; }
		 public string aeQualifierR { get; set; }
		 public string pSelectorR { get; set; }
		 public string sSelectorR { get; set; }
		 public string tSelectorR { get; set; }
		 public string apTitleL { get; set; }
		 public string aeQualifierL { get; set; }
		 public string pSelectorL { get; set; }
		 public string sSelectorL { get; set; }
		 public string tSelectorL { get; set; }
		 public bool enabled { get; set; }
		 public string password { get; set; }
		 public List<Item61850> items61850 { get; set; }
		 public object itemsASDU { get; set; }
	 }

	 public class Item61850
	 {
		public string path { get; set; }
		public MmsType typeMMS { get; set; }
		public FunctionalConstraint typeFC { get; set; }
	 }
	 
 	public class DataItem
 	{
		public string Host { get; set; }
		public int Port { get; set;}
			 
		public TypeID TypeId { get; set;}
		public int Sq { get; set;}
		public CauseOfTransmission Reason { get; set;}
		public bool IsNegative { get; set;}
		public bool IsTest { get; set;}
		public byte OriginAddr { get; set;}
		public int CommonAddr { get; set;}
			
		public List<InfObject> InfObjects = new List<InfObject>();
 	}
 
 	public class InfObject
 	{
    public int InfAddr { get; set; }
		public List<AttributeObject> InfAttributes = new List<AttributeObject>();
		 
		public class AttributeObject
		{
			public string Object { get; set; }
			public string Attribute { get; set; }
		}
 	}
 }