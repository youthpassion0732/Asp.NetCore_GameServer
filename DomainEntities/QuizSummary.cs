using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace DomainEntities
{
   public class QuizSummary
    {
        [Key]
        public int Id { get; set; }
        
        public string SessionId { get; set; }

        public bool IsPass { get; set; }

        public int TotalQuestions { get; set; }

        public int TotalScore { get; set; }

        public int ObtainedScore { get; set; }

        public DateTime CreateDateTime { get; set; }

        [ForeignKey("QuizCategory")]
        public int QuizCategoryId { get; set; }

        [ForeignKey("User")]
        [Required]
        public string UserId { get; set; }

        public virtual QuizCategory QuizCategory { get; set; }

        public virtual IdentityUser User { get; set; }
    }
}
