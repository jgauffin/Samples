using System;
using System.Linq;
using Xunit;

namespace WinFormsSample.Domain.Tests
{
    public class NoteTests
    {
        public NoteTests()
        {
            TestDomainEvent.UseMe();
        }

        [Fact]
        public void Constructor_Empty()
        {
            Assert.Throws<ArgumentNullException>(() => new Note(null, null));
        }

        [Fact]
        public void Constructor_EmptyBody()
        {
            Assert.Throws<ArgumentNullException>(() => new Note("Some title", null));
        }

        [Fact]
        public void Constructor_EmptyTitle()
        {
            Assert.Throws<ArgumentNullException>(() => new Note(null, "Some body"));
        }

        [Fact]
        public void Constructor_Ok()
        {
            var note = new Note("Title", "This is a body.");

            Assert.Equal(DateTime.Today, note.CreatedAt.Date);
            Assert.Equal("Title", note.Title);
            Assert.Equal("This is a body.", note.Body);
            Assert.Equal(DateTime.MinValue, note.CompletedAt);
            Assert.Equal(DateTime.MinValue, note.UpdatedAt);
            Assert.False(note.IsCompleted);
        }

        [Fact]
        public void Updated()
        {
            var note = new Note("Title", "This is a body.");
            note.GetType().GetProperty("Id").SetValue(note, "1", null);

            note.Update("The new body.");

            Assert.Equal(DateTime.Today, note.UpdatedAt.Date);
            Assert.Equal("The new body.", note.Body);
            Assert.Equal(DateTime.MinValue, note.CompletedAt);
            Assert.False(note.IsCompleted);

            var domainEvent = (NoteUpdated) TestDomainEvent.Events.First();
            Assert.Equal("1", domainEvent.Id);
            Assert.Equal("This is a body.", domainEvent.OldText);
            Assert.Equal("The new body.", domainEvent.NewText);
        }

        [Fact]
        public void Update_UpdateCompleted()
        {
            var note = new Note("Title", "This is a body.");
            note.GetType().GetProperty("Id").SetValue(note, "1", null);

            note.Complete();
            Assert.Throws<InvalidOperationException>(() => note.Update("The new body."));
        }

        // Just a business requirement, to make sure that it still works 
        // when future changes are being added.
        [Fact]
        public void Update_UpdateUpdated()
        {
            var note = new Note("Title", "This is a body.");
            note.GetType().GetProperty("Id").SetValue(note, "1", null);

            note.Update("The new body.");
            note.Update("The new body2.");

            Assert.Equal("The new body2.", note.Body);
        }


        [Fact]
        public void Completed()
        {
            var note = new Note("Title", "This is a body.");
            note.GetType().GetProperty("Id").SetValue(note, "1", null);

            note.Complete();

            Assert.Equal(DateTime.MinValue, note.UpdatedAt);
            Assert.Equal(DateTime.Today, note.CompletedAt.Date);
            Assert.True(note.IsCompleted);

            var domainEvent = (NoteCompleted) TestDomainEvent.Events.First();
            Assert.Equal("1", domainEvent.Id);
        }

        [Fact]
        public void Complete_AlreadyCompleted()
        {
            var note = new Note("Title", "This is a body.");
            note.GetType().GetProperty("Id").SetValue(note, "1", null);

            note.Complete();
            Assert.Throws<InvalidOperationException>(() => note.Complete());
        }
    }
}