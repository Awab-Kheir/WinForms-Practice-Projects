using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XO_Game_Final.Properties;

namespace XO_Game_Final
{
    public partial class TicTacToeForm : Form
    {
        stGameStatus GameStatus;
        enPlayer PlayerTurn = enPlayer.Player1;
        enum enPlayer
        {
            Player1,
            Player2
        }

        enum enWinner
        {
            Player1,
            Player2,
            Draw,
            GameInProgress
        }

        struct stGameStatus
        {
            public enWinner Winner;
            public bool GameOver;
            public short PlayCount;
        }

        public bool CheckValues(Button btn1, Button btn2, Button btn3)             //.ToString()    ظبطت بدون  II   
        {
            if ((btn1.Tag.ToString() != "?") && (btn1.Tag.ToString() == btn2.Tag.ToString()) && (btn1.Tag.ToString() == btn3.Tag.ToString()))
            {
                btn1.BackColor = Color.GreenYellow;
                btn2.BackColor = Color.GreenYellow;
                btn3.BackColor = Color.GreenYellow;

                if (btn1.Tag.ToString() == "X")
                {
                    GameStatus.Winner = enWinner.Player1;
                    /*GameStatus.GameOver = true;
                    EndGame();
                    return true;*/

                }
                else
                {
                    GameStatus.Winner = enWinner.Player2;
                    /*GameStatus.GameOver = true;
                    EndGame();
                    return true;*/
                }

                GameStatus.GameOver = true;                            //    ليش لاكتبهم مرتين II
                EndGame();
                return true;
            }

            GameStatus.GameOver = false;               // II مو ضرورية
            return false;
        }

        void EndGame()
        {

            lblTurn.Text = "GameOver";
            switch (GameStatus.Winner)
            {
                case enWinner.Player1:

                    lblWinner.Text = "Player1";
                    break;

                case enWinner.Player2:

                    lblWinner.Text = "Player2";
                    break;

                default:

                    lblWinner.Text = "Draw";
                    break;
            }

            MessageBox.Show("GameOver", "GameOver", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public void CheckWinner()
        {
            //checked rows
            if (CheckValues(button1, button2, button3))
                return;

            if (CheckValues(button4, button5, button6))
                return;

            if (CheckValues(button7, button8, button9))
                return;

            //checked cols
            if (CheckValues(button1, button4, button7))
                return;

            if (CheckValues(button2, button5, button8))
                return;

            if (CheckValues(button3, button6, button9))
                return;

            //check Diagonal
            if (CheckValues(button1, button5, button9))
                return;

            if (CheckValues(button3, button5, button7))
                return;

        }

        public void ChangeImage(Button btn)
        {
            if (btn.Tag.ToString() == "?")
            {
                switch (PlayerTurn)
                {
                    case enPlayer.Player1:
                        btn.Image = Resources.X;
                        PlayerTurn = enPlayer.Player2;
                        lblTurn.Text = "Player 2";
                        GameStatus.PlayCount++;
                        btn.Tag = "X";
                        CheckWinner();
                        break;

                    case enPlayer.Player2:
                        btn.Image = Resources.O;
                        PlayerTurn = enPlayer.Player1;
                        lblTurn.Text = "Player 1";
                        GameStatus.PlayCount++;
                        btn.Tag = "O";
                        CheckWinner();
                        break;

                }
            }
            else
            {
                MessageBox.Show("Wrong Choice", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            if (GameStatus.PlayCount == 9 && lblTurn.Text != "GameOver") // GameStatus.Winner == enWinner.GameInProgress  او بقدر قول II
            {
                GameStatus.GameOver = true;
                GameStatus.Winner = enWinner.Draw;
                EndGame();
            }


        }


        public TicTacToeForm()
        {
            InitializeComponent();
        }


        /*************************************************************/

        private void button_Click(object sender, EventArgs e)
        {

            ChangeImage((Button)sender);

            //ChangeImage(button1);                      لو بدي كل واحد لحاله بحدث  II
        }

        private void TicTacToeForm_Load(object sender, EventArgs e)
        {

        }

        private void ResetButton(Button btn)
        {
            btn.Image = Resources.question_mark_96;
            btn.Tag = "?";
            btn.BackColor = Color.Transparent;                     //Color.Black;   نفس الشي  II
        }

        private void RestartGame()
        {
            ResetButton(button1);
            ResetButton(button2);
            ResetButton(button3);
            ResetButton(button4);
            ResetButton(button5);
            ResetButton(button6);
            ResetButton(button7);
            ResetButton(button8);
            ResetButton(button9);

            PlayerTurn = enPlayer.Player1;
            lblTurn.Text = "Player 1";
            GameStatus.PlayCount = 0;
            GameStatus.GameOver = false;
            GameStatus.Winner = enWinner.GameInProgress;
            lblWinner.Text = "In Progress";

        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            RestartGame();
        }


        private void TicTacToeForm_Paint(object sender, PaintEventArgs e)
        {

            Color white = Color.FromArgb(255, 255, 255, 255);

            Pen whitePen = new Pen(white);
            whitePen.Width = 15;

            //whitePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            whitePen.StartCap = System.Drawing.Drawing2D.LineCap.Round;     //LineCap.ArrowAnchor
            whitePen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            //draw Horizental lines
            e.Graphics.DrawLine(whitePen, 400, 300, 1050, 300);
            e.Graphics.DrawLine(whitePen, 400, 460, 1050, 460);
            //draw Vertical lines 
            e.Graphics.DrawLine(whitePen, 610, 140, 610, 620);
            e.Graphics.DrawLine(whitePen, 840, 140, 840, 620);

        }

    }
}

