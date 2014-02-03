using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Slot_Machine.Forms;
using Slot_Machine.Properties;

namespace Slot_Machine.Forms
{
    public partial class ScoreWindow : Form
    {
        public int Score;
        string[,] Scores;

        public ScoreWindow()
        {
            InitializeComponent();
        }

        private void ScoreWindow_Load(object sender, EventArgs e)
        {
            int boundWidth = Screen.PrimaryScreen.Bounds.Width;
            int boundHeight = Screen.PrimaryScreen.Bounds.Height;
            int x = boundWidth - this.Width;
            int y = boundHeight - this.Height;
            this.Location = new Point(x / 2, y / 2);

            //Scores = new string[,] {{"Name:",Convert.ToString(Score)}};
            //Settings.Default.Score = new ArrayList(Scores);
             
            //Scores = (string[,])Settings.Default.Score.ToArray(typeof(string));
            
            Scores = new string[1,2] {{"1st Matt Jones:"," 20"}};
            for (int i = Scores.GetLowerBound(0); i <= Scores.GetUpperBound(0); i++ )
            {
                for (int k = Scores.GetLowerBound(1); k <= Scores.GetUpperBound(1); k++ )
                {
                    txtScores.Text = Scores[i, k];
                }
            }
            //txtScores.Text = Scores[0,0] + Scores[0,1];
            txtScore.Text = Convert.ToString(Score);
            //Settings.Default.Save();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            PlayWindow play = new PlayWindow();
            play.Show();
            this.Close();
        }

        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            MainMenu menu = new MainMenu(); //New instance of form created
            menu.Show(); //Menu told to show on screen
            this.Close(); //Current menu told to close
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
