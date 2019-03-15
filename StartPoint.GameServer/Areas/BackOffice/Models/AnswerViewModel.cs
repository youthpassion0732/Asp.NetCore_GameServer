using DomainEntities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StartPoint.GameServer.Areas.BackOffice.Models
{
    public class AnswerViewModel
    {
        public List<Answer> Answers { get; set; }

        public int Id { get; set; }

        public string AnswerInEnglish { get; set; }

        public string AnswerInFrench { get; set; }

        public string AnswerInGerman { get; set; }

        public string AnswerInSpain { get; set; }

        public string AnswerInItalian { get; set; }

        public bool IsTrue { get; set; }

        public int QuestionId { get; set; }

        public int QuizCategoryId { get; set; }

        public int AnswersCount
        {
            get
            {
                return Answers == null ? 0 : Answers.Count;
            }
        }
    }
}
