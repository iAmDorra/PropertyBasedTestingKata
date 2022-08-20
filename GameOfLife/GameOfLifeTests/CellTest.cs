using NFluent;

namespace GameOfLifeTests
{
    public class CellTest
    {
        [Fact]
        public void Dead_Cell()
        {
            Cell deadCell = Cell.Dead();
            Check.That(deadCell.IsAlive()).IsFalse();
        }

        [Fact]
        public void Alive_Cell()
        {
            Cell aliveCell = Cell.Alive();
            NFluent.Check.That(aliveCell.IsAlive()).IsTrue();
        }

        [Fact]
        public void Dead_cell_with_no_neighbor_still_dead()
        {
            Cell deadCell = Cell.Dead();
            var neighbors = new List<Cell>();
            var cell = deadCell.NextGeneration(neighbors);
            NFluent.Check.That(cell).IsEqualTo(Cell.Dead());
        }
    }

    internal class Cell
    {
        private bool isAlive;

        private Cell(bool isAlive= false)
        {
            this.isAlive = isAlive;
        }

        internal static Cell Alive()
        {
            return new Cell(true);
        }

        internal static Cell Dead()
        {
            return new Cell(false);
        }

        internal bool IsAlive()
        {
            return isAlive;
        }

        internal Cell NextGeneration(List<Cell> neightbors)
        {
            return Cell.Dead();
        }
        public override bool Equals(object? obj)
        {
            var cell = obj as Cell;
            if (cell == null)
                return base.Equals(obj);
            else
                return cell.isAlive == this.isAlive;
        }
    }
}