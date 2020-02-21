using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCDIAMDemo
{
    public class UCDIAMUserPRAssignment
    {
        public string iamId { get; set; }
        public string assocRank { get; set; }
        public string apptDeptCode { get; set; }
        public string apptDeptDisplayName { get; set; }
        public string titleCode { get; set; }
        public string titleDisplayName { get; set; }
        public string positionType { get; set; }
        public string percentFullTime { get; set; }
        public string assocEndDate { get; set; }
        public string assocStartDate { get; set; }
        public string emplClassDesc { get; set; }

        public UCDIAMUserPRAssignment()
        {
            iamId = string.Empty;
            assocRank = string.Empty;
            apptDeptCode = string.Empty;
            apptDeptDisplayName = string.Empty;
            titleCode = string.Empty;
            titleDisplayName = string.Empty;
            positionType = string.Empty;
            percentFullTime = string.Empty;
            assocEndDate = string.Empty;
            assocStartDate = string.Empty;
            emplClassDesc = string.Empty;
        }


    }
}
