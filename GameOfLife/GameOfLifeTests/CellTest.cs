using NFluent;

namespace GameOfLifeTests
{
    public class CellTest
    {
        [Fact]
        public void Dead_Cell()
        {
            Cell deadCell = new Cell();
            Check.That(deadCell.IsAlive()).IsFalse();
        }

        [Fact]
        public void Alive_Cell()
        {
            Cell aliveCell = new Cell(true);
            NFluent.Check.That(aliveCell.IsAlive()).IsTrue();
        }

    }

    internal class Cell
    {
        private bool isAlive;

        public Cell(bool isAlive= false)
        {
            this.isAlive = isAlive;
        }

        internal bool IsAlive()
        {
            return isAlive;
        }
    }
}