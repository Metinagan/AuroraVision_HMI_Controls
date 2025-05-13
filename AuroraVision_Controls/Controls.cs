using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AuroraVision_Controls
{
    public class Controls : HMI.IUserHMIControlsProvider
    {
        public Type[] GetCustomControlTypes()
        {
            Type[] types = new Type[25];
            types[0] = typeof(ClickDragDrop);
            types[1] = typeof(Buttons);
            types[2] = typeof(CameraContol);
            types[3] = typeof(cameraView);
            types[4] = typeof(ButtonAçıklama);
            types[5] = typeof(progressBarH);
            types[6] = typeof(progressBarRadial);
            types[7] = typeof(toggleSwitch);
            types[8] = typeof(signalChart);
            types[9] = typeof(pieChart);
            types[10] = typeof(splitContainerCustom);
            types[11] = typeof(tableLayoutCustom);
            types[12] = typeof(LastFault);
            types[13] = typeof(PointChart);
            types[14] = typeof(mjpegStream);
            types[15] = typeof(numericUpDownCustom);
            types[16] = typeof(table);
            types[17] = typeof(DynamicTable);






           
            
           
            return types;
        }
    }
}
