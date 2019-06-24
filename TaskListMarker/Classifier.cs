using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text;
using System.Windows.Media;

namespace ToDoGlyphFactory
{
    //  /// <summary>
    ///// ToDo tag classifier finds every instance of ToDoTag within a given span.
    ///// </summary>
    //class ToDoClassifier : IClassifier
    //{
    //    IClassificationType _classificationType;
    //    ITagAggregator<ToDoTag> _tagger;

    //    internal ToDoClassifier(ITagAggregator<ToDoTag> tagger, IClassificationType todoType)
    //    {
    //        _tagger = tagger;
    //        _classificationType = todoType;
    //    }
        
    //    /// <summary>
    //    /// Get every ToDoTag instance within the given span. Generally, the span in 
    //    /// question is the displayed portion of the file currently open in the Editor
    //    /// </summary>
    //    /// <param name="span">The span of text that will be searched for ToDo tags</param>
    //    /// <returns>A list of every relevant tag in the given span</returns>
    //    public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
    //    {           
    //        IList<ClassificationSpan> classifiedSpans = new List<ClassificationSpan>();

    //        var tags = _tagger.GetTags(span);

    //        foreach (IMappingTagSpan<ToDoTag> tagSpan in tags)
    //        {
    //            SnapshotSpan todoSpan = tagSpan.Span.GetSpans(span.Snapshot).First();
    //            classifiedSpans.Add(new ClassificationSpan(todoSpan, _classificationType));
    //        }

    //        return classifiedSpans;
    //    }

    //    public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
    //}

}
