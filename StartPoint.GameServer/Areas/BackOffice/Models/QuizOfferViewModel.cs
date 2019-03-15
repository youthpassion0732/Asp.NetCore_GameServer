using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StartPoint.GameServer.Areas.BackOffice.Models
{
    public class QuizOfferViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int MinimumScore { get; set; }

        public IFormFile VideoFile { get; set; }

        public IFormFile PhotoFile { get; set; }

        [Required]
        public int GameId { get; set; }
    }
}
