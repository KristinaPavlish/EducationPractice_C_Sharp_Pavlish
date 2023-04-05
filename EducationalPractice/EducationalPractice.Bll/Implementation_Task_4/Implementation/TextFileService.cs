using System.Reflection;
using EducationalPractice.Bll.Implementation_Task_4.Interfaces;
using EducationalPractice.Bll.Implementation_Task_4.Models;

namespace EducationalPractice.Bll.Implementation_Task_4.Implementation;


public class TextFileService : IFileService
{

    public string GetValidFileName()
    {
        string fileName = "";
        bool fileExists = false;

        while (!fileExists)
        {
            Console.WriteLine("Введіть назву текстового файлу:");
            fileName = Console.ReadLine();

            if (File.Exists(fileName))
            {
                fileExists = true;
                Console.WriteLine("Файл існує.");
            }
            else
            {
                Console.WriteLine("Файл не існує. Спробуйте ще раз.");
            }
        }

        return fileName;
    }
    public void Save<T>(MyCollection<T> myCollection, string fileName)
    {
        string toString = "";
        string stringOfObjects = "";
        foreach (var item in myCollection.CollectionList)
        {
            Type t = item.GetType();
            PropertyInfo[] props = t.GetProperties();

            for (int i = 0; i < props.Length - 1; i++)
            {
                toString += props[i].ToString()!.Split(" ")[1] + "=" + props[i].GetValue(myCollection.CollectionList[0]) + ", ";
            }

            var strObject = toString.Remove(toString.Length - 2) + "\n";
            stringOfObjects += strObject;
            toString = "";
        }

        Console.WriteLine(stringOfObjects);
        File.WriteAllText(fileName, stringOfObjects);
    }

    public MyCollection<T> Upload<T>(string fileName) where T : new()
    {
        bool isValid = true;
        int i = 0;
        MyCollection<T> myCollection = new MyCollection<T>();
        myCollection.CollectionList = new List<T>();
        var lines = File.ReadAllLines(fileName);
        Console.WriteLine($"____ {lines.Length} objects in file ____");
        foreach (var line in lines)
        {
            i += 1;
            T someObj = new T();
            Console.WriteLine($" {i} - objects`s errors");
            (isValid, someObj) = Helper.Helper.Deserialize<T>(line);
            if (isValid)
            {
                Type objectType = typeof(T);
                FieldInfo[] fields = objectType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (var field in fields)
                {
                    if (field.FieldType == typeof(Guid))
                    {
                        field.SetValue(someObj, Guid.NewGuid());
                    }
                }

                myCollection.CollectionList.Add(someObj);
            }
        }
        return myCollection;
    }
}