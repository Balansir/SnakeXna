using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SnakeProject
{
    public class Snake:DrawableGameComponent
    {
        private int _length;
        private List<SnakeBlock> _body;
        private Vector2 _vector;
	    private Map _map;

        public Vector2 Vector
        {
            get
            {
                return _vector;
            }
            set
            {
                _vector = value;
                if (_body.Any())
                    _body[0].Vector = _vector; // при изменении направления движения змейки, меняем вектор движения головы
            }
        }

        public Vector2 Location { get; set; }

	    public event Action<int> CountBlockChanged;

        public Snake(Game game, Map map, int countBlock = 3)
            : base(game)
        {
            _body = new List<SnakeBlock>();
            _length = countBlock;
	        _map = map;
            Location = new Vector2(0, 0);
        }

        public override void Initialize()
        {
            base.Initialize();

            // create head
            var head = new SnakeBlock(Game) {Location = Location, Vector = Vector}; // для головы другую текстурку нужно
			head.CurrentPathChanged += SnakeBlockCurrentPathChanged;
            _body.Add(head);
            for (int i = 1; i < _length; i++)
            {
                var last = _body[i - 1];
                var next = new SnakeBlock(Game) {Location = last.Location - new Vector2(last.Size.X, 0), GoToPoint = last.Location};
                _body.Add(next);
            }

            _body.ForEach(block => Game.Components.Add(block));
            UpdateSnakeBlock();
        }

	    private void SnakeBlockCurrentPathChanged(Vector2 location, Vector2 vector)
	    {
		    var result = _map.UpdateMaskMap(location, vector, Masks.Snake);
		    if (result == TypeMessage.Add)
		    {
				// если съели яблоко, увеличиваем змею
				// добавляем в начало голову
			    var newLocation = new Vector2(location.X + Helper.SizeBlock*vector.X, location.Y + Helper.SizeBlock*vector.Y);
			    var head = new SnakeBlock(Game) {Location = newLocation, Vector = vector};
			    _body[0].CurrentPathChanged -= SnakeBlockCurrentPathChanged;
			    head.CurrentPathChanged += SnakeBlockCurrentPathChanged;
			    _body.Insert(0, head);
			    Game.Components.Add(head);
				UpdateSnakeBlock();
				
				_map.AppleRemove();
				_map.AppleAdd();
			    if (CountBlockChanged != null)
				    CountBlockChanged(_body.Count);
		    }

	    }

	    public override void Update(GameTime gameTime)
        {
            var stateKeyBoard = Keyboard.GetState();
            if (stateKeyBoard.IsKeyDown(Keys.W))
                Vector = new Vector2(0, -1);
            if (stateKeyBoard.IsKeyDown(Keys.S))
                Vector = new Vector2(0, 1);
            if (stateKeyBoard.IsKeyDown(Keys.A))
                Vector = new Vector2(-1, 0);
            if (stateKeyBoard.IsKeyDown(Keys.D))
                Vector = new Vector2(1, 0);

            UpdateSnakeBlock();

            base.Update(gameTime);
        }

        private void UpdateSnakeBlock()
        {
            if (!_body.Any())
                return;

            for (int i = 1; i < _body.Count; i++)
                _body[i].GoToPoint = _body[i - 1].Location;
        }
    }
}
