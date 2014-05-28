using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnakeProject
{
    public class SnakeBlock:Block
    {
        private int currentPath;
        private int _defaultPath;
        private Vector2 _destinationPoint;
        protected Vector2 _currentVector;
        private Vector2? _nextVector;

        public Vector2 Vector
        {
            get
            {
                return _currentVector;
            }
            set
            {
                _nextVector = value;
            }
        }

        public Vector2 GoToPoint
        {
            get
            {
                return _destinationPoint;
            }

            set
            {
                _destinationPoint = value;
                Vector = Vector2.Normalize(_destinationPoint - Location);
            }
        }

	    public event Action<Vector2, Vector2> CurrentPathChanged;


        public SnakeBlock(Game game)
            : base(game, game.Content.Load<Texture2D>("SnakeBlock"))
        {
            Size = new Vector2(Helper.SizeBlock, Helper.SizeBlock);
            _defaultPath = Helper.SizeBlock;
        }

        public override void Update(GameTime gameTime)
        {
            // перемещаем блок
            if (currentPath == 0) // если дошли до точки возможной смены направления
            {
				currentPath = _defaultPath;	
                if (_nextVector != null)
                    _currentVector = _nextVector.Value;
                else
                    GoToPoint = Location + _currentVector*currentPath;

                _nextVector = null;

				// когда дошли до места где можно сменить направление, генерируем событие (нужно для карты)
				if (CurrentPathChanged != null)
					CurrentPathChanged(Location, Vector);
            }
            
            // перемешение блока
            Location += Vector;
            currentPath--;

            base.Update(gameTime);
        }
    }
}
