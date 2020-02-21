using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace UCDIAMDemo
{
    public class UCDIAMUserWrkr
    {
        public string wrkStatus { get; set; }
        public string iamURL { get; set; }
        public string iamKey { get; set; }


        public UCDIAMUserWrkr()
        {
            //Set Worker Status to Empty
            wrkStatus = string.Empty;

            iamKey = "XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX";
            iamURL = "https://xxxxxxxxxxx.ucdavis.edu/api/iam/";

        }


        public List<UCDIAMPPSDeptInfo> Get_All_IAM_PPS_Depts_Infos()
        {
            //Var for Return Value
            List<UCDIAMPPSDeptInfo> lAIMPPSDeptInfos = new List<UCDIAMPPSDeptInfo>();

            //Create Http Web Request to Pull PPS Depts Information
            HttpWebRequest hwreqUCDDptCodeInfo = (HttpWebRequest)WebRequest.Create(iamURL + "orginfo/pps/depts?key=" + iamKey + "&v=1.0");
            hwreqUCDDptCodeInfo.Method = "GET";
            hwreqUCDDptCodeInfo.ContentType = "application/json; charset=utf-8";

            //Web Response for UCD Dept Information
            HttpWebResponse hwrespUCDDptCodeInfo = (HttpWebResponse)hwreqUCDDptCodeInfo.GetResponse();

            //Stream Reader to Convert JSON Data
            StreamReader strmrdrUCDDptCodeInfo = new StreamReader(hwrespUCDDptCodeInfo.GetResponseStream());

            //Convert Returned JSON to Dynamic Object
            dynamic dynjUCDDptCodeInfo = JsonConvert.DeserializeObject<dynamic>(strmrdrUCDDptCodeInfo.ReadToEnd());

            //Close Connections and Reader
            hwrespUCDDptCodeInfo.Close();
            strmrdrUCDDptCodeInfo.Close();

            //Null\Empty Checks on Dynamic Objects and Data
            if (dynjUCDDptCodeInfo != null && dynjUCDDptCodeInfo.responseData != null && (int)dynjUCDDptCodeInfo.responseData.results.Count > 0)
            {

                foreach (var uDptCodeInfoRslt in dynjUCDDptCodeInfo.responseData.results)
                {
                    //Initiate IAM PPS Dept Report Object
                    UCDIAMPPSDeptInfo aipdi = new UCDIAMPPSDeptInfo();

                    aipdi.deptAbbrev = uDptCodeInfoRslt.deptAbbrev;
                    aipdi.deptCode = uDptCodeInfoRslt.deptCode;
                    aipdi.deptDisplayName = uDptCodeInfoRslt.deptDisplayName;
                    aipdi.deptOfficialName = uDptCodeInfoRslt.deptOfficialName;
                    aipdi.isUCDHS = (bool)uDptCodeInfoRslt.isUCDHS;

                    lAIMPPSDeptInfos.Add(aipdi);

                }//End of dynjUCDDptCodeInfo.responseData.results foreach

            }//End of dynjUCDDptCodeInfo Null Check

            return lAIMPPSDeptInfos;
        }

        public List<UCDIAMUserPRAssignment> Get_IAM_PRAssignments_By_ApptDept_Codes(List<string> lDeptCodes)
        {

            //Initiate List for Return
            List<UCDIAMUserPRAssignment> lRtnPRAssignments = new List<UCDIAMUserPRAssignment>();

            if (lDeptCodes != null && lDeptCodes.Count > 0 && string.IsNullOrEmpty(iamKey) == false && string.IsNullOrEmpty(iamURL) == false)
            {

                foreach (string deptCode in lDeptCodes)
                {
                    //Create Http Web Request to Pull IAM UCPath Associations
                    HttpWebRequest hwreqUCDUCPathAssocs = (HttpWebRequest)WebRequest.Create(iamURL + "associations/pps/search?key=" + iamKey + "&v=1.0&apptDeptCode=" + deptCode);

                    hwreqUCDUCPathAssocs.Method = "GET";
                    hwreqUCDUCPathAssocs.ContentType = "application/json; charset=utf-8";

                    //Web Response for IAM UCPath Associations
                    HttpWebResponse hwrespUCDUCPathAssocs = (HttpWebResponse)hwreqUCDUCPathAssocs.GetResponse();

                    //Stream Reader to Convert JSON Data
                    StreamReader strmrdrUCDUCPathAssocs = new StreamReader(hwrespUCDUCPathAssocs.GetResponseStream());

                    //Convert Returned JSON to Dynamic Object
                    dynamic dynjUCDUCPathAssocs = JsonConvert.DeserializeObject<dynamic>(strmrdrUCDUCPathAssocs.ReadToEnd());

                    //Close Connections and Reader
                    hwrespUCDUCPathAssocs.Close();
                    strmrdrUCDUCPathAssocs.Close();

                    //Null\Empty Checks on Dynamic Objects and Data
                    if (dynjUCDUCPathAssocs != null && dynjUCDUCPathAssocs.responseData != null && (int)dynjUCDUCPathAssocs.responseData.results.Count > 0)
                    {

                        foreach (var dynPPSAssoc in dynjUCDUCPathAssocs.responseData.results)
                        {

                            //Initiate PR Assignment
                            UCDIAMUserPRAssignment prasign = new UCDIAMUserPRAssignment();

                            //Assign PPS Information
                            prasign.iamId = dynPPSAssoc.iamId;
                            prasign.apptDeptCode = dynPPSAssoc.apptDeptCode;
                            prasign.apptDeptDisplayName = dynPPSAssoc.apptDeptDisplayName;
                            prasign.titleCode = dynPPSAssoc.titleCode;
                            prasign.titleDisplayName = dynPPSAssoc.titleDisplayName;
                            prasign.emplClassDesc = dynPPSAssoc.emplClassDesc;
                            prasign.assocStartDate = dynPPSAssoc.assocStartDate;

                            //Add PR Assignment to List
                            lRtnPRAssignments.Add(prasign);

                        }//End of responseData.results foreach

                    }//End of dynjUCDPPSAssocs Null\Empty Checks

                }//End of lDeptCodes Foreach

            }//End of lDeptCodes Null\Empty Checks

            return lRtnPRAssignments;
        }

        public UCDIAMUser Get_IAM_User_By_KerbID(string uKerbID)
        {
            //Initiate Return Object
            UCDIAMUser ucdiamusr = new UCDIAMUser();

            //Null\Empty Check on IAM Key
            if (string.IsNullOrEmpty(iamKey) == false && string.IsNullOrEmpty(iamURL) == false && string.IsNullOrEmpty(uKerbID) == false)
            {

                //Create Http Web Request to Pull User General Information
                HttpWebRequest hwreqUCDGenInfo = (HttpWebRequest)WebRequest.Create(iamURL + "people/prikerbacct/search?key=" + iamKey + "&v=1.0&userId=" + uKerbID);
                hwreqUCDGenInfo.Method = "GET";
                hwreqUCDGenInfo.ContentType = "application/json; charset=utf-8";

                //Web Response for UCD Gen Information
                HttpWebResponse hwrespUCDGenInfo = (HttpWebResponse)hwreqUCDGenInfo.GetResponse();

                //Stream Reader to Convert JSON Data
                StreamReader strmrdrUCDGenInfo = new StreamReader(hwrespUCDGenInfo.GetResponseStream());

                //Convert Returned JSON to Dynamic Object
                dynamic dynjUCDGenInfo = JsonConvert.DeserializeObject<dynamic>(strmrdrUCDGenInfo.ReadToEnd());

                //Close Connections and Reader
                hwrespUCDGenInfo.Close();
                strmrdrUCDGenInfo.Close();

                //Null\Empty Checks on Dynamic Objects and Data
                if (dynjUCDGenInfo != null && dynjUCDGenInfo.responseData != null && (int)dynjUCDGenInfo.responseData.results.Count > 0)
                {

                    foreach (var uGenInfoRslt in dynjUCDGenInfo.responseData.results)
                    {
                        ucdiamusr.iamId = uGenInfoRslt.iamId;
                        ucdiamusr.userId = uGenInfoRslt.userId;
                        ucdiamusr.uuId = uGenInfoRslt.uuId;
                    }

                }

                //Check for IAM ID
                if (string.IsNullOrEmpty(ucdiamusr.iamId) == false)
                {

                    //Create Http Web Request to Pull User's Contact Information
                    HttpWebRequest hwreqUCDPeopleInfo = (HttpWebRequest)WebRequest.Create(iamURL + "people/" + ucdiamusr.iamId + "?key=" + iamKey + "&v=1.0");
                    hwreqUCDPeopleInfo.Method = "GET";
                    hwreqUCDPeopleInfo.ContentType = "application/json; charset=utf-8";

                    //Web Response for UCD Contact Information
                    HttpWebResponse hwrespUCDPeopleInfo = (HttpWebResponse)hwreqUCDPeopleInfo.GetResponse();

                    //Stream Reader to Convert JSON Data
                    StreamReader strmrdrUCDPeopleInfo = new StreamReader(hwrespUCDPeopleInfo.GetResponseStream());

                    //Convert Returned JSON to Dynamic Object
                    dynamic dynjUCDPeopleInfo = JsonConvert.DeserializeObject<dynamic>(strmrdrUCDPeopleInfo.ReadToEnd());

                    //Close Connections and Reader
                    hwrespUCDPeopleInfo.Close();
                    strmrdrUCDPeopleInfo.Close();

                    //Null\Empty Checks on Dynamic Objects and Data
                    if (dynjUCDPeopleInfo != null && dynjUCDPeopleInfo.responseData != null && (int)dynjUCDPeopleInfo.responseData.results.Count > 0)
                    {

                        foreach (var uPeopleInfoRslt in dynjUCDPeopleInfo.responseData.results)
                        {
                            ucdiamusr.ppsId = uPeopleInfoRslt.ppsId;
                            ucdiamusr.mothraId = uPeopleInfoRslt.mothraId;
                            ucdiamusr.studentId = uPeopleInfoRslt.studentId;
                            ucdiamusr.bannerPIdM = uPeopleInfoRslt.bannerPIdM;
                            ucdiamusr.oFirstName = uPeopleInfoRslt.oFirstName;
                            ucdiamusr.oMiddleName = uPeopleInfoRslt.oMiddleName;
                            ucdiamusr.oLastName = uPeopleInfoRslt.oLastName;
                            ucdiamusr.oFullName = uPeopleInfoRslt.oFullName;
                            ucdiamusr.dFullName = uPeopleInfoRslt.dFullName;
                            ucdiamusr.dFirstName = uPeopleInfoRslt.dFirstName;
                            ucdiamusr.dMiddleName = uPeopleInfoRslt.dMiddleName;
                            ucdiamusr.dLastName = uPeopleInfoRslt.dLastName;

                            //isEmployee Check
                            if (uPeopleInfoRslt.isEmployee == true)
                            {
                                ucdiamusr.isEmployee = "Yes";
                            }
                            else
                            {
                                ucdiamusr.isEmployee = "No";
                            }

                            //isHSEmployee Check
                            if (uPeopleInfoRslt.isHSEmployee == true)
                            {
                                ucdiamusr.isHSEmployee = "Yes";
                            }
                            else
                            {
                                ucdiamusr.isHSEmployee = "No";
                            }

                            //isFaculty Check
                            if (uPeopleInfoRslt.isFaculty == true)
                            {
                                ucdiamusr.isFaculty = "Yes";
                            }
                            else
                            {
                                ucdiamusr.isFaculty = "No";
                            }

                            //isStudent Check
                            if (uPeopleInfoRslt.isStudent == true)
                            {
                                ucdiamusr.isStudent = "Yes";
                            }
                            else
                            {
                                ucdiamusr.isStudent = "No";
                            }

                            //isStaff Check
                            if (uPeopleInfoRslt.isStaff == true)
                            {
                                ucdiamusr.isStaff = "Yes";
                            }
                            else
                            {
                                ucdiamusr.isStaff = "No";
                            }

                            //isStaff Check
                            if (uPeopleInfoRslt.isExternal == true)
                            {
                                ucdiamusr.isExternal = "Yes";
                            }
                            else
                            {
                                ucdiamusr.isExternal = "No";
                            }


                        }//End of dynjUCDContactInfo.responseData.results foreach

                    }//End of dynjUCDContactInfo Null Check


                    //Create Http Web Request to Pull IAM Affiliations
                    HttpWebRequest hwreqUCDAffiliationsInfo = (HttpWebRequest)WebRequest.Create(iamURL + "people/affiliations/" + ucdiamusr.iamId + "?key=" + iamKey + "&v=1.0");
                    hwreqUCDAffiliationsInfo.Method = "GET";
                    hwreqUCDAffiliationsInfo.ContentType = "application/json; charset=utf-8";

                    //Web Response for IAM Affiliations
                    HttpWebResponse hwrespUCDAffiliationsInfo = (HttpWebResponse)hwreqUCDAffiliationsInfo.GetResponse();

                    //Stream Reader to Convert JSON Data
                    StreamReader strmrdrUCDAffiliationsInfo = new StreamReader(hwrespUCDAffiliationsInfo.GetResponseStream());

                    //Convert Returned JSON to Dynamic Object
                    dynamic dynjUCDAffiliationsInfo = JsonConvert.DeserializeObject<dynamic>(strmrdrUCDAffiliationsInfo.ReadToEnd());

                    //Close Connections and Reader
                    hwrespUCDAffiliationsInfo.Close();
                    strmrdrUCDAffiliationsInfo.Close();

                    //Null\Empty Checks on Dynamic Objects and Data
                    if (dynjUCDAffiliationsInfo != null && dynjUCDAffiliationsInfo.responseData != null && (int)dynjUCDAffiliationsInfo.responseData.results.Count > 0)
                    {

                        foreach (var uIAMAffiliationsInfo in dynjUCDAffiliationsInfo.responseData.results)
                        {

                            //isAcademicSenate Check
                            if (uIAMAffiliationsInfo.isAcademicSenate == true)
                            {
                                ucdiamusr.isAcademicSenate = "Yes";
                            }
                            else
                            {
                                ucdiamusr.isAcademicSenate = "No";
                            }

                            //isAcademicFederation Check
                            if (uIAMAffiliationsInfo.isAcademicFederation == true)
                            {
                                ucdiamusr.isAcademicFederation = "Yes";
                            }
                            else
                            {
                                ucdiamusr.isAcademicFederation = "No";
                            }

                            //isTeachingFaculty Check
                            if (uIAMAffiliationsInfo.isTeachingFaculty == true)
                            {
                                ucdiamusr.isTeachingFaculty = "Yes";
                            }
                            else
                            {
                                ucdiamusr.isTeachingFaculty = "No";
                            }

                            //isLadderRank Check
                            if (uIAMAffiliationsInfo.isLadderRank == true)
                            {
                                ucdiamusr.isLadderRank = "Yes";
                            }
                            else
                            {
                                ucdiamusr.isLadderRank = "No";
                            }
                        }

                    }//End of dynjUCDAffiliationsInfo Null\Empty Checks


                    //Create Http Web Request to Pull User's Contact Information
                    HttpWebRequest hwreqUCDContactInfo = (HttpWebRequest)WebRequest.Create(iamURL + "people/contactinfo/" + ucdiamusr.iamId + "?key=" + iamKey + "&v=1.0");
                    hwreqUCDContactInfo.Method = "GET";
                    hwreqUCDContactInfo.ContentType = "application/json; charset=utf-8";

                    //Web Response for UCD Contact Information
                    HttpWebResponse hwrespUCDContactInfo = (HttpWebResponse)hwreqUCDContactInfo.GetResponse();

                    //Stream Reader to Convert JSON Data
                    StreamReader strmrdrUCDContactInfo = new StreamReader(hwrespUCDContactInfo.GetResponseStream());

                    //Convert Returned JSON to Dynamic Object
                    dynamic dynjUCDContactInfo = JsonConvert.DeserializeObject<dynamic>(strmrdrUCDContactInfo.ReadToEnd());

                    //Close Connections and Reader
                    hwrespUCDContactInfo.Close();
                    strmrdrUCDContactInfo.Close();

                    //Null\Empty Checks on Dynamic Objects and Data
                    if (dynjUCDContactInfo != null && dynjUCDContactInfo.responseData != null && (int)dynjUCDContactInfo.responseData.results.Count > 0)
                    {

                        foreach (var uContactInfoRslt in dynjUCDContactInfo.responseData.results)
                        {
                            ucdiamusr.email = uContactInfoRslt.email;
                            ucdiamusr.phone = uContactInfoRslt.workPhone;
                            ucdiamusr.addrStreet = uContactInfoRslt.addrStreet;

                        }//End of dynjUCDContactInfo.responseData.results foreach

                    }//End of dynjUCDContactInfo Null Check



                    //Create Http Web Request to Pull User's PPS Information
                    HttpWebRequest hwreqUCDPPSInfo = (HttpWebRequest)WebRequest.Create(iamURL + "associations/pps/" + ucdiamusr.iamId + "?key=" + iamKey + "&v=1.0");
                    hwreqUCDPPSInfo.Method = "GET";
                    hwreqUCDPPSInfo.ContentType = "application/json; charset=utf-8";

                    //Web Response for UCD Gen Information
                    HttpWebResponse hwrespUCDPPSInfo = (HttpWebResponse)hwreqUCDPPSInfo.GetResponse();

                    //Stream Reader to Convert JSON Data
                    StreamReader strmrdrUCDPPSInfo = new StreamReader(hwrespUCDPPSInfo.GetResponseStream());

                    //Convert Returned JSON to Dynamic Object
                    dynamic dynjUCDPPSInfo = JsonConvert.DeserializeObject<dynamic>(strmrdrUCDPPSInfo.ReadToEnd());

                    //Close Connections and Reader
                    hwrespUCDPPSInfo.Close();
                    strmrdrUCDPPSInfo.Close();

                    //Null\Empty Checks on Dynamic Objects and Data
                    if (dynjUCDPPSInfo != null && dynjUCDPPSInfo.responseData != null && (int)dynjUCDPPSInfo.responseData.results.Count > 0)
                    {

                        foreach (var uPPSInfoRslt in dynjUCDPPSInfo.responseData.results)
                        {
                            //Initiate PR Assignment
                            UCDIAMUserPRAssignment prasign = new UCDIAMUserPRAssignment();

                            //Assign PPS Information
                            prasign.iamId = uPPSInfoRslt.iamId;
                            prasign.assocRank = uPPSInfoRslt.assocRank;
                            prasign.apptDeptCode = uPPSInfoRslt.apptDeptCode;
                            prasign.apptDeptDisplayName = uPPSInfoRslt.apptDeptDisplayName;
                            prasign.titleCode = uPPSInfoRslt.titleCode;
                            prasign.titleDisplayName = uPPSInfoRslt.titleDisplayName;
                            prasign.positionType = uPPSInfoRslt.positionType;
                            prasign.percentFullTime = uPPSInfoRslt.percentFullTime;
                            prasign.assocEndDate = uPPSInfoRslt.assocEndDate;
                            prasign.emplClassDesc = uPPSInfoRslt.emplClassDesc;

                            //Add PR Assignment to IAM User PR Listings
                            ucdiamusr.pr_assignments.Add(prasign);

                        }//End of dynjUCDPPSInfo.responseData.results foreach

                    }//End of dynjUCDPPSInfo Null Check


                    //Pull SIS Info if Student
                    if (ucdiamusr.isStudent == "Yes")
                    {
                        //Create Http Web Request to Pull User's PPS Information
                        HttpWebRequest hwreqUCDSISInfo = (HttpWebRequest)WebRequest.Create(iamURL + "associations/sis/" + ucdiamusr.iamId + "?key=" + iamKey + "&v=1.0");
                        hwreqUCDSISInfo.Method = "GET";
                        hwreqUCDSISInfo.ContentType = "application/json; charset=utf-8";

                        //Web Response for UCD Gen Information
                        HttpWebResponse hwrespUCDSISInfo = (HttpWebResponse)hwreqUCDSISInfo.GetResponse();

                        //Stream Reader to Convert JSON Data
                        StreamReader strmrdrUCDSISInfo = new StreamReader(hwrespUCDSISInfo.GetResponseStream());

                        //Convert Returned JSON to Dynamic Object
                        dynamic dynjUCDSISInfo = JsonConvert.DeserializeObject<dynamic>(strmrdrUCDSISInfo.ReadToEnd());

                        //Close Connections and Reader
                        hwrespUCDSISInfo.Close();
                        strmrdrUCDSISInfo.Close();

                        //Null\Empty Checks on Dynamic Objects and Data
                        if (dynjUCDSISInfo != null && dynjUCDSISInfo.responseData != null && (int)dynjUCDSISInfo.responseData.results.Count > 0)
                        {

                            foreach (var uSISInfoRslt in dynjUCDSISInfo.responseData.results)
                            {
                                //Initiate SIS Enrollment
                                UCDIAMUserSISEnrollment sisenroll = new UCDIAMUserSISEnrollment();

                                //Assign SIS Information
                                sisenroll.iamId = uSISInfoRslt.iamId;
                                sisenroll.classCode = uSISInfoRslt.classCode;
                                sisenroll.className = uSISInfoRslt.className;
                                sisenroll.collegeCode = uSISInfoRslt.collegeCode;
                                sisenroll.collegeName = uSISInfoRslt.collegeName;
                                sisenroll.levelCode = uSISInfoRslt.levelCode;
                                sisenroll.levelName = uSISInfoRslt.levelName;
                                sisenroll.majorCode = uSISInfoRslt.majorCode;
                                sisenroll.majorName = uSISInfoRslt.majorName;

                                //Add SIS Enrollment to IAM User SIS Enrollments
                                ucdiamusr.sis_enrollments.Add(sisenroll);

                            }//End of dynjUCDSISInfo.responseData.results foreach

                        }//End of dynjUCDSISInfo Null Check

                    }//End of isStudent Check

                }//End of IAM ID Null\Empty Check


            }
            else
            {
                ucdiamusr.ustatus = "No Connection Info";
            }//End of IAM Key Null\Empty Check

            return ucdiamusr;

        }


    }
}
