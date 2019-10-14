using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;

namespace Aberus.TaskListMarker
{
    /// <summary>
    /// ToDo tag classifier finds every instance of ToDoTag within a given span.
    /// </summary>
    class TaskListClassifier : IClassifier
    {
        readonly IClassificationType classificationType;
        ITagAggregator<TaskListTag> tagger;

        internal TaskListClassifier(ITagAggregator<TaskListTag> tagger, IClassificationType classificationType)
        {
            this.tagger = tagger;
            this.classificationType = classificationType;
        }

        /// <summary>
        /// Get every <see cref="TaskListTag"/> instance within the given span. Generally, the span in 
        /// question is the displayed portion of the file currently open in the Editor
        /// </summary>
        /// <param name="span">The span of text that will be searched for <see cref="TaskListTag"/>s</param>
        /// <returns>A list of every relevant tag in the given span</returns>
        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {           
            IList<ClassificationSpan> classifiedSpans = new List<ClassificationSpan>();

            var tags = tagger.GetTags(span);

            foreach (IMappingTagSpan<TaskListTag> tagSpan in tags)
            {
                SnapshotSpan todoSpan = tagSpan.Span.GetSpans(span.Snapshot).First();
                classifiedSpans.Add(new ClassificationSpan(todoSpan, classificationType));
            }

            return classifiedSpans;
        }

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
    }
}
