using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using AlfasiWeb;
using AlfasiWeb.Models;
using System.Text;
using Dinota.Domain.User;
using Dinota.Domain.PageObject;
using Dinota.Domain.UserPermission;



namespace MIMS.Helpers
{
    /// <summary>
    /// This class is sepecifically writen for avoid the architecture of this project 
    /// |To minimize to loading time of the drawing registry. used as testing purpose
    /// </summary>
    public class CustomDataBaseManger
    {
        private string connectionString = Configuration.ConnectionString;

        public DataTable SelectData()
        {
            DataTable dt=new DataTable();
            try
            {
                SqlConnection conn=new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(GetSqlText(168), conn);
                SqlDataAdapter adapter=new SqlDataAdapter(command);
                adapter.Fill(dt);

            }
            catch (Exception)
            {
                throw;
            }
            return dt;

        }

        public DataTable SelectData(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);

            }
            catch (Exception)
            {
                throw;
            }
            return dt;

        }
        public bool Update(string sql)
        {
            bool status = false;
            try
            {
                SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                int rowAffected = command.ExecuteNonQuery();
                if (rowAffected > 0)
                    status = true;
              

            }
            catch (Exception)
            {
                throw;
            }
            return status;
        }
        public List<SessionNaviModel> SelectPermittedNavigation(int UserTypeId)
        {
            List<SessionNaviModel> SessionNaviModelList = new List<SessionNaviModel>();
            try
            {
                string sql = "select P.ObjectName,P.ControllerName,U.IsPermitted from [dbo].[PageObject] AS P,[dbo].[UserPermission] AS U WHERE P.UId=U.ObjectId " +
                            " AND U.UserTypeUId='" + UserTypeId + "' AND U.IsPermitted='TRUE'";
                DataTable dt = SelectData(sql);
                foreach (DataRow row in dt.Rows)
                {
                    SessionNaviModel mod = new SessionNaviModel();
                    if (!string.IsNullOrEmpty(row["ObjectName"].ToString()))
                    {
                        mod.ObjectName = row["ObjectName"].ToString();
                    }
                    if (!string.IsNullOrEmpty(row["ControllerName"].ToString()))
                    {
                        mod.ControllerName = row["ControllerName"].ToString();
                    }
                    if (!string.IsNullOrEmpty(row["IsPermitted"].ToString()))
                    {
                        mod.IsPermitted = bool.Parse(row["IsPermitted"].ToString());
                    }
                    SessionNaviModelList.Add(mod);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return SessionNaviModelList;
        }
        
        public ProfileDetailModel SelectProfileInfo(string ProfileId)
        {
            ProfileDetailModel oProfileDetailModel = new ProfileDetailModel();
            try
            {
                string sql = "Select D.Name AS DistrictName,DGD.GramaDivision,GK.KottashaDivision,KH.HouseNo,EP.* from [dbo].[District] AS D,[dbo].[DistrictGramaDivision] AS DGD,[dbo].[GramaKottasha] AS GK,[dbo].[KottashaHouseNo] " +
                    " AS KH,[dbo].[ElectorateProfile] AS EP WHERE D.UId=DGD.DistrictUId AND DGD.UId=GK.DistrictGramaDivisionUId AND "+
                    " GK.UId=KH.GramaKottashaUId AND KH.UId=EP.HouseNoRef AND EP.UId='"+ProfileId+"'";
                DataTable dt = SelectData(sql);
                foreach (DataRow row in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(row["DistrictName"].ToString()))
                    {
                        oProfileDetailModel.DistrictName = row["DistrictName"].ToString();
                    }
                    if (!string.IsNullOrEmpty(row["GramaDivision"].ToString()))
                    {
                        oProfileDetailModel.GramaniladariName = row["GramaDivision"].ToString();
                    }
                    if (!string.IsNullOrEmpty(row["KottashaDivision"].ToString()))
                    {
                        oProfileDetailModel.KottashaName = row["KottashaDivision"].ToString();
                    }
                    if (!string.IsNullOrEmpty(row["HouseNo"].ToString()))
                    {
                        oProfileDetailModel.HouseNo = row["HouseNo"].ToString();
                    }
                    if (!string.IsNullOrEmpty(row["Name"].ToString()))
                    {
                        oProfileDetailModel.Name = row["Name"].ToString();
                    }
                    if (!string.IsNullOrEmpty(row["NIC"].ToString()))
                    {
                        oProfileDetailModel.NIC = row["NIC"].ToString();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return oProfileDetailModel;
        }
        public int CheckVehicleExist(string VehicleNo)
        {
            int count=0;
            try 
	        {
                string sql = "SELECT * FROM VehicleInfo where VehicleNo='" + VehicleNo + "' AND Active='TRUE'";
                DataTable dt=SelectData(sql);
                count=dt.Rows.Count;
	        }
	        catch (Exception)
	        {
		
		        throw;
	        }
            return count;
        }
        public decimal GetOrderF140Amount(int OrderId)
        {
            decimal Amount = 0;
            try 
	        {
                string sql = "select Sum(T2.Amount)  AS Amount from [dbo].[F140Data] AS T2,[dbo].[F140Header] AS T1 where T2.F140HeaderUId=T1.UId AND T1.MenuOrderId=" + OrderId + "";
                DataTable dt=SelectData(sql);
                foreach (DataRow row in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(row["Amount"].ToString()))
                    {
                        Amount = decimal.Parse(row["Amount"].ToString());
                    }
                   
                }
              
	        }
	        catch (Exception)
	        {
		
		        throw;
	        }
            return Amount;
        }

        
        public int CheckVehicleSserviceNoExist(string ServiceNo)
        {
            int count = 0;
            try
            {
                string sql = "SELECT * FROM VehicleInfo where ServiceNo='" + ServiceNo + "' AND Active='TRUE'";
                DataTable dt = SelectData(sql);
                count = dt.Rows.Count;
            }
            catch (Exception)
            {

                throw;
            }
            return count;
        }
        public int CheckVehicleFuelDrawExist(string ServiceNo)
        {
            int count = 0;
            try
            {
                string sql = "select V.* from [dbo].[VehicleInfo] V,[dbo].[FuelDrawInfo] AS F WHERE V.UId=F.VehicleUId and V.ServiceNo='"+ ServiceNo + "' AND V.Active='TRUE'";
                DataTable dt = SelectData(sql);
                count = dt.Rows.Count;
            }
            catch (Exception)
            {

                throw;
            }
            return count;
        }
        public int UpdateVehicleStatus (string ServiceNo)
        {
            int count = 0;
            try
            {
                string sql = "update [VehicleInfo] set Active='FALSE',CheckUpdate='FALSE' where ServiceNo='" + ServiceNo + "' AND Active='TRUE'";
                bool status = Update(sql);
               
            }
            catch (Exception)
            {

                throw;
            }
            return count;
        }
        public int CheckServiceExist(string ServiceNo)
        {
            int count = 0;
            try
            {
                string sql = "SELECT * FROM VehicleInfo where ServiceNo='" + ServiceNo + "' AND Active='TRUE'";
                DataTable dt = SelectData(sql);
                count = dt.Rows.Count;
            }
            catch (Exception)
            {

                throw;
            }
            return count;
        }
        public int CheckExistQueueNo(int QueueNo, int SLAFLocationUId,int VehicleCategoryUId)
        {
            int count = 0;
            try
            {
                string sql = "SELECT * FROM VehicleInfo where QueueNo='" + QueueNo + "' AND Active='TRUE' AND SLAFLocationUId='" + SLAFLocationUId + "' AND VehicleCategoryUId='" + VehicleCategoryUId + "'";
                DataTable dt = SelectData(sql);
                count = dt.Rows.Count;
            }
            catch (Exception)
            {

                throw;
            }
            return count;
        }

        //Check last queue number in the station for a particular vehicle category
        public int CheckLastQueueNo(int SLAFLocationUId, int VehicleCategoryUId)
        {
            int queueno = 0;

            try
            {
                string sql = "SELECT max(QueueNo) as LastQueueNo FROM VehicleInfo where Active='TRUE' AND SLAFLocationUId='" + SLAFLocationUId + "' AND VehicleCategoryUId='" + VehicleCategoryUId + "'";
                DataTable dt = SelectData(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    queueno = int.Parse(dr["LastQueueNo"].ToString());
                }
                return queueno;
               
            }
            catch (Exception)
            {

                throw;
            }
            return queueno;
        }


        public string GetMeasurementUnit(int IngriedientUId)
        {
            string measurementUnit = "";

            try
            {
                //string sql = "SELECT MeasurementUnitUId FROM IngredientInfo where UId='" + IngriedientUId + "'";
                string sql = "SELECT MeasurementUnit.Unit as Unit FROM IngredientInfo INNER JOIN MeasurementUnit ON IngredientInfo.MeasurementUnitUId = MeasurementUnit.UId where IngredientInfo.UId='" + IngriedientUId + "'";
                DataTable dt = SelectData(sql);
                foreach (DataRow dr in dt.Rows)
                {
                    //measurementUId = int.Parse(dr["MeasurementUnitUId"].ToString());
                    measurementUnit = dr["Unit"].ToString();
                }
                return measurementUnit;

            }
            catch (Exception)
            {

                throw;
            }
            return measurementUnit;
        }

        public UserAccount SelectUser(string Username)
        {
            UserAccount oUserAccount = new UserAccount();
            try
            {
                string sql = "select * from [User] WHERE UserName='" + Username + "' and Active='TRUE'";
                DataTable dt = SelectData(sql);
                foreach (DataRow row in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(row["UserTypeId"].ToString()))
                    {
                        oUserAccount.UserTypeId = int.Parse(row["UserTypeId"].ToString());
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return oUserAccount;
        }
        public PageObject SelectPageObject(string action, string controller)
        {
            PageObject oPageObject = new PageObject();
            try
            {
                string sql = "select * from [PageObject] WHERE ObjectName='" + action + "' and  ControllerName='" + controller + "' and Active='TRUE'";
                DataTable dt = SelectData(sql);
                foreach (DataRow row in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(row["UId"].ToString()))
                    {
                        oPageObject.UId = int.Parse(row["UId"].ToString());
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return oPageObject;
        }
        public UserPermission CheckUserPermission(int UserTypeId, int ObjectId)
        {
            UserPermission oUserPermission = new UserPermission();
            try
            {
                string sql = "select * from UserPermission WHERE UserTypeUId='" + UserTypeId + "' and ObjectId='" + ObjectId + "' and Active='TRUE'";
                DataTable dt = SelectData(sql);
                foreach (DataRow row in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(row["UserTypeUId"].ToString()))
                    {
                        oUserPermission.UserTypeUId = int.Parse(row["UserTypeUId"].ToString());
                    }
                    if (!string.IsNullOrEmpty(row["ObjectId"].ToString()))
                    {
                        oUserPermission.ObjectId = int.Parse(row["ObjectId"].ToString());
                    }
                    if (!string.IsNullOrEmpty(row["IsPermitted"].ToString()))
                    {
                        oUserPermission.IsPermitted = bool.Parse(row["IsPermitted"].ToString());
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return oUserPermission;
        }
        private  string GetSqlText(int uid)
        {
            string sql = null;

            sql = "SELECT * FROM Mark WHERE LotUid IN " +
                  "(SELECT Uid FROM ProjectHierarchy WHERE ProjectHierarchyLevelUid=" +
                  "(SELECT Uid FROM ProjectHierarchyLevel  WHERE ProjectUid='"+uid+"' AND LevelOrder='3')) ";
            return sql;
        }
        public  int SelectStrengthCount(int ServiceProfileUId)
        {
            string sql = null;

            sql = "SELECT Count(*) AS CountVal FROM ServiceProfile WHERE ForceTypeUId='" + ServiceProfileUId + "' AND Active='TRUE' ";
            DataTable dt = SelectData(sql);
            int count = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (!string.IsNullOrEmpty(dr["CountVal"].ToString()))
                { 
                    count=int.Parse(dr["CountVal"].ToString());
                }
            }
            return count;
        }
        
        public int SelectLoanCount(int ForceTypeId,int Type )
        {
            string sql = null;

            sql = "SELECT LoanType,ForceTypeId,Count(*)  AS CountVal FROM [ServiceLoan] where ForceTypeId='" + ForceTypeId + "' and ";
            if(Type==1)
               sql +=" LoanType='EDUCATION' ";
            else if (Type == 2)
                sql += " LoanType='HOUSING-gammana' ";
            else if (Type == 3)
                sql += " LoanType='HOUSING-sansada' ";
            else if (Type == 4)
                sql += " LoanType='Inservice - Airforce' ";
            else if (Type == 5)
                sql += " LoanType='Inservice - Army' ";
            else if (Type == 6)
                sql += " LoanType='INSERVICE - NAVY' ";
            else if (Type ==7)
                sql += " LoanType='INSERVICE - POLICE' ";
            else if (Type == 8)
                sql += " LoanType='MEDICAL' ";
            else if (Type == 9)
                sql += " LoanType='SELF EMPLOYEEMENT' ";
            else if (Type == 10)
                sql += " LoanType='THREEWHEEL' ";
            sql+="  GROUP BY LoanType,ForceTypeId Order by LoanType ASC";
            DataTable dt = SelectData(sql);
            int count = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (!string.IsNullOrEmpty(dr["CountVal"].ToString()))
                {
                    count = int.Parse(dr["CountVal"].ToString());
                }
            }
            return count;
        }
        public UserProfileSummry SelectUserProfile(int UserId)
        {
            string sql = null;
            UserProfileSummry profile = new UserProfileSummry();
            sql = "select A.Name,E.Name AS EName,E.Img from [User] as U,Employee as E,Appointment as A " +
                    " where E.UserId=U.Id and E.AppointmentId=A.UId and U.Id='" + UserId + "'";
            DataTable dt = SelectData(sql);
            
            foreach (DataRow dr in dt.Rows)
            {

                if (!string.IsNullOrEmpty(dr["Name"].ToString()))
                {
                    profile.Appointment = dr["Name"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["EName"].ToString()))
                {
                    profile.Name = dr["EName"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["Img"].ToString()))
                {
                    profile.ImagData = (byte[])(dr["Img"]);
                }
            }
            return profile;
        }
        public List<CustomServiceProfile> SelectServiceProfileList(int ForceTypeId)
        {
            string sql = null;
            List<CustomServiceProfile> profileList = new List<CustomServiceProfile>();
            sql = "Select top 10000 S.UId,S.RefUId,S.ServiceNo,R.RankName,S.ServiceName,S.ServiceAddress,ST.Type from ServiceProfile " +
              "  AS S,[Rank] AS R,ServiceStatus AS ST where S.RankUId=R.UId and S.ServiceStatus=ST.RefUId "+
              "   and ForceTypeId='" + ForceTypeId + "' AND S.Active='TRUE'";
            DataTable dt = SelectData(sql);
            
            foreach (DataRow dr in dt.Rows)
            {
                CustomServiceProfile profile = new CustomServiceProfile();
                if (!string.IsNullOrEmpty(dr["UId"].ToString()))
                {
                    profile.UId = int.Parse(dr["UId"].ToString());
                }
                if (!string.IsNullOrEmpty(dr["RefUId"].ToString()))
                {
                    profile.RefUId = int.Parse(dr["RefUId"].ToString());
                }
                if (!string.IsNullOrEmpty(dr["ServiceNo"].ToString()))
                {
                    profile.ServiceNo = dr["ServiceNo"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["RankName"].ToString()))
                {
                    profile.RankName = dr["RankName"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["ServiceName"].ToString()))
                {
                    profile.ServiceName = dr["ServiceName"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["ServiceAddress"].ToString()))
                {
                    profile.ServiceAddress = dr["ServiceAddress"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["Type"].ToString()))
                {
                    profile.Type = dr["Type"].ToString();
                }
                profileList.Add(profile);
            }
            return profileList;
        }
        public CustomServiceProfile SelectServiceProfileInfo(string SerNo)
        {
            string sql = null;
            CustomServiceProfile profile = new CustomServiceProfile();
            sql = "Select top 1 * from ServiceProfile where ServiceNo='" + SerNo + "' and Active='TRUE'";
            DataTable dt = SelectData(sql);

            foreach (DataRow dr in dt.Rows)
            {
                
                if (!string.IsNullOrEmpty(dr["UId"].ToString()))
                {
                    profile.UId = int.Parse(dr["UId"].ToString());
                }
                if (!string.IsNullOrEmpty(dr["RefUId"].ToString()))
                {
                    profile.RefUId = int.Parse(dr["RefUId"].ToString());
                }
                if (!string.IsNullOrEmpty(dr["ServiceNo"].ToString()))
                {
                    profile.ServiceNo = dr["ServiceNo"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["RankUId"].ToString()))
                {
                    profile.RankUId = int.Parse(dr["RankUId"].ToString());
                }
                if (!string.IsNullOrEmpty(dr["DistrictUId"].ToString()))
                {
                    profile.DistrictUId = int.Parse(dr["DistrictUId"].ToString());
                }
                if (!string.IsNullOrEmpty(dr["ForceTypeUId"].ToString()))
                {
                    profile.ForceTypeUId = int.Parse(dr["ForceTypeUId"].ToString());
                }
                if (!string.IsNullOrEmpty(dr["ServiceName"].ToString()))
                {
                    profile.ServiceName = dr["ServiceName"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["ServiceAddress"].ToString()))
                {
                    profile.ServiceAddress = dr["ServiceAddress"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["TeleNo"].ToString()))
                {
                    profile.TeleNo = dr["TeleNo"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["NIC"].ToString()))
                {
                    profile.NIC = dr["NIC"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["ServicePercent"].ToString()))
                {
                    profile.ServicePercent = dr["ServicePercent"].ToString();
                }
            }
            return profile;
        }
        public int SelectLoanStrengthCount(int ServiceProfileUId)
        {
            string sql = null;

            sql = "SELECT Count(*) AS CountVal FROM ServiceLoan WHERE ForceTypeId='" + ServiceProfileUId + "' ";
            DataTable dt = SelectData(sql);
            int count = 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (!string.IsNullOrEmpty(dr["CountVal"].ToString()))
                {
                    count = int.Parse(dr["CountVal"].ToString());
                }
            }
            return count;
        }
        public int SelectStrengthCountServiceStatus(int ServiceProfileUId,int ServiceType)
        {
            string sql = null;

            sql = "SELECT Count(*) AS CountVal FROM ServiceProfile WHERE ForceTypeUId='" + ServiceProfileUId + "' AND ServiceStatus='" + ServiceType + "' AND Active='TRUE' ";
            DataTable dt = SelectData(sql);
            int  count= 0;
            foreach (DataRow dr in dt.Rows)
            {
                if (!string.IsNullOrEmpty(dr["CountVal"].ToString()))
                { 
                    count=int.Parse(dr["CountVal"].ToString());
                }
            }
            
            return count;
        }
      

        public List<int> SelectLotIdList(int projectId)
        {
            List<int> lotList = new List<int>();
            try
            {
                string sql = "SELECT Uid FROM dbo.ProjectHierarchy WHERE  ProjectHierarchyLevelUid=(SELECT Uid FROM dbo.ProjectHierarchyLevel WHERE LevelOrder='3' AND ProjectUid='"+projectId+"' AND ExpiryDate IS  NULL)";
                DataTable dt = SelectData(sql);
                foreach (DataRow row in dt.Rows)
                {
                    lotList.Add(int.Parse(row["Uid"].ToString()));
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return lotList;
        }
        public DataTable SelectDataTable()
        {
            DataTable dt = new DataTable();
            try
            {
                string sql = "Select Top 10 * from ServiceProfile";
                dt = SelectData(sql);
                
            }
            catch (Exception)
            {

                throw;
            }
            return dt;
        }
        public List<PageObject> SelectPageObject(int UId)
        {
            List<PageObject> PageObjectList = new List<PageObject>();
            try
            {
                string sql = "select * from PageObject WHERE ParentUId='" + UId + "' and Active='TRUE'";
                DataTable dt = SelectData(sql);
                foreach (DataRow row in dt.Rows)
                {
                    PageObject obj = new PageObject();
                    if (!string.IsNullOrEmpty(row["UId"].ToString()))
                    {
                        obj.UId = int.Parse(row["UId"].ToString());
                    }
                    if (!string.IsNullOrEmpty(row["ObjectName"].ToString()))
                    {
                        obj.ObjectName = row["ObjectName"].ToString();
                    }
                    if (!string.IsNullOrEmpty(row["Description"].ToString()))
                    {
                        obj.Description = row["Description"].ToString();
                    }
                    if (!string.IsNullOrEmpty(row["ControllerName"].ToString()))
                    {
                        obj.ControllerName = row["ControllerName"].ToString();
                    }
                    if (!string.IsNullOrEmpty(row["ParentUId"].ToString()))
                    {
                        obj.ParentUId = int.Parse(row["ParentUId"].ToString());
                    }
                    PageObjectList.Add(obj);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return PageObjectList;
        }
        public UserPermission SelectUserPermission(int UserTypeId, int ObjectId)
        {
            UserPermission oUserPermission = new UserPermission();
            try
            {
                string sql = "select * from UserPermission WHERE UserTypeUId='" + UserTypeId + "' and ObjectId='" + ObjectId + "' and Active='TRUE'";
                DataTable dt = SelectData(sql);
                foreach (DataRow row in dt.Rows)
                {
                    if (!string.IsNullOrEmpty(row["UId"].ToString()))
                    {
                        oUserPermission.UId = int.Parse(row["UId"].ToString());
                    }
                    if (!string.IsNullOrEmpty(row["UserTypeUId"].ToString()))
                    {
                        oUserPermission.UserTypeUId = int.Parse(row["UserTypeUId"].ToString());
                    }
                    if (!string.IsNullOrEmpty(row["ObjectId"].ToString()))
                    {
                        oUserPermission.ObjectId = int.Parse(row["ObjectId"].ToString());
                    }
                    if (!string.IsNullOrEmpty(row["IsPermitted"].ToString()))
                    {
                        oUserPermission.IsPermitted = bool.Parse(row["IsPermitted"].ToString());
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return oUserPermission;
        }
    }
}