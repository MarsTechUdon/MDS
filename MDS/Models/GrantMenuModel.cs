using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class GrantMenuModel
    {
        public List<UserGroupModel> ugmData { get; set; }
        public string UserGId { get; set; }
    }
}