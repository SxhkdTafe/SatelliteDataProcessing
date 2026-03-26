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
            if (type == Sensors.SensorA) A.Clear();
            if (type == Sensors.SensorB) B.Clear();
            for (int i = 0; i < length; i++)
            {
                double value = type switch
                {
                    Sensors.SensorA => lol.SensorA(mu, sig),
                    Sensors.SensorB => lol.SensorB(mu, sig),
                    _ => throw new ArgumentException()
                };

                if (type == Sensors.SensorA) A.AddLast(value);
                else B.AddLast(value);
            }
        }
        public List<double> ShowAllSensorData (Sensors type)
        {  
            List<double> data = new List<double>();
            var list = type == Sensors.SensorA ? A : B;

            if (list == null || list.First == null)
                return data;
            var current = list.First;
            while (current != null)
            {
                data.Add(current.Value);
                current = current.Next;
            }
            return data;
        }
        public int NumberOfNodes(LinkedList<double> list)
        {
            if (list == null || list.First == null)
            {
                return -1;
            }
            return list?.Count ?? 0;
        }
    }
}
