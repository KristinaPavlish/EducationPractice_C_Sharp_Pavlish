using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using EducationalPractice.Bll.Implementation_Task_5.Models;

namespace EducationalPractice.Bll.Implementation_Task_5.Helper;

  
public class Helper
{
    public static string Serialize<T>(MyCollection<T> myCollection)
    {
        string stringOfObjects = "";
        foreach (var item in myCollection.CollectionList)
        {
            string strObject = ToString(item);
            stringOfObjects += strObject;
        }

        return stringOfObjects;
    }

    public static( bool, T) Deserialize<T>(string inputObjectString)
    {
        bool isValid = true;
        Type type = typeof(T);
        var obj = Activator.CreateInstance(type);
        foreach (var line in inputObjectString.Split(", ").ToList())
        {
            var keyVal = line.Split('=');
            if (keyVal.Length != 2) continue;

            var prop = type.GetProperty(keyVal[0].Trim());
            if (prop != null)
            {
                var value = keyVal[1].Trim();
                try
                {
                    if (prop.PropertyType == typeof(DateOnly))
                    {
                        prop.SetValue(obj,
                            DateOnly.ParseExact(value, "dd.MM.yyyy", CultureInfo.InvariantCulture));
                    }
                    else if (prop.PropertyType == typeof(TimeOnly))
                    {
                        prop.SetValue(obj,
                            TimeOnly.ParseExact(value, "HH:mm", CultureInfo.InvariantCulture));
                    }
                    else if (prop.PropertyType == typeof(DateTime))
                    {
                        prop.SetValue(obj,
                            DateTime.ParseExact(value, "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture));
                    }
                    else if (prop.PropertyType == typeof(uint))
                    {
                        prop.SetValue(obj, uint.Parse(value));
                    }
                    else if (prop.PropertyType == typeof(int))
                    {
                        prop.SetValue(obj, int.Parse(value));
                    }
                    else if (prop.PropertyType == typeof(State))
                    {
                        State enumValue = (State)Enum.Parse(typeof(State), value);
                        prop.SetValue(obj, enumValue);
                    }
                    else if (prop.PropertyType == typeof(string))
                    {
                        var nameRegex = new Regex("^[a-zA-Z]+$");

                        if (!nameRegex.IsMatch(value))
                        {
                            throw new ArgumentException(prop.Name + " " + value + " can only contain letters.");
                        }

                        prop.SetValue(obj, Convert.ChangeType(value, prop.PropertyType));
                    }
                    else
                    {
                        prop.SetValue(obj, Convert.ChangeType(value, prop.PropertyType));
                    }
                }
                catch (ValidationException ex)
                {
                    isValid = false;
                    Console.WriteLine($"Error validating property {prop.Name}: {ex.Message}");
                }
                catch (Exception ex)
                {
                    isValid = false;
                    Console.WriteLine($"Error setting property {prop.Name}: {ex.Message}");
                }

            }
        }

        return (isValid, (T)obj);
    }

    public static string ToString(Object obj)
    {
        string toString = "";
        Type t = obj.GetType();
        PropertyInfo[] props = t.GetProperties();
        foreach (var prop in props)
        {
            toString += prop.ToString()!.Split(" ")[1] + "=" + prop.GetValue(obj) + ", ";
        }

        return toString.Remove(toString.Length - 2) + "\n";
    }


}