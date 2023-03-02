using System;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace GameSnake
{
    class Game : GameWindow
    {
        private string level;
        private string lastDirection;
        private int count;
        Map map;
        Snake snake;
        Fruit fruit;
        public Game(string level) : base(650, 650)
        {
            WindowState = WindowState.Normal;
            Icon = Icon.ExtractAssociatedIcon(@"Resources\IconSnake.ico");
            VSync = VSyncMode.On;
            this.level = level;
        }
              
        protected override void OnLoad(EventArgs e)
        {
            map = new Map();
            map.ReadMap(level);
            snake = new Snake(-0.9, 0.9, -0.8, 0.9, -0.8, 0.8, -0.9, 0.8);
            fruit = new Fruit(map.coordinatesBloks, snake.coordinatesSnake);
            fruit.GenerationFruit();
        }
        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
        }
        protected override void OnKeyDown(KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Right && Key.Left.ToString() != lastDirection)
            {
                lastDirection = e.Key.ToString();
                snake.SetDirection(Directions.Right);
            }
            else if (e.Key == Key.Left && Key.Right.ToString() != lastDirection)
            {
                lastDirection = e.Key.ToString();
                snake.SetDirection(Directions.Left);
            }
            else if (e.Key == Key.Up && Key.Down.ToString() != lastDirection)
            {
                lastDirection = e.Key.ToString();
                snake.SetDirection(Directions.Up);
            }
            else if (e.Key == Key.Down && Key.Up.ToString() != lastDirection)
            {
                lastDirection = e.Key.ToString();
                snake.SetDirection(Directions.Down);
            }
            else if (e.Key == Key.Space)
            {
                pause = !pause;
            }
        }        
        bool pause = true;
        double lag;
        double timePerFrame = 0.3;
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            if (pause)
            {
                lag += e.Time;
                if (lag > timePerFrame)
                {
                    snake.MoveSnake(count);
                    CheckOutMap();
                    CheckOnBloks();
                    CheckSnakeBit();
                    CheckEatFruit();
                    IncreaseSpeed();
                    while (lag > timePerFrame)
                        lag -= timePerFrame;
                }
            }
            Title = $"Счет - {count}     Пауза - пробел";
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            map.DrawGrid();
            map.DrawBorder();
            map.DrawBloks();
            snake.DrawSnake();
            fruit.DrawFruit();
            SwapBuffers();
        }        

        private void CheckSnakeBit()
        {
            for (int i = 1; i < count; i++)
            {
                if (snake.coordinatesSnake[0].X1 == snake.coordinatesSnake[i].X1 && snake.coordinatesSnake[0].Y1 == snake.coordinatesSnake[i].Y1)
                {
                    snake.coordinatesSnake.RemoveRange(i, count - i + 1);
                    count -= count - i + 1;
                }
            }
        }

        private void CheckOnBloks()
        {
            for (int i = 0; i < map.coordinatesBloks.Length; i++)
            {
                if (map.coordinatesBloks[i].X1 == snake.coordinatesSnake[0].X1 && map.coordinatesBloks[i].Y1 == snake.coordinatesSnake[0].Y1)
                {
                    AskQuestion();
                }
            }
        }

        private void CheckOutMap()
        {
            if (snake.coordinatesSnake[0].X1 < -0.9 || snake.coordinatesSnake[0].Y1 > 0.9 
                || snake.coordinatesSnake[0].X3 > 0.9 || snake.coordinatesSnake[0].Y3 < -0.9)
            {
                AskQuestion();
            }
        }

        private void CheckEatFruit()
        {
            if (snake.coordinatesSnake[0].X1 == fruit.coordinatesFruit[0].X1 && snake.coordinatesSnake[0].Y1 == fruit.coordinatesFruit[0].Y1)
            {
                Title = $"Счет - {++count}     Пауза - пробел";
                snake.coordinatesSnake.Add(new Coordinates()
                {
                    X1 = snake.coordinatesSnake[count - 1].X2,
                    Y1 = snake.coordinatesSnake[count - 1].Y2,
                    X2 = Math.Round(snake.coordinatesSnake[count - 1].X2 + snake.dirX, 1),
                    Y2 = Math.Round(snake.coordinatesSnake[count - 1].Y2, 1),
                    X3 = Math.Round(snake.coordinatesSnake[count - 1].X2 + snake.dirX, 1),
                    Y3 = Math.Round(snake.coordinatesSnake[count - 1].Y2 - snake.dirY, 1),
                    X4 = Math.Round(snake.coordinatesSnake[count - 1].X2, 1),
                    Y4 = Math.Round(snake.coordinatesSnake[count - 1].Y2 - snake.dirY, 1)
                });
                fruit.GenerationFruit();
            }
        }

        private void IncreaseSpeed()
        {
            if (count >= 40)
            {
                timePerFrame = 0.2;
            }
            else if (count >= 20)
            {
                timePerFrame = 0.25;
            }
            else
            {
                timePerFrame = 0.3;
            }
        }

        private void AskQuestion()
        {
            new PlayForm(count);
            if (MessageBox.Show($"Вы прогиграли!\r\nВаш результат - {count} очков. Начать заново?", "Змейка", MessageBoxButtons.YesNo, MessageBoxIcon.Question) 
                == DialogResult.No)
            {
                Close();
            }
            else
            {
                Clear();
            }
        }
        private void Clear()
        {
            pause = false;
            snake.coordinatesSnake.Clear();
            snake = new Snake(-0.9, 0.9, -0.8, 0.9, -0.8, 0.8, -0.9, 0.8);
            lastDirection = null;
            count = 0;
            fruit.GenerationFruit();
            pause = true;
        }
    }
}