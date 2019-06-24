using Microsoft.VisualStudio.Text.Classification;

namespace Aberus.TaskListMarker
{
    internal static class ClassificationSpanExtensions
    {
        public static bool IsComment(this ClassificationSpan span)
        {
            return span.ClassificationType.Classification.ContainsCaseIgnored("comment");
        }
    }
}
