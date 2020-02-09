﻿namespace QuizHut.Services.Question
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Primitives;
    using QuizHut.Data.Common.Repositories;
    using QuizHut.Data.Models;
    using QuizHut.Services.Common;
    using QuizHut.Services.Mapping;
    using QuizHut.Web.ViewModels.Answer;
    using QuizHut.Web.ViewModels.Question;
    using QuizHut.Web.ViewModels.Quiz;

    public class QuestionService : IQuestionService
    {
        private readonly IDeletableEntityRepository<Question> repository;
        private readonly IDeletableEntityRepository<Quiz> quizRepository;

        public QuestionService(IDeletableEntityRepository<Question> repository, IDeletableEntityRepository<Quiz> quizRepository)
        {
            this.repository = repository;
            this.quizRepository = quizRepository;
        }

        public async Task<string> AddNewQuestionAsync(QuestionViewModel questionModel)
        {
            var quizId = questionModel.QuizId;
            var quiz = await this.quizRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                Questions = x.Questions.ToList(),
            }).FirstOrDefaultAsync(x => x.Id == quizId);

            var question = new Question
            {
                Number = quiz.Questions.Count() + 1,
                Text = questionModel.Text,
                QuizId = quizId,
            };

            await this.repository.AddAsync(question);
            await this.repository.SaveChangesAsync();

            return question.Id;
        }

        public int CalculateQuestionResult(IEnumerable<KeyValuePair<string, StringValues>> assumtions, IEnumerable<KeyValuePair<string, StringValues>> answers)
        {
            foreach (var assumptionAnswer in assumtions)
            {
                var numberPatern = ServicesConstants.AnswerNumberPattern;
                var match = Regex.Match(assumptionAnswer.Key, numberPatern);
                var assumtionNumber = match.Value;

                var correspondingAnswer = answers.Where(x => x.Key.Contains(assumtionNumber)).FirstOrDefault();
                var isCorrespondingAnswerCorrect = correspondingAnswer.Value == ServicesConstants.RightAnswerValue;
                var isAssumptionCorrect = assumptionAnswer.Value.Any(x => x == ServicesConstants.RightAnswerValue.ToLower());

                if (isAssumptionCorrect != isCorrespondingAnswerCorrect)
                {
                    return ServicesConstants.NoPointsValue;
                }
            }

            return ServicesConstants.PointsValue;
        }

        public IOrderedQueryable<AttemtedQuizQuestionViewModel> GetAllQuestionsQuizById(string id)
        {
            var questions = this.repository
               .AllAsNoTracking()
               .Where(x => x.QuizId == id)
               .Select(x => new AttemtedQuizQuestionViewModel
               {
                   Text = x.Text,
                   Number = x.Number,
                   Answers = x.Answers.Select(y => new AttemtedQuizAnswerViewModel
                   {
                       Text = y.Text,
                       IsRightAnswer = y.IsRightAnswer,
                       IsRightAnswerAssumption = false,
                   }).ToList(),
               }).OrderBy(x => x.Number);

            return questions;
        }

        //public async Task<string> GetQuizIdByQuestionIdAsync(string id)
        //{
        //    var question = await this.repository.GetByIdWithDeletedAsync(id);
        //    return question.QuizId;
        //}
    }
}
