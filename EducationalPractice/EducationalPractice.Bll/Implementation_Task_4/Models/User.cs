using System;
using System.Collections.Generic;

// User class with first name, last name, email, role, and password fields
public class User
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public string Password { get; set; }
}

// Authentication class
public class Auth
{
    private static List<User> _users = new List<User>()
    {
        new User { FirstName = "John", LastName = "Doe", Email = "johndoe@example.com", Role = "admin", Password = "admin123" },
        new User { FirstName = "Jane", LastName = "Doe", Email = "janedoe@example.com", Role = "customer", Password = "customer123" }
    };

    public static User Login(string email, string password)
    {
        User user = _users.Find(u => u.Email == email && u.Password == password);
        if (user != null)
        {
            Console.WriteLine($"Logged in as {user.FirstName} {user.LastName} ({user.Role})");
        }
        else
        {
            Console.WriteLine("Invalid email or password");
        }
        return user;
    }

    public static void Register(string firstName, string lastName, string email, string role, string password)
    {
        User user = new User { FirstName = firstName, LastName = lastName, Email = email, Role = role, Password = password };
        _users.Add(user);
        Console.WriteLine($"Registered as {user.FirstName} {user.LastName} ({user.Role})");
    }

    public static void Logout(User user)
    {
        if (user != null)
        {
            Console.WriteLine($"Logged out {user.FirstName} {user.LastName} ({user.Role})");
        }
        else
        {
            Console.WriteLine("No user logged in");
        }
    }
}

