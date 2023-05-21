using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AlfasiWeb
{
    public class DataStruct
    {
        public enum LoanType
        {
            HOUSELOAN = 11,
            SELFEMPLOYEMENT = 3,
            MEDICAL = 2,
            EDUCATIONAL = 4
        };
        public enum ServiceStatus
        {
            KIA = 0,
            WIA=1
        };
        public enum InstallementRole
        {
            Chairman = 1,
            Accountant = 2
        };
        public enum DisabilityItemMaster
        {
            Artificial_Leg=1,
            Artificial_Hand = 2,
            Wheel_Chair = 3,
            Crutches = 4,
            Clutches=5,
            White_Stick=6,
            Surgery_Shoe=7,
            Gel_Sock=8,
            Commod_Seat=9,
            Roho_Cusion=10,
            Eye_Spec=11,
            HearingAid=12,
            AirMattress=13,
            SpecialBike=15,
            Bastam=16,
            Other=17
        };
        public enum MeasurementUnit
        {
            Each = 1,
            Gram = 2,
            KG = 3,
            ml = 4,
            L = 5,
            Pkt = 6,
            Bottle = 7,
            Tin = 8,
            Cup = 9,
            Scoop = 10,
            Pax = 11
        };
    }
}