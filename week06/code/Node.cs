public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        Data = data;
    }

    public void Insert(int value)
    {
        // TODO Start Problem 1
        if (value == Data)
        {
            // Do nothing as the value is equal to the value at the current location in the tree and no more searching is needed and a new Node is not needed
        }
        else // The value is not equal to the value of the current location, so keep searching for the location to put the new value
        {
            if (value < Data)
            {
                // Insert to the left
                if (Left is null)  // The value is not in the tree, so add it to a new node in the empty spot
                    Left = new Node(value);
                else
                    Left.Insert(value);  // The left node is not empty so recursively call and insert from this location
            }
            else
            {
                // Insert to the right
                if (Right is null) // The value is not in the tree, so add it to a new node in the empty spot
                    Right = new Node(value);
                else
                    Right.Insert(value); // The right node is not empty so recursively call and insert from this new location
            }
        }
    }

    public bool Contains(int value)
    {
        if (value == Data)
        {
            return true;
        }
        else // The value is not equal to the value of the current location, so keep searching for the location to put the new value
        {
            if (value < Data)
            {
                // Check if contains to the left
                if (Left is null)  // This new location is empty so the value is not in the tree, so return false
                    return false;
                else
                    return Left.Contains(value);  // The left node is not empty so recursively call and check if the value is in the tree from this location and return the result
            }
            else
            {
                // Check if contains to the right
                if (Right is null) // The value is not in the tree, so add it to a new node in the empty spot
                    return false;
                else
                    return Right.Contains(value); // The right node is not empty so recursively call and check if the value is in the tree from this location and return the result
            }
        }
    }

    public int GetHeight()
    {
        // TODO Start Problem 4
        int heightLeft = new int();
        if (Left is null)  // The height increment from this function call going to the left includes only the current node
            heightLeft = 1;
        else
            heightLeft = 1 + Left.GetHeight();  // The left node is not empty so recursively call and get the height of that node and add to the height increment of the current node
        int heightRight = new int();
        if (Right is null)  // The height increment from this function call going to the right includes only the current node
            heightRight = 1;
        else
            heightRight = 1 + Right.GetHeight();  // The right node is not empty so recursively call and get the height of that node and add to the height increment of the current node
        return Math.Max(heightLeft, heightRight); // return the maximum of the height looking left or looking right
    }
}