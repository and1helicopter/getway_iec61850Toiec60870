using System;
using System.Collections.Generic;
using System.Linq;
using Abstraction;
using lib60870;
using lib60870.CS101;
using Logger;

namespace IEC_60870
{
	public partial class Server
	{
	    private lib60870.CS104.Server _server;
        private bool _workServer;
	    private readonly object _locker = new object();
	    private List<string> WhiteListIP { get; set; } = new List<string>();
	    private List<string> BlackListIP { get; set; } = new List<string>();

	    public Server(string host, int port, int maxQueue, int maxConnection, bool statusTls, List<string> whiteListIp, List<string> blackListIp)
        {
            try
            {
                _server = new lib60870.CS104.Server();
                _server.SetLocalAddress(host);
                _server.SetLocalPort(port);
                _server.MaxQueueSize = maxQueue;
                _server.MaxOpenConnections = maxConnection;

                if (whiteListIp != null)
                    WhiteListIP = whiteListIp;
                if (blackListIp != null)
                    BlackListIP = blackListIp;
            }
            catch
            {
                Log.Write(new Exception("IEC_60870.Server.Server()"), Log.Code.ERROR);
            }
        }

		public bool ServerStart()
		{
            _server.Start();
			_workServer = true;
		    return _workServer;
		}
		
        public void ServerSetHandlers()
        {
            _server.SetConnectionRequestHandler(connectionRequestHandler, null);                 //Проверка  ip - адреса клиента на белый и черный список
            _server.SetASDUHandler(asduHandler, null);                                                                         //Оброботчик запросов от клиента
            _server.SetInterrogationHandler(interrogationHandler, null);                                         //Обработчик команды опроса|Handler for interrogation command (C_IC_NA_1 - 100)|    
            _server.SetCounterInterrogationHandler(counterInterrogationHandler, null);          //Обработчик команды опроса счетчиков|Handler for counter interrogation command (C_CI_NA_1 - 101)|
            _server.SetReadHandler(readHandler, null);                                                                           //Обработчик команды чтения|Handler for read command (C_RD_NA_1 - 102)|
            _server.SetClockSynchronizationHandler(clockSynchronizationHandler, null);          //Обработчик команды синхронизации часов|Handler for clock synchronization command (C_CS_NA_1 - 103)|
            _server.SetResetProcessHandler(resetProcessHandler, null);                                          //Обработчик команды сброса процесса в исходное состояние|Handler for reset process command (C_RP_NA_1 - 105)|
            _server.SetDelayAcquisitionHandler(delayAcquisitionHandler, null);                             //Обработчик команды определения запаздывания|Handler for delay acquisition command (C_CD_NA:1 - 106)|
        }

        public bool ServerStop()
		{
			_server.Stop();
			_workServer = false;
		    return _workServer;
		}
        


		public void AddASDUServer(Item items, dynamic[] values)
		{
		    var item = (ItemBridge) items;
		    var cot = item.Item.Cot;
		    var isTest = item.Item.IsTest;
		    var isNegative = item.Item.IsNegative;
		    var oa = (byte)item.Item.Oa;
		    var ca = item.Item.Ca;
		    var sq = item.Item.Sq;
		    var typeId = (TypeID)item.Item.TypeId;
		    var count = item.Item.Length;

            //Строим ASDU
            var asdu = new ASDU(_server.GetApplicationLayerParameters(), (CauseOfTransmission)cot, isTest, isNegative, oa, ca, sq);
		    var index = item.Dictionary.Count / count;
		    var tempList = item.Dictionary.Values.Select(x=> (Obj)x).ToList();

            for (var i = 0; i < count; i++)
		    {
		        var currentIndex = index * i;
                //Определяем адрес
		        var addr = tempList[currentIndex].AddrObj;
                //Все элементы по адресу
		        var listObj = tempList.FindAll(x => x.AddrObj == addr);
                //Добовляем информационный объект к ASDU
		        var informationObject = GetObject(typeId, addr, listObj, values);
                if(informationObject != null)
                    asdu.AddInformationObject(informationObject);
            }

            if (asdu.NumberOfElements != 0)
                lock (_locker)
                    _server.EnqueueASDU(asdu);
        }

