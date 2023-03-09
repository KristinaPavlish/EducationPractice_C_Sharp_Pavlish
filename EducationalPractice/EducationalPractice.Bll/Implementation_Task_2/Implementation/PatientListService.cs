using System.Reflection;
using System.Text;
using EducationalPractice.Bll.Implementation_Task_2.Interfaces;
using EducationalPractice.Bll.Implementation_Task_2.Models;

namespace EducationalPractice.Bll.Implementation_Task_2.Implementation;

public class PatientListService : IPatientListService
{
    public void AddPatient(PatientList patientList, Patient patient)
    {
        if (patientList == null)
        {
            throw new Exception("Patient list is not provided");
        }
        if (patientList.Patients == null || patientList.Patients.Count == 0)
        {
            patientList.Patients = new List<Patient>();
        }
        patientList.Patients.Add(patient);
        
    }

    public void DeletePatientById(PatientList patientList, string id)
    {
        Patient patient = patientList.Patients.FirstOrDefault(p => p.PatientId.ToString() == id);
        if (patient == null)
        {
            throw new ArgumentException("Id " + id + " does not exist");
        }
        patientList.Patients.RemoveAll(x => x.PatientId.ToString() == id);
    }

    public void EditPatientById(PatientList patientList, string id, string property, string value)
    {
        Patient patient1 = patientList.Patients.FirstOrDefault(p => p.PatientId.ToString() == id);
        if (patient1 == null)
        {
            throw new ArgumentException("Id " + id + " does not exist");
        }
        
        var t = typeof(Patient);
        PropertyInfo[] props = t.GetProperties();
        Patient patient = new Patient();
        foreach (var pati in patientList.Patients)
        {
            if (pati.PatientId.ToString() == id)
            {
                patient = pati;
            }
        }
        PropertyInfo fieldInfo = t.GetProperty(property);
        Console.WriteLine(fieldInfo);
        foreach (var prop in props)
        {
            if (prop.Name == property)
            {
                fieldInfo.SetValue(patient, value);
            }
            
        }
    }

    public PatientList Search(PatientList patientList, string search)
    {
        PatientListService patientListService = new PatientListService();
        PatientList searchedPatientList = new PatientList();
        Type objectType = typeof(Patient);
        FieldInfo[] fields = objectType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        StringBuilder sb = new StringBuilder();
        List<string> stringArrayPatients = new List<string>();
        string strPatient;
        foreach (var patient in patientList.Patients)
        {
            sb.Clear();
            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(patient);
                sb.Append(value + " ");
            }
            strPatient = sb.ToString();
            stringArrayPatients.Add(strPatient);
        }

        for (int i = 0; i < patientList.Patients.Count; i++)
        {
            if (stringArrayPatients[i].Contains(search))
            {
                patientListService.AddPatient(searchedPatientList, patientList.Patients[i]);
            }
        }

        if (searchedPatientList.Patients == null)
        {
            Console.WriteLine($"No patients who contain {search}");
        }
        return searchedPatientList;
    }


    public PatientList? SortBy(PatientList patientList, string sortBy)
    {
        var t = typeof(Patient);
        PropertyInfo[] props = t.GetProperties();
        PropertyInfo prop = typeof(Patient).GetProperty(sortBy);
        if (prop == null)
        {
            throw new ArgumentException("Property " + sortBy + " does not exist");
        }
        IOrderedEnumerable<Patient> sortedPatientList = null;
        PatientListService patientListService = new PatientListService();
        PatientList orderedPatientList = new PatientList();
        foreach (var property in props)
        {
            if (property.Name == sortBy)
            {
                sortedPatientList = patientList.Patients.OrderBy(x => prop.GetValue(x, null));

            }
        }

        foreach (var patient in sortedPatientList)
        {
            patientListService.AddPatient(orderedPatientList, patient);
        }
        if (sortedPatientList == null)
        {
            throw new ArgumentException("Property " + sortBy + " does not exist");
        }
        
        return orderedPatientList;
    }
}