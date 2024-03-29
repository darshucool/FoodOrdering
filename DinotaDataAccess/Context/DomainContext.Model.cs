using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Dinota.DataAccces.Tracking;
using Dinota.DataAccces.User;
using Dinota.DataAccces.District;
using Dinota.DataAccces.Division;
using Dinota.DataAccces.UserType;
using Dinota.DataAccces.PageObject;
using Dinota.DataAccces.UserPermission;
using Dinota.DataAccces.SLAFLocation;
using Dinota.DataAccces.FuelType;
using Dinota.DataAccces.Rank;
using Dinota.DataAccces.Town;
using Dinota.DataAccces.MenuCategory;
using Dinota.DataAccces.MenuItem;
using Dinota.DataAccces.MenuOrder;
using Dinota.DataAccces.Event;
using Dinota.DataAccces.EventParticipation;
using Dinota.DataAccces.EventParticipationKid;
using Dinota.DataAccces.EventAttendance;
using Dinota.DataAccces.RoomNo;
using Dinota.DataAccces.RoomInfo;
using Dinota.DataAccces.News;
using Dinota.DataAccces.MenuFavorite;
using Dinota.DataAccces.MeasurementUnit;
using Dinota.DataAccces.MenuOption;
using Dinota.DataAccces.MenuMultiOption;
using Dinota.DataAccces.IngredientInfo;
using Dinota.DataAccces.IngredientBOC;
using Dinota.DataAccces.MenuPackage;
using Dinota.DataAccces.MenuItemDetail;
using Dinota.DataAccces.F140Data;
using Dinota.DataAccces.F140Header;
using Dinota.DataAccces.BOCTransaction;
using Dinota.DataAccces.MenuOrderHeader;
using Dinota.DataAccces.MenuOrderItemDetail;
using Dinota.DataAccces.MenuOrderOfficer;
using Dinota.DataAccces.UserStatus;
using Dinota.DataAccces.OfficerRequest;
using Dinota.DataAccces.StockSheetTransaction;
using Dinota.DataAccces.PaymentMethod;
using Dinota.DataAccces.MenuOrderExtraItemDetail;
using Dinota.DataAccces.AlertNotify;
using Dinota.DataAccces.SetMenuHeader;
using Dinota.DataAccces.SetMenuDetail;
using Dinota.DataAccces.PaymentInfo;
using Dinota.DataAccces.EventCount;
using Dinota.DataAccces.BarRecovery;
using Dinota.DataAccces.MainCourseMealHeader;
using Dinota.DataAccces.Supplier;
using Dinota.DataAccces.SupplierInvoice;
using Dinota.DataAccces.SubscriptionFee;
using Dinota.DataAccces.UserPermissionGroup;
using Dinota.DataAccces.UserArea;
using Dinota.DataAccces.UserTrn;

namespace Dinota.DataAccces.Context
{
    public partial class DomainContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new UserMap());           
            modelBuilder.Configurations.Add(new UserAccountMap());         
            modelBuilder.Configurations.Add(new TrackingMap());
            modelBuilder.Configurations.Add(new DistrictMap());
            modelBuilder.Configurations.Add(new DivisionMap());
            modelBuilder.Configurations.Add(new UserTypeMap());
            modelBuilder.Configurations.Add(new PageObjectMap());
            modelBuilder.Configurations.Add(new UserPermissionMap());
            modelBuilder.Configurations.Add(new SLAFLocationMap());
            modelBuilder.Configurations.Add(new FuelTypeMap());
            modelBuilder.Configurations.Add(new RankMap());
            modelBuilder.Configurations.Add(new TownMap());
            modelBuilder.Configurations.Add(new MenuCategoryMap());
            modelBuilder.Configurations.Add(new MenuItemMap());  
            modelBuilder.Configurations.Add(new MenuOrderMap());
            modelBuilder.Configurations.Add(new EventMap());
            modelBuilder.Configurations.Add(new EventParticipationMap());
            modelBuilder.Configurations.Add(new EventParticipationKidMap());
            modelBuilder.Configurations.Add(new EventAttendanceMap());
            modelBuilder.Configurations.Add(new RoomNoMap());
            modelBuilder.Configurations.Add(new RoomInfoMap());
            modelBuilder.Configurations.Add(new NewsMap());
            modelBuilder.Configurations.Add(new MenuFavoriteMap());
            modelBuilder.Configurations.Add(new MeasurementUnitMap());
            modelBuilder.Configurations.Add(new MenuOptionMap());
            modelBuilder.Configurations.Add(new MenuMultiOptionMap());
            modelBuilder.Configurations.Add(new IngredientInfoMap());
            modelBuilder.Configurations.Add(new IngredientBOCMap()); 
            modelBuilder.Configurations.Add(new MenuPackageMap());
            modelBuilder.Configurations.Add(new MenuItemDetailMap());
            modelBuilder.Configurations.Add(new F140DataMap());
            modelBuilder.Configurations.Add(new F140HeaderMap()); 
            modelBuilder.Configurations.Add(new BOCTransactionMap());   
            modelBuilder.Configurations.Add(new MenuOrderHeaderMap()); 
            modelBuilder.Configurations.Add(new MenuOrderItemDetailMap()); 
            modelBuilder.Configurations.Add(new MenuOrderOfficerMap()); 
            modelBuilder.Configurations.Add(new UserStatusMap());
            modelBuilder.Configurations.Add(new OfficerRequestMap());
            modelBuilder.Configurations.Add(new StockSheetTransactionMap());
            modelBuilder.Configurations.Add(new PaymentMethodMap());
            modelBuilder.Configurations.Add(new MenuOrderExtraItemDetailMap());   
            modelBuilder.Configurations.Add(new AlertNotifyMap());
            modelBuilder.Configurations.Add(new SetMenuDetailMap());
            modelBuilder.Configurations.Add(new SetMenuHeaderMap());
            modelBuilder.Configurations.Add(new PaymentInfoMap());
            modelBuilder.Configurations.Add(new EventCountMap());
            modelBuilder.Configurations.Add(new BarRecoveryMap());
            modelBuilder.Configurations.Add(new MainCourseMealHeaderMap());
            modelBuilder.Configurations.Add(new SupplierMap());
            modelBuilder.Configurations.Add(new SupplierInvoiceMap());
            modelBuilder.Configurations.Add(new SubscriptionFeeMap());
  modelBuilder.Configurations.Add(new UserPermissionGroupMap());
            modelBuilder.Configurations.Add(new UserAreaMap()); 
            modelBuilder.Configurations.Add(new UserTrnMap());
        }
    }
}
