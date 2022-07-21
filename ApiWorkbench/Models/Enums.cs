using System;
namespace ApiWorkbench.Models
{
    public enum BloodPressureType
    {
        Normal = 0 ,
        Prehypertension,
        Hypertension,
        Prehypotension,
        Hypotension
    }
    public class Enums {
        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}

