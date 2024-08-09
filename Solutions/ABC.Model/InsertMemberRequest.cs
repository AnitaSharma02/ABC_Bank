using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABC.Model
{
    public class InsertMemberRequest
    {
        public List<MemberRelationsList> MemberRelationsList { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Nationality { get; set; }
        public string Address1 { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public int ProgramId { get; set; }
        public string PassportNumber { get; set; } = string.Empty;
        public string MothersMaidenName { get; set; } = string.Empty;
        public string NationalId { get; set; }= string.Empty;
        public string CustomerSegment { get; set; }=string.Empty;
        public string CustomerType { get; set; } = string.Empty;
        public string PreferredLanguage { get; set; } = string.Empty;
        public string AdditionalDetails { get; set; } = string.Empty;
        public string AdditionalDetails1 { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
    }
    public class MemberRelationsList
    {
        public int RelationType { get; set; }
        public string RelationReference { get; set; }
        public bool IsAccountActivated { get; set;}
        public int Status { get; set;}
    }
}
