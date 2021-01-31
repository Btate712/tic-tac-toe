using System;

enum Output {
  CLI,
  Web
}

enum PositionStatus {
  X,
  O,
  Empty
}
class Game {
  private Output outputMode;
  private PositionStatus[,] board = new PositionStatus[3,3];
  private PositionStatus currentPlayer;
  public Game(Output mode) {
    this.outputMode = mode;
    InitializeBoard();
    SetRandomFirstPlayer();
  }
  public void ShowWelcomeMessage() {
    if(outputMode == Output.CLI) {
      Console.WriteLine("Welcome to my tic-tac-toe game!");
    }
  }

  public void DrawBoard() {
    Console.WriteLine($" {GetChar(board[0,0])} | {GetChar(board[0,1])} | {GetChar(board[0,2])} ");
    Console.WriteLine( "-----------");
    Console.WriteLine($" {GetChar(board[1,0])} | {GetChar(board[1,1])} | {GetChar(board[1,2])} ");
    Console.WriteLine( "-----------");
    Console.WriteLine($" {GetChar(board[2,0])} | {GetChar(board[2,1])} | {GetChar(board[2,2])} ");
  }

  private char GetChar(PositionStatus status) {
    switch(status) {
      case PositionStatus.Empty:
        return ' ';
      case PositionStatus.X:
        return 'X';
      case PositionStatus.O:
        return 'O';
      default:
        throw new ArgumentOutOfRangeException();
    }
  }
  private void InitializeBoard() {
    for(int i = 0; i < 3; i++) {
      for(int j = 0; j < 3; j++) {
        board[i,j] = PositionStatus.Empty;
      }
    }
  }

  public void move(byte x, byte y) {
    board[x,y] = currentPlayer;
    currentPlayer = currentPlayer == PositionStatus.X ? PositionStatus.O : PositionStatus.X;
  }

  private void SetRandomFirstPlayer() {
    var rand = new Random();
    currentPlayer = rand.Next(2) == 1 ? PositionStatus.X : PositionStatus.O;
  }
}