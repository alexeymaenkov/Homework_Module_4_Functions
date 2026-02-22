namespace Homework_Module_4_Function.Task3_Shuffle;

public class Shuffle
{
    public void Run()
    {
        //Реализуйте функцию Shuffle, которая перемешивает элементы
        //массива в случайном порядке.
        
        //Random random = new();

        int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        
        ShuffleArray(array);

        foreach (var element in array)
            Console.Write(element + " ");
    }

    static Array ShuffleArray(int[] array)
    {
        Random random = new();

        for (int i = 0; i < array.Length; i++)
        {
            int randomIndex = random.Next(i, array.Length);
            (array[i], array[randomIndex]) = (array[randomIndex], array[i]);
        }
        
        return array;
    }
}