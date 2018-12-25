using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Day10_MonoGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            lights = problem.ParseInput(false);
            problem.ProgresSeconds(10066, lights);
            seconds = 10066;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if (canProgress)
                {
                    canProgress = false;
                problem.ProgresSeconds(1, lights);
                seconds++;
                lights = problem.MoveToOrigin(problem.Normalize(lights));
                }
            }

            if (Keyboard.GetState().IsKeyUp(Keys.Space) && canProgress == false)
            {
                canProgress = true;
            }

                // TODO: Add your update logic here

                base.Update(gameTime);
        }

        bool canProgress = true;
        int seconds = 0;
        List<Light> lights = new List<Light>();

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            
            Texture2D rect = new Texture2D(graphics.GraphicsDevice, 1, 1);

            Color[] data = new Color[1]
            {
                Color.Black
            };
            rect.SetData(data);

        //    for (int i = 0; i < data.Length; ++i) data[i] = Color.Chocolate;
          //      rect.SetData(data);

            Vector2 coor = new Vector2(10, 20);
            spriteBatch.Begin();
            

            foreach(Light l in lights)
            {
                Pixel p = new Pixel(l.Position.X, l.Position.Y, spriteBatch, graphics);
                p.Draw();
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    public class Pixel
    {
        public Pixel(int x, int y, SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            rect = new Texture2D(graphics.GraphicsDevice, 1, 1);
            Color[] data = new Color[1]
            {
                Color.Black
            };
            rect.SetData(data);
            this.SpriteBatch = spriteBatch;
            coordinate = new Vector2(x, y);
        }

        public void Draw()
        {
            SpriteBatch.Draw(rect, coordinate, Color.White);
        }

        Vector2 coordinate;
        Texture2D rect;
        SpriteBatch SpriteBatch;
        int size = 2;

    }
}
