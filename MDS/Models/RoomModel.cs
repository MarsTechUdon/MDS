using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class RoomModel
    {
        //internal object username;

        public string Ind { get; set; }
        public string User { get; set; }
        public string RoomNo { get; set; }
        public string RoomName { get; set; }
        public string Seat { get; set; }
        public string Area { get; set; }
        public object Status { get; set; }
        public string result { get; set; }
        public string Username { get; internal set; }
        public string msg { get; set; }
        public string Insertdate { get; set; }
        // public object Username { get; internal set; }
        public List<string> arrMenuId { get; set; }

    }
}