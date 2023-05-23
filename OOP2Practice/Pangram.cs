namespace OOP2Practice;

// Until index not included
public record StringSlice(int StartIndex, int UntilIndex) {
    public int Length => UntilIndex - StartIndex;
    
    public string Slice(string str) =>
        str.Substring(StartIndex, Length);
}

public class PangramSearcher {
    private HashSet<char> _alphabet;

    public HashSet<char> Alphabet => _alphabet;

    public PangramSearcher(IEnumerable<char> alphabet) {
        _alphabet = alphabet.ToHashSet();
        if (_alphabet.Count == 0)
            throw new ArgumentException();
    }

    public StringSlice? GetShortestPangram(string str) {
        if (string.IsNullOrEmpty(str))
            return null;

        int l = 0, r = 1;
        Dictionary<char, int> charCount = new Dictionary<char, int>();
        foreach (char a in _alphabet)
            charCount[a] = 0;

        int charsPresent = 0;
        if (_alphabet.Contains(str[0])) {
            charCount[str[0]] = 1;
            charsPresent = 1;
        }

        int resL = -str.Length - 2, resR = -1;
        while (l < str.Length - 1) {
            if (r < str.Length && charsPresent < _alphabet.Count) {
                if (charCount.ContainsKey(str[r])) {
                    if (charCount[str[r]] == 0)
                        charsPresent++;
                    charCount[str[r]]++;
                }
                r++;
            }
            else {
                if (charCount.ContainsKey(str[l])) {
                    if (charCount[str[l]] == 1)
                        charsPresent--;
                    charCount[str[l]]--;
                } 
                l++;
            }

            if (charsPresent == _alphabet.Count &&
                r - l < resR - resL) {
                resR = r;
                resL = l;
            }
        }

        if (resR == -1)
            return null;
        return new(resL, resR);
    }
}