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
        public TaskListTagger(IClassificationTypeRegistryService registry)
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
        public TaskListTagger(IClassificationTypeRegistryService registry, DTE2 dte)
        {
            LoadTokens(dte);
            this.registry = registry;
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
            foreach (SnapshotSpan curSpan in spans)
            {
                foreach (var token in tokens)
                {
                    var todoLineRegex = new Regex($@"\/\/\s*{token}\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);

                    string text = curSpan.GetText();
                    var match = todoLineRegex.Match(text);
                    if (match.Success)
                    {
                        SnapshotSpan todoSpan = new SnapshotSpan(curSpan.Snapshot, new Span(curSpan.Start + match.Index, curSpan.Length-1));
                        yield return new TagSpan<TaskListTag>(todoSpan, new TaskListTag(registry.GetClassificationType("TaskList")));
                        //TagsChanged.Invoke(this, new SnapshotSpanEventArgs(todoSpan));
                    }
                }
            }
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;
    }
}
