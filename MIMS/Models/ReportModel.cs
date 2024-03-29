﻿

using Dinota.Domain.IngredientInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AlfasiWeb.Models
{
    public class ReportModel
    {
        public DateTime date { get; set; }
        public int SLAFLocationUId { get; set; }
    }
    public class FuelDrawModel {
        public int SLAFLocationUId { get; set; }
        public string ServiceNo { get; set; }
        public string VehicleNo { get; set; }
        public string NICNo { get; set; }
        public string Contact { get; set; }
        public bool IsUpdate { get; set; }
        
        public bool IsEnable { get; set; }
        public string dateInfo { get; set; }
    }

    public class EventReportSummary {
        public int Incount { get; set; }
        public int Outcount { get; set; }
        public int Pendingcount { get; set; }
        public List<EntrySummary> EntrySummaryList { get; set; }
    }

    public class EntrySummary {
        public string Time { get; set; }
        public int Incount { get; set; }
        public int Outcount { get; set; }
    }
    public class DailySaleReportModel {

        public bool IsSet { get; set; }
        public DateTime EffectiveDate { get; set; }
        public decimal DutyBreakfastCount { get; set; }
        public decimal DutyBreakfastReceivableAmt { get; set; }
        public decimal DutyBreakfastExpenditureAmt { get; set; }

        public decimal DutyLunchCount { get; set; }
        public decimal DutyLunchReceivableAmt { get; set; }
        public decimal DutyLunchExpenditureAmt { get; set; }

        public decimal DutyDinnerCount { get; set; }
        public decimal DutyDinnerReceivableAmt { get; set; }
        public decimal DutyDinnerExpenditureAmt { get; set; }


        public decimal CasualBreakfastCount { get; set; }
        public decimal CasualBreakfastCredit { get; set; }
        public decimal CasualBreakfastCash { get; set; }
        public decimal CasualBreakfastExpenditure { get; set; }

        public decimal CasualLunchCount { get; set; }
        public decimal CasualLunchCredit { get; set; }
        public decimal CasualLunchCash { get; set; }
        public decimal CasualLunchExpenditure { get; set; }

        public decimal CasualDinnerCount { get; set; }
        public decimal CasualDinnerCredit { get; set; }
        public decimal CasualDinnerCash { get; set; }
        public decimal CasualDinnerExpenditure { get; set; }


        public decimal ExtraMessingCreditAmount { get; set; }
        public decimal ExtraMessingCashAmount { get; set; }
        public decimal ExtraMessingExpenditureAmount { get; set; }

        public decimal BreakfastGasCharges { get; set; }
        public decimal LunchGasCharges { get; set; }
        public decimal DinnerGasCharges { get; set; }
        public decimal ExtraMessingGasCharges { get; set; }

        public decimal Commodities { get; set; }

        public decimal TotalIncomeBreakfast { get; set; }
        public decimal TotalIncomeLunch { get; set; }
        public decimal TotalIncomeDinner { get; set; }
        public decimal TotalIncomeExtramessing { get; set; }

        public decimal TotalExpenditureBreakfast { get; set; }
        public decimal TotalExpenditureLunch { get; set; }
        public decimal TotalExpenditureDinner { get; set; }
        public decimal TotalExpenditureExtramessing { get; set; }

        public decimal TotalDutyReceivableAmount { get; set; }
        public decimal TotalDutyExpenditureAmount { get; set; }

        public decimal TotalExtraMessingCredit { get; set; }
        public decimal TotalExtraMessingCash { get; set; }
        public decimal TotalExtraMessingExpenditure { get; set; }

        public decimal TotalGasAmount { get; set; }
        public decimal TotalCommodotiesAmount { get; set; }
        public decimal TotalIncomeAmount { get; set; }
        public decimal TotalExpenditureAmount { get; set; }

        public decimal BreakfastCommodities { get; set; }
        public decimal LunchCommodities { get; set; }
        public decimal DinnerCommodities { get; set; }
        public decimal TotalCommodities { get; set; }
    }
    public class StockSheetTransactionModel {
        public List<IngredientInfo> IngredientInfoList { get; set; }
        public DateTime Date { get; set; }
        
        public decimal IssueAmount{ get; set; }
        public List<BOCAmountList> BOCAmountList { get; set; }
        public List<BOCQtyList> BOCQtyList { get; set; }
        public List<IngIssueList> IngIssueList { get; set; }
       
    }
    public class BOCTotalModel {
        public IngredientInfo IngredientInfo { get; set; }
        public decimal RemQty { get; set; }
        public decimal BOCValue { get; set; }
    }
    public class BOCAmountList {
        public decimal BocAmount { get; set; }
    }
    public class BOCQtyList
    {
        public decimal Qty { get; set; }
    }
    public class IngIssueList
    {
        public decimal Qty { get; set; }
    }
    
}