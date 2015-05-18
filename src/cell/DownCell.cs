namespace kakuro.cell {

	public class DownCell : Cell, Down {

	internal int total;

	public DownCell(int total) {
	  this.total = total;
	}

	public virtual string draw() {
	  return string.Format("   {0,2:D}\\--  ", total);
	}

	public virtual bool isAcross() {
		  return false;
	}

	public virtual bool isDown() {
		  return true;
	}

	public virtual bool isEmpty() {
		  return false;
	}

	public virtual int getDownTotal() {
		  return total;
	}

	}

}
