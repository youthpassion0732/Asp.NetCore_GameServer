using DomainEntities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using StartPoint.GameServer.Areas.BackOffice.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace StartPoint.GameServer.Helpers
{
    public class Helper
    {
        #region Helping methods
        public string GetFileName(IFormFile file)
        {
            return file == null ? null : file.FileName.Split('.')[0]
                                                + "-" + DateTime.Now.ToString("ddMMyyhhmmss") + "." + file.FileName.Split('.')[1];
        }
        #endregion
        #region File Uploading
        public async Task<bool> FileUploadAsync(IFormFile file, string fileName, string folder)
        {
            if (file != null)
            {
                DirectoryInfo directoryInfo = new DirectoryInfo($"wwwroot\\Resources\\{folder}");
                string directoryPath = $"wwwroot\\Resources\\{folder}";

                if (!Directory.Exists(directoryPath))
                    Directory.CreateDirectory(directoryPath);

                if (directoryInfo.Exists && file.Length > 0)
                {
                    directoryPath = directoryPath + "\\" + fileName;
                    using (System.IO.Stream stream = new FileStream(directoryPath, FileMode.Create))
                    {
                        // upload file to target location
                        await file.CopyToAsync(stream);
                    }
                    return true;
                }
            }
            return false;
        }
        #endregion

        #region Objects Mapping
        public Game GetGame(GameViewModel gameViewModel, bool withId = false)
        {
            Game game = new Game
            {
                Banner = GetFileName(gameViewModel.Banner),
                CSSSkin = GetFileName(gameViewModel.CSSSkin),
                DisplayScore = gameViewModel.DisplayScore,
                EnableAnonymous = gameViewModel.EnableAnonymous,
                EnableTimer = gameViewModel.EnableTimer,
                GameDescription = gameViewModel.GameDescription,
                GameName = gameViewModel.GameName,
                GameSlotType = gameViewModel.GameSlotType,
                GameType = gameViewModel.GameType,
                LoginRule = gameViewModel.LoginRule,
                LoginWithFacbook = gameViewModel.LoginWithFacbook,
                LoginWithGoogle = gameViewModel.LoginWithGoogle,
                LoginWithTwitter = gameViewModel.LoginWithTwitter,
                Logo = GetFileName(gameViewModel.Logo),
                PrivacyAndGameConditions = gameViewModel.PrivacyAndGameConditions,
                TimerMaxPerQuestion = gameViewModel.TimerMaxPerQuestion,
                GameSlotsCount = gameViewModel.GameSlotsCount
            };
            if (withId)
            {
                game.Id = gameViewModel.Id;
            }
            return game;
        }

        public Icon GetIcon(IconViewModel iconFromViewModel, bool withId = false)
        {
            Icon icon;
            icon = new Icon
            {
                GameId = iconFromViewModel.GameId,
                Value = iconFromViewModel.Value,
                IconName = GetFileName(iconFromViewModel.IconFile)
            };
            if (withId)
                icon.Id = iconFromViewModel.Id;
            return icon;
        }

        public Answer GetAnswer(AnswerViewModel answerFromViewModel, bool withId = false)
        {
            Answer answer;
            answer = new Answer
            {
                AnswerInEnglish = answerFromViewModel.AnswerInEnglish,
                AnswerInFrench = answerFromViewModel.AnswerInFrench,
                AnswerInGerman = answerFromViewModel.AnswerInGerman,
                AnswerInItalian = answerFromViewModel.AnswerInItalian,
                AnswerInSpain = answerFromViewModel.AnswerInSpain,
                QuestionId = answerFromViewModel.QuestionId,
                IsTrue = answerFromViewModel.IsTrue
            };
            if (withId)
                answer.Id = answerFromViewModel.Id;
            return answer;
        }

        public Offer GetOffer(OfferViewModel offerViewModel, bool withId = false)
        {
            var offer = new Offer
            {
                Description = offerViewModel.Description,
                Photo1 = GetFileName(offerViewModel.Photo1),
                Photo2 = GetFileName(offerViewModel.Photo2),
                Photo3 = GetFileName(offerViewModel.Photo3),
                Photo4 = GetFileName(offerViewModel.Photo4),
                Photo5 = GetFileName(offerViewModel.Photo5),
                SlotCode = offerViewModel.SlotCode,
                VideoName = GetFileName(offerViewModel.VideoFile),
                VideoURL = offerViewModel.VideoURL,
                GameId = offerViewModel.GameId
            };
            if (withId)
                offer.Id = offerViewModel.Id;
            return offer;
        }

        public OfferViewModel GetOfferViewModel(Offer offer, bool withId = false)
        {
            if (offer == null)
                return null;
            var offerViewModel = new OfferViewModel
            {
                Description = offer.Description,
                SlotCode = offer.SlotCode,
                VideoURL = offer.VideoURL,
                GameId = offer.GameId
            };
            if (withId)
                offerViewModel.Id = offer.Id;
            return offerViewModel;
        }

        public GameViewModel GetGameViewModel(Game game, bool withId = false)
        {
            GameViewModel gameViewModel = new GameViewModel
            {
                DisplayScore = game.DisplayScore,
                EnableAnonymous = game.EnableAnonymous,
                EnableTimer = game.EnableTimer,
                GameDescription = game.GameDescription,
                GameName = game.GameName,
                GameSlotType = game.GameSlotType,
                GameType = game.GameType,
                LoginRule = game.LoginRule,
                LoginWithFacbook = game.LoginWithFacbook,
                LoginWithGoogle = game.LoginWithGoogle,
                LoginWithTwitter = game.LoginWithTwitter,
                PrivacyAndGameConditions = game.PrivacyAndGameConditions,
                TimerMaxPerQuestion = game.TimerMaxPerQuestion,
                GameSlotsCount = game.GameSlotsCount
            };
            if (withId)
            {
                gameViewModel.Id = game.Id;
            }
            return gameViewModel;
        }

        public QuizCategory GetQuizCategory(QuizCategoryViewModel quizCategoryViewModel, bool withId = false)
        {
            var quizCategory = new QuizCategory
            {
                IconInButtonName = GetFileName(quizCategoryViewModel.IconInButtonFile),
                IconInPartyName = GetFileName(quizCategoryViewModel.IconInPartyFile),
                TitleInEnglish = quizCategoryViewModel.TitleInEnglish,
                TitleInFrench = quizCategoryViewModel.TitleInFrench,
                TitleInGerman = quizCategoryViewModel.TitleInGerman,
                TitleInItalian = quizCategoryViewModel.TitleInItalian,
                TitleInSpain = quizCategoryViewModel.TitleInSpain,
                GameId = quizCategoryViewModel.GameId
            };
            if (withId)
                quizCategory.Id = quizCategoryViewModel.Id;
            return quizCategory;
        }

        public Question GetQuestion(QuestionViewModel questionViewModel, bool withId = false)
        {
            var question = new Question
            {
                VideoName = GetFileName(questionViewModel.VideoFile),
                Photo1 = GetFileName(questionViewModel.Photo1),
                Photo2 = GetFileName(questionViewModel.Photo2),
                Photo3 = GetFileName(questionViewModel.Photo3),
                Photo4 = GetFileName(questionViewModel.Photo4),
                Photo5 = GetFileName(questionViewModel.Photo5),
                TitleInFrench = questionViewModel.TitleInFrench,
                TitleInGerman = questionViewModel.TitleInGerman,
                TitleInItalian = questionViewModel.TitleInItalian,
                TitleInSpain = questionViewModel.TitleInSpain,
                TitleInEnglish = questionViewModel.TitleInEnglish,
                MaxTimeAllowed = questionViewModel.MaxTimeAllowed,
                ScoreToWin = questionViewModel.ScoreToWin,
                QuizCategoryId = questionViewModel.QuizCategoryId
            };
            if (withId)
                question.Id = questionViewModel.Id;
            return question;
        }

        public QuizOffer GetQuizOffer(QuizOfferViewModel quizOfferViewModel, bool withId = false)
        {
            var quizOffer = new QuizOffer
            {
                PhotoName = GetFileName(quizOfferViewModel.PhotoFile),
                VideoName = GetFileName(quizOfferViewModel.VideoFile),
                Description = quizOfferViewModel.Description,
                MinimumScore = quizOfferViewModel.MinimumScore,
                GameId = quizOfferViewModel.GameId
            };
            if (withId)
                quizOffer.Id = quizOfferViewModel.Id;
            return quizOffer;
        }

        public QuizCategoryViewModel GetQuizCategoryViewModel(QuizCategory quizCategory, bool withId = false)
        {
            if (quizCategory == null)
                return null;
            var quizCategoryViewModel = new QuizCategoryViewModel
            {
                TitleInEnglish = quizCategory.TitleInEnglish,
                TitleInFrench = quizCategory.TitleInFrench,
                TitleInGerman = quizCategory.TitleInGerman,
                TitleInItalian = quizCategory.TitleInItalian,
                TitleInSpain = quizCategory.TitleInSpain,
                GameId = quizCategory.GameId
            };
            if (withId)
                quizCategoryViewModel.Id = quizCategory.Id;
            return quizCategoryViewModel;
        }

        public QuestionViewModel GetQuestionViewModel(Question question, bool withId = false)
        {
            if (question == null)
                return null;
            var questionViewModel = new QuestionViewModel
            {
                TitleInFrench = question.TitleInFrench,
                TitleInGerman = question.TitleInGerman,
                TitleInItalian = question.TitleInItalian,
                TitleInSpain = question.TitleInSpain,
                TitleInEnglish = question.TitleInEnglish,
                MaxTimeAllowed = question.MaxTimeAllowed,
                ScoreToWin = question.ScoreToWin,
                QuizCategoryId = question.QuizCategoryId
            };
            if (withId)
                questionViewModel.Id = question.Id;
            return questionViewModel;
        }

        public QuizOfferViewModel GetQuizOfferViewModel(QuizOffer quizOffer, bool withId = false)
        {
            if (quizOffer == null)
                return null;
            var quizOfferViewModel = new QuizOfferViewModel
            {
                Description = quizOffer.Description,
                GameId = quizOffer.GameId,
                MinimumScore = quizOffer.MinimumScore
            };
            if (withId)
                quizOfferViewModel.Id = quizOffer.Id;
            return quizOfferViewModel;
        }
        #endregion
    }
}
