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
        
        int number;

        while (true)
        {
            string userInput = GetInput("Введите целое число:");

            if (int.TryParse(userInput, out number))
                break;
            
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ошибка ввода! Попробуйте еще раз:");
            Console.ResetColor();
        }
        
        Console.Write($"Введенное число: {number}");
    }
    
    static string GetInput(string message)
    {
        Console.Write(message);
        return Console.ReadLine().Trim();
    }
}