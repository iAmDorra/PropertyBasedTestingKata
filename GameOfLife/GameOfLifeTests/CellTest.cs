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

        #region For alive cell
        #region Each cell with one or no neighbors dies, as if by solitude
        [Fact]
        public void Alive_cell_with_no_neighbor_dies()
        {
            Cell aliveCell = Cell.Alive();
            var neighbors = new List<Cell>();
            var cell = aliveCell.NextGeneration(neighbors);
            NFluent.Check.That(cell).IsEqualTo(Cell.Dead());
        }

        [Fact]
        public void Alive_cell_with_1_neighbor_dies()
        {
            Cell deadCell = Cell.Alive();
            var neighbors = new List<Cell>
            {
                Cell.Alive(),
            };
            var cell = deadCell.NextGeneration(neighbors);
            Check.That(cell).IsEqualTo(Cell.Dead());
        }
        #endregion

        #region Each cell with four or more neighbors dies, as if by overpopulation
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
        #endregion

        #region Each cell with two or three neighbors survives. For a space that is empty or unpopulated
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

        [Fact]
        public void Alive_cell_with_2_alive_neighbors_survives()
        {
            Cell deadCell = Cell.Alive();
            var neighbors = new List<Cell>
            {
                Cell.Alive(),
                Cell.Alive(),
                Cell.Dead(),
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
        #endregion
        #endregion

        #region For dead cell
        #region Each cell with three neighbors becomes populated
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
        public void Dead_cell_with_3_alive_neighbors_survives()
        {
            Cell deadCell = Cell.Dead();
            var neighbors = new List<Cell>
            {
                Cell.Alive(),
                Cell.Alive(),
                Cell.Alive(),
                Cell.Dead(),
            };
            var cell = deadCell.NextGeneration(neighbors);
            Check.That(cell).IsEqualTo(Cell.Alive());
        }
        #endregion

        [Fact]
        public void Dead_cell_with_no_neighbor_still_dead()
        {
            Cell deadCell = Cell.Dead();
            var neighbors = new List<Cell>();
            var cell = deadCell.NextGeneration(neighbors);
            NFluent.Check.That(cell).IsEqualTo(Cell.Dead());
        }

        [Fact]
        public void Dead_cell_with_1_neighbor_dies()
        {
            Cell deadCell = Cell.Dead();
            var neighbors = new List<Cell>
            {
                Cell.Alive(),
            };
            var cell = deadCell.NextGeneration(neighbors);
            Check.That(cell).IsEqualTo(Cell.Dead());
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
        public void Dead_cell_with_2_neighbors_still_dead()
        {
            Cell deadCell = Cell.Dead();
            var neighbors = new List<Cell>
            {
                Cell.Alive(),
                Cell.Alive(),
            };
            var cell = deadCell.NextGeneration(neighbors);
            Check.That(cell).IsEqualTo(Cell.Dead());
        }
        #endregion
    }
}