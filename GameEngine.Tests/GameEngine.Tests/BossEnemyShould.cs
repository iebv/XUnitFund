using System;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    public class BossEnemyShould
    {
        private readonly ITestOutputHelper output;

        public BossEnemyShould(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        [Trait("Category", "Boss")]
        public void HaveCorrectPower()
        {
            output.WriteLine("Creating Boss Enemy");
             
            BossEnemy sut = new BossEnemy();

            Assert.Equal(166.667, sut.TotalSpecialAttackPower, 3);
        }
        
    }
}
