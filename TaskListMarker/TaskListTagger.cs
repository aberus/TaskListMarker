using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text.Classification;
#if !MAC
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
#else
using MonoDevelop.Ide.Tasks;
#endif

namespace Aberus.TaskListMarker
{
    /// <summary>
    /// This class implements <see cref="ITagger{}"/> for <see cref="TaskListTag"/>.  It is responsible for creating
    /// <see cref="TaskListTag"/> <see cref="TagSpan{}"/>, which our <see cref="TaskListTagGlyphFactory"/> will then create glyphs for.
    /// </summary>
    internal class TaskListTagger : ITagger<TaskListTag>
    {
        private readonly IClassificationTypeRegistryService registry;
        private IReadOnlyList<string> tokens;

#if MAC
        public TaskListTagger(ITextBuffer buffer, IClassificationTypeRegistryService registry) : this(buffer)
        {
            LoadTokens();
            CommentTag.SpecialCommentTagsChanged += (s,e) => LoadTokens();
            this.registry = registry;
        }

        private void LoadTokens()
        {
            tokens = CommentTag.SpecialCommentTags.Select(x => x.Tag).ToReadOnlyList();
        }
#else
        public TaskListTagger(ITextBuffer buffer, IClassificationTypeRegistryService registry, DTE2 dte) : this(buffer)
        {
            LoadTokens(dte);
            this.registry = registry;
        }


        private TaskListTagger(ITextBuffer buffer)
        {
            buffer.Changed += (sender, args) => OnBufferChanged(args);
        }

        private void LoadTokens(DTE2 dte)
        {
            var toolWindows = dte.ToolWindows;
            var taskList = dte.ToolWindows.TaskList;
            var commentTokens = taskList.GetType().GetProperty("CommentTokens");
            var commentTaskTokens = commentTokens.GetValue(taskList, null) as IReadOnlyList<ICommentTaskToken>;
            tokens = commentTaskTokens.Select(x => x.Text).ToReadOnlyList();
        }
#endif

        /// <summary>
        /// This method creates <see cref="TaskListTag"/> <see cref="TagSpan{}"/>s over a set of <see cref="SnapshotSpan"/>s.
        /// </summary>
        /// <param name="spans">A set of spans we want to get tags for.</param>
        /// <returns>The list of <see cref="TaskListTag"/> TagSpans.</returns>
        IEnumerable<ITagSpan<TaskListTag>> ITagger<TaskListTag>.GetTags(NormalizedSnapshotSpanCollection spans)
        {
            foreach (var line in GetIntersectingLines(spans))
            {
                foreach (var token in tokens)
                {
                    var todoLineRegex = new Regex($@"\/\/\s*{token}\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);

                    string text = line.GetText();
                    var match = todoLineRegex.Match(text);
                    if (match.Success)
                    {
                        var todoSpan = new SnapshotSpan(line.Snapshot, new Span(line.Start + match.Index, line.Length));
                        yield return new TagSpan<TaskListTag>(todoSpan, new TaskListTag(registry.GetClassificationType("TaskList")));
                    }
                }
            }
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;

        IEnumerable<ITextSnapshotLine> GetIntersectingLines(NormalizedSnapshotSpanCollection spans)
        {
            if (spans.Count == 0)
                yield break;

            int lastVisitedLineNumber = -1;
            var snapshot = spans[0].Snapshot;

            foreach (var span in spans)
            {
                int firstLineNumber = snapshot.GetLineNumberFromPosition(span.Start);
                int lastLineNumber = snapshot.GetLineNumberFromPosition(span.End);

                for (int i = Math.Max(lastVisitedLineNumber, firstLineNumber); i <= lastLineNumber; i++)
                {
                    yield return snapshot.GetLineFromLineNumber(i);
                }

                lastVisitedLineNumber = lastLineNumber;
            }
        }

        /// <summary>
        /// Handle buffer changes. The default implementation expands changes to full lines and sends out
        /// a <see cref="TagsChanged"/> event for these lines.
        /// </summary>
        /// <param name="args">The buffer change arguments.</param>
        protected virtual void OnBufferChanged(TextContentChangedEventArgs args)
        {
            var snapshot = args.After;

            foreach (var change in args.Changes)
            {
                var startLine = snapshot.GetLineFromPosition(change.NewPosition);
                var endLine = (change.NewEnd <= startLine.End) ? startLine : snapshot.GetLineFromPosition(change.NewEnd);

                TagsChanged?.Invoke(this, new SnapshotSpanEventArgs(new SnapshotSpan(startLine.Start, endLine.End)));
            }
        }
    }
}
