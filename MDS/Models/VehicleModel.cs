using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class VehicleModel
    {
        public string Ind { get; set; }
        public string User { get; set; }
	    public string Carno1 { get; set; }
        public string CarNo2 { get; set; }
        public string CarChangWat { get; set; }
        public string CarFull { get; set; }
        public string CarType { get; set; }
        public string GearID { get; set; }
        public string CarSubType { get; set; }
        public string Brand { get; set; }
        public string ChassisNo { get; set; }
        public string EngineNo { get; set; }
        public string ExpireDate { get; set; }
        public string Flag { get; set; }
        public string status { get; internal set; }
        public string Message { get; internal set; }
        public string Result { get; internal set; }
    }
}