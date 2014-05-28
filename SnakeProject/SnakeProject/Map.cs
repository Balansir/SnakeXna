using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SnakeProject
{
    public class Map:GameComponent
    {
        private int startX = 0;
        private int startY = 0;
	    
		
	    private AppleBlock _apple;
	    private Snake _snake;
	    private Mongoose _mongoose;

	    private int[,] _mask;

	    private string _path;
	    private bool _withMangoose;

		public int SizeX;
		public int SizeY;

	    public int[,] Mask { get { return _mask; } }

        public Vector2 Location
        {
            get
            {
                return new Vector2(startX, startY);
            }
            set
            {
                startX = (int) value.X;
                startY = (int) value.Y;
            }
        }

	    public Snake Snake { get { return _snake; }}

        public Map(Game game, bool withMangoose = false)
            : base(game)
        {
	        _path = "map.txt";
	        _withMangoose = withMangoose;
	        _snake = new Snake(Game, this);
        }

        public override void Initialize()
        {
            base.Initialize();

            // создание объектов карты
            // загрузка карты, первая строка - число с количеством блоков в строке
	        var sr = new StreamReader(_path);
	        SizeX = Int32.Parse(sr.ReadLine());
	        var mask = sr.ReadToEnd().Replace("\r\n", string.Empty);

			_mask = new int[mask.Length / SizeX, SizeX];

	        int dx = 0;
	        int dy = 0;

            foreach (char item in mask)
            {
				if (dx == SizeX)
	            {
		            dx = 0;
		            dy++;
	            }

	            if (item != '0')
	            {
		            var wall = new Wall(Game) {Size = new Vector2(Helper.SizeBlock, Helper.SizeBlock), Location = new Vector2(startX + dx*Helper.SizeBlock, startY + dy*Helper.SizeBlock)};
		            Game.Components.Add(wall);
		            _mask[dy, dx] = Helper.MaskWallBlock;
	            }
	            else
		            _mask[dy, dx] = Helper.MaskEmptyBlock;

	            dx++;
            }
	        SizeY = dy;

            // insert snake
	        int startIndexX = 6;
	        int startIndexY = 2;
	        _snake.Location = new Vector2(startX + startIndexX*Helper.SizeBlock, startY + startIndexY*Helper.SizeBlock);
	        _snake.Vector = new Vector2(1, 0);
	        Game.Components.Add(_snake);

			// добавляем яблоко
	        _apple = new AppleBlock(Game);
			Game.Components.Add(_apple);
			AppleAdd();

			if (_withMangoose)
				MongooseAdd();
        }

		public TypeMessage UpdateMaskMap(Vector2 location, Vector2 vector, Masks type)
		{
			int i = (int) (location.Y - startY)/Helper.SizeBlock;
			int j = (int) (location.X - startX)/Helper.SizeBlock;
			int iNext = i + (int) vector.Y;
			int jNext = j + (int) vector.X;
			
			// если вышли за границы
			if (i < 0 || j < 0 || jNext < 0 || iNext < 0 || i >= SizeY || j >= SizeX || iNext >= SizeY || jNext >= SizeX)
				return TypeMessage.Dead;

			switch (type)
			{
				case Masks.Snake:
					if (_mask[iNext, jNext] == (int) Masks.Wall)
					{
						return TypeMessage.WallBreak;
					}
					if (_mask[iNext, jNext] == (int) Masks.Apple)
					{
						_mask[iNext, jNext] = (int) Masks.Snake;
						_mask[i, j] = (int) Masks.Empty;
						// обработать съедание яблока
						return TypeMessage.Add;
					}
					_mask[iNext, jNext] = (int) Masks.Snake;
					_mask[i, j] = (int) Masks.Empty;
					break;
				case Masks.Sk:
					if (_mask[iNext, jNext] == (int)Masks.Snake)
						return TypeMessage.Dead;
					_mask[i, j] = (int) Masks.Empty;
					_mask[iNext, jNext] = (int) Masks.Sk;
					_mongoose.I = iNext;
					_mongoose.J = jNext;

					break;
			}
			
			return TypeMessage.None;
	    }

	    public void AppleAdd()
	    {
		    var rnd = new Random();

			// выбираем свободное пространство
		    var freeSpace = new List<int>();
		    int index = 0;
		    foreach (var item in _mask)
		    {
			    if (item == (int) Masks.Empty)
				    freeSpace.Add(index);
			    index++;
		    }
		    var rndIndex = rnd.Next(freeSpace.Count / 4);
		    var i = freeSpace[rndIndex]/SizeX;
		    var j = freeSpace[rndIndex]%SizeX;

		    _mask[i, j] = (int) Masks.Apple;
		    _apple.I = i;
		    _apple.J = j;
		    _apple.Location = GetLocationByMaskIndex(i, j);
		    _apple.Visible = true;
	    }

	    public void AppleRemove()
	    {
			_mask[_apple.I, _apple.J] = (int)Masks.Empty;
		    _apple.Visible = false;
	    }

	    private void MongooseAdd()
	    {
			_mongoose = new Mongoose(Game, this);
			_mongoose.I = SizeY - 2;
			_mongoose.J = SizeX - 2;
		    _mask[_mongoose.I, _mongoose.J] = (int) Masks.Sk;
		    _mongoose.Location = new Vector2(startX + _mongoose.J*Helper.SizeBlock, startY + _mongoose.I*Helper.SizeBlock);
			

			Game.Components.Add(_mongoose);
	    }

	    private Vector2 GetLocationByMaskIndex(int i, int j)
	    {
		    return new Vector2(startX + j*Helper.SizeBlock, startY + i*Helper.SizeBlock);
	    }
    }
}
