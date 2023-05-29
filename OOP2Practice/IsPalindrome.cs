using System.Globalization;

namespace OOP2Practice; 

public static class IsPalindromeExtension {
    public static bool IsPalindrome(this string str,
        CultureInfo culture) {
        int l = 0, r = str.Length - 1;
        while (l < r) {
            while (l < str.Length && !Char.IsLetter(str[l]))
                l++;
            while (r >= 0 && !Char.IsLetter(str[r]))
                r--;
            if (r < 0 || l >= str.Length)
                return true;
            if (Char.ToUpper(str[l], culture) != Char.ToUpper(str[r], culture))
                return false;
            l++;
            r--;
        }
        return true;
    }

    public static bool IsPalindrome(this string str) =>
        IsPalindrome(str, Thread.CurrentThread.CurrentCulture);
}