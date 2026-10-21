using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Datenbank_Projekt
{
    public partial class LoadingScreen : Form
    {
        private int loadingBarValue;

        public LoadingScreen()
        {
            InitializeComponent();
        }
        private void LoadingScreen_Load(object sender, EventArgs e)
        {
            loadingbarTimer.Start();
        }

        private void loadingbarTimer_Tick(object sender, EventArgs e)
        {
            loadingBarValue++;

            lblLoadingProgress.Text = loadingBarValue.ToString() + "%";
            loadingProgressbar.Value = loadingBarValue;

            if(loadingBarValue >= loadingProgressbar.Maximum)
            {
                loadingbarTimer.Stop();

                // Finisch loading show main menu screen

                // Unten ist der Code welcher erscheint und den Loadingbar danach verstecken lässt
                MainMenuScreen mainMenuScreen = new MainMenuScreen();
                mainMenuScreen.Show();

                this.Hide();
            }
        }


    }
}
