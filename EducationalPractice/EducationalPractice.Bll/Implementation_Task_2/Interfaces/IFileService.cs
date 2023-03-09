using EducationalPractice.Bll.Implementation_Task_2.Models;

namespace EducationalPractice.Bll.Implementation_Task_2.Interfaces;

public interface IFileService
{
    void Save(PatientList patientList, string fileName);
    PatientList Upload(string fileName);
    string GetValidFileName();
}