using Lateetud.PrimeritusService.Manager.Models;
using Lateetud.Utilities;
using Lateetud.Utilities.ExcelManager;
using Lateetud.Utilities.Models;
using Lateetud.Utilities.XmlManager;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace Lateetud.PrimeritusService.Manager.LibClasses
{
    public class LateetudService : Controller
    {
        public VMExcel ExcelToXml(HttpPostedFileBase file, string SheetName, string rootPath, string DirExcel, string DirXml)
        {
            VMExcel vMExcel = new VMExcel
            {
                OriginalExcelFileName = null,
                UploadedExcelFileName = null,
                Message = "You have not specified a excel file.",
                Status = PStatus.None
            };
            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    string strdatetime = DateTime.Now.ToString("yyyyMMdd") + "-" + DateTime.Now.ToString("HHmmss");
                    vMExcel.OriginalExcelFileName = Path.GetFileName(file.FileName);
                    vMExcel.UploadedExcelFileName = strdatetime + "-" + vMExcel.OriginalExcelFileName;
                    vMExcel.UploadedExcelFilePath = Path.Combine(rootPath, DirExcel, vMExcel.UploadedExcelFileName);
                    file.SaveAs(vMExcel.UploadedExcelFilePath);

                    DataTable dtexcel = new ExcelManagerByNetClasses().ReadExcel(vMExcel.UploadedExcelFilePath, SheetName);
                    if (dtexcel.Rows.Count == 1)
                    {
                        vMExcel.Message = "Nothing in the excel sheet";
                        vMExcel.Status = PStatus.Failed;
                    }
                    else
                    {
                        List<string> PersonTag = new List<string>(new string[] { "<Persons>", "</Persons>", "<Person>", "</Person>" });
                        List<string> AddressTag = new List<string>(new string[] { "<Addresses>", "</Addresses>", "<Address>", "</Address>" });
                        List<string> ContactTag = new List<string>(new string[] { "<ContactInformation>", "</ContactInformation>", "<Contact>", "</Contact>" });

                        StringBuilder xml_fullcontent = new StringBuilder();
                        StringBuilder xml = new StringBuilder();
                        for (int row = 1; row < dtexcel.Rows.Count; row++)
                        {
                            // Assignment - starttag
                            xml.Clear();
                            xml.Append(@"<?xml version=""1.0""?><Vendor_AssignmentRecord_v13 xmlns:xsd = ""http://www.w3.org/2001/XMLSchema"" xmlns:xsi = ""http://www.w3.org/2001/XMLSchema-instance"">");

                            #region Assignment Data
                            // F1 - ClientName < CLIENTNAME ></ CLIENTNAME >
                            xml.Append("<ClientName>" + Convert.ToString(dtexcel.Rows[row]["F1"]).Trim() + "</ClientName>");
                            // F2 - AssignmentTypeName < ASSIGNMENT_TYPE ></ ASSIGNMENT_TYPE >
                            xml.Append("<AssignmentTypeName>" + Convert.ToString(dtexcel.Rows[row]["F2"]).Trim() + "</AssignmentTypeName>");
                            // F3 - AssignmentIdentity < ASSIGNMENTID ></ ASSIGNMENTID >
                            string AssignmentIdentity = Convert.ToString(dtexcel.Rows[row]["F3"]).Trim();
                            xml.Append("<AssignmentIdentity>" + AssignmentIdentity + "</AssignmentIdentity>");
                            // F10 - AssignedCSR < CSR ></ CSR >
                            //xml.Append("<AssignedCSR>" + Convert.ToString(dtexcel.Rows[row]["F10"]) + "</AssignedCSR>");
                            // F17 - ClientAccountNumber < CLIENTREFERENCENUMBER ></ CLIENTREFERENCENUMBER >
                            xml.Append("<ClientAccountNumber>" + Convert.ToString(dtexcel.Rows[row]["F17"]).Trim() + "</ClientAccountNumber>");
                            // F18 - LoanNumber < CLIENTLOANNUMBER ></ CLIENTLOANNUMBER >
                            xml.Append("<LoanNumber>" + Convert.ToString(dtexcel.Rows[row]["F18"]).Trim() + "</LoanNumber>");
                            // F34 - LienHolder < LienHolder ></ LienHolder >
                            xml.Append("<LienHolder>" + Convert.ToString(dtexcel.Rows[row]["F34"]).Trim() + "</LienHolder>");
                            // F19 - Year < YEAR ></ YEAR >
                            xml.Append("<Year>" + Convert.ToString(dtexcel.Rows[row]["F19"]).Trim() + "</Year>");
                            // F20 - Make < MAKE ></ MAKE >
                            xml.Append("<Make>" + Convert.ToString(dtexcel.Rows[row]["F20"]).Trim() + "</Make>");
                            // F21 - Model < MODEL ></ MODEL >
                            xml.Append("<Model>" + Convert.ToString(dtexcel.Rows[row]["F21"]).Trim() + "</Model>");
                            // F22 - Vin < VINSERIALNUM ></ VINSERIALNUM >
                            xml.Append("<Vin>" + Convert.ToString(dtexcel.Rows[row]["F22"]).Trim() + "</Vin>");
                            // F31 - ClientContact < CLIENT_CONTACT ></ CLIENT_CONTACT >
                            xml.Append("<ClientContact>" + Convert.ToString(dtexcel.Rows[row]["F31"]).Trim() + "</ClientContact>");
                            // F32 - CollateralType < COLLATERALNAME ></ COLLATERALNAME >
                            xml.Append("<CollateralType>" + Convert.ToString(dtexcel.Rows[row]["F32"]).Trim() + "</CollateralType>");
                            // F33 - ClientCompanyID < CLIENTID ></ CLIENTID >
                            xml.Append("<ClientCompanyID>" + Convert.ToString(dtexcel.Rows[row]["F33"]).Trim() + "</ClientCompanyID>");
                            #endregion

                            #region Persons
                            if (Convert.ToString(dtexcel.Rows[row]["F23"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F24"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F25"]).Trim() != "")
                            {
                                if (!StaticUtilities.IsSubstringValueExist(xml.ToString(), PersonTag[0])) xml.Append(PersonTag[0]);
                                #region Maker
                                // Maker - starttag
                                xml.Append(PersonTag[2]);

                                // F23 - FirstName < CUSTOMER ></ CUSTOMER >
                                xml.Append("<First_Name>" + Convert.ToString(dtexcel.Rows[row]["F23"]).Trim() + "</First_Name>");
                                // F24 - SSN < Customer SSN ></ Customer SSN >
                                xml.Append("<SSN>" + Convert.ToString(dtexcel.Rows[row]["F24"]).Trim() + "</SSN>");
                                // F25 - DebtorTypeName < Person Type ></ Person Type >
                                xml.Append("<Debtor_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F25"]).Trim() + "</Debtor_Type_Name>");
                                
                                // Maker - endtag
                                xml.Append(PersonTag[3]);
                                #endregion
                            }

                            if (Convert.ToString(dtexcel.Rows[row]["F27"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F29"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F28"]).Trim() != "")
                            {
                                if (!StaticUtilities.IsSubstringValueExist(xml.ToString(), PersonTag[0])) xml.Append(PersonTag[0]);
                                #region Co-Maker
                                // Co-Maker - starttag
                                xml.Append(PersonTag[2]);

                                // F27 - FirstName < Comaker ></ Comaker >
                                xml.Append("<First_Name>" + Convert.ToString(dtexcel.Rows[row]["F27"]).Trim() + "</First_Name>");
                                // F29 - SSN < Comaker SSN ></ Comaker SSN >
                                xml.Append("<SSN>" + Convert.ToString(dtexcel.Rows[row]["F29"]).Trim() + "</SSN>");
                                // F28 - DebtorTypeName < Person Type ></ Person Type >
                                xml.Append("<Debtor_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F28"]).Trim() + "</Debtor_Type_Name>");

                                // Co-Maker - endtag
                                xml.Append(PersonTag[3]);
                                #endregion
                            }
                            if (StaticUtilities.IsSubstringValueExist(xml.ToString(), PersonTag[0])) xml.Append(PersonTag[1]);
                            #endregion

                            #region Addresses

                            #region Maker Address
                            // Maker Address 1
                            if (Convert.ToString(dtexcel.Rows[row]["F4"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F5"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F6"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F7"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F9"]).Trim() != "")
                            {
                                if (!StaticUtilities.IsSubstringValueExist(xml.ToString(), AddressTag[0])) xml.Append(AddressTag[0]);
                                #region Maker Address 1
                                // Maker Address 1 - starttag
                                xml.Append(AddressTag[2]);

                                // F25 - DebtorTypeName < Person Type ></ Person Type >
                                xml.Append("<Debtor_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F25"]).Trim() + "</Debtor_Type_Name>");
                                // F4 - AddressLine1 < maker_ADDRESS ></ maker_ADDRESS >
                                xml.Append("<Address_Line_1>" + Convert.ToString(dtexcel.Rows[row]["F4"]).Trim() + "</Address_Line_1>");
                                // F5 - City < maker_CITY ></ maker_CITY >
                                xml.Append("<City>" + Convert.ToString(dtexcel.Rows[row]["F5"]).Trim() + "</City>");
                                // F6 - State < maker_STATE ></ maker_STATE >
                                xml.Append("<State>" + Convert.ToString(dtexcel.Rows[row]["F6"]).Trim() + "</State>");
                                // F7 - ZipCode < maker_ZIP ></ maker_ZIP >
                                xml.Append("<Zip_Code>" + Convert.ToString(dtexcel.Rows[row]["F7"]).Trim() + "</Zip_Code>");
                                // F9 - DebtorAddressTypeName < Address Type ></ Address Type >
                                xml.Append("<Debtor_Address_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F9"]).Trim() + "</Debtor_Address_Type_Name>");

                                // Maker Address 1 - endtag
                                xml.Append(AddressTag[3]);
                                #endregion
                            }

                            // Maker Address 2
                            if (Convert.ToString(dtexcel.Rows[row]["F35"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F36"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F37"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F38"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F40"]).Trim() != "")
                            {
                                if (!StaticUtilities.IsSubstringValueExist(xml.ToString(), AddressTag[0])) xml.Append(AddressTag[0]);
                                #region Maker Address 2
                                // Maker Address 2 - starttag
                                xml.Append(AddressTag[2]);

                                // F25 - DebtorTypeName < Person Type ></ Person Type >
                                xml.Append("<Debtor_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F25"]).Trim() + "</Debtor_Type_Name>");
                                // F35 - AddressLine1 < maker_ADDRESS_2 ></ maker_ADDRESS_2 >
                                xml.Append("<Address_Line_1>" + Convert.ToString(dtexcel.Rows[row]["F35"]).Trim() + "</Address_Line_1>");
                                // F36 - City < maker_CITY_2 ></ maker_CITY_2 >
                                xml.Append("<City>" + Convert.ToString(dtexcel.Rows[row]["F36"]).Trim() + "</City>");
                                // F37 - State < maker_STATE_2 ></ maker_STATE_2 >
                                xml.Append("<State>" + Convert.ToString(dtexcel.Rows[row]["F37"]).Trim() + "</State>");
                                // F38 - ZipCode < maker_ZIP_2 ></ maker_ZIP_2 >
                                xml.Append("<Zip_Code>" + Convert.ToString(dtexcel.Rows[row]["F38"]).Trim() + "</Zip_Code>");
                                // F40 - DebtorAddressTypeName < Address Type ></ Address Type >
                                xml.Append("<Debtor_Address_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F40"]).Trim() + "</Debtor_Address_Type_Name>");

                                // Maker Address 2 - endtag
                                xml.Append(AddressTag[3]);
                                #endregion
                            }
                            #endregion

                            #region Co-Maker Address
                            // Co-Maker Address 1
                            if (Convert.ToString(dtexcel.Rows[row]["F11"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F12"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F13"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F14"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F16"]).Trim() != "")
                            {
                                if (!StaticUtilities.IsSubstringValueExist(xml.ToString(), AddressTag[0])) xml.Append(AddressTag[0]);
                                #region Co-Maker Address 1
                                // Co-Maker Address 1 - starttag
                                xml.Append(AddressTag[2]);

                                // F28 - DebtorTypeName < Person Type ></ Person Type >
                                xml.Append("<Debtor_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F28"]).Trim() + "</Debtor_Type_Name>");
                                // F11 - AddressLine1 < comaker_ADDRESS_1 ></ comaker_ADDRESS_1 >
                                xml.Append("<Address_Line_1>" + Convert.ToString(dtexcel.Rows[row]["F11"]).Trim() + "</Address_Line_1>");
                                // F12 - City < comaker_CITY_1 ></ comaker_CITY_1 >
                                xml.Append("<City>" + Convert.ToString(dtexcel.Rows[row]["F12"]).Trim() + "</City>");
                                // F13 - State < comaker_STATE_1 ></ comaker_STATE_1 >
                                xml.Append("<State>" + Convert.ToString(dtexcel.Rows[row]["F13"]).Trim() + "</State>");
                                // F14 - ZipCode < comaker_ZIP_1 ></ comaker_ZIP_1 >
                                xml.Append("<Zip_Code>" + Convert.ToString(dtexcel.Rows[row]["F14"]).Trim() + "</Zip_Code>");
                                // F16 - DebtorAddressTypeName < Address Type ></ Address Type >
                                xml.Append("<Debtor_Address_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F16"]).Trim() + "</Debtor_Address_Type_Name>");
                                
                                // Maker Address 1 - endtag
                                xml.Append(AddressTag[3]);
                                #endregion
                            }

                            // Co-Maker Address 2
                            if (Convert.ToString(dtexcel.Rows[row]["F41"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F42"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F43"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F44"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F46"]).Trim() != "")
                            {
                                if (!StaticUtilities.IsSubstringValueExist(xml.ToString(), AddressTag[0])) xml.Append(AddressTag[0]);
                                #region Co-Maker Address 2
                                // Co-Maker Address 2 - starttag
                                xml.Append(AddressTag[2]);

                                // F28 - DebtorTypeName < Person Type ></ Person Type >
                                xml.Append("<Debtor_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F28"]).Trim() + "</Debtor_Type_Name>");
                                // F41 - AddressLine1 < comaker_ADDRESS_2 ></ comaker_ADDRESS_2 >
                                xml.Append("<Address_Line_1>" + Convert.ToString(dtexcel.Rows[row]["F41"]).Trim() + "</Address_Line_1>");
                                // F42 - City < comaker_CITY_2 ></ comaker_CITY_2 >
                                xml.Append("<City>" + Convert.ToString(dtexcel.Rows[row]["F42"]).Trim() + "</City>");
                                // F43 - State < comaker_STATE_2 ></ comaker_STATE_2 >
                                xml.Append("<State>" + Convert.ToString(dtexcel.Rows[row]["F43"]).Trim() + "</State>");
                                // F44 - ZipCode < comaker_ZIP_2 ></ comaker_ZIP_2 >
                                xml.Append("<Zip_Code>" + Convert.ToString(dtexcel.Rows[row]["F44"]).Trim() + "</Zip_Code>");
                                // F46 - DebtorAddressTypeName < Address Type ></ Address Type >
                                xml.Append("<Debtor_Address_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F46"]).Trim() + "</Debtor_Address_Type_Name>");
                                
                                // Maker Address 2 - endtag
                                xml.Append(AddressTag[3]);
                                #endregion
                            }
                            #endregion

                            #region Address without Person
                            // Address 1
                            if (Convert.ToString(dtexcel.Rows[row]["F47"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F48"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F49"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F50"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F52"]).Trim() != "")
                            {
                                if (!StaticUtilities.IsSubstringValueExist(xml.ToString(), AddressTag[0])) xml.Append(AddressTag[0]);
                                #region Address 1
                                // Address 1 - starttag
                                xml.Append(AddressTag[2]);

                                // DebtorTypeName
                                xml.Append("<Debtor_Type_Name></Debtor_Type_Name>");

                                // F47 - AddressLine1 < ADDRESS_1 ></ ADDRESS_1 >
                                xml.Append("<Address_Line_1>" + Convert.ToString(dtexcel.Rows[row]["F47"]).Trim() + "</Address_Line_1>");
                                // F48 - City < CITY_1 ></ CITY_1 >
                                xml.Append("<City>" + Convert.ToString(dtexcel.Rows[row]["F48"]).Trim() + "</City>");
                                // F49 - State < STATE_1 ></ STATE_1 >
                                xml.Append("<State>" + Convert.ToString(dtexcel.Rows[row]["F49"]).Trim() + "</State>");
                                // F50 - ZipCode < ZIP_1 ></ ZIP_1 >
                                xml.Append("<Zip_Code>" + Convert.ToString(dtexcel.Rows[row]["F50"]).Trim() + "</Zip_Code>");
                                // F52 - DebtorAddressTypeName < Address Type ></ Address Type >
                                xml.Append("<Debtor_Address_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F52"]).Trim() + "</Debtor_Address_Type_Name>");

                                // Address 1 - endtag
                                xml.Append(AddressTag[3]);
                                #endregion
                            }

                            // Address 2
                            if (Convert.ToString(dtexcel.Rows[row]["F53"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F54"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F55"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F56"]).Trim() != "" ||
                                Convert.ToString(dtexcel.Rows[row]["F58"]).Trim() != "")
                            {
                                if (!StaticUtilities.IsSubstringValueExist(xml.ToString(), AddressTag[0])) xml.Append(AddressTag[0]);
                                #region Address 2
                                // Address 2 - starttag
                                xml.Append(AddressTag[2]);

                                // DebtorTypeName
                                xml.Append("<Debtor_Type_Name></Debtor_Type_Name>");

                                // F53 - AddressLine1 < ADDRESS_2 ></ ADDRESS_2 >
                                xml.Append("<Address_Line_1>" + Convert.ToString(dtexcel.Rows[row]["F53"]).Trim() + "</Address_Line_1>");
                                // F54 - City < CITY_2 ></ CITY_2 >
                                xml.Append("<City>" + Convert.ToString(dtexcel.Rows[row]["F54"]).Trim() + "</City>");
                                // F55 - State < STATE_2 ></ STATE_2 >
                                xml.Append("<State>" + Convert.ToString(dtexcel.Rows[row]["F55"]).Trim() + "</State>");
                                // F56 - ZipCode < ZIP_2 ></ ZIP_2 >
                                xml.Append("<Zip_Code>" + Convert.ToString(dtexcel.Rows[row]["F56"]).Trim() + "</Zip_Code>");
                                // F58 - DebtorAddressTypeName < Address Type ></ Address Type >
                                xml.Append("<Debtor_Address_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F58"]).Trim() + "</Debtor_Address_Type_Name>");

                                // Address 2 - endtag
                                xml.Append(AddressTag[3]);
                                #endregion
                            }
                            #endregion

                            if (StaticUtilities.IsSubstringValueExist(xml.ToString(), AddressTag[0])) xml.Append(AddressTag[1]);
                            #endregion

                            #region Phone Numbers

                            #region Maker Phone Number
                            // Maker Phone Number 1
                            if (Convert.ToString(dtexcel.Rows[row]["F8"]).Trim() != "")
                            {
                                if (!StaticUtilities.IsSubstringValueExist(xml.ToString(), ContactTag[0])) xml.Append(ContactTag[0]);
                                #region Maker Phone Number 1
                                // Maker Phone Number 1 - starttag
                                xml.Append(ContactTag[2]);

                                // F25 - DebtorTypeName < Person Type ></ Person Type >
                                xml.Append("<Debtor_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F25"]).Trim() + "</Debtor_Type_Name>");
                                // F8 - PhoneNumber < maker Phone_1 ></< maker Phone_1 >
                                xml.Append("<Phone_Number>" + Convert.ToString(dtexcel.Rows[row]["F8"]).Trim() + "</Phone_Number>");
                                // F9 - DebtorAddressTypeName < Address Type ></ Address Type >
                                xml.Append("<Debtor_Address_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F9"]).Trim() + "</Debtor_Address_Type_Name>");

                                // Maker Phone Number 1 - endtag
                                xml.Append(ContactTag[3]);
                                #endregion
                            }

                            // Maker Phone Number without Address
                            if (Convert.ToString(dtexcel.Rows[row]["F26"]).Trim() != "")
                            {
                                if (!StaticUtilities.IsSubstringValueExist(xml.ToString(), ContactTag[0])) xml.Append(ContactTag[0]);
                                #region Maker Phone Number
                                // Maker Phone Number - starttag
                                xml.Append(ContactTag[2]);

                                // F25 - DebtorTypeName < Person Type ></ Person Type >
                                xml.Append("<Debtor_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F25"]).Trim() + "</Debtor_Type_Name>");
                                // F26 - PhoneNumber < Maker_Phone_Number ></ Maker_Phone_Number >
                                xml.Append("<Phone_Number>" + Convert.ToString(dtexcel.Rows[row]["F26"]).Trim() + "</Phone_Number>");
                                // DebtorAddressTypeName < Address Type ></ Address Type >
                                xml.Append("<Debtor_Address_Type_Name></Debtor_Address_Type_Name>");

                                // Maker Phone Number - endtag
                                xml.Append(ContactTag[3]);
                                #endregion
                            }

                            // Maker Phone Number 2
                            if (Convert.ToString(dtexcel.Rows[row]["F39"]).Trim() != "")
                            {
                                if (!StaticUtilities.IsSubstringValueExist(xml.ToString(), ContactTag[0])) xml.Append(ContactTag[0]);
                                #region Maker Phone Number 2
                                // Maker Phone Number 2 - starttag
                                xml.Append(ContactTag[2]);

                                // F25 - DebtorTypeName < Person Type ></ Person Type >
                                xml.Append("<Debtor_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F25"]).Trim() + "</Debtor_Type_Name>");
                                // F39 - PhoneNumber < maker Phone_2 ></< maker Phone_2 >
                                xml.Append("<Phone_Number>" + Convert.ToString(dtexcel.Rows[row]["F39"]).Trim() + "</Phone_Number>");
                                // F40 - DebtorAddressTypeName < Address Type ></ Address Type >
                                xml.Append("<Debtor_Address_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F40"]).Trim() + "</Debtor_Address_Type_Name>");

                                // Maker Phone Number 2 - endtag
                                xml.Append(ContactTag[3]);
                                #endregion
                            }
                            #endregion

                            #region Co-Maker Phone Number
                            // Co Maker Phone Number 1
                            if (Convert.ToString(dtexcel.Rows[row]["F15"]).Trim() != "")
                            {
                                if (!StaticUtilities.IsSubstringValueExist(xml.ToString(), ContactTag[0])) xml.Append(ContactTag[0]);
                                #region Co Maker Phone Number 1
                                // Co Maker Phone Number 1 - starttag
                                xml.Append(ContactTag[2]);

                                // F28 - DebtorTypeName < Person Type ></ Person Type >
                                xml.Append("<Debtor_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F28"]).Trim() + "</Debtor_Type_Name>");
                                // F15 - PhoneNumber < maker Phone_1 ></< maker Phone_1 >
                                xml.Append("<Phone_Number>" + Convert.ToString(dtexcel.Rows[row]["F15"]).Trim() + "</Phone_Number>");
                                // F16 - DebtorAddressTypeName < Address Type ></ Address Type >
                                xml.Append("<Debtor_Address_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F16"]).Trim() + "</Debtor_Address_Type_Name>");

                                // Co Maker Phone Number 1 - endtag
                                xml.Append(ContactTag[3]);
                                #endregion
                            }

                            // Co Maker Phone Number without Address
                            if (Convert.ToString(dtexcel.Rows[row]["F30"]).Trim() != "")
                            {
                                if (!StaticUtilities.IsSubstringValueExist(xml.ToString(), ContactTag[0])) xml.Append(ContactTag[0]);
                                #region Co Maker Phone Number
                                // Co Maker Phone Number - starttag
                                xml.Append(ContactTag[2]);

                                // F28 - DebtorTypeName < Person Type ></ Person Type >
                                xml.Append("<Debtor_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F28"]).Trim() + "</Debtor_Type_Name>");
                                // F30 - PhoneNumber < CoMaker_Phone_Number ></ CoMaker_Phone_Number >
                                xml.Append("<Phone_Number>" + Convert.ToString(dtexcel.Rows[row]["F30"]).Trim() + "</Phone_Number>");
                                // DebtorAddressTypeName < Address Type ></ Address Type >
                                xml.Append("<Debtor_Address_Type_Name></Debtor_Address_Type_Name>");

                                // Co Maker Phone Number - endtag
                                xml.Append(ContactTag[3]);
                                #endregion
                            }

                            // Co Maker Phone Number 2
                            if (Convert.ToString(dtexcel.Rows[row]["F45"]).Trim() != "")
                            {
                                if (!StaticUtilities.IsSubstringValueExist(xml.ToString(), ContactTag[0])) xml.Append(ContactTag[0]);
                                #region Co Maker Phone Number 2
                                // Co Maker Phone Number 2 - starttag
                                xml.Append(ContactTag[2]);

                                // F28 - DebtorTypeName < Person Type ></ Person Type >
                                xml.Append("<Debtor_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F28"]).Trim() + "</Debtor_Type_Name>");
                                // F45 - PhoneNumber < maker Phone_2 ></< maker Phone_2 >
                                xml.Append("<Phone_Number>" + Convert.ToString(dtexcel.Rows[row]["F45"]).Trim() + "</Phone_Number>");
                                // F46 - DebtorAddressTypeName < Address Type ></ Address Type >
                                xml.Append("<Debtor_Address_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F46"]).Trim() + "</Debtor_Address_Type_Name>");

                                // Co Maker Phone Number 2 - endtag
                                xml.Append(ContactTag[3]);
                                #endregion
                            }
                            #endregion

                            #region Phone Number without Person
                            // Phone Number 1
                            if (Convert.ToString(dtexcel.Rows[row]["F51"]).Trim() != "")
                            {
                                if (!StaticUtilities.IsSubstringValueExist(xml.ToString(), ContactTag[0])) xml.Append(ContactTag[0]);
                                #region Phone Number 1
                                // Phone Number 1 - starttag
                                xml.Append(ContactTag[2]);

                                // DebtorTypeName
                                xml.Append("<Debtor_Type_Name></Debtor_Type_Name>");

                                // F51 - PhoneNumber < maker Phone_1 ></< maker Phone_1 >
                                xml.Append("<Phone_Number>" + Convert.ToString(dtexcel.Rows[row]["F51"]).Trim() + "</Phone_Number>");
                                // F52 - DebtorAddressTypeName < Address Type ></ Address Type >
                                xml.Append("<Debtor_Address_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F52"]).Trim() + "</Debtor_Address_Type_Name>");

                                // Phone Number 1 - endtag
                                xml.Append(ContactTag[3]);
                                #endregion
                            }

                            // Phone Number 2
                            if (Convert.ToString(dtexcel.Rows[row]["F57"]).Trim() != "")
                            {
                                if (!StaticUtilities.IsSubstringValueExist(xml.ToString(), ContactTag[0])) xml.Append(ContactTag[0]);
                                #region Phone Number 2
                                // Phone Number 2 - starttag
                                xml.Append(ContactTag[2]);

                                // DebtorTypeName
                                xml.Append("<Debtor_Type_Name></Debtor_Type_Name>");

                                // F57 - PhoneNumber < maker Phone_2 ></< maker Phone_2 >
                                xml.Append("<Phone_Number>" + Convert.ToString(dtexcel.Rows[row]["F57"]).Trim() + "</Phone_Number>");
                                // F58 - DebtorAddressTypeName < Address Type ></ Address Type >
                                xml.Append("<Debtor_Address_Type_Name>" + Convert.ToString(dtexcel.Rows[row]["F58"]).Trim() + "</Debtor_Address_Type_Name>");

                                // Phone Number 2 - endtag
                                xml.Append(ContactTag[3]);
                                #endregion
                            }
                            #endregion

                            #region Phone Number without Person and also without Address
                            // Phone Number 1
                            if (Convert.ToString(dtexcel.Rows[row]["F59"]).Trim() != "")
                            {
                                if (!StaticUtilities.IsSubstringValueExist(xml.ToString(), ContactTag[0])) xml.Append(ContactTag[0]);
                                #region Phone Number 1
                                // Phone Number 1 - starttag
                                xml.Append(ContactTag[2]);

                                // DebtorTypeName
                                xml.Append("<Debtor_Type_Name></Debtor_Type_Name>");

                                // F59 - PhoneNumber < maker Phone_1 ></< maker Phone_1 >
                                xml.Append("<Phone_Number>" + Convert.ToString(dtexcel.Rows[row]["F59"]).Trim() + "</Phone_Number>");
                                // DebtorAddressTypeName < Address Type ></ Address Type >
                                xml.Append("<Debtor_Address_Type_Name></Debtor_Address_Type_Name>");

                                // Phone Number 1 - endtag
                                xml.Append(ContactTag[3]);
                                #endregion
                            }

                            // Phone Number 2
                            if (Convert.ToString(dtexcel.Rows[row]["F60"]).Trim() != "")
                            {
                                if (!StaticUtilities.IsSubstringValueExist(xml.ToString(), ContactTag[0])) xml.Append(ContactTag[0]);
                                #region Phone Number 2
                                // Phone Number 2 - starttag
                                xml.Append(ContactTag[2]);

                                // DebtorTypeName
                                xml.Append("<Debtor_Type_Name></Debtor_Type_Name>");

                                // F60 - PhoneNumber < maker Phone_2 ></< maker Phone_2 >
                                xml.Append("<Phone_Number>" + Convert.ToString(dtexcel.Rows[row]["F60"]).Trim() + "</Phone_Number>");
                                // DebtorAddressTypeName < Address Type ></ Address Type >
                                xml.Append("<Debtor_Address_Type_Name></Debtor_Address_Type_Name>");

                                // Phone Number 2 - endtag
                                xml.Append(ContactTag[3]);
                                #endregion
                            }
                            #endregion

                            if (StaticUtilities.IsSubstringValueExist(xml.ToString(), ContactTag[0])) xml.Append(ContactTag[1]);
                            #endregion

                            // Assignment - endtag
                            xml.Append("</Vendor_AssignmentRecord_v13>");
                            xml_fullcontent.Append(xml.ToString());

                            // create the file
                            vMExcel.ConvertedXmlFileName = DateTime.Now.ToString("yyyyMMdd") + "-" + DateTime.Now.ToString("HHmmssfff") + "-" + new RandomGenerator().RandomGuid() + "-" + Path.GetFileNameWithoutExtension(vMExcel.OriginalExcelFileName) + ".xml";
                            string strxmlDir = Path.Combine(rootPath, DirXml, strdatetime);
                            if (!Directory.Exists(strxmlDir)) Directory.CreateDirectory(strxmlDir);
                            vMExcel.ConvertedXmlFilePath = Path.Combine(strxmlDir, vMExcel.ConvertedXmlFileName);
                            if (!new XmlService().CreateXml(xml.ToString(), vMExcel.ConvertedXmlFilePath))
                            {
                                vMExcel.Message = "Unable to convert. Please check excel content";
                                vMExcel.Status = PStatus.Failed;
                                break;
                            }
                        }
                        vMExcel.ConvertedXmlContent = xml_fullcontent.ToString();
                        //vMExcel.ConvertedXmlDownloadLink = Path.Combine(DirXml, vMExcel.ConvertedXmlFileName);

                        if (vMExcel.Status == PStatus.None)
                        {
                            vMExcel.Message = "File successfully converted to xml";
                            vMExcel.Status = PStatus.Success;
                        }
                    }
                }
                catch (Exception ex)
                {
                    vMExcel.Message = "ERROR:" + ex.Message.ToString();
                    vMExcel.Status = PStatus.Error;
                }
            }
            return vMExcel;
        }

        public FileModelList ReadXml(HttpPostedFileBase[] files, string DestinationPath)
        {
            StaticUtilities.ProcessTimeFormat = PTime.Milliseconds;

            FileModelList fileModelList = new FileModelList();
            fileModelList.FileModels = new GeneralService().UploadFiles(files, DestinationPath);
            if (fileModelList == null) return null;
            if (fileModelList.FileModels == null) return null;
            if (fileModelList.FileModels.Count > 0)
            {
                try
                {
                    Primeritus.PrimeritusServiceSoapClient client = new Primeritus.PrimeritusServiceSoapClient();
                    for (int TheFile = 0; TheFile < fileModelList.FileModels.Count; TheFile++)
                    {
                        DateTime dtStart = DateTime.Now;
                        if (fileModelList.FileModels[TheFile].Status == PStatus.Error)
                        {
                            DateTime dtEnd = DateTime.Now;
                            fileModelList.FileModels[TheFile].ExecutionTimeSpan = dtEnd.Subtract(dtStart);
                            fileModelList.FileModels[TheFile].TotalExecutionTimeSpan = fileModelList.FileModels[TheFile].UploadTimeSpan + fileModelList.FileModels[TheFile].ExecutionTimeSpan;
                            fileModelList.TotalProcessTimeSpan += fileModelList.FileModels[TheFile].TotalExecutionTimeSpan;
                            if (StaticUtilities.ProcessTimeFormat == PTime.Ticks)
                            {
                                fileModelList.FileModels[TheFile].ExecutionTime = fileModelList.FileModels[TheFile].ExecutionTimeSpan.Seconds.ToString("00") + "." + fileModelList.FileModels[TheFile].ExecutionTimeSpan.Ticks.ToString();
                                fileModelList.FileModels[TheFile].TotalExecutionTime = fileModelList.FileModels[TheFile].TotalExecutionTimeSpan.Seconds.ToString("00") + "." + fileModelList.FileModels[TheFile].TotalExecutionTimeSpan.Ticks.ToString();
                            }
                            else if (StaticUtilities.ProcessTimeFormat == PTime.Milliseconds)
                            {
                                fileModelList.FileModels[TheFile].ExecutionTime = fileModelList.FileModels[TheFile].ExecutionTimeSpan.Seconds.ToString("00") + "." + fileModelList.FileModels[TheFile].ExecutionTimeSpan.Milliseconds.ToString("000");
                                fileModelList.FileModels[TheFile].TotalExecutionTime = fileModelList.FileModels[TheFile].TotalExecutionTimeSpan.Seconds.ToString("00") + "." + fileModelList.FileModels[TheFile].TotalExecutionTimeSpan.Milliseconds.ToString("000");
                            }
                        }
                        else
                        {
                            XmlDocument xmldoc = new XmlDocument();
                            xmldoc.Load(fileModelList.FileModels[TheFile].FilePath);
                            string response = client.InvokeXmlService(xmldoc.InnerXml);
                            if (response == "Published")
                            {
                                fileModelList.FileModels[TheFile].Status = PStatus.Success;
                                fileModelList.FileModels[TheFile].StatusText = response;
                            }
                            else
                            {
                                fileModelList.FileModels[TheFile].Status = PStatus.Failed;
                                fileModelList.FileModels[TheFile].StatusText = "Failed";
                            }

                            DateTime dtEnd = DateTime.Now;
                            fileModelList.FileModels[TheFile].ExecutionTimeSpan = dtEnd.Subtract(dtStart);
                            fileModelList.FileModels[TheFile].TotalExecutionTimeSpan = fileModelList.FileModels[TheFile].UploadTimeSpan + fileModelList.FileModels[TheFile].ExecutionTimeSpan;
                            fileModelList.TotalProcessTimeSpan += fileModelList.FileModels[TheFile].TotalExecutionTimeSpan;

                            if (StaticUtilities.ProcessTimeFormat == PTime.Ticks)
                            {
                                fileModelList.FileModels[TheFile].ExecutionTime = fileModelList.FileModels[TheFile].ExecutionTimeSpan.Seconds.ToString("00") + "." + fileModelList.FileModels[TheFile].ExecutionTimeSpan.Ticks.ToString();
                                fileModelList.FileModels[TheFile].TotalExecutionTime = fileModelList.FileModels[TheFile].TotalExecutionTimeSpan.Seconds.ToString("00") + "." + fileModelList.FileModels[TheFile].TotalExecutionTimeSpan.Ticks.ToString();
                            }
                            else if (StaticUtilities.ProcessTimeFormat == PTime.Milliseconds)
                            {
                                fileModelList.FileModels[TheFile].ExecutionTime = fileModelList.FileModels[TheFile].ExecutionTimeSpan.Seconds.ToString("00") + "." + fileModelList.FileModels[TheFile].ExecutionTimeSpan.Milliseconds.ToString("000");
                                fileModelList.FileModels[TheFile].TotalExecutionTime = fileModelList.FileModels[TheFile].TotalExecutionTimeSpan.Seconds.ToString("00") + "." + fileModelList.FileModels[TheFile].TotalExecutionTimeSpan.Milliseconds.ToString("000");
                            }
                        }
                    }
                    if (StaticUtilities.ProcessTimeFormat == PTime.Ticks)
                        fileModelList.TotalProcessTime = fileModelList.TotalProcessTimeSpan.Hours.ToString("00") + ":" + fileModelList.TotalProcessTimeSpan.Minutes.ToString("00") + ":" + fileModelList.TotalProcessTimeSpan.Seconds.ToString("00") + "." + fileModelList.TotalProcessTimeSpan.Ticks.ToString();
                    else if (StaticUtilities.ProcessTimeFormat == PTime.Milliseconds)
                        fileModelList.TotalProcessTime = fileModelList.TotalProcessTimeSpan.Hours.ToString("00") + ":" + fileModelList.TotalProcessTimeSpan.Minutes.ToString("00") + ":" + fileModelList.TotalProcessTimeSpan.Seconds.ToString("00") + "." + fileModelList.TotalProcessTimeSpan.Milliseconds.ToString("000");
                }
                catch (Exception ex)
                {
                    return null;
                }
            }
            new GeneralService().DeleteDirectory(fileModelList.FileModels[0].DirectoryPath);
            return fileModelList;
        }
    }
}