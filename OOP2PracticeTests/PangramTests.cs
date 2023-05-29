using OOP2Practice;
using Xunit;

namespace OOP2PracticeTests; 

public class PangramTests {
    [Fact]
    public void TestHelloWorld() {
        PangramSearcher searcher = new PangramSearcher("Hello World!");
        string testStr = "Hello World!";
        Assert.Equal(testStr, searcher.GetShortestPangram(testStr)!.Slice(testStr));
    }

    [Fact]
    public void TestNoPangram() {
        PangramSearcher searcher = new PangramSearcher("abcd");
        Assert.Null(searcher.GetShortestPangram("abcabcabc"));
        Assert.Null(searcher.GetShortestPangram("abdabdwabdeabdrabq"));
        Assert.Null(searcher.GetShortestPangram(""));
    }

    [Fact]
    public void TestMultipleCases() {
        PangramSearcher searcher = new PangramSearcher("abcd");
        List<Tuple<string, int>> testStrList = new List<Tuple<string, int>> {
            new("abcd", 4),
            new("abcdabcdabcd", 4),
            new("qaaaaabbbbbcccccddddd", 12),
            new("cbad", 4),
            new("aebfcgdh", 7),
            new("0a0b0c0d0", 7),
            new("abacabadabacabae", 5),
            new("qwertyuiopasdfghjklzxcvbnm", 14),
            new("aabbaacgad abacaaad bcbfda capabbcaacmnkbd", 5),
            new(":-)", 0),
            new("ddbbcbcbbdd", 0),
            new("", 0)
        };
        foreach (var test in testStrList) {
            if (test.Item2 == 0)
                Assert.Null(searcher.GetShortestPangram(test.Item1));
            else {
                string actual = searcher.GetShortestPangram(test.Item1)!.Slice(test.Item1);
                Assert.Equal(test.Item2, actual.Length);
                Assert.All(searcher.Alphabet, ch => actual.Contains(ch));
            }
        }
    }

    [Fact]
    public void TestSingleLetter() {
        PangramSearcher searcher = new PangramSearcher("A");
        Assert.Equal(1, searcher.GetShortestPangram("QWERYUIOPASDFGHJKLZXCVBNM")!.Length);
        Assert.Equal("A", searcher.GetShortestPangram("ABACABADABACABA")!.Slice("ABACABADABACABA"));
        Assert.Null(searcher.GetShortestPangram("$"));
    }
}