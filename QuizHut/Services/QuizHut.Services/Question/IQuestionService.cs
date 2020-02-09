﻿namespace QuizHut.Services.Question
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Primitives;
    using QuizHut.Web.ViewModels.Quiz;

    public interface IQuestionService
    {
        Task<string> AddNewQuestionAsync(string quizId, string quizText);

        IOrderedQueryable<AttemtedQuizQuestionViewModel> GetAllQuestionsQuizById(string id);

        Task DeleteQuestionByIdAsync(string id);

        int CalculateQuestionResult(IEnumerable<KeyValuePair<string, StringValues>> assumtions, IEnumerable<KeyValuePair<string, StringValues>> rightAnswers);
    }
}
