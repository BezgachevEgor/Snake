using System;
using OpenTK.Graphics;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace GameSnake
{
    public class Map
    {
        public Coordinates[] coordinatesBloks;
        public double widht = 1.8;
        public double height = 1.8;
        public double cellsize = 0.1;
        public void ReadMap(string level)
        {
            switch (level)
            {
                case "Level1": coordinatesBloks = new Coordinates[0]; break;
                case "Level2": coordinatesBloks = new Coordinates[2]; GenerationBloks(@"Levels\Level2.txt"); break;
                case "Level3": coordinatesBloks = new Coordinates[24]; GenerationBloks(@"Levels\Level3.txt"); break; 
                case "Level4": coordinatesBloks = new Coordinates[42]; GenerationBloks(@"Levels\Level4.txt"); break;
            }
        }
        public void DrawGrid()
        {
            GL.Color4(Color4.White);
            GL.LineWidth(2f);
            GL.Begin(PrimitiveType.Lines);
            for (int i = 1; i <= height / cellsize + 1; i++)
            {
                double a = Math.Round(cellsize * i, 1);
                double b = Math.Round(-1 + a, 1);
                GL.Vertex2(b, 0.9);
                GL.Vertex2(b, -0.9);
            }
            for (int i = 1; i <= widht / cellsize + 1; i++)
            {
                double a = Math.Round(cellsize * i, 1);
                double b = Math.Round(1 - a, 1);
                GL.Vertex2(-0.9, b);
                GL.Vertex2(0.9, b);
            }
            GL.End();           
        }

        public void DrawBorder()
        {
            GL.Color4(Color4.OrangeRed);
            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(-1, 1);
            GL.Vertex2(-1, 0.902);
            GL.Vertex2(1, 0.902);
            GL.Vertex2(1, 1);
            GL.Vertex2(-1, 1);
            GL.Vertex2(-0.902, 1);
            GL.Vertex2(-0.902, -1);
            GL.Vertex2(-1, -1);
            GL.Vertex2(-1, -0.902);
            GL.Vertex2(1, -0.902);
            GL.Vertex2(1, -1);
            GL.Vertex2(-1, -1);
            GL.Vertex2(0.902, -1);
            GL.Vertex2(1, -1);
            GL.Vertex2(1, 1);
            GL.Vertex2(0.902, 1);
            GL.End();
        }

        public void GenerationBloks(string path)
        {
            StreamReader streamReader = new StreamReader(path);
            for (int i = 0; i < coordinatesBloks.Length; i++)
            {
                coordinatesBloks[i].X1 = Convert.ToDouble(streamReader.ReadLine());
                coordinatesBloks[i].Y1 = Convert.ToDouble(streamReader.ReadLine());
                coordinatesBloks[i].X2 = Math.Round(coordinatesBloks[i].X1 + cellsize,1);
                coordinatesBloks[i].Y2 = Math.Round(coordinatesBloks[i].Y1,1);
                coordinatesBloks[i].X3 = Math.Round(coordinatesBloks[i].X1 + cellsize,1);
                coordinatesBloks[i].Y3 = Math.Round(coordinatesBloks[i].Y1 - cellsize,1);
                coordinatesBloks[i].X4 = Math.Round(coordinatesBloks[i].X1,1);
                coordinatesBloks[i].Y4 = Math.Round(coordinatesBloks[i].Y1 - cellsize,1);
            }
            streamReader.Close();
        }

        public void DrawBloks()
        {
            if (coordinatesBloks.Length != 0)
            {
                for (int i = 0; i < coordinatesBloks.Length; i++)
                {
                    GL.Color4(Color4.LightGray);
                    GL.Begin(PrimitiveType.Quads);
                    GL.Vertex2(coordinatesBloks[i].X1, coordinatesBloks[i].Y1);
                    GL.Vertex2(coordinatesBloks[i].X2, coordinatesBloks[i].Y2);
                    GL.Vertex2(coordinatesBloks[i].X3, coordinatesBloks[i].Y3);
                    GL.Vertex2(coordinatesBloks[i].X4, coordinatesBloks[i].Y4);
                    GL.End();
                    GL.Color4(Color4.White);
                    GL.LineWidth(4f);
                    GL.Begin(PrimitiveType.Lines);
                    GL.Vertex2(coordinatesBloks[i].X1, coordinatesBloks[i].Y1);
                    GL.Vertex2(coordinatesBloks[i].X3, coordinatesBloks[i].Y3);
                    GL.Vertex2(coordinatesBloks[i].X2, coordinatesBloks[i].Y2);
                    GL.Vertex2(coordinatesBloks[i].X4, coordinatesBloks[i].Y4);
                    GL.End();
                }
            }
        }
    }
}