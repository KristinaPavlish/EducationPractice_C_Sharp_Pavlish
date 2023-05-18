using EducationalPractice.Bll.Implementation_Task_5.Models;

Proxy proxy = new Proxy();
User user = null;
Auth auth = new Auth();

auth.LoadUsersFromFile();
auth.LoadAdminsFromFile();
auth.LoadManagersFromFile();
UserF();
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
            Console.WriteLine("Помилка: " + ex.Message);
            Console.WriteLine("Будь ласка спробуйте ще раз.");
        }
    }
}


void Register()
{
    Console.WriteLine("Введiть iм'я:");
    string firstName = Console.ReadLine();
    Console.WriteLine("Введiть прiзвище:");
    string lastName = Console.ReadLine();
    Console.WriteLine("Введiть пароль:");
    string password = Console.ReadLine();
    Console.WriteLine("Введiть електронну пошту:");
    string email = Console.ReadLine();
    proxy.user = auth.RegisterUser(firstName, lastName, password, email, user);
}

void Login()
{
    Console.WriteLine("Введiть електронну пошту:");
    string email = Console.ReadLine();
    Console.WriteLine("Введiть пароль:");
    string password = Console.ReadLine();
    
    proxy.user = auth.Login(email, password, user);
}

void LoginAdmin()
{
    Console.WriteLine("Введiть електронну пошту:");
    string email = Console.ReadLine();
    Console.WriteLine("Введiть пароль:");
    string password = Console.ReadLine();
    proxy.user = auth.LoginAdmin(email, password, user);
}
void LoginManager()
{
    Console.WriteLine("Введiть електронну пошту:");
    string email = Console.ReadLine();
    Console.WriteLine("Введiть пароль:");
    string password = Console.ReadLine();
    proxy.user = auth.LoginManager(email, password, user);
}

void Logout()
{
    proxy.user = auth.Logout(user);
}

void UserF()
{
    bool exit = false;
    while (!exit)
    {
        Console.WriteLine("Оберiть дiю:");
        Console.WriteLine("1. Реєстрацiя");
        Console.WriteLine("2. Вхiд як користувач");
        Console.WriteLine("3. Вхiд як адмiн");
        Console.WriteLine("4. Вхiд як менеджер");
        Console.WriteLine("5. Вихiд");
        Console.WriteLine("6. -- Завершити --");

        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                RunWithRetry(() => Register());
                Console.WriteLine("Реєстрацiя користувача пройшла успiшно.");
                RunWithRetry(() => Main());
                break;
            case "2":
                RunWithRetry(() => Login());
                Console.WriteLine("Вхiд користувача виконано успiшно");
                RunWithRetry(() => Main());
                break;
            case "3":
                RunWithRetry(() => LoginAdmin());
                Console.WriteLine("Вхiд адмiну виконано успiшно.");
                RunWithRetry(() => Main());
                break;
            case "4":
                RunWithRetry(() => LoginManager());
                Console.WriteLine("Вхiд менеджера виконано успiшно.");
                RunWithRetry(() => Main());
                break;
            case "5":
                Logout();
                Console.WriteLine("Вихiд виконано успiшно.");
                UserF();
                break;
            case "6":
                Console.WriteLine("Виходимо");
                exit = true;
                break;
            default:
                Console.WriteLine("Неправильний вибір. Спробуйте ще раз.");
                break;
        }
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
        Console.WriteLine("9. Переглянути пацієнта по id");
        Console.WriteLine("10. Опублiкувати пацiєнта");
        Console.WriteLine("11. Надiслати на перевiрку пацiєнта");
        Console.WriteLine("12. Вийти з програми");

        Console.Write("Ваш вибір: ");
        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                // Додати пацієнта
                proxy.AddPatient();
                break;
            case "2":
                // Видалити пацієнта
                proxy.DeletePatient();
                break;
            case "3":
                // Редагувати пацієнта
                proxy.EditPatient();
                break;
            case "4":
                // Записати пацієнтів в файл
                proxy.SavePatientsIntoFile();
                break;
            case "5":
                // Записати пацієнтів в файл
                proxy.UploadPatients();
                break;
            case "6":
                // Посортувати пацієнтів
                proxy.SortPatient();
                break;
            case "7":
                // Пошук
                proxy.SearchPatients();
                break;
            case "8":
                // Переглянути список пацієнтів
                proxy.ViewPatientList();
                break;
            case "9":
                // Переглянути пацієнтa
                proxy.ViewPatient();
                break;
            case "10":
                // Опублікувати пацієнта
                proxy.PublishPatient();
                break;
            case "11":
                // Надіслати на модерацію пацієнта
                proxy.ToModerationPatient();
                break;
            case "12":
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
