namespace EducationalPractice.Bll.Implementation_Task_4.Models;


public class PermissionProxy
{
    private User _user;

    public PermissionProxy(User user)
    {
        _user = user;
    }

    public void AddPatient(Patient patient)
    {
        if (_user.Role == "admin")
        {
            PatientCollection collection = new PatientCollection();
            collection.AddPatient(patient);
            Log("Create", patient);
        }
        else
        {
            Console.WriteLine("Access denied");
        }
    }

    public void RemovePatient(int id)
    {
        if (_user.Role == "admin")
        {
            PatientCollection collection = new PatientCollection();
            Patient patient = collection.GetPatientById(id);
            collection.RemovePatient(id);
            Log("Delete", patient);
        }
        else
        {
            Console.WriteLine("Access denied");
        }
    }

    public Patient GetPatientById(int id)
    {
        PatientCollection collection = new PatientCollection();
        Patient patient = collection.GetPatientById(id);

        if (_user.Role == "admin" || (_user.Role == "customer" && patient != null))
        {
            return patient;
        }
        else
        {
            Console.WriteLine("Access denied");
            return null;
        }
    }

    public List<Patient> SearchPatients(string searchText)
    {
        PatientCollection collection = new PatientCollection();
        List<Patient> patients = collection.SearchPatients(searchText);

        if (_user.Role == "admin")
        {
            return patients;
        }
        else
        {
            Console.WriteLine("Access denied");
            return new List<Patient>();
        }
    }

    public List<Patient> GetAllPatients()
    {
        PatientCollection collection = new PatientCollection();
        List<Patient> patients = collection.GetAllPatients();

        if (_user.Role == "admin")
        {
            return patients;
        }
        else
        {
            Console.WriteLine("Access denied");
            return new List<Patient>();
        }
    }

    public List<Patient> SortPatients(string sortBy)
    {
        PatientCollection collection = new PatientCollection();
        List<Patient> patients = collection.SortPatients(sortBy);

        if (_user.Role == "admin")
        {
            return patients;
        }
        else
        {
            Console.WriteLine("Access denied");
            return new List<Patient>();
        }
    }

    private void Log(string action, Patient patient)
    {
        Console.WriteLine($"User {_user.FirstName} {_user.LastName} ({_user.Email}) {action} patient with ID {patient.Id}");
    }
}
