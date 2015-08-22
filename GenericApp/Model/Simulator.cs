using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MpdBaileyTechnology.GenericApp.Model
{
    public class Simulator : IReadingSource
    {
        private double count = 0;
        public Reading GetReading()
        {
            count += 0.1;
            return new Reading(Math.Sin(count));
        }
    }
}
