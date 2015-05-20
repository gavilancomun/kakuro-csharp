namespace kakuro.cell {

	public class AcrossCell : Cell, Across {

  private int total;

	public AcrossCell(int total) {
	  this.total = total;
	}

	public virtual string draw() {
	  return string.Format("   --\\{0,-2:D}  ", total);
	}

	public virtual int getAcrossTotal() {
		  return total;
	}

	}

}
