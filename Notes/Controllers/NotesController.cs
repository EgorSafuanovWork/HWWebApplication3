using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Notes.Models;

namespace Notes.Controllers
{
    public class NotesController : Controller
    {
        private static string Filename = "notes.txt";
        private static Random Rand = new();

        [HttpGet]
        public IActionResult Index()
        {
            var defNotes = new List<Note>();
            for (int i = 0; i < Rand.Next(2, 10); i++)
            {
                defNotes.Add(new()
                {
                    Title = $"Note {Rand.Next(1, 100)}",
                    Text = $"Text for {i + 1} note",
                    CreateDT = DateTime.Now,
                    Tags = new[] { $"#Tag{Rand.Next(1, 100)}", $"#Tag{Rand.Next(1, 100)}" }
                });
            }
            return View("Index", defNotes);  
        }

        [HttpPost]
        public async Task<IActionResult> SaveInFile([FromBody] List<Note> notes)
        {
            try
            {
                JsonSerializerOptions options = new()
                {
                    WriteIndented = true  
                };
                string jsonString = JsonSerializer.Serialize(notes, options);
                await System.IO.File.WriteAllTextAsync(Filename, jsonString);
                return Json(new { success = true, message = "Збережено..." });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Помилка..." });
            }
        }

        [HttpGet]
        public async Task<IActionResult> LoadFromFile()
        {
            string jsonString = await System.IO.File.ReadAllTextAsync(Filename);
            var notesFromFile = JsonSerializer.Deserialize<List<Note>>(jsonString);
            return PartialView("_NotesPartial", notesFromFile);  
        }
    }
}
