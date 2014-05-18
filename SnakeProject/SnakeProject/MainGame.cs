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

namespace SnakeProject
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        static SpriteBatch Sprite;

	    private int _countEatenApples;

	    private SpriteFont _font;

		private Vector2 _locationCountEatenApples;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1024;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;

            Content.RootDirectory = "Content";

	        _countEatenApples = 0;
        }

        protected override void Initialize()
        {
            var map = new Map(this) {Location = new Vector2(10, 10)};
			map.Snake.CountBlockChanged += SnakeOnCountBlockChanged;
            Components.Add(map);

	        var grid = new Grid(this, new Rectangle(graphics.PreferredBackBufferWidth - 150, 10, 140, graphics.PreferredBackBufferHeight - 30), new Vector2(140, graphics.PreferredBackBufferHeight - 30));
	        grid.Color = Color.BlueViolet;
	        Components.Add(grid);

	        var statisticAppleBlock = new AppleBlock(this)
			{
				Location = new Vector2(graphics.PreferredBackBufferWidth - 140, 60),
				Size = new Vector2(30, 30)
			};
	        Components.Add(statisticAppleBlock);

			_locationCountEatenApples = new Vector2(graphics.PreferredBackBufferWidth - 100, 50);

            base.Initialize();
        }

	    private void SnakeOnCountBlockChanged(int countBlock)
	    {
		    _countEatenApples++;
	    }

	    protected override void LoadContent()
        {
            Sprite = new SpriteBatch(GraphicsDevice);
		    _font = Content.Load<SpriteFont>("font");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

	        Sprite.Begin();


	        Sprite.DrawString(_font, string.Format("{0}", _countEatenApples), _locationCountEatenApples, Color.OrangeRed);
			Sprite.End();

            base.Draw(gameTime);
        }

	    protected override void OnExiting(object sender, EventArgs args)
	    {
		    base.OnExiting(sender, args);
	    }
    }
}
