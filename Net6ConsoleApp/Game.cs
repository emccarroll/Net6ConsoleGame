using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Net6ConsoleApp
{
    public class Game
    {

        public static void ReadInputs()
        {

            while (true)
            {
                ConsoleKeyInfo keyinfo = Console.ReadKey();
                if (keyinfo.Key == ConsoleKey.UpArrow)
                {
                    Program.Actions.Enqueue(Enums.GameActions.MoveUp);
                }
                else if (keyinfo.Key == ConsoleKey.DownArrow)
                {
                    Program.Actions.Enqueue(Enums.GameActions.MoveDown);
                }
                else if (keyinfo.Key == ConsoleKey.LeftArrow)
                {
                    Program.Actions.Enqueue(Enums.GameActions.MoveLeft);
                }
                else if (keyinfo.Key == ConsoleKey.RightArrow)
                {
                    Program.Actions.Enqueue(Enums.GameActions.MoveRight);
                }
            }

        }




    }
}
