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
        
        const ConsoleKey COMMAND_MOOVE_UP = ConsoleKey.UpArrow;
        const ConsoleKey COMMAND_MOOVE_DOWN = ConsoleKey.DownArrow;
        const ConsoleKey COMMAND_MOOVE_LEFT = ConsoleKey.LeftArrow;
        const ConsoleKey COMMAND_MOOVE_RIGHT = ConsoleKey.RightArrow;
        const ConsoleKey COMMAND_MARK_MINE = ConsoleKey.Spacebar;
        const ConsoleKey COMMAND_DETACH_MINE = ConsoleKey.Backspace;
        const ConsoleKey COMMAND_OPEN_FIELD = ConsoleKey.Enter;
        const ConsoleKey COMMAND_RESTART = ConsoleKey.Y;
        const ConsoleKey COMMAND_EXIT = ConsoleKey.N;
        
        int minefieldX = 10;
        int minefieldY = 10;
        
        int mine = -1;
        int mineQuantity = 10;
        
        char border = '#';
        char field = '*';
        char possibleMine = '?';
        
        bool restart = true;
        
        while (restart)
        {
            int[,] minefield = CreateMinefield(ref minefieldX, ref minefieldY, ref mine, ref mineQuantity, ref random);
            
            char[,] map = GetMap(minefieldX + 2, minefieldY + 2, border, field);

            DrawLegend(ref minefieldY, ref mineQuantity);

            int userX = 1;
            int userY = 1;
            int countMine = 0;
            int countWin = 0;

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
                
                int quantityFreeMinefieldСells = (minefieldX * minefieldY) - mineQuantity;
                
                ConsoleKeyInfo charKey = Console.ReadKey();
                
                switch (charKey.Key)
                {
                    case COMMAND_MOOVE_UP:
                        MovingUser(COMMAND_MOOVE_UP, ref map, ref userX, ref userY, border);
                        break;
                    
                    case COMMAND_MOOVE_DOWN:
                        MovingUser(COMMAND_MOOVE_DOWN, ref map, ref userX, ref userY, border);
                        break;
                    
                    case COMMAND_MOOVE_LEFT:
                        MovingUser(COMMAND_MOOVE_LEFT, ref map, ref userX, ref userY, border);
                        break;
                    
                    case COMMAND_MOOVE_RIGHT:
                        MovingUser(COMMAND_MOOVE_RIGHT, ref map, ref userX, ref userY, border);
                        break;
                    
                    case COMMAND_MARK_MINE:
                        MarksMine(COMMAND_MARK_MINE, ref map, ref userX, ref userY, ref countMine, field, possibleMine);
                        break;
                    
                    case COMMAND_DETACH_MINE:
                        MarksMine(COMMAND_DETACH_MINE, ref map, ref userX, ref userY, ref countMine, field, possibleMine);
                        break;
                    
                    case COMMAND_OPEN_FIELD:
                        if (map[userX, userY] == field)
                        {
                            map[userX, userY] = minefield[userX - 1, userY - 1].ToString()[0];
                            countWin++;
                        }
                        if (countWin == quantityFreeMinefieldСells)
                        {
                            Console.SetCursorPosition(0, 0);
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
                            Console.SetCursorPosition(0, 0);
                            Console.BackgroundColor = ConsoleColor.Red;
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.WriteLine("!----------ВЫ ПРОИГАЛИ---------!");
                            Console.ReadKey();
                            isWorking = false;
                        }
                        break;
                }
            }
            
            Restart(ref restart);
        }
        static char[,] GetMap(int x, int y, char border, char field)
        {
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

        static void MovingUser(ConsoleKey key, ref char[,] map, ref int userX, ref int userY, char border)
        {
            if (key == COMMAND_MOOVE_UP)
                if (map[userX - 1, userY] != border)
                    userX--;
            
            if (key == COMMAND_MOOVE_DOWN)
                if (map[userX + 1, userY] != border)
                    userX++;
            
            if (key == COMMAND_MOOVE_LEFT)
                if (map[userX, userY - 1] != border)
                    userY--;
            
            if (key == COMMAND_MOOVE_RIGHT)
                if (map[userX, userY + 1] != border)
                    userY++;
        }

        static void MarksMine(ConsoleKey key, ref char[,] map, ref int userX, ref int userY, ref int countMine, char field, char possibleMine)
        {
            if (key == COMMAND_MARK_MINE)
            {
                map[userX, userY] = possibleMine;
                countMine++;
            }

            if (key == COMMAND_DETACH_MINE)
            {
                map[userX, userY] = field;
                countMine--;
            }
        }
        
        static void Restart(ref bool restart)
        {
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Хотите попробовать еще раз (Y/N)?");
            Console.ResetColor();
            ConsoleKeyInfo charKey = Console.ReadKey();
            
            switch (charKey.Key)
            {
                case COMMAND_RESTART:
                    restart = true;
                    break;

                case COMMAND_EXIT:
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