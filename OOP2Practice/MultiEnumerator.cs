using System.Collections;

namespace OOP2Practice; 

public class MultiEnumerator<T>: IEnumerator<T> {
    private Queue<IEnumerator<T>> _iterators;

    public MultiEnumerator(IEnumerator<IEnumerator<T>> iterators) {
        _iterators = new Queue<IEnumerator<T>>();
        while (iterators.MoveNext())
            _iterators.Enqueue(iterators.Current);
    }

    public bool MoveNext() {
        while (_iterators.Count > 0 && !_iterators.Peek().MoveNext())
            _iterators.Dequeue();
        return _iterators.Count > 0;
    }

    public void Reset() {
        throw new NotSupportedException();
    }

    public T Current => _iterators.Peek().Current;

    object IEnumerator.Current => Current!;

    public void Dispose() {
        while (_iterators.Count > 0)
            _iterators.Dequeue();
    }
}