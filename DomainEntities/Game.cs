using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace DomainEntities
{
    public class Game
    {
        [Key]
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

        public string Logo { get; set; }

        public string Banner { get; set; }

        public string CSSSkin { get; set; }

        public string PrivacyAndGameConditions { get; set; }

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

        public enum LoginRuleTypeEnum
        {
            InternalLogin = 1,
            SocialMediaLogin = 2,
            CustomLogin = 3
        }

        public enum GameTypeEnum
        {
            Slot = 1,
            Quiz = 2
        }

        public enum GameSlotTypeEnum
        {
            OncePerDay = 1,
            OncePerWeek = 2,
            OncePerMonth = 3,
            MultiPerUser = 4
        }
    }
}
