using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DomainEntities
{
   public class QuizHistory
    {
        [Key]
        public int Id { get; set; }
        
        public string SessionId { get; set; }
        
        public bool IsCorrectAnswer { get; set; }

        public DateTime CreateDateTime { get; set; }

        [ForeignKey("QuizCategory")]
        public int QuizCategoryId { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        [ForeignKey("Answer")]
        public int? AnswerId { get; set; }

        [ForeignKey("User")]
        [Required]
        public string UserId { get; set; }

        public virtual QuizCategory QuizCategory { get; set; }

        public virtual Question Question { get; set; }

        public virtual Answer Answer { get; set; }

        public virtual IdentityUser User { get; set; }
    }
}
