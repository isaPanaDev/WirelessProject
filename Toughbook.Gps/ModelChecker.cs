using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;
namespace Toughbook.Gps
{
    internal class ModelChecker
    {
        public static bool IsPanasonic
        {
            get
            {
                ManagementClass managementClass = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = managementClass.GetInstances();
                string model = "";
                if (moc.Count != 0)
                {

                    foreach (ManagementObject mo in managementClass.GetInstances())
                    {

                        model = mo["Model"].ToString();
                        return model.StartsWith("CF");
                    }

                }
                return false;
            }
          
        }
        public static string Model
        {
            get
            {
                ManagementClass managementClass = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = managementClass.GetInstances();
                string model = "";
                if (moc.Count != 0)
                {

                    foreach (ManagementObject mo in managementClass.GetInstances())
                    {

                        model = mo["Model"].ToString();
                        if (model.StartsWith("CF"))
                        {
                            return model.Substring(0, 5);
                        }
                        return "";
                    }

                }
                return model;
            }

        }
        public static string ModelNo
        {
            get
            {
                ManagementClass managementClass = new ManagementClass("Win32_ComputerSystem");
                ManagementObjectCollection moc = managementClass.GetInstances();
                string model = "";
                if (moc.Count != 0)
                {
                    
                    foreach (ManagementObject mo in managementClass.GetInstances())
                    {

                       model = mo["Model"].ToString();
                       break;
                    }
                    
                }
                return model;
            }
          
        }
    }
}
