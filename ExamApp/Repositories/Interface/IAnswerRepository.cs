using ExamApp.Models;

namespace ExamApp.Repositories.Interface
{
    public interface IAnswerRepository: IRepository<Answer>
    {
        bool UpdateRange(List<Answer> answers);
        Task<List<Answer>> GetByQuestionIdAsync(int questionId);
        void AddRange(params List<Answer> choices);
        bool Delete(Answer answer);
    }
}
