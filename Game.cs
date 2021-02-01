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
  public PositionStatus[,] board { get; } = new PositionStatus[3,3];
  private PositionStatus currentPlayer;
  private bool won = false;
  public Game(Output mode) {
    this.outputMode = mode;
    InitializeBoard();
    SetRandomFirstPlayer();
  }
  

  public void DrawBoard() {
    Console.WriteLine();
    Console.WriteLine($" {GetStatusChar(board[0,0])} | {GetStatusChar(board[0,1])} | {GetStatusChar(board[0,2])} ");
    Console.WriteLine( "-----------");
    Console.WriteLine($" {GetStatusChar(board[1,0])} | {GetStatusChar(board[1,1])} | {GetStatusChar(board[1,2])} ");
    Console.WriteLine( "-----------");
    Console.WriteLine($" {GetStatusChar(board[2,0])} | {GetStatusChar(board[2,1])} | {GetStatusChar(board[2,2])} ");
  }
  private string ShowWelcomeMessage() {
    string message = "Welcome to my tic-tac-toe game!";
    if(outputMode == Output.CLI) Console.WriteLine(message);
    return message;
  }

  private char GetStatusChar(PositionStatus status) {
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
      }
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
    Console.WriteLine($"{GetStatusChar(currentPlayer)}'s move: ");
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

  private void TrytMove(int position) {
    var movePosition = new Position(position);
    if(PositionAvailable(movePosition)) {
      Move(movePosition);
    }
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
    DrawBoard();
    CheckForWin();
    if(won) {
      ShowWinMessage();
    } else if (IsBoardFull()) {
      ShowGameOver();
    } else {
      NextPlayer();
    }
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

  private string ShowWinMessage() {
    string message = $"Game Over. Congratulations to Player {GetStatusChar(currentPlayer)}!";
    if(outputMode == Output.CLI) {
      Console.WriteLine(message);
    }
    return message;
  }

  private string ShowGameOver() {
    string message = "Game Over. There is no winner.";
    if(outputMode == Output.CLI) {
      Console.WriteLine(message);
    }
    return message;
  }
}