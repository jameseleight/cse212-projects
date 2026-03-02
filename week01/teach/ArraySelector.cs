public static class ArraySelector
{
    public static void Run()
    {
        var l1 = new[] { 1, 2, 3, 4, 5 };
        var l2 = new[] { 2, 4, 6, 8, 10};
        var select = new[] { 1, 1, 1, 2, 2, 1, 2, 2, 2, 1};
        var intResult = ListSelector(l1, l2, select);
        Console.WriteLine("<int[]>{" + string.Join(", ", intResult) + "}"); // expected is <int[]>{1, 2, 3, 2, 4, 4, 6, 8, 10, 5}
    }

    private static int[] ListSelector(int[] list1, int[] list2, int[] select)
    {
        int index1 = 0;
        int index2 = 0;
        int resultIndex = 0;
        int[] listResult = new int[select.Length];
        foreach (var selectorValue in select)
        {
            if (selectorValue == 1)
            {
                listResult[resultIndex] = list1[index1];
                index1++;
                resultIndex++;
            }
            else // selectorValue == 2
            {
                listResult[resultIndex] = list2[index2];
                index2++;
                resultIndex++;
            }
        }
        return listResult;
    }
}