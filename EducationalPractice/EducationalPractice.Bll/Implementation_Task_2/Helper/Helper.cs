using EducationalPractice.Bll.Implementation_Task_2.Models;

namespace EducationalPractice.Bll.Implementation_Task_2.Helper;

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;


public static class Helper
{
    public static string Serialize(PatientList patientList)
    {
        string stringOfPatients = "";
        foreach (var patient in patientList.Patients)
        {
            string strPatient = ToString(patient);
            stringOfPatients += strPatient;
        }

        return stringOfPatients;
    }

    public static( bool, T) Deserialize<T>(string inputPatientString)
    {
        bool isValid = true;
        Type type = typeof(T);
        var obj = Activator.CreateInstance(type);
        foreach (var line in inputPatientString.Split(", ").ToList())
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
                    else if (prop.PropertyType == typeof(uint))
                    {
                        prop.SetValue(obj, uint.Parse(value));
                    }
                    else if (prop.PropertyType == typeof(int))
                    {
                        prop.SetValue(obj, int.Parse(value));
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

