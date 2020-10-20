using System;
using System.Drawing;
using System.Windows;
using System.Text;
using System.Linq;
using System.Threading;


namespace Tetris
{



    class Program_entry
    {
        static void Main(string[] args)
        {

            Generate_console();
            Interface menu = new Interface();
            menu.LaunchMenu();

        }


        static void Generate_console()
        {
            //Console.SetWindowSize(80, 45);
            Console.CursorVisible = false;
            Console.Title = "Tetris";

        }
    } 
  

}


