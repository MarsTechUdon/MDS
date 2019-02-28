using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDS.Models
{
    public class ManageExamModel
    {
        public string ind { get; set; }
        public string Langid { get; set; }
        public string subjectGroupid { get; set; }
        public string Question { get; set; }
        public string QuestionImage { get; set; }
        public string Questionsound { get; set; }

        public string qind { get; set; }
        public string Choice { get; set; }
        public string ChoiceImage { get; set; }
        public string ChoiceSound { get; set; }
        public string iscorrect { get; set; }

        public string iscorrect1 { get; set; }
        public string choice1 { get; set; }
        public string choiceimage1 { get; set; }
        public string choicesound1 { get; set; }

        public string iscorrect2 { get; set; }
        public string choice2 { get; set; }
        public string choiceimage2 { get; set; }
        public string choicesound2 { get; set; }

        public string iscorrect3 { get; set; }
        public string choice3 { get; set; }
        public string choiceimage3 { get; set; }
        public string choicesound3 { get; set; }

        public string iscorrect4 { get; set; }
        public string choice4 { get; set; }
        public string choiceimage4 { get; set; }
        public string choicesound4 { get; set; }

        public string User { get; set; }
        public string result { get; set; }
        public string msg { get; set; }

        public string examid { get; set; }
        public string examQNO { get; set; }
        public string examAnswerNo { get; set; }
        public string choiceid { get; set; }
        public string answer { get; set; }

        public string num { get; set; }
        public string studentname { get; set; }
        public string coursenickname { get; set; }
        public string dltNo { get; set; }

        public string array_id { get; set; }
        
        public List<ManageExamModel> QuestionList { get; internal set; }
        public List<ManageExamModel> ChoiceList { get; internal set; }
        public List<ManageExamModel> Detail { get; internal set; }
    }
}