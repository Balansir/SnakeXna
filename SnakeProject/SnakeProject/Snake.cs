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

        public Snake(Game game, int countBlock = 10)
            : base(game)
        {
            _body = new List<SnakeBlock>();
            _length = countBlock;
            Location = new Vector2(0, 0);
        }

        public override void Initialize()
        {
            base.Initialize();

            // create by head
            var head = new SnakeBlock(Game) {Location = Location, Vector = Vector}; // для головы другую текстурку нужно
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
                //_body[i].Vector = _body[i - 1].Vector;
                _body[i].GoToPoint = _body[i - 1].Location;
        }
    }
}
