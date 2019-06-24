using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
#if !MAC
using System.Windows;
#else
using System;
using Microsoft.VisualStudio.Core.Imaging;
#endif

namespace Aberus.TaskListMarker
{
    /// <summary>
    /// This class implements IGlyphFactory, which provides the visual
    /// element that will appear in the glyph margin.
    /// </summary>
    internal class TaskListTagGlyphFactory : IGlyphFactory
    {
#if MAC          
        private IImageService imageService;

        public TaskListTagGlyphFactory(IImageService imageService)
        {
            this.imageService = imageService;
        }

        public object GenerateGlyph (ITextViewLine line, IGlyphTag tag)
        {
            if (tag is TaskListTag)
            {
                var imageId = new ImageId(new Guid("{ae27a6b0-e345-4288-96df-5eaf394ee369}"), 2852/*3063KnownImageIds.TaskList*/);
                var image = (AppKit.NSImage)imageService.GetImage(imageId);
                var imageView = AppKit.NSImageView.FromImage(image);
                imageView.SetFrameSize(imageView.FittingSize);
                return imageView;
            }

            return null;
        }
#else
        public UIElement GenerateGlyph(IWpfTextViewLine line, IGlyphTag tag)
        {
            if (tag is TaskListTag)
            {
                return new TaskListTagGlyph();
            }

            return null;
        }
#endif
    }
}
