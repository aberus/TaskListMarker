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
    /// Export a <see cref="IClassifierProvider"/>
    /// </summary>
    [Export(typeof(IClassifierProvider))]
    [ContentType("code")]
    internal class TaskListClassifierProvider : IClassifierProvider
    {
        //[Export(typeof(ClassificationTypeDefinition))]
        //[Name("todo")]
        //internal ClassificationTypeDefinition ToDoClassificationType = null;

        [Import]
        internal IClassificationTypeRegistryService classificationRegistry;

        [Import]
        internal IBufferTagAggregatorFactoryService tagAggregatorFactory;

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            IClassificationType classificationType = classificationRegistry.GetClassificationType("TaskList");

            var tagAggregator = tagAggregatorFactory.CreateTagAggregator<TaskListTag>(buffer);
            return new TaskListClassifier(tagAggregator, classificationType);
            //return buffer.Properties.GetOrCreateSingletonProperty(creator: () => new EditorClassifier1(this.classificationRegistry));

        }
    }
}
