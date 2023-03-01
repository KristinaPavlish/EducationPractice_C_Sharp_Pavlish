
using EducationalPractice.Bll.Implementation_Task_1;

void Menu()
{
    while (true)
    {
        Console.WriteLine("MENU:");
        Console.WriteLine("[1] - Enter element from keyboard");
        Console.WriteLine("[2] - Generate an arbitrary array");
        Console.WriteLine("[3] - Exit");

        string number = Console.ReadLine();

        if (number == "1")
        {
            int[] entered_array = FillArray.EnterIntegerArray();
            WhatToDoWithArray(entered_array);
        }
        else if (number == "2")
        {
            int[] random_array = FillArray.GenerateArray();
            WhatToDoWithArray(random_array);
        }
        else if (number == "3")
        {
            break;
        }
    }
}
void WhatToDoWithArray(int[] array)
{
    while (true)
    {
        int[] clonedArray = (int[])array.Clone();
        Console.WriteLine();
        Console.WriteLine("MENU:");
        Console.WriteLine("[1] - Show array");
        Console.WriteLine("[2] - Show compressed array");
        Console.WriteLine("[3] - Go to entering array");

        string number = Console.ReadLine();

        if (number == "1")
        {
            FillArray.ShowArray(clonedArray);
        }
        else if (number == "2")
        {
            int[] arrayForCompress = Compress.CompressTheArray(clonedArray);
            FillArray.ShowArray(arrayForCompress);
        }
        else if (number == "3")
        {
            break;
        }
    }
}

Menu();