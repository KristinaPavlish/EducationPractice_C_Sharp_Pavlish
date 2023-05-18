using EducationalPractice.Bll.Implementation_Task_5.Implementation;
using EducationalPractice.Bll.Implementation_Task_5.Interfaces;
using Microsoft.Extensions.Logging;

namespace EducationalPractice.Bll.Implementation_Task_5.Models;

public class Proxy : IPatientCollectionService
{
    public User user;
    PatientService patientService = new PatientService();
    CollectionService collectionService = new CollectionService();
    Patient patient = new Patient();
    TextFileService textFileService = new TextFileService();
    public MyCollection<Patient> patientCollection = new MyCollection<Patient>();

    private readonly string logPath =
        "E:\\EducationPractice_C_Sharp_Pavlish\\EducationalPractice\\EducationalPractice.Task_5.ConsoleApp\\Logger.txt";

    private readonly ILogger<Proxy> logger;

    public Proxy()
    {
    }

    public void AddPatient()
    {
        if (user.Role == UserRole.Admin || user.Role == UserRole.Manager)
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
                writer.WriteLine($"[{DateTime.Now}] added by {user.Role} : {user.FirstName} {user.LastName} Patient {Helper.Helper.ToString(patient)}");
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
                Console.WriteLine(Helper.Helper.ToString(pat));
            }
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"______________________DELETE_________________________");
            }
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"[{DateTime.Now}] deleted by {user.Role} : {user.FirstName} {user.LastName} Patient [ {Helper.Helper.ToString(patient)} ] ");
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
        if (user.Role == UserRole.Admin || user.Role == UserRole.Manager)
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
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"______________________EDIT_________________________");
            }
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"[{DateTime.Now}] eddited by {user.Role}: {user.FirstName} {user.LastName} Patient [ {Helper.Helper.ToString(patient)} ] (changed {property} to {value})");
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
    public void PublishPatient()
    {
        if (user.Role == UserRole.Admin )
        {
            if (patientCollection.CollectionList == null)
            {
                Console.WriteLine("Patient list is empty");
                return;
            }

            Console.Write("Enter id to publish: ");
            string idToEdit = Console.ReadLine();

            collectionService.ToPublished(patientCollection, idToEdit);
            foreach (var pat in patientCollection.CollectionList)
            {
                Console.WriteLine(Helper.Helper.ToString(pat));
            }
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"______________________Publish_________________________");
            }
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"[{DateTime.Now}] published by {user.Role}: {user.FirstName} {user.LastName} Patient [ {Helper.Helper.ToString(patient)} ] ");
            }
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"_______________________________________________");
            }
        }
        else
        {
            throw new ArgumentException("Only admin can publish patient");
        }
    }
    public void ToModerationPatient()
    {
        if (user.Role == UserRole.Manager)
        {
            if (patientCollection.CollectionList == null)
            {
                Console.WriteLine("Patient list is empty");
                return;
            }

            Console.Write("Enter id to send to moderation: ");
            string idToEdit = Console.ReadLine();

            collectionService.ToModeration(patientCollection, idToEdit);
            foreach (var pat in patientCollection.CollectionList)
            {
                Console.WriteLine(Helper.Helper.ToString(pat));
            }
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"______________________To moderation_________________________");
            }
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"[{DateTime.Now}] to moderation by {user.Role}: {user.FirstName} {user.LastName} Patient [ {Helper.Helper.ToString(patient)} ] ");
            }
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"_______________________________________________");
            }
        }
        else
        {
            throw new ArgumentException("Only manager can send to moderation patient");
        }
    }
    public void SavePatientsIntoFile()
    {
        if (user.Role == UserRole.Admin || user.Role == UserRole.Manager)
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
                    writer.WriteLine($"Patient [ {Helper.Helper.ToString(patient1)} ]");
                }
            }
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"_______________________________________________");
            }
        }
        else
        {
            throw new ArgumentException("Only admin or manager can save patients to file patient");
        }
    }

    public void UploadPatients()
    {
        if (user.Role == UserRole.Admin || user.Role == UserRole.Manager)
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

            var serializedPatientList = Helper.Helper.Serialize(uploadedPatientList);
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
                    writer.WriteLine($"Patient [ {Helper.Helper.ToString(patient1)} ] ");
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
        if (user.Role == UserRole.Admin || user.Role == UserRole.Manager)
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
        MyCollection<Patient> newPatientListForUser = new MyCollection<Patient>();
        CollectionService collectionService = new CollectionService();
        foreach (var patii in patientCollection.CollectionList)
        {
            collectionService.AddObject(newPatientListForUser,patii);
        }

        if (user.Role == UserRole.User)
        {
            MyCollection<Patient> sortedPatientList = new MyCollection<Patient>();
            Console.WriteLine("Enter property to sort");
            string sortBy = Console.ReadLine();
            sortedPatientList = collectionService.SortBy(newPatientListForUser, sortBy);
            foreach (var pati in sortedPatientList.CollectionList)
            {
                Console.WriteLine(Helper.Helper.ToString(pati));
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
                    writer.WriteLine($" Patient: [ {Helper.Helper.ToString(patient1)} ] ");
                }
            }
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"_______________________________________________");
            }
        }
        else
        {
            MyCollection<Patient> sortedPatientList = new MyCollection<Patient>();
            Console.WriteLine("Enter property to sort");
            string sortBy = Console.ReadLine();
            sortedPatientList = collectionService.SortBy(patientCollection, sortBy);
            foreach (var pati in sortedPatientList.CollectionList)
            {
                Console.WriteLine(Helper.Helper.ToString(pati));
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
                    writer.WriteLine($" Patient: [ {Helper.Helper.ToString(patient1)} ] ");
                }
            }
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"_______________________________________________");
            }
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
        
        MyCollection<Patient> newPatientListForUser = new MyCollection<Patient>();
        CollectionService collectionService = new CollectionService();
        foreach (var patii in patientCollection.CollectionList)
        {
            collectionService.AddObject(newPatientListForUser,patii);
        }

        if (user.Role == UserRole.User)
        {
            Console.WriteLine("Enter value to search");
            string search = Console.ReadLine();
            newPatientList = collectionService.Search(newPatientListForUser, search);
            if (newPatientList.CollectionList == null)
            {
                Console.WriteLine("Patient searched list is empty");
                return;
            }

            foreach (var pati in newPatientList.CollectionList)
            {
                Console.WriteLine(Helper.Helper.ToString(pati));
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
                    writer.WriteLine($" Patient [ {Helper.Helper.ToString(patient1)} ] ");
                }
            }
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"_______________________________________________");
            }
        }
        else
        {
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
                    writer.WriteLine($" Patient [ {Helper.Helper.ToString(patient1)} ] ");
                }
            }
            using (StreamWriter writer = File.AppendText(logPath))
            {
                writer.WriteLine($"_______________________________________________");
            }
        }
        
    }

    public void ViewPatientList()
    {
        if (patientCollection.CollectionList == null)
        {
            Console.WriteLine("Patient list is empty");
            return;
        }

        foreach (var pati in patientCollection.CollectionList)
        {
            if(pati.State == State.Published)
                Console.WriteLine(Helper.Helper.ToString(pati));
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
                if(patient1.State == State.Published)
                    writer.WriteLine($" Patient [ {Helper.Helper.ToString(patient1)} ]  ");
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
        foreach (var patient in patientCollection.CollectionList)
        {
            if(patient.State == State.Published)
                Console.WriteLine(Helper.Helper.ToString(patient));
        }
        Console.WriteLine("Введіть id");
        string id = Console.ReadLine();

        var pati = collectionService.SearchById(patientCollection, id);
        Console.WriteLine(Helper.Helper.ToString(pati));

        using (StreamWriter writer = File.AppendText(logPath))
        {
            writer.WriteLine($"[{DateTime.Now}] Patient [ {Helper.Helper.ToString(pati)} ] viewed by {user.ToString()}");
        }
    }
}