// See https://aka.ms/new-console-template for more information

using SelectFramework;

namespace Net6ConsoleApp;

public class Program
{


    private static Queue<Enums.GameActions> _actions = new Queue<Enums.GameActions>();

    public static Queue<Enums.GameActions> Actions { get => _actions; }

    private static char[,] _gameMap = new char[Console.WindowHeight, Console.WindowWidth];
    public static char[,] GameMap { get => _gameMap; }

    public static void Main(string[] args)
    {
        Console.SetBufferSize(Console.WindowWidth, Console.WindowHeight);
        GameMap.Initialize();
        Console.CursorVisible = false;
        DateTime ProgramStartTime = DateTime.Now;
        DateTime OldTime = DateTime.MinValue;
        DateTime currentTime = DateTime.Now;
        TimeSpan? differenceBetweenTimes = null;
        Thread gameThread = new Thread(Game.ReadInputs);
        gameThread.Start();
        int iteration = 1;

        (int, int) position = (0, 0);

        
        while (true)
        {
            currentTime = DateTime.Now;
            differenceBetweenTimes = currentTime - OldTime;
            var numberOfSecondsRunning = (currentTime - ProgramStartTime).TotalSeconds;

            var framerate = 1 / differenceBetweenTimes.Value.TotalSeconds;


            //Console.WriteLine(Console.WindowWidth);
            //Console.WriteLine(Console.WindowHeight);
            //Console.WriteLine(Console.BufferWidth);
            //Console.WriteLine(Console.BufferHeight);
            //
            if(iteration %30 ==0) //Not needed. Felt like adding
                RenderGameMap();
            //throw new Exception("hahahahaha");

            //Console.SetCursorPosition(0, 0);
            //Console.WriteLine("                                                                                     ");
            // 
            // Console.SetCursorPosition(0, 0);
            WriteString($"{Math.Floor(framerate)} framerate    {iteration} frames     {Math.Floor(numberOfSecondsRunning)} seconds", 10,10);
            bool didDequeue = Program.Actions.TryDequeue(out var action);

            if (didDequeue)
            {
                if(action == Enums.GameActions.MoveDown)
                {
                    position = (position.Item1, position.Item2 - 1);
                    WriteString("Woah man", 5, 15);
                }
                    
                else if(action == Enums.GameActions.MoveUp)
                    position = (position.Item1,position.Item2+1);
                else if(action == Enums.GameActions.MoveLeft)
                    position = (position.Item1-1,position.Item2);
                else if(action == Enums.GameActions.MoveRight)
                    position = (position.Item1+1,position.Item2);

                //Console.SetCursorPosition(0, 1);
                //Console.WriteLine("                                                                                     ");
                //Console.SetCursorPosition(0, 1);
                //Console.WriteLine(Enum.GetName(typeof(Enums.GameActions), action));
                //Console.SetCursorPosition(0, 2);
                //Console.WriteLine("                                                                                     ");
                //Console.SetCursorPosition(0, 2);
                //Console.WriteLine($"x={position.Item1} y={position.Item2}");
            }
            Thread.Sleep(1);

            OldTime = currentTime;
            iteration += 1;
        }
    }

    public static void RenderGameMap()
    {
        Console.SetCursorPosition(0, 0);
        var length = GameMap.GetLength(0);
        for (int x = 0; x < length; x++)
        {
            Console.SetCursorPosition(0, x);
            string rowChars = new string(Enumerable.Range(0, GameMap.GetLength(1)).Select(i => GameMap[x, i]).ToArray());
            rowChars = rowChars.Replace('\0', ' ');//needed for columns to work
            Console.Write(rowChars);
        }
    }
    public static void WriteString(string s, int xpos, int ypos)
    {
        int length = s.Length;
        if(xpos < GameMap.GetLength(0) && ypos < GameMap.GetLength(1))
        {
            for(int i =ypos; i< GameMap.GetLength(1) && i-ypos >= 0 && i-ypos <length; i++)
            {
                GameMap[xpos, i] = s[i - ypos];
            }
        }
    }
    public static void SetAllPixels(char val)
    {
        for (int x = 0; x < GameMap.GetLength(0); x++)
        {
            for (int y = 0; y < GameMap.GetLength(1); y++)
            {
                GameMap[x, y] = val;
            }
        }
    }

}




//HSFrame.drawRec(00, 1, 20, 21, ConsoleColor.Green);  