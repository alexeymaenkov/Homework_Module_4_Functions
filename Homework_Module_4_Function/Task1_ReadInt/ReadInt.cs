namespace Homework_Module_4_Function.Task1_ReadInt;

public class ReadInt
{
    public void Run()
    {
        //Если конвертация не удалась у пользователя запрашивается число повторно до тех пор,
        //пока не будет введено верно. После ввода, который удалось преобразовать в число, число возвращается.
        //Полученное число из функции надо в Main вывести в консоль.
        //P.S. Задача решается с помощью циклов
        //P.S. Также в TryParse используется модификатор параметра out

        int number = 0;
        
        string userInput = " ";
        
        bool isWorking = true;

        while (isWorking)
        {
            GetUserInput(ref userInput);
            
            ConvertToInt(userInput, ref number, ref isWorking);
        }
        
        Console.Write($"Введенное число: {number}");
    }
    
    static void GetUserInput(ref string userInput)
    {
        Console.Write("Введите целое число:");
        string input = Console.ReadLine();
        
        userInput = input;
    }
    
    static void ConvertToInt(string userInput, ref int number, ref bool isWorking)
    {
        if (int.TryParse(userInput, out int userNumber))
        {
            number = userNumber;
            isWorking = false;
        }
        else
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ошибка ввода! Попробуйте еще раз:");
            Console.ResetColor();
        }
    }
}