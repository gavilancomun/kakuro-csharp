namespace Kakuro.Cell
{

    public struct DownAcrossCell : ICell, IAcross, IDown
    {

        private readonly int down;
        private readonly int across;

        public DownAcrossCell(int down, int across)
        {
            this.down = down;
            this.across = across;
        }

        public string Draw()
        {
            return string.Format("   {0,2:D}\\{1,2:D}  ", down, across);
        }

        public int GetAcross()
        {
            return across;
        }

        public int GetDown()
        {
            return down;
        }

    }

}
