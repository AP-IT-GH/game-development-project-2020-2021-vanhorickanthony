using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameDevelopment.Input
{
    public class MouseReader : IInputReader
    {
        public Vector2 ReadInput(Vector2 direction)
        {
            throw new NotImplementedException();
        }

        public bool ReadFollower()
        {
            throw new NotImplementedException();
        }

        public Vector2 ReadInput()
        {
            MouseState state = Mouse.GetState();
            var mouseVector = new Vector2(state.X, state.Y);
            return mouseVector;
        }
    }
}
