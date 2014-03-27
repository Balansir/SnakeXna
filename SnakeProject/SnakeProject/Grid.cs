using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnakeProject
{
    public class Grid:DrawableGameComponent
    {
        private SpriteBatch spBath;
        private Texture2D bodyTexure;
        private int countVertical;
        private int countHorisontal;
        private Vector2 _sizeBlock;
        private Color _color;
        private Rectangle _bounds;

        public Vector2 SizeBlock
        {
            get { return _sizeBlock; }
            set
            {
                _sizeBlock = value;
                SizeChanged();
            }
        }

        public Vector2 Size
        {
            get { return new Vector2(_bounds.Width, _bounds.Height); }
            set
            {
                _bounds.Width = (int)value.X;
                _bounds.Height = (int) value.Y;
                SizeChanged();
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
                _bounds.X = (int) value.X;
                _bounds.Y = (int) value.Y;
            }
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public Grid(Game game)
            : base(game)
        {
            spBath = new SpriteBatch(game.GraphicsDevice);

            bodyTexure = new Texture2D(game.GraphicsDevice, 1, 1);
            bodyTexure.SetData<Color>(new Color[] {Color.White});

            _bounds = new Rectangle(0, 0, 100, 100);
            _sizeBlock = new Vector2(30, 30);
            Color = Color.Red;
        }

        public Grid(Game game, Rectangle rect, Vector2 sizeBlock)
            :this(game)
        {
            _bounds = rect;
            _sizeBlock = sizeBlock;
        }

        private void SizeChanged()
        {
            countHorisontal = _bounds.Height/(int) SizeBlock.Y;
            countVertical = _bounds.Width/(int) SizeBlock.X;
        }

        public override void Initialize()
        {
            SizeChanged();
            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            spBath.Begin();
            int startX = _bounds.X;
            int startY = _bounds.Y;

            for (int i = 0; i < countVertical; i++)
            {
                DrawLine(startX, _bounds.Y, startX, _bounds.Bottom, spBath);
                startX += (int) _sizeBlock.X + 1;
            }
            
            DrawLine(startX - 1, _bounds.Y, startX - 1, _bounds.Bottom, spBath);
            
            for (int i = 0; i < countHorisontal; i++)
            {
                DrawLine(_bounds.X, startY, _bounds.Right, startY, spBath);
                startY += (int) _sizeBlock.Y + 1;
            }
            DrawLine(_bounds.X, startY - 1, _bounds.Right, startY - 1, spBath);



            spBath.End();
            
            base.Draw(gameTime);
        }

        private void DrawLine(int x1, int y1, int x2, int y2, SpriteBatch spB)
        {
            int deltaX = Math.Abs(x2 - x1);
            int deltaY = Math.Abs(y2 - y1);
            int singX = x1 < x2 ? 1 : -1;
            int singY = y1 < y2 ? 1 : -1;

            int error = deltaX - deltaY;

            spB.Draw(bodyTexure, new Vector2(x2, y2), Color);
            while (x1 != x2 || y1 != y2)
            {
                spB.Draw(bodyTexure, new Vector2(x1, y1), Color);

                int error2 = error*2;

                if (error2 > -deltaY)
                {
                    error -= deltaY;
                    x1 += singX;
                }
                if (error2 < deltaX)
                {
                    error += deltaX;
                    y1 += singY;
                }
            }

        }
    }
}
