using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCDIAMDemo
{
    public class UCDIAMPPSDeptInfo
    {

        public string deptCode { get; set; }
        public string deptOfficialName { get; set; }
        public string deptDisplayName { get; set; }
        public string deptAbbrev { get; set; }
        public bool isUCDHS { get; set; }

        public UCDIAMPPSDeptInfo()
        {
            deptAbbrev = string.Empty;
            deptCode = string.Empty;
            deptDisplayName = string.Empty;
            deptOfficialName = string.Empty;
            isUCDHS = false;
        }



    }
}
