using Bliki.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Bliki.Test
{
    [TestClass]
    public class EditorTests
    {
        [TestMethod]
        public void AddInlineMarkersToSelection()
        {
            // Arrange
            var editManager = new MarkdownEditorManager();

            foreach (var marker in _inlineMarkers)
            {
                // Act
                var result = editManager.ToggleMarker(marker, "Hello with some bold text!", 16, 20);

                // Assert
                Assert.AreEqual($"Hello with some {marker.Value}bold{marker.Value} text!",
                    result.Content);
            }
        }


        [TestMethod]
        public void AddInlineMarkerFailsIfAlreadySet()
        {
            // Arrange
            var editManager = new MarkdownEditorManager();

            foreach (var marker in _inlineMarkers)
            {
                // Act
                var result = editManager.ToggleMarker(marker,
                    $"{marker.Value}Hello with some bold text!{marker.Value}", 18, 22);

                // Assert
                Assert.AreNotEqual(
                    $"{marker.Value}Hello with some {marker.Value}bold{marker.Value} text!{marker.Value}",
                    result.Content);
                Assert.AreEqual("Hello with some bold text!", result.Content);
            }
        }


        [TestMethod]
        public void RemoveInlineMarkerFromSelection()
        {
            // Arrange
            var editManager = new MarkdownEditorManager();

            foreach (var marker in _inlineMarkers)
            {
                var offset = marker.Value.Length;

                // Act
                var result = editManager
                    .ToggleMarker(marker,
                        $"Hello with some {marker.Value}bold{marker.Value} text!", 16 + offset,
                        20 + offset);

                // Assert
                Assert.AreEqual("Hello with some bold text!", result.Content);
            }
        }


        [TestMethod]
        public void RemoveInlineMarkerFromRegionLargerThanSelection()
        {
            // Arrange
            var editManager = new MarkdownEditorManager();

            foreach (var marker in _inlineMarkers)
            {
                var offset = marker.Value.Length;

                // Act
                var result = editManager
                    .ToggleMarker(marker,
                        $"{marker.Value}Hello with some bold text!{marker.Value}", 16 + offset,
                        16 + offset);

                // Assert
                Assert.AreEqual("Hello with some bold text!", result.Content);
            }
        }


        [TestMethod]
        public void RemoveInlineMarkerWithinSelection()
        {
            // Arrange
            var editManager = new MarkdownEditorManager();

            foreach (var marker in _inlineMarkers)
            {
                var offset = marker.Value.Length;

                // Act
                var result = editManager
                    .ToggleMarker(marker,
                        $"Hello with some {marker.Value}bold{marker.Value} text!", 0,
                        26 + offset * 2);

                // Assert
                Assert.AreEqual("Hello with some bold text!", result.Content);
            }
        }


        [TestMethod]
        public void RemoveOddInlineMarkers()
        {
            // Arrange
            var editManager = new MarkdownEditorManager();

            foreach (var marker in _inlineMarkers)
            {
                var offset = marker.Value.Length;

                // Act
                var result = editManager
                    .ToggleMarker(marker,
                        $"Hello with some {marker.Value}bold text!", 0, 26 + offset);

                // Assert
                Assert.AreEqual("Hello with some bold text!", result.Content);
            }
        }


        [TestMethod]
        public void ToggleDoesNotAffectPreviousMarkedSections()
        {
            // Arrange
            var editManager = new MarkdownEditorManager();

            foreach (var marker in _inlineMarkers)
            {
                var offset = marker.Value.Length;

                // Act
                var result = editManager
                    .ToggleMarker(marker,
                        $"{marker.Value}Hello{marker.Value} with some bold text!", 16 + offset * 2,
                        20 + offset * 2);

                // Assert
                Assert.AreEqual(
                    $"{marker.Value}Hello{marker.Value} with some {marker.Value}bold{marker.Value} text!",
                    result.Content);
            }
        }


        [TestMethod]
        public void ToggleWithCursorAtEndOfBlockExitsBlock()
        {
            // Arrange
            var editManager = new MarkdownEditorManager();

            foreach (var marker in _inlineMarkers)
            {
                var offset = marker.Value.Length;

                // Act
                var result = editManager
                    .ToggleMarker(marker,
                        $"{marker.Value}Hello with some bold text!{marker.Value}", 26 + offset,
                        26 + offset);

                // Assert
                Assert.AreEqual($"{marker.Value}Hello with some bold text!{marker.Value}",
                    result.Content);
                Assert.AreEqual(offset, result.Offset);
            }
        }


        private readonly TextMarker[] _inlineMarkers =
        {
            TextMarkers.Bold, TextMarkers.Italic, TextMarkers.Strikethrough, TextMarkers.InlineCode
        };
    }
}