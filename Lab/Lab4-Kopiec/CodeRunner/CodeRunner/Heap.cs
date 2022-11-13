using CodeRunner;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        list = new List<T>(collection);
        switch (option)
        {
            case HeapOptions.MaxHeap:
                list.Sort((a, b) => b.CompareTo(a));
                break;

            case HeapOptions.MinHeap:
                list.Sort((a, b) => a.CompareTo(b));
                break;
        }
    }

    public int Count => list.Count;

    public void Insert(T x)
    {
        list.Add(x);

        throw new NotImplementedException();
    }

    public T Delete()
    {
        throw new NotImplementedException();
    }

    public T Top()
    {
        throw new NotImplementedException();
    }

    public void Clear()
    {
        throw new NotImplementedException();
    }

    public T[] ToArray()
    {
        throw new NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    // TODO: próba odczytu elementu lub usunięcia elementu topowego z kopca pustego powoduje zgłoszenie wyjątku InvalidOperationException.
}