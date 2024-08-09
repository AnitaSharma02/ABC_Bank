using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace BeMyGuest.Entities
{
    [Serializable]
    [DataContract]
    public class ProductInfo
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
        public string highlights { get; set; }
        [DataMember]
        public string highlightsTranslated { get; set; }
        [DataMember]
        public string additionalInfo { get; set; }
        [DataMember]
        public string additionalInfoTranslated { get; set; }
        [DataMember]
        public string covid19Measures { get; set; }
        [DataMember]
        public string covid19MeasuresTranslated { get; set; }
        [DataMember]
        public string priceIncludes { get; set; }
        [DataMember]
        public string priceIncludesTranslated { get; set; }
        [DataMember]
        public string priceExcludes { get; set; }
        [DataMember]
        public string priceExcludesTranslated { get; set; }
        [DataMember]
        public string validFrom { get; set; }
        [DataMember]
        public string validThrough { get; set; }
        [DataMember]
        public string itinerary { get; set; }
        [DataMember]
        public string itineraryTranslated { get; set; }
        [DataMember]
        public string warnings { get; set; }
        [DataMember]
        public string warningsTranslated { get; set; }
        [DataMember]
        public string safety { get; set; }
        [DataMember]
        public string safetyTranslated { get; set; }
        [DataMember]
        public string latitude { get; set; }
        [DataMember]
        public string longitude { get; set; }
        [DataMember]
        public string address { get; set; }
        [DataMember]
        public int minPax { get; set; }
        [DataMember]
        public int maxPax { get; set; }
        [DataMember]
        public decimal basePrice { get; set; }
        [DataMember]
        public ExperiencesCurrency currency { get; set; }
        [DataMember]
        public ExperiencesCurrency convertedCurrency { get; set; }
        [DataMember]
        public bool? isFlatPaxPrice { get; set; }
        [DataMember]
        public int reviewCount { get; set; }
        [DataMember]
        public decimal? reviewAverageScore { get; set; }
        [DataMember]
        public string typeName { get; set; }
        [DataMember]
        public string tourType { get; set; }
        [DataMember]
        public string typeUuid { get; set; }
        [DataMember]
        public string photosUrl { get; set; }
        [DataMember]
        public string businessHoursFrom { get; set; }
        [DataMember]
        public string businessHoursTo { get; set; }
        [DataMember]
        public int averageDelivery { get; set; }
        [DataMember]
        public bool? hotelPickup { get; set; }
        [DataMember]
        public bool? airportPickup { get; set; }
        [DataMember]
        public bool? hasOptions { get; set; }
        [DataMember]
        public bool? allProductTypesHaveOptions { get; set; }
        [DataMember]
        public List<Photo> photos { get; set; }
        [DataMember]
        public List<Location> locations { get; set; }
        [DataMember]
        public List<ExperiencesLanguage> guideLanguages { get; set; }
        [DataMember]
        public List<ExperiencesLanguage> audioHeadsetLanguages { get; set; }
        [DataMember]
        public List<ExperiencesLanguage> writtenLanguages { get; set; }
        [DataMember]
        public List<ExperiencesLanguage> translationLanguages { get; set; }
        [DataMember]
        public bool? isSrvEligible { get; set; }
        [DataMember]
        public string imagePath { get; set; }
    }
}