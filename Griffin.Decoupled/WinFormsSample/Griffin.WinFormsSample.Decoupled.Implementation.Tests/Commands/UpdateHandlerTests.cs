using NSubstitute;
using WinFormsSample.Decoupled;
using WinFormsSample.Decoupled.Commands;
using WinFormsSample.Decoupled.Implementation.Commands;
using WinFormsSample.Domain;
using Xunit;

namespace Griffin.WinFormsSample.Decoupled.Implementation.Tests.Commands
{
    public class UpdateHandlerTests
    {
        public UpdateHandlerTests()
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
            var handler = new UpdateHandler(storage);

            handler.Invoke(new UpdateNote("10", "Some new body"));

            Assert.Equal("Some new body", note.Body);
            storage.Received().Save(note);
        }
    }
}