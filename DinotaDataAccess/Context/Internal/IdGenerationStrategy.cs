using System;
using System.Data;
using System.Data.SqlClient;
using Dinota.Domain;

namespace Dinota.DataAccces.Context.Internal
{
    class IdGenerationStrategy
    {
        public DomainContext DomainContext { get; set; }

        public IdGenerationStrategy(DomainContext domainContext)
        {
            DomainContext = domainContext;
        }

        public string GenerateID(int idType, params object[] args)
        {
            if (args.Length == 0)
            {
                return GetNextId(GetTableName(idType), 0);
            }
            else
            {
                System.Diagnostics.Debug.Assert(args.Length == 1);

                return GetNextId(GetTableName(idType), (short)args[0]);
            }

        }

        private static string GetTableName(int idType)
        {
            var idTypeEnum = (IdTypes)idType;

            switch (idTypeEnum)
            {
                case IdTypes.RouteWeek:
                    return "Week";
                case IdTypes.RouteDay:
                    return "Day";
                case IdTypes.PromotionCode:
                    return "OrderPromoCode";
                case IdTypes.KeyAccountGroup:
                    return "KeyAccountGroup";
                case IdTypes.Brand:
                    return "Brand";
                case IdTypes.Designation:
                    return "Designation";
                case IdTypes.BusinessType:
                    return "BusinessType";
                case IdTypes.OperatorClass:
                    return "OutletClass";
                case IdTypes.Channel:
                    return "OutletChannel";
                case IdTypes.SubChannel:
                    return "SubChannel";
                case IdTypes.OrderType:
                    return "OrderType";
                case IdTypes.ConvenienceLevel:
                    return "ConvenienceLevel";
                case IdTypes.Agent:
                    return "Agent";
                case IdTypes.Competitor:
                    return "Competitor";
                case IdTypes.AreaSalesManager:
                    return "Agent";
                case IdTypes.CompetitorItem:
                    return "CompetitorItem";
                case IdTypes.KosherType:
                    return "ReligiousType";
                case IdTypes.Geo0:
                    return "Geo0";
                case IdTypes.Geo1:
                    return "Geo1";
                case IdTypes.Geo2:
                    return "Geo2";
                case IdTypes.Geo3:
                    return "Geo3";
                case IdTypes.Geo4:
                    return "Geo4";
                case IdTypes.Geo5:
                    return "Geo5";
                case IdTypes.SamplingResponse:
                    return "SamplingFeedback";
                case IdTypes.ReturnReason:
                    return "ReturnReason";
                case IdTypes.Outlet:
                    return "Outlet";
                case IdTypes.ObjectiveResponseGroups:
                    return "ObjRespGroup";
                case IdTypes.ObjectiveResponse:
                    return "ObjResponse";
                case IdTypes.Objective:
                    return "Objective";
                case IdTypes.CompetIntelCriterion:
                    return "CompetIntelCriterion";
                case IdTypes.Category:
                    return "Category";
                case IdTypes.Item:
                    return "Item";
                case IdTypes.Content:
                    return "Content";
                case IdTypes.OutletContact:
                    return "OutletContact";
                case IdTypes.Survey:
                    return "Survey";
                case IdTypes.SurveyQuestion:
                    return "SurveyQuestion";
                case IdTypes.SurveyAnswerChoices:
                    return "SurveyAnswerChoices";
                case IdTypes.OutletTag:
                    return "OutletTag";
                case IdTypes.ObjectiveSubTasks:
                    return "ObjectiveSubTasks";
                case IdTypes.RecipeDish:
                    return "RecipeDish";
                case IdTypes.RecipeCostingSheets:
                    return "RecipeCostingSheets";
                case IdTypes.TouchPointType:
                    return "TouchPointType";
                case IdTypes.TopDish:
                    return "TopDish";
                case IdTypes.SalesCycle:
                    return "SalesCycle";
                case IdTypes.TargetAudience:
                    return "TargetAudience";
                case IdTypes.PantryLostReason:
                    return "PantryLostReason";
                case IdTypes.PantryRetentionReason:
                    return "PantryRetentionReason";
                case IdTypes.JbpUpliftIssueType:
                    return "JbpUpliftIssueType";
                case IdTypes.RAPEquipment:
                    return "RAPEquipment";
                case IdTypes.DiscountTier:
                    return "DiscountTier";
                case IdTypes.RAPOperationVolume:
                    return "RAPOperationVolume";
                case IdTypes.ReturnType:
                    return "ReturnType";
                case IdTypes.ConsolOrderType:
                    return "ConsolOrderType";
                case IdTypes.CompetitorItemUsageReason:
                    return "CompetitorItemUsageReason";
                case IdTypes.StaffSkill:
                    return "StaffSkill";
                case IdTypes.ChefTopology:
                    return "ChefTopology";
                case IdTypes.Promotion:
                    return "Promotions";
                case IdTypes.DisplayType:
                    return "DisplayType";
				case IdTypes.Chef:
                    return "OutletChef";
                case IdTypes.DisplayCategory:
                    return "DisplayCategory";
                case IdTypes.CompetitorCampaignType:
                    return "CompetitorCampaignType";
                default:
                    throw new ArgumentException("Table is not mapped for the supplied ID Type");
            }
        }

        private string GetNextId(string tableName, short type)
        {
            var command = DomainContext.Database.Connection.CreateCommand();
            command.CommandText = "IDAutoGen";
            command.CommandType = CommandType.StoredProcedure;

            var tableParameter = command.CreateParameter();
            var typeParameter = command.CreateParameter();
            tableParameter.ParameterName = "Table";
            tableParameter.Value = tableName;
            command.Parameters.Add(tableParameter);

            if (type != 0)
            {
                typeParameter.ParameterName = "type";
                typeParameter.Value = type;
                command.Parameters.Add(typeParameter);
            }

            var maxParameter = command.CreateParameter();
            maxParameter.ParameterName = "MaxId";
            maxParameter.DbType = DbType.Int64;

            maxParameter.Direction = ParameterDirection.Output;
            command.Parameters.Add(maxParameter);

            var connectionOpened = false;
            if (DomainContext.Database.Connection.State == ConnectionState.Closed)
            {
                DomainContext.Database.Connection.Open();
                connectionOpened = true;
            }

            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException sqlException)
            {
                if (sqlException.Message.Contains("Arithmetic overflow error converting expression to data type"))
                {
                    throw new DataException("Unable to generate an ID automatically. Please enter an ID manually.",
                                            sqlException);
                }
                throw;
            }
            finally
            {
                if (connectionOpened)
                {
                    DomainContext.Database.Connection.Close();
                }
            }

            return maxParameter.Value.ToString();
        }
    }
}
