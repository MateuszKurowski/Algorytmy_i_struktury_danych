using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Multizbior
{
    public class MultiSet<T> : IMultiSet<T>,  IEquatable<MultiSet<T>>
    {
        private readonly Dictionary<T, int> mset = new();

        public MultiSet() { }

        public MultiSet(IEnumerable<T> data) => AddElementsFromIEnumerable(data);

        public MultiSet(IEqualityComparer<T> comparer) => _comparer = comparer;

        public MultiSet(IEnumerable<T> sequence, IEqualityComparer<T> comparer)
        {
            AddElementsFromIEnumerable(sequence);
            _comparer = comparer;
        }

        public static IMultiSet<T> operator +(MultiSet<T> first, IMultiSet<T> second) // One of the parameters of a binary operator must be the containing type
        {
            if (first == null || second == null) throw new ArgumentNullException();

            var multiset = new MultiSet<T>();
            multiset.UnionWith(first);
            multiset.UnionWith(second);
            return multiset;
        }

        public static IMultiSet<T> operator -(MultiSet<T> first, IMultiSet<T> second)
        {
            if (first == null || second == null) throw new ArgumentNullException();

            var multiset = new MultiSet<T>();
            multiset.UnionWith(first);
            multiset.ExceptWith(second);
            return multiset;
        }

        public static IMultiSet<T> operator *(MultiSet<T> first, IMultiSet<T> second)
        {
            if (first == null || second == null) throw new ArgumentNullException();

            var multiset = new MultiSet<T>();
            multiset.UnionWith(first);
            multiset.IntersectWith(second);
            return multiset;
        }

        public T this[int index]
        {
            get
            {
                var currentIndex = 0;
                foreach (var (item, multiplicity) in mset)
                    for (int i = 0; i < multiplicity; i++)
                    {
                        if (currentIndex == index)
                            return item;
                        currentIndex++;
                    }
                throw new KeyNotFoundException();
            }
        }

        public int Count
        {
            get
            {
                var count = 0; 
                foreach (var keyValuePair in mset)
                {
                    count += keyValuePair.Value;
                } 
                return count;
            }
        }

        private bool _isReadOnly = false;

        public bool IsReadOnly => _isReadOnly;

        public void SealedMultiSet() => _isReadOnly = true;

        public bool IsEmpty => mset.Count < 1;

        private IEqualityComparer<T> _comparer = EqualityComparer<T>.Default;

        public IEqualityComparer<T> Comparer => _comparer;

        public int this[T item] => mset[item];

        public void Add(T item)
        {
            if (IsReadOnly) throw new NotSupportedException();

            if (Contains(item))
                mset[item]++;
            else mset.Add(item, 1);
        }

        public bool Remove(T item)
        {
            if (IsReadOnly) throw new NotSupportedException();

            if (!Contains(item))
                return false;
            if (mset[item] > 1)
                mset[item]--;
            else
                mset.Remove(item);
            return true;
        }

        public void Clear()
        {
            if (IsReadOnly) throw new NotSupportedException();

            mset.Clear();
        }

        public bool Contains(T item) => mset.ContainsKey(item);

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null) throw new ArgumentNullException("array");

            var j = 0;
            for (int i = arrayIndex; i < Count; i++)
            {
                array[j] = this[i];
                j++;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var (item, multiplicity) in mset)
                for (int i = 0; i < multiplicity; i++)
                    yield return item;
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString()
        {
            var sb = new StringBuilder();

            foreach (var (item, multiplicity) in mset)
            {
                sb.Append($"{item}: {multiplicity}, ");
            }

            return sb.ToString(0, sb.Length - 2);
        }

        public string ToStringExpanded()
        {
            var sb = new StringBuilder();

            foreach (var (item, multiplicity) in mset)
            {
                for (int i = 0; i < multiplicity; i++)
                {
                    sb.Append($"{item}, ");
                }
            }

            return sb.ToString(0, sb.Length - 2);
        }

        public MultiSet<T> Add(T item, int numberOfItems = 1)
        {
            if (IsReadOnly) throw new NotSupportedException();

            if (!mset.ContainsKey(item)) mset.Add(item, numberOfItems);
            else mset[item] += numberOfItems;
            return this;
        }

        public MultiSet<T> Remove(T item, int numberOfItems = 1)
        {
            if (IsReadOnly) throw new NotSupportedException();

            if (mset.ContainsKey(item))
            {
                mset[item] -= numberOfItems;
                if (mset[item] < 1) mset.Remove(item);
            }
            return this;
        }

        public MultiSet<T> RemoveAll(T item)
        {
            if (IsReadOnly) throw new NotSupportedException();

            if (mset.ContainsKey(item))
                mset.Remove(item);
            return this;
        }

        public MultiSet<T> UnionWith(IEnumerable<T> other)
        {
            CheckIEnumerable(other);

            foreach (var element in other)
            {
                if (mset.ContainsKey(element)) mset[element]++;
                else Add(element);
            }

            return this;
        }

        public MultiSet<T> IntersectWith(IEnumerable<T> other)
        {
            CheckIEnumerable(other);
            other = other.Distinct();

            foreach (var element in mset)
            {
                if (!other.Contains(element.Key))
                    RemoveAll(element.Key);
                else Add(element.Key);
            }
            return this;
        }

        public MultiSet<T> ExceptWith(IEnumerable<T> other)
        {
            CheckIEnumerable(other);

            foreach (var element in other)
                if (mset.ContainsKey(element))
                    Remove(element);
            return this;
        }

        public MultiSet<T> SymmetricExceptWith(IEnumerable<T> other)
        {
            CheckIEnumerable(other);

            foreach (var element in other)
            {
                if (mset.ContainsKey(element))
                    Remove(element);
                else Add(element);
            }
            return this;
        }

        public bool IsSubsetOf(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException();

            foreach (var element in mset)
                if (!other.Contains(element.Key))
                    return false;
            return true;
        }

        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException();

            if (other.Count() <= mset.Count) return false;

            return IsSubsetOf(other);
        }

        public bool IsSupersetOf(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException();

            if (other.Count() > mset.Count) return false;

            foreach (var element in other)
                if (!mset.ContainsKey(element))
                    return false;

            return true;
        }

        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException();

            if (other.Count() >= mset.Count) return false;

            return IsSupersetOf(other);
        }

        public bool Overlaps(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException();

            foreach (var element in other)
                if (mset.ContainsKey(element))
                    return true;
            return false;
        }

        public bool MultiSetEquals(IEnumerable<T> other)
        {
            var tempMultiSet = new MultiSet<T>(other);
            return Equals(tempMultiSet);
        }

        public IReadOnlyDictionary<T, int> AsDictionary() => mset;

        public IReadOnlySet<T> AsSet()
        {
            var set = new HashSet<T>();
            foreach (var element in mset)
            {
                set.Add(element.Key);
            }
            return set;
        }

        private void CheckIEnumerable(IEnumerable<T> other)
        {
            if (other == null) throw new ArgumentNullException();
            if (IsReadOnly) throw new NotSupportedException();
        }

        private void AddElementsFromIEnumerable(IEnumerable<T> data)
        {
            foreach (var element in data)
            {
                Add(element);
            }
        }

        public bool Equals(MultiSet<T> other)
        {
            if (other == null) return false;

            if (other.mset == null && mset == null) return true;
            if (other.mset == null) return false;

            return mset.OrderBy(x => x.Key)
                        .SequenceEqual(other.mset
                        .OrderBy(x => x.Key));
        }

        public override bool Equals(object obj) => Equals(obj as MultiSet<T>);

        public override int GetHashCode() => mset.GetHashCode();
    }
}