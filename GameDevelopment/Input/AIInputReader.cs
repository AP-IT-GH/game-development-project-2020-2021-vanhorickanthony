using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameDevelopment.Input
{
    class AIInputReader : IInputReader
    {
        private Vector2 _currentInput = Vector2.Zero;
        
        public Vector2 ReadInput()
        {
            return _currentInput;
        }

        public bool ReadFollower()
        {
            KeyboardState state = Keyboard.GetState();
            
            if (state.IsKeyDown(Keys.M))
                return true;

            return false;
        }

        public Vector2 ReadInput(Vector2 direction)
        {
            if (direction.X > float.Epsilon)
            {
                Console.WriteLine("Move right.");

                direction = new Vector2(1, 0);
            }
            else if (direction.X < - float.Epsilon)
            {
                Console.WriteLine("Move left.");

                direction = new Vector2(-1, 0);
            }
            else
            {
                Console.WriteLine("Stand still.");

                direction = new Vector2(0, 0);
            }

            _currentInput = direction;

            Console.WriteLine(_currentInput);

            return direction;
        }
    }
}
