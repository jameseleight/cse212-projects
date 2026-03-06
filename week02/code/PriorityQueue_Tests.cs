using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: In this scenario, the queue is empty and the Dequeue will throw an error exception "InvalidOperationException" with a message of "The queue is empty."
    // Expected Result: pass means that the output of running the Dequeue method on an empty queue is "InvalidOperationException" wit the message of "The queue is empty."
    // This test will verify requirement 4
    // Defect(s) Found: None found.  Implenenting the code for this test below results in a passed result.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();
    try
        {
            priorityQueue.Dequeue();
            Assert.Fail("Exception should have been thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
        catch (AssertFailedException)
        {
            throw;
        }
        catch (Exception e)
        {
            Assert.Fail(
                 string.Format("Unexpected exception of type {0} caught: {1}",
                                e.GetType(), e.Message)
            );
        }
    }

    [TestMethod]
    // Scenario: Add items to the queue with priority so that they are out of order from what will be pulled based on priority.  since the value is generic, we can just use names
    // as if the names are say patients in an emergency room and priority is in the range 1-5 based on a triage with some categories of injuries or medical conditions determined
    // by the intake technician.
    // Expected Result: Enqueue the following in order: smith, 1; jones, 4; white, 4, hanner, 2; sharp, 3; brown, 1; green, 5.  Dequeue 6 of these 7 patients/values to give 
    // [green, jones, white, sharp, hanner, smith]  This test determies if the priority overrides the order in the queue, values with equal priority are Dequeue in the order they entered
    // the queue.  Also, that the Enqueue adds items to the back of the queue which also results in the person first in the queue with equal highest priority is Dequeue first.
    // This test will verify requirements 1, 2, 3.
    // Defect(s) Found: running this test failed.  expected value was green, actual was white.  So the highest priority did not get pulled first.  Examination of the code
    // revealed that it looks like the Enqueue correctly creates a PriorityItem class var and then adds it to the list as expected.  My hypothesis is that Enqueue is correctly coded.
    // examination of Dequeue reveals that the for loop to go through the list starts at index 1 which is an error and it should start at 0.  Also the end value is .Count - 1 
    // which won't give an out of range exception, but should be .Count.  My hypothesis is that correcting the for loop index will fix the error.  It looks like the conditional 
    // statement should have > rather than >= so that two items with equal priority pulls the first in rather than the last in.  First I will check the first correction, then can 
    // make an additional correction.  
    // The result of the first update and test was that expected jones and actual was green.  This means that the first Dequeue worked, but the second didn't.  The actual was green 
    // so this looks to mean that green was not removed from the queue list.  Will modify the code to remove after the value is determined.  Then test second time.  Test failed.
    // This time expected jones, actual white.  This looks to be from the >= rather than >, so will make that update.  Then test third time.  This time the test passed.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Enqueue("smith", 1);
        priorityQueue.Enqueue("jones", 4);
        priorityQueue.Enqueue("white", 4);
        priorityQueue.Enqueue("hanner", 2);
        priorityQueue.Enqueue("sharp", 3);
        priorityQueue.Enqueue("brown", 1);
        priorityQueue.Enqueue("green", 5);
        string[] expectedResult = ["green","jones","white","sharp","hanner","smith"];
        for (int i = 0; i < 6; i++)
        {
            var value = priorityQueue.Dequeue();
            Assert.AreEqual(expectedResult[i], value);
        }

    }

    // Add more test cases as needed below.
    // Since all the requirements have test conditions in the scenario, my hypothesis is that the code is completely tested and valid.
}