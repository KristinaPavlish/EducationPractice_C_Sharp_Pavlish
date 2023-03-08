namespace EducationalPractice.Bll.Implementation_Task_1;

public class FillArray
{
    static int FIRST_RANGE = 999;
    static int SECOND_RANGE = 9999;

    static int InputSize()
    {
        int number = ReadPositiveIntegerFromConsole();
        return number;
    }

    static int ReadPositiveIntegerFromConsole()
    {
        while (true)
        {
            Console.Write("Enter a size(positive integer) : ");
            string input = Console.ReadLine();
            if (int.TryParse(input, out int number) && number > 0)
            {
                return number;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a positive integer.");
            }
        }
    }

    public static bool TryReadInt(string prompt, out int result)
    {
        Console.Write(prompt);
        string input = Console.ReadLine();

        // try to parse the input as an integer
        if (int.TryParse(input, out result))
        {
            return true;
        }

        result = 0;
        return false;
    }

    public static int[] EnterIntegerArray()
    {
        int size = InputSize();
        int[] array = new int[size];
        int currentIndex = 0;
        Console.WriteLine($"Enter {size} elements: ");

        while (currentIndex < array.Length)
        {
            // prompt the user to enter a number and handle any errors
            if (TryReadInt($"Enter number[{currentIndex}] between {FIRST_RANGE} and {SECOND_RANGE}: ", out int number))
            {
                // check if the number is in the valid range
                if (number >= FIRST_RANGE && number <= SECOND_RANGE)
                {
                    array[currentIndex] = number;
                    currentIndex++;
                }
                else
                {
                    Console.WriteLine("Error: Number is not in the valid range.");
                }
            }
            else
            {
                Console.WriteLine("Error: Invalid input.");
            }
        }

        return array;
    }

    public static void ShowArray(int[] array)
    {
        Console.Write("Array: [");

        foreach (int element in array)
        {
            Console.Write($" {element} ");
        }

        Console.Write("]");
    }

    public static int[] GenerateArray()
    {
        int length = InputSize();
        Random random = new Random();
        int[] random_array = new int[length];

        for (int i = 0; i < length; i++)
        {
            random_array[i] = random.Next(FIRST_RANGE, SECOND_RANGE);
        }

        return random_array;
    }
}

