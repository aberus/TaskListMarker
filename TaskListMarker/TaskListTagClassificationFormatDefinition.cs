using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Aberus.TaskListMarker
{
    /// <summary>
    /// Set the display values for the classification
    /// </summary>
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "TaskList")]
    [Name("TaskListTagText")]
    [UserVisible(false)]
    [Order(After = Priority.High)]
    internal sealed class TaskListTagClassificationFormatDefinition : ClassificationFormatDefinition
    {
        public TaskListTagClassificationFormatDefinition()
        {
            this.DisplayName = "ToDo Text";
            this.IsBold = true;
        }
    }
}
