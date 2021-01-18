using Xunit;
using Xunit.Abstractions;

namespace GameEngine.Tests
{
    public class GameStateShould : IClassFixture<GameStateFixture>
    {
        private readonly GameStateFixture gameStateFixture;
        private readonly ITestOutputHelper output;

        public GameStateShould(GameStateFixture gameStateFixture, ITestOutputHelper output)
        {
            this.gameStateFixture = gameStateFixture;
            this.output = output;
        }
        [Fact]
        public void DamageAllPlayersWhenEarthquake()
        {
            output.WriteLine($"GameState ID={gameStateFixture.State.Id}");

            var player1 = new PlayerCharacter();
            var player2 = new PlayerCharacter();

            gameStateFixture.State.Players.Add(player1);
            gameStateFixture.State.Players.Add(player2);

            var expectedHealthAfterEarthquake = player1.Health - GameState.EarthquakeDamage;

            gameStateFixture.State.Earthquake();

            Assert.Equal(expectedHealthAfterEarthquake, player1.Health);
            Assert.Equal(expectedHealthAfterEarthquake, player2.Health);
        }

        [Fact]
        public void Reset()
        {
            output.WriteLine($"GameState ID={gameStateFixture.State.Id}");

            var player1 = new PlayerCharacter();
            var player2 = new PlayerCharacter();

            gameStateFixture.State.Players.Add(player1);
            gameStateFixture.State.Players.Add(player2);

            gameStateFixture.State.Reset();

            Assert.Empty(gameStateFixture.State.Players);
        }
    }
}
