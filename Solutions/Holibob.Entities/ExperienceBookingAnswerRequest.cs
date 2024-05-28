using System.Collections.Generic;

namespace Holibob.Entities
{
    public class ExperienceBookingAnswerRequest
    {
        public ExperienceBookingInput input { get; set; }
        public string bookId { get; set; }
        public string currency { get; set; }
    }
    public class ExperienceBookingAnswerList
    {
        public string questionId { get; set; }
        public string value { get; set; }
    }
    public class ExperienceBookingInput
    {
        public List<ExperienceBookingAnswerList> answerList { get; set; }
    }
}
