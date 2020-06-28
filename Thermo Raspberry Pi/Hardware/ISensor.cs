using System;
using System.Collections.Generic;
using System.Text;

namespace Thermo_Raspberry_Pi.Hardware
{
    public interface ISensor
    {
        public IEnumerable<Temperature> GetTemperatures();
        public IEnumerable<string> GetSensors();
    }
}
