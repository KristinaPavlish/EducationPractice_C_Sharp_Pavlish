using EducationalPractice.Bll.Implementation_Task_3.Models;

namespace EducationalPractice.Bll.Implementation_Task_3.Interfaces;

public interface ICollectionService
{
    public void AddObject<T>(MyCollection<T> myCollection, T someClass);
    public void DeleteObjectById<T>(MyCollection<T> myCollection, string id);
    public void EditObjectById<T>(MyCollection<T> myCollection, string id, string property, string value);
 
}