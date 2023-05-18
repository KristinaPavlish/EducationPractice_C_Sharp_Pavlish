using EducationalPractice.Bll.Implementation_Task_5.Models;

namespace EducationalPractice.Bll.Implementation_Task_5.Interfaces;

public interface IFileService
{

    public void Save<T>(MyCollection<T> myCollection, string fileName);
    MyCollection<T> Upload<T>(string fileName) where T : new();
    string GetValidFileName();

}