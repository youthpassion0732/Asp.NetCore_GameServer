using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StartPoint.GameServer.Areas.BackOffice.Models
{
    public class OfferViewModel
    {
        public int Id { get; set; }
        public IFormFile VideoFile { get; set; }
        public string VideoURL { get; set; }
        [Required]
        public string Description { get; set; }
        public IFormFile Photo1 { get; set; }
        public IFormFile Photo2 { get; set; }
        public IFormFile Photo3 { get; set; }
        public IFormFile Photo4 { get; set; }
        public IFormFile Photo5 { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "should be greater than 0")]
        public int SlotCode { get; set; }
        [Required]
        public int GameId { get; set; }
    }
}
