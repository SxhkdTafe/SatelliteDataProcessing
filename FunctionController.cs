using SatelliteDataLibrary.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;


namespace SatelliteDataProcessing
{
    class FunctionController
    {
        private DataManagement Basic = new DataManagement();
        
        public void Display(Button Btn, ListBox ListBox, ListView ListA, ListView ListB, TextBox mu, TextBox sigma )
        {
            if( Btn == null ) return;
            if (ListBox == null) return; 
            if (ListA == null) return;
            if (ListB == null) return;
            ListBox.Items.Clear();
            ListA.Items.Clear();
            ListB.Items.Clear();
            Basic.LoadDataToList(Convert.ToInt32(mu), Convert.ToInt32(sigma), DataManagement.Sensors.SensorA);
            Basic.LoadDataToList(Convert.ToInt32(mu), Convert.ToInt32(sigma), DataManagement.Sensors.SensorB);
            
        }
    }
}
