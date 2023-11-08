using BloomPostprocess;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ShapeBlaster;
using System;
using WindowsFormsApplication1;
using WindowsFormsApplication1.ParticleEffects;

namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D playField;
        Vector2 playFieldLocation;
        public static WindowsFormsApplication1.ParticleEffects.ParticleManager<ParticleState> ParticleManager1 { get; private set; }
        GameState _state = new GameState();
        KeyboardState currentKeyboardState;
        
        playerSprite player;
        Bullet b = new Bullet();
        Enemy Dinner;
        Background stars;
        EnemySpawner spawn;
        SpriteFont font;

        Scrolling scrolling1;
        Scrolling scrolling2;
        Scrolling mainMenu;
        KeyboardState mPreviousKeyboardState;
        BloomComponent bloom;
        Level firstLevel;
        public Game1()
        {
            

            graphics = new GraphicsDeviceManager(this);
            this.IsFixedTimeStep = false;//max fps
            this.graphics.SynchronizeWithVerticalRetrace = false;//vsync removes vsync on slower montors to let the fps run uncapped


            bloom = new BloomComponent(this);
            bloom.Settings = new BloomSettings(null, .25f, 6, 2, 1, 4, 4);
            Components.Add(bloom);

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
            //GlobalVars.readLevel("Content/level1.json");
            firstLevel = new Level("Content/level1.json");//load a level
            ParticleManager1 = new ParticleManager<ParticleState>(1024 * 20, ParticleState.UpdateParticle);
            playField = new Texture2D(GraphicsDevice, GraphicsDevice.Viewport.Width / 3, GraphicsDevice.Viewport.Height);
            playFieldLocation = new Vector2(10, 20);
            //KeyboardState 
            // TODO: Add your initialization logic here
            player = new playerSprite();
            Dinner = new Enemy();
            stars = new Background(null);
            //spawn = new EnemySpawner(100, 100);
            spawn = new EnemySpawner(firstLevel.getEnemies());
            // b = new Bullet();
            //EntityManager.Add(stars);
            EntityManager.Add(player);
            EntityManager.Add(Dinner);
            EntityManager.Add(spawn.getEnemies()[0]);

            foreach (Enemy spawnedenemy in spawn.getEnemies())
            {
                EntityManager.Add(spawnedenemy);
            }
            //_state = new GameState();
            _state.GameStateTypePropertie = GameState.GameStateType.menu;
            GlobalVars.SCREEN_HEIGHT = GraphicsDevice.DisplayMode.Height;
            GlobalVars.SCREEN_WIDTH = GraphicsDevice.DisplayMode.Width;

            graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
            //graphics.IsFullScreen = false;
            graphics.IsFullScreen = true;
            //graphics.ToggleFullScreen();    
            graphics.ApplyChanges();
            _state.GameStateTypePropertie = GameState.GameStateType.menu;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            scrolling1 = new Scrolling(Content.Load<Texture2D>("startest.jpg"), new Rectangle(0, 0, 1800, 1600));
            scrolling2 = new Scrolling(Content.Load<Texture2D>("startest.jpg"), new Rectangle(0, -1600, 1800, 1600));
            mainMenu = new Scrolling(Content.Load<Texture2D>("imgres.png"), new Rectangle(0, 0, 1800, 1600));
            font = Content.Load<SpriteFont>("gamefont");
            //Console.WriteLine(Content.Load<Texture2D>("TechBreak"));
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player.LoadContent(this.Content);
            Dinner.LoadContent(this.Content);
            spawn.getEnemies()[0].LoadContent(this.Content);
            foreach (Enemy spawnedenemy in spawn.getEnemies())
            {
                spawnedenemy.LoadContent(this.Content);
            }
            stars.LoadContent(this.Content);
            Art.Load(this.Content);
            //b.LoadContent(this.Content);
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

            currentKeyboardState = Keyboard.GetState();

            switch (_state.GameStateTypePropertie)
            {
                case GameState.GameStateType.menu:
                    updateMenu();
                    Console.WriteLine("main menu");
                    break;
                case GameState.GameStateType.gameplay:
                    updateGameplay(gameTime);
                    break;
                case GameState.GameStateType.gameover:
                    //updateGameOver();
                    break;
            }           

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            //drawGame(spriteBatch,gameTime);
            switch (_state.GameStateTypePropertie)
            {
                case GameState.GameStateType.menu:
                    drawMainMenu(gameTime);
                    break;
                case GameState.GameStateType.gameplay:
                    drawGame(spriteBatch, gameTime);
                    break;
                case GameState.GameStateType.gameover:
                    //updateGameOver();
                    break;


            }

            //b.Draw(this.spriteBatch);
            //spriteBatch.Draw(mSpriteTexture, mPosition, Color.White);
            // spriteBatch.End();
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
               
            // TODO: Add your drawing code here

           
            
        }
        public void updateMenu()
        {
            Console.WriteLine("listening...");
            if (currentKeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter) == true && mPreviousKeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter) == false)
            {
                _state.GameStateTypePropertie = GameState.GameStateType.gameplay;
                Console.WriteLine("enter pressed");
            }
        }
        public void updateGameplay(GameTime gameTime)
        {
            mPreviousKeyboardState = currentKeyboardState;
            GlobalVars.ELAPSED_TIME = gameTime;

            //spriteBatch.DrawString(scoreFont, ELAPSED_TIME.ToString(), new Vector2(10, 10), Color.White);
            Console.WriteLine(GlobalVars.ELAPSED_TIME.TotalGameTime.Seconds);
            ParticleManager1.Update(gameTime);
            /*if (scrolling1.rectangle.Y + scrolling1.texture.Height >= 1800)
                scrolling1.rectangle.Y = 0;
            if (scrolling2.rectangle.Y + scrolling2.texture.Height >= 1800)
                scrolling2.rectangle.Y = scrolling1.texture.Height;*/

            if (scrolling1.rectangle.Y + scrolling1.texture.Height >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)//somehow refactor this to use the entity manager
                scrolling1.rectangle.Y = -1600;
            if (scrolling2.rectangle.Y + scrolling2.texture.Height >= GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width)//same with this
                scrolling2.rectangle.Y = -1600;
            
            scrolling1.Update();
            scrolling2.Update();
            //mainMenu.Update();

            EntityManager.Update(gameTime);
            //ParticleManager1.
            //TODO: Add your update logic here
            //player.Update(gameTime);
            //b.Update(gameTime); 
            //Dinner.Update(gameTime);

            Rectangle playerBounds = new Rectangle((int)player.Position.X, (int)player.Position.Y, 50, 100);
            Rectangle enemyBounds = new Rectangle((int)Dinner.Position.X, (int)Dinner.Position.Y, 50, 100);

            if (playerBounds.Intersects(enemyBounds))
            {
                //Console.WriteLine("collided");
            }
        }
        public void drawGame(SpriteBatch spriteBatch, GameTime gameTime)
        {



           

            bloom.BeginDraw();            
            //bloom.Draw(gameTime);
           // spriteBatch.Begin();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend);
            //priteBatch.Begin(0, BlendState.Opaque);
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied); //start my spritebatch  
            
            GraphicsDevice.Clear(Color.Transparent);
            //.bloombloom.GraphicsDevice.Flush();
            drawBackGround(); 
           
            //stars.Draw(this.spriteBatch);
            //spawn.getEnemies()[0].Draw(this.spriteBatch);
            //spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            //player.Draw(this.spriteBatch);
            EntityManager.Draw(spriteBatch);
            Dinner.Draw(this.spriteBatch);

            ParticleManager1.Draw(this.spriteBatch);
            var fps = 1 / gameTime.ElapsedGameTime.TotalSeconds;
            Window.Title = fps.ToString() + "";
            
     
            //GraphicsDevice.SetRenderTarget(null);
            //GraphicsDevice.Clear(new Color(0, 0, 0, 0));
            spriteBatch.End();
            base.Draw(gameTime);
            //draw again?????

            spriteBatch.Begin(0, BlendState.NonPremultiplied);
            //spriteBatch.Begin();
            //spriteBatch.Begin(0, BlendState.Opaque);
            EntityManager.Draw(spriteBatch);
            Dinner.Draw(this.spriteBatch);
            //Dinner.Draw(this.spriteBatch);
            
            //GraphicsDevice.Clear(Color.Transparent);
            
            Color[] data = new Color[GraphicsDevice.Viewport.Height * 200];
            for (int i = 0; i < data.Length; ++i) data[i] = Color.Green;
            //rect.GraphicsDevice.Clear(Color.Transparent);
            playField.SetData(data);
            //playField.Utilities.CreateBorder(5, Color.Red);
            spriteBatch.Draw(playField, playFieldLocation, Color.Green);
            //Rectangle gameField = new Rectangle(0, 0, 1800, 1600);
            
            //ParticleManager1.Draw(this.spriteBatch);
            spriteBatch.End();//draw again end...
            drawOverlay();
            
            
        }
        public void drawMainMenu( GameTime gameTime)
        {
            spriteBatch.Begin();
            mainMenu.Draw(spriteBatch);
            spriteBatch.End();
            //base.Draw(gameTime);
        }
        public void drawOverlay()
        {
            spriteBatch.Begin();
            
            spriteBatch.DrawString(font, "bodied", new Vector2(64, 64), Color.White);
            spriteBatch.DrawString(font, "Lives " + GlobalVars.LIVES.ToString(), new Vector2(64, 84), Color.White);
            spriteBatch.DrawString(font, "Health " + GlobalVars.HEALTH.ToString(), new Vector2(64, 94), Color.White);
            spriteBatch.DrawString(font, "Score " + GlobalVars.SCORE.ToString(), new Vector2(64, 104), Color.White);
            spriteBatch.DrawString(font, "kill count " + GlobalVars.KILL_COUNT.ToString(), new Vector2(64, 174), Color.White);
            spriteBatch.DrawString(font, "Bloom Buffer " + bloom.GraphicsDevice.Metrics.ClearCount.ToString(), new Vector2(64, 274), Color.White);
            spriteBatch.DrawString(font, "enemies spawned " + firstLevel.currentLevel(), new Vector2(64, 294), Color.White);
            ;
            
            spriteBatch.End();
        }
        public void drawBackGround() 
        {
            //spriteBatch.Begin();
            scrolling1.Draw(spriteBatch);
            scrolling2.Draw(spriteBatch);
            //spriteBatch.End();
        }
    }
}
