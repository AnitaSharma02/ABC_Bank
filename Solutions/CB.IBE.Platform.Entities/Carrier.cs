using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace CB.IBE.Platform.Entities
{

    [DataContract]
    [Serializable]
    public class Carrier
    {
        string mstrCarrierCode = string.Empty;
        [DataMember]
        public string CarrierCode
        {
            get { return mstrCarrierCode; }
            set { mstrCarrierCode = value; }
        }

        string mstrCarrierName = string.Empty;
        [DataMember]
        public string CarrierName
        {
            get { return mstrCarrierName; }
            set { mstrCarrierName = value; }
        }

        string mstrCarrierLogoPath = string.Empty;
        [DataMember]
        public string CarrierLogoPath
        {
            get { return mstrCarrierLogoPath; }
            set { mstrCarrierLogoPath = value; }
        }

        private string pstrIATACode = string.Empty;
        [DataMember]
        public string IATACode
        {
            get { return pstrIATACode; }
            set { pstrIATACode = value; }
        }
        private string pstrICAOCode = string.Empty;
        [DataMember]
        public string ICAOCode
        {
            get { return pstrICAOCode; }
            set { pstrICAOCode = value; }
        }

        private string pstrAircraftType = string.Empty;
        [DataMember]
        public string AircraftType
        {
            get { return pstrAircraftType; }
            set { pstrAircraftType = value; }
        }

        private string pstrWakeCategory = string.Empty;
        [DataMember]
        public string WakeCategory
        {
            get { return pstrWakeCategory; }
            set { pstrWakeCategory = value; }
        }
        private string mstrEquipmentType = string.Empty;
        [DataMember]
        public string EquipmentType
        {
            get { return mstrEquipmentType; }
            set { mstrEquipmentType = value; }
        }

        private bool mboolIsLcc = false;

        [DataMember]
        public bool IsLcc
        {
            get { return mboolIsLcc; }
            set { mboolIsLcc = value; }
        }
    }
}
