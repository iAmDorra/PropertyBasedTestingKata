using FsCheck;
using FsCheck.Xunit;
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

        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(10, 32, -73)]
        public void Sum_associativity(int a, int b, int c)
        {
            NFluent.Check.That((a + b) + c)
                .IsEqualTo(a + (b + c));
        }

        [Property]
        public Property Associativity_of_sum(int a, int b, int c)
        {
            return Prop.When(Global.AnyInput,
                () => (a + b) + c == a + (b + c));
        }

        [Property]
        public Property Associativity_of_sum_with_positif_numbers(int a, int b, int c)
        {
            return Prop.When(a > 0 && b > 0 && c > 0,
                () => (a + b) + c == a + (b + c));
        }

        [Property(Arbitrary = new[] { typeof(PositiveNumberGenerator) })]
        public Property AssociativityWith_positive_numbers(int a, int b, int c)
        {
            return Prop.When(Global.AnyInput,
                () => (a + b) + c == a + (b + c));
        }

        public static class PositiveNumberGenerator
        {
            public static Arbitrary<int> Generate()
            {
                return Arb.Default.Int32()
                    .MapFilter(
                    x => Math.Abs(x),
                    x => Global.AnyInput);
            }
        }
    }
}
