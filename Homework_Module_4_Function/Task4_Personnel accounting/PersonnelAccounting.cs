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

        const string COMMAND_ADD_DOSSIER = "1";
        const string COMMAND_SHOW_ALL_DOSSIERS = "2";
        const string COMMAND_DELETE_DOSSIER = "3";
        const string COMMAND_FINDE_BY_SURNAME = "4";
        const string COMMAND_EXIT = "5";
        
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

            string userCommand = GetInput("Введите номер команды: ");
            
            switch (userCommand)
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
                    OutputError("Ошибка ввода команды! Попробуйте еще раз:");
                    break;
            }
        }
    }
    static void OutputSuccess(string message)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    static void OutputError(string message)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    
    static string GetInput(string message)
    {
        Console.Write(message);
        return Console.ReadLine().Trim();
    }
    
    static void AddDossier(ref string[]  fullName, ref string[] job)
    {
        string newFullName = GetInput("Введите ФИО сотрудника: ");
        
        if (string.IsNullOrWhiteSpace(newFullName))
        {
            OutputError("Ошибка! Не верный ввод ФИО сотрудника.\n");
            return;
        }
        
        string newJob = GetInput("Введите должность сотрудника: ");

        if (string.IsNullOrWhiteSpace(newJob))
        {
            OutputError("Ошибка! Не верный ввод должности сотрудника.\n");
            return;
        }
        
        Resize(ref fullName, ref newFullName);
        
        Resize(ref job, ref newJob);
        
        OutputSuccess("Сотрудник успешно добавлен.\n");
    }

    static void Resize(ref string[] array, ref string newElement)
    {
        string[] tempArray = new string[array.Length + 1];
        
        for (int i =  0; i < array.Length; i++)
        {
            tempArray[i] = array[i];
        }
        
        tempArray[^1] = newElement;
        array = tempArray;
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
            OutputError("База Досье Пуста!\n");
            return;
        }
        
        string userInput = GetInput("\nВведите порядковый номер досье, которое нужно удалить: ");

        if (int.TryParse(userInput, out int deleteNumber) == false)
        {
            OutputError("Ошибка ввода номера! Попробуйте еще раз:");
            return;
        }

        deleteNumber--;

        if (deleteNumber < 0 && deleteNumber > fullName.Length)
        {
            OutputError("Ошибка! Такого номера не существует. Попробуйте еще раз:");
            return;
        }

        if (deleteNumber <  fullName.Length)
        {
            (fullName[deleteNumber], fullName[^1]) = (fullName[^1], fullName[deleteNumber]);
            (job[deleteNumber], job[^1]) = (job[^1], job[deleteNumber]);
        }
        
        string[] tempFullName = new string[fullName.Length - 1];
        string[] tempJob = new string[job.Length - 1];
        
        for (int i =  0; i < fullName.Length - 1; i++)
        {
            tempFullName[i] = fullName[i];
            tempJob[i] = job[i];
        }

        fullName = tempFullName;
        job = tempJob;
        
        OutputSuccess("Сотрудник успешно удален!");
    }

    static void FindDossier(ref string[] fullName)
    {
        string searchedSurname = GetInput("\nВведите Фамилию сотрудника: ");
        
        if (string.IsNullOrWhiteSpace(searchedSurname))
        {
            OutputError("Ошибка! Не верный ввод Фамилии сотрудника.\n");
            return;
        }

        int countSymbols = 0;
        int index = 0;
        
        for (int i = 0; i < fullName.Length; i++)
        {
            for (int j = 0; j < searchedSurname.Length; j++)
            {
                if (char.ToLower(searchedSurname[j]) != char.ToLower(fullName[i][j]))
                {
                    break;
                }

                countSymbols++;
                
                if (countSymbols == searchedSurname.Length)
                {
                    countSymbols = 0;
                    index = i + 1;
                    break;
                }
            }
        }

        if (index > 0)
        {
            OutputSuccess($"Досье сотрудника по фамилии '{searchedSurname}' находится под номером: {index}\n");
        }
        else
        {
            OutputError($"Досье сотрудника по фамилии '{searchedSurname}' не существует.\n");
        }
    }
}