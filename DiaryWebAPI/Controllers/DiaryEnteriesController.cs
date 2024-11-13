using DiaryWebAPI.Data;
using DiaryWebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiaryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryEnteriesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DiaryEnteriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiaryEntry>>> GetDiaryEntries()
        {
            return await _context.DiaryEntries.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DiaryEntry>> GetDiaryEntry(int id)
        {
            var diaryEntry = await _context.DiaryEntries.FindAsync(id);
            if (diaryEntry == null)
            {
                return NotFound();
            }
            return diaryEntry;
        }

        [HttpPost]
        public async Task<ActionResult<DiaryEntry>> PostDiaryEntry(DiaryEntry diaryEntry)
        {
            diaryEntry.Id = 0;
            _context.DiaryEntries.Add(diaryEntry);
            await _context.SaveChangesAsync();
            return Created("", diaryEntry);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteDiaryEntry(int Id)
        {
            var diaryEntry = await _context.DiaryEntries.FindAsync(Id);
            if (diaryEntry == null)
            {
                return NotFound();
            }

            _context.DiaryEntries.Remove(diaryEntry);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult<DiaryEntry>> UpdateDiaryEntry(int Id, [FromBody] DiaryEntry diaryEntry)
        {
            if (Id != diaryEntry.Id)
            {
                return BadRequest(Id + " ID mismatch " + diaryEntry.Id);
            }

            _context.Entry(diaryEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return diaryEntry;
        }
    }
}
