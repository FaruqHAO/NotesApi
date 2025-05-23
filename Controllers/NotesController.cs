using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApi.Models;
using NotesApi.Services;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;  // For JwtRegisteredClaimNames

namespace NotesApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly NotesService _notesService;

        public NotesController(NotesService notesService) =>
            _notesService = notesService;

        [HttpGet]
        public async Task<ActionResult<List<Note>>> Get()
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID is missing in token.");

            var notes = await _notesService.GetByUserIdAsync(userId);
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Note>> Get(string id)
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            var note = await _notesService.GetAsync(id);
            if (note is null || note.UserId != userId)
                return NotFound("Note not found or not authorized.");

            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateNoteDto dto)
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            if (string.IsNullOrEmpty(userId))
                return Unauthorized("User ID is missing in token.");

            var note = new Note
            {
                Title = dto.Title,
                Content = dto.Content,
                UserId = userId
            };

            await _notesService.CreateAsync(note);
            return CreatedAtAction(nameof(Get), new { id = note.Id }, note);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, Note updatedNote)
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            var note = await _notesService.GetAsync(id);
            if (note is null || note.UserId != userId)
                return NotFound("Note not found or not authorized.");

            updatedNote.Id = note.Id;
            updatedNote.UserId = note.UserId;

            await _notesService.UpdateAsync(id, updatedNote);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            var note = await _notesService.GetAsync(id);
            if (note is null || note.UserId != userId)
                return NotFound("Note not found or not authorized.");

            await _notesService.DeleteAsync(id);
            return NoContent();
        }
    }
}
