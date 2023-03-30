using EducationalPractice.Bll.Implementation_Task_2.Models;

namespace EducationalPractice.Bll.Implementation_Task_2.Interfaces;

public interface IPatientListService
{
    public void AddPatient(PatientList patientList, Patient patient);
    public void DeletePatientById(PatientList patientList, string id);
    public void EditPatientById(PatientList patientList, string id, string property, string value);
    public PatientList Search(PatientList patientList, string search);
}