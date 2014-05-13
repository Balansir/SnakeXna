using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SnakeProject
{
	public static class Helper
	{
		public static int SizeBlock = 15;

		public static int MaskWallBlock = 1;
		public static int MaskEmptyBlock = 0;

		
	}

	public enum Masks
	{
		Empty = 0,
		Wall = 1,
		Snake = 2,
		Sk = 3,
		Apple = 4
	}

	public enum TypeMessage
	{
		Dead,
		Add,
		WallBreak
	}
}
