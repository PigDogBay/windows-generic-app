using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MpdBaileyTechnology.GenericApp.Model
{
    public class Connection
    {
        public bool Connected { get; set; }
        public string PortName { get; set; }
        public IReadingSource ReadingSource { get; set; }
 
        public IEnumerable<String> Ports
        {
            get
            {
                yield return "USB";
                yield return "COM0";
                yield return "TCP/IP";
                yield return "Simulated";
            }
        }

        public void Connect()
        {
            if (Connected)
            {
                return;
            }
            if ("Simulated" == this.PortName)
            {
                Connected = true;
                this.ReadingSource = new Simulator();
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public void Disconnect()
        {
            Connected = false;
        }
    }
}
