using System.Text.RegularExpressions;

namespace EducationalPractice.Bll.Implementation_Task_5.Models;

public class Auth
{
    private List<User> users;
    private List<User> admins;
    private List<User> managers;
    private string usersFilePath = "E:\\EducationPractice_C_Sharp_Pavlish\\EducationalPractice\\EducationalPractice.Task_5.ConsoleApp\\users.txt"; // шлях до файлу для збереження даних про користувачів
    private string adminssFilePath = "E:\\EducationPractice_C_Sharp_Pavlish\\EducationalPractice\\EducationalPractice.Task_5.ConsoleApp\\Admins.txt"; // шлях до файлу для збереження даних про адмінів
    private string managersFilePath = "E:\\EducationPractice_C_Sharp_Pavlish\\EducationalPractice\\EducationalPractice.Task_5.ConsoleApp\\meneger.txt"; // шлях до файлу для збереження даних про менеджерів

    public Auth()
    {
        users = new List<User>();
        admins = new List<User>();
        managers = new List<User>();
    }

    public User RegisterUser(string firstName, string lastName, string password, string email, User user)
    {

        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
        {
            throw new ArgumentException("Усi поля повиннi бути заповненi");
        }

        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            throw new ArgumentException("Неправильний формат електронної пошти");
        }

        if (password.Length < 8 || !Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"))
        {
            throw new ArgumentException("Пароль повинен мати щонайменше 8 символiв, включаючи великi та малi лiтери та цифри");
        }

        user = new User()
        {
            FirstName = firstName,
            LastName = lastName,
            Password = password,
            Email = email,
            Role = UserRole.User
        };
        users.Add(user);
        SaveUsersToFile(); 
        return user;
    }

    public User Login(string email, string password, User user)
    {
        user = users.Find(u => u.Email == email && u.Password == password);
        if (user == null)
        {
            throw new ArgumentException("Неправильна електронна пошта або пароль.");
        }
        return user;
    }

    public User Logout(User user)
    {
        user = null;
        return user;
    }
    
    public User LoginAdmin(string email, string password, User user)
    {
        user = admins.Find(u => u.Email == email && u.Password == password);
        if (user == null)
        {
            throw new ArgumentException("Неправильна електронна пошта або пароль.");
        }
        return user;
    }
    public User LoginManager(string email, string password, User user)
    {
        user = managers.Find(u => u.Email == email && u.Password == password);
        if (user == null)
        {
            throw new ArgumentException("Неправильна електронна пошта або пароль.");
        }
        return user;
    }
    
    public void LoadUsersFromFile()
    {
        if (File.Exists(usersFilePath))
        {
            string[] lines = File.ReadAllLines(usersFilePath);
            foreach (string line in lines)
            {
                string[] data = line.Split(", ");
                User user = new User()
                {
                    FirstName = data[0],
                    LastName = data[1],
                    Password = data[2],
                    Email = data[3],
                    Role = (UserRole)Enum.Parse(typeof(UserRole), data[4])
                };
                users.Add(user);
            }
        }
    }
    
    public void LoadAdminsFromFile()
    {
        if (File.Exists(adminssFilePath))
        {
            string[] lines = File.ReadAllLines(adminssFilePath);
            foreach (string line in lines)
            {
                string[] data = line.Split(", ");
                User user = new User()
                {
                    FirstName = data[0],
                    LastName = data[1],
                    Password = data[2],
                    Email = data[3],
                    Role = (UserRole)Enum.Parse(typeof(UserRole), data[4])
                };
                admins.Add(user);
            }
        }
    }
    public void LoadManagersFromFile()
    {
        if (File.Exists(managersFilePath))
        {
            string[] lines = File.ReadAllLines(managersFilePath);
            foreach (string line in lines)
            {
                string[] data = line.Split(", ");
                User user = new User()
                {
                    FirstName = data[0],
                    LastName = data[1],
                    Password = data[2],
                    Email = data[3],
                    Role = (UserRole)Enum.Parse(typeof(UserRole), data[4])
                };
                managers.Add(user);
            }
        }
    }
    public void SaveUsersToFile()
    {
        if (File.Exists(usersFilePath))
        {
            File.Delete(usersFilePath);
        }

        using (StreamWriter writer = new StreamWriter(usersFilePath))
        {
            foreach (User user in users)
            {
                writer.WriteLine($"{user.FirstName}, {user.LastName}, {user.Password}, {user.Email}, {user.Role}");
            }
        }
    }
}