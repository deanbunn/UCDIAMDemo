using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace UCDIAMDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            //Initiate IAM Worker 
            UCDIAMUserWrkr iamWrkr = new UCDIAMUserWrkr();

            //Pull All Departments
            List<UCDIAMPPSDeptInfo> lUCDDepts = iamWrkr.Get_All_IAM_PPS_Depts_Infos();

            foreach (UCDIAMPPSDeptInfo dptInfo in lUCDDepts.OrderBy(r => r.deptDisplayName))
            {
                Console.WriteLine(dptInfo.deptCode + " - " + dptInfo.deptDisplayName);
            }

            //List for Dept Codes to Lookup Payroll Associations
            List<string> lDeptCodes = new List<string>();
            lDeptCodes.Add("036005,000036");

            //List of Payroll Assignments for Departments
            List<UCDIAMUserPRAssignment> lPMAssignments = iamWrkr.Get_IAM_PRAssignments_By_ApptDept_Codes(lDeptCodes);

            foreach (UCDIAMUserPRAssignment prAssgnmt in lPMAssignments)
            {
                Console.WriteLine(prAssgnmt.iamId + " - " + prAssgnmt.titleCode + " - " + prAssgnmt.titleDisplayName);
            }

            //Pull IAM User by Kerb ID
            UCDIAMUser iamUsr = iamWrkr.Get_IAM_User_By_KerbID("dbunn");

            Console.WriteLine("Full Name: " + iamUsr.oFullName);
            Console.WriteLine("Email Address: " + iamUsr.email);
            Console.WriteLine("Is Faculty: " + iamUsr.isAcademicSenate);
            Console.WriteLine("Is Staff: " + iamUsr.isStaff);

            foreach(UCDIAMUserPRAssignment usrPRAsgnmt in iamUsr.pr_assignments)
            {
                Console.WriteLine(usrPRAsgnmt.titleCode + " - " + usrPRAsgnmt.emplClassDesc + " - " + usrPRAsgnmt.titleDisplayName);
            }


            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine("All Done");
            Console.ReadLine();

        }
    }
}
