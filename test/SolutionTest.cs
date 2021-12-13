using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using src;

namespace test;

public class Tests
{
    private Stopwatch _stopWatch = Stopwatch.StartNew();

    [SetUp]
    public void Init()
    {
        _stopWatch = Stopwatch.StartNew();
    }

    [TearDown]
    public void Cleanup()
    {
        _stopWatch.Stop();
        TestContext.WriteLine("Excution time for {0} - {1} ms",
            TestContext.CurrentContext.Test.Name,
            _stopWatch.ElapsedMilliseconds);
    }

    public static IEnumerable TestDataCases
    {
        get
        {
            yield return new TestCaseData(
                new[] { 8, 8, 1 },
                1,
                0);
            yield return new TestCaseData(
                new[] { 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 8, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                1,
                7);
            yield return new TestCaseData(
                Enumerable.Repeat(3, 10000000).ToArray(),
                1,
                0);
            yield return new TestCaseData(
                Enumerable.Repeat(3, 10000000).ToArray(),
                2,
                0);
            {
                var temp = new List<int>(Enumerable.Repeat(3, 10000000));
                temp.AddRange(Enumerable.Repeat(2, 10000000));
                yield return new TestCaseData(
                    temp.ToArray(),
                    2,
                    1);
            }
            yield return new TestCaseData(
                new[] { 8, 8, 4, 1, 2 },
                2,
                3);
            yield return new TestCaseData(
                new[] { 8, 8, 4, 1, 2 },
                3,
                0);
        }
    }

    [Test, TestCaseSource("TestDataCases")]
    public void ReverseTest(
        int[] input1,
        int input2,
        int expected)
    {
        var sample = new Solution();

        var actual = sample.Method(input1, input2);

        Assert.AreEqual(expected, actual);
    }
}