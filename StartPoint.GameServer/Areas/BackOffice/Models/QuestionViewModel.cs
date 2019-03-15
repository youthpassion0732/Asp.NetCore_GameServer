using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StartPoint.GameServer.Areas.BackOffice.Models
{
    public class QuestionViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TitleInEnglish { get; set; }
        
        public string TitleInFrench { get; set; }
        
        public string TitleInGerman { get; set; }
        
        public string TitleInSpain { get; set; }
        
        public string TitleInItalian { get; set; }

        public IFormFile Photo1 { get; set; }

        public IFormFile Photo2 { get; set; }

        public IFormFile Photo3 { get; set; }

        public IFormFile Photo4 { get; set; }

        public IFormFile Photo5 { get; set; }

        public IFormFile VideoFile { get; set; }

        [Required]
        public int MaxTimeAllowed { get; set; }

        [Required]
        public int ScoreToWin { get; set; }

        [Required]
        public int QuizCategoryId { get; set; }
    }
}
