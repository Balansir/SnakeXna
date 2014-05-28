using System;
using System.Windows.Forms;

namespace SnakeProject
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
	        bool _isHard;
	        using (MainForm form = new MainForm())
	        {
		        if (form.ShowDialog() != DialogResult.OK)
					return;
		        _isHard = form.GetDifficult();
	        }

			using (MainGame game = new MainGame(_isHard))
            {
                game.Run();
            }
        }
    }
#endif
}

