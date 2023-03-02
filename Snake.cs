using System;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace GameSnake
{
    public class Snake
    {
        public double dirX, dirY;
        public List<Coordinates> coordinatesSnake = new List<Coordinates>();
        public Snake(double X1, double Y1, double X2, double Y2, double X3, double Y3, double X4, double Y4)
        {
            coordinatesSnake.Add(new Coordinates() { X1 = X1, Y1 = Y1, X2 = X2, Y2 = Y2, X3 = X3, Y3 = Y3, X4 = X4, Y4 = Y4 });
        }
        public void SetDirection(Directions dir)
        {
            dirX = 0;
            dirY = 0;
            switch (dir)
            {
                case Directions.Right: dirX = 0.1; break;
                case Directions.Left: dirX = -0.1; break;
                case Directions.Up: dirY = 0.1; break;
                case Directions.Down: dirY = -0.1; break;
            }
        }

        public void MoveSnake(int count)
        {
            for (int i = count; i >= 1; i--)
            {
                coordinatesSnake[i] = new Coordinates() { X1 = coordinatesSnake[i - 1].X1, Y1 = coordinatesSnake[i - 1].Y1,
                                                          X2 = coordinatesSnake[i - 1].X2, Y2 = coordinatesSnake[i - 1].Y2,
                                                          X3 = coordinatesSnake[i - 1].X3, Y3 = coordinatesSnake[i - 1].Y3, 
                                                          X4 = coordinatesSnake[i - 1].X4, Y4 = coordinatesSnake[i - 1].Y4 };
            }
            Coordinates crd = coordinatesSnake[0];
            crd.X1 = Math.Round(crd.X1 + dirX,1);
            crd.X2 = Math.Round(crd.X2 + dirX,1);
            crd.X3 = Math.Round(crd.X3 + dirX,1);
            crd.X4 = Math.Round(crd.X4 + dirX,1);
            crd.Y1 = Math.Round(crd.Y1 + dirY,1);
            crd.Y2 = Math.Round(crd.Y2 + dirY,1);
            crd.Y3 = Math.Round(crd.Y3 + dirY,1);
            crd.Y4 = Math.Round(crd.Y4 + dirY,1);
            coordinatesSnake[0] = crd;
        }

        public void DrawSnake()
        {
            for (int i = 0; i < coordinatesSnake.Count; i++)
            {
                if (i == 0)
                {
                    GL.Color4(Color4.Coral);
                    PrintCoordinatesSnake(i);
                }
                else
                {
                    GL.Color4(Color4.Green);
                    PrintCoordinatesSnake(i);
                }
            }
        }
        
        private void PrintCoordinatesSnake(int i)
        {
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(coordinatesSnake[i].X1, coordinatesSnake[i].Y1);
            GL.Vertex2(coordinatesSnake[i].X2, coordinatesSnake[i].Y2);
            GL.Vertex2(coordinatesSnake[i].X3, coordinatesSnake[i].Y3);
            GL.Vertex2(coordinatesSnake[i].X4, coordinatesSnake[i].Y4);
            GL.End();
        }
    }

    public enum Directions
    {
        Right,
        Left,
        Up,
        Down
    }
}
