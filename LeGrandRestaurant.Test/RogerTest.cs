using System.Linq;
using LeGrandRestaurant.Test.Utilities;
using Xunit;

namespace LeGrandRestaurant.Test
{
    public class RogerTest
    {
        [Fact]
        public void Roger_A_Un_CA_De_0()
        {
            var roger = new ServeurBuilder()
                .Nommé("Roger")
                .Build();

            Assert.Equal(0, roger.ChiffreAffaires);
        }

        [Fact]
        public void Plusieurs_Roger_Ont_Un_CA_De_0()
        {
            var arméeDeRoger = new ServeurGenerator()
                .Nommés("Roger")
                .Generate(800);

            Assert.Equal(0, arméeDeRoger.Sum(roger => roger.ChiffreAffaires));
        }
    }
}
