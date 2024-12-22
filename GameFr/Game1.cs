using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace GameFr
{
    
    public class Entity
    {
        public static Vector2 WindowSize;
        public static List<Entity> entities = new List<Entity>();
        public static Game1 Game1_;
        public static SpriteBatch SpriteBatch_;


        public Vector2 Position;
        public Vector2 LastPositionMove = new Vector2 (0, 0);
        public Vector2 Rotation = new Vector2(1,1);
        public Vector2 Hitbox = new Vector2(1,1);

        public Texture2D _Texture;
        private float LengthNShit;
        protected Entity CollidingEntity;
        public int RenderLayer = 0;
        public bool HasColision = true;
        public bool Drawable = true;
        protected bool ignoreCollisionPush = false;
        public bool Collided = false;

        public float Health = 100.0f;





        Vector2 AreaVector = new Vector2();
        bool InArea(Entity ent)
        {

            // AreaVector = Position, ent.Position;
            Vector2 Bounds = getWidth();
            Vector2 EnemyBounds = ent.getWidth();


            //Links/Rechts
            if((Position.X + Bounds.X) > (ent.Position.X - EnemyBounds.X / HitboxDecreasion) && (Position.X - Bounds.X / HitboxDecreasion) < (ent.Position.X + EnemyBounds.X))
            {

                
                if((Position.X + Bounds.X) - (ent.Position.X - EnemyBounds.X / HitboxDecreasion) < 0)
                {
                    AreaVector.X += (Position.X + Bounds.X) - (ent.Position.X - EnemyBounds.X / HitboxDecreasion);
                }
                if ((Position.X - Bounds.X / HitboxDecreasion) - (ent.Position.X + EnemyBounds.X) > 0)
                {
                    AreaVector.X += (Position.X - Bounds.X / HitboxDecreasion) - (ent.Position.X + EnemyBounds.X);
                }


                //Unten/Oben
                if ((Position.Y + Bounds.Y) > (ent.Position.Y - EnemyBounds.Y / HitboxDecreasion) && (Position.Y - Bounds.Y / HitboxDecreasion) < (ent.Position.Y + EnemyBounds.Y))
                {



                    if ((Position.Y + Bounds.Y) - (ent.Position.Y - EnemyBounds.Y / HitboxDecreasion) < 0)
                    {
                        AreaVector.Y += (Position.Y + Bounds.Y) - (ent.Position.Y - EnemyBounds.Y / HitboxDecreasion);
                    }
                    if ((Position.Y - Bounds.Y / HitboxDecreasion) - (ent.Position.Y + EnemyBounds.Y) > 0)
                    {
                        AreaVector.Y += (Position.Y - Bounds.Y / HitboxDecreasion) - (ent.Position.Y + EnemyBounds.Y);
                    }

                    return true;
                }
            }





            return false;
        }

        public static Entity[] FindEntitys(string ClassName, int Length)
        {


            Entity[] Entitiess = new Entity[Length];
            int i2 = 0;
            for (int i = 0; i < entities.Count; i++)
            {
                if (ClassName == entities[i].GetType().Name)
                {
                    Entitiess[i2] = entities[i];
                    i2++;
                }
            }


            return Entitiess;
        }

        public static Entity[] FindEntitysTexture(string TextureName, int Length)
        {


            Entity[] Entitiess = new Entity[Length];
            int i2 = 0;
            for (int i = 0; i < entities.Count; i++)
            {
                if (TextureName == entities[i]._Texture.Name)
                {
                    Entitiess[i2] = entities[i];
                    i2++;
                }
            }


            return Entitiess;
        }

        bool isColiding()
        {
            for (int i = 0; entities.Count > i; i++)
            {
                if (entities[i] == this) continue;

                if (entities[i].HasColision)
                {


                    if (InArea(entities[i]))
                    {
                        
                        CollidingEntity = entities[i];
                        return true;
                    }
                }
            }


            return false;
        }
        const float HitboxDecreasion = 512;
        public void DrawHitbox(Color color)
        {


            Vector2 Bounds = getWidth();


            Vector2 Hitboxp1 = new Vector2(Position.X - Bounds.X / HitboxDecreasion, Position.Y + Bounds.Y / HitboxDecreasion);
            Vector2 Hitboxp2 = new Vector2(Position.X + Bounds.X, Position.Y + Bounds.Y / HitboxDecreasion);

            Vector2 Hitboxp3 = new Vector2(Position.X - Bounds.X / HitboxDecreasion, Position.Y + Bounds.Y);
            Vector2 Hitboxp4 = new Vector2(Position.X + Bounds.X, Position.Y + Bounds.Y);

            Vector2 Hitboxp5 = new Vector2(Position.X - Bounds.X / HitboxDecreasion, Position.Y + Bounds.Y / HitboxDecreasion);
            Vector2 Hitboxp6 = new Vector2(Position.X - Bounds.X / HitboxDecreasion, Position.Y + Bounds.Y);

            Vector2 Hitboxp7 = new Vector2(Position.X + Bounds.X, Position.Y + Bounds.Y / HitboxDecreasion);
            Vector2 Hitboxp8 = new Vector2(Position.X + Bounds.X, Position.Y + Bounds.Y);


            Game1_.DrawLine(Hitboxp1, Hitboxp2, 1.0f, color);
            Game1_.DrawLine(Hitboxp3, Hitboxp4, 1.0f, color);
            Game1_.DrawLine(Hitboxp5, Hitboxp6, 1.0f, color);
            Game1_.DrawLine(Hitboxp7, Hitboxp8, 1.0f, color);
        }

        public bool GetColliders()
        {
            Collided = false;
            if (HasColision && isColiding())
            {
                if (ignoreCollisionPush)
                {
                    Collided = true;
                    return false;
                }
                Position -= LastPositionMove;
                Collided = true;
                return true;
            }

            return false;
        }

        void MakeBounds(Vector2 pos)
        {
            Vector2 Width = getWidth();

            if ((Position.X + pos.X + Width.X) - WindowSize.X > 0)
            {
                Position.X += WindowSize.X - (Position.X + pos.X + Width.X);
            }

            if (0 - (Position.X + pos.X) > 0)
            {
                Position.X += 0 - (Position.X + pos.X);
            }


            if ((Position.Y + pos.Y + Width.Y) - WindowSize.Y > 0)
            {
                Position.Y += WindowSize.Y - (Position.Y + pos.Y + Width.Y);
            }

            if (0 - (Position.Y + pos.Y) > 0)
            {
                Position.Y += 0 - (Position.Y + pos.Y);
            }

        }



        public void Move(Vector2 pos)
        {

            LastPositionMove = pos;

            if(GetColliders())return;
            MakeBounds(pos);
            Position += pos;
        }

        protected void ResetVoid()
        {
            Hitbox = new Vector2(_Texture.Width / LengthNShit, _Texture.Height / LengthNShit);
        }

        protected void ChangeHitbox(Vector2 HitboxChange)
        {
            Hitbox.X = HitboxChange.X / LengthNShit;
            Hitbox.Y = HitboxChange.Y;
        }

        Vector2 getWidth()
        {
            
            return Hitbox;

        }

        public Rectangle getEntityRect()
        {
            Rectangle r1 = _Texture.Bounds;

            Rectangle r = new Rectangle( (int)Position.X, (int)Position.Y, (int)(r1.Width / LengthNShit), (int)(r1.Height / LengthNShit));

            return r;
        }

        public Entity(Texture2D texture, Vector2 StartPos, float length = 6)
        {
            _Texture = texture;
            Position = StartPos;
            LengthNShit = length;
            Hitbox = new Vector2(texture.Width / length, texture.Height / length);

            entities.Add(this); 
        }


        ~Entity()
        {
            entities.Remove(this);
        }

    }




    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        public Spieler _witcherEntity;
        public List<EnemyEntity> Enemy = new List<EnemyEntity>();
        Texture2D Background;
        public Entity[] _Hearts = new Entity[5];
        public bool DrawHitboxes = false;
        public int Lives = 5;
        public bool ExitGame = false;
        public bool Lost = false;

        double Timescore = 0;
        public int Highscore = 0;
        SpriteFont HighscoreFont;
        SpriteFont TextFont;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Entity.WindowSize.Y = _graphics.PreferredBackBufferHeight;
            Entity.WindowSize.X = _graphics.PreferredBackBufferWidth;
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();


         

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Entity.SpriteBatch_ = _spriteBatch;
            Entity.Game1_ = this;


            LineTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            LineTexture.SetData(new[] { Color.Red });


            // TODO: use this.Content to load your game content here

            _witcherEntity = new Spieler(new Texture2D[] { Content.Load<Texture2D>("Sprites/witcher_sprite_R"), Content.Load<Texture2D>("Sprites/witcher_sprite_L") }, new Vector2(10.0f, 10.0f), 8);
            //_BushEntity = new Entity(Content.Load<Texture2D>("Sprites/Plant1"), new Vector2(150.0f, 150.0f), 0.2f);

            Enemy.Add(new EnemyEntity(Content.Load<Texture2D>("Sprites/Plant1"), new Vector2(150.0f, 150.0f), 1.2f));
            Enemy.Add(new EnemyEntity(Content.Load<Texture2D>("Sprites/Plant1"), new Vector2(100.0f, 100.0f), 1.2f));
            Enemy.Add(new EnemyEntity(Content.Load<Texture2D>("Sprites/Plant1"), new Vector2(12.0f, 50.0f), 1.2f));
            Enemy.Add(new EnemyEntity(Content.Load<Texture2D>("Sprites/Plant1"), new Vector2(43.0f, 20.0f), 1.2f));
            Enemy.Add(new EnemyEntity(Content.Load<Texture2D>("Sprites/Plant1"), new Vector2(60.0f, 10.0f), 1.2f));

            TextFont = Content.Load<SpriteFont>("NormalFont");

            HighscoreFont = Content.Load<SpriteFont>("Score");

            PowerUps.Inistuff();

            Background = Content.Load<Texture2D>("Sprites/StrandPixel");
            for (int i = 0; i < _Hearts.Length; i++) {
                _Hearts[i] = new Entity(Content.Load<Texture2D>("Sprites/Herz"), new Vector2(Entity.WindowSize.X / 2 - 50 + (50.0f * i), 30.0f), 5.2f);
                _Hearts[i].HasColision = false;
            }

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            EnemyEntity.UpdateTime(gameTime.ElapsedGameTime.TotalSeconds);

            _witcherEntity.MovementLogic();
            _witcherEntity.GetColliders();
            for (int i = 0; i < Entity.entities.Count; i++)
            {
                Entity.entities[i].GetColliders();
            }

            for (int i = 0; i < PowerUps.PowerUpList.Count; i++)
            {
                PowerUps.PowerUpList[i].ItemUpdate();
                if (PowerUps.PowerUpList[i].Destroyable)
                {
                    Entity.entities.Remove(PowerUps.PowerUpList[i]);
                    PowerUps.PowerUpList.RemoveAt(i);
                }
            }

            PowerUps.ItemSpawner();


            for (int i = 0; i < Enemy.Count; i++)
            {
                Enemy[i].MoveToPlayer(gameTime.TotalGameTime.TotalSeconds);
            }


            
            /*
            if(EnemyEntity.GameStart && (Timescore += gameTime.ElapsedGameTime.TotalSeconds) > 5.0f)
            {
                Timescore = 0;
                Highscore += 50;
            }*/

            /*
            for (int i = 0; i < EnemyEntity.EnemyList.Count; i++)
            {
                EnemyEntity.EnemyList[i].MoveToPlayer(gameTime.TotalGameTime.TotalSeconds);
            }*/



            if (ExitGame)
            {
                Exit();
            }

            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();

            _spriteBatch.Draw(Background, new Rectangle(0, 0, (int)Entity.WindowSize.X, (int)Entity.WindowSize.Y), Color.White);


            if (Lost)
            {

                _spriteBatch.End();
            }


            //Draw all Entities
            for (int i = 0; Entity.entities.Count > i; i++)
            {
                if (Entity.entities[i]._Texture == null || !Entity.entities[i].Drawable) continue;
                _spriteBatch.Draw(Entity.entities[i]._Texture, Entity.entities[i].getEntityRect(), Color.White);


                if (DrawHitboxes)
                {
                    if (Entity.entities[i].Collided)
                    {
                        Entity.entities[i].DrawHitbox(Color.Blue);
                        continue;
                    }

                    Entity.entities[i].DrawHitbox(Color.Red);
                }
                

            }

            

            _spriteBatch.Draw(_witcherEntity._Texture, _witcherEntity.getEntityRect(), Color.White);

            if (!EnemyEntity.GameStart)
            {
                float TimeStart = (float)(EnemyEntity.GameTickBefore - gameTime.TotalGameTime.TotalSeconds);
                _spriteBatch.DrawString(TextFont, $"Game is starting: {TimeStart}", new Vector2((Entity.WindowSize.X / 2) - 70, 10), Color.Black);
            }
            else
            {
                //float TimeStart = (float)(EnemyEntity.GameTime);
                //_spriteBatch.DrawString(TextFont, $"Survive!: {TimeStart}", new Vector2((Entity.WindowSize.X / 2) - 160, 10), Color.Black);
                _spriteBatch.DrawString(HighscoreFont, $"Highscore: {Highscore}", new Vector2(20, 10), Color.Black);
            }


            _spriteBatch.End();



            base.Draw(gameTime);
        }


        private Texture2D LineTexture;
        public void DrawLine(Vector2 p1, Vector2 p2,float thickness, Color col)
        {
            Vector2 direction = p2 - p1;

            float angle = (float)Math.Atan2(direction.Y, direction.X);
            float length = direction.Length();

            _spriteBatch.Draw(
                LineTexture,
                p1,
                null,
                col,
                angle,
                Vector2.Zero,
                new Vector2(length, thickness),
                SpriteEffects.None,
                0
                ) ;
        }
    }
}