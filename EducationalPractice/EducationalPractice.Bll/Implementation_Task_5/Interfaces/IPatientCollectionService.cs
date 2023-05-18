using EducationalPractice.Bll.Implementation_Task_5.Models;

namespace EducationalPractice.Bll.Implementation_Task_5.Interfaces;

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