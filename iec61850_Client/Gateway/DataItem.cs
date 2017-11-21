using System.Collections.Generic;
using IEC_60870;
using lib60870;

 
 namespace Gateway
 {
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