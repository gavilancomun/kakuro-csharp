namespace kakuro.cell {

	public class DownCell : Cell, Down {

  private int total;

	public DownCell(int total) {
	  this.total = total;
	}

	public virtual string draw() {
	  return string.Format("   {0,2:D}\\--  ", total);
	}

	public virtual int getDownTotal() {
		  return total;
	}

	}

}
