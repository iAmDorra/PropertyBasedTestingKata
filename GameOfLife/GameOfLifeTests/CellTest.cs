using FsCheck;
using FsCheck.Xunit;
using NFluent;

namespace GameOfLifeTests
{
    public class CellTest
    {
        [Fact]
        public void Dead_Cell()
        {
            Cell deadCell = Cell.Dead();
           NFluent.Check.That(deadCell.IsAlive()).IsFalse();
        }

        [Fact]
        public void Alive_Cell()
        {
            Cell aliveCell = Cell.Alive();
            NFluent.Check.That(aliveCell.IsAlive()).IsTrue();
        }

        #region For a space that is populated
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
           NFluent.Check.That(cell).IsEqualTo(Cell.Dead());
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
           NFluent.Check.That(cell).IsEqualTo(Cell.Dead());
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
           NFluent.Check.That(cell).IsEqualTo(Cell.Alive());
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
           NFluent.Check.That(cell).IsEqualTo(Cell.Alive());
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
           NFluent.Check.That(cell).IsEqualTo(Cell.Alive());
        }
        #endregion
        #endregion

        #region For a space that is empty or unpopulated
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
           NFluent.Check.That(cell).IsEqualTo(Cell.Alive());
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
           NFluent.Check.That(cell).IsEqualTo(Cell.Alive());
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
           NFluent.Check.That(cell).IsEqualTo(Cell.Dead());
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
           NFluent.Check.That(cell).IsEqualTo(Cell.Dead());
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
           NFluent.Check.That(cell).IsEqualTo(Cell.Dead());
        }
        #endregion

        // Failed test
        //[Property]
        //public Property CellShouldDieBySolitude(List<Cell> neighbors)
        //{
        //    return Prop.When(Global.AnyInput,
        //        () => Cell.Alive()
        //        .NextGeneration(neighbors)
        //        .IsDead());
        //}

        [Property(Arbitrary = new[] { typeof(DeadCellGenerator) })]
        public Property DeadCells(List<Cell> neighbors)
        {
            return Prop.When(Global.AnyInput,
                () => Cell.Dead()
                .NextGeneration(neighbors)
                .IsDead());
        }
     
        [Property(Arbitrary = new[] { typeof(OverpopulationGenerator) })]
        public Property Overpopulation(IEnumerable<Cell> neighbors)
        {
            return Prop.When(Global.AnyInput,
                () => Cell.Alive()
                .NextGeneration(neighbors)
                .IsDead());
        }

        [Property(Arbitrary = new[] { typeof(OverpopulationGenerator2) })]
        public Property Overpopulation2(IEnumerable<Cell> neighbors)
        {
            return Prop.When(neighbors.Count() > 3,
                () => Cell.Alive()
                .NextGeneration(neighbors)
                .IsDead());
        }

        [Property(Arbitrary = new[] { typeof(OnlyTwoAliveNeighborsGenerator) })]
        public Property WithinStabilityThreshold_withAliveCell(List<Cell> neighbors)
        {
            return Prop.When(Global.AnyInput,
                () => Cell.Alive().NextGeneration(neighbors).IsAlive());
        }

        [Property(Arbitrary = new[] { typeof(OnlyTwoAliveNeighborsGenerator) })]
        public Property WithinStabilityThreshold_withDeadCell(List<Cell> neighbors)
        {
            return Prop.When(Global.AnyInput,
                () => Cell.Dead().NextGeneration(neighbors).IsDead());
        }

        [Property(Arbitrary = new[] { typeof(OnlyThreeAliveNeighborsGenerator) })]
        public Property IsFertilityThreshold_withAliveCell(List<Cell> neighbors)
        {
            return Prop.When(Global.AnyInput,
                () => Cell.Alive().NextGeneration(neighbors).IsAlive());
        }

        [Property(Arbitrary = new[] { typeof(OnlyThreeAliveNeighborsGenerator) })]
        public Property IsFertilityThreshold_withDeadCell(List<Cell> neighbors)
        {
            return Prop.When(Global.AnyInput,
                () => Cell.Dead().NextGeneration(neighbors).IsAlive());
        }
  }

    public static class OverpopulationGenerator2
    {
        public static Arbitrary<IEnumerable<Cell>> Generate()
        {
            var arbInt = Arb.Default.Int32()
                .Convert(
                x => Math.Abs(x),
                y => y);

            var arbCell = arbInt.Convert(
                x => Enumerable.Range(0, x).Select(x => Cell.Alive()),
                c => c.Count());

            return arbCell;
        }
    }
    public static class OverpopulationGenerator
    {
        public static Arbitrary<IEnumerable<Cell>> Generate()
        {
            var arbInt = Arb.Default.Int32()
                .Convert(
                x => Math.Abs(x) + 4,
                y => y - 4);

            var arbCell = arbInt.Convert(
                x => Enumerable.Range(0, x).Select(x => Cell.Alive()),
                c => c.Count());

            return arbCell;
        }
    }
    public static class OnlyThreeAliveNeighborsGenerator
    {
        public static Arbitrary<List<Cell>> Generate()
        {
            Arbitrary<List<Cell>> deadCells = Arb.Default.List<Cell>().Filter(l => l.All(c => !c.IsAlive()));
            var deadCellsWithTwoAliveOnes = deadCells.MapFilter(c1 =>
            {
                var cellsWith2Alive = new List<Cell>
                  {
                    Cell.Alive(),
                    Cell.Alive(),
                    Cell.Alive()
                  };
                cellsWith2Alive.AddRange(c1);
                return cellsWith2Alive;
            }, c => true);
            return deadCellsWithTwoAliveOnes;
        }
    }

    public static class OnlyTwoAliveNeighborsGenerator
    {
        public static Arbitrary<List<Cell>> Generate()
        {
            Arbitrary<List<Cell>> deadCells = Arb.Default.List<Cell>().Filter(l => l.All(c => !c.IsAlive()));
            var deadCellsWithTwoAliveOnes = deadCells.MapFilter(c1 =>
            {
                var cellsWith2Alive = new List<Cell>
                  {
                    Cell.Alive(),
                    Cell.Alive()
                  };
                cellsWith2Alive.AddRange(c1);
                return cellsWith2Alive;
            }, c => true);
            return deadCellsWithTwoAliveOnes;
        }
    }

    public static class DeadCellGenerator
    {
        public static Arbitrary<List<Cell>> Generate()
        {
            return Arb.Default.List<Cell>().Filter(l => l.All(c => !c.IsAlive()));
        }
    }
}