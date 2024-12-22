using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GameFr
{
    public class Spieler : Entity
    {
        public enum CharacterState
        {
            Right,
            Left,
            Up,
            Down
        }

        Texture2D[] TexturesStates;
        



        int Speed = 5;
        public CharacterState state = CharacterState.Right;


        public void IniTextures(Texture2D[] FF)
        {
            TexturesStates = FF;
        }


        bool WasUpHitbox = true;
        bool WasUpPause = true;
        private bool LockMovement = false;
        public static bool Pause = false;
        public void MovementLogic()
        {
            if (LockMovement) return;

            KeyboardState state = Keyboard.GetState();

            
            if (state.IsKeyDown(Keys.G) && WasUpPause)
            {
                WasUpPause = false;
                Pause = !Pause;
            }
            else
            {
                if (!WasUpPause && state.IsKeyUp(Keys.K)) WasUpPause = true;
            }

            if (Pause) return;


            if (state.IsKeyDown(Keys.Left))
            {
                this._Texture = this.TexturesStates[1];
                this.state = CharacterState.Left;
                this.Move(new Vector2(-Speed, 0));
            }

            if (state.IsKeyDown(Keys.Right))
            {
                this._Texture = this.TexturesStates[0];
                this.state = CharacterState.Right;
                this.Move(new Vector2(Speed, 0));
            }

            if (state.IsKeyDown(Keys.Down))
            {
                this.Move(new Vector2(0, Speed));
            }

            if (state.IsKeyDown(Keys.Up))
            {
                this.Move(new Vector2(0, -Speed));
            }

            if (state.IsKeyDown(Keys.K) && WasUpHitbox)
            {
                WasUpHitbox = false;
                Entity.Game1_.DrawHitboxes = !Entity.Game1_.DrawHitboxes;
            }
            else
            {
                if (!WasUpHitbox && state.IsKeyUp(Keys.K)) WasUpHitbox = true;
            }
        }


        public Spieler(Texture2D[] texture, Vector2 StartPos, int Speed, float length = 6) : base(texture[0], StartPos, length) 
        {
            this.Speed = Speed;
            IniTextures(texture);
            this.ignoreCollisionPush = true;
        }
    }
}
