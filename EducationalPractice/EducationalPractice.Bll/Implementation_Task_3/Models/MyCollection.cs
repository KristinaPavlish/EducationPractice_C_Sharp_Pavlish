namespace EducationalPractice.Bll.Implementation_Task_3.Models;

public class MyCollection<T>
{
    public List<T> CollectionList = new List<T>();

    public MyCollection(List<T> collectionList) {
        CollectionList = collectionList;
    }
    public MyCollection(){}
}