using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.OpenGL;
using OpenTK.Graphics;

namespace GameSnake
{
    public class Fruit
    {
        Map map = new Map();
        Random rnd = new Random();
        public Coordinates[] coordinatesFruit = new Coordinates[1];
        Coordinates[] coordinatesBloks;
        List<Coordinates> coordinatesSnake;
        public Fruit (Coordinates[] coordinatesBloks, List<Coordinates> coordinatesSnake)
        {
            this.coordinatesBloks = new Coordinates[coordinatesBloks.Length];
            this.coordinatesBloks = coordinatesBloks;
            this.coordinatesSnake = new List<Coordinates>();
            this.coordinatesSnake = coordinatesSnake;
        }
        public void GenerationFruit()
        {
            CheckOnBloksAndSnake();
            coordinatesFruit[0].X2 = Math.Round(coordinatesFruit[0].X1 + map.cellsize, 1);
            coordinatesFruit[0].Y2 = Math.Round(coordinatesFruit[0].Y1, 1);
            coordinatesFruit[0].X3 = Math.Round(coordinatesFruit[0].X1 + map.cellsize, 1);
            coordinatesFruit[0].Y3 = Math.Round(coordinatesFruit[0].Y1 - map.cellsize, 1);
            coordinatesFruit[0].X4 = Math.Round(coordinatesFruit[0].X1, 1);
            coordinatesFruit[0].Y4 = Math.Round(coordinatesFruit[0].Y1 - map.cellsize, 1);
        }

        public void CheckOnBloksAndSnake()
        {
            double x, y;
            do
            {
                do
                {
                    int rows = rnd.Next(1, (int)(map.height / map.cellsize + 1));
                    int columns = rnd.Next(1, (int)(map.widht / map.cellsize + 1));
                    x = Math.Round(-1 + (columns * map.cellsize), 1);
                    y = Math.Round(1 - (rows * map.cellsize), 1);
                }
                while (coordinatesSnake.Any(crd => crd.X1 == x && crd.Y1 == y));              
            }
            while (coordinatesBloks.Any(crd => crd.X1 == x && crd.Y1 == y));
            coordinatesFruit[0].X1 = x;
            coordinatesFruit[0].Y1 = y;
        }

        public void DrawFruit()
        {
            GL.Color4(Color4.Red);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(coordinatesFruit[0].X1, coordinatesFruit[0].Y1);
            GL.Vertex2(coordinatesFruit[0].X2, coordinatesFruit[0].Y2);
            GL.Vertex2(coordinatesFruit[0].X3, coordinatesFruit[0].Y3);
            GL.Vertex2(coordinatesFruit[0].X4, coordinatesFruit[0].Y4);
            GL.End();
        }
    }
}
