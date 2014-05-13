using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnakeProject
{
	public class AppleBlock:Block
	{
		public int I;
		public int J;

		public AppleBlock(Game game)
			: base(game, game.Content.Load<Texture2D>("AppleBlock"))
		{
			Size = new Vector2(Helper.SizeBlock, Helper.SizeBlock);
		}
	}
}
