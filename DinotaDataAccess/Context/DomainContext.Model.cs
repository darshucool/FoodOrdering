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
        }
    }
}
