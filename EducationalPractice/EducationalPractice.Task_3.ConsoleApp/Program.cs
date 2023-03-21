

using System.Collections.ObjectModel;
using EducationalPractice.Bll.Implementation_Task_3.Helper;
using EducationalPractice.Bll.Implementation_Task_3.Implementation;
using EducationalPractice.Bll.Implementation_Task_3.Models;
using Patient = EducationalPractice.Bll.Implementation_Task_3.Models.Patient;


static void RunWithRetry(Action action)
{
    bool success = false;
    while (!success)
    {
        try
        {
            action.Invoke();
            success = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error occurred: " + ex.Message);
            Console.WriteLine("Please try again.");
        }
    }
}

PatientService patientService = new PatientService();
CollectionService collectionService = new CollectionService();
Patient patient = new Patient();
TextFileService textFileService = new TextFileService();
MyCollection<Patient> patientCollection = new MyCollection<Patient>();
void AddPatient()
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

void DeletePatient()
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
        Console.WriteLine(Helper.ToString(pat));
    }
}

void EditPatient()
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
        Console.WriteLine(Helper.ToString(pat));
    }
}

void SavePatientsIntoFile()
{
    string fileName = textFileService.GetValidFileName();
    if (patientCollection.CollectionList == null)
    {
        Console.WriteLine("Patient list is empty");
        return;
    }
    textFileService.Save(patientCollection,fileName);
}

MyCollection<Patient> UploadPatientsFromFile()
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
     var serializedPatientList = Helper.Serialize(uploadedPatientList);
     Console.WriteLine(serializedPatientList);
     return patientCollection;
}



void SortPatient()
{
    if (patientCollection.CollectionList == null)
    {
        Console.WriteLine("Patient list is empty");
        return;
    }
    MyCollection<Patient> sortedPatientList = new MyCollection<Patient> ();
    Console.WriteLine("Enter property to sort");
    string sortBy = Console.ReadLine();
    sortedPatientList =  collectionService.SortBy(patientCollection, sortBy);
    foreach (var pati in sortedPatientList.CollectionList)
    {
        Console.WriteLine(Helper.ToString(pati));
    }
}

void SearchPatients()
{
    if (patientCollection.CollectionList == null)
    {
        Console.WriteLine("Patient list is empty");
        return;
    }
    MyCollection<Patient> newPatientList = new MyCollection<Patient>();
    Console.WriteLine("Enter value to search");
    string search = Console.ReadLine();
    newPatientList =  collectionService.Search(patientCollection, search);
    if (newPatientList.CollectionList == null)
    {
        Console.WriteLine("Patient searched list is empty");
        return;
    }
    foreach (var pati in newPatientList.CollectionList)
    {
        Console.WriteLine(Helper.ToString(pati));
    }
}

void ViewPatientList()
{
    if (patientCollection.CollectionList == null)
    {
        Console.WriteLine("Patient list is empty");
        return;
    }

    Console.WriteLine($"{patientCollection.CollectionList.Count} patients");
    foreach (var pati in patientCollection.CollectionList)
    {
        Console.WriteLine(Helper.ToString(pati));
    }
}

void Main()
{
    while (true)
    {
        Console.WriteLine("1. Додати пацієнта");
        Console.WriteLine("2. Видалити пацієнта");
        Console.WriteLine("3. Редагувати пацієнта");
        Console.WriteLine("4. Записати пацієнтів в файл");
        Console.WriteLine("5. Зчитати пацієнтів з файлу");
        Console.WriteLine("6. Посортувати пацієнтів");
        Console.WriteLine("7. Пошук");
        Console.WriteLine("8. Переглянути список пацієнтів");
        Console.WriteLine("9. Вийти з програми");

        Console.Write("Ваш вибір: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                // Додати пацієнта
                RunWithRetry(() => AddPatient());
                break;
            case "2":
                // Видалити пацієнта
                RunWithRetry(() => DeletePatient());
                break;
            case "3":
                // Редагувати пацієнта
                RunWithRetry(() => EditPatient());
                break;
            case "4":
                // Записати пацієнтів в файл
                SavePatientsIntoFile();
                break;
            case "5":
                // Записати пацієнтів в файл
                patientCollection = UploadPatientsFromFile();
                break;
            case "6":
                // Посортувати пацієнтів
                RunWithRetry(() =>  SortPatient());
               
                break;
            case "7":
                // Пошук
                SearchPatients();
                break;
            case "8":
                // Переглянути список пацієнтів
                ViewPatientList();
                break;
            case "9":
                // Вийти з програми
                Console.WriteLine("До побачення!");
                return;
            default:
                Console.WriteLine("Неправильний вибір. Будь ласка, спробуйте знову.");
                break;
        }

        Console.WriteLine();
    }
}




Car car = new Car();
MyCollection<Car> carCollection = new MyCollection<Car>();

