namespace Kakuro.Cell
{

    public struct DownCell : ICell, IDown
    {

        private readonly int total;

        public DownCell(int total)
        {
            this.total = total;
        }

        public string Draw()
        {
            return string.Format("   {0,2:D}\\--  ", total);
        }

        public int GetDown()
        {
            return total;
        }

    }

}
