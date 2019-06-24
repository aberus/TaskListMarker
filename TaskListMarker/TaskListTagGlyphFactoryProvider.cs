using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.Text.Tagging;
#if MAC
using Microsoft.VisualStudio.Core.Imaging;
#endif

namespace Aberus.TaskListMarker
{
    /// <summary>
    /// Export a <see cref="IGlyphFactoryProvider"/>
    /// </summary>
    [Export(typeof(IGlyphFactoryProvider))]
    [Name("TaskListGlyph")]
    [Order(Before = "VsTextMarker")]
    [ContentType("code")]
    [TagType(typeof(TaskListTag))]
    internal sealed class TaskListTagGlyphFactoryProvider : IGlyphFactoryProvider
    {
#if MAC
        [Import]
        private IImageService imageService;
#endif

        /// <summary>
        /// This method creates an instance of our custom glyph factory for a given text view.
        /// </summary>
        /// <param name="view">The text view we are creating a glyph factory for, we don't use this.</param>
        /// <param name="margin">The glyph margin for the text view, we don't use this.</param>
        /// <returns>An instance of our custom glyph factory.</returns>
#if !MAC
        public IGlyphFactory GetGlyphFactory(IWpfTextView view, IWpfTextViewMargin margin)
        {
            return new TaskListTagGlyphFactory();
        }
#else
        public IGlyphFactory GetGlyphFactory(ICocoaTextView view, ICocoaTextViewMargin margin)
        {
            return new TaskListTagGlyphFactory(imageService);
        }
#endif
    }
}
