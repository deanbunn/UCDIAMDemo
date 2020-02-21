using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCDIAMDemo
{
    public class UCDIAMUserSISEnrollment
    {
        public string iamId { get; set; }
        public string levelCode { get; set; }
        public string levelName { get; set; }
        public string classCode { get; set; }
        public string className { get; set; }
        public string collegeCode { get; set; }
        public string collegeName { get; set; }
        public string majorCode { get; set; }
        public string majorName { get; set; }

        public UCDIAMUserSISEnrollment()
        {
            iamId = string.Empty;
            levelCode = string.Empty;
            levelName = string.Empty;
            classCode = string.Empty;
            className = string.Empty;
            collegeCode = string.Empty;
            collegeName = string.Empty;
            majorCode = string.Empty;
            majorName = string.Empty;
        }



    }
}
