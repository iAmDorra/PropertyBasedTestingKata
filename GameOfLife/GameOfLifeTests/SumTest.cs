using NFluent;

namespace GameOfLifeTests
{
    public class SumTest
    {
        [Fact]
        public void Sum_is_associative()
        {
            int a = 1;
            int b = 2;
            int c = 3;
            NFluent.Check.That((a + b) + c)
                .IsEqualTo(a + (b + c));
        }
    }
}
