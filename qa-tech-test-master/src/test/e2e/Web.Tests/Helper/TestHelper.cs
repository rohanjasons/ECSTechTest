using OpenQA.Selenium;
using System.Collections.Generic;

namespace Web.Integration.Test.Helper
{
    public static class TestHelper
    {
        public static List<int> ArrayConvertor(IWebElement table)
        {
            var value = new List<int>();
            var columns = table.FindElements(By.TagName("td"));

            for(int i = 0; i < columns.Count; i++)
            {
                value.Add(int.Parse(columns[i].Text));
            }

            return value;
        }

        public static int GetRowCount(IWebElement table)
        {
            return table.FindElements(By.TagName("tr")).Count;
        }

        public static int CheckPointArraySumMatches(List<int> array)
        {
            // initialize sum of whole array
            int sum = 0;

            // initialize leftsum
            int leftsum = 0;

            // Find sum of the whole array 
            for (int i = 0; i < array.Count; ++i)
                sum += array[i];

            for (int i = 0; i < array.Count; ++i)
            {
                // sum is now right sum for index i
                sum -= array[i];

                if (leftsum == sum)
                    return i;

                leftsum += array[i];
            }

            // If no equilibrium index found, then return 0
            return -1;
        }
    }
}