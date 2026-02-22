using System.Globalization;
using System.Text;

namespace Homework_Module_4_Function.Task4_Personnel_accounting;

public class PersonnelAccounting
{
    public void Run()
    {
        /*Будет 2 одномерных массива:
        1) Полные имена сотрудников (фамилия, имя, отчество);
        2) Должности.

            Описать функцию заполнения массивов досье, функцию форматированного вывода,
            функцию поиска по фамилии и функцию удаления досье.
            Функция добавления элемента расширяет уже имеющийся массив на 1 и дописывает туда новое значение.

            Программа должна быть с меню, которое содержит пункты:
        1) добавить досье
        2) вывести все досье (в одну строку через “-” фио и должность с порядковым номером в начале)
        3) удалить досье (Удаление должно быть конкретного элемента, указанного пользователем. Массивы уменьшаются на один элемент. Нужны дополнительные проверки, чтобы не возникало ошибок)
        4) поиск по фамилии (показ всех с данной фамилией)
        5) выход 
            Не используйте Array.Resize   */

        const int COMMAND_ADD_DOSSIER = 1;
        const int COMMAND_SHOW_ALL_DOSSIERS = 2;
        const int COMMAND_DELETE_DOSSIER = 3;
        const int COMMAND_FINDE_BY_SURNAME = 4;
        const int COMMAND_EXIT = 5;
        
        //int command = 0;
        
        string[] fullName = { "Maenkov Alexey Aleksandrovich", "Ivanov Sergey Petrovich", "Pavlov Alexandr Viktorovich"  };
        string[] job = { "Director", "Engineer", "Mechanic" };

        bool isWorking = true;
        
        while (isWorking)
        {
            Console.WriteLine("Команды работы с базой сотрудников:");
            Console.WriteLine($"{COMMAND_ADD_DOSSIER} - добавить сотрудника.");
            Console.WriteLine($"{COMMAND_SHOW_ALL_DOSSIERS} - вывести все досье.");
            Console.WriteLine($"{COMMAND_DELETE_DOSSIER} - удалить досье.");
            Console.WriteLine($"{COMMAND_FINDE_BY_SURNAME} - поиск сотрудника по фамилии.");
            Console.WriteLine($"{COMMAND_EXIT} - выход.");
            
            int command = 0;
            
            GetCommand(ref command);
            
            switch (command)
            {
                case COMMAND_ADD_DOSSIER:
                    AddDossier(ref fullName, ref job);
                    break;
                case COMMAND_SHOW_ALL_DOSSIERS:
                    ShowAllDossiers(ref fullName, ref job);
                    break;
                case COMMAND_DELETE_DOSSIER:
                    DeleteDossier(ref fullName, ref job);
                    break;
                case COMMAND_FINDE_BY_SURNAME:
                    FindDossier(ref fullName);
                    break;
                case COMMAND_EXIT:
                    isWorking = false;
                    break;
                default:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка ввода команды! Попробуйте еще раз:");
                    Console.ResetColor();
                    break;
            }
        }
    }
    
