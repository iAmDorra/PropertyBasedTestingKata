using NFluent;

namespace GameOfLifeTests
{
    public class UnitTest1
    {
        [Fact]
        public void Dead_Cell()
        {
            Cell deadCell = new Cell();
            Check.That(deadCell.IsAlive()).IsFalse();
        }

    }

    internal class Cell
    {
        internal bool IsAlive()
        {
            return false;
        }
    }
}