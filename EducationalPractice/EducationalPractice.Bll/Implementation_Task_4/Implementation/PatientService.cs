using System.Reflection;
using EducationalPractice.Bll.Implementation_Task_4.Interfaces;
using EducationalPractice.Bll.Implementation_Task_4.Models;

namespace EducationalPractice.Bll.Implementation_Task_4.Implementation;


public class PatientService : IPatientService
{
    public Patient ReadPatient()
    {
        bool isValid = true;
        Patient patient = new Patient();

        Console.WriteLine("Enter patient: ");
        string patientString = "";
        string value;
        Guid guid = Guid.NewGuid();
        Type type = typeof(Patient);
        IList<PropertyInfo> props = new List<PropertyInfo>(type.GetProperties());
        for (int i = 0; i < props.Count; i++)
        {
            if (props[i].PropertyType == typeof(Guid))
            {
                guid = Guid.NewGuid();
                continue;
            }
            patientString += props[i].Name + "=";
            Console.Write(props[i].Name + "=");
            value = Console.ReadLine();
            patientString += value;
            patientString += ", ";
        }
        (isValid, patient) = Helper.Helper.Deserialize<Patient>(patientString.Remove(patientString.Length - 2));
        if (isValid)
        {
            patient.PatientId = guid;
            return patient;
        }
        return null;
    }
}