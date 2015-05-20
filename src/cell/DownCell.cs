namespace kakuro.cell {

  public struct DownCell : Cell, Down {

    private int total;

    public DownCell(int total) {
      this.total = total;
    }

    public string draw() {
      return string.Format("   {0,2:D}\\--  ", total);
    }

    public int getDownTotal() {
      return total;
    }

  }

}
