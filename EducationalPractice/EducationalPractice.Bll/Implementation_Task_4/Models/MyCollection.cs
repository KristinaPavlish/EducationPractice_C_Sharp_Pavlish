namespace EducationalPractice.Bll.Implementation_Task_4.Models;

public class MyCollection<T>
{
    public List<T> CollectionList { get; set; }
    public T GetItem(int index) {
        return CollectionList[index];
    }
    public void SetItem(int index, T value) {
        CollectionList[index] = value;
    }

    public MyCollection(List<T> collectionList) {
        CollectionList = collectionList;
    }
    public MyCollection(){}
}