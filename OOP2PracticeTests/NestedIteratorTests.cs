using Xunit;
using OOP2Practice;

namespace OOP2PracticeTests; 

public class NestedIteratorTests {
    [Fact]
    public void TestEmpty() {
        var nested = new Nested<int>(new Nested<int>[] { });
        using var iterator = nested.GetEnumerator();
        iterator.MoveNext();
        Assert.False(iterator.MoveNext());
    }

    [Fact]
    public void TestSingleObject() {
        var nested = new Nested<string>(
            new Nested<string>[] { new("Hello World!") });
        using var iterator = nested.GetEnumerator();
        iterator.MoveNext();
        Assert.Equal("Hello World!", iterator.Current);
        Assert.False(iterator.MoveNext());
    }

    [Fact]
    public void TestSample() {
        var nested = new Nested<int>(
            new Nested<int>[] {
                new(new Nested<int>[] {
                    new(1), 
                    new(new Nested<int>[] { new(2), new(3) })
                }),
                new(new Nested<int>[] {
                    new(4),
                    new(new Nested<int>[] {
                        new(5), 
                        new(6),
                        new(7),
                        new(new Nested<int>[] { })
                    }),
                    new(8)
                }),
                new(9),
                new(new Nested<int>[] {
                    new(new Nested<int>[] {
                        new(new Nested<int>[] { }),
                        new(new Nested<int>[] { })
                    })
                }),
                new(10)
            });
        using var iterator = nested.GetEnumerator();
        iterator.MoveNext();
        for (int i = 1; i <= 10; i++) {
            Assert.Equal(i, iterator.Current);
            Assert.True(iterator.MoveNext() ^ (i == 10));
        }
    }

    [Fact]
    public void TestDifferentEnumerables() {
        Stack<Nested<char>> stack = new();
        stack.Push(new('f'));
        stack.Push(new('e'));

        LinkedList<Nested<char>> linkedList = new();
        linkedList.AddLast(new Nested<char>('g'));
        linkedList.AddLast(new Nested<char>('h'));

        Nested<Nested<char>> lowOrderNested = new(
            new Nested<Nested<char>>[] {
                new(new Nested<char>('i')),
                new(new Nested<char>(new Nested<char>[] { new('j') }.AsEnumerable().GetEnumerator()))
            });

        var nested = new Nested<char>(new Nested<char>[] {
            new(new Nested<char>[] { new('a'), new('b') }),
            new(new List<Nested<char>> { new('c'), new('d') }),
            new(stack),
            new(linkedList),
            new(lowOrderNested)
        });
        using var iterator = nested.GetEnumerator();
        iterator.MoveNext();
        for (int i = 0; i < 10; i++) {
            Assert.Equal((char)('a' + i), iterator.Current);
            Assert.True(iterator.MoveNext() ^ (i == 9));
        }
    }

    [Fact]
    public void TestNestedNested() {
        var nested = new Nested<int>(1);
        for (int i = 0; i < 10; i++)
            nested = new Nested<int>(new[] { nested });
        using var iterator = nested.GetEnumerator();
        iterator.MoveNext();
        Assert.Equal(1, iterator.Current);
        Assert.False(iterator.MoveNext());
    }
}