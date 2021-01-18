using System;
using Xunit;

namespace GameEngine.Tests
{
    [Trait("Category", "Enemy")]
    public class EnemyFactoryShould
    {
        [Fact]
        
        public void CreateNormalEnemyByDefault()
        {
            EnemyFactory sut = new EnemyFactory();

            Enemy enemy = sut.Create("Zombie");

            Assert.IsType<NormalEnemy>(enemy);
        }

        [Fact(Skip = "Dont need to run this")]
        public void CreateNormalEnemyByDefaul_NotType()
        {
            EnemyFactory sut = new EnemyFactory();

            Enemy enemy = sut.Create("Zombie");

            Assert.IsNotType<DateTime>(enemy);
        }

        [Fact]
        public void CreateBossEnemy()
        {
            EnemyFactory sut = new EnemyFactory();

            Enemy enemy = sut.Create("Zombie King", true);

            Assert.IsType<BossEnemy>(enemy);
        }

        [Fact]
        public void CreateBossEnemy_CastReturnedType()
        {
            EnemyFactory sut = new EnemyFactory();

            Enemy enemy = sut.Create("Zombie King", true);

            BossEnemy boss = Assert.IsType<BossEnemy>(enemy);

            Assert.Equal("Zombie King", boss.Name);
        }


        [Fact]
        public void CreateBossEnemy_assertAssignableTypes()
        {
            EnemyFactory sut = new EnemyFactory();

            Enemy enemy = sut.Create("Zombie King", true);

            Assert.IsAssignableFrom<Enemy>(enemy); //Checks for supeclass type
        }

        [Fact]
        public void CreateSeparateInstances()
        {
            EnemyFactory sut = new EnemyFactory();

            Enemy enemy1 = sut.Create("Zombie");
            Enemy enemy2 = sut.Create("Zombie");

            Assert.NotSame(enemy1, enemy2);

        }


        [Fact]
        public void CheckSameInstance()
        {
            EnemyFactory sut = new EnemyFactory();

            Enemy enemy1 = sut.Create("Zombie");
            Enemy enemy2 = enemy1;

            Assert.Same(enemy1, enemy2);

        }

        [Fact]
        public void NotAllowNullName()
        {
            EnemyFactory sut = new EnemyFactory();

            //Assert.Throws<ArgumentNullException>(() => sut.Create(null));

            Assert.Throws<ArgumentNullException>("name", () => sut.Create(null));

        }

        [Fact]
        public void OnlyAllowKingorQueenBossEnemies()
        {
            EnemyFactory sut = new EnemyFactory();

            EnemyCreationException ex = Assert.Throws<EnemyCreationException>(() => sut.Create("Zombie", true));

            Assert.Equal("Zombie", ex.RequestedEnemyName);

        }

        
    }
}
