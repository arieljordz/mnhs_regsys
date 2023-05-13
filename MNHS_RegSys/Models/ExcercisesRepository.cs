using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using MNHS_RegSys.Models;

namespace MNHS_RegSys.Models
{
    public class ExcercisesRepository
    {
        mnhsregsysEntities db = new mnhsregsysEntities();

        public void LogOut(int UserID)
        {
            HttpCookie httpCookie = HttpContext.Current.Response.Cookies[UserID];
            httpCookie.Expires = DateTime.Today.AddDays(-1);
            HttpContext.Current.Response.SetCookie(httpCookie);
            HttpContext.Current.Session.Clear();
        }


        public string[] ListOfQuestionsArray()
        {
            var ObjList = (from a in db.tbl_Question select a.Description).ToArray();

            return ObjList;
        }

        public List<QuestionsDTO> ListOfQuestions()
        {
            var ObjList = (from a in db.tbl_Question
                           select new QuestionsDTO
                           {
                               QuestionID = a.QuestionID,
                               Question = a.Description
                           }).ToList();

            return ObjList;
        }

        public List<AnswersDTO> ListOfAnswers()
        {
            var ObjList = (from a in db.tbl_Answer
                           select new AnswersDTO
                           {
                               AnswerID = a.AnswerID,
                               QuestionID = (int)a.QuestionID,
                               Answer = a.Description
                           }).ToList();

            return ObjList;
        }

    }///end
}