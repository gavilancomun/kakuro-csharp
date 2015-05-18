namespace kakuro.cell {

	public class EmptyCell : Cell {

	public virtual string draw() {
	  return "   -----  ";
	}

	public virtual bool isAcross() {
		  return false;
	}

	public virtual bool isDown() {
		  return false;
	}

	public virtual bool isEmpty() {
		  return false;
	}

	}

}
