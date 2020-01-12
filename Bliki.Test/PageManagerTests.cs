using Bliki.Data;
using Bliki.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using System.Linq;

namespace Bliki.Test
{
    [TestClass]
    public class PageManagerTests
    {
        [TestMethod]
        public void CanSave()
        {
            // Arrange
            var gitMock = new Mock<IGitManager>();
            var manager = new PageManager(gitMock.Object, @"..\..\..\WikiPages");

            var testPageModel = new WikiPageModel
            {
                Content = "This is test content",
                Title = "Test Page 1",
                SubTitle = "SubTitle Page 1"
            };
            var savePath = @"..\..\..\WikiPages\test-page-1.md";

            // Act
            var result = manager.SavePage(testPageModel, "test");

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("test-page-1", testPageModel.PageLink);
            Assert.IsTrue(File.Exists(savePath));
            Assert.AreEqual($@"<!-- TITLE: Test Page 1 -->
<!-- SUBTITLE: SubTitle Page 1 -->
This is test content", 
                File.ReadAllText(savePath));
            
            // Cleanup
            File.Delete(savePath);
        }


        [TestMethod]
        public void CanLoad()
        {
            // Arrange
            var gitMock = new Mock<IGitManager>();
            var manager = new PageManager(gitMock.Object, @"..\..\..\WikiPages");

            var savePath = @"..\..\..\WikiPages\test-page-2.md";
            var content = $@"<!-- TITLE: Test Page 2 -->
<!-- SUBTITLE: SubTitle Page 2 -->
This is load test content";
            File.WriteAllText(savePath, content);

            // Act
            var pageModel = manager.LoadPage("test-page-2", null);

            // Assert
            Assert.IsNotNull(pageModel);
            Assert.AreEqual("Test Page 2", pageModel.Title);
            Assert.AreEqual("SubTitle Page 2", pageModel.SubTitle);
            Assert.AreEqual("This is load test content", pageModel.Content);
            Assert.AreEqual("test-page-2", pageModel.PageLink);

            // Cleanup
            File.Delete(savePath);
        }


        [TestMethod]
        public void CanSaveToFolder()
        {
            // Arrange
            var gitMock = new Mock<IGitManager>();
            var manager = new PageManager(gitMock.Object, @"..\..\..\WikiPages");

            var testPageModel = new WikiPageModel
            {
                Content = "This is test content",
                Title = "Test Page 1",
                SubTitle = "SubTitle Page 1",
                Folder = "SubFolder1"
            };
            var savePath = @"..\..\..\WikiPages\SubFolder1\test-page-1.md";

            // Act
            var result = manager.SavePage(testPageModel, "test");

            // Assert
            Assert.IsTrue(result);
            Assert.AreEqual("test-page-1", testPageModel.PageLink);
            Assert.IsTrue(File.Exists(savePath));
            Assert.AreEqual($@"<!-- TITLE: Test Page 1 -->
<!-- SUBTITLE: SubTitle Page 1 -->
This is test content",
                File.ReadAllText(savePath));

            // Cleanup
            File.Delete(savePath);
            Directory.Delete(Path.GetDirectoryName(savePath));
        }


        [TestMethod]
        public void CanLoadFromFolder()
        {
            // Arrange
            var gitMock = new Mock<IGitManager>();
            var manager = new PageManager(gitMock.Object, @"..\..\..\WikiPages");
            Directory.CreateDirectory(@"..\..\..\WikiPages\SubFolder2");
            var savePath = @"..\..\..\WikiPages\SubFolder2\test-page-3.md";
            var content = $@"<!-- TITLE: Test Page 3 -->
<!-- SUBTITLE: SubTitle Page 3 -->
This is load test content";
            File.WriteAllText(savePath, content);

            // Act
            var pageModel = manager.LoadPage("test-page-3", "SubFolder2");

            // Assert
            Assert.IsNotNull(pageModel);
            Assert.AreEqual("Test Page 3", pageModel.Title);
            Assert.AreEqual("SubTitle Page 3", pageModel.SubTitle);
            Assert.AreEqual("This is load test content", pageModel.Content);
            Assert.AreEqual("test-page-3", pageModel.PageLink);
            Assert.AreEqual("SubFolder2", pageModel.Folder);

            // Cleanup
            File.Delete(savePath);
            Directory.Delete(Path.GetDirectoryName(savePath));
        }

        [TestMethod]
        public void LoadNavPageList()
        {
            // Arrange
            var gitMock = new Mock<IGitManager>();
            var manager = new PageManager(gitMock.Object, @"..\..\..\WikiPages");

            var savePath4 = @"..\..\..\WikiPages\test-page-4.md";
            var content = $@"<!-- TITLE: Test Page 4 -->
<!-- SUBTITLE: SubTitle Page 4 -->
This is load test content";
            File.WriteAllText(savePath4, content);
            Directory.CreateDirectory(@"..\..\..\WikiPages\SubFolder3");
            var savePath5 = @"..\..\..\WikiPages\SubFolder3\test-page-5.md";
            content = $@"<!-- TITLE: Test Page 5 -->
<!-- SUBTITLE: SubTitle Page 5 -->
This is load test content";
            File.WriteAllText(savePath5, content);

            // Act
            var result = manager.GetNavMenuMetas();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            var pg4 = result.FirstOrDefault(m => m.Title == "Test Page 4");
            Assert.IsNotNull(pg4);
            Assert.IsNull(pg4.Folder);
            Assert.AreEqual("test-page-4", pg4.PageLink);
            var pg5 = result.FirstOrDefault(m => m.Title == "Test Page 5");
            Assert.IsNotNull(pg5);
            Assert.AreEqual("SubFolder3", pg5.Folder);
            Assert.AreEqual("test-page-5", pg5.PageLink);

            // Cleanup
            File.Delete(savePath4);
            File.Delete(savePath5);
            Directory.Delete(Path.GetDirectoryName(savePath5));
        }
    }
}
