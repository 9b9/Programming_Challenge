namespace src;

public class Solution
{
    public int Method(int[] array, int k)
    {
        if (array.Length == 1 || array.Length == k + 1)
        {
            return 0;
        }

        // K == 1, 必定是移除最大值、或最小值，達到震盪最小
        if (k == 1)
        {
            var min_1 = int.MaxValue;
            var min_2 = int.MaxValue;

            var max_1 = int.MinValue;
            var max_2 = int.MinValue;

            for (int index = 0; index < array.Length; index++)
            {
                var a = array[index];
                if (min_1 > a)
                {
                    min_2 = min_1;
                    min_1 = a;
                }
                else if (min_2 > a)
                {
                    min_2 = a;
                }

                if (max_1 < a)
                {
                    max_2 = max_1;
                    max_1 = a;
                }
                else if (max_2 < a)
                {
                    max_2 = a;
                }
            }

            return Math.Min(max_1 - min_2, max_2 - min_1);
        }

        var leftArray = new List<(int min, int max)>()
            {
                (array[0], array[0])
            };
        for (int index = 1; index < array.Length; index++)
        {
            leftArray.Add((
                Math.Min(array[index], leftArray[leftArray.Count - 1].min),
                Math.Max(array[index], leftArray[leftArray.Count - 1].max)));
        }

        // 若整串最大值 = 最小值, 則無論 K 為多少都最小震盪都是 0
        if (leftArray[leftArray.Count - 1].min == leftArray[leftArray.Count - 1].max)
        {
            return 0;
        }

        var rightArray = new List<(int min, int max)>()
            {
                 (array[array.Length - 1], array[array.Length - 1])
            };
        for (int index = array.Length - 2; index >= 0; index--)
        {
            rightArray.Add((
                Math.Min(array[index], rightArray[rightArray.Count - 1].min),
                Math.Max(array[index], rightArray[rightArray.Count - 1].max)));
        }

        rightArray.Reverse();

        var result = Math.Min(
            Math.Abs(leftArray[array.Length - k - 1].max - leftArray[array.Length - k - 1].min),
            Math.Abs(rightArray[k].max - rightArray[k].min));

        for (int startIndex = 0; startIndex + k + 1 < array.Length; startIndex++)
        {
            result = Math.Min(
                result,
                Math.Max(
                    Math.Abs(leftArray[startIndex].max - rightArray[startIndex + k + 1].min),
                    Math.Abs(leftArray[startIndex].min - rightArray[startIndex + k + 1].max)));
        }

        return result;
    }
}
