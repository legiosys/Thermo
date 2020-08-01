using System;
using System.Collections.Generic;
using System.Text;

namespace Thermo_Raspberry_Pi.Hardware
{
    public class MockSensor : ISensor
    {
        public IEnumerable<Temperature> GetTemperatures()
        {
            var temp = new Temperature();
            temp.HardwareId = "Human";
            temp.Value = 36.6F;
            yield return temp;
            temp.HardwareId = "Dog";
            temp.Value = 42F;
            yield return temp;
        }
        public IEnumerable<string> GetSensors()
        {
            yield return "Human";
            yield return "Dog";
        }

        public string GetSerial()
        {
            return "TestUser";
        }
    }
}
