using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using System.Collections;

namespace MindFlayer
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MindFlayer : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D pixel;
        Game game;
        Random randomNumberGenerator;
        Stack<Collision> collisions;
        Menu mainMenu;
        SpriteFont font;
        KeyboardState currentState, previousState;

        //TEST**********************
        //bool isCollision;
        //TEST**********************
        byte[] colorBytes;

        public MindFlayer()
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
            game = new Game(new Rectangle(GraphicsDevice.Viewport.X, GraphicsDevice.Viewport.Y, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height));
            randomNumberGenerator = new Random();
            //isCollision = false;
            mainMenu = new Menu();
            mainMenu.AddItem("Single-Player");
            mainMenu.AddItem("Co-op 2-Player");
            mainMenu.AddItem("Competitive 2-Player");
            mainMenu.AddItem("Quit");
            mainMenu.activated = true;
            colorBytes = new byte[3];
            currentState = Keyboard.GetState();

            font = Content.Load<SpriteFont>("courierNew");



            //TEST**************************
            //randomNumberGenerator.NextBytes(colorBytes);
            //game.asteroids.Add(new GameObject(new Vector2(0, 0), new Vector2(1.0f, 0.75f), 10, new Color(colorBytes[0], colorBytes[1], colorBytes[2]), 0.0f, 0.0f));
            //TEST**************************

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
            pixel = Content.Load<Texture2D>("pixel");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            previousState = currentState;
            currentState = Keyboard.GetState();

            if (mainMenu.activated)
            {
                HandleMenuKeys();
            }
            else
            {
                game.Update();
                collisions = game.GetCollisions();
                //TEST**************************
                //if (collisions.Count != 0)
                //    isCollision = true;
                //else
                //    isCollision = false;
                //TEST**************************
            }

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            base.Update(gameTime);
        }

        private void HandleMenuKeys()
        {
            if (KeyPressed(Keys.Down))
            {
                mainMenu.SelectNext();
            }
            if (KeyPressed(Keys.Up))
            {
                mainMenu.SelectPrevious();
            }
        }

        private bool KeyPressed(Keys key)
        {
            return currentState.IsKeyDown(key) && previousState.IsKeyUp(key);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //TEST************************************
            //if (isCollision)
            //    GraphicsDevice.Clear(Color.White);
            //else
            //TEST************************************
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            //TEST***************************
            //DrawLine(new Vector2(0.0f, 0.0f), new Vector2(100.0f, 100.0f), Color.White);
            //TEST***************************


            if (mainMenu.activated)
            {
                DrawMenu(mainMenu);
            }
            else
            {
                DrawGame(game);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawGame(Game game)
        {
            foreach (GameObject asteroid in game.asteroids)
            {
                DrawObject(asteroid);
            }
        }

        private void DrawMenu(Menu menu)
        {
            Color color;
            foreach (String item in menu.GetOptions())
            {
                if (mainMenu.GetHighlighted() == mainMenu.GetOptions().IndexOf(item))
                {
                    color = Color.Yellow;
                }
                else
                {
                    color = Color.White;
                }
                DrawString(new Vector2(GraphicsDevice.Viewport.Width / 2, 50f + 50f * mainMenu.GetOptions().IndexOf(item)), item, color);
            }
        }

        private void DrawString(Vector2 position, String text, Color colour)
        {
            spriteBatch.DrawString(font, text, position,
                colour, 0, font.MeasureString(text) / 2, 1.0f, SpriteEffects.None, 0.5f);
        }

        private void DrawLine(Vector2 start, Vector2 end, Color colour)
        {
            spriteBatch.Draw(pixel, start, null, colour,
                (float)Math.Atan2(end.Y - start.Y, end.X - start.X),
                new Vector2(0, 0),
                new Vector2(Vector2.Distance(start, end), 1),
                SpriteEffects.None, 0);
        }

        private void DrawObject(GameObject asteroid)
        {
            foreach (Vector2 point1 in asteroid.vertices)
            {
                foreach (Vector2 point2 in asteroid.vertices)
                {
                    DrawLine(point1, point2, asteroid.colour);
                }
            }
        }
    }
}
