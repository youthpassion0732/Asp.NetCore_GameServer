using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainEntities
{
    public class GameUser
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Game")]
        [Required]
        public int GameId { get; set; }

        [ForeignKey("User")]
        [Required]
        public string UserId { get; set; }

        public DateTime? LastPlayedDate { get; set; }

        public virtual Game Game { get; set; }

        public virtual IdentityUser User { get; set; }
    }
}
