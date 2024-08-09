using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class TypeInfo
    {
        [DataMember]
        public string uuid { get; set; }
        [DataMember]
        public string title { get; set; }
        [DataMember]
        public string titleTranslated { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string descriptionTranslated { get; set; }
        [DataMember]
        public int durationDays { get; set; }
        [DataMember]
        public int durationHours { get; set; }
        [DataMember]
        public int durationMinutes { get; set; }
        [DataMember]
        public int daysInAdvance { get; set; }
        [DataMember]
        public string cutOffTime { get; set; }
        [DataMember]
        public string firstAvailabilityDate { get; set; }
        [DataMember]
        public bool? isNonRefundable { get; set; }
        [DataMember]
        public bool? allowAdults { get; set; }
        [DataMember]
        public int minPax { get; set; } = 0;
        [DataMember]
        public int maxPax { get; set; } = 0;
        [DataMember]
        public int minAdultAge { get; set; } = 0;
        [DataMember]
        public int maxAdultAge { get; set; } = 0;
        [DataMember]
        public bool? hasChildPrice { get; set; }
        [DataMember]
        public bool? allowChildren { get; set; }
        [DataMember]
        public int minChildren { get; set; } = 0;
        [DataMember]
        public int maxChildren { get; set; } = 0;
        [DataMember]
        public int minChildAge { get; set; }
        [DataMember]
        public int maxChildAge { get; set; }
        [DataMember]
        public bool? allowSeniors { get; set; }
        [DataMember]
        public int minSeniors { get; set; } = 0;
        [DataMember]
        public int maxSeniors { get; set; } = 0;
        [DataMember]
        public int minSeniorAge { get; set; }
        [DataMember]
        public int maxSeniorAge { get; set; }
        [DataMember]
        public bool? allowInfant { get; set; }
        [DataMember]
        public int minInfantAge { get; set; }
        [DataMember]
        public int maxInfantAge { get; set; }
        [DataMember]
        public int maxGroup { get; set; }
        [DataMember]
        public int minGroup { get; set; }
        [DataMember]
        public bool? instantConfirmation { get; set; }
        [DataMember]
        public bool? nonInstantVoucher { get; set; }
        [DataMember]
        public bool? directAdmission { get; set; }
        [DataMember]
        public string voucherUse { get; set; }
        [DataMember]
        public string voucherUseTranslated { get; set; }
        [DataMember]
        public string voucherRedemptionAddress { get; set; }
        [DataMember]
        public string voucherRedemptionAddressTranslated { get; set; }
        [DataMember]
        public bool? voucherRequiresPrinting { get; set; }
        [DataMember]
        public string meetingTime { get; set; }
        [DataMember]
        public string meetingAddress { get; set; }
        [DataMember]
        public string meetingLocation { get; set; }
        [DataMember]
        public string cancellationPolicySummary { get; set; }
        [DataMember]
        public decimal? recommendedMarkup { get; set; }
        [DataMember]
        public decimal? childRecommendedMarkup { get; set; }
        [DataMember]
        public decimal? seniorRecommendedMarkup { get; set; }
        [DataMember]
        public decimal? adultParityPrice { get; set; }
        [DataMember]
        public decimal? childParityPrice { get; set; }
        [DataMember]
        public decimal? seniorParityPrice { get; set; }
        [DataMember]
        public decimal? adultGateRatePrice { get; set; }
        [DataMember]
        public decimal? childGateRatePrice { get; set; }
        [DataMember]
        public decimal? seniorGateRatePrice { get; set; }
        [DataMember]
        public List<TicketType> ticketTypes { get; set; }
        [DataMember]
        public Validity validity { get; set; }
        [DataMember]
        public List<TimeSlots> timeslots { get; set; }
        [DataMember]
        public Options options { get; set; }
        [DataMember]
        public bool hasOptions { get; set; } = false;
        [DataMember]
        public bool? hasFileUploadOptions { get; set; }
        [DataMember]
        public bool? hasPriceOptions { get; set; }
        [DataMember]
        public bool? hasRequiredPriceOptions { get; set; }
        [DataMember]
        public bool? isBmgVoucher { get; set; }
        [DataMember]
        public bool? isSrvEligible { get; set; }
    }
}