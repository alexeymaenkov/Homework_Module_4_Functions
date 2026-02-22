namespace Homework_Module_4_Function.Task5_Brave_new_world;

public class BraveNewWorld
{
    public void Run()
    {
        //Сделать игровую карту с помощью двумерного массива. Сделать функцию показа карты в консоли.
        //Помимо этого, дать пользователю возможность перемещаться по карте
        //и взаимодействовать с элементами (например пользователь не может пройти сквозь стену)
        //Все элементы являются обычными символами
        //Не используйте Task.Run
            
        Console.CursorVisible = false;
        
        Random random = new ();
        
        int minefieldX = 10;
        int minefieldY = 10;
        
        int mine = -1;
        int mineQuantity = 10;
        
        bool restart = true;
        
        while (restart)
        {
            int[,] minefield = CreateMinefield(ref minefieldX, ref minefieldY, ref mine, ref mineQuantity, ref random);
            
            char[,] map = GetMap(minefieldX + 2, minefieldY + 2);

            DrawLegend(ref minefieldY, ref mineQuantity);

            int userX = 1;
            int userY = 1;
            int countMine = 0;

            bool isWorking = true;

            while (isWorking)
            {
                DrawMap(map);

                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.SetCursorPosition(userY, userX);
                Console.Write(" ");
                Console.ResetColor();

                Console.SetCursorPosition(minefieldY + 4, 9);
                Console.WriteLine($"Найдено МИН: {countMine}");
                
                HandleInput(ref map, ref userX, ref userY, ref countMine, ref isWorking, ref minefield, ref minefieldX, ref minefieldY, ref mineQuantity, ref mine);
            }
            
            Restart(ref restart);
        }
        static char[,] GetMap(int x, int y)
        {
            char border = '#';
            char field = '*';

            char[,] map = new char[x, y];

            for (int mapX = 0; mapX < map.GetLength(0); mapX++)
            {
                for (int mapY = 0; mapY < map.GetLength(1); mapY++)
                {
                    if ((mapX == 0) || (mapY == 0) || (mapX == x - 1) || (mapY == y - 1))
                    {
                        map[mapX, mapY] = border;
                    }
                    else { map[mapX, mapY] = field; }
                }
            }
            
            return map;
        }

        static void DrawMap(char[,] map)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, 0);
            
            for (int mapX = 0; mapX < map.GetLength(0); mapX++)
            {
                for (int mapY = 0; mapY < map.GetLength(1); mapY++)
                {
                    Console.Write(map[mapX, mapY]);
                }

                Console.Write("\n");
            }
            
