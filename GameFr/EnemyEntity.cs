using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace GameFr
{
    enum EnemyState
    {

        Normal,
        DestroyStart,
        Destroy
    }
    public class EnemyEntity : Entity
    {
        public float Speed = 6.0f;
        public EnemyEntity(Texture2D texture, Vector2 StartPos, float length = 6) : base(texture, StartPos, length)
        {
            Chill = true;
            GameTickBefore = Game1_.MaxElapsedTime.TotalSeconds + 5.0;
        }

        public static bool GameStart = false;
        public static bool Chill = false;
        public static double GameTime = 0;
        public static double GameTickBefore = 0;
        public static double GameTickBeforeFFFF = 0;
        public static double GameTickBeforeTimer = 0;
        public static double EndTime = 0;

        private double TickEnemyVanish = 0;

        EnemyState enemyState = EnemyState.Normal;

        bool DontMove = false;
        Random rnd = new Random();

        public static void UpdateTime(double time)
        {
            if(Spieler.Pause || EndTime != 0)
            {
                return;
            }

            GameTime += time;
        }
        public void MoveToPlayer(double GameTick)
        {
            if(Spieler.Pause || EndTime != 0)
            {
                return;
            }

            GameTickBeforeFFFF = GameTick;

            if (DontMove) return;

            if (Chill)
            {
                if (GameTick > GameTickBefore)
                {
                    Chill = false;
                    GameTickBefore = 0;
                    if(!GameStart)
                    {
                        GameTickBeforeTimer = GameTick;
                    }
                    GameStart = true;
                }
                else
                {
                    if (GameStart && Game1_._witcherEntity != null)
                    {
                        //float SpeedFr = rnd.Next(1, 4) * 0.026f;


                        //Position -= ((Position - Game1_._witcherEntity.Position) * (Speed + SpeedFr));
                        //Position.X += rnd.Next(1, 4) * 5.026f;


                        Vector2 direction = Game1_._witcherEntity.Position - Position;
                        direction.Normalize();


                        Position += direction * Speed;
                    }
                }
                return;
            }

            if(Game1_._witcherEntity != null)
            {

                Vector2 direction = Game1_._witcherEntity.Position - Position;
                direction.Normalize();

                Position += direction * Speed;
            }

            /*
            if (GameTick >= TickEnemyVanish || CollidingEntity == Game1_._witcherEntity)
            {
                TickEnemyVanish = GameTick + 500;
                Position.X = rnd.Next(10, (int)WindowSize.X - 20);
                Position.Y = rnd.Next(10, (int)WindowSize.Y - 20);
            }*/


            if (GetColliders())
            {

                

                if (CollidingEntity == Game1_._witcherEntity)
                {


                    

                    GameTickBefore =+ GameTick + 5.0;
                    Chill = true;

                    Entity[] Hearts = FindEntitysTexture(Game1_._Hearts[0]._Texture.Name, Game1_._Hearts.Length);

                    int MaxLen = Hearts.GetUpperBound(0);
                    for (int i = 0; i < Hearts.Length; i++)
                    {
                        if (Hearts[MaxLen - i].Drawable)
                        {
                            Hearts[MaxLen - i].Drawable = false;
                            break;
                        }
                    }

                    if (Game1_.Lives == 1)
                    {
                        EndTime = GameTick;
                        GameTickBefore = +GameTick + 10.0;
                    }
                    
                    Game1_.Lives--;
                }
            }
        }
    }
}
