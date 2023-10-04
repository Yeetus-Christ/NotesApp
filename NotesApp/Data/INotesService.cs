using NotesApp.Models;

namespace NotesApp.Data
{
    public interface INotesService
    {
        public Task<bool> CreateNote(Note note);

        public Task<List<Note>> GetAllNotes();

        public Task<bool> UpdateNote(Note note);
    }
}
