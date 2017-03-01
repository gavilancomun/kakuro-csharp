namespace kakuro.cell {

  public struct AcrossCell : Cell, Across {

    private int total;

    public AcrossCell(int total) {
      this.total = total;
    }

    public string draw() {
      return string.Format("   --\\{0,2:D}  ", total);
    }

    public int getAcrossTotal() {
      return total;
    }

  }

}
