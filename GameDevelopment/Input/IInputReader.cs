using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestGame.Input
{
    public interface IInputReader
    {
        Vector2 ReadInput();

        bool ReadFollower();
    }
}
