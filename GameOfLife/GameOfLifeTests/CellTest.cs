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

        [Fact]
        public void Alive_cell_with_no_neighbor_dies()
        {
            Cell aliveCell = Cell.Alive();
            var neighbors = new List<Cell>();
            var cell = aliveCell.NextGeneration(neighbors);
            NFluent.Check.That(cell).IsEqualTo(Cell.Dead());
        }

        [Fact]
        public void Dead_cell_with_3_neighbor_survives()
        {
            Cell deadCell = Cell.Dead();
            var neighbors = new List<Cell>
            {
                Cell.Alive(),
                Cell.Alive(),
                Cell.Alive(),
            };
            var cell = deadCell.NextGeneration(neighbors);
            Check.That(cell).IsEqualTo(Cell.Alive());
        }

        [Fact]
        public void Alive_cell_with_3_neighbor_survives()
        {
            Cell deadCell = Cell.Alive();
            var neighbors = new List<Cell>
            {
                Cell.Alive(),
                Cell.Alive(),
                Cell.Alive(),
            };
            var cell = deadCell.NextGeneration(neighbors);
            Check.That(cell).IsEqualTo(Cell.Alive());
        }

        [Fact]
        public void Dead_cell_with_4_neighbor_still_dead()
        {
            Cell deadCell = Cell.Dead();
            var neighbors = new List<Cell>
            {
                Cell.Alive(),
                Cell.Alive(),
                Cell.Alive(),
                Cell.Alive(),
            };
            var cell = deadCell.NextGeneration(neighbors);
            Check.That(cell).IsEqualTo(Cell.Dead());
        }

        [Fact]
        public void Alive_cell_with_4_neighbor_still_dead()
        {
            Cell deadCell = Cell.Alive();
            var neighbors = new List<Cell>
            {
                Cell.Alive(),
                Cell.Alive(),
                Cell.Alive(),
                Cell.Alive(),
            };
            var cell = deadCell.NextGeneration(neighbors);
            Check.That(cell).IsEqualTo(Cell.Dead());
        }

        [Fact]
        public void Alive_cell_with_2_neighbors_still_alive()
        {
            Cell deadCell = Cell.Alive();
            var neighbors = new List<Cell>
            {
                Cell.Alive(),
                Cell.Alive(),
            };
            var cell = deadCell.NextGeneration(neighbors);
            Check.That(cell).IsEqualTo(Cell.Alive());
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

        internal Cell NextGeneration(List<Cell> neighbors)
        {
            if (neighbors.Count == 2
                && neighbors.All(neighbor => neighbor.isAlive))
            {
                return Cell.Alive();
            }

            if (neighbors.Count == 3
                && neighbors.All(neighbor => neighbor.isAlive))
            {
                return Cell.Alive();
            }
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