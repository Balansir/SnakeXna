using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SnakeProject
{
	public class Mongoose:SnakeBlock
	{
		private double _speed;
		private TimeSpan _lastUpdate;
		private long tiks;
		private Map _map;

		public int I;
		public int J;

		public Mongoose(Game game, Map map)
			: base(game)
		{
			_texture = game.Content.Load<Texture2D>("Mangoose");
			_speed = Helper.SpeedMangoose;
			_map = map;
			tiks = (long) Math.Truncate(Game.TargetElapsedTime.Ticks/_speed);
			CurrentPathChanged += OnCurrentPathChanged;
		}

		public override void Update(GameTime gameTime)
		{
			if (_lastUpdate > gameTime.TotalGameTime)
				return;

			_lastUpdate += new TimeSpan(tiks);
			base.Update(gameTime);
		}

		private void OnCurrentPathChanged(Vector2 location, Vector2 vector)
		{
			var result = _map.UpdateMaskMap(location, vector, Masks.Sk);
			if (result == TypeMessage.Dead)
			{
				MessageBox.Show("Вас сожрал мангуст", "Конец игры", MessageBoxButtons.OK, MessageBoxIcon.Information);
				Game.Exit();
			
			}
			_currentVector = FindVector();
		}

		private Vector2 FindVector()
		{
			int[,] fld = new int[_map.SizeY, _map.SizeX];
			for(int i=0;i<_map.SizeY;i++)
				for (int j = 0; j < _map.SizeX; j++)
					fld[i, j] = _map.Mask[i, j];

			bool flag = false;
			bool changed = true;
			int k = (int) Masks.Snake;
			while (!flag && changed)
			{
				changed = false;
				for (int i = 0; (i < _map.SizeY - 1) && (!flag); i++)
				{
					for (int j = 0; (j < _map.SizeX - 1) && (!flag); j++)
					{
						if (fld[i, j] == k)
						{
							if (fld[i, j + 1] == 0)
							{
								fld[i, j + 1] = k - 3;
								changed = true;
							}
							if (fld[i, j - 1] == 0)
							{
								fld[i, j - 1] = k - 3;
								changed = true;
							}
							if (fld[i + 1, j] == 0)
							{
								fld[i + 1, j] = k - 3;
								changed = true;
							}
							if (fld[i - 1, j] == 0)
							{
								fld[i - 1, j] = k - 3;
								changed = true;
							}
						}
						else if (fld[i, j] == (int) Masks.Sk)
						{
							if ((fld[i, j + 1] == k - 3) || (fld[i, j - 1] == k - 3) ||
							    (fld[i + 1, j] == k - 3) || (fld[i - 1, j] == k - 3))
								flag = true;
						}
					}
				}
				k -= 3;

			}
			if (fld[I, J + 1] == k)
				return new Vector2(1, 0);
			if (fld[I, J - 1] == k)
				return new Vector2(-1, 0);
			if (fld[I + 1, J] == k)
				return new Vector2(0, 1);
			if (fld[I - 1, J] == k)
				return new Vector2(0, -1);
			
			return new Vector2(0, 0);
		}
	}
}
