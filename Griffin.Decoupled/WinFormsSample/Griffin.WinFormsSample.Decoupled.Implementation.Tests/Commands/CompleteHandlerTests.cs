using NSubstitute;
using WinFormsSample.Decoupled;
using WinFormsSample.Decoupled.Commands;
using WinFormsSample.Decoupled.Implementation.Commands;
using WinFormsSample.Domain;
using Xunit;

namespace Griffin.WinFormsSample.Decoupled.Implementation.Tests.Commands
{
    public class CompleteHandlerTests
    {
        public CompleteHandlerTests()
        {
            TestDomainEvent.UseMe();
        }

        [Fact]
        public void Test()
        {
            var storage = Substitute.For<INoteStorage>();
            var note = new Note("My note", "Body");
            note.GetType().GetProperty("Id").SetValue(note, "10", null);
            storage.Load("10").Returns(note);
            var handler = new CompleteHandler(storage);

            handler.Invoke(new CompleteNote("10"));

            Assert.True(note.IsCompleted);
            storage.Received().Save(note);
        }
    }
}