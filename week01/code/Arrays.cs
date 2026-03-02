using System.Diagnostics;

public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // This method will return an array of doubles.  The approach is to first create a new array of size length.
        // Then run a for loop from index i 1 to length.  Then at each step
        // there will be a line to calculate the multiple of the i * number and this value will be added to the array.
        double[] multiples = new double[length];
        for (int i = 0; i < length; i++)
        {
            multiples[i] = (i + 1) * number;
        }
        // Debug.WriteLine("<List>{" + string.Join(", ", multiples) + "}");
        return multiples; // replace this return statement with your own
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        // TODO Problem 2 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.

        // The approach used to implement this method is to first load the items at the end of the list into a temporary list.
        // This is a GetRange method.  Then RemoveRange to remove that copied list from the original list data.  Then InsertRange to put that list at the front.
        List<int> tempStore = new List<int>();
        tempStore = data.GetRange(data.Count - amount, amount);
        // Debug.WriteLine("<List>{" + string.Join(", ", tempStore) + "}");
        data.RemoveRange(data.Count - amount, amount);
        data.InsertRange(0,tempStore);
        // Debug.WriteLine("<List>{" + string.Join(", ", data) + "}");
    }
}
