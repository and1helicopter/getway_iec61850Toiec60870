using System.Linq;
using System.Net;
using lib60870;
using lib60870.CS101;

namespace IEC_60870
{
    public partial class Server
    {
        //Проверка  ip - адреса клиента на белый и черный список
        private static bool connectionRequestHandler(object parameter, IPAddress ipAddress)
        {
            if (!WhiteListIP.FindAll(x => x.ToString().Equals(ipAddress.ToString())).Any() && WhiteListIP.Any())
            {
                return false;
            }
            else if (BlackListIP.FindAll(x => x.ToString().Equals(ipAddress.ToString())).Any())
            {
                return false;
            }
            else
                return true;
        }

        //Оброботчик запросов от клиента
        private static bool asduHandler(object parameter, IMasterConnection connection, ASDU asdu)
        {
            return true;
        }

        //Обработчик команд опроса
        private static bool interrogationHandler(object parameter, IMasterConnection connection, ASDU asdu, byte qoi)
        {
            return true;
        }

        //Обработчик команд опроса счетчиков 
        private static bool counterInterrogationHandler(object parameter, IMasterConnection connection, ASDU asdu, byte qoi)
        {
            return true;
        }

        //Обработчик комаанд чтения
        private static bool readHandler(object parameter, IMasterConnection connection, ASDU asdu, int ioa)
        {
            return true;
        }

        //Обработчик команд синхронизации часов
        private static bool clockSynchronizationHandler(object parameter, IMasterConnection connection, ASDU asdu, CP56Time2a newTime)
        {
            return true;
        }

        //Обработчик команды сброса процесса в исходное состояние
        private static bool resetProcessHandler(object parameter, IMasterConnection connection, ASDU asdu, byte qrp)
        {
            return true;
        }

        //Обработчик команды определения запаздывания
        private static bool delayAcquisitionHandler(object parameter, IMasterConnection connection, ASDU asdu, CP16Time2a delayTime)
        {
            return true;
        }

    }
}
