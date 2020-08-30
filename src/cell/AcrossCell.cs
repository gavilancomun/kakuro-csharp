namespace Kakuro.Cell
{

    public struct AcrossCell : ICell, IAcross
    {

        private readonly int total;

        public AcrossCell(int total)
        {
            this.total = total;
        }

        public string Draw()
        {
            return string.Format("   --\\{0,2:D}  ", total);
        }

        public int GetAcross()
        {
            return total;
        }

    }

}
