using System.Collections;

public static class Recursion
{
    /// <summary>
    /// #############
    /// # Problem 1 #
    /// #############
    /// Using recursion, find the sum of 1^2 + 2^2 + 3^2 + ... + n^2
    /// and return it.  Remember to both express the solution 
    /// in terms of recursive call on a smaller problem and 
    /// to identify a base case (terminating case).  If the value of
    /// n <= 0, just return 0.   A loop should not be used.
    /// </summary>
    public static int SumSquaresRecursive(int n)
    {
        if (n <=0)
        {
            return 0;
        }
        else if (n == 1)
        {
            return 1;
        }
        else
        {
            return n*n + SumSquaresRecursive(n-1);
        }
    }

    /// <summary>
    /// #############
    /// # Problem 2 #
    /// #############
    /// Using recursion, insert permutations of length
    /// 'size' from a list of 'letters' into the results list.  This function
    /// should assume that each letter is unique (i.e. the 
    /// function does not need to find unique permutations).
    ///
    /// In mathematics, we can calculate the number of permutations
    /// using the formula: len(letters)! / (len(letters) - size)!
    ///
    /// For example, if letters was [A,B,C] and size was 2 then
    /// the following would the contents of the results array after the function ran: AB, AC, BA, BC, CA, CB (might be in 
    /// a different order).
    ///
    /// You can assume that the size specified is always valid (between 1 
    /// and the length of the letters list).
    /// </summary>
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    // note that "" is the default for word if no value included in the method call
    // note that "size" is equivalent to "choose" and I would use that as the variable name to match the mathematical definition
    {
        if (letters.Length == 0 || size == 0)
        {
            // add whatever is in the variable word to the varaible list of results if there are no more char in letters available
            // to choose to add to the permutation OR the number of letters to choose is 0
            results.Add(word);
            return;  // then return with no more recursion
        }
        // if there are letters available for the permutation and some nonzero number of letters to choose, then go through each letter
        for (var i = 0; i < letters.Length; i++)
            {
                var lettersLeft =  letters.Remove(i,1); // remove the letter for this i value in the loop
                var wordNew = word + letters[i]; // Add the letter char to the right of the word string as you build up word for the permutation
                // call the function again.  pass the results list, the remaining letters, choose 1 fewer from the remaining letters, and pass the current 
                // value of the word
                PermutationsChoose(results, lettersLeft, size-1, wordNew);
            }
    }

