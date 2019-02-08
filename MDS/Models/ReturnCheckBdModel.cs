using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class ReturnCheckBdModel
    {
        public string result { get; set; }
        public List<ResultCheckBdModel> resultlist { get; internal set; }
    }
}