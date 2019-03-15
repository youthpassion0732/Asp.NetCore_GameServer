using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartPoint.GameServer.Areas.Front.Models
{
    public class QuizQuestionViewModel
    {
        public Question Question { get; set; }

        public List<Answer> Answers { get; set; }

        public int? SelectedAnswerId { get; set; }

        public int QuestionId { get; set; }

        public int QuestionNumber { get; set; }

        public bool IsTimeOut { get; set; }

        public int CurrentScore { get; set; }

        public int TotalScore { get; set; }

        public int TotalQuestions { get; set; }

        public int CategoryId { get; set; }

        public string SessionId { get; set; }

        public int CurrentQuestionScore { get; set; }

        public int AllowedTime => Question == null ? 0 : Question.MaxTimeAllowed;

        public int AnswersCount => Answers == null ? 0 : Answers.Count;

        public bool IsLastQuestion => TotalQuestions - QuestionNumber == 0;

    }
}
