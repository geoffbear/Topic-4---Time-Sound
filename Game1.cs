using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel.DataAnnotations;

namespace Topic_4___Time___Sound
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        SpriteFont timeFont;
        Texture2D bombTexture, explodeTexture;
        Rectangle bombRect, explodeRect;
        float seconds;
        float opacity = 0f;
        MouseState mouseState;
        SoundEffect kablamo;
        SoundEffectInstance kablamo2;
        bool boomed;
        bool exit = false;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
            bombRect = new Rectangle(50, 50, 700, 400);
            explodeRect = new Rectangle(50, 50, 700, 400);
            seconds = 0f;
            boomed = false;

           
            base.Initialize();

            kablamo2.IsLooped = true;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            bombTexture = Content.Load<Texture2D>("bomb");
            explodeTexture = Content.Load<Texture2D>("BOOM");
            timeFont = Content.Load<SpriteFont>("TimeFont");
            kablamo = Content.Load<SoundEffect>("explosion");
            kablamo2 = kablamo.CreateInstance();
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            seconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                seconds = 0;
            }

            if (seconds >= 10)
            {
                seconds = 0;
                boomed = true;
                kablamo2.Play();
            }
            if (boomed)
            {
               if (seconds > 0.1)
                {
                    seconds = 0;
                    opacity += 0.01f;
                    if (opacity >= 1f)
                    {
                        opacity = 1f;
                        exit = true;
                    }
                }
            }
            if (exit)
            {
                Exit();
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Crimson);

            // TODO: Add your drawing code here

            _spriteBatch.Begin();
            _spriteBatch.Draw(bombTexture, bombRect, Color.White);
            _spriteBatch.DrawString(timeFont, (10 - seconds).ToString("0:00"), new Vector2(280, 200), Color.DarkOliveGreen);
            _spriteBatch.Draw(explodeTexture, explodeRect, Color.White * opacity);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}