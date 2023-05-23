namespace OOP2Practice; 

public static class IsPalindromeExtension {
    public static bool IsPalindrome(this string str) {
        string upper = str.ToUpper(System.Threading.Thread.CurrentThread.CurrentCulture);
        string adjusted = string.Join("", upper.Where(Char.IsLetter));
        if (adjusted.Length == 0)
            return true;
        return Enumerable.Range(0, adjusted.Length / 2 + 1)
            .All(i => adjusted[i] == adjusted[adjusted.Length - 1 - i]);
    }
}