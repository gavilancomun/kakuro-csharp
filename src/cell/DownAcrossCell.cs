namespace kakuro.cell {


	public class DownAcrossCell : Cell, Across, Down {

	internal int down;
	internal int across;

	public DownAcrossCell(int down, int across) {
	  this.down = down;
	  this.across = across;
	}

	public virtual string draw() {
	  return string.Format("   {0,2:D}\\{1,-2:D}  ", down, across);
	}

	public virtual bool isAcross() {
		  return true;
	}

	public virtual bool isDown() {
		  return true;
	}

	public virtual bool isEmpty() {
		  return false;
	}

	public virtual int getAcrossTotal() {
		  return across;
	}

	public virtual int getDownTotal() {
		  return down;
	}

	}

}
