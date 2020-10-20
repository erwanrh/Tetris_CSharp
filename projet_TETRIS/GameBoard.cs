using System;
using System.Drawing;
using System.Windows;
using System.Text;
using System.Linq;
using System.Threading;

namespace Tetris
{
    class GameBoard // La classe Gameboard dessine le cardre de jeu et permet de gérer le score, le nombre de ligne effacé et d'afficher niveau


    {

        public int left;

        public int top;

        public int width;

        public int height;

        public GameBoard(int left, int top, int width, int height)

        {

            this.top = top;

            this.left = left;

            this.width = width;

            this.height = height;


            this.Draw_gameboard();


        }


        public void Update_scores(int score, int line)
        {
            Console.SetCursorPosition(45, 7);
            Console.WriteLine("Score : {0}", score);
            Console.SetCursorPosition(45, 10);
            Console.WriteLine("Lines : {0}", line);

        }


        public void Draw_gameboard()

        {
            Console.SetCursorPosition(55, 3);
            Console.WriteLine("T E T R I S");
            Console.SetCursorPosition(45, 7);
            Console.WriteLine("Score : {0}", 0);
            Console.SetCursorPosition(45, 10);
            Console.WriteLine("Lines : {0}", 0);
            Console.SetCursorPosition(45, 13);
            Console.WriteLine("Level : {0}", Options.level);
            for (int i = this.left; i < this.width + 2; i++)

            {

                for (int j = this.top; j < this.height + 2; j++)

                {

                    if (j == 0 || j == this.height + 1)

                    {

                        Console.CursorLeft = i;

                        Console.CursorTop = j;

                        Console.WriteLine("─");

                    }

                    if (i == 0 && j > 0 || i == this.width + 1 && j > 0)

                    {

                        Console.CursorLeft = i;

                        Console.CursorTop = j;

                        Console.WriteLine("│");

                    }

                    if (i == 0 && j == 0)
                    {
                        Console.CursorLeft = i;

                        Console.CursorTop = j;

                        Console.WriteLine("┌");
                    }

                    if (i == this.width + 1 && j == 0)
                    {
                        Console.CursorLeft = i;

                        Console.CursorTop = j;

                        Console.WriteLine("┐");

                    }

                    if (i == this.width + 1 && j == this.height + 1)
                    {
                        Console.CursorLeft = i;

                        Console.CursorTop = j;

                        Console.WriteLine("┘");

                    }

                    if (i == 0 && j == this.height + 1)
                    {
                        Console.CursorLeft = i;

                        Console.CursorTop = j;

                        Console.WriteLine("└");

                    }

                }

            }

        }

    }
}
