using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Aberus.TaskListMarker
{
    internal static class TaskListTagsClassificationDefinition
    {
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("TaskList")]
        internal static ClassificationTypeDefinition tasklist;
    }
}
