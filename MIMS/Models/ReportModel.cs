

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
    public class DailySaleReportModel {

        public bool IsSet { get; set; }
        public DateTime EffectiveDate { get; set; }
        public int DutyBreakfastCount { get; set; }
        public decimal DutyBreakfastReceivableAmt { get; set; }
        public decimal DutyBreakfastExpenditureAmt { get; set; }

        public int DutyLunchCount { get; set; }
        public decimal DutyLunchReceivableAmt { get; set; }
        public decimal DutyLunchExpenditureAmt { get; set; }

        public int DutyDinnerCount { get; set; }
        public decimal DutyDinnerReceivableAmt { get; set; }
        public decimal DutyDinnerExpenditureAmt { get; set; }


        public int CasualBreakfastCount { get; set; }
        public decimal CasualBreakfastCredit { get; set; }
        public decimal CasualBreakfastCash { get; set; }
        public decimal CasualBreakfastExpenditure { get; set; }

        public int CasualLunchCount { get; set; }
        public decimal CasualLunchCredit { get; set; }
        public decimal CasualLunchCash { get; set; }
        public decimal CasualLunchExpenditure { get; set; }

        public int CasualDinnerCount { get; set; }
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
    }
    
}