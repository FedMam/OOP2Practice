using System.Collections;

namespace OOP2Practice; 

public class MultiEnumerator<T>: IEnumerator<T> {
    private IEnumerator<IEnumerator<T>> _iterators;
    private bool _movedNextFirst = false;
    private bool _finished = false;

    public MultiEnumerator(IEnumerator<IEnumerator<T>> iterators) {
        _iterators = iterators;
    }

    public bool MoveNext() {
        if (_finished)
            return false;
        
        if (!_movedNextFirst) {
            _movedNextFirst = true;
            if (!_iterators.MoveNext()) {
                _finished = true;
                return false;
            }
        }
        
        while (!_iterators.Current.MoveNext())
            if (!_iterators.MoveNext()) {
                _finished = true;
                return false;
            }
        return true;
    }

    public void Reset() {
        throw new NotSupportedException();
    }

    public T Current {
        get {
            if (!_movedNextFirst)
                throw new InvalidOperationException("MultiEnumerator: Enumeration has not started. Call MoveNext.");
            if (_finished)
                throw new InvalidOperationException("MultiEnumerator: Enumeration already finished.");
            return _iterators.Current.Current;
        }
    }

    object IEnumerator.Current => Current!;

    public void Dispose() {
        if (!_movedNextFirst)
            if (_iterators.MoveNext())
                _iterators.Current.Dispose();
        while (_iterators.MoveNext())
            _iterators.Current.Dispose();
        _iterators.Dispose();
    }
}