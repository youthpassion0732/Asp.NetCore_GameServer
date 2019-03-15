using DomainEntities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StartPoint.GameServer.Areas.BackOffice.Models
{
    public class IconViewModel
    {
        public List<Icon> Icons { get; set; }
        public int Id { get; set; }
        [Range(1, 9, ErrorMessage = "Can only be between 1 .. 9")]
        public int Value { get; set; }
        public IFormFile IconFile { get; set; }
        public int GameId { get; set; }
        public int IconsCount
        {
            get
            {
                return Icons == null ? 0 : Icons.Count;
            }
        }
    }
}