            Console.ResetColor();
        }

        static int[,] CreateMinefield(ref int minefieldX, ref int minefieldY, ref int mine, ref int mineQuantity, ref Random random)
        {
            const int QUANTITY_STEPS_AROUND = 8;
            
            int[,] minefield = new int[minefieldX, minefieldY];
            
            int[] aroundX = { -1, -1, -1, 0, 1, 1, 1, 0 };
            int[] aroundY = { -1, 0, 1, 1, 1, 0, -1, -1 };
            
            for (int i = 0; i < mineQuantity; i++)
            {
                int xMine = random.Next(0, minefieldX);
                int yMine = random.Next(0, minefieldY);
                minefield[xMine, yMine] = mine;
            }

            for (int x = 0; x < minefieldX; x++)
            {
                for (int y = 0; y < minefieldY; y++)
                {
                    if (minefield[x, y] == mine)
                        continue;

                    int count = 0;

                    for (int i = 0; i < QUANTITY_STEPS_AROUND; i++)
                    {
                        int newX = x + aroundX[i];
                        int newY = y + aroundY[i];

                        if (newX >= 0 && newX < minefieldX && newY >= 0 && newY < minefieldY)
                        {
                            if (minefield[newX, newY] == mine)
                            {
                                count++;
                            }
                        }
                    }

                    minefield[x, y] = count;
                }
            }
            
            return minefield;
        }

        static void DrawLegend(ref int battlefieldSizeY, ref int mineQuantity)
        {
            Console.Clear();
            Console.SetCursorPosition(battlefieldSizeY + 4, 0);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Добро пожаловать в игру САПЕР!");
            Console.SetCursorPosition(battlefieldSizeY + 4, 2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"На карте случайным образом размещены {mineQuantity} мин. Найдите их!");
            Console.SetCursorPosition(battlefieldSizeY + 4, 4);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Для перемещения курсора используйте клавиши UP, DOWN, LEFT, RIGHT.");
            Console.SetCursorPosition(battlefieldSizeY + 4, 5);
            Console.WriteLine("Для вскрытия поля - клавишу ENTER.");
            Console.SetCursorPosition(battlefieldSizeY + 4, 6);
            Console.WriteLine("Для того чтобы выделить предполагаемое место нахождения бомбы - клавишу SPACE.");
            Console.SetCursorPosition(battlefieldSizeY + 4, 7);
            Console.WriteLine("Для того чтобы спрятать выделение предполагаемого места нахождения бомбы - клавишу BACKSPACE.");
            Console.ResetColor();
        }

        static void HandleInput(ref char[,] map, ref int userX, ref int userY, ref int countMine, ref bool isWorking, ref int[,] minefield, ref int minefieldX, ref int minefieldY, ref int mineQuantity, ref int mine)
        {
            int countWin = 0;
            
            ConsoleKeyInfo charKey = Console.ReadKey();
                
                switch (charKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (map[userX - 1, userY] != '#')
                        {
                            userX--;
                        }
                        break;
                    
                    case ConsoleKey.DownArrow:
                        if (map[userX + 1, userY] != '#')
                        {
                            userX++;
                        }
                        break;
                    
                    case ConsoleKey.LeftArrow:
                        if (map[userX, userY - 1] != '#')
                        {
                            userY--;
                        }
                        break;
                    
                    case ConsoleKey.RightArrow:
                        if (map[userX, userY + 1] != '#')
                        {
                            userY++;
                        }
                        break;
                    
                    case ConsoleKey.Backspace:
                        map[userX, userY] = '*';
                        countMine--;
                        break;
                    
                    case ConsoleKey.Spacebar:
                        map[userX, userY] = '?';
                        countMine++;
                        break;
                    
                    case ConsoleKey.Enter:
                        if (map[userX, userY] == '*')
                        {
                            countWin++;
                        }
                        if (countWin == (minefieldX * minefieldY) - mineQuantity)
                        {
                            Console.SetCursorPosition(0, minefieldY + 5);
                            Console.BackgroundColor = ConsoleColor.DarkGreen;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("!----------ВЫ ВЫГРАЛИ---------!");
                            isWorking = false;
                        }
                        if (minefield[userX - 1, userY - 1] == mine)
                        {
                            map[userX, userY] = 'X';
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.SetCursorPosition(userY, userX);
                            Console.Write("X");
                            Console.ResetColor();
                            Console.SetCursorPosition(0, minefieldY + 5);
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("!----------ВЫ ПРОИГАЛИ---------!");
                            isWorking = false;
                        }
                        else if (minefield[userX - 1, userY - 1] == 0)
                        {
                            map[userX, userY] = '0';
                        }
                        else if (minefield[userX - 1, userY - 1] == 1)
                        {
                            map[userX, userY] = '1';
                        }
                        else if (minefield[userX - 1, userY - 1] == 2)
                        {
                            map[userX, userY] = '2';
                        }
                        else if (minefield[userX - 1, userY - 1] == 3)
                        {
                            map[userX, userY] = '3';
                        }
                        else if (minefield[userX - 1, userY - 1] == 4)
                        {
                            map[userX, userY] = '4';
                        }
                        else if (minefield[userX - 1, userY - 1] == 5)
                        {
                            map[userX, userY] = '5';
                        }
                        else if (minefield[userX - 1, userY - 1] == 6)
                        {
                            map[userX, userY] = '6';
                        }
                        else if (minefield[userX - 1, userY - 1] == 7)
                        {
                            map[userX, userY] = '7';
                        }
                        else if (minefield[userX - 1, userY - 1] == 8)
                        {
                            map[userX, userY] = '8';
                        }
                        break;
                }
        }

        static void Restart(ref bool restart)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Хотите попробовать еще раз (Y/N): ");
            Console.ResetColor();
            ConsoleKeyInfo charKey = Console.ReadKey();
            
            switch (charKey.Key)
            {
                case ConsoleKey.Y:
                    restart = true;
                    break;

                case ConsoleKey.N:
                    Console.Clear();
                    restart = false;
                    break;
                
                default:
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("Нажмите Y или N!");
                    Console.ResetColor();
                    break;
            }
        }
    }
}