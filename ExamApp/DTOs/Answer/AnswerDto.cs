using ExamApp.Models;

namespace ExamApp.DTOs.Answer
{
    public class AnswerDto
    {
        public int Id { get; set; }

        public int ResultId { get; set; }

        public int QuestionId { get; set; }

        public int ChoiceId { get; set; }

 
    }
}
