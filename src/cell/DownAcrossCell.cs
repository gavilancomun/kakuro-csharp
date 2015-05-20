namespace kakuro.cell {


	public class DownAcrossCell : Cell, Across, Down {

	private int down;
  private int across;

	public DownAcrossCell(int down, int across) {
	  this.down = down;
	  this.across = across;
	}

	public virtual string draw() {
	  return string.Format("   {0,2:D}\\{1,-2:D}  ", down, across);
	}

	public virtual int getAcrossTotal() {
		  return across;
	}

	public virtual int getDownTotal() {
		  return down;
	}

	}

}
