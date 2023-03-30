using System.Reflection;
using EducationalPractice.Bll.Implementation_Task_2.Interfaces;
using EducationalPractice.Bll.Implementation_Task_2.Models;

namespace EducationalPractice.Bll.Implementation_Task_2.Implementation;


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
    public void Save(PatientList patientList, string fileName)
    {
        string toString = "";
        string stringOfPatients = "";
        foreach (var patient in patientList.Patients)
        {
            Type t = patient.GetType();
            PropertyInfo[] props = t.GetProperties();

            for (int i = 0; i < props.Length - 1; i++)
            {
                toString += props[i].ToString()!.Split(" ")[1] + "=" + props[i].GetValue(patient) + ", ";
            }

            var strPatient = toString.Remove(toString.Length - 2) + "\n";
            stringOfPatients += strPatient;
            toString = "";
        }

        Console.WriteLine(stringOfPatients);
        File.WriteAllText(fileName, stringOfPatients);
    }

    public PatientList Upload(string fileName)
    {
        bool isValid = true;
        int i = 0;
        PatientService patientService = new PatientService();
        PatientList patientList = new PatientList();
        patientList.Patients = new List<Patient>();
        var lines = File.ReadAllLines(fileName);
        Console.WriteLine($"____ {lines.Length} patients in file ____");
        foreach (var line in lines)
        {
            i += 1;
            Patient patient = new Patient();
            Console.WriteLine($" {i} - patient`s errors");
            (isValid, patient) = Helper.Helper.Deserialize<Patient>(line);
            if (isValid)
            {
                patient.PatientId = Guid.NewGuid();
                patientList.Patients.Add(patient);
            }
        }

        return patientList;
    }
}