using Bliki.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Bliki.Test
{
    [TestClass]
    public class EditorTests
    {
        [TestMethod]
        public void AddBoldToSelection()
        {
            // Arrange
            var editManager = new MarkdownEditorManager();

            // Act
            var result = editManager.ToggleMarker(TextMarkers.Bold, "Hello with some bold text!", 16, 20);

            // Assert
            Assert.AreEqual("Hello with some **bold** text!", result.Content);
        }

        [TestMethod]
        public void AddBoldFailsIfAlreadySet()
        {
            // Arrange
            var editManager = new MarkdownEditorManager();

            // Act
            var result = editManager.ToggleMarker(TextMarkers.Bold, "**Hello with some bold text!**", 18, 22);

            // Assert
            Assert.AreNotEqual("**Hello with some **bold** text!**", result.Content);
            Assert.AreEqual("Hello with some bold text!", result.Content);
        }


        [TestMethod]
        public void RemoveBoldFromSelection()
        {
            // Arrange
            var editManager = new MarkdownEditorManager();

            // Act
            var result = editManager.ToggleMarker(TextMarkers.Bold, "Hello with some **bold** text!", 18, 22);

            // Assert
            Assert.AreEqual("Hello with some bold text!", result.Content);
        }


        [TestMethod]
        public void RemoveBoldFromRegionLargerThanSelection()
        {
            // Arrange
            var editManager = new MarkdownEditorManager();

            // Act
            var result = editManager.ToggleMarker(TextMarkers.Bold, "**Hello with some bold text!**", 18, 18);

            // Assert
            Assert.AreEqual("Hello with some bold text!", result.Content);
        }


        [TestMethod]
        public void RemoveBoldWithinSelection()
        {
            // Arrange
            var editManager = new MarkdownEditorManager();

            // Act
            var result = editManager.ToggleMarker(TextMarkers.Bold, "Hello with some **bold** text!", 0, 30);

            // Assert
            Assert.AreEqual("Hello with some bold text!", result.Content);
        }


        [TestMethod]
        public void RemoveOddBoldMarkers()
        {
            // Arrange
            var editManager = new MarkdownEditorManager();

            // Act
            var result = editManager.ToggleMarker(TextMarkers.Bold, "Hello with some **bold text!", 0, 30);

            // Assert
            Assert.AreEqual("Hello with some bold text!", result.Content);
        }


        [TestMethod]
        public void ToggleDoesNotAffectPreviousBoldSections()
        {
            // Arrange
            var editManager = new MarkdownEditorManager();

            // Act
            var result = editManager.ToggleMarker(TextMarkers.Bold, "**Hello** with some bold text!", 20, 24);

            // Assert
            Assert.AreEqual("**Hello** with some **bold** text!", result.Content);
        }


        [TestMethod]
        public void ToggleWithCursorAtEndOfBlockExitsBlock()
        {
            // Arrange
            var editManager = new MarkdownEditorManager();

            // Act
            var result = editManager.ToggleMarker(TextMarkers.Bold, "**Hello with some bold text!**", 28, 28);

            // Assert
            Assert.AreEqual("**Hello with some bold text!**", result.Content);
            Assert.AreEqual(2, result.Offset);
        }
    }
}
