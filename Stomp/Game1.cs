#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Net;
using FarseerPhysics.Factories;
#endregion

namespace Stomp
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        List<Fixture> fixturesToDestroy;
        List<Fixture> fixturesToEnable;
        List<Enemy> enemyList;
        List<Floor> floorList;
        List<Water> waterList;
        public List<Bullet> bulletList;
        public static World world;
        Player player;
        PacketReader packetReader = new PacketReader();
        PacketWriter packetWriter = new PacketWriter();

        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            world = new World(new Vector2(0f, 9.81f));
           
        
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

            base.Initialize();
            graphics.PreferredBackBufferHeight = 480;
            graphics.PreferredBackBufferWidth = 640;
            Window.IsBorderless = true;
            graphics.IsFullScreen = true;
            
            graphics.ApplyChanges();
            fixturesToDestroy = new List<Fixture>();
            fixturesToEnable = new List<Fixture>();
            enemyList = new List<Enemy>();
            bulletList = new List<Bullet>();
            floorList = new List<Floor>();
            waterList = new List<Water>();
            player = new Player("mario", new Vector2(100f, 100f),-1, Keys.W, Keys.A, Keys.D);
            bulletList.Add(new Bullet(10, new Vector2(400f, -100f)));
            
            enemyList.Add(new Enemy(Content.Load<Texture2D>("boxEmpty"), new Vector2(160f, -100f), 100));
            floorList.Add( new Floor(Content.Load<Texture2D>("Grass"), new Vector2(32, 480-32)));
            floorList.Add(new Floor(Content.Load<Texture2D>("Grass"), new Vector2(96, 480 - 32)));
            floorList.Add(new Floor(Content.Load<Texture2D>("Grass"), new Vector2(160, 480 - 32)));
            waterList.Add(new Water(Content.Load<Texture2D>("iceWaterMid"), new Vector2(64 + 160, 480 - 32)));
            waterList.Add(new Water(Content.Load<Texture2D>("iceWaterMid"), new Vector2(64*2 + 160, 480 - 32)));
            waterList.Add(new Water(Content.Load<Texture2D>("iceWaterMid"), new Vector2(64*3 + 160, 480 - 32)));
            waterList.Add(new Water(Content.Load<Texture2D>("iceWaterMid"), new Vector2( 64*4 + 160, 480 - 32)));
            waterList.Add(new Water(Content.Load<Texture2D>("iceWaterMid"), new Vector2(64*5 + 160, 480 - 32)));
            waterList.Add(new Water(Content.Load<Texture2D>("iceWaterMid"), new Vector2(64*6 + 160, 480 - 32)));
            waterList.Add(new Water(Content.Load<Texture2D>("iceWaterMid"), new Vector2(64 *7+ 160, 480 - 32)));
            floorList.Add(new Floor(Content.Load<Texture2D>("Grass"), new Vector2(64*8 + 160, 480 - 32)));
            floorList.Add(new Floor(Content.Load<Texture2D>("Grass"), new Vector2(64*9 + 160, 480 - 32)));
            floorList.Add(new Floor(Content.Load<Texture2D>("Grass"), new Vector2(64 * 10 + 160, 480 - 32)));
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

        public void disableFixture(Fixture fixture)
        {
            fixturesToDestroy.Add(fixture);
        }
        public void enableFixture(Fixture fixture)
        {
            fixturesToEnable.Add(fixture);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));
            foreach(Fixture fixture in fixturesToDestroy){
                fixture.Body.Enabled = false;
                
            }
            fixturesToDestroy.Clear();
            foreach (Fixture fixture in fixturesToEnable)
            {
                fixture.Body.Enabled = true;
                
            }
            fixturesToEnable.Clear();
            player.Update();
            
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SkyBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            player.Draw(spriteBatch);
            foreach (Enemy enemy in enemyList)
            {
                enemy.Draw(spriteBatch);
            }
            foreach (Bullet bullet in bulletList)
            {
                bullet.Draw(spriteBatch);
            }

            foreach (Floor floor in floorList)
            {
                floor.Draw(spriteBatch);
            }
            foreach (Water water in waterList)
            {
                water.Draw(spriteBatch);
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
