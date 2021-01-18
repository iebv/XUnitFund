using System;
using Xunit;

namespace GameEngine.Tests
{
    public class GameStateFixture : IDisposable
    {
        public GameState State { get; private set; }

        public GameStateFixture()
        {
            State = new GameState();  
        }

        public void Dispose()
        {
        }
    }
}
