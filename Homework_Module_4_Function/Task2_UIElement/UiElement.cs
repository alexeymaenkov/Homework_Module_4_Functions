using System.Text;
namespace Homework_Module_4_Function.Task2_UIElement;

public class UiElement
{
    public void Run()
    {
        //Разработайте функцию, которая рисует некий бар (Healthbar, Manabar) в определённой позиции.
        //Функция принимает некий закрашенный процент, длину бара и при необходимости дополнительные параметры.
        //При 40% бар выглядит так: [####______]
        //Реализуйте показ данных здоровья и маны.

        int currentHealth = 11;
        int currentMana = 7;
        
        DrewHealthBar(currentHealth);
        DrewManaBar(currentMana);
    }

    static void DrewHealthBar(int currentHealth)
    {
        const int MAX_HEALTH = 20;
        
        DrawBar(currentHealth, MAX_HEALTH, 0);
    }
    
    static void DrewManaBar(int currentMana)
    {
        const int MAX_MANA = 10;
        
        DrawBar(currentMana, MAX_MANA, 1);
    }

    static void DrawBar(int value, int maxValue, int positionY)
    {
        StringBuilder bar = new ();

        for (int i = 0; i < value; i++)
        {
            bar.Append("#");
        }
        
        for (int j = value + 1; j <= maxValue; j++)
        {
            bar.Append("_");
        }
        
        Console.SetCursorPosition(0, positionY);
        Console.Write($"[{bar}]");
    }
}