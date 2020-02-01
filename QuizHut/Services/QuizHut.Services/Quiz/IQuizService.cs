﻿using QuizHut.Web.ViewModels.Quiz;
using System.Threading.Tasks;

namespace QuizHut.Services.Quiz
{
    public interface IQuizService
    {
        Task<int> AddNewQuizAsync(InputQuizViewModel quizModel);

    }
}