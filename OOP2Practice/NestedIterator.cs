using System.Collections;

namespace OOP2Practice;

public class Nested<T>: IEnumerable<T> {
    private T? _object;
    private IEnumerator<Nested<T>>? _iterator;
    private bool _isObject;

    public Nested(T obj) {
        _isObject = true;
        _object = obj;
        _iterator = null;
    }

    public Nested(IEnumerable<Nested<T>> enumerable) {
        _isObject = false;
        _object = default(T);
        _iterator = enumerable.GetEnumerator();
    }

    public Nested(IEnumerator<Nested<T>> iterator) {
        _isObject = false;
        _object = default(T);
        _iterator = iterator;
    }

    public bool IsObject() => _isObject;

    public T AsObject() => _isObject
        ? _object!
        : throw new InvalidOperationException();

    public IEnumerator<Nested<T>> AsIterator() => _isObject
        ? throw new InvalidOperationException()
        : _iterator!;

    public IEnumerator<T> GetEnumerator() =>
        new NestedEnumerator<T>(this);

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class NestedEnumerator<T>: IEnumerator<T> {
    private Stack<Nested<T>> _nestedStack;
    private Nested<T> _start;
    private bool _movedNextFirst = false;

    public NestedEnumerator(Nested<T> start) {
        _start = start;
        _nestedStack = new Stack<Nested<T>>();
        _nestedStack.Push(start);
    }

    private void ExpandFrontier() {
        /* Expand all iterators on top of the stack
         * until an object appears on the top of the stack.
         */
        while (_nestedStack.Count > 0 && !_nestedStack.Peek().IsObject()) {
            IEnumerator<Nested<T>> iterator = _nestedStack.Pop().AsIterator();
            /* Put the contents of iterator into the stack
             * in the reversed order using another stack
             * so that the first Nested<T> of iterator
             * will be next to be expanded (if it's another
             * iterator) or returned as an object.
             */
            Stack<Nested<T>> reversedContents = new Stack<Nested<T>>();
            while (iterator.MoveNext()) {
                reversedContents.Push(iterator.Current);
            }
            while (reversedContents.Count > 0) {
                _nestedStack.Push(reversedContents.Pop());
            }
        }
    }
    
    public bool MoveNext() {
        if (_nestedStack.Count == 0)
            return false;
        if (_movedNextFirst)
            _nestedStack.Pop();
        _movedNextFirst = true;
        ExpandFrontier();
        return _nestedStack.Count > 0;
    }

    public void Reset() {
        _nestedStack.Clear();
        _nestedStack.Push(_start);
        _movedNextFirst = false;
    }

    public T Current => _nestedStack.Peek().AsObject();

    object IEnumerator.Current => Current!;

    public void Dispose() { }
}