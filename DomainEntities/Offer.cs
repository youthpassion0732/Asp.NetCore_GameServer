using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainEntities
{
   public class Offer
    {
        [Key]
        public int Id { get; set; }

        public string VideoName { get; set; }

        public string VideoURL { get; set; }

        [Required]
        public string Description { get; set; }

        public string Photo1 { get; set; }

        public string Photo2 { get; set; }

        public string Photo3 { get; set; }

        public string Photo4 { get; set; }

        public string Photo5 { get; set; }

        [Required]
        public int SlotCode { get; set; }

        [Required]
        [ForeignKey("Game")]
        public int GameId { get; set; }
        
        public virtual Game Game { get; set; }
    }
}
