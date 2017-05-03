using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment05
{
    class Program : Engine
    {
        public Program() : base()
        {

        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Program.canvas.csAdd(doctor);

            for(int i = 0; i < 13; i++)
            {
                Box box = new Box(i * 100, 0);
                canvas.csAdd(box);
                box = new Box(i * 100, 600);
                canvas.csAdd(box);
            }
            for(int i = 0; i < 7; i++)
            {
                Box box = new Box(0, i * 100);
                canvas.csAdd(box);
                box = new Box(1200, i * 100);
                canvas.csAdd(box);
            }
            Program.canvas.csAdd(silent);
            Application.Run(new Program());
        }
    }
}
