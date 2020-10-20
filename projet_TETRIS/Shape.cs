using System;
using System.Drawing;
using System.Windows;
using System.Text;
using System.Linq;
using System.Threading;

namespace Tetris
{
    public class Shape // La classe Shape permet de dessiner les formes des blocs de Tetris selon leurs angles et de gérer leurs rotations.
    {
        public readonly string TypeShape;
        public int rotation;
        public string carre = "██";
        public int x;
        public int y;
        public int Index_Color;
        public static int[] Rotations = new int[] { 90, 180, 270, 0 };


        public Shape(string TypeShape, int rotation) // Constructeur de SHAPE qui prend en arguments le type de forme et la rotation
        {

            this.TypeShape = TypeShape;
            this.rotation = rotation;
            Random rnd = new Random();
            this.Index_Color = rnd.Next(5) + 1;
        }

        public Shape(string TypeShape) // Constructeur de SHAPE qui prend en arguments le type de forme et attribue la valeur 
        {
            this.TypeShape = TypeShape;
            this.rotation = 0;
            Random rnd = new Random();
            this.Index_Color = rnd.Next(5) + 1;
        }

        public int[,] GenerateShape()
        {
            int[,] TableShape = new int[4, 4];

            switch (this.TypeShape)
            {
                case "I":
                    switch (this.rotation)
                    {
                        case 0:
                        case 180:
                            TableShape = new int[,] { { 1 }, { 1 }, { 1 }, { 1 } };
                            break;
                        case 90:
                        case 270:
                            TableShape = new int[,] { { 1, 1, 1, 1 } };
                            break;
                    }

                    break;

                case "O":
                    TableShape = new int[,] { { 1, 1 }, { 1, 1 } };
                    break;

                case "L":
                    switch (this.rotation)
                    {
                        case 0:
                            TableShape = new int[,] { { 1, 0 }, { 1, 0 }, { 1, 0 }, { 1, 1 } };
                            break;
                        case 270:
                            TableShape = new int[,] { { 0, 0, 0, 1 }, { 1, 1, 1, 1 } };
                            break;
                        case 180:
                            TableShape = new int[,] { { 1, 1 }, { 0, 1 }, { 0, 1 }, { 0, 1 } };
                            break;
                        case 90:
                            TableShape = new int[,] { { 1, 1, 1, 1 }, { 1, 0, 0, 0 } };
                            break;
                    }

                    break;

                case "Z":
                    switch (this.rotation)
                    {
                        case 0:
                        case 180:
                            TableShape = new int[,] { { 1, 0 }, { 1, 1 }, { 0, 1 } };
                            break;
                        case 90:
                        case 270:
                            TableShape = new int[,] { { 0, 1, 1 }, { 1, 1, 0 } };
                            break;
                    }

                    break;

                case "T":
                    switch (this.rotation)
                    {
                        case 0:
                            TableShape = new int[,] { { 1, 0 }, { 1, 1 }, { 1, 0 } };
                            break;
                        case 180:
                            TableShape = new int[,] { { 0, 1 }, { 1, 1 }, { 0, 1 }, };
                            break;
                        case 90:
                            TableShape = new int[,] { { 1, 1, 1 }, { 0, 1, 0 } };
                            break;
                        case 270:
                            TableShape = new int[,] { { 0, 1, 0 }, { 1, 1, 1 } };
                            break;
                    }

                    break;

            }

            return TableShape;

        }




        public void Move_shape(int x, int y, int direction) // Prend en argument les coordonnées actuelles de la forme avant qu'elle ne bouge
        {
            int[,] TableShape = GenerateShape();

            switch (direction)
            {
                case 1: //Quand la forme descend
                    {
                        Clear_Shape(x, y, TableShape);
                        Draw(x, y + 1);
                        break;
                    }

                case 2: //Quand la forme bouge à gauche
                    {
                        Clear_Shape(x, y, TableShape);
                        Draw(x - 2, y);

                        break;
                    }

                case 3: //Quand la forme bouge à droite
                    {

                        Clear_Shape(x, y, TableShape);
                        Draw(x + 2, y);
                        break;
                    }




            }

        }

        public void Rotate_Shape(int x, int y) // Supprime la shape existante et dessine une nouvelle shape avec une rotation +1
        {
            int index_rotation = Utilities.Find_Index(Rotations, rotation);
            int[,] TableShape = GenerateShape();
            Clear_Shape(x, y, TableShape);

            this.rotation = Rotations[Utilities.Next_Index(Rotations, index_rotation)];

            Draw(x, y);

        }


        public void Clear_Shape(int x, int y, int[,] TableShape) // Supprime la shape en x,y
        {
            // BOUCLE QUI EFFACE LA SHAPE EXISTANTE
            Console.SetCursorPosition(x, y);
            for (int i = 0; i < TableShape.GetLength(0); i++)
            {
                for (int j = 0; j < TableShape.GetLength(1); j++)
                {
                    if (TableShape[i, j] == 1)
                    {
                        Console.Write("  ");

                    }
                    else
                    {
                        Console.SetCursorPosition(Console.CursorLeft + 2, Console.CursorTop);
                        //Console.Write("  ");
                    }


                }
                Console.SetCursorPosition(x, Console.CursorTop + 1);
            }



        }

