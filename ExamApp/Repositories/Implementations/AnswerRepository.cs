using ExamApp.Models;
using ExamApp.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Repositories.Implementations
{
    public class AnswerRepository : IAnswerRepository
    {
        public AnswerRepository(ApplicationDbContext _db)
        {
            Db = _db;
        }

        public ApplicationDbContext Db { get; }

        public void AddRange(params List<Answer> answers)
        {
            Db.Answers.AddRange(answers);
        }

        public Task<Answer> CreateAsync(Answer answer)
        {
            var entity= Db.Answers.Add(answer).Entity;
            return Task.FromResult(entity);
        }

        public bool Delete(Answer answer)
        {
            return Db.Answers.Remove(answer)?.Entity != null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var answer = await Db.Answers.FindAsync(id);
            if(answer == null) return false;
            
            return Db.Answers.Remove(answer)?.Entity != null;
        }

        public async Task<List<Answer>> GetAllAsync()
        {
            return await Db.Answers.ToListAsync();
        }

        public async Task<Answer?> GetByIdAsync(int id)
        {
            return await Db.Answers.FindAsync(id);
        }

        public async Task<List<Answer>> GetByQuestionIdAsync(int questionId)
        {
            return await Db.Answers.Where(a=> a.QuestionId == questionId).ToListAsync();
        }

        public async Task<bool> UpdateAsync(Answer entity)
        {
            return Db.Answers.Update(entity).Entity != null;
        }

        public bool UpdateRange(List<Answer> answers)
        {
            Db.Answers.UpdateRange(answers);
            return true;
        }
    }
}
