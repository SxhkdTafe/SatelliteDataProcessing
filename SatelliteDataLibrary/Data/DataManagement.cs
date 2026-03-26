using Galileo6;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteDataLibrary.Data
{
    public class DataManagement
    {
        private ReadData lol = new ReadData();
        public LinkedList<double> A  { get; private set; }
        public LinkedList<double> B { get; private set; }
        public DataManagement()
        {
            A = new LinkedList<double>();
            B = new LinkedList<double>();
        }
        public enum Sensors{
            SensorA , SensorB
        }
        public void LoadDataToList(int mu, int sig, Sensors type) 
        {
            int length = 400;
            for (int i = 0; i < length; i++)
            {
                double value = type switch
                {
                    Sensors.SensorA => lol.SensorA(mu, sig),
                    Sensors.SensorB => lol.SensorB(mu, sig),
                    _ => throw new ArgumentException()
                };

                if (type == Sensors.SensorA) A.AddLast(value);
                if (type == Sensors.SensorB) B.AddLast(value);
            }
        }
        public List<string> ShowAllSensorData (int mu, int sig, Sensors type)
        {  
            List<string> data = new List<string>();
            if (A == null || A.First == null || B == null || B.First == null) return data;
            var currentA = A.First;
            var currentB = B.First;
            for (int i = 0;i < 400 ;i++)
            {
                if (type == Sensors.SensorA) 
                {
                    data.Add(currentA.Value.ToString());
                    currentA = currentA.Next;
                }
                if (type == Sensors.SensorB)
                {
                    data.Add(currentB.Value.ToString()); ;
                    currentB = currentB.Next;
                }
            }
            return data;
        }
        public int NumberOfNodes(LinkedList<double> list)
        {
            if (list == null || list.First == null)
            {
                return -1;
            }
            return list.Count;
        }
    }
}
