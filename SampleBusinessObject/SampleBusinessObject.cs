using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BusinessObjectFramework.Helpers;
using BusinessObjectFramework;

namespace SampleBusinessObject
{
    [Serializable]
    public class SampleBusinessObject : BusinessObject
    {
        private Nullable<int> uniqueID = null;
        [DataMapping("UniqueID")]
        public Nullable<int> UniqueID
        {
            get
            {
                return this.uniqueID;
            }
            set
            {
                if (this.uniqueID != value)
                {
                    this.uniqueID = value;
                    this.isDirty = true;
                }
            }
        }

        private string uniqueName = null;
        [DataMapping("UniqueName")]
        public string UniqueName
        {
            get
            {
                return this.uniqueName;
            }
            set
            {
                if (this.uniqueName != value)
                {
                    this.uniqueName = value;
                    this.isDirty = true;
                }
            }
        }


        public static List<SampleBusinessObject> List()
        {
            List<SampleBusinessObject> list = null;

            //  This might be a stored procedure, or a web service call, whatever...
            DataSet dataSet = new DataSet();

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                if (list == null)
                    list = new List<SampleBusinessObject>();

                SampleBusinessObject sampleBusinessObject = new SampleBusinessObject();

                //  This is better than setting each property by hand
                DataMapper.SetValue(sampleBusinessObject, dataRow);

                list.Add(sampleBusinessObject);
            }

            return list;
        }

        public bool Create()
        {
            throw new NotImplementedException();
        }

        public bool Read()
        {
            throw new NotImplementedException();
        }

        public bool Update()
        {
            throw new NotImplementedException();
        }

        public bool Delete()
        {
            throw new NotImplementedException();
        }


    }
}
