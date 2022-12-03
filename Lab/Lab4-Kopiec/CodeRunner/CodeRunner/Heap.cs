using CodeRunner;

using System;
using System.Collections;
using System.Collections.Generic;

public class Heap<T> : IEnumerable<T> where T : IComparable<T>
{
    private List<T> list;
    public HeapOptions Option { get; }

    public Heap(HeapOptions option = HeapOptions.MinHeap)
    {
        this.Option = option;
        list = new List<T>();
    }

    public Heap(IEnumerable<T> collection, HeapOptions option = HeapOptions.MinHeap)
    {
        this.Option = option;
        list = new List<T>();
        foreach (var item in collection)
        {
            Insert(item);
        }
    }

    public int Count => list.Count;

    private void CheckParent(int index)
    {
        var parentIndex = (index - 1) / 2;
        switch (Option)
        {
            case HeapOptions.MaxHeap:
                while (list[index].CompareTo(list[parentIndex]) > 0)
                {
                    if (parentIndex > Count - 1 || parentIndex < 0)
                        break;

                    var temp = list[parentIndex];
                    list[parentIndex] = list[index];
                    list[index] = temp;
                    index = parentIndex;
                    parentIndex = (index - 1) / 2;
                }
                break;

            case HeapOptions.MinHeap:
                while (list[index].CompareTo(list[parentIndex]) < 0)
                {
                    if (parentIndex > Count - 1 || parentIndex < 0)
                        break;

                    var temp = list[parentIndex];
                    list[parentIndex] = list[index];
                    list[index] = temp;
                    index = parentIndex;
                    parentIndex = (index - 1) / 2;
                }
                break;
        }
    }

    private void CheckChild(int index)
    {
        var firstChildIndex = (2 * index) + 1;
        var secondChildIndex = (2 * index) + 2;

        switch (Option)
        {
            case HeapOptions.MaxHeap:
                var biggerIndex = list[firstChildIndex].CompareTo(list[secondChildIndex]) > 0 ? firstChildIndex : secondChildIndex;
                while (list[index].CompareTo(list[biggerIndex]) < 0)
                {
                    T temp = list[biggerIndex];
                    list[biggerIndex] = list[index];
                    list[index] = temp;
                    index = biggerIndex;

                    firstChildIndex = (2 * index) + 1;
                    secondChildIndex = (2 * index) + 2;
                    if (firstChildIndex > Count - 1 || secondChildIndex > Count - 1)
                        break;
                    biggerIndex = list[firstChildIndex].CompareTo(list[secondChildIndex]) > 0 ? firstChildIndex : secondChildIndex;
                }
                break;

            case HeapOptions.MinHeap:
                var smallerIndex = list[firstChildIndex].CompareTo(list[secondChildIndex]) < 0 ? firstChildIndex : secondChildIndex;
                while (list[index].CompareTo(list[smallerIndex]) > 0)
                {
                    var temp = list[smallerIndex];
                    list[smallerIndex] = list[index];
                    list[index] = temp;
                    index = smallerIndex;

                    firstChildIndex = (2 * index) + 1;
                    secondChildIndex = (2 * index) + 2;
                    if (firstChildIndex > Count - 1 || secondChildIndex > Count - 1)
                        break;
                    smallerIndex = list[firstChildIndex].CompareTo(list[secondChildIndex]) < 0 ? firstChildIndex : secondChildIndex;
                }
                break;
        }
    }


    public void Insert(T x)
    {
        list.Add(x);
        var index = list.LastIndexOf(x);
        CheckParent(index);
    }

    public T Delete()
    {
        var elementToDelete = Top();
        var lastIndex = list.Count - 1;
        var lastElement = list[lastIndex];

        var temp = lastElement;
        list[lastIndex] = elementToDelete;
        list[0] = temp;
        list.RemoveAt(lastIndex);

        CheckChild(0);
        
        return elementToDelete;
    }

    public T Top() => list.Count > 0 ? list[0] : throw new InvalidOperationException("Heap is empty.");

    public void Clear() => list.Clear();

    public T[] ToArray() => list.ToArray();

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in list)
        {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
