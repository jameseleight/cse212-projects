public class SimpleQueue {
    public static void Run() {
        // Test Cases

        // Test 1
        // Scenario: Enqueue one value and then Dequeue it.
        // Expected Result: It should display 100
        Console.WriteLine("Test 1");
        var queue = new SimpleQueue();
        queue.Enqueue(100);
        var value = queue.Dequeue();
        Console.WriteLine(value);
        // Defect(s) Found:  There was an System.ArgumentOutOfRangeException: Index was out of range exception.  My hypothesis is tha the Dequeue() method has an error
        // where the incorrect index of 1 is used instead of 0 to pull the first item from the list and remove it from the list.  I changed the code to correct according
        // to my hypothesis and the Tests executed and there was no Exception.  So the hypothesis was valid.

        Console.WriteLine("------------");

        // Test 2
        // Scenario: Enqueue multiple values and then Dequeue all of them
        // Expected Result: It should display 200, then 300, then 400 in that order
        Console.WriteLine("Test 2");
        queue = new SimpleQueue();
        queue.Enqueue(200);
        queue.Enqueue(300);
        queue.Enqueue(400);
        value = queue.Dequeue();
        Console.WriteLine(value);
        value = queue.Dequeue();
        Console.WriteLine(value);
        value = queue.Dequeue();
        Console.WriteLine(value);
        // Defect(s) Found: Upon running the code the console output is not as expected.  The values 200, 300, 400 should be written in order.  But the reverse order
        // was written to the console.  My hypothesis is that there is a logical error in the Enqueue method.
        // I took two hypothesis to determine the logical correction is to use Add rather than Insert.  This produced the expected result.

        Console.WriteLine("------------");

        // Test 3
        // Scenario: Dequeue from an empty Queue
        // Expected Result: An exception should be raised
        Console.WriteLine("Test 3");
        queue = new SimpleQueue();
        try {
            queue.Dequeue();
            Console.WriteLine("Oops ... This shouldn't have worked.");
        }
        catch (IndexOutOfRangeException) {
            Console.WriteLine("I got the exception as expected.");
        }
        // Defect(s) Found: No defects found.  the catch message is written to the console.
    }

    private readonly List<int> _queue = new();

    /// <summary>
    /// Enqueue the value provided into the queue
    /// </summary>
    // <param name="value">Integer value to add to the queue</param>
    private void Enqueue(int value) {
        // my hypothesis is that the method Insert() is incorrect as this takes the new value and adds it to the front of the line.  This would give last in first out
        // with the current Denqueue() method.  Instead my hypothesis is that the correct method is Append(value)
        // I tested this and there was an System.IndexOutOfRangeException.  With some research, it looks like Append does not add to the original list, it just gives an 
        // immediate result.  So I will try the Add() method instead for my second iteration.
        // This produced the expected result so the second hypothesis is confirmed.
        _queue.Add(value);
    }

    /// <summary>
    /// Dequeue the next value and return it
    /// </summary>
    // <exception cref="IndexOutOfRangeException">If queue is empty</exception>
    /// <returns>First integer in the queue</returns>
    private int Dequeue() {
        if (_queue.Count <= 0)
            throw new IndexOutOfRangeException();

        // My hypothesis is that the index should be 0 instead of 1 since an array starts with index 0, this would be the first item in the queue
        // I will change the index for the next two lines from 1 to 0 and see if the test passes.
        var value = _queue[0];
        _queue.RemoveAt(0);
        return value;
    }
}