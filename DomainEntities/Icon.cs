using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainEntities
{
   public class Icon
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int Value { get; set; }

        public string IconName { get; set; }

        [ForeignKey("Game")]
        public int GameId { get; set; }
       
        public virtual Game Game { get; set; }
    }
}
