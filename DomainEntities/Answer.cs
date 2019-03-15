using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainEntities
{
   public class Answer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string AnswerInEnglish { get; set; }
        
        public string AnswerInFrench { get; set; }
        
        public string AnswerInGerman { get; set; }
        
        public string AnswerInSpain { get; set; }
        
        public string AnswerInItalian { get; set; }
        
        public bool IsTrue { get; set; }

        [ForeignKey("Question")]
        public int QuestionId { get; set; }
       
        public virtual Question Question { get; set; }
    }
}
