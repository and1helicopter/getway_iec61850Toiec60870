using System;
using System.Collections.Generic;

namespace Gateway.DataMap
{
    public static class Periodic
    {
        //Объявления делегата периодического обновления данных
        public delegate void PeriodicHandler();
        
        //Объявления события периодического обновления данных
        public static event PeriodicHandler periodic;

        static List<object> periodicList = new List<object>();
        
        public static void InitCOT()
        {
            periodic += Update;
        }

        public static void Update()
        {
            //Как обновляем
            foreach (var periodicItem in periodicList)
            {
                
            }
            
        }

        public static void Start()
        {
            //Когда обновляем
            
        }

        public static void Add(object item)
        {
            periodicList.Add(item);
        }

        public static void Clear()
        {
            periodicList.Clear();
        }
    }

    public class PeriodicItem
    {
        //Указатель на ASDU
        private dynamic ItemASDU { get; set; }
        private dynamic Value { get; set; }
        private DateTime Time { get; set; }
        
        
        
        

    }
}