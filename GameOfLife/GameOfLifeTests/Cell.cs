using NFluent;

namespace GameOfLifeTests
{
    public class Cell
    {
        private bool isAlive;

        public Cell(bool isAlive = false)
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
            if (WithinStabilityThreshold(neighbors))
            {
                return this;
            }

            if (IsFertilityThreshold(neighbors))
            {
                return Cell.Alive();
            }
            return Cell.Dead();
        }

        private static bool IsFertilityThreshold(List<Cell> neighbors)
        {
            return neighbors.Count(neighbor => neighbor.isAlive) == 3;
        }

        private static bool WithinStabilityThreshold(List<Cell> neighbors)
        {
            return neighbors.Count(neighbor => neighbor.isAlive) == 2;
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