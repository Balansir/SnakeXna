using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SnakeProject
{
    public class Map:DrawableGameComponent
    {
        private int startX = 0;
        private int startY = 0;

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

        public Map(Game game)
            : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            // создание объектов карты
            // загрузка карты - пока без загрузки
            var mask = new string[]
            {
                "000000000000000",
                "000000000000000",
                "000011100111000",
                "000010000001000",
                "000010000001000",
                "000010000001000",
                "000010000001000",
                "000000000001000",
                "000011111101000",
                "000000000000000",
                "000000000000000",
            };

            for (int i = 0; i < mask.Length; i++)
            {
                for (int j = 0; j < mask[i].Length; j++)
                {
                    if (mask[i][j] == '0')
                        continue;
                    
                    var wall = new Wall(Game) {Size = new Vector2(31, 31), Location = new Vector2(startX + j*31, startY + i*31)};
                    Game.Components.Add(wall);
                }
            }

            // insert snake
            Game.Components.Add(new Snake(Game) {Location = new Vector2(0, 0), Vector = new Vector2(1, 0)});
        }
    }
}
