using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainEntities
{
   public class QuizCategory
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TitleInEnglish { get; set; }
        
        public string TitleInFrench { get; set; }
        
        public string TitleInGerman { get; set; }
        
        public string TitleInSpain { get; set; }
        
        public string TitleInItalian { get; set; }
        
        public string IconInButtonName { get; set; }
        
        public string IconInPartyName { get; set; }

        [Required]
        [ForeignKey("Game")]
        public int GameId { get; set; }

        public virtual Game Game { get; set; }
    }
}
