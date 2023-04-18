using EducationalPractice.Bll.Implementation_Task_4.Interfaces;
using EducationalPractice.Bll.Implementation_Task_4.Models;

namespace EducationalPractice.Bll.Implementation_Task_4.Implementation;

public class PatientCollectionService : IPatientCollectionService
{
    PatientService patientService = new PatientService();
    CollectionService collectionService = new CollectionService();
    Patient patient = new Patient();
    TextFileService textFileService = new TextFileService();
    MyCollection<Patient> patientCollection = new MyCollection<Patient>();
    public void AddPatient()
    {
        patient = patientService.ReadPatient();
        if (patient != null)
        {
            collectionService.AddObject(patientCollection, patient);
        }
        else
        {
            Console.WriteLine("Invalid patient");
        }
    }


    public void DeletePatient()
    {
        if (patientCollection.CollectionList == null)
        {
            Console.WriteLine("Patient list is empty");
            return;
        }

        Console.Write("Enter id to delete: ");
        string idToDelete = Console.ReadLine();
        collectionService.DeleteObjectById(patientCollection, idToDelete);

        foreach (var pat in patientCollection.CollectionList)
        {
            Console.WriteLine(Helper.Helper.ToString(pat));
        }
    }

    public void EditPatient()
    {
        if (patientCollection.CollectionList == null)
        {
            Console.WriteLine("Patient list is empty");
            return;
        }

        Console.Write("Enter id to edit: ");
        string idToEdit = Console.ReadLine();
        Console.Write("Enter property: ");
        string property = Console.ReadLine();
        Console.Write("Enter value: ");
        string value = Console.ReadLine();
        collectionService.EditObjectById(patientCollection, idToEdit, property, value);
        foreach (var pat in patientCollection.CollectionList)
        {
            Console.WriteLine(Helper.Helper.ToString(pat));
        }
    }

    public void SavePatientsIntoFile()
    {
        string fileName = textFileService.GetValidFileName();
        if (patientCollection.CollectionList == null)
        {
            Console.WriteLine("Patient list is empty");
            return;
        }

        textFileService.Save(patientCollection, fileName);
    }

    public MyCollection<Patient> UploadPatientsFromFile()
    {
        string fileName = textFileService.GetValidFileName();
        MyCollection<Patient> uploadedPatientList = new MyCollection<Patient>();
        uploadedPatientList = textFileService.Upload<Patient>(fileName);
        if (uploadedPatientList.CollectionList.Count != 0)
        {
            foreach (var patient in uploadedPatientList.CollectionList)
            {
                collectionService.AddObject(patientCollection, patient);
            }
        }

        var serializedPatientList = Helper.Helper.Serialize(uploadedPatientList);
        Console.WriteLine(serializedPatientList);
        return patientCollection;
    }



    public void SortPatient()
    {
        if (patientCollection.CollectionList == null)
        {
            Console.WriteLine("Patient list is empty");
            return;
        }

        MyCollection<Patient> sortedPatientList = new MyCollection<Patient>();
        Console.WriteLine("Enter property to sort");
        string sortBy = Console.ReadLine();
        sortedPatientList = collectionService.SortBy(patientCollection, sortBy);
        foreach (var pati in sortedPatientList.CollectionList)
        {
            Console.WriteLine(Helper.Helper.ToString(pati));
        }
    }

    public void SearchPatients()
    {
        if (patientCollection.CollectionList == null)
        {
            Console.WriteLine("Patient list is empty");
            return;
        }

        MyCollection<Patient> newPatientList = new MyCollection<Patient>();
        Console.WriteLine("Enter value to search");
        string search = Console.ReadLine();
        newPatientList = collectionService.Search(patientCollection, search);
        if (newPatientList.CollectionList == null)
        {
            Console.WriteLine("Patient searched list is empty");
            return;
        }

        foreach (var pati in newPatientList.CollectionList)
        {
            Console.WriteLine(Helper.Helper.ToString(pati));
        }
    }

    public void ViewPatientList()
    {
        if (patientCollection.CollectionList == null)
        {
            Console.WriteLine("Patient list is empty");
            return;
        }

        Console.WriteLine($"{patientCollection.CollectionList.Count} patients");
        foreach (var pati in patientCollection.CollectionList)
        {
            Console.WriteLine(Helper.Helper.ToString(pati));
        }
    }

    public void ViewPatient()
    {
        if (patientCollection.CollectionList == null)
        {
            Console.WriteLine("Patient list is empty");
            return;
        }

        Console.WriteLine("Введіть id");
        string id = Console.ReadLine();

        var pati = collectionService.SearchById(patientCollection, id);
        Console.WriteLine(Helper.Helper.ToString(pati));
    }
}