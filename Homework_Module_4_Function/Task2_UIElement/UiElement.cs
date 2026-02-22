namespace Homework_Module_4_Function.Task2_UIElement;

public class UiElement
{
    public void Run()
    {
        //Разработайте функцию, которая рисует некий бар (Healthbar, Manabar) в определённой позиции.
        //Функция принимает некий закрашенный процент, длину бара и при необходимости дополнительные параметры.
        //При 40% бар выглядит так: [####______]
        //Реализуйте показ данных здоровья и маны.

        int health = 5;
        int mana = 2;
        int maxValue = 10;
        
        DrawBar("Health", health, maxValue, 0);
        DrawBar("Mana", mana, maxValue, 1);
    }

    static void DrawBar(string attributeName, int value, int maxValue, int positionY)
    {
        string bar = "";

        for (int i = 0; i < value; i++)
        {
            bar += "#";
        }
        
        for (int j = value + 1; j <= maxValue; j++)
        {
            bar += "_";
        }
        
        Console.SetCursorPosition(0, positionY);
        Console.Write($"{attributeName}: [{bar}]");
    }
}