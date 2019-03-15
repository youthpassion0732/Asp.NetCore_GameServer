using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainEntities
{
   public class QuizOffer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int MinimumScore { get; set; }
        
        public string VideoName { get; set; }
        
        public string PhotoName { get; set; }

        [Required]
        [ForeignKey("Game")]
        public int GameId { get; set; }

        public virtual Game Game { get; set; }
    }
}
