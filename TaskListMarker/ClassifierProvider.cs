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
    ///// <summary>
    ///// Export a <see cref="IClassifierProvider"/>
    ///// </summary>
    //[Export(typeof(IClassifierProvider))]
    //[ContentType("code")]
    //internal class ToDoClassifierProvider : IClassifierProvider
    //{
    //    [Export(typeof(ClassificationTypeDefinition))]
    //    [Name("todo")]
    //    internal ClassificationTypeDefinition ToDoClassificationType = null;

    //    [Import]
    //    internal IClassificationTypeRegistryService ClassificationRegistry = null;

    //    [Import]
    //    internal IBufferTagAggregatorFactoryService _tagAggregatorFactory = null;

    //    public IClassifier GetClassifier(ITextBuffer buffer)
    //    {
    //        IClassificationType classificationType = ClassificationRegistry.GetClassificationType("todo");

    //        var tagAggregator = _tagAggregatorFactory.CreateTagAggregator<ToDoTag>(buffer);
    //        return new ToDoClassifier(tagAggregator, classificationType);
                //return buffer.Properties.GetOrCreateSingletonProperty(creator: () => new EditorClassifier1(this.classificationRegistry));

    //    }
    //}
}
