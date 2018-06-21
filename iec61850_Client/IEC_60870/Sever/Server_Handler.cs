using System.Linq;
using System.Net;
using lib60870;
using lib60870.CS101;

namespace IEC_60870.Sever
{
    public partial class Server
    {
        //Проверка  ip - адреса клиента на белый и черный список
        private bool connectionRequestHandler(object parameter, IPAddress ipAddress)
        {
            if (!WhiteListIp.FindAll(x => x.ToString().Equals(ipAddress.ToString())).Any() && WhiteListIp.Any())
            {
                return false;
            }
            else if (BlackListIp.FindAll(x => x.ToString().Equals(ipAddress.ToString())).Any())
            {
                return false;
            }
            else
                return true;
        }

        //Оброботчик запросов от клиента
        private bool asduHandler(object parameter, IMasterConnection connection, ASDU asdu)
        {
            return true;
        }

        //Обработчик команд опроса
        private bool interrogationHandler(object parameter, IMasterConnection connection, ASDU asdu, byte qoi)
        {
            return true;
        }

        //Обработчик команд опроса счетчиков 
        private bool counterInterrogationHandler(object parameter, IMasterConnection connection, ASDU asdu, byte qoi)
        {
            return true;
        }

        //Обработчик комаанд чтения
        private bool readHandler(object parameter, IMasterConnection connection, ASDU asdu, int ioa)
        {
            return true;
        }

        //Обработчик команд синхронизации часов
        private bool clockSynchronizationHandler(object parameter, IMasterConnection connection, ASDU asdu, CP56Time2a newTime)
        {
            return true;
        }

        //Обработчик команды сброса процесса в исходное состояние
        private bool resetProcessHandler(object parameter, IMasterConnection connection, ASDU asdu, byte qrp)
        {
            return true;
        }

        //Обработчик команды определения запаздывания
        private bool delayAcquisitionHandler(object parameter, IMasterConnection connection, ASDU asdu, CP16Time2a delayTime)
        {
            return true;
        }

    }
}
