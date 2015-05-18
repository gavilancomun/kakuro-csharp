namespace kakuro.cell {

	public class AcrossCell : Cell, Across {

	internal int total;

	public AcrossCell(int total) {
	  this.total = total;
	}

	public virtual string draw() {
	  return string.Format("   --\\{0,-2:D}  ", total);
	}

	public virtual bool isAcross() {
		  return true;
	}

	public virtual bool isDown() {
		  return false;
	}

	public virtual bool isEmpty() {
		  return false;
	}

	public virtual int getAcrossTotal() {
		  return total;
	}

	}

}
