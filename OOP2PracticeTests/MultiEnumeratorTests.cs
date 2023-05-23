using Xunit;
using OOP2Practice;

namespace OOP2PracticeTests; 

public class MultiEnumeratorTests {
    [Fact]
    public void TestEmpty() {
        using var iterator = new MultiEnumerator<int>(
            Array.Empty<IEnumerator<int>>().AsEnumerable().GetEnumerator());
        Assert.False(iterator.MoveNext());
    }

    [Fact]
    public void TestSingleObject() {
        using var iterator = new MultiEnumerator<string>(
            new IEnumerator<string>[] {
                new string[] { "Hello World!" }.AsEnumerable().GetEnumerator()
            }.AsEnumerable().GetEnumerator());
        iterator.MoveNext();
        Assert.Equal("Hello World!", iterator.Current);
        Assert.False(iterator.MoveNext());
    }

    [Fact]
    public void TestSample() {
        using var iterator = new MultiEnumerator<int>(
            new IEnumerator<int>[] {
                new int[] { 1, 2, 3 }.AsEnumerable().GetEnumerator(),
                new int[] { }.AsEnumerable().GetEnumerator(),
                new int[] { }.AsEnumerable().GetEnumerator(),
                new int[] { 4 }.AsEnumerable().GetEnumerator(),
                new int[] { 5, 6, 7, 8, 9, 10 }.AsEnumerable().GetEnumerator(),
                new int[] { }.AsEnumerable().GetEnumerator(),
                new int[] { }.AsEnumerable().GetEnumerator()
            }.AsEnumerable().GetEnumerator());
        iterator.MoveNext();
        for (int i = 1; i <= 10; i++) {
            Assert.Equal(i, iterator.Current);
            Assert.True(iterator.MoveNext() ^ (i == 10));
        }
    }

    [Fact]
    public void TestDifferentEnumerables() {
        Stack<char> stack = new();
        stack.Push('f');
        stack.Push('e');

        LinkedList<char> linkedList = new();
        linkedList.AddLast('g');
        linkedList.AddLast('h');

        Nested<char> nested = new(
            new Nested<char>[] {
                new('i'),
                new(new Nested<char>[] { new('j') }.AsEnumerable().GetEnumerator())
            });

        using var iterator = new MultiEnumerator<char>(
            new IEnumerator<char>[] {
                new char[] { 'a', 'b' }.AsEnumerable().GetEnumerator(),
                new List<char> { 'c', 'd' }.GetEnumerator(),
                stack.GetEnumerator(),
                linkedList.GetEnumerator(),
                nested.GetEnumerator()
            }.AsEnumerable().GetEnumerator());
        iterator.MoveNext();
        for (int i = 0; i < 10; i++) {
            Assert.Equal((char)('a' + i), iterator.Current);
            Assert.True(iterator.MoveNext() ^ (i == 9));
        }
    }

    [Fact]
    public void Test5Empty() {
        using var iterator = new MultiEnumerator<int>(
            new IEnumerator<int>[] {
                Array.Empty<int>().AsEnumerable().GetEnumerator(),
                Array.Empty<int>().AsEnumerable().GetEnumerator(),
                Array.Empty<int>().AsEnumerable().GetEnumerator(),
                Array.Empty<int>().AsEnumerable().GetEnumerator(),
                Array.Empty<int>().AsEnumerable().GetEnumerator()
            }.AsEnumerable().GetEnumerator());
        Assert.False(iterator.MoveNext());
    }
}