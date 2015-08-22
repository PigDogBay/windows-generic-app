using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MpdBaileyTechnology.GenericApp.Model
{
    public class Reading
    {
        public double Temperature { get; private set; }
        public DateTime TimeStamp { get; private set; }

        public Reading()
        {
            TimeStamp = DateTime.Now;
        }
        public Reading(double temp)
        {
            Temperature = temp;
            TimeStamp = DateTime.Now;
        }
        public Reading(double temp, DateTime timeStamp)
        {
            TimeStamp = timeStamp;
            Temperature = temp;
            TimeStamp = DateTime.Now;
        }

        public override string ToString()
        {
            return String.Format("{0} {1:N2}", TimeStamp.ToLongTimeString(), Temperature);
        }
    }
}
