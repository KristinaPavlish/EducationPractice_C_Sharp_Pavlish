namespace EducationalPractice.Bll.Implementation_Task_1;

public class Compress
{
    public static int[] CompressTheArray(int[] arrayOfNumbers)
    {
        List<int> arrayAfterCompression = new List<int>();

        foreach (int element in arrayOfNumbers)
        {
            string stringElement = element.ToString();
            if (CheckPattern.CheckPatternXYXY(stringElement) || CheckPattern.CheckPatternXYYX(stringElement))
            {
                arrayAfterCompression.Add(element);
            }
            else
            {
                arrayAfterCompression.Add(0);
            }
        }

        int lastNotNull = 0;
        int index = arrayAfterCompression.Count;
        while (index != 0)
        {
            index--;
            if (arrayAfterCompression[index] != 0)
            {
                lastNotNull = index;
                break;
            }
        }

        List<int> newList = new List<int>();
        int countZero = arrayAfterCompression.Count - lastNotNull - 1;

        // delete zero
        for (int i = 0; i <= lastNotNull; i++)
        {
            if (arrayAfterCompression[i] != 0)
            {
                newList.Add(arrayAfterCompression[i]);
            }
        }

        // add zero in the end
        if (newList.Count == 0)
        {
            for (int i = 0; i < arrayAfterCompression.Count; i++)
            {
                newList.Add(0);
            }
        }
        else
        {
            for (int i = 0; i < countZero; i++)
            {
                newList.Add(0);
            }
        }

        return newList.ToArray();
    }
}