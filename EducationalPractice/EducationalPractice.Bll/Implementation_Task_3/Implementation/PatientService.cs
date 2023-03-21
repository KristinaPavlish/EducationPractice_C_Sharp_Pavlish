using System.Reflection;
using EducationalPractice.Bll.Implementation_Task_3.Interfaces;
using EducationalPractice.Bll.Implementation_Task_3.Models;

namespace EducationalPractice.Bll.Implementation_Task_3.Implementation;


public class PatientService : IPatientService
{
    public Patient ReadPatient()
    {
        bool isValid = true;
        Patient patient = new Patient();

        Console.WriteLine("Enter patient: ");
        string patientString = "";
        string value;
        Type type = typeof(Patient);
        IList<PropertyInfo> props = new List<PropertyInfo>(type.GetProperties());
        for (int i = 0; i < props.Count; i++)
        {
            if (props[i].PropertyType == typeof(Guid))
            {
                i++;
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
            patient.PatientId = Guid.NewGuid();
            return patient;
        }
        return null;
    }
}