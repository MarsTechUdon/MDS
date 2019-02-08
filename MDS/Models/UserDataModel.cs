using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class UserDataModel
    {
        public UserModel UserData { get; set; }
        public List<PrefixModel> PrefixDataList { get; set; }
        public List<DepartmentModel> DepartmentDataList { get; set; }
        public List<UserGroupModel> UserGroupDataList { get; set; }
        public string Result { get; set; }
        public string MessageReturn { get; set; }
    }
}