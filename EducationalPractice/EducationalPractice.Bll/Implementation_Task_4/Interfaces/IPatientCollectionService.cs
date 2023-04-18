using EducationalPractice.Bll.Implementation_Task_4.Models;

namespace EducationalPractice.Bll.Implementation_Task_4.Interfaces;

public interface IPatientCollectionService
{

    void AddPatient();
    void DeletePatient();
    void EditPatient();
    void SavePatientsIntoFile();
    MyCollection<Patient> UploadPatientsFromFile();
    void SortPatient();
    void SearchPatients();
    void ViewPatientList();
    void ViewPatient();
}