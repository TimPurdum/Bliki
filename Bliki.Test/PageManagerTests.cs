using Bliki.Data;
using Bliki.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;

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
            var pageModel = manager.LoadPage("test-page-2");

            // Assert
            Assert.IsNotNull(pageModel);
            Assert.AreEqual("Test Page 2", pageModel.Title);
            Assert.AreEqual("SubTitle Page 2", pageModel.SubTitle);
            Assert.AreEqual("This is load test content", pageModel.Content);
            Assert.AreEqual("test-page-2", pageModel.PageLink);

            // Cleanup
            File.Delete(savePath);
        }
    }
}
