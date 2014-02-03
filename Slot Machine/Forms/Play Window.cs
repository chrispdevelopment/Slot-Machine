//Christopher Pratt 24/04/12
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Slot_Machine.Forms
{
    public partial class PlayWindow : Form
    {
        //Declare varibles
        int Score; //Holds player score
        int Nudges; //Holds player nudge amount
        int Holds; //Holds player hold amount
        int Coins; //Holds player coin amount
        int Money; //Holds player money amount
        bool[] holdsPressed; //Holds which hold buttons have been pressed
        int[] picNumbers; //Holds array of middle pictuer numbers
        int[] topPicNumbers; //Holds array of top pictuer numbers
        int[] bottomPicNumbers; //Holds array of bottom pictuer numbers
        Bitmap[] pics; //Holds array of middle images
        Random rand; //Holds random number generator object
        ScoreWindow score; //Holds score window object
        
        //Constructor for class
        public PlayWindow()
        {
            InitializeComponent();
        }

        //Events to happen when form runs
        private void PlayWindow_Load(object sender, EventArgs e)
        {
            //Next set of commands tell the form to center on the screen
            int boundWidth = Screen.PrimaryScreen.Bounds.Width;
            int boundHeight = Screen.PrimaryScreen.Bounds.Height;
            int x = boundWidth - this.Width;
            int y = boundHeight - this.Height;
            this.Location = new Point(x / 2, y / 2);

            rand = new Random(); //Creates new instance of random number generator

            score = new ScoreWindow(); //Creates new instance of the play window

            //Set the player score to 0 & give them 3 coins
            Score = 0;
            Coins = 1;

            //Sets the coins text field to display coin amount
            txtCoins.Text = Convert.ToString(Coins);

            //initiate Arrays
            holdsPressed = new bool[3] {false, false, false}; //Array filled on load so that no holds are pressed
            picNumbers = new int[3];
            topPicNumbers = new int[3];
            bottomPicNumbers = new int[3];
            pics = new Bitmap[10] { Properties.Resources.eg1, //Array filled with images from resource area
                Properties.Resources.eg2, 
                Properties.Resources.eg3, 
                Properties.Resources.eg4, 
                Properties.Resources.eg5, 
                Properties.Resources.eg6, 
                Properties.Resources.eg7, 
                Properties.Resources.eg8, 
                Properties.Resources.eg9, 
                Properties.Resources.eg10 };
        }

        //Events to happend when nudge button is pressed
        private void btnNudge1_Click(object sender, EventArgs e)
        {
            Nudge(0); //Run nudge function when button is clicked
        }

        //Events to happend when nudge button is pressed
        private void btnNudge2_Click(object sender, EventArgs e)
        {
            Nudge(1); //Run nudge function when button is clicked
        }

        //Events to happend when nudge button is pressed
        private void btnNudge3_Click(object sender, EventArgs e)
        {
            Nudge(2); //Run nudge function when button is clicked
        }

        //Events to happend when hold button is pressed
        private void btnHold1_Click(object sender, EventArgs e)
        {
            if (Holds > 0) //Check to see if player has holds
            {
                btnHold1.BackColor = Color.Yellow; //Set hold button colour to yellow
                Hold(0); //Run hold function when button is clicked
            }
        }

        //Events to happend when hold button is pressed
        private void btnHold2_Click(object sender, EventArgs e)
        {
            if (Holds > 0) //Check to see if player has holds
            {
                btnHold2.BackColor = Color.Yellow; //Set hold button colour to yellow
                Hold(1); //Run hold function when button is clicked
            }
        }

        //Events to happend when hold button is pressed
        private void btnHold3_Click(object sender, EventArgs e)
        {
            if (Holds > 0) //Check to see if player has holds
            {
                btnHold3.BackColor = Color.Yellow; //Set hold button colour to yellow
                Hold(2); //Run hold function when button is clicked
            }
        }

        //Events to happen when coin button is pressed
        private void btnCoin_Click(object sender, EventArgs e)
        {
            //Recursive statment to check whether the player has any coins left
            if (Coins > 0)
            {
                Coins = Coins - 1; //1 coin taken away
                Money = Money + 5; //5 added to money amount
                txtCoins.Text = Convert.ToString(Coins); //Coins text set to equal coins variable
                txtMoney.Text = "$" + Convert.ToString(Money); //Money text set to equal money variable
            }
            //If the player has no coins left then this is run
            else
            {
                MessageBox.Show("You are out of coins"); //Displays help message to player
            }
        }

        //Events to happen when spin button is pressed
        private void btnSpin_Click(object sender, EventArgs e)
        {
            Spin(); //Runs spin function
        }

        //Events to happen when main menu button is pressed
        private void btnMainMenu_Click(object sender, EventArgs e)
        {
            MainMenu menu = new MainMenu(); //New instance of form created
            menu.Show(); //Menu told to show on screen
            this.Close(); //Current menu told to close
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            HelpWindow help = new HelpWindow();
            help.Show();
        }

        //Function for when spin is called
        public void Spin()
        {
            //Select statment to see if the players money has run out
            if (Money > 0)
            {
                Money = Money - 1; //Takes 1 of the players money each time the spin method is called
                txtMoney.ForeColor = System.Drawing.Color.Red; //Set the money font colour to red

                //Run function to update pictuers
                pictuerUpdate();

                //Run function to see whether pictuers match
                matchCheck();

                //A select statment to see whether the players score equals 100
                if (Score == 100)
                {
                    Coins = Coins + 1; //If the players score equals 100 then 1 is added to the players coins
                    MessageBox.Show("You have hit 100 points and earned an extra coin");
                }

                //Next statments change colour of hold buttons back to start colour
                btnHold1.BackColor = SystemColors.Control;
                btnHold2.BackColor = SystemColors.Control;
                btnHold3.BackColor = SystemColors.Control;

                //Loop to set all hold vaules in hold array back to false
                for (int i = 0; i <= 2; i++)
                {
                    holdsPressed[i] = false;
                }

                if (Money == 0 && Coins == 0)
                {
                    score.Score = Score;
                    score.Show();
                    this.Close();
                }
            }
            //If the players money is empty then this is run
            else
            {
                MessageBox.Show("You need to insert more coins"); //Displays help message to player
            }
        }

        //function for when hold is called
        public void Hold(int hold)
        {
            if (Holds > 0) //Check if player has any holds
            {
                holdsPressed[hold] = true; //Set hold value in hold array to true
                Holds = Holds - 1; //Take one of holds
                txtHolds.Text = Convert.ToString(Holds); //Update holds text field
            }
            else
            {
                MessageBox.Show("You have run out of holds"); //If player has no holds then display message
            }
        }

        //Function for when nudge is called
        public void Nudge(int nudge)
        {
            if (Nudges > 0) //Check if player has any nudges
            {
                if (picNumbers[nudge] == 9) //Check if pictuer number is 9
                {
                    picNumbers[nudge] = 1; //If it is then set pictuer number to 1
                }
                else
                {
                    picNumbers[nudge] = picNumbers[nudge] + 1; //If it isn't add 1 to the pictuer number
                }

                pictuerUpdate(); //Run function to update pictuers
                matchCheck(); //Check to see if any pictuers match
                Nudges = Nudges - 1; //Take 1 of player nudges
                txtNudges.Text = Convert.ToString(Nudges); //Update nudges text field
            }
            else
            {
                MessageBox.Show("You have run out of nudges"); //If player has no nudges then display message
            }
        }

        //Function to check if any pictuers match
        public void matchCheck()
        {
            //A recursive to see whether any of the pictuers match
            if (picNumbers[0] == picNumbers[1] && picNumbers[1] == picNumbers[2]) //This checks to see if all 3 pictuers match
            {
                Money = Money + 10; //If all the pictuers match then 10 is added to the players money
                Score = Score + 10; //If all the pictuers match then 10 is added to the players score
                txtMoney.ForeColor = System.Drawing.Color.Blue; //The money font colour is set to blue
                Nudges = Nudges + 1; //If all the pictuers match then 1 is added to the players nudges
            }
            //The next part of the recursive statment checks to see if 2 of the pictuers match if 3 don't
            else if (picNumbers[0] == picNumbers[1] ||
                picNumbers[0] == picNumbers[2] ||
                picNumbers[1] == picNumbers[2])
            {
                Money = Money + 2; //If 2 of the pictuers match then 2 is added to the players money
                Score = Score + 5; //If 2 of the pictuers match then 5 is added to the players score
                txtMoney.ForeColor = System.Drawing.Color.Blue; //The money font colour is set to blue
                Holds = Holds + 2; //If 2 of the pictuers match then 2 is added to the players holds
            }

            //Next 2 statments set the text fields for money and score to the current amount
            txtMoney.Text = "$" + Convert.ToString(Money);
            txtScore.Text = Convert.ToString(Score);
            txtHolds.Text = Convert.ToString(Holds);
            txtNudges.Text = Convert.ToString(Nudges);
        }

        //Function to update top and bottom pictuer number arrays
        public void topBottomUpdate(int i)
        {
            //A selection statment to work out what the top pic number variable should be
            if (picNumbers[i] == 9) //Check to see if variable = 9
            {
                topPicNumbers[i] = 1; //If it does then set variable to 1
            }
            else
            {
                topPicNumbers[i] = picNumbers[i] + 1; //If it dosnt then add one to the variable
            }

            //A selection statment to work out what the bottom pic number variable should be
            if (picNumbers[i] == 0) //Check to see if variable = 0
            {
                bottomPicNumbers[i] = 9; //If it does then set variable to 9
            }
            else
            {
                bottomPicNumbers[i] = picNumbers[i] - 1; //If it dosnt then subtract one from the variable
            }
        }

        //Function to update the pictuers on the interface
        public void pictuerUpdate()
        {
            if (holdsPressed[0] == false) //Check to see if the first hold value in the hold array is set to false
            {
                picNumbers[0] = rand.Next(9); //Randomly generates a number that is no larger then 9

                //Next statment assign images to the pictuer boxes, pulling from the pics array using the randoml numbers generated above
                picMiddle1.Image = pics[picNumbers[0]];

                //Run function to update top & bottom pictuer numbers
                topBottomUpdate(0);

                //Next statments assign images to the top and bottom pictuers depending on what is in the middle pictuers
                picTop1.Image = pics[topPicNumbers[0]];
                picBottom1.Image = pics[bottomPicNumbers[0]];
            }

            if (holdsPressed[1] == false) //Check to see if the second hold value in the hold array is set to false
            {
                picNumbers[1] = rand.Next(9); //Randomly generates a number that is no larger then 9

                picMiddle2.Image = pics[picNumbers[1]]; //Next statment assign images to the pictuer boxes, pulling from the pics array using the randoml numbers generated above

                topBottomUpdate(1); //Run function to update top & bottom pictuer numbers

                //Next statments assign images to the top and bottom pictuers depending on what is in the middle pictuers
                picTop2.Image = pics[topPicNumbers[1]];
                picBottom2.Image = pics[bottomPicNumbers[1]];
            }

            if (holdsPressed[2] == false) //Check to see if the third hold value in the hold array is set to false
            {
                picNumbers[2] = rand.Next(9); //Randomly generates a number that is no larger then 9
                
                picMiddle3.Image = pics[picNumbers[2]]; //Next statment assign images to the pictuer boxes, pulling from the pics array using the randoml numbers generated above
                
                topBottomUpdate(2);//Run function to update top & bottom pictuer numbers

                //Next statments assign images to the top and bottom pictuers depending on what is in the middle pictuers
                picTop3.Image = pics[topPicNumbers[2]];
                picBottom3.Image = pics[bottomPicNumbers[2]];
            }
        }
    }
}
