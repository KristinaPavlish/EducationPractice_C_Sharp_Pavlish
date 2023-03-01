namespace EducationalPractice.Bll.Implementation_Task_1;

public class CheckPattern
{
    /// <summary>
    /// Check if a given string matches the pattern XYXY
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static bool CheckPatternXYXY(string element)
    {
        int length = element.Length;
        char first_digit = element[0];
        char second_digit = element[1];
        bool is_number_pattern = true;

        // Check incorrect pattern XXXX
        if (first_digit == second_digit)
        {
            is_number_pattern = false;
            return is_number_pattern;
        }

        // Check all X and Y
        for (int i = 0; i < length; i += 2)
        {
            if (element[i] != first_digit)
            {
                is_number_pattern = false;
                return is_number_pattern;
            }

            if (element[i + 1] != second_digit)
            {
                is_number_pattern = false;
                return is_number_pattern;
            }
        }

        return is_number_pattern;
    }

    /// <summary>
    /// Check if a given string matches the pattern XYYX
    /// </summary>
    /// <param name="element"></param>
    /// <returns></returns>
    public static bool CheckPatternXYYX(string element)
    {
        int length = element.Length;
        char first_digit = element[0];
        char second_digit = element[1];
        bool is_number_pattern = true;

        if (element[1] != element[2])
        {
            is_number_pattern = false;
            return is_number_pattern;
        }

        // Check incorrect pattern XXXX
        if (first_digit == second_digit)
        {
            is_number_pattern = false;
            return is_number_pattern;
        }

        // Check the length of the element and find the position
        // to which to check all X
        int pos;
        if (length % 4 == 0)
        {
            pos = length - 2;
        }
        else if (length % 2 == 0 && length % 4 != 0)
        {
            int len = length / 4;
            pos = len - 1;
        }
        else if (length % 3 == 0)
        {
            pos = length - 3;
        }
        else
        {
            pos = length - 1;
        }

        if (length == 4)
        {
            pos = length - 1;
        }

        // Check all X
        while (pos != 0)
        {
            if (int.Parse(first_digit.ToString()) == int.Parse(element[pos].ToString()))
            {
                is_number_pattern = true;
                pos -= 3;
            }
            else
            {
                is_number_pattern = false;
                return is_number_pattern;
            }
        }

        // Create new string without X
        string another_digits = "";
        foreach (char digit in element)
        {
            if (digit != first_digit)
            {
                another_digits += digit;
            }
            else
            {
                continue;
            }
        }

        // Check all Y
        foreach (char digit in another_digits)
        {
            if (digit == second_digit)
            {
                is_number_pattern = true;
            }
            else
            {
                is_number_pattern = false;
                return is_number_pattern;
            }
        }
        return is_number_pattern;
    }
}