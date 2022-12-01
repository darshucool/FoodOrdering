using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using BusinessObjectFramework.Helpers;

namespace AlfasiWeb.Helpers
{
    public class XMLReader
    {
        private void DiplayUploadDocuments(string ParaUid)
        {
            //  Read database and display file links for this page
            string pageName = "Prototype";

            //  Update OfficeUploads
            string resources = @"D:\Sudesh\Project\AlfasiWeb";

            if (File.Exists(resources + @"\Resources\OfficeUploads.xml"))
            {
                //  Read in the page visits data file
                DataSet dataSet = DataAccess.ReadXMLDataSet(resources + @"\Resources\OfficeUploads.xml");


                string createdOn = string.Empty;
                string createdBy = string.Empty;
                string updatedOn = string.Empty;
                string updatedBy = string.Empty;
                string fileName = string.Empty;
                string rawFilePath = string.Empty;
                string webFilePath = string.Empty;
                string relativePath = string.Empty;
                string status = string.Empty;
                string Uid = string.Empty;

               


                //  Find the URL and update it if it exists. If not, add it.

                int count = 0;
                foreach (DataRow dataRow in dataSet.Tables["Table1"].Rows)
                {
                    //if (dataRow["Uid"].ToString() == ParaUid)
                    if(true)
                    {
                        if (dataRow["PageName"].ToString().ToLower() == pageName.ToLower())
                        {
                            count++;
                            createdOn = dataRow["CreatedOn"].ToString();
                            createdBy = dataRow["CreatedBy"].ToString();
                            updatedOn = dataRow["UpdatedOn"].ToString();
                            updatedBy = dataRow["UpdatedBy"].ToString();
                            fileName = dataRow["FileName"].ToString();
                            rawFilePath = dataRow["RawFilePath"].ToString();
                            webFilePath = dataRow["WebPath"].ToString();
                            relativePath = dataRow["RelativePath"].ToString();
                            status = dataRow["Status"].ToString();


                            if (status == "0")
                                continue;

                            //  Dynamically add new Hyperlink controls to this page
                          
                        }
                    }

                }

               
            }

        }
    }
}