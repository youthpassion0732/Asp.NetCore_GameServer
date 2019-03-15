using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StartPoint.GameServer.Areas.BackOffice.Models
{
    public class GameViewModel
    {
        public int Id { get; set; }
        [Required]
        public int GameType { get; set; }
        [Required]
        public string GameName { get; set; }
        [Required]
        public string GameDescription { get; set; }

        #region LoginRule
        [Required]
        public int LoginRule { get; set; }
        public bool LoginWithFacbook { get; set; }
        public bool LoginWithGoogle { get; set; }
        public bool LoginWithTwitter { get; set; }
        #endregion

        [Required]
        public IFormFile Logo { get; set; }
        [Required]
        public IFormFile Banner { get; set; }
        [Required]
        public IFormFile CSSSkin { get; set; }
        [Required]
        public string PrivacyAndGameConditions { get; set; }
        [Required]
        public bool EnableAnonymous { get; set; }

        #region If game is slot type
        public int? GameSlotType { get; set; }
        public int? GameSlotsCount { get; set; }
        #endregion

        #region If game is Quizz type
        public bool EnableTimer { get; set; }
        public int? TimerMaxPerQuestion { get; set; }
        public bool DisplayScore { get; set; }
        #endregion
    }
}