    /// <summary>
    /// #############
    /// # Problem 3 #
    /// #############
    /// Imagine that there was a staircase with 's' stairs.  
    /// We want to count how many ways there are to climb 
    /// the stairs.  If the person could only climb one 
    /// stair at a time, then the total would be just one.  
    /// However, if the person could choose to climb either 
    /// one, two, or three stairs at a time (in any order), 
    /// then the total possibilities become much more 
    /// complicated.  If there were just three stairs,
    /// the possible ways to climb would be four as follows:
    ///
    ///     1 step, 1 step, 1 step
    ///     1 step, 2 step
    ///     2 step, 1 step
    ///     3 step
    ///
    /// With just one step to go, the ways to get
    /// to the top of 's' stairs is to either:
    ///
    /// - take a single step from the second to last step, 
    /// - take a double step from the third to last step, 
    /// - take a triple step from the fourth to last step
    ///
    /// We don't need to think about scenarios like taking two 
    /// single steps from the third to last step because this
    /// is already part of the first scenario (taking a single
    /// step from the second to last step).
    ///
    /// These final leaps give us a sum:
    ///
    /// CountWaysToClimb(s) = CountWaysToClimb(s-1) + 
    ///                       CountWaysToClimb(s-2) +
    ///                       CountWaysToClimb(s-3)
    ///
    /// To run this function for larger values of 's', you will need
    /// to update this function to use memoization.  The parameter
    /// 'remember' has already been added as an input parameter to 
    /// the function for you to complete this task.
    /// </summary>
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        // Base Cases
        if (remember == null)
        {
            remember = new Dictionary<int, decimal>();
        }
        if (s == 0)
        {
            remember[s] = 0; // no ways when there are no stairs (0)
            return remember[s];
        }
        if (s == 1)
        {
            remember[s] = 1; // 1 step from top (1)
            return remember[s];
        }
        if (s == 2)
        {
            remember[s] = 2; // 1 step + 1 step from top OR 2 step from top (2)
            return remember[s];
        }
        if (s == 3)
        {
            remember[s] = 4; // 1 step + 1 step + 1 step from top OR 2 step + 1 step from top OR 1 step + 2 step from top OR 3 step from top (4)
            return remember[s];
        }  
        if (remember.ContainsKey(s))
        {
            return remember[s];
        }
            decimal ways = CountWaysToClimb(s - 1, remember) + CountWaysToClimb(s - 2, remember) + CountWaysToClimb(s - 3, remember);
            remember.Add(s,ways);
            return ways;
    }

    /// <summary>
    /// #############
    /// # Problem 4 #
    /// #############
    /// A binary string is a string consisting of just 1's and 0's.  For example, 1010111 is 
    /// a binary string.  If we introduce a wildcard symbol * into the string, we can say that 
    /// this is now a pattern for multiple binary strings.  For example, 101*1 could be used 
    /// to represent 10101 and 10111.  A pattern can have more than one * wildcard.  For example, 
    /// 1**1 would result in 4 different binary strings: 1001, 1011, 1101, and 1111.
    ///	
    /// Using recursion, insert all possible binary strings for a given pattern into the results list.  You might find 
    /// some of the string functions like IndexOf and [..X] / [X..] to be useful in solving this problem.
    /// </summary>
    public static void WildcardBinary(string pattern, List<string> results)
    {
        // Base Case
        if (!pattern.Contains('*'))
        {
            results.Add(pattern);
            return;
        }
        // determien where is the first '*' in the string
        int indexLocation = pattern.IndexOf('*');
        // convert to char array and set index location value to '0' and recurse with new pattern
        char[] patternChar0 = pattern.ToCharArray();
        patternChar0[indexLocation] = '0';
        string patternNew0 = new string(patternChar0);
        WildcardBinary(patternNew0, results);

        char[] patternChar1 = pattern.ToCharArray();
        patternChar1[indexLocation] = '1';
        string patternNew1 = new string(patternChar1);
        WildcardBinary(patternNew1, results);
    }

    /// <summary>
    /// Use recursion to insert all paths that start at (0,0) and end at the
    /// 'end' square into the results list.
    /// </summary>
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        // Starting Case:  If this is the first time running the function, then we need
        // to initialize the currPath list and add (0,0) to the list of ValueTuple. Note that (0,0) is the (x,y) position to start
        if (currPath == null)
        {
            currPath = new List<ValueTuple<int, int>>();
        }

        // Base Case:  Check if this call to the function at (x,y) is at the end.
        if (maze.IsEnd(x,y))
        {
            // if we have reached the 2 value, then add the current to results and return
            currPath.Add((x,y));
            results.Add(currPath.AsString());
            // then remove this last location to clean out the curr path as the other locations are checked
            currPath.RemoveAt(currPath.Count-1);
            return;
        }
        // If we have not reached the end, then check all possible directions in order of Up, Down, Left, Right, for a valid move
        // and then recursively solve the maze at that valid move.  You need to pass currPath to check if you just came from that location you are
        // checking.
        // Check Up
        if (maze.IsValidMove(currPath, x, y-1))
        {
            // if this is a valid move, then solve the maze from there
            currPath.Add((x,y));
            SolveMaze(results, maze, x, y-1, currPath);
            // then remove this last location to clean out the curr path as the other locations are checked
            currPath.RemoveAt(currPath.Count-1);
        }
        // Check Down
        if (maze.IsValidMove(currPath, x, y+1))
        {
            // if this is a valid move, then solve the maze from there
            currPath.Add((x,y));
            SolveMaze(results, maze, x, y+1, currPath);
            // then remove this last location to clean out the curr path as the other locations are checked
            currPath.RemoveAt(currPath.Count-1);
        }
        // Check Left
        if (maze.IsValidMove(currPath, x-1, y))
        {
            // if this is a valid move, then solve the maze from there
            currPath.Add((x,y));
            SolveMaze(results, maze, x-1, y, currPath);
            // then remove this last location to clean out the curr path as the other locations are checked
            currPath.RemoveAt(currPath.Count-1);
        }
        // Check Right
        if (maze.IsValidMove(currPath, x+1, y))
        {
            // if this is a valid move, then the current locatin to the current path and solve the maze from there
            currPath.Add((x,y));
            SolveMaze(results, maze, x+1, y, currPath);
            // then remove this last location to clean out the curr path as the other locations are checked
            currPath.RemoveAt(currPath.Count-1);
        }
        // after all the possible directions are checked and any valid paths are added to the results, then just return from the function call
        // if none of the directions were valid, then the location we are at will not add the location to the current path.
        return;
        // currPath.Add((1,2)); // Use this syntax to add to the current path
        // results.Add(currPath.AsString()); // Use this to add your path to the results array keeping track of complete maze
        // solutions when you find a solution.
    }
}