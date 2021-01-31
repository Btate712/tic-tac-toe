using System;

enum Output {
  CLI,
  WEB
}
class Game {
  private Output outputMode;
  
  public Game(Output mode) {
    this.outputMode = mode;
  }
  public void ShowWelcomeMessage() {
    if(outputMode == Output.CLI) {
      Console.WriteLine("Welcome to my tic-tac-toe game!");
    }
  }

}