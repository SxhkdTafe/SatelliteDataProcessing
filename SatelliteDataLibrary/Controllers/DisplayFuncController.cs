using SatelliteDataLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatelliteDataLibrary.Controllers
{
    public class DisplayFuncController
    {
         internal DataManagement Basic = new DataManagement();

        public (List<string> A, List<string> B) DataHandler(int sig, int mu)
        {
            List<string> listA = new List<string>();
            List<string> listB = new List<string>();
            Basic.LoadDataToList(mu, sig, DataManagement.Sensors.SensorA);
            Basic.LoadDataToList(mu, sig, DataManagement.Sensors.SensorB);
            var sensorAData = Basic.ShowAllSensorData(DataManagement.Sensors.SensorA);
            var sensorBData = Basic.ShowAllSensorData(DataManagement.Sensors.SensorB);
            foreach( var sensor in sensorAData)
            {
                listA.Add(sensor.ToString());
            }
            foreach (var sensor in sensorBData)
            {
                listB.Add(sensor.ToString());
            }
            return (listA,listB);
        }
    }
}
