using NotesApp.Data;
using Moq.EntityFrameworkCore;
using Moq;
using NotesApp.Models;
using Microsoft.EntityFrameworkCore;

namespace NotesAppTests
{
    public class UnitTest
    {
        private static DateTime date = new DateTime(2023, 09, 03, 12, 35, 30, DateTimeKind.Utc);

        private readonly List<Note> _notes = new List<Note>()
        {
            new Note(){Id = 1, Title = "Note1", Content = "NoteContent1", CreatedDate = date},
            new Note(){Id = 2, Title = "Note2", Content = "NoteContent2", CreatedDate = date},
            new Note(){Id = 3, Title = "Note3", Content = "NoteContent3", CreatedDate = date},
            new Note(){Id = 4, Title = "Note4", Content = "NoteContent4", CreatedDate = date},
            new Note(){Id = 5, Title = "Note5", Content = "NoteContent5", CreatedDate = date},
            new Note(){Id = 6, Title = "Note6", Content = "NoteContent6", CreatedDate = date},
            new Note(){Id = 7, Title = "Note7", Content = "NoteContent7", CreatedDate = date},
            new Note(){Id = 8, Title = "Note8", Content = "NoteContent8", CreatedDate = date},
            new Note(){Id = 9, Title = "Note9", Content = "NoteContent9", CreatedDate = date},
            new Note(){Id = 10, Title = "Note10", Content = "NoteContent10", CreatedDate = date},
        };

        private readonly Note createdNote = new Note() { Id = 11, Title = "Note11", Content = "NoteContent11", CreatedDate = date };

        private readonly Note updatedNote = new Note() { Id = 1, Title = "Note1", Content = "NoteContent123", CreatedDate = date };

        [Fact]
        public async Task GetAllNotes_ReturnsNotesList()
        {
            Mock<AppDbContext> _context = new Mock<AppDbContext>();

            _context.Setup(x => x.Notes).Returns(DbContextMock.GetQueryableMockDbSet(_notes));
            _context.Setup(p => p.SaveChanges()).Returns(1);

            INotesService service = new NotesService(_context.Object);
            var notes = await service.GetAllNotes();

            Assert.NotNull(notes);
            Assert.Equal(10, notes.Count);
        }

        [Fact]
        public async Task CreateNote_CreatesNote()
        {
            Mock<AppDbContext> _context = new Mock<AppDbContext>();

            _context.Setup(x => x.Notes).Returns(DbContextMock.GetQueryableMockDbSet(_notes));
            _context.Setup(p => p.SaveChanges()).Returns(1);

            INotesService service = new NotesService(_context.Object);
            var result = await service.CreateNote(createdNote);

            Assert.True(result);
            Assert.Equal(_context.Object.Notes.Select(x => x).Where(x => x.Id == createdNote.Id).FirstOrDefault(), createdNote);
        }

        [Fact]
        public async Task UpdateNote_UpdatesNote()
        {
            Mock<AppDbContext> _context = new Mock<AppDbContext>();

            _context.Setup(x => x.Notes).Returns(DbContextMock.GetQueryableMockDbSet(_notes));
            _context.Setup(p => p.SaveChanges()).Returns(1);

            INotesService service = new NotesService(_context.Object);
            var result = await service.UpdateNote(updatedNote);

            Assert.True(result);
            Assert.Equal(_context.Object.Notes.Select(x => x).Where(x => x.Id == updatedNote.Id).FirstOrDefault().Content, updatedNote.Content);
        }
    }
}