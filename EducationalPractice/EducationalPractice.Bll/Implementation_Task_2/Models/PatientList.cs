namespace EducationalPractice.Bll.Implementation_Task_2.Models;

public class PatientList
{
    public List<Patient> Patients { get; set; }

    public PatientList(){}

    public PatientList(List<Patient> patients)
    {
        Patients = patients;
    }

}