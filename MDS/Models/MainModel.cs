using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class MainModel
    {       
        public MainDetailModel Main { get; set; }
        public List<MainDetailModel> Branch { get; set; }
    }
}