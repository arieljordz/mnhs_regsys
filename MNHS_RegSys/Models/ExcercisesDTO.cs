using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MNHS_RegSys.Models
{
    public class QuestionsDTO
    {
        public int QuestionID { get; set; }
        public string Question { get; set; }
    }
    public class AnswersDTO
    {
        public int AnswerID { get; set; }
        public string Answer { get; set; }
        public int QuestionID { get; set; }
    }

}