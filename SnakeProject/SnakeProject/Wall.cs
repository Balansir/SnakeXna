using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SnakeProject
{
    public class Wall:Block
    {
        public Wall(Game game)
            : base(game, game.Content.Load<Texture2D>("Wall"))
        {
        }
    }
}
