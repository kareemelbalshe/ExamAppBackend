using AutoMapper;
using ExamApp.DTOs.Answer;
using ExamApp.Models;
using ExamApp.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        public AnswerController(IUnitOfWork _uow, IMapper _mapper)
        {
            Uow = _uow;
            Mapper = _mapper;
        }
        IUnitOfWork Uow { get; }
        IMapper Mapper { get; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var answers = await Uow.AnswerRepo.GetAllAsync();
            return Ok(Mapper.Map<List<AnswerDto>>(answers));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Console.WriteLine($"\n**************************************************\nFetching Answer with Id {id}\n**************************************************");
            var answer = await Uow.AnswerRepo.GetByIdAsync(id);
            Console.WriteLine($"Correctly retrived {answer?.Id} {answer?.QuestionId} {answer?.ResultId}");
            if (answer == null) return NotFound();

            return Ok(answer);
        }

        [HttpGet("q/{id}")]
        public async Task<IActionResult> GetByQuestionId(int id)
        {
            var answers = await Uow.AnswerRepo.GetByQuestionIdAsync(id);
            if (answers.Count == 0) return NotFound();

            return Ok(answers);
        }

        [HttpPost]
        public async Task<IActionResult> Create(AnswerDto answerDto)
        {
            var choice = await Uow.ChoiceRepo.GetByIdAsync(answerDto.ChoiceId);
            if (choice == null) return BadRequest("choice not found");
            if (choice.QuestionId != answerDto.QuestionId) return BadRequest("Question and choice don't match!");

            var answer = await Uow.AnswerRepo.CreateAsync(Mapper.Map<Answer>(answerDto));
            try
            {
                await Uow.SaveChangesAsync();
                return CreatedAtAction(nameof(GetAll), new {}, answer);
            }
            catch
            {
                return Conflict();
            }
        }
        //[HttpPost]
        //public async Task<IActionResult> Create(List<AnswerDto> answerDtos)
        //{
        //    Uow.AnswerRepo.AddRange(Mapper.Map<List<Answer>>(answerDtos));
        //    try
        //    {
        //        int noRowsAdded = await Uow.SaveChangesAsync();
        //        //return NoContent();
        //        return CreatedAtAction(nameof(GetAll), new {}, answerDtos);
        //    }
        //    catch
        //    {
        //        return Conflict();
        //    }
        //}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(AnswerDto answerDto, int id)
        {

            if (answerDto.Id != id) return BadRequest();

            var answer = Mapper.Map<Answer>(answerDto);
            await Uow.AnswerRepo.UpdateAsync(answer);

            try
            {
                var noRowsAffected = await Uow.SaveChangesAsync();

                if(noRowsAffected == 0) return NotFound();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("A concurrency conflict occurred.");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateRange(List<AnswerDto> answerDto)
        {

            if (answerDto == null || answerDto.Count == 0) return BadRequest();


            var choices = Mapper.Map<List<Answer>>(answerDto);
            Uow.AnswerRepo.UpdateRange(choices);

            try
            {
                var noRowsUpdated = await Uow.SaveChangesAsync();
                if (noRowsUpdated > 0) return NoContent();

                return NotFound();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict("A concurrency conflict occurred.");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByIdAsync(int id)
        {
            var answer = await Uow.AnswerRepo.GetByIdAsync(id);
            if (answer == null) return NotFound();
            Uow.AnswerRepo.Delete(answer);

            try
            {
                await Uow.SaveChangesAsync();
                return NoContent();
            }
            catch
            {
                return Conflict();
            }
        }


    }
}
