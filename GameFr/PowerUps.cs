using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;



namespace GameFr
{
    public class PowerUps : Entity
    {

        string[] ItemName = { "Herz", "Coin" };
        static Vector2 Empty = new Vector2();
        static List<Texture2D> ItemsTextures = new List<Texture2D>();
        public static List <PowerUps> PowerUpList = new List<PowerUps>();

        public bool Destroyable = false;
        enum PowerUpType
        {
            Herz,
            Coin
        }

        PowerUpType powerUpType;

        public static void Inistuff()
        {
            ItemsTextures.Add(Entity.Game1_.Content.Load<Texture2D>("Sprites/HerzItem"));
            ItemsTextures.Add(Entity.Game1_.Content.Load<Texture2D>("Sprites/Coin_Sprite"));
        }

        public static void ItemSpawner()
        {
            if (Spieler.Pause || EnemyEntity.EndTime != 0) return;
            Random rnd = new Random();

            

            if (EnemyEntity.GameStart && rnd.Next(0, 1005) == 64) PowerUpList.Add(new PowerUps(ItemsTextures[0], Vector2.Zero, 6));
        }

        public void ItemUpdate()
        {
            if (Spieler.Pause || EnemyEntity.EndTime != 0) return;

            if (CollidingEntity == Game1_._witcherEntity)
            {
                switch (powerUpType)
                {
                    case PowerUpType.Herz:
                            if (Drawable && Collided && CollidingEntity == Game1_._witcherEntity)                            {
                            //Entity.entities.Remove(Entity.entities[i]);
 
                                

                                Entity[] Hearts = FindEntitysTexture(Game1_._Hearts[0]._Texture.Name, Game1_._Hearts.Length);

                                for (int i = 0; i < Hearts.Length; i++)
                                {
                                    if (!Hearts[i].Drawable)
                                    {
                                    Game1_.Highscore += 50;
                                    Game1_.Lives++;
                                       Hearts[i].Drawable = true;
                                       Destroyable = true;
                                       Drawable = false;

                                    break;
                                    }
                                }
                            }

                    break;


                    case PowerUpType.Coin:
                        if (Drawable && Collided && CollidingEntity == Game1_._witcherEntity)
                        {
                            //Entity.entities.Remove(Entity.entities[i]);

                            Game1_.Highscore += 50;

                            Entity[] Coins = FindEntitysTexture(Game1_._Hearts[0]._Texture.Name, Game1_._Hearts.Length);

                            Destroyable = true;
                            Drawable = false;
                            
                        }

                    break;



                    default:
                        break;
                }
            }
        }

        public PowerUps(Texture2D texture, Vector2 StartPos, float length = 6) : base(texture, StartPos, length)
        {
            Random rnd = new Random();
            powerUpType = (PowerUpType)rnd.Next(0, ItemName.Length-1);

            if (powerUpType == PowerUpType.Coin) length = 1;

            _Texture = ItemsTextures[(int)powerUpType];
            

            StartPos.X = rnd.Next(10, (int)WindowSize.X - 50);
            StartPos.Y = rnd.Next(10, (int)WindowSize.Y - 50);

            Position = StartPos;
        }
    }
}
