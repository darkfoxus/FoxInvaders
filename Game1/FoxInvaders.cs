using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace FoxInvaders
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class FoxInvaders : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D sprites;

        // Hay diferencias si lo defino tipo int? mas facil para pixeles 
        // (pos*vel) pequeños - mov -> cero
        double shipPosX; 
        double shipPosY;

        //Velocidad por defecto de la nave
        Vector2 shipVelocity = new Vector2(230, 0);

        //sprite position in the game frame
        Vector2 shipPosition;
        Vector2 shootPosition;
        //sprite coordinates on the GIF file
        Rectangle shipSpritePositionOnFile;
        Rectangle shootSpritePositionOnFile;

        public FoxInvaders()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            this.Window.AllowUserResizing = true;
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
            // qué ocurre aqui si coloco (int) o quito el  (double)?
            this.shipPosX = (double)(GraphicsDevice.PresentationParameters.BackBufferWidth / 2);
            this.shipPosY = (double)(GraphicsDevice.PresentationParameters.BackBufferHeight * (1 - 0.20f));

            //initial position in the game frame
            shipPosition = new Vector2(
                (int)this.shipPosX, // Por que el cast a entero? 
                (int)this.shipPosY);

            //sprite position in the game frame
            //esquina superior derecha x,y - ancho, largo
            shipSpritePositionOnFile = new Rectangle(150, 637, 75, 53);
            shootSpritePositionOnFile = new Rectangle(235, 514, 10, 41);
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
            this.sprites=this.Content.Load<Texture2D>("sprites");
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

            // TODO: Add your update logic here
            double distanceMoved =
                    // que pasa si pongo (int) ? mov -> 0
                    (double)(shipVelocity.X *
                             gameTime.ElapsedGameTime.TotalSeconds 
                             );
            //#####################################################################################
            // SHIP MOVEMENT ######################################################################
            //#####################################################################################
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                // is the ship at right end?
                if (shipPosX + distanceMoved 
                    < 
                    GraphicsDevice.PresentationParameters.BackBufferWidth -
                        shipSpritePositionOnFile.Width)// que hace esto?
                {
                    shipPosX += distanceMoved;
                }
                else
                {   // esto que hace?
                    shipPosX = GraphicsDevice.PresentationParameters.BackBufferWidth
                        - shipSpritePositionOnFile.Width;// que hace esto?
                }
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                // is the ship at left end?
                if (shipPosX - distanceMoved > 0)
                {
                    shipPosX = shipPosX - distanceMoved;
                }
                else
                {
                    shipPosX = 0;
                }
            }

            //#####################################################################################
            // SHOOTING ###########################################################################
            //#####################################################################################
            if (Keyboard.GetState(). IsKeyDown(Keys.Space))
            {
                shoot();
            }


                base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);

            // TODO: Add your drawing code here

            //New positions in the game frame
            shipPosition.X = (float)this.shipPosX; // Por que el cast a entero? 

            //iniciamos
            spriteBatch.Begin();
            //Vector2 shipStartingPosition  = new Vector2();
            spriteBatch.Draw(sprites, shipPosition,
                shipSpritePositionOnFile,//sourceRectangle is used to draw a portion of the sprite only
                Color.White);
            //destinationRectangle:shipRectangle//,
            //scale: spritesScale

            //terminamos
            spriteBatch.End();
            base.Draw(gameTime);
        }

        static public void shoot ()
        {
            
            Debug.WriteLine("bang");
        }
    }
}
