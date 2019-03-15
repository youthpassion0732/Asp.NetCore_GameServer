using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainEntities
{
   public class Question
    {
        [Key]
        public int Id { get; set; }
        
        public string TitleInEnglish { get; set; }
        
        public string TitleInFrench { get; set; }
        
        public string TitleInGerman { get; set; }
        
        public string TitleInSpain { get; set; }
        
        public string TitleInItalian { get; set; }

        public string Photo1 { get; set; }

        public string Photo2 { get; set; }

        public string Photo3 { get; set; }

        public string Photo4 { get; set; }

        public string Photo5 { get; set; }

        public string VideoName { get; set; }

        [Required]
        public int MaxTimeAllowed { get; set; }

        [Required]
        public int ScoreToWin { get; set; }

        [ForeignKey("QuizCategory")]
        public int QuizCategoryId { get; set; }
       
        public virtual QuizCategory QuizCategory { get; set; }
    }
}
