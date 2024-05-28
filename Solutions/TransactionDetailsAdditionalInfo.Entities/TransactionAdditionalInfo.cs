using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransactionDetailsAdditionalInfo.Entities
{
    public class TransactionAdditionalInfo
    {
        public string accrual_channel { get; set; } = string.Empty;
        public string additional_details_1 { get; set; } = string.Empty;
        public string additional_details_2 { get; set; } = string.Empty;
        public int amount { get; set; } = 0;
        public string bin_number { get; set; } = string.Empty;
        public string execution_date { get; set; } = string.Empty;
        public string member_relation_reference { get; set; } = string.Empty;
        public string merchant_category_code { get; set; } = string.Empty;
        public string merchant_city { get; set; } = string.Empty;
        public string merchant_country { get; set; } = string.Empty;
        public string merchant_id { get; set; } = string.Empty;
        public string merchant_name { get; set; } = string.Empty;
        public string national_id { get; set; } = string.Empty;
        public string product_code { get; set; } = string.Empty;
        public int source_amount { get; set; } = 0;
        public string source_currency { get; set; } = string.Empty;
        public string sub_product_code { get; set; } = string.Empty;
        public string terminal_id { get; set; } = string.Empty;
        public string transaction_codes { get; set; } = string.Empty;
        public string transaction_date { get; set; } = string.Empty;
        public string transaction_id { get; set; } = string.Empty;
        public string transaction_time { get; set; } = string.Empty;
        public string transaction_type { get; set; } = string.Empty;
    }
}
