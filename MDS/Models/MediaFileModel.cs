using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class MediaFileModel
    {
        public string MediaId { get; set; }
        public string FDesc { get; set; }
        public string FName { get; set; }
        public string FType { get; set; }
        public string FSize { get; set; }
        public string FLink { get; set; }
        public string IpAddress { get; set; }
        public string MediaLoad { get; set; }
        public string Status { get; set; }
        public string CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string ModifyDate { get; set; }
        public string ModifyBy { get; set; }
    }
}