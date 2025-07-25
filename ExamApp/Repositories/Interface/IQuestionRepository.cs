﻿using ExamApp.Models;

namespace ExamApp.Repositories.Interface
{
    public interface IQuestionRepository:IRepository<Question>
    {
        Task<List<Question>> GetByExamIdAsync(int examId);
        Task<Question> GetByIdWithChoicesAsync(int id);
        Task<List<Question>> GetByExamIdWithChoicesAsync(int examId);
    }
}
