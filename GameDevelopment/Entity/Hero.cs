using Microsoft.Xna.Framework;

using GameDevelopment.Animation.HeroAnimations;
using GameDevelopment.Animation.Interfaces;
using GameDevelopment.Input;

using GameDevelopment.Entity.Abstracts;

namespace GameDevelopment.Entity
{
    
    public class Hero: BaseEntity
    {

        public Hero(Vector2 spawnPosition, IAnimationSheet idleSheet, IAnimationSheet walkSheet, IAnimationSheet runSheet, IAnimationSheet jumpSheet, IInputReader inputReader):
            base(spawnPosition, inputReader, new Rectangle((int)spawnPosition.X, (int)spawnPosition.Y, 16, 40))
        {
            Animations.Add(new Vector2(0, 0), new IdleAnimation(idleSheet, this));
            Animations.Add(new Vector2(1, 0), new WalkAnimation(walkSheet, this));
            Animations.Add(new Vector2(2, 0), new RunAnimation(runSheet, this));
            Animations.Add(new Vector2(0, 2), new JumpAnimation(jumpSheet, this));

            SelectedAnimation = Animations[new Vector2(0, 0)];
        }
    }
}