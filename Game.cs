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
  private bool won = false;
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
    if(outputMode == Output.CLI) {
      Console.WriteLine();
      Console.WriteLine($" {GetChar(board[0,0])} | {GetChar(board[0,1])} | {GetChar(board[0,2])} ");
      Console.WriteLine( "-----------");
      Console.WriteLine($" {GetChar(board[1,0])} | {GetChar(board[1,1])} | {GetChar(board[1,2])} ");
      Console.WriteLine( "-----------");
      Console.WriteLine($" {GetChar(board[2,0])} | {GetChar(board[2,1])} | {GetChar(board[2,2])} ");
    }
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

  public void Play() {
    ShowWelcomeMessage();
    DrawBoard();
    while(!won && !IsBoardFull()) {
      Position position = GetNextMove();
      if(PositionAvailable(position)) {
        Move(position);
        DrawBoard();
        CheckForWin();
        if(!won) NextPlayer();
      }
    }
    if(won) {
      ShowWinMessage();
    } else {
      ShowGameOver();
    }
  }

  private bool IsBoardFull() {
    for(int i = 0; i < 3; i++) {
      for(int j = 0; j < 3; j++) {
        if(board[i,j] == PositionStatus.Empty) return false;
      }
    }
    return true;
  }

  private Position GetNextMove() {
    if(outputMode == Output.CLI) {
      Console.WriteLine($"{GetChar(currentPlayer)}'s move: ");
      ConsoleKeyInfo userInput = Console.ReadKey();
      Position movePosition;
      if (char.IsDigit(userInput.KeyChar)) {
        movePosition = new Position(int.Parse(userInput.KeyChar.ToString()));
        return movePosition;
      } else {
        Console.WriteLine("Invalid input. Please enter a number between 1 and 9");
        return GetNextMove();
      }
    } 
    return new Position(1);
  }

  private void CheckForWin() {
    // Check Horizontals
    for(int i = 0; i < 3; i++) {
      // Check Horizontals
      if(board[i,0] != PositionStatus.Empty && 
        board[i,0] == board[i,1] && 
        board[i,1] == board[i,2]
      ) {
        won = true;
        return;
      // Check Verticals
      } else if (board[0,i] != PositionStatus.Empty 
        && board[0,i] == board[1,i] && 
        board[1,i] == board[2,i]
      ) {
        won = true;
        return;
      }
    }
    // Check Diagonals
    if(board[1,1] != PositionStatus.Empty && 
      ((board[0,0] == board[1,1] && board[1,1] == board[2,2]) || 
      (board[0,2] == board[1,1] && board[1,1] == board[2,0]))
    ) {
      won = true;
      return;
    }
  }
  private void Move(Position position) {
    board[position.x, position.y] = currentPlayer;
  }

  private bool PositionAvailable(Position position) {
    return board[position.x, position.y] == PositionStatus.Empty;
  } 

  private void SetRandomFirstPlayer() {
    var rand = new Random();
    currentPlayer = rand.Next(2) == 1 ? PositionStatus.X : PositionStatus.O;
  }

  private void NextPlayer() {
    currentPlayer = currentPlayer == PositionStatus.X ? PositionStatus.O : PositionStatus.X;
  }

  private void ShowWinMessage() {
    Console.WriteLine($"Game Over. Congratulations to Player {GetChar(currentPlayer)}!");
  }

  private void ShowGameOver() {
    Console.WriteLine("Game Over. There is no winner.");
  }
}