	    private InformationObject GetObject(TypeID typeId, int ioa, List<Obj> list, dynamic[] values)
	    {
            try
	        {
	            int index;
	            bool valueSiq;
	            DoublePointValue valueDiq;
	            DateTime valueCp24Time2A;
	            byte valueQds;
	            byte valueVti;
	            uint valueBsi;
	            float valueNva;
	            int valueSva;
	            float valueIeee;
	            long valueBcr;
	            BinaryCounterReading tempBcr;
	            byte valueSep;
	            int valueCp16Time2A;
	            byte valueSpe;
	            byte valueQdp;
	            DateTime valueCp56Time2A;
	            byte valueOci;
	            byte valueQos;
	            byte valueQpm;

                switch (typeId)
                {
                    case TypeID.M_SP_NA_1: /* 1 */
                        index = list.First(x => x.TypeElement == "SIQ").IndexElement;
                        valueSiq = Convert.ToBoolean(values[index]);
                        return new SinglePointInformation(ioa, valueSiq, new QualityDescriptor());

                    case TypeID.M_SP_TA_1: /* 2 */
                        index = list.First(x => x.TypeElement == "SIQ").IndexElement;
                        valueSiq = Convert.ToBoolean(values[index]);
                        index = list.First(x => x.TypeElement == "CP24Time2a").IndexElement;
                        valueCp24Time2A = Convert.ToDateTime(values[index]);
                        return new SinglePointWithCP24Time2a(ioa, valueSiq, new QualityDescriptor(),
                            new CP24Time2a(valueCp24Time2A.Minute, valueCp24Time2A.Second, valueCp24Time2A.Millisecond));

                    case TypeID.M_DP_NA_1: /* 3 */
                        index = list.First(x => x.TypeElement == "DIQ").IndexElement;
                        valueDiq = (DoublePointValue)Convert.ToInt32(values[index]);
                        return new DoublePointInformation(ioa, valueDiq, new QualityDescriptor());

                    case TypeID.M_DP_TA_1: /* 4 */
                        index = list.First(x => x.TypeElement == "DIQ").IndexElement;
                        valueDiq = (DoublePointValue)Convert.ToInt32(values[index]);
                        index = list.First(x => x.TypeElement == "CP24Time2a").IndexElement;
                        valueCp24Time2A = Convert.ToDateTime(values[index]);
                        return new DoublePointWithCP24Time2a(ioa, valueDiq, new QualityDescriptor(),
                            new CP24Time2a(valueCp24Time2A.Minute, valueCp24Time2A.Second, valueCp24Time2A.Millisecond));

                    case TypeID.M_ST_NA_1: /* 5 */
                        index = list.First(x => x.TypeElement == "VTI").IndexElement;
                        valueVti = Convert.ToByte(values[index]);
                        index = list.First(x => x.TypeElement == "QDS").IndexElement;
                        valueQds = Convert.ToByte(values[index]);
                        return new StepPositionInformation(ioa, valueVti & 0x7F, (valueVti & 0x01) == 0x01, new QualityDescriptor(valueQds));

                    case TypeID.M_ST_TA_1: /* 6 */
                        index = list.First(x => x.TypeElement == "VTI").IndexElement;
                        valueVti = Convert.ToByte(values[index]);
                        index = list.First(x => x.TypeElement == "QDS").IndexElement;
                        valueQds = Convert.ToByte(values[index]);
                        index = list.First(x => x.TypeElement == "CP24Time2a").IndexElement;
                        valueCp24Time2A = Convert.ToDateTime(values[index]);
                        return new StepPositionWithCP24Time2a(ioa, valueVti & 0x7F, (valueVti & 0x01) == 0x01, new QualityDescriptor(valueQds),
                            new CP24Time2a(valueCp24Time2A.Minute, valueCp24Time2A.Second, valueCp24Time2A.Millisecond));

                    case TypeID.M_BO_NA_1: /* 7 */
                        index = list.First(x => x.TypeElement == "BSI").IndexElement;
                        valueBsi = Convert.ToUInt32(values[index]);
                        index = list.First(x => x.TypeElement == "QDS").IndexElement;
                        valueQds = Convert.ToByte(values[index]);
                        return new Bitstring32(ioa, valueBsi, new QualityDescriptor(valueQds));

                    case TypeID.M_BO_TA_1: /* 8 */
                        index = list.First(x => x.TypeElement == "BSI").IndexElement;
                        valueBsi = Convert.ToUInt32(values[index]);
                        index = list.First(x => x.TypeElement == "QDS").IndexElement;
                        valueQds = Convert.ToByte(values[index]);
                        index = list.First(x => x.TypeElement == "CP24Time2a").IndexElement;
                        valueCp24Time2A = Convert.ToDateTime(values[index]);
                        return new Bitstring32WithCP24Time2a(ioa, valueBsi, new QualityDescriptor(valueQds),
                            new CP24Time2a(valueCp24Time2A.Minute, valueCp24Time2A.Second, valueCp24Time2A.Millisecond));

                    case TypeID.M_ME_NA_1: /* 9 */
                        index = list.First(x => x.TypeElement == "NVA").IndexElement;
                        valueNva = Convert.ToSingle(values[index]);
                        index = list.First(x => x.TypeElement == "QDS").IndexElement;
                        valueQds = Convert.ToByte(values[index]);
                        return new MeasuredValueNormalized(ioa, valueNva, new QualityDescriptor(valueQds));

                    case TypeID.M_ME_TA_1: /* 10 */
                        index = list.First(x => x.TypeElement == "NVA").IndexElement;
                        valueNva = Convert.ToSingle(values[index]);
                        index = list.First(x => x.TypeElement == "QDS").IndexElement;
                        valueQds = Convert.ToByte(values[index]);
                        index = list.First(x => x.TypeElement == "CP24Time2a").IndexElement;
                        valueCp24Time2A = Convert.ToDateTime(values[index]);
                        return new MeasuredValueNormalizedWithCP24Time2a(ioa, valueNva, new QualityDescriptor(valueQds),
                            new CP24Time2a(valueCp24Time2A.Minute, valueCp24Time2A.Second, valueCp24Time2A.Millisecond));

                    case TypeID.M_ME_NB_1: /* 11 */
                        index = list.First(x => x.TypeElement == "SVA").IndexElement;
                        valueSva = Convert.ToInt32(values[index]);
                        index = list.First(x => x.TypeElement == "QDS").IndexElement;
                        valueQds = Convert.ToByte(values[index]);
                        return new MeasuredValueScaled(ioa, valueSva, new QualityDescriptor(valueQds));

                    case TypeID.M_ME_TB_1: /* 12 */
                        index = list.First(x => x.TypeElement == "SVA").IndexElement;
                        valueSva = Convert.ToInt32(values[index]);
                        index = list.First(x => x.TypeElement == "QDS").IndexElement;
                        valueQds = Convert.ToByte(values[index]);
                        index = list.First(x => x.TypeElement == "CP24Time2a").IndexElement;
                        valueCp24Time2A = Convert.ToDateTime(values[index]);
                        return new MeasuredValueScaledWithCP24Time2a(ioa, valueSva, new QualityDescriptor(valueQds),
                            new CP24Time2a(valueCp24Time2A.Minute, valueCp24Time2A.Second, valueCp24Time2A.Millisecond));

                    case TypeID.M_ME_NC_1: /* 13 */
                        index = list.First(x => x.TypeElement == "IEEE STD 754").IndexElement;
                        valueIeee = Convert.ToSingle(values[index]);
                        index = list.First(x => x.TypeElement == "QDS").IndexElement;
                        valueQds = Convert.ToByte(values[index]);
                        return new MeasuredValueShort(ioa, valueIeee, new QualityDescriptor(valueQds));

                    case TypeID.M_ME_TC_1: /* 14 */
                        index = list.First(x => x.TypeElement == "IEEE STD 754").IndexElement;
                        valueIeee = Convert.ToSingle(values[index]);
                        index = list.First(x => x.TypeElement == "QDS").IndexElement;
                        valueQds = Convert.ToByte(values[index]);
                        index = list.First(x => x.TypeElement == "CP24Time2a").IndexElement;
                        valueCp24Time2A = Convert.ToDateTime(values[index]);
                        return new MeasuredValueShortWithCP24Time2a(ioa, valueIeee, new QualityDescriptor(valueQds),
                            new CP24Time2a(valueCp24Time2A.Minute, valueCp24Time2A.Second, valueCp24Time2A.Millisecond));

                    case TypeID.M_IT_NA_1: /* 15 */
                        index = list.First(x => x.TypeElement == "BCR").IndexElement;
                        valueBcr = Convert.ToSingle(values[index]);
                        tempBcr = new BinaryCounterReading()
                        {
                            Adjusted = (valueBcr & 0x008000000000) >> 38 == 1,
                            Carry = (valueBcr & 0x004000000000) >> 37 == 1,
                            Invalid = (valueBcr & 0x010000000000) >> 39 == 1,
                            SequenceNumber = (int)((valueBcr & 0x003E00000000) >> 32),
                            Value = (int)(valueBcr & 0x0000FFFFFFFF)
                        };
                        return new IntegratedTotals(ioa, tempBcr);

                    case TypeID.M_IT_TA_1: /* 16 */
                        index = list.First(x => x.TypeElement == "BCR").IndexElement;
                        valueBcr = Convert.ToSingle(values[index]);
                        tempBcr = new BinaryCounterReading()
                        {
                            Adjusted = (valueBcr & 0x008000000000) >> 38 == 1,
                            Carry = (valueBcr & 0x004000000000) >> 37 == 1,
                            Invalid = (valueBcr & 0x010000000000) >> 39 == 1,
                            SequenceNumber = (int)((valueBcr & 0x003E00000000) >> 32),
                            Value = (int)(valueBcr & 0x0000FFFFFFFF)
                        };
                        index = list.First(x => x.TypeElement == "CP24Time2a").IndexElement;
                        valueCp24Time2A = Convert.ToDateTime(values[index]);
                        return new IntegratedTotalsWithCP24Time2a(ioa, tempBcr,
                            new CP24Time2a(valueCp24Time2A.Minute, valueCp24Time2A.Second, valueCp24Time2A.Millisecond));

                    case TypeID.M_EP_TA_1: /* 17 */
                        index = list.First(x => x.TypeElement == "SEP").IndexElement;
                        valueSep = Convert.ToByte(values[index]);
                        index = list.First(x => x.TypeElement == "CP16Time2a").IndexElement;
                        valueCp16Time2A = Convert.ToInt32(values[index]);
                        index = list.First(x => x.TypeElement == "CP24Time2a").IndexElement;
                        valueCp24Time2A = Convert.ToDateTime(values[index]);
                        return new EventOfProtectionEquipment(ioa, new SingleEvent(valueSep), new CP16Time2a(valueCp16Time2A),
                            new CP24Time2a(valueCp24Time2A.Minute, valueCp24Time2A.Second, valueCp24Time2A.Millisecond));

                    case TypeID.M_EP_TB_1: /* 18 */
                        index = list.First(x => x.TypeElement == "SPE").IndexElement;
                        valueSpe = Convert.ToByte(values[index]);
                        index = list.First(x => x.TypeElement == "QDP").IndexElement;
                        valueQdp = Convert.ToInt32(values[index]);
                        index = list.First(x => x.TypeElement == "CP16Time2a").IndexElement;
                        valueCp16Time2A = Convert.ToInt32(values[index]);
                        index = list.First(x => x.TypeElement == "CP24Time2a").IndexElement;
                        valueCp24Time2A = Convert.ToDateTime(values[index]);
                        return new PackedStartEventsOfProtectionEquipment(ioa, new StartEvent(valueSpe), new QualityDescriptorP(valueQdp),
                            new CP16Time2a(valueCp16Time2A), new CP24Time2a(valueCp24Time2A.Minute, valueCp24Time2A.Second, valueCp24Time2A.Millisecond));

                    case TypeID.M_EP_TC_1: /* 19 */
                        index = list.First(x => x.TypeElement == "OCI").IndexElement;
                        valueOci = Convert.ToByte(values[index]);
                        index = list.First(x => x.TypeElement == "QDP").IndexElement;
                        valueQdp = Convert.ToInt32(values[index]);
                        index = list.First(x => x.TypeElement == "CP16Time2a").IndexElement;
                        valueCp16Time2A = Convert.ToInt32(values[index]);
                        index = list.First(x => x.TypeElement == "CP24Time2a").IndexElement;
                        valueCp24Time2A = Convert.ToDateTime(values[index]);
                        return new PackedOutputCircuitInfo(ioa, new OutputCircuitInfo(valueOci), new QualityDescriptorP(valueQdp),
                            new CP16Time2a(valueCp16Time2A), new CP24Time2a(valueCp24Time2A.Minute, valueCp24Time2A.Second, valueCp24Time2A.Millisecond));

                    case TypeID.M_PS_NA_1: /* 20 */
                        index = list.First(x => x.TypeElement == "SCD").IndexElement;
                        long valueTemp = Convert.ToInt64(values[index]);
                        byte[] valueScdByte = BitConverter.GetBytes(valueTemp & 0xFFFFFFFF);
                        int valueScdInt = Convert.ToInt32(valueTemp >> 32);
                        index = list.First(x => x.TypeElement == "QDS").IndexElement;
                        valueQds = Convert.ToByte(values[index]);
                        return new PackedSinglePointWithSCD(ioa, new StatusAndStatusChangeDetection(valueScdByte, valueScdInt), new QualityDescriptor(valueQds));

                    case TypeID.M_ME_ND_1: /* 21 */
                        index = list.First(x => x.TypeElement == "NVA").IndexElement;
                        valueNva = Convert.ToSingle(values[index]);
                        return new MeasuredValueNormalizedWithoutQuality(ioa, valueNva);

                    case TypeID.M_SP_TB_1: /* 30 */
                        index = list.First(x => x.TypeElement == "SIQ").IndexElement;
                        valueSiq = Convert.ToBoolean(values[index]);
                        index = list.First(x => x.TypeElement == "CP56Time2a").IndexElement;
                        valueCp56Time2A = Convert.ToDateTime(values[index]);
                        return new SinglePointWithCP56Time2a(ioa, valueSiq, new QualityDescriptor(), new CP56Time2a(valueCp56Time2A));

                    case TypeID.M_DP_TB_1: /* 31 */
                        index = list.First(x => x.TypeElement == "DIQ").IndexElement;
                        valueDiq = (DoublePointValue)Convert.ToInt32(values[index]);
                        index = list.First(x => x.TypeElement == "CP56Time2a").IndexElement;
                        valueCp56Time2A = Convert.ToDateTime(values[index]);
                        return new DoublePointWithCP56Time2a(ioa, valueDiq, new QualityDescriptor(), new CP56Time2a(valueCp56Time2A));

                    case TypeID.M_ST_TB_1: /* 32 */
                        index = list.First(x => x.TypeElement == "VTI").IndexElement;
                        valueVti = Convert.ToByte(values[index]);
                        index = list.First(x => x.TypeElement == "CP56Time2a").IndexElement;
                        valueCp56Time2A = Convert.ToDateTime(values[index]);
                        return new StepPositionWithCP56Time2a(ioa, valueVti & 0x7F, (valueVti & 0x01) == 0x01, new QualityDescriptor(),
                            new CP56Time2a(valueCp56Time2A));

                    case TypeID.M_BO_TB_1: /* 33 */
                        index = list.First(x => x.TypeElement == "BSI").IndexElement;
                        valueBsi = Convert.ToUInt32(values[index]);
                        index = list.First(x => x.TypeElement == "CP56Time2a").IndexElement;
                        valueCp56Time2A = Convert.ToDateTime(values[index]);
                        return new Bitstring32WithCP56Time2a(ioa, valueBsi, new QualityDescriptor(), new CP56Time2a(valueCp56Time2A));

                    case TypeID.M_ME_TD_1: /* 34 */
                        index = list.First(x => x.TypeElement == "NVA").IndexElement;
                        valueNva = Convert.ToSingle(values[index]);
                        index = list.First(x => x.TypeElement == "QDS").IndexElement;
                        valueQds = Convert.ToByte(values[index]);
                        index = list.First(x => x.TypeElement == "CP56Time2a").IndexElement;
                        valueCp56Time2A = Convert.ToDateTime(values[index]);
                        return new MeasuredValueNormalizedWithCP56Time2a(ioa, valueNva, new QualityDescriptor(valueQds), new CP56Time2a(valueCp56Time2A));

                    case TypeID.M_ME_TE_1: /* 35 */
                        index = list.First(x => x.TypeElement == "SVA").IndexElement;
                        valueSva = Convert.ToInt32(values[index]);
                        index = list.First(x => x.TypeElement == "QDS").IndexElement;
                        valueQds = Convert.ToByte(values[index]);
                        index = list.First(x => x.TypeElement == "CP56Time2a").IndexElement;
                        valueCp56Time2A = Convert.ToDateTime(values[index]);
                        return new MeasuredValueScaledWithCP56Time2a(ioa, valueSva, new QualityDescriptor(valueQds), new CP56Time2a(valueCp56Time2A));

                    case TypeID.M_ME_TF_1: /* 36 */
                        index = list.First(x => x.TypeElement == "IEEE STD 754").IndexElement;
                        valueIeee = Convert.ToSingle(values[index]);
                        index = list.First(x => x.TypeElement == "QDS").IndexElement;
                        valueQds = Convert.ToByte(values[index]);
                        index = list.First(x => x.TypeElement == "CP56Time2a").IndexElement;
                        valueCp56Time2A = Convert.ToDateTime(values[index]);
                        return new MeasuredValueShortWithCP56Time2a(ioa, valueIeee, new QualityDescriptor(valueQds), new CP56Time2a(valueCp56Time2A));

                    case TypeID.M_IT_TB_1: /* 37 */
                        index = list.First(x => x.TypeElement == "BCR").IndexElement;
                        valueBcr = Convert.ToSingle(values[index]);
                        tempBcr = new BinaryCounterReading()
                        {
                            Adjusted = (valueBcr & 0x008000000000) >> 38 == 1,
                            Carry = (valueBcr & 0x004000000000) >> 37 == 1,
                            Invalid = (valueBcr & 0x010000000000) >> 39 == 1,
                            SequenceNumber = (int)((valueBcr & 0x003E00000000) >> 32),
                            Value = (int)(valueBcr & 0x0000FFFFFFFF)
                        };
                        index = list.First(x => x.TypeElement == "CP56Time2a").IndexElement;
                        valueCp56Time2A = Convert.ToDateTime(values[index]);
                        return new IntegratedTotalsWithCP56Time2a(ioa, tempBcr, new CP56Time2a(valueCp56Time2A));

                    case TypeID.M_EP_TD_1: /* 38 */
                        index = list.First(x => x.TypeElement == "SEP").IndexElement;
                        valueSep = Convert.ToByte(values[index]);
                        index = list.First(x => x.TypeElement == "CP16Time2a").IndexElement;
                        valueCp16Time2A = Convert.ToInt32(values[index]);
                        index = list.First(x => x.TypeElement == "CP56Time2a").IndexElement;
                        valueCp56Time2A = Convert.ToDateTime(values[index]);
                        return new EventOfProtectionEquipmentWithCP56Time2a(ioa, new SingleEvent(valueSep), new CP16Time2a(valueCp16Time2A),
                            new CP56Time2a(valueCp56Time2A));

                    case TypeID.M_EP_TE_1: /* 39 */
                        index = list.First(x => x.TypeElement == "SPE").IndexElement;
                        valueSpe = Convert.ToByte(values[index]);
                        index = list.First(x => x.TypeElement == "QDP").IndexElement;
                        valueQdp = Convert.ToInt32(values[index]);
                        index = list.First(x => x.TypeElement == "CP16Time2a").IndexElement;
                        valueCp16Time2A = Convert.ToInt32(values[index]);
                        index = list.First(x => x.TypeElement == "CP56Time2a").IndexElement;
                        valueCp56Time2A = Convert.ToDateTime(values[index]);
                        return new PackedStartEventsOfProtectionEquipmentWithCP56Time2a(ioa, new StartEvent(valueSpe), new QualityDescriptorP(valueQdp),
                            new CP16Time2a(valueCp16Time2A), new CP56Time2a(valueCp56Time2A));

                    case TypeID.M_EP_TF_1: /* 40 */
                        index = list.First(x => x.TypeElement == "OCI").IndexElement;
                        valueOci = Convert.ToByte(values[index]);
                        index = list.First(x => x.TypeElement == "QDP").IndexElement;
                        valueQdp = Convert.ToInt32(values[index]);
                        index = list.First(x => x.TypeElement == "CP16Time2a").IndexElement;
                        valueCp16Time2A = Convert.ToInt32(values[index]);
                        index = list.First(x => x.TypeElement == "CP56Time2a").IndexElement;
                        valueCp56Time2A = Convert.ToDateTime(values[index]);
                        return new PackedOutputCircuitInfoWithCP56Time2a(ioa, new OutputCircuitInfo(valueOci), new QualityDescriptorP(valueQdp),
                            new CP16Time2a(valueCp16Time2A), new CP56Time2a(valueCp56Time2A));

                    case TypeID.C_SC_NA_1: /* 45 */
                        index = list.First(x => x.TypeElement == "SCO").IndexElement;
                        var valueSco = Convert.ToByte(values[index]);
                        return new SingleCommand(ioa, (valueSco & 0x01) == 0x01, (valueSco & 0x01) == 0x01, (valueSco & 0xFC) >> 2);

                    case TypeID.C_DC_NA_1: /* 46 */
                        index = list.First(x => x.TypeElement == "DCO").IndexElement;
                        var valueDco = Convert.ToByte(values[index]);
                        return new DoubleCommand(ioa, valueDco & 0x03, (valueDco & 0x03) != 0x00, (valueDco & 0xFC) >> 2);

                    case TypeID.C_RC_NA_1: /* 47 */
                        index = list.First(x => x.TypeElement == "RCO").IndexElement;
                        var valueRco = Convert.ToByte(values[index]);
                        return new StepCommand(ioa, (StepCommandValue)(valueRco & 0x03), (valueRco & 0x03) != 0x00, (valueRco & 0xFC) >> 2);

                    case TypeID.C_SE_NA_1: /* 48 - Set-point command, normalized value */
                        index = list.First(x => x.TypeElement == "NVA").IndexElement;
                        valueNva = Convert.ToSingle(values[index]);
                        index = list.First(x => x.TypeElement == "QOS").IndexElement;
                        valueQos = Convert.ToByte(values[index]);
                        return new SetpointCommandNormalized(ioa, valueNva, new SetpointCommandQualifier(valueQos));

                    case TypeID.C_SE_NB_1: /* 49 - Set-point command, scaled value */
                        index = list.First(x => x.TypeElement == "SVA").IndexElement;
                        valueSva = Convert.ToInt32(values[index]);
                        index = list.First(x => x.TypeElement == "QOS").IndexElement;
                        valueQos = Convert.ToByte(values[index]);
                        return new SetpointCommandScaled(ioa, new ScaledValue(valueSva), new SetpointCommandQualifier(valueQos));

                    case TypeID.C_SE_NC_1: /* 50 - Set-point command, short floating point number */
                        index = list.First(x => x.TypeElement == "IEEE STD 754").IndexElement;
                        valueIeee = Convert.ToSingle(values[index]);
                        index = list.First(x => x.TypeElement == "QOS").IndexElement;
                        valueQos = Convert.ToByte(values[index]);
                        return new SetpointCommandShort(ioa, valueIeee, new SetpointCommandQualifier(valueQos));

                    case TypeID.C_BO_NA_1: /* 51 - Bitstring command */
                        index = list.First(x => x.TypeElement == "BSI").IndexElement;
                        valueBsi = Convert.ToUInt32(values[index]);
                        return new Bitstring32Command(ioa, valueBsi);

                    case TypeID.M_EI_NA_1: /* 70 - End of initialization */
                        index = list.First(x => x.TypeElement == "COI").IndexElement;
                        byte valueCoi = Convert.ToByte(values[index]);
                        return new EndOfInitialization(valueCoi);

                    case TypeID.C_IC_NA_1: /* 100 - Interrogation command */
                        index = list.First(x => x.TypeElement == "QOI").IndexElement;
                        byte valueQoi = Convert.ToByte(values[index]);
                        return new InterrogationCommand(ioa, valueQoi);


                    case TypeID.C_CI_NA_1: /* 101 - Counter interrogation command */
                        index = list.First(x => x.TypeElement == "QCC").IndexElement;
                        byte valueQcc = Convert.ToByte(values[index]);
                        return new CounterInterrogationCommand(ioa, valueQcc);

                    case TypeID.C_RD_NA_1: /* 102 - Read command */
                        return new ReadCommand(ioa);

                    case TypeID.C_CS_NA_1: /* 103 - Clock synchronization command */
                        index = list.First(x => x.TypeElement == "CP56Time2a").IndexElement;
                        valueCp56Time2A = Convert.ToDateTime(values[index]);
                        return new ClockSynchronizationCommand(ioa, new CP56Time2a(valueCp56Time2A));

                    case TypeID.C_TS_NA_1: /* 104 - Test command */
                        return new TestCommand();

                    case TypeID.C_RP_NA_1: /* 105 - Reset process command */
                        index = list.First(x => x.TypeElement == "QRP").IndexElement;
                        byte valueQrp = Convert.ToByte(values[index]);
                        return new ResetProcessCommand(ioa, valueQrp);

                    case TypeID.C_CD_NA_1: /* 106 - Delay acquisition command */
                        index = list.First(x => x.TypeElement == "CP16Time2a").IndexElement;
                        valueCp16Time2A = Convert.ToInt32(values[index]);
                        return new DelayAcquisitionCommand(ioa, new CP16Time2a(valueCp16Time2A));

                    case TypeID.P_ME_NA_1: /* 110 - Parameter of measured values, normalized value */
                        index = list.First(x => x.TypeElement == "NVA").IndexElement;
                        valueNva = Convert.ToSingle(values[index]);
                        index = list.First(x => x.TypeElement == "QPM").IndexElement;
                        valueQpm = Convert.ToByte(values[index]);
                        return new ParameterNormalizedValue(ioa, valueNva, valueQpm);

                    case TypeID.P_ME_NB_1: /* 111 - Parameter of measured values, scaled value */
                        index = list.First(x => x.TypeElement == "SVA").IndexElement;
                        valueSva = Convert.ToInt32(values[index]);
                        index = list.First(x => x.TypeElement == "QPM").IndexElement;
                        valueQpm = Convert.ToByte(values[index]);
                        return new ParameterScaledValue(ioa, new ScaledValue(valueSva), valueQpm);

                    case TypeID.P_ME_NC_1: /* 112 - Parameter of measured values, short floating point number */
                        index = list.First(x => x.TypeElement == "IEEE STD 754").IndexElement;
                        valueIeee = Convert.ToSingle(values[index]);
                        index = list.First(x => x.TypeElement == "QPM").IndexElement;
                        valueQpm = Convert.ToByte(values[index]);
                        return new ParameterFloatValue(ioa, valueIeee, valueQpm);

                    case TypeID.P_AC_NA_1: /* 113 - Parameter for activation */
                        index = list.First(x => x.TypeElement == "QPA").IndexElement;
                        byte valueQpa = Convert.ToByte(values[index]);
                        return new ParameterActivation(ioa, valueQpa);

                    default:
                        return null;
                }
            }
            catch 
	        {
                Log.Write(new Exception("IEC_60870.Server.GetObject"), Log.Code.WARNING);
	            return null;
            }
	    }
    }
}