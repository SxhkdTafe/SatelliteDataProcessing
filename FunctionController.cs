using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SatelliteDataLibrary.Data;


namespace SatelliteDataProcessing
{
    class FunctionController
    {
        private DataManagement Basic = new DataManagement();
        
        public void Display(ListBox ListBox, ListView ListA, ListView ListB, TextBox mu, TextBox sigma )
        {
            if (!int.TryParse(mu.Text, out int muValue) || !int.TryParse(sigma.Text, out int sigmaValue))
            {
                MessageBox.Show("Invalid input");
                return;
            }
            if (ListBox == null) return; 
            if (ListA == null) return;
            if (ListB == null) return;
            ListBox.Items.Clear();
            ListA.Items.Clear();
            ListB.Items.Clear();
            Basic.LoadDataToList(Convert.ToInt32(mu.Text), Convert.ToInt32(sigma.Text), DataManagement.Sensors.SensorA);
            Basic.LoadDataToList(Convert.ToInt32(mu.Text), Convert.ToInt32(sigma.Text), DataManagement.Sensors.SensorB);
            var sensorAData = Basic.ShowAllSensorData(DataManagement.Sensors.SensorA);
            var sensorBData = Basic.ShowAllSensorData(DataManagement.Sensors.SensorB);

            foreach( var sensor in sensorAData )
            {
                ListBox.Items.Add($"A: {sensor}");
                ListA.Items.Add(sensor);
            }
            foreach (var sensor in sensorBData)
            {
                ListBox.Items.Add($"B: {sensor}");
                ListB.Items.Add(sensor);
            }
        }
    }
}
