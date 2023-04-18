using EducationalPractice.Bll.Implementation_Task_4.Helper;
using EducationalPractice.Bll.Implementation_Task_4.Implementation;
using EducationalPractice.Bll.Implementation_Task_4.Interfaces;
using EducationalPractice.Bll.Implementation_Task_4.Models;
using System.IO;
using Microsoft.Extensions.Logging;
using ILoggerFactory = Microsoft.Extensions.Logging.ILoggerFactory;

public class Proxy : IPatientCollectionService
{
    public User user;
    PatientService patientService = new PatientService();
    CollectionService collectionService = new CollectionService();
    Patient patient = new Patient();
    TextFileService textFileService = new TextFileService();
    public MyCollection<Patient> patientCollection = new MyCollection<Patient>();

    private readonly string logPath =
        "E:\\EducationPractice_C_Sharp_Pavlish\\EducationalPractice\\EducationalPractice.Task_4.ConsoleApp\\Logger.txt";

    private readonly ILogger<Proxy> logger;

    public Proxy()
    {
    }

    public void AddPatient()
    {
        if (user.Role == UserRole.Admin)
        {
            patient = patientService.ReadPatient();
            if (patient != null)
            {
                patientCollection.CollectionList.Add(patient);
            }
            else
            {
                Console.WriteLine("Invalid patient");
            }
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"______________________ADD_________________________");
            }
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"[{DateTime.Now}] added by {user.Role} : {user.FirstName} {user.LastName} Patient {Helper.ToString(patient)}");
            }
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"_______________________________________________");
            }
        }
        else
        {
            throw new ArgumentException("Only admin can add patient");
        }
    }

    public void DeletePatient()
    {
        if (user.Role == UserRole.Admin)
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
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"______________________DELETE_________________________");
            }
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"[{DateTime.Now}] deleted by {user.Role} : {user.FirstName} {user.LastName} Patient [ {Helper.ToString(patient)} ] ");
            }
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"_______________________________________________");
            }
        }
        else
        {
            throw new ArgumentException("Only admin can delete patient");
        }
    }

    public void EditPatient()
    {
        if (user.Role == UserRole.Admin)
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
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"______________________EDIT_________________________");
            }
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"[{DateTime.Now}] eddited by {user.Role}: {user.FirstName} {user.LastName} Patient [ {Helper.ToString(patient)} ] (changed {property} to {value})");
            }
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"_______________________________________________");
            }
        }
        else
        {
            throw new ArgumentException("Only admin can edit patient");
        }
    }

    public void SavePatientsIntoFile()
    {
        if (user.Role == UserRole.Admin)
        {
            string fileName = textFileService.GetValidFileName();
            if (patientCollection.CollectionList == null)
            {
                Console.WriteLine("Patient list is empty");
                return;
            }

            textFileService.Save(patientCollection, fileName);
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"______________________SAVE_________________________");
                writer.WriteLine($"[{DateTime.Now}] saved by {user.Role} : {user.FirstName} {user.LastName} ");
            }
            foreach (var patient1 in patientCollection.CollectionList)
            {
                using (StreamWriter writer = File.AppendText(logPath))
                {
                    writer.WriteLine($"Patient [ {Helper.ToString(patient1)} ]");
                }
            }
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"_______________________________________________");
            }
        }
        else
        {
            throw new ArgumentException("Only admin can save patients to file patient");
        }
    }

    public void UploadPatients()
    {
        if (user.Role == UserRole.Admin)
        {
            string fileName = textFileService.GetValidFileName();
            MyCollection<Patient> uploadedPatientList = new MyCollection<Patient>();
            uploadedPatientList = textFileService.Upload<Patient>(fileName);

            if (uploadedPatientList.CollectionList.Count != 0)
            {
                foreach (var patient in uploadedPatientList.CollectionList)
                {
                    patientCollection.CollectionList.Add(patient);
                }
            }

            var serializedPatientList = Helper.Serialize(uploadedPatientList);
            Console.WriteLine(serializedPatientList);
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"______________________UPLOAD_________________________");
                writer.WriteLine($"[{DateTime.Now}] uploaded by {user.Role}: {user.FirstName} {user.LastName}");
            }

            foreach (var patient1 in patientCollection.CollectionList)
            {
                using (StreamWriter writer = File.AppendText(logPath))
                {
                    writer.WriteLine($"Patient [ {Helper.ToString(patient1)} ] ");
                }
            }
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"_______________________________________________");
            }
        }
        else
        {
            throw new ArgumentException("Only admin can delete patient");
        }
    }

    public MyCollection<Patient> UploadPatientsFromFile()
    {
        if (user.Role == UserRole.Admin)
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
        else
        {
            throw new ArgumentException("Only admin can delete patient");
        }
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
            Console.WriteLine(Helper.ToString(pati));
        }

        using (StreamWriter writer = File.AppendText(logPath))
        {
            writer.WriteLine($"______________________SAVE_________________________");
            writer.WriteLine(
                $"[{DateTime.Now}] sorted by {user.Role}: {user.FirstName} {user.LastName}");
        }

        foreach (var patient1 in sortedPatientList.CollectionList)
        {
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($" Patient: [ {Helper.ToString(patient1)} ] ");
            }
        }
        using (StreamWriter writer = File.AppendText(logPath))
        {
            writer.WriteLine($"_______________________________________________");
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
            Console.WriteLine(Helper.ToString(pati));
        }

        using (StreamWriter writer = File.AppendText(logPath))
        {
            writer.WriteLine($"______________________SEARCH_________________________");
            writer.WriteLine(
                $"[{DateTime.Now}] searched by {user.Role}: {user.FirstName} {user.LastName}  (word to search {search}) ");
        }

        foreach (var patient1 in newPatientList.CollectionList)
        {
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($" Patient [ {Helper.ToString(patient1)} ] ");
            }
        }
        using (StreamWriter writer = File.AppendText(logPath))
        {
            writer.WriteLine($"_______________________________________________");
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
            Console.WriteLine(Helper.ToString(pati));
        }

        using (StreamWriter writer = File.AppendText(logPath))
        {
            writer.WriteLine($"______________________VIEW_________________________");
            writer.WriteLine($"[{DateTime.Now}] viewed by {user.Role}: {user.FirstName} {user.LastName}");
        }

        foreach (var patient1 in patientCollection.CollectionList)
        {
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($" Patient [ {Helper.ToString(patient1)} ]  ");
            }
        }
        using (StreamWriter writer = File.AppendText(logPath))
        {
            writer.WriteLine($"_______________________________________________");
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
        Console.WriteLine(Helper.ToString(pati));

        using (StreamWriter writer = File.AppendText(logPath))
        {
            writer.WriteLine($"[{DateTime.Now}] Patient [ {Helper.ToString(pati)} ] viewed by {user.ToString()}");
        }

    }
}