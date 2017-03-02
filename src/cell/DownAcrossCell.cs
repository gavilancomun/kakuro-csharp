namespace kakuro.cell {

  public struct DownAcrossCell : ICell, IAcross, IDown {

    private int down;
    private int across;

    public DownAcrossCell(int down, int across) {
      this.down = down;
      this.across = across;
    }

    public string draw() {
      return string.Format("   {0,2:D}\\{1,2:D}  ", down, across);
    }

    public int getAcrossTotal() {
      return across;
    }

    public int getDownTotal() {
      return down;
    }

  }

}
