using System;
using System.Drawing;
using System.Windows;
using System.Text;
using System.Linq;
using System.Threading;

namespace Tetris
{

    public class Utilities //Fonctions annexe 
    {

        public static int Find_Index(int[] array, int n)// Permet de retrouner l'indice d'une valeur dans un tableau
        {
            int i;
            for (i = 0; i < array.Length; i++)
            {
                if (array[i] == n)
                    return i;
            }
            return -1;
        }



        public static int Next_Index(int[] array, int Current_Index)  // Retourne l'index suivant dans un array en renvoyant 0 si on atteint la dernière case
        {
            if (Current_Index < array.Length - 1)
            {
                return Current_Index + 1;
            }
            else
            {
                return 0;
            }

        }

    }
}
