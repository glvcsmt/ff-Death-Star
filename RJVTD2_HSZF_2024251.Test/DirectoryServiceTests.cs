using NUnit.Framework;
using Moq;
using RJVTD2_HSZF_2024251.Application.Services;
using RJVTD2_HSZF_2024251.Persistence.MsSql.Providers;

namespace RJVTD2_HSZF_2024251.Test
{
    [TestFixture]
    public class DirectoryServiceTests
    {
        private Mock<IDirectoryProvider> _mockDirectoryProvider;
        private DirectoryService _directoryService;

        [SetUp]
        public void SetUp()
        {
            _mockDirectoryProvider = new Mock<IDirectoryProvider>();
            
            _directoryService = new DirectoryService(_mockDirectoryProvider.Object);
        }

        [Test]
        public void EnsureDirectoryExists_ShouldReturnTrue_WhenDirectoryExists()
        {
            var directoryName = "TestDirectory";
            
            _mockDirectoryProvider.Setup(m => m.EnsureDirectoryExists(directoryName))
                .Returns(true);
            
            var result = _directoryService.EnsureDirectoryExists(directoryName);
            
            Assert.That(result, Is.True);
        }

        [Test]
        public void EnsureDirectoryExists_ShouldReturnFalse_WhenDirectoryDoesNotExist()
        {
            var directoryName = "NonExistentDirectory";
            
            _mockDirectoryProvider.Setup(m => m.EnsureDirectoryExists(directoryName))
                .Returns(false);
            
            var result = _directoryService.EnsureDirectoryExists(directoryName);
            
            Assert.That(result, Is.False);
        }

        [Test]
        public void CreateDirectory_ShouldReturnTrue_WhenDirectoryIsCreatedSuccessfully()
        {
            var directoryName = "NewDirectory";
            
            _mockDirectoryProvider.Setup(m => m.CreateDirectory(directoryName))
                .Returns(true);
            
            var result = _directoryService.CreateDirectory(directoryName);
            
            Assert.That(result, Is.True);
        }

        [Test]
        public void CreateDirectory_ShouldReturnFalse_WhenDirectoryCreationFails()
        {
            var directoryName = "InvalidDirectory";
            
            _mockDirectoryProvider.Setup(m => m.CreateDirectory(directoryName))
                .Returns(false);
            
            var result = _directoryService.CreateDirectory(directoryName);
            
            Assert.That(result, Is.False);
        }

        [Test]
        public void ReadDirectoryContent_ShouldReturnContent_WhenDirectoryHasFiles()
        {
            var directoryName = "TestDirectory";
            var files = new List<FileSystemInfo>
            {
                new FileInfo("file1.txt"),
                new FileInfo("file2.txt")
            };
            
            _mockDirectoryProvider.Setup(m => m.ReadDirectoryContent(directoryName))
                .Returns(files);
            
            var result = _directoryService.ReadDirectoryContent(directoryName);
            
            Assert.That(result, Is.EqualTo(files));
        }

        [Test]
        public void ReadDirectoryContent_ShouldReturnEmpty_WhenDirectoryHasNoFiles()
        {
            var directoryName = "EmptyDirectory";
            var files = new List<FileSystemInfo>();
            
            _mockDirectoryProvider.Setup(m => m.ReadDirectoryContent(directoryName))
                .Returns(files);
            
            var result = _directoryService.ReadDirectoryContent(directoryName);
            
            Assert.That(result, Is.EqualTo(files));
        }

        [Test]
        public void DeleteDirectory_ShouldReturnTrue_WhenDirectoryIsDeletedSuccessfully()
        {
            var directoryName = "TestDirectory";
            
            _mockDirectoryProvider.Setup(m => m.DeleteDirectory(directoryName))
                .Returns(true);
            
            var result = _directoryService.DeleteDirectory(directoryName);
            
            Assert.That(result, Is.True);
        }

        [Test]
        public void DeleteDirectory_ShouldReturnFalse_WhenDirectoryDeletionFails()
        {
            var directoryName = "NonExistentDirectory";
            
            _mockDirectoryProvider.Setup(m => m.DeleteDirectory(directoryName))
                .Returns(false);
            
            var result = _directoryService.DeleteDirectory(directoryName);

            Assert.That(result, Is.False);
        }
    }
}
