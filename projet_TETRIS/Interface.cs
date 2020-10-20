using System;
using System.Drawing;
using System.Windows;
using System.Text;
using System.Linq;
using System.Threading;

namespace Tetris
{



    class Interface // Création du menu 
    {

        public static ConsoleColor[] Colors = { ConsoleColor.White, ConsoleColor.Yellow, ConsoleColor.Cyan, ConsoleColor.DarkGreen, ConsoleColor.Magenta, ConsoleColor.DarkRed };


        public void ShowMenu(int currrentChoice) // CurrentCHoice est un entier entre 1 et 3 qui détermine quel choix est en surbrillance

        {
            Console.Clear();
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string a = "\u25ba"; //FLECHE 
            string SelectionString = a + " "; // ELEMENT SELECTIONNE
            string[] Choices = { "PLAY", "SETTINGS", "EXIT" }; // TABLEAU CONTENANT LES OPTIONS POSSIBLES
            string[] TETRIS_title = { "T ", "E ", "T ", "R ", "I ", "S" };

            int hauteur = Console.WindowHeight;
            int largeur = Console.WindowWidth;

            Console.SetCursorPosition((largeur - 12) / 2, (hauteur - 7) / 2 - 2); // Le curseur se positionne au milieu de la fenêtre
            for (int c = 0; c < 6; c++)
            {
                Console.ForegroundColor = Colors[c];
                Console.Write(TETRIS_title[c]);
            }
            Console.ForegroundColor = Colors[0];

            Console.SetCursorPosition((largeur - 8) / 2, Console.CursorTop + 1); // Le curseur se positionne au milieu de la fenêtre
            Console.WriteLine("M E N U");
            Console.WriteLine();

            int i = 1;
            foreach (string ch in Choices) // BOUCLE POUR AFFICHER LA FLECHE DEVANT L'OPTION CHOISIE
            {
                if (i == currrentChoice)
                {
                    Console.SetCursorPosition((largeur - 8) / 2 - 2, Console.CursorTop); // Alignement du texte avec le menu
                    Console.WriteLine(SelectionString + ch); //Si l'option dans choices est celle sélectionnée alors on affiche la flèche devant 
                }
                else
                {
                    Console.SetCursorPosition((largeur - 8) / 2, Console.CursorTop); // Alignement du texte
                    Console.WriteLine(ch);
                }
                i = i + 1;
            }

        }



        public void LaunchMenu() // LANCEMENT DU MENU
        {
            bool Continue = true;
            int StartPosition = 1; // POSITION PAR DEFAUT = 1 
            ShowMenu(StartPosition); // AFFICHE LE MENU

            while (Continue == true)
            {

                StartPosition = ChangeSelection(StartPosition, 3, 0); //En attente d'une action du joueur
                ShowMenu(StartPosition); // AFFICHE LE MENU AVEC LA  NOUVELLE POSITION une fois que le joueur a appuyé sur une flèche

            }


        }



        public int ChangeSelection(int ChoiceIndex, int ChoicesTotal, int Menu_level) // CHANGEMENT DE SELECTION DANS LE MENU : 
        //choiceindex est le numéro de la ligne en surbrillance, 
        //choice total est le nombre total d'options dans le menu, 
        //menu_level est égal à 0 ou 1 (menu principal ou menu réglages)
        {
            ConsoleKeyInfo entree = Console.ReadKey(true); //attente que le joueur appuie sur une touche

            switch (entree.Key)
            {
                case ConsoleKey.UpArrow:
                    if (ChoiceIndex > 1)
                    {
                        ChoiceIndex = ChoiceIndex - 1;

                    }

                    break;

                case ConsoleKey.DownArrow:

                    if (ChoiceIndex < ChoicesTotal)
                    {
                        ChoiceIndex = ChoiceIndex + 1;

                    }

                    break;

                case ConsoleKey.Enter:
                    if (Menu_level == 0)
                    {
                        EnterKey_home(ChoiceIndex); // Lancement de la méthode enterkey quand on clique sur entrée dans le menu principal
                    }
                    else
                    {
                        EnterKey_setting(ChoiceIndex);
                    }
                    break;

                case ConsoleKey.RightArrow:
                case ConsoleKey.LeftArrow:
                    if (Menu_level == 1 & ChoiceIndex == 1)
                    {
                        if (Options.level == 1)
                        {
                            Options.level = 2;
                            Options.Thread_Level = 100;
                        }
                        else
                        {
                            Options.level = 1;
                            Options.Thread_Level = 150;
                        }
                    }

                    break;


            }
            return ChoiceIndex;

        }




        public void EnterKey_home(int ChoiceIndex) //détermination de l'action en appuyant sur la touche entrée dans le menu principal
        {
            switch (ChoiceIndex)
            {
                case 1:
                    Game gm = new Game();


                    gm.Run();
                    break;

                case 2:
                    LaunchSettings();
                    break;

                case 3:
                    System.Environment.Exit(0);
                    break;

            }
        }


        public void EnterKey_setting(int ChoiceIndex)
        {

            switch (ChoiceIndex)
            {
                case 1:
                    LaunchMenu();
                    break;

                case 2:
                    LaunchMenu();
                    break;

            }

        }


        public void LaunchSettings()
        {

            bool Continue = true;
            int StartPosition = 1;
            ShowSettings(StartPosition);

            while (Continue == true)
            {

                StartPosition = ChangeSelection(StartPosition, 2, 1); //En attente d'une action du joueur
                ShowSettings(StartPosition);

            }
        }




        public void ShowSettings(int currentChoice)
        {
            Console.Clear();


            Console.OutputEncoding = System.Text.Encoding.UTF8;
            string a = "\u25ba"; //FLECHE 
            string SelectionString = a + " "; // ELEMENT SELECTIONNE
            string lev = "LEVEL : " + Options.level;
            string[] Choices = { lev, "RETURN" };
            int hauteur = Console.WindowHeight;
            int largeur = Console.WindowWidth;


            Console.SetCursorPosition((largeur - 12) / 2, (hauteur - 7) / 2 - 2);
            Console.WriteLine("T E T R I S");
            Console.SetCursorPosition((largeur - 16) / 2, Console.CursorTop);
            Console.WriteLine("S E T T I N G S");
            Console.WriteLine();


            int i = 1;
            foreach (string ch in Choices)
            {
                if (i == currentChoice)
                {
                    Console.SetCursorPosition(((largeur - 8) / 2) - 2, Console.CursorTop);
                    Console.WriteLine(SelectionString + ch);
                }
                else
                {
                    Console.SetCursorPosition((largeur - 8) / 2, Console.CursorTop);
                    Console.WriteLine(ch);
                }


                i = i + 1;
            }

        }


    }
}
