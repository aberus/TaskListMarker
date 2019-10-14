using System;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Classification;
#if !MAC
using EnvDTE80;
#else
#endif

namespace Aberus.TaskListMarker
{
    /// <summary>
    /// <see cref="TaskListTag"/> class.
    /// </summary>
    internal class TaskListTag : /*IClassificationTag,*/ IGlyphTag
    {
        ///// <summary>
        ///// The classification type associated with this tag.
        ///// </summary>
        //public IClassificationType ClassificationType
        //{
        //    get;
        //    private set;
        //}

        /// <summary>
        /// Create a new tag associated with the given type of
        /// classification.
        /// </summary>
        ///// <param name="type">The type of classification</param>
        /// <exception cref="T:System.ArgumentNullException">If the type is passed in as null</exception>
        public TaskListTag(/*IClassificationType type*/)
        {
            //ClassificationType = type ?? throw new ArgumentNullException("type");
        }
    }
}
