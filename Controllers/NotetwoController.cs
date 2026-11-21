using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotesApi.Models;
using NotesApi.Services;
using System.Security.Claims;

namespace NotesApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]

    public class NotetwoController : Controller
    {
      
            private readonly NotesService _notesService;

            public NotetwoController(NotesService notesService) =>
                _notesService = notesService;

            [HttpGet]
            public async Task<ActionResult<List<Note>>> Get()
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


                if (string.IsNullOrEmpty(userId))
                    return Unauthorized("User ID is missing in token.");


                var notes = await _notesService.GetByUserIdAsync(userId);
                return Ok(notes);
            }

            [HttpGet("{id},{usersid}")]
            public async Task<ActionResult<Note>> Get(string id,string usersid)
            {
               var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(userId) || userId != usersid)
                    return Forbid();

                var note = await _notesService.GetAsync(id);
                if (note is null || note.UserId != userId)
                    return NotFound("Note not found or not authorized.");

                return Ok(note);
            }

           
        }

    
}