        public void Draw(int x, int y) // Dessine la shape en x,y
        {
            this.x = x;
            this.y = y;

            int[,] TableShape = GenerateShape();


            // BOUCLE QUI DESSINE LA NOUVELLE SHAPE
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = Interface.Colors[Index_Color];
            for (int i = 0; i < TableShape.GetLength(0); i++)
            {
                for (int j = 0; j < TableShape.GetLength(1); j++)
                {
                    if (TableShape[i, j] == 1)
                    {
                        Console.Write(carre);

                    }
                    else
                    {
                        Console.SetCursorPosition(Console.CursorLeft + 2, Console.CursorTop);
                        //Console.Write("  ");
                    }


                }
                Console.SetCursorPosition(x, Console.CursorTop + 1);
            }
            Console.ForegroundColor = Interface.Colors[0];

        }


        public void FillGameboard(int x, int y, int[,] gameboard_grid) // Rempli l'array gameboard qui définit les cases du tetris qui sont remplies
        {
            int[,] TableShape = this.GenerateShape();

            for (int i = 0; i < TableShape.GetLength(0); i++)
            {
                for (int j = 0; j < TableShape.GetLength(1); j++)
                {
                    if (TableShape[i, j] == 1)
                    {
                        gameboard_grid[y + i - 1, (x - 1) / 2 + j] = 1;
                    }
                }
            }


        }


        public bool Check_collision(int x, int y, int direction, int[,] gameboard_grid) // Vérifie que la forme n'est pas en collision avec une autre forme et retourne un booléen
        {
            bool collision = false;
            int[,] TableShape = this.GenerateShape();
            int index_rotation = Utilities.Find_Index(Rotations, this.rotation);



            switch (direction)
            {
                case 1: // Quand la forme descend on vérifie qu'il n'y a pas de forme déjà en dessous.
                    {
                        if (y < 41 - TableShape.GetLength(0))
                        {
                            for (int i = 0; i < TableShape.GetLength(0); i++)
                            {
                                for (int j = 0; j < TableShape.GetLength(1); j++)
                                {
                                    if (TableShape[i, j] == 1 && gameboard_grid[y + i, (x - 1) / 2 + j] == 1) // On vérifie qu'il n'existe pas une forme dans le gameboard grid qui entre en collision avec la forme qui descend
                                    {
                                        collision = true;
                                    }
                                }
                            }
                        }

                        break;
                    }

                case 2: // Quand la forme se deplace à gauche on vérifie qu'il n'y ait pas déjà une autre forme
                    {
                        if (x > 1)
                        {
                            for (int i = 0; i < TableShape.GetLength(0); i++)
                            {
                                for (int j = 0; j < TableShape.GetLength(1); j++)
                                {
                                    if (TableShape[i, j] == 1 && gameboard_grid[y + i - 1, (x - 1) / 2 + j - 1] == 1) // On vérifie qu'il n'existe pas une forme dans le gameboard grid qui entre en collision avec la forme qui descend
                                    {
                                        collision = true;
                                    }
                                }
                            }
                        }


                        break;
                    }


                case 3: // Quand la forme se deplace à droite on vérifie qu'il n'y ait pas déjà une autre forme
                    {
                        if (x < 40 - this.GenerateShape().GetLength(1) * 2)
                        {
                            for (int i = 0; i < TableShape.GetLength(0); i++)
                            {
                                for (int j = 0; j < TableShape.GetLength(1); j++)
                                {
                                    if (TableShape[i, j] == 1 && gameboard_grid[y + i - 1, (x - 1) / 2 + j + 1] == 1) // On vérifie qu'il n'existe pas une forme dans le gameboard grid qui entre en collision avec la forme qui descend
                                    {
                                        collision = true;
                                    }
                                }
                            }
                        }

                        break;
                    }

                case 4: // Dans le cas où on fait tourner la forme
                    {
                        Shape temp_newshape = new Shape(this.TypeShape, Rotations[Utilities.Next_Index(Rotations, index_rotation)]);

                        {
                            for (int i = 0; i < temp_newshape.GenerateShape().GetLength(0); i++)
                            {
                                for (int j = 0; j < temp_newshape.GenerateShape().GetLength(1); j++)
                                {
                                    if (temp_newshape.GenerateShape()[i, j] == 1 && gameboard_grid[y + i - 1, (x - 1) / 2 + j] == 1) // On vérifie qu'il n'existe pas une forme dans le gameboard grid qui entre en collision avec la forme qui descend
                                    {
                                        collision = true;
                                    }
                                }
                            }
                        }




                    }

                    break;
            }



            return collision;

        }

    }


}
