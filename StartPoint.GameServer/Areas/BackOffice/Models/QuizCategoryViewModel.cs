using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StartPoint.GameServer.Areas.BackOffice.Models
{
    public class QuizCategoryViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TitleInEnglish { get; set; }
        
        public string TitleInFrench { get; set; }

        public string TitleInGerman { get; set; }

        public string TitleInSpain { get; set; }

        public string TitleInItalian { get; set; }

        public IFormFile IconInButtonFile { get; set; }

        public IFormFile IconInPartyFile { get; set; }

        [Required]
        public int GameId { get; set; }
    }
}
