using System;
using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    public class PlayerCharacterShould: IDisposable
    {

        private readonly PlayerCharacter sut;
        private readonly ITestOutputHelper output;

        public PlayerCharacterShould(ITestOutputHelper output)
        {
            
            this.output = output;

            output.WriteLine("Creating new Player Character");
            sut = new PlayerCharacter();
        }


        public void Dispose()
        {
            output.WriteLine($"Disposing Player Character {sut.FullName}");
        }

        [Fact]
        public void BeInexperiencedWhenNew()
        {
   
            Assert.True(sut.IsNoob);

        }

        [Fact]
        public void CalculateFullName()
        {
    
            sut.FirstName = "Sarah";
            sut.LastName = "Smith";

            Assert.Equal("Sarah Smith", sut.FullName);
        }

        [Fact]
        public void HaveFullNameStartingWithFirstName()
        {

            sut.FirstName = "Sarah";
            sut.LastName = "Smith";

            Assert.StartsWith("Sarah", sut.FullName);
        }

        [Fact]
        public void HaveFullNameEndingWithLastName()
        {

            sut.FirstName = "Sarah";
            sut.LastName = "Smith";

            Assert.EndsWith("Smith", sut.FullName);
        }

        [Fact]
        public void CalculateFullName_IgnoreCaseAssert()
        {

            sut.FirstName = "SARAH";
            sut.LastName = "SMITH";

            Assert.Equal("Sarah Smith", sut.FullName, ignoreCase: true);
        }

        [Fact]
        public void CalculateFullName_SubstringAssert()
        {

            sut.FirstName = "Sarah";
            sut.LastName = "Smith";

            Assert.Contains("ah Sm", sut.FullName);

        }

        [Fact]
        public void CalculateFullNameWithTitleCase()
        {

            sut.FirstName = "Sarah";
            sut.LastName = "Smith";

            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", sut.FullName);

        }

        [Fact]
        public void StartWithDefaultHealth()
        {

            Assert.Equal(100, sut.Health);

        }

        [Fact]
        public void StartWithDefaultHealth_NotEqual()
        {

            Assert.NotEqual(0, sut.Health);

        }

        [Fact]
        public void IncreaseHealthAfterSleeping()
        {

            sut.Sleep(); //Expect increase between 1 and 100 inclusive

            Assert.InRange(sut.Health, 101, 200);

        }

        [Fact]
        public void NotHaveNickNameByDefault()
        {

            Assert.Null(sut.Nickname);

        }

        [Fact]
        public void HaveALongBow()
        {

            Assert.Contains("Long Bow", sut.Weapons);

        }

        [Fact]
        public void NotHaveMagicalSword()
        {

            Assert.DoesNotContain("Magical Sword", sut.Weapons);

        }

        [Fact]
        public void HaveAtLeastOneKindOfSword()
        {

            Assert.Contains(sut.Weapons, weapon => weapon.Contains("Sword"));

        }

        [Fact]
        public void HaveAllExpectedWeapons()
        {

            var expectedWeapons = new[]
            {
                "Long Bow",
                "Short Bow",
                "Short Sword"
            };

            Assert.Equal(expectedWeapons, sut.Weapons);

        }

        [Fact]
        public void HaveNoEmptyDefaultWeapons()
        {
   
            Assert.All(sut.Weapons, weapon => Assert.False(string.IsNullOrWhiteSpace(weapon)));

        }

        [Fact]
        public void RaiseSleptEvent()
        {

            Assert.Raises<EventArgs>(
                handler => sut.PlayerSlept += handler,
                handler => sut.PlayerSlept -= handler,
                () => sut.Sleep());

        }

        [Fact]
        public void RaisePropertyChangeEvent()
        {

            Assert.PropertyChanged(sut, "Health", () => sut.TakeDamage(10));

        }

        /*[Fact]
        public void TakeZeroDamage()
        {
            sut.TakeDamage(0);

            Assert.Equal(100, sut.Health);

        }

        [Fact]
        public void TakeSmallDamage()
        {
            sut.TakeDamage(1);

            Assert.Equal(99, sut.Health);

        }

        [Fact]
        public void TakeMediumDamage()
        {
            sut.TakeDamage(50);
            Assert.Equal(50, sut.Health);

        }

        [Fact]
        public void HaveMinimunHealth()
        {
            sut.TakeDamage(101);

            Assert.Equal(1, sut.Health);

        }*/

        [Theory]
        [MemberData(nameof(InternalHealthDamageTestData.TestData), 
            MemberType = typeof(InternalHealthDamageTestData))]
        public void TakeDamage(int damage, int expectedHealth)
        {

            sut.TakeDamage(damage);

            Assert.Equal(expectedHealth, sut.Health);

        }
    }
}