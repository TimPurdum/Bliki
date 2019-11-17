using Bliki.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Management.Automation;

namespace Bliki.Test
{
    [TestClass]
    public class PageManagerTests
    {
        [TestMethod]
        public void CanSave()
        {
            var manager = new PageManager();

            var testPageModel = new WikiPageModel
            {
                Content = "This is test content",
                Title = "Test Page 1"
            };

            var result = manager.SavePage(testPageModel);
            Assert.IsTrue(result);
            Assert.AreEqual("test-page-1", testPageModel.PageLink);
        }
    }
}
