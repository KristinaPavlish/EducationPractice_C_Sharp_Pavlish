using EducationalPractice.Bll.Implementation_Task_5.Models;

namespace EducationalPractice.Bll.Implementation_Task_5.Interfaces;

public interface ICollectionService
{
    public void AddObject<T>(MyCollection<T> myCollection, T someClass);
    public void DeleteObjectById<T>(MyCollection<T> myCollection, string id);
    public void EditObjectById<T>(MyCollection<T> myCollection, string id, string property, string value);
    public object SearchById<T>(MyCollection<T> myCollection, string id);
    public MyCollection<T> Search<T>(MyCollection<T> myCollection, string search);
    public MyCollection<T>? SortBy<T>(MyCollection<T> myCollection, string sortBy);
    //proxyServer.ViewPatientList();

}