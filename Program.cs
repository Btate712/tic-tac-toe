using System;

namespace tic_tac_toe
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(Output.CLI);
            game.ShowWelcomeMessage();
            game.move(1,1);
            game.move(0,0);
            game.move(0,2);
            game.DrawBoard();
        }
    }
}
