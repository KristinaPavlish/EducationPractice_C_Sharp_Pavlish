using EducationalPractice.Bll.Implementation_Task_2.Helper;
using EducationalPractice.Bll.Implementation_Task_2.Implementation;
using EducationalPractice.Bll.Implementation_Task_2.Models;

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
PatientList patientList = new PatientList();
PatientService patientService = new PatientService();
PatientListService patientListService = new PatientListService();
Patient patient = new Patient();
TextFileService textFileService = new TextFileService();


void AddPatient(PatientList patientList, Patient patient)
{
    patient = patientService.ReadPatient();
    if (patient != null)
    {
        patientListService.AddPatient(patientList, patient);
    }
    else
    {
        Console.WriteLine("Invalid patient");
    }
}

void DeletePatient(PatientList patientList)
{
    if (patientList.Patients == null)
    {
        Console.WriteLine("Patient list is empty");
        return;
    }
    Console.Write("Enter id to delete: ");
    string idToDelete = Console.ReadLine();
    patientListService.DeletePatientById(patientList, idToDelete);

    foreach (var pat in patientList.Patients)
    {
        Console.WriteLine(Helper.ToString(pat));
    }
}

void EditPatient(PatientList patientList)
{
    if (patientList.Patients == null)
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
    patientListService.EditPatientById(patientList, idToEdit, property, value);
    foreach (var pat in patientList.Patients)
    {
        Console.WriteLine(Helper.ToString(pat));
    }
}

void SavePatientsIntoFile(PatientList patientList)
{
    string fileName = textFileService.GetValidFileName();
    if (patientList.Patients == null)
    {
        Console.WriteLine("Patient list is empty");
        return;
    }
    textFileService.Save(patientList,fileName);
}

PatientList UploadPatientsFromFile(PatientList patientList)
{
     string fileName = textFileService.GetValidFileName();
     PatientListService patientListService = new PatientListService();
     PatientList uploadedPatientList = new PatientList();
     uploadedPatientList = textFileService.Upload(fileName);
     if (uploadedPatientList.Patients.Count != 0)
     {
         foreach (var patient in uploadedPatientList.Patients)
         {
             patientListService.AddPatient(patientList, patient);
         }
     }
     var serializedPatientList = Helper.Serialize(uploadedPatientList);
     Console.WriteLine(serializedPatientList);
     return patientList;
}

void SortPatient(PatientList patientList)
{
    if (patientList.Patients == null)
    {
        Console.WriteLine("Patient list is empty");
        return;
    }
    PatientList sortedPatientList = new PatientList();
    Console.WriteLine("Enter property to sort");
    string sortBy = Console.ReadLine();
    sortedPatientList =  patientListService.SortBy(patientList, sortBy);
    foreach (var pati in sortedPatientList.Patients)
    {
        Console.WriteLine(Helper.ToString(pati));
    }
}

void SearchPatients(PatientList patientList)
{
    if (patientList.Patients == null)
    {
        Console.WriteLine("Patient list is empty");
        return;
    }
    PatientList newPatientList = new PatientList();
    Console.WriteLine("Enter value to search");
    string search = Console.ReadLine();
    newPatientList =  patientListService.Search(patientList, search);
    if (newPatientList.Patients == null)
    {
        Console.WriteLine("Patient searched list is empty");
        return;
    }
    foreach (var pati in newPatientList.Patients)
    {
        Console.WriteLine(Helper.ToString(pati));
    }
}

void ViewPatientList(PatientList patientList)
{
    if (patientList.Patients == null)
    {
        Console.WriteLine("Patient list is empty");
        return;
    }

    Console.WriteLine($"{patientList.Patients.Count} patients");
    foreach (var pati in patientList.Patients)
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
                RunWithRetry(() => AddPatient(patientList, patient));
                break;
            case "2":
                // Видалити пацієнта
                RunWithRetry(() => DeletePatient(patientList));
                break;
            case "3":
                // Редагувати пацієнта
                RunWithRetry(() => EditPatient(patientList));
                break;
            case "4":
                // Записати пацієнтів в файл
                SavePatientsIntoFile(patientList);
                break;
            case "5":
                // Записати пацієнтів в файл
                patientList = UploadPatientsFromFile(patientList);
                break;
            case "6":
                // Посортувати пацієнтів
                RunWithRetry(() =>  SortPatient(patientList));
               
                break;
            case "7":
                // Пошук
                SearchPatients(patientList);
                break;
            case "8":
                // Переглянути список пацієнтів
                ViewPatientList(patientList);
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