    static void GetCommand(ref int command)
    {
        Console.Write("Введите номер команды: ");
        string userInput = Console.ReadLine();
        
        if (int.TryParse(userInput, out int userNumber))
        {
            command = userNumber;
        }
        else
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ошибка ввода команды! Попробуйте еще раз:");
            Console.ResetColor();
        }
    }

    static void AddDossier(ref string[]  fullName, ref string[] job)
    {
        StringBuilder newFullName = new ();
        
        Console.Write("Введите Фамилию сотрудника: ");
        newFullName.Append(Console.ReadLine() + " ");
        Console.Write("Введите Имя сотрудника: ");
        newFullName.Append(Console.ReadLine() + " ");
        Console.Write("Введите Отчество сотрудника: ");
        newFullName.Append(Console.ReadLine());

        Console.Write("Введите должность сотрудника: ");
        string newJob = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(newFullName.ToString()))
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ошибка! Не верный ввод ФИО сотрудника.\n");
            Console.ResetColor();
            return;
        }

        if (string.IsNullOrWhiteSpace(newJob))
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ошибка! Не верный ввод должности сотрудника.\n");
            Console.ResetColor();
            return;
        }
        
        string[] tempFullName = new string[fullName.Length + 1];
        string[] tempJob = new string[job.Length + 1];
        
        for (int i =  0; i < fullName.Length; i++)
        {
            tempFullName[i] = fullName[i];
        }
        
        tempFullName[^1] = newFullName.ToString();
        fullName = tempFullName;
        
        for (int i =  0; i < job.Length; i++)
        {
            tempJob[i] = job[i];
        }
        
        tempJob[^1] = newJob;
        job = tempJob;
        
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Сотрудник успешно добавлен.\n");
        Console.ResetColor();
    }

    static void ShowAllDossiers(ref string[] fullName, ref string[] job)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Список всех досье:");
        
        for (int i = 0; i < fullName.Length; i++)
        {
            Console.Write($"{i + 1}. {fullName[i]}, {job[i]} - ");
        }
        
        Console.ResetColor();
        Console.WriteLine();
    }

    static void DeleteDossier(ref string[] fullName, ref string[] job)
    {
        if (fullName.Length == 0)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("База Досье Пуста!\n");
            Console.ResetColor();
        }
        else
        {
            Console.Write("\nВведите порядковый номер досье, которое нужно удалить: ");
            string userInput = Console.ReadLine();
            
            if (int.TryParse(userInput, out int deleteNumber))
            {
                deleteNumber--;
                if (deleteNumber >= 0 && deleteNumber <= fullName.Length)
                {
                    string[] tempFullName = new string[fullName.Length - 1];
                    string[] tempJob = new string[job.Length - 1];

                    if (deleteNumber <  fullName.Length)
                    {
                        (fullName[deleteNumber], fullName[fullName.Length - 1]) = (fullName[fullName.Length - 1], fullName[deleteNumber]);
                        (job[deleteNumber], job[job.Length - 1]) = (job[job.Length - 1], job[deleteNumber]);
                    }
                    
                    for (int i =  0; i < fullName.Length - 1; i++)
                    {
                        tempFullName[i] = fullName[i];
                    }
            
                    fullName = tempFullName;
            
                    for (int i =  0; i < job.Length - 1; i++)
                    {
                        tempJob[i] = job[i];
                    }
            
                    job = tempJob;
                    
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Сотрудник успешно удален.\n");
                    Console.ResetColor();
                }
                else
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка! Такого номера не существует. Попробуйте еще раз:");
                    Console.ResetColor();
                }
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка ввода номера! Попробуйте еще раз:");
                Console.ResetColor();
            }
        }
    }

    static void FindDossier(ref string[] fullName)
    {
        Console.Write("\nВведите Фамилию сотрудника: ");
        string searchedSurname = Console.ReadLine();
        
        if (string.IsNullOrWhiteSpace(searchedSurname))
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ошибка! Не верный ввод Фамилии сотрудника.\n");
            Console.ResetColor();
            return;
        }

        int countSymbols = 0;
        int index = 0;
        
        for (int i = 0; i < fullName.Length; i++)
        {
            for (int j = 0; j < searchedSurname.Length; j++)
            {
                if (char.ToLower(searchedSurname[j]) == char.ToLower(fullName[i][j]))
                {
                    countSymbols++;
                    if (countSymbols == searchedSurname.Length)
                    {
                        countSymbols = 0;
                        index = i + 1;
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        if (index > 0)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Досье сотрудника по фамилии '{searchedSurname}' находится под номером: {index}\n");
            Console.ResetColor();
        }
        else
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Досье сотрудника по фамилии '{searchedSurname}' не существует.\n");
            Console.ResetColor();
        }
    }
}