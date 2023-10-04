using NotesApp.Models;
using Serilog;

namespace NotesApp.Data
{
    public class NotesService : INotesService
    {
        private readonly AppDbContext _context;

        public NotesService(AppDbContext context)
        {
            _context = context;
        }

        public Task<bool> CreateNote(Note note)
        {
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note));
            }

            _context.Notes.Add(note);

            Log.Information($"Added note with an id of {_context.Notes.Select(x => x).OrderBy(x => x.Id).LastOrDefault().Id + 1}", note);
            return Task.FromResult(_context.SaveChanges() > 0);
        }

        public Task<List<Note>> GetAllNotes()
        {
            Log.Information($"Fetched all notes");
            return Task.FromResult(_context.Notes.ToList());
        }

        public Task<bool> UpdateNote(Note note)
        {
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note));
            }

            var note1 = _context.Notes.FirstOrDefault(x => x.Id == note.Id);

            Log.Information($"Updated note with an id of {note.Id}", note, note1);

            note1.Content = note.Content;

            return Task.FromResult(_context.SaveChanges() > 0);
        }
    }
}
