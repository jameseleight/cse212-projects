using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    // <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // make a new set for the results
        var wordsSet = new HashSet<string>(words);
        var results = new HashSet<string>();
        var reverseResults = new HashSet<string>();
        foreach (var item in wordsSet)
        {
            // turn the string into a char array
            char[] itemChars = item.ToCharArray();
            // use method to reverse the chars
            Array.Reverse(itemChars);
            // use string constructor to make a new string of the reversed characters
            string itemReverse = new string(itemChars);
            if (itemReverse != item)  // check to not operate on case where the item is equal to its reverse
            {
                // determine if the reverse is in the set
                if (wordsSet.Contains(itemReverse))
                {
                    // if the reverse is in the set, check that the pair has not already been added to the results set
                    if (!reverseResults.Contains(item) && !reverseResults.Contains(itemReverse))
                    {
                        results.Add(item);
                        reverseResults.Add(itemReverse);
                    }
                }
            }
        }
        // make a string array of the results
        int index = 0;
        string[] resultsDisplay = new string[results.Count];
        List<string> resultsList = results.ToList();
        List<string> reverseResultsList = reverseResults.ToList();
        for (int i = 0; i < results.Count; i++)
        {
            resultsDisplay[index] = $"{resultsList[i]} & {reverseResultsList[i]}";
            index++;
        }
        return resultsDisplay;
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    // <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            if (degrees.ContainsKey(fields[3]))
            {
                degrees[fields[3]]++;
            }
            else
            {
                degrees[fields[3]] = 1;
            }
        }
        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        if (word1.Length != word2.Length)
        {
            return false;
        }
        var word1Check = new Dictionary<char, int>();
        var word2Check = new Dictionary<char, int>();
        for (int i = 0; i < word1.Length; i++)
        {
            // load the chars from the word into a dictionary and count the number for each char
            char itemChar1 = Char.ToLower(word1[i]);
            char itemChar2 = Char.ToLower(word2[i]);
            if (word1Check.ContainsKey(itemChar1))
            {
                word1Check[itemChar1]++;
            }
            else if (itemChar1 != ' ')
            {
                word1Check[itemChar1] = 1;
            }
            if (word2Check.ContainsKey(itemChar2))
            {
                word2Check[itemChar2]++;
            }
            else if (itemChar2 != ' ')
            {
                word2Check[itemChar2] = 1;
            }
        }
        foreach (char item in word1Check.Keys)
        {
            if (!word2Check.ContainsKey(item))
            {
                return false;
            }
            else if (word1Check[item] != word2Check[item])
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        FeatureCollection featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        string[] stringReturn = new string[featureCollection.features.Count()];
        int index = 0;
        foreach (var item in featureCollection.features)
        {
            stringReturn[index] = $"{item.properties.place} - Mag {item.properties.mag}";
            index++;
        }
        return stringReturn;
    }
}