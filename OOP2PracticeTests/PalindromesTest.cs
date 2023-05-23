using System.Globalization;
using OOP2Practice;
using Xunit;

namespace OOP2PracticeTests; 

public class PalindromesTest {
    [Fact]
    public void TestLettersOnly() {
        Assert.True("abacabadabacaba".IsPalindrome());
        Assert.True("qwertyuioppoiuytrewq".IsPalindrome());
        Assert.True("ABCDdcba".IsPalindrome());
        Assert.True("w".IsPalindrome());
        Assert.False("abcdfcba".IsPalindrome());
        Assert.False("cat".IsPalindrome());
        Assert.False("qwertytrewf".IsPalindrome());
        Assert.False("zecvbnmnbdcxz".IsPalindrome());
        Assert.True("saippuakivikauppias".IsPalindrome());
    }

    [Fact]
    public void TestPunctuationsOnly() {
        Assert.True("".IsPalindrome());
        Assert.True("!@#$%^&*()".IsPalindrome());
        Assert.True("/\\/\\/\\/\\/\\/".IsPalindrome());
    }

    [Fact]
    public void TestSimpleSentences() {
        // https://en.wikipedia.org/wiki/Palindrome
        Assert.True("А роза упала на лапу Азора".IsPalindrome());
        Assert.True("In girum imus nocte et consumimur igni".IsPalindrome());
        Assert.True("A man, a plan, a canal – Panama".IsPalindrome());
        Assert.False("The quick brown fox jumps over the lazy dog".IsPalindrome());
        Assert.False("A man, a plate, a canal – Panama".IsPalindrome());
        Assert.False("A man, a plan, b canal – Panama".IsPalindrome());
    }

    [Fact]
    public void TestSpecificCulture() {
        var currentCulture = Thread.CurrentThread.CurrentCulture;
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
        Assert.False("İıIi".IsPalindrome());
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("tr-TR");
        Assert.True("İıIi".IsPalindrome());
        Thread.CurrentThread.CurrentCulture = currentCulture;
    }
}