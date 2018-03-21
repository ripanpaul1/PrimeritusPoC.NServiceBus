using Lateetud.Utilities;
using Lateetud.Utilities.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Lateetud.NServiceBus.Subscriber.LibClasses
{
    public class PrimeritusXmlService
    {
        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
        public partial class Vendor_AssignmentRecord_v13
        {

            private string clientNameField;

            private string assignmentTypeNameField;

            private string assignmentIdentityField;

            private string clientAccountNumberField;

            private string loanNumberField;

            private string lienHolderField;

            private string yearField;

            private string makeField;

            private string modelField;

            private string vinField;

            private string clientContactField;

            private string collateralTypeField;

            private string clientCompanyIDField;

            private Vendor_AssignmentRecord_v13Person[] personsField;

            private Vendor_AssignmentRecord_v13Address[] addressesField;

            private Vendor_AssignmentRecord_v13Contact[] contactInformationField;

            /// <remarks/>
            public string ClientName
            {
                get
                {
                    return this.clientNameField;
                }
                set
                {
                    this.clientNameField = value;
                }
            }

            /// <remarks/>
            public string AssignmentTypeName
            {
                get
                {
                    return this.assignmentTypeNameField;
                }
                set
                {
                    this.assignmentTypeNameField = value;
                }
            }

            /// <remarks/>
            public string AssignmentIdentity
            {
                get
                {
                    return this.assignmentIdentityField;
                }
                set
                {
                    this.assignmentIdentityField = value;
                }
            }

            /// <remarks/>
            public string ClientAccountNumber
            {
                get
                {
                    return this.clientAccountNumberField;
                }
                set
                {
                    this.clientAccountNumberField = value;
                }
            }

            /// <remarks/>
            public string LoanNumber
            {
                get
                {
                    return this.loanNumberField;
                }
                set
                {
                    this.loanNumberField = value;
                }
            }

            /// <remarks/>
            public string LienHolder
            {
                get
                {
                    return this.lienHolderField;
                }
                set
                {
                    this.lienHolderField = value;
                }
            }

            /// <remarks/>
            public string Year
            {
                get
                {
                    return this.yearField;
                }
                set
                {
                    this.yearField = value;
                }
            }

            /// <remarks/>
            public string Make
            {
                get
                {
                    return this.makeField;
                }
                set
                {
                    this.makeField = value;
                }
            }

            /// <remarks/>
            public string Model
            {
                get
                {
                    return this.modelField;
                }
                set
                {
                    this.modelField = value;
                }
            }

            /// <remarks/>
            public string Vin
            {
                get
                {
                    return this.vinField;
                }
                set
                {
                    this.vinField = value;
                }
            }

            /// <remarks/>
            public string ClientContact
            {
                get
                {
                    return this.clientContactField;
                }
                set
                {
                    this.clientContactField = value;
                }
            }

            /// <remarks/>
            public string CollateralType
            {
                get
                {
                    return this.collateralTypeField;
                }
                set
                {
                    this.collateralTypeField = value;
                }
            }

            /// <remarks/>
            public string ClientCompanyID
            {
                get
                {
                    return this.clientCompanyIDField;
                }
                set
                {
                    this.clientCompanyIDField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Person", IsNullable = false)]
            public Vendor_AssignmentRecord_v13Person[] Persons
            {
                get
                {
                    return this.personsField;
                }
                set
                {
                    this.personsField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Address", IsNullable = false)]
            public Vendor_AssignmentRecord_v13Address[] Addresses
            {
                get
                {
                    return this.addressesField;
                }
                set
                {
                    this.addressesField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlArrayItemAttribute("Contact", IsNullable = false)]
            public Vendor_AssignmentRecord_v13Contact[] ContactInformation
            {
                get
                {
                    return this.contactInformationField;
                }
                set
                {
                    this.contactInformationField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class Vendor_AssignmentRecord_v13Person
        {

            private string first_NameField;

            private string sSNField;

            private string debtor_Type_NameField;

            /// <remarks/>
            public string First_Name
            {
                get
                {
                    return this.first_NameField;
                }
                set
                {
                    this.first_NameField = value;
                }
            }

            /// <remarks/>
            public string SSN
            {
                get
                {
                    return this.sSNField;
                }
                set
                {
                    this.sSNField = value;
                }
            }

            /// <remarks/>
            public string Debtor_Type_Name
            {
                get
                {
                    return this.debtor_Type_NameField;
                }
                set
                {
                    this.debtor_Type_NameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class Vendor_AssignmentRecord_v13Address
        {

            private string debtor_Type_NameField;

            private string address_Line_1Field;

            private string cityField;

            private string stateField;

            private string zip_CodeField;

            private string debtor_Address_Type_NameField;

            /// <remarks/>
            public string Debtor_Type_Name
            {
                get
                {
                    return this.debtor_Type_NameField;
                }
                set
                {
                    this.debtor_Type_NameField = value;
                }
            }

            /// <remarks/>
            public string Address_Line_1
            {
                get
                {
                    return this.address_Line_1Field;
                }
                set
                {
                    this.address_Line_1Field = value;
                }
            }

            /// <remarks/>
            public string City
            {
                get
                {
                    return this.cityField;
                }
                set
                {
                    this.cityField = value;
                }
            }

            /// <remarks/>
            public string State
            {
                get
                {
                    return this.stateField;
                }
                set
                {
                    this.stateField = value;
                }
            }

            /// <remarks/>
            public string Zip_Code
            {
                get
                {
                    return this.zip_CodeField;
                }
                set
                {
                    this.zip_CodeField = value;
                }
            }

            /// <remarks/>
            public string Debtor_Address_Type_Name
            {
                get
                {
                    return this.debtor_Address_Type_NameField;
                }
                set
                {
                    this.debtor_Address_Type_NameField = value;
                }
            }
        }

        /// <remarks/>
        [System.SerializableAttribute()]
        [System.ComponentModel.DesignerCategoryAttribute("code")]
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class Vendor_AssignmentRecord_v13Contact
        {

            private string debtor_Type_NameField;

            private string phone_NumberField;

            private string debtor_Address_Type_NameField;

            /// <remarks/>
            public string Debtor_Type_Name
            {
                get
                {
                    return this.debtor_Type_NameField;
                }
                set
                {
                    this.debtor_Type_NameField = value;
                }
            }

            /// <remarks/>
            public string Phone_Number
            {
                get
                {
                    return this.phone_NumberField;
                }
                set
                {
                    this.phone_NumberField = value;
                }
            }

            /// <remarks/>
            public string Debtor_Address_Type_Name
            {
                get
                {
                    return this.debtor_Address_Type_NameField;
                }
                set
                {
                    this.debtor_Address_Type_NameField = value;
                }
            }
        }

        public bool IsSendAura(string aurastring)
        {
            try
            {
                //com.renovo.test.aptest.AssignmentRequest assignment = new com.renovo.test.aptest.AssignmentRequest();
                //System.Net.ServicePointManager.ServerCertificateValidationCallback = ((sender, cert, chain, errors) => cert.Subject.Contains("test.renovo.com"));
                //assignment.Credentials = new NetworkCredential("rpaul", "Recovery@1991");
                //if (assignment._AssignmentRequest(aurastring) <= 0) return false;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public FileModel XmlToAuraString(string XmlString)
        {
            if (XmlString == null) return null;
            FileModel TheFile = new FileModel();
            try
            {
                var vendor_AssignmentRecord_v13 = (Vendor_AssignmentRecord_v13)new XmlSerializer(typeof(Vendor_AssignmentRecord_v13)).Deserialize(new MemoryStream(Encoding.UTF8.GetBytes(XmlString)));
                if (vendor_AssignmentRecord_v13 == null)
                {
                    TheFile.FileContent = "";
                    TheFile.Status = PStatus.Failed;
                    TheFile.StatusText = "Failed";
                }
                else
                {
                    StringBuilder data = new StringBuilder();

                    #region assignment data
                    data.Append("[[[3_AssignmentIdentity:::" + vendor_AssignmentRecord_v13.AssignmentIdentity + "]]]");
                    data.Append("[[[3_AssignmentTypeName:::" + vendor_AssignmentRecord_v13.AssignmentTypeName + "]]]");
                    data.Append("[[[3_ClientReferenceNumber:::" + vendor_AssignmentRecord_v13.ClientAccountNumber + "]]]");
                    data.Append("[[[3_ClientCompanyID:::" + vendor_AssignmentRecord_v13.ClientCompanyID + "]]]");
                    data.Append("[[[3_ClientContact:::" + vendor_AssignmentRecord_v13.ClientContact + "]]]");
                    data.Append("[[[3_ClientName:::" + vendor_AssignmentRecord_v13.ClientName + "]]]");
                    data.Append("[[[3_CollateralType:::" + vendor_AssignmentRecord_v13.CollateralType + "]]]");
                    data.Append("[[[3_LoanNumber:::" + vendor_AssignmentRecord_v13.LoanNumber + "]]]");
                    data.Append("[[[3_LienHolder:::" + vendor_AssignmentRecord_v13.LienHolder + "]]]");
                    data.Append("[[[3_Make:::" + vendor_AssignmentRecord_v13.Make + "]]]");
                    data.Append("[[[3_Model:::" + vendor_AssignmentRecord_v13.Model + "]]]");
                    data.Append("[[[3_Vin:::" + vendor_AssignmentRecord_v13.Vin + "]]]");
                    data.Append("[[[3_Year:::" + vendor_AssignmentRecord_v13.Year + "]]]");
                    #endregion

                    #region person
                    if (vendor_AssignmentRecord_v13.Persons != null && vendor_AssignmentRecord_v13.Persons.Length > 0)
                    {
                        data.Append("[[[6_Person:::|||");
                        for (int prow = 0; prow < vendor_AssignmentRecord_v13.Persons.Length; prow++)
                        {
                            if (prow > 0) data.Append("|||___|||");
                            data.Append("3_First Name===" + vendor_AssignmentRecord_v13.Persons[prow].First_Name + "|||");
                            data.Append("3_SSN===" + vendor_AssignmentRecord_v13.Persons[prow].SSN + "|||");
                            data.Append("3_PersonTypeName===" + vendor_AssignmentRecord_v13.Persons[prow].Debtor_Type_Name);
                        }
                        data.Append("]]]");
                    }
                    #endregion

                    #region Address
                    if (vendor_AssignmentRecord_v13.Addresses != null && vendor_AssignmentRecord_v13.Addresses.Length > 0)
                    {
                        data.Append("[[[6_Address:::|||");
                        for (int arow = 0; arow < vendor_AssignmentRecord_v13.Addresses.Length; arow++)
                        {
                            if (arow > 0) data.Append("|||___|||");
                            data.Append("3_PersonTypeID===" + vendor_AssignmentRecord_v13.Addresses[arow].Debtor_Type_Name + "|||");
                            data.Append("3_AddressLine1===" + vendor_AssignmentRecord_v13.Addresses[arow].Address_Line_1 + "|||");
                            data.Append("3_City===" + vendor_AssignmentRecord_v13.Addresses[arow].City + "|||");
                            data.Append("3_State===" + vendor_AssignmentRecord_v13.Addresses[arow].State + "|||");
                            data.Append("3_ZipCode===" + vendor_AssignmentRecord_v13.Addresses[arow].Zip_Code + "|||");
                            data.Append("3_DebtorAddressTypeID===" + vendor_AssignmentRecord_v13.Addresses[arow].Debtor_Address_Type_Name);
                        }
                        data.Append("]]]");
                    }
                    #endregion

                    #region ContactInformation
                    if (vendor_AssignmentRecord_v13.ContactInformation != null && vendor_AssignmentRecord_v13.ContactInformation.Length > 0)
                    {
                        data.Append("[[[6_ContactInformation:::|||");
                        for (int crow = 0; crow < vendor_AssignmentRecord_v13.ContactInformation.Length; crow++)
                        {
                            if (crow > 0) data.Append("|||___|||");
                            data.Append("3_PersonTypeCode===" + vendor_AssignmentRecord_v13.ContactInformation[crow].Debtor_Type_Name + "|||");
                            data.Append("3_Phone_Number===" + vendor_AssignmentRecord_v13.ContactInformation[crow].Phone_Number + "|||");
                            data.Append("3_AddressTypeCode===" + vendor_AssignmentRecord_v13.ContactInformation[crow].Debtor_Address_Type_Name);
                        }
                        data.Append("]]]");
                    }
                    #endregion

                    TheFile.FileContent = data.ToString();

                    
                }
            }
            catch (Exception ex)
            {
                TheFile.Status = PStatus.Failed;
                TheFile.StatusText = ex.Message;
            }
            return TheFile;
        }
        
    }
}
