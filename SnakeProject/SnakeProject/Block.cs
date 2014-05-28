using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnakeProject
{
    public class Block:DrawableGameComponent
    {
        private Color _color;
        private Rectangle _bounds;
        protected Texture2D _texture;
        private SpriteBatch _spBatch;


        public Vector2 Size
        {
            get { return new Vector2(_bounds.Width, _bounds.Height); }
            set
            {
                _bounds.Width = (int)value.X;
                _bounds.Height = (int)value.Y;
            }
        }

        public Vector2 Location
        {
            get
            {
                return new Vector2(_bounds.X, _bounds.Y);
            }

            set
            {
                _bounds.X = (int)value.X;
                _bounds.Y = (int)value.Y;
            }
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public Block(Game game, Texture2D texture)
            : base(game)
        {
            if (texture == null)
                throw new ArgumentNullException("texture");

            _texture = texture;
            _spBatch = new SpriteBatch(game.GraphicsDevice);

            _color = new Color(255, 255, 0);
        }

        public override void Draw(GameTime gameTime)
        {
            _spBatch.Begin();
	        _spBatch.Draw(_texture, _bounds, null, _color);
            _spBatch.End();
        }
    }
}
