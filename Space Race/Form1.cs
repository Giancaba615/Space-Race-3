using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Space_Race
{
    public partial class Form1 : Form
    {

        Rectangle player1 = new Rectangle(150, 560, 15, 20);
        Rectangle player2 = new Rectangle(400, 560, 15, 20);
        int playersSpeed = 10;

        List<Rectangle> astroid = new List<Rectangle>();
        List<int> astroidSpeed = new List<int>();
        List<string> astroidColours = new List<string>();
        int astroidSize = 5;

        int player1Score = 0;
        int player2Score = 0;
        int time = 1000;

        bool wDown = false;
        bool sDown = false;
        bool upArrowDown = false;
        bool downArrowDown = false;

        SolidBrush whiteBrush = new SolidBrush(Color.White);
        SolidBrush rocketColor = new SolidBrush(Color.NavajoWhite);

        Random randGen = new Random();
        int randValue = 0;

        string gameState = "waiting";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {

            timeLabel.Text = $"TimeLeft:{time}";
            p1ScoreLabel.Text = $"Score:{player1Score}";
            p2ScoreLabel.Text = $"Score:{player2Score}";


            if (gameState == "waiting")

            {
                titleLabel.Text = "BALL CATCH";

                subTitleLabel.Text = "Press Space Bar to Start or Escape to Exit";
            }

            //astroids
            if (gameState == "running")
            { 
                // draw text at top 
                timeLabel.Text = $"Time Left:{time}";
                p1ScoreLabel.Text = $"Score:{player1Score}";
                p2ScoreLabel.Text = $"Score:{player2Score}";

                for (int i = 0; i < astroid.Count(); i++)
                {
                    if (astroidColours[i] == "white")
                    {
                        e.Graphics.FillEllipse(whiteBrush, astroid[i]);
                    }
                    else if (astroidColours[i] == "white")
                    {
                        e.Graphics.FillEllipse(whiteBrush, astroid[i]);
                    }
                    else if (astroidColours[i] == "white")
                    {
                        e.Graphics.FillEllipse(whiteBrush, astroid[i]);
                    }
                }


                e.Graphics.FillRectangle(rocketColor, player1);
                e.Graphics.FillRectangle(rocketColor, player2);

            }
            if (gameState == "over")
            {

                timeLabel.Text = "";

                p1ScoreLabel.Text = "";
                p2ScoreLabel.Text = "";

                titleLabel.Text = "GAME OVER";

                subTitleLabel.Text = $"Your final score was {player1Score}";
                subTitleLabel.Text = $"Your final score was {player2Score}";

                subTitleLabel.Text += "\nPress Space Bar to Start or Escape to Exit";

            }
        }

       

        public void GameInitialize()
        {
            titleLabel.Text = "";
            subTitleLabel.Text = "";
            
            gameLoop.Enabled = true;
            gameState = "running";
            time = 1000;
            player1Score = 0;
            player2Score = 0;
            astroid.Clear();
            astroidColours.Clear();
            astroidSpeed.Clear();

        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = true;
                    break;
                case Keys.S:
                    sDown = true;
                    break;
                case Keys.Up:
                    upArrowDown = true;
                    break;
                case Keys.Down:
                    downArrowDown = true;
                    break;
                case Keys.Space:

                    if (gameState == "waiting" || gameState == "over")

                    {

                        // GameInitialize();
                     //   gameState = "running";
                        gameLoop.Enabled = true;
                    }

                    break;

                case Keys.Escape:

                    if (gameState == "waiting" || gameState == "over")

                    {

                        Application.Exit();

                    }

                    break;
                  

            }

            }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    wDown = false;
                    break;
                case Keys.S:
                    sDown = false;
                    break;
                case Keys.Up:
                    upArrowDown = false;
                    break;
                case Keys.Down:
                    downArrowDown = false;
                    break;
            }

        }


        private void gameLoop_Tick_1(object sender, EventArgs e)
        {
            //player 1 move button
            if (wDown == true)
            {
                player1.Y += playersSpeed;
            }

            if (sDown == true)
            {
                player1.Y -= playersSpeed;
            }
            //player 2 move button 
            if (upArrowDown == true)
            {
                player2.Y += playersSpeed;
            }

            if (downArrowDown == true)
            {
                player2.Y -= playersSpeed;
            }

            for (int i = 0; i < astroid.Count(); i++)
            {


                int x = astroid[i].X + astroidSpeed[i];

                astroid[i] = new Rectangle(x, astroid[i].Y, astroidSize, astroidSize);

            }

            randValue = randGen.Next(0, 100);

            if (randValue < 15) //left side
            {

                int y = randGen.Next(10, this.Height - 50);
                astroid.Add(new Rectangle(0, y, astroidSize, astroidSize));

                astroidSpeed.Add(randGen.Next(1, 3));

                astroidColours.Add("white");

            }
            //ride side
            if (randValue < 15)
            {

                int y = randGen.Next(10, this.Height - 50);
                astroid.Add(new Rectangle(this.Width, y, astroidSize, astroidSize));

                astroidSpeed.Add(randGen.Next(1, 3) * -1);

                astroidColours.Add("white");

            }


            //check collision betweenastroidandrocket

            for (int i = 0; i < astroid.Count(); i++)
            {
                if (player1.IntersectsWith(astroid[i]))
                {
                    player1.Y = 560;
                }
                else if (player2.IntersectsWith(astroid[i]))
                {
                    player2.Y = 560;
                }



            }
            if (player1.Y < 0)
            {
                player1Score++;
                p1ScoreLabel.Text = $"{player1Score}";

                player1.Y = 560;

            }
            else if (player2.Y < 0)
            {
                player2Score++;
                p2ScoreLabel.Text = $"{player2Score}";

                player2.Y = 560;
            }

            //decrease time counter and check to see if time is  up 
            time--;

            if (time == 0)
            {
                gameLoop.Enabled = false;
                gameState = "over";

            }

            Refresh();



        }
    }
}






