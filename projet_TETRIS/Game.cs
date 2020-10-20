using System;
using System.Drawing;
using System.Windows;
using System.Text;
using System.Linq;
using System.Threading;

namespace Tetris
{
    class Game // La classe game permet de gérer la déroulement du jeu (chute des blocs, perte du joueur, effacement d'une ligne complète)
    {
        public int Score = 0;
        public int Lines = 0;


        public void Fall(Shape shp, int[,] gameboard_grid)
        {

            int index_rotation = Utilities.Find_Index(Shape.Rotations, shp.rotation);
            int x_start = shp.y;
            int y_start = shp.x;
            bool collision = false;

            while (x_start < 41 - shp.GenerateShape().GetLength(0) && collision == false)
            {
                do // boucle do while pour que les opérations soient faites en continue tant que le joueur n'appuie pas sur des touches
                {
                    //Console.MoveBufferArea(y_start, x_start, 8, 4, y_start, x_start + 1);
                    shp.Move_shape(y_start, x_start, 1);
                    x_start += 1;
                    collision = shp.Check_collision(y_start, x_start, 1, gameboard_grid);

                    Thread.Sleep(Options.Thread_Level);

                }
                while (Console.KeyAvailable == false && x_start < 41 - shp.GenerateShape().GetLength(0) && collision == false);

                if (x_start < 41 - shp.GenerateShape().GetLength(0) && collision == false)
                {
                    ConsoleKeyInfo csk = Console.ReadKey(true);
                    switch (csk.Key)
                    {

                        case (ConsoleKey.LeftArrow):
                            {

                                if (y_start > 1 && shp.Check_collision(y_start, x_start, 2, gameboard_grid) == false)
                                {
                                    //Console.MoveBufferArea(y_start, x_start, 8, 4, y_start - 2, x_start);
                                    shp.Move_shape(y_start, x_start, 2);
                                    y_start = y_start - 2;
                                }

                                break;
                            }
                        case (ConsoleKey.RightArrow):
                            {
                                if (y_start < 40 - shp.GenerateShape().GetLength(1) * 2 && shp.Check_collision(y_start, x_start, 3, gameboard_grid) == false)
                                {
                                    //Console.MoveBufferArea(y_start, x_start, 8, 4, y_start + 2, x_start);
                                    shp.Move_shape(y_start, x_start, 3);
                                    y_start = y_start + 2;

                                }
                                break;
                            }
                        case (ConsoleKey.DownArrow):
                            {
                                if (x_start < 42 - shp.GenerateShape().GetLength(0) && shp.Check_collision(y_start, x_start, 1, gameboard_grid) == false)
                                {
                                    shp.Move_shape(y_start, x_start, 1);
                                    //Console.MoveBufferArea(y_start, x_start, 8, 4, y_start, x_start + 1);
                                    x_start = x_start + 1;
                                }
                                break;
                            }

                        case (ConsoleKey.UpArrow):
                            {
                                Shape temp_newshape = new Shape(shp.TypeShape, Shape.Rotations[Utilities.Next_Index(Shape.Rotations, Utilities.Find_Index(Shape.Rotations, shp.rotation))]);

                                if (y_start + 2 * temp_newshape.GenerateShape().GetLength(1) < 42)
                                {
                                    if (shp.Check_collision(y_start, x_start, 4, gameboard_grid) == false)
                                    {
                                        shp.Rotate_Shape(y_start, x_start);
                                    }

                                }


                                break;
                            }

                        case (ConsoleKey.Escape):
                            {
                                Interface menu = new Interface();
                                menu.LaunchMenu();
                                break;
                            }


                    }
                }
            }
            //une fois que la "chute" est finie, le gameboad se remplit 
            shp.FillGameboard(y_start, x_start, gameboard_grid);

        }



        public void Run()
        {
            Console.Clear();

            GameBoard game_board = new GameBoard(0, 0, 40, Console.WindowHeight - 5);

            string[] shapes = new string[] { "L", "O", "Z", "T", "I" };
            int[] rotations = new int[] { 90, 180, 270, 0 };
            Random rnd = new Random();
            int[,] gameboard_grid = new int[40, 20];


            bool Lost = false;

            while (Lost == false)
            {

                Shape sp = new Shape(shapes[rnd.Next(5)], rotations[rnd.Next(4)]);
                Lost = this.Game_Lost(gameboard_grid);

                if (Lost == false)
                {
                    sp.Draw(game_board.width / 2 - 1, 1);
                    this.Fall(sp, gameboard_grid);
                }

                int indiceline;

                while (Full_Line(gameboard_grid) != -1)
                {
                    indiceline = Full_Line(gameboard_grid);
                    Delete_Line(indiceline, gameboard_grid);
                    Score += 500 * Options.level;
                    Lines += 1;
                    game_board.Update_scores(Score, Lines);
                }

                Score += 50 * Options.level;
                game_board.Update_scores(Score, Lines);

            }

            Console.SetCursorPosition(55, 16);
            Console.WriteLine("P E R D U");
            Console.SetCursorPosition(52, 17);
            Console.WriteLine("Appuyez sur Entrée");

            ConsoleKeyInfo csk = Console.ReadKey(true);
            switch (csk.Key)
            {

                case (ConsoleKey.Enter):
                    {

                        Interface menu = new Interface();
                        menu.LaunchMenu();

                        break;
                    }
            }

        }

        public void Delete_Line(int line, int[,] gameboard_grid) // Supprimer la ligne du table gameboard et déplacer le grahpique de la console pour effacer la ligne complete
        {

            Console.MoveBufferArea(1, 1, 40, line, 1, 2);

            for (int i = line; i > 0; i--)
            {
                for (int j = 0; j < gameboard_grid.GetLength(1); j++)
                {
                    gameboard_grid[i, j] = gameboard_grid[i - 1, j];

                }
            }

            for (int j = 0; j < gameboard_grid.GetLength(1); j++)
            {
                gameboard_grid[0, j] = 0;

            }


        }




        public bool Game_Lost(int[,] gameboard_grid) // Vérifie si il y a une case remplie dans la premiere ligne du tableau pour renvoyer True si c'est perdu
        {
            bool lost = false;


            for (int i = 0; i < gameboard_grid.GetLength(1); i++)
            {
                if (gameboard_grid[1, i] == 1)
                {
                    lost = true;
                }
            }

            return lost;


        }


        public int Full_Line(int[,] gameboard_grid) // Vérifie toutes les lignes du GameBoard et renvoie l'indice de la première ligne complète
        {
            int sum = 0;

            for (int i = 0; i < gameboard_grid.GetLength(0); i++)
            {
                sum = 0;
                for (int j = 0; j < gameboard_grid.GetLength(1); j++)
                {
                    sum += gameboard_grid[i, j];
                }

                if (sum == gameboard_grid.GetLength(1))
                {
                    return i;
                }

            }

            return -1;
        }






    }
}
