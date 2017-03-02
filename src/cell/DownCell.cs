namespace kakuro.cell {

  public struct DownCell : ICell, IDown {

    private int total;

    public DownCell(int total) {
      this.total = total;
    }

    public string draw() {
      return string.Format("   {0,2:D}\\--  ", total);
    }

    public int getDown() {
      return total;
    }

  }

}
