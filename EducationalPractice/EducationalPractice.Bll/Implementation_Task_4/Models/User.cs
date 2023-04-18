using System;
using System.Text.RegularExpressions;

public enum UserRole
{
    Admin,
    User
}

public class User
{
    private string firstName;
    private string lastName;
    private string password;
    private string email;
    private UserRole role;

    public string FirstName
    {
        get { return firstName; }
        set { firstName = value; }
    }

    public string LastName
    {
        get { return lastName; }
        set { lastName = value; }
    }

    public string Password
    {
        get { return password; }
        set
        {
            // Валідація пароля
            if (value.Length < 8 || !Regex.IsMatch(value, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$"))
            {
                throw new ArgumentException("Пароль повинен мати щонайменше 8 символів, включаючи великі та малі літери та цифри");
            }
            password = value;
        }
    }

    public string Email
    {
        get { return email; }
        set
        {
            // Валідація електронної пошти
            if (!Regex.IsMatch(value, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ArgumentException("Неправильний формат електронної пошти");
            }
            email = value;
        }
    }

    public UserRole Role
    {
        get { return role; }
        set { role = value; }
    }
}