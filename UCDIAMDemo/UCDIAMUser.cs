using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCDIAMDemo
{
    public class UCDIAMUser
    {
        public string ustatus { get; set; }
        public string iamId { get; set; }
        public string userId { get; set; }
        public string uuId { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string addrStreet { get; set; }
        public string ppsId { get; set; }
        public string mothraId { get; set; }
        public string studentId { get; set; }
        public string bannerPIdM { get; set; }
        public string oFirstName { get; set; }
        public string oLastName { get; set; }
        public string oMiddleName { get; set; }
        public string oFullName { get; set; }
        public string dFirstName { get; set; }
        public string dLastName { get; set; }
        public string dMiddleName { get; set; }
        public string dFullName { get; set; }
        public string isEmployee { get; set; }
        public string isHSEmployee { get; set; }
        public string isFaculty { get; set; }
        public string isStudent { get; set; }
        public string isStaff { get; set; }
        public string isExternal { get; set; }
        public string isAcademicSenate { get; set; }
        public string isAcademicFederation { get; set; }
        public string isTeachingFaculty { get; set; }
        public string isLadderRank { get; set; }
        public string ucnetId { get; set; }

        public List<UCDIAMUserPRAssignment> pr_assignments { get; set; }
        public List<UCDIAMUserSISEnrollment> sis_enrollments { get; set; }

        public UCDIAMUser()
        {
            ustatus = string.Empty;
            iamId = string.Empty;
            userId = string.Empty;
            uuId = string.Empty;
            email = string.Empty;
            phone = string.Empty;
            addrStreet = string.Empty;
            ppsId = string.Empty;
            mothraId = string.Empty;
            studentId = string.Empty;
            bannerPIdM = string.Empty;
            oFirstName = string.Empty;
            oLastName = string.Empty;
            oMiddleName = string.Empty;
            oFullName = string.Empty;
            dFirstName = string.Empty;
            dLastName = string.Empty;
            dMiddleName = string.Empty;
            dFullName = string.Empty;
            isEmployee = string.Empty;
            isHSEmployee = string.Empty;
            isFaculty = string.Empty;
            isStudent = string.Empty;
            isStaff = string.Empty;
            isExternal = string.Empty;
            isAcademicSenate = string.Empty;
            isAcademicFederation = string.Empty;
            isTeachingFaculty = string.Empty;
            isLadderRank = string.Empty;
            ucnetId = string.Empty;

            pr_assignments = new List<UCDIAMUserPRAssignment>();
            sis_enrollments = new List<UCDIAMUserSISEnrollment>();
        }


    }
}
