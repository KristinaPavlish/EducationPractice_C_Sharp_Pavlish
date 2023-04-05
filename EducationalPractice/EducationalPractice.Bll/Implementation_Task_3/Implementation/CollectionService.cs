using System.Reflection;
using System.Text;
using EducationalPractice.Bll.Implementation_Task_3.Interfaces;
using EducationalPractice.Bll.Implementation_Task_3.Models;

namespace EducationalPractice.Bll.Implementation_Task_3.Implementation;

public class CollectionService : ICollectionService
{
    public void AddObject<T>(MyCollection<T> myCollection, T someClass)
    {
        if (myCollection == null)
        {
            throw new Exception("Patient list is not provided");
        }

        if (myCollection.CollectionList == null || myCollection.CollectionList.Count == 0)
        {
            myCollection.CollectionList = new List<T>();
        }

        myCollection.CollectionList.Add(someClass);

    }

    public void DeleteObjectById<T>(MyCollection<T> myCollection, string id)
    {
        Type objectType = typeof(T);
        FieldInfo[] fields = objectType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        T someClass = Activator.CreateInstance<T>();
        foreach (var field in fields)
        {
            if (field.FieldType == typeof(Guid))
            {
                someClass = myCollection.CollectionList.FirstOrDefault(p => field.GetValue(p).ToString() == id);
            }
        }

        if (someClass == null)
        {
            throw new ArgumentException("Id " + id + " does not exist");
        }
        foreach (var field in fields)
        {
            if (field.FieldType == typeof(Guid))
            {
                myCollection.CollectionList.RemoveAll(x => field.GetValue(x).ToString() == id);
            }
        }
    }

    public void EditObjectById<T>(MyCollection<T> myCollection, string id, string property, string value)
    {
        PropertyInfo propert = typeof(T).GetProperty(property);
        if (propert == null)
        { 
            throw new ArgumentException("Property " + property + " does not exist");
        }
        Type objectType = typeof(T);
        FieldInfo[] fields = objectType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);


        foreach (var field in fields)
        {
            if (field.FieldType == typeof(Guid))
            {
                var someClass = myCollection.CollectionList.FirstOrDefault(p => field.GetValue(p).ToString() == id);
                if (someClass == null)
                {
                    throw new ArgumentException("Id " + id + " does not exist");
                }

                var t = typeof(T);
                PropertyInfo[] props = t.GetProperties();
                
                T newClass = Activator.CreateInstance<T>();
                foreach (var obje in myCollection.CollectionList)
                {
                    if (field.FieldType == typeof(Guid))
                    {
                        if (field.GetValue(obje).ToString() == id)
                        {
                            newClass = obje;
                        }
                    }
                }

                PropertyInfo fieldInfo = t.GetProperty(property);
                Console.WriteLine(fieldInfo);
                foreach (var prop in props)
                {
                    if (prop.Name == property)
                    {
                        fieldInfo.SetValue(newClass, value);
                    }
                }
            }
            
        }
      
    }

    public MyCollection<T> Search<T>(MyCollection<T> myCollection, string search)
    {
        CollectionService collectionService = new CollectionService();
        MyCollection<T> searchedMyCollection = new MyCollection<T>();
        Type objectType = typeof(T);
        FieldInfo[] fields = objectType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        StringBuilder sb = new StringBuilder();
        List<string> stringArrayCollections = new List<string>();
        string strCollection;
        foreach (var item in myCollection.CollectionList)
        {
            sb.Clear();
            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(item);
                sb.Append(value + " ");
            }

            strCollection = sb.ToString();
            stringArrayCollections.Add(strCollection);
        }

/*        int j = 0;
        foreach (var item in myCollection.CollectionList)
        {
            j += 1;
            if (stringArrayCollections[j].Contains(search))
            {
                collectionService.AddObject(searchedMyCollection, item);
            }
        }
*/
        for (int i = 0; i < myCollection.CollectionList.Count; i++)
        {
            if (stringArrayCollections[i].Contains(search))
            {
                collectionService.AddObject(searchedMyCollection, myCollection.CollectionList[i]);
            }
        }

        if (searchedMyCollection.CollectionList == null)
        {
            Console.WriteLine($"No objects who contain {search}");
        }

        return searchedMyCollection;
    }


    public MyCollection<T>? SortBy<T>(MyCollection<T> myCollection, string sortBy)
    {
        var t = typeof(T);
        PropertyInfo[] props = t.GetProperties();
        PropertyInfo prop = typeof(T).GetProperty(sortBy);
        if (prop == null)
        {
            throw new ArgumentException("Property " + sortBy + " does not exist");
        }

        IOrderedEnumerable<T> sortedCollection = null;
        CollectionService collectionService = new CollectionService();
        MyCollection<T> orderedMyCollection = new MyCollection<T>();
        foreach (var property in props)
        {
            if (property.Name == sortBy)
            {
                sortedCollection = myCollection.CollectionList.OrderBy(x => prop.GetValue(x, null));

            }
        }

        foreach (var item in sortedCollection)
        {
            collectionService.AddObject(orderedMyCollection, item);
        }

        if (sortedCollection == null)
        {
            throw new ArgumentException("Property " + sortBy + " does not exist");
        }

        return orderedMyCollection;
    }
}