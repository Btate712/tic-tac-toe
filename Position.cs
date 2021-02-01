class Position {
  public int x { get; }
  public int y { get; }

  public Position(int p) {
    p--;
    x = p / 3;
    y = p % 3;
  }
}