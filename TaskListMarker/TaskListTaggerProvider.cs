using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
#if !MAC
using EnvDTE80;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
#endif

namespace Aberus.TaskListMarker
{
    /// <summary>
    /// Export a <see cref="ITaggerProvider"/>
    /// </summary>
    [Export(typeof(ITaggerProvider))]
    [ContentType("code")]
    [TagType(typeof(TaskListTag))]
    class TaskListTaggerProvider : ITaggerProvider
    {
#if !MAC
        [Import]
        internal SVsServiceProvider serviceProvider;
#endif

        [Import]
        private IClassificationTypeRegistryService registry;

        /// <summary>
        /// Creates an instance of our custom TodoTagger for a given buffer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="buffer">The buffer we are creating the tagger for.</param>
        /// <returns>An instance of our custom TodoTagger.</returns>
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

#if !MAC
            return buffer.Properties.GetOrCreateSingletonProperty(() => new TaskListTagger(buffer, registry, (DTE2)serviceProvider.GetService(typeof(SDTE))) as ITagger<T>);
#else
            return buffer.Properties.GetOrCreateSingletonProperty(() => new TaskListTagger(buffer, registry) as ITagger<T>);
#endif
        }
    }
}
