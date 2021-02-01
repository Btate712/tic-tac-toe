using System;

namespace tic_tac_toe
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(Output.CLI);
            game.Play();
        }
    }
}
