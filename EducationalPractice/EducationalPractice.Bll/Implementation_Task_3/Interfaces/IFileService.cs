using EducationalPractice.Bll.Implementation_Task_3.Models;

namespace EducationalPractice.Bll.Implementation_Task_3.Interfaces;

public interface IFileService
{

    public void Save<T>(MyCollection<T> myCollection, string fileName);
    MyCollection<T> Upload<T>(string fileName) where T : new();
    string GetValidFileName();

}