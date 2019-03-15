using DomainEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StartPoint.GameServer.Areas.Front.Models
{
    public class HomeViewModel
    {
        public GameViewModel GameModel { get; set; }
        public List<QuizCategory> QuizCategories { get; set; }
    }

    public class GameViewModel
    {
        public int GameId { get; set; }
        public int GameType { get; set; }
        public string GameName { get; set; }
        public string GameDescription { get; set; }
        public string GameLogo { get; set; }
        public string GameBanner { get; set; }
        public string GameCSSSkin { get; set; }

        public int? GameSlotType { get; set; }
        public int? GameSlotsCount { get; set; }
        public List<Icon> SlotIcons { get; set; }
        public bool AllowedToPlay { get; set; }

        public bool? EnableTimer { get; set; }
        public int? TimerMaxPerQuestion { get; set; }
        public bool? DisplayScore { get; set; }
    }

    public class OfferViewModel
    {
        public int OfferId { get; set; }
        public string VideoName { get; set; }
        public string VideoURL { get; set; }
        public string Description { get; set; }
        public string Photo1 { get; set; }
        public string Photo2 { get; set; }
        public string Photo3 { get; set; }
        public string Photo4 { get; set; }
        public string Photo5 { get; set; }
        public int SlotCode { get; set; }
        public int GameId { get; set; }
    }
}