void DeleteCar()
{
    if (carCollection.CollectionList == null)
    {
        Console.WriteLine("Car list is empty");
        return;
    }
    Console.Write("Enter id to delete: ");
    string idToDelete = Console.ReadLine();
    collectionService.DeleteObjectById(carCollection, idToDelete);

    foreach (var pat in carCollection.CollectionList)
    {
        Console.WriteLine(Helper.ToString(pat));
    }
}

void EditCar()
{
    if (carCollection.CollectionList == null)
    {
        Console.WriteLine("Car list is empty");
        return;
    }
    Console.Write("Enter id to edit: ");
    string idToEdit = Console.ReadLine();
    Console.Write("Enter property: ");
    string property = Console.ReadLine();
    Console.Write("Enter value: ");
    string value = Console.ReadLine();
    collectionService.EditObjectById(carCollection, idToEdit, property, value);
    foreach (var pat in carCollection.CollectionList)
    {
        Console.WriteLine(Helper.ToString(pat));
    }
}

void SaveCarsIntoFile()
{
    string fileName = textFileService.GetValidFileName();
    if (carCollection.CollectionList == null)
    {
        Console.WriteLine("Patient list is empty");
        return;
    }
    textFileService.Save(carCollection,fileName);
}

MyCollection<Car> UploadCarsFromFile()
{
     string fileName = textFileService.GetValidFileName();
     MyCollection<Car> uploadedPatientList = new MyCollection<Car>();
     uploadedPatientList = textFileService.Upload<Car>(fileName);
     if (uploadedPatientList.CollectionList.Count != 0)
     {
         foreach (var uplcar in uploadedPatientList.CollectionList)
         {
             collectionService.AddObject(carCollection, uplcar);
         }
     }
     var serializedPatientList = Helper.Serialize(uploadedPatientList);
     Console.WriteLine(serializedPatientList);
     return carCollection;
}

void SortCar()
{
    if (carCollection.CollectionList == null)
    {
        Console.WriteLine("Patient list is empty");
        return;
    }
    MyCollection<Car> sortedPatientList = new MyCollection<Car> ();
    Console.WriteLine("Enter property to sort");
    string sortBy = Console.ReadLine();
    sortedPatientList =  collectionService.SortBy(carCollection, sortBy);
    foreach (var pati in sortedPatientList.CollectionList)
    {
        Console.WriteLine(Helper.ToString(pati));
    }
}

void SearchCars()
{
    if (carCollection.CollectionList == null)
    {
        Console.WriteLine("Car list is empty");
        return;
    }
    MyCollection<Car> newPatientList = new MyCollection<Car>();
    Console.WriteLine("Enter value to search");
    string search = Console.ReadLine();
    newPatientList =  collectionService.Search(carCollection, search);
    if (newPatientList.CollectionList == null)
    {
        Console.WriteLine("Car searched list is empty");
        return;
    }
    foreach (var pati in newPatientList.CollectionList)
    {
        Console.WriteLine(Helper.ToString(pati));
    }
}

void ViewCarList()
{
    if (carCollection.CollectionList == null)
    {
        Console.WriteLine("Car list is empty");
        return;
    }

    Console.WriteLine($"{carCollection.CollectionList.Count} cars");
    foreach (var pati in carCollection.CollectionList)
    {
        Console.WriteLine(Helper.ToString(pati));
    }
}

void MainCar()
{
    while (true)
    {
        Console.WriteLine("1. Додати машину");
        Console.WriteLine("2. Видалити машину");
        Console.WriteLine("3. Редагувати машину");
        Console.WriteLine("4. Записати машини в файл");
        Console.WriteLine("5. Зчитати машини з файлу");
        Console.WriteLine("6. Посортувати машини");
        Console.WriteLine("7. Пошук");
        Console.WriteLine("8. Переглянути список машин");
        Console.WriteLine("9. Вийти з програми");

        Console.Write("Ваш вибір: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                // Додати пацієнта
               
                break;
            case "2":
                // Видалити пацієнта
                RunWithRetry(() => DeleteCar());
                break;
            case "3":
                // Редагувати пацієнта
                RunWithRetry(() => EditCar());
                break;
            case "4":
                // Записати пацієнтів в файл
                SaveCarsIntoFile();
                break;
            case "5":
                // Записати пацієнтів в файл
                carCollection = UploadCarsFromFile();
                break;
            case "6":
                // Посортувати пацієнтів
                RunWithRetry(() =>  SortCar());
               
                break;
            case "7":
                // Пошук
                SearchCars();
                break;
            case "8":
                // Переглянути список пацієнтів
                ViewCarList();
                break;
            case "9":
                // Вийти з програми
                Console.WriteLine("До побачення!");
                return;
            default:
                Console.WriteLine("Неправильний вибір. Будь ласка, спробуйте знову.");
                break;
        }

        Console.WriteLine();
    }
}

Main();
MainCar();