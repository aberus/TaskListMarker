using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;

namespace Aberus.TaskListMarker
{
    internal static class ListExtensions
    {
        /// <summary>
        /// View an <see cref="IList{T}"/> as a read-only list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IReadOnlyList<T> AsReadOnlyList<T>(this IList<T> source)
        {
            if (source == null)
                return null;
            return new ReadOnlyCollection<T>(source);
        }

        /// <summary>
        /// Creates a new read-only list from an <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IReadOnlyList<T> ToReadOnlyList<T>(this IEnumerable<T> source)
        {
            return source.ToList().AsReadOnlyList();
        }
    }
}
