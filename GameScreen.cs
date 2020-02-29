using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public enum Player1Buttons
    {
        Left,
        Right,
        Up,
        Down
    };

    public enum Player2Buttons
    {
        A,
        D,
        W,
        S
    };

    public partial class GameScreen : Form
    {
        private List<SnakeSegment> snakeP1, snakeP2;
        private SnakeSegment food;
        private Boolean IsGameStarted { get; set; }
        private List<Snake> playersList;
        public GameScreen()
        {
            InitializeComponent();

            Snake player1 = new Snake(16, 16, 20, 0, 100);
            Snake player2 = new Snake(16, 16, 20, 0, 100);

            food = new SnakeSegment();

            foreach (Player player in playersList)
            {
                
            }



            gameTimer.Interval = 1250 / SettingsP1.Speed;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();


            StartGame();
        }


        private async Task MoveSnake1()
        {
            if (Input.KeyPress(Keys.Right) && SettingsP1.P1Directions != DirectionsP1.Left)
            {
                SettingsP1.P1Directions = DirectionsP1.Right;
            }
            else if (Input.KeyPress(Keys.Left) && SettingsP1.P1Directions != DirectionsP1.Right)
            {
                SettingsP1.P1Directions = DirectionsP1.Left;
            }
            else if (Input.KeyPress(Keys.Up) && SettingsP1.P1Directions != DirectionsP1.Down)
            {
                SettingsP1.P1Directions = DirectionsP1.Up;
            }
            else if (Input.KeyPress(Keys.Down) && SettingsP1.P1Directions != DirectionsP1.Up)
            {
                SettingsP1.P1Directions = DirectionsP1.Down;
            }


            for (int i = snakeP1.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {
                    switch (SettingsP1.P1Directions)
                    {
                        case DirectionsP1.Left:
                            snakeP1[i].X--;
                            break;
                        case DirectionsP1.Right:
                            snakeP1[i].X++;
                            break;
                        case DirectionsP1.Up:
                            snakeP1[i].Y--;
                            break;
                        case DirectionsP1.Down:
                            snakeP1[i].Y++;
                            break;
                    }


                    int maxXpos = pbCanvas.Size.Width / SettingsP1.Width;
                    int maxYpos = pbCanvas.Size.Height / SettingsP1.Height;

                    if (
                        snakeP1[i].X < 0 || snakeP1[i].Y < 0 ||
                        snakeP1[i].X > maxXpos || snakeP1[i].Y > maxYpos

                        )
                    {
                        Die(1);
                    }
                    for (int j = 1; j < snakeP1.Count; j++)
                    {
                        if (snakeP1[i].X == snakeP1[j].X && snakeP1[i].Y == snakeP1[j].Y)
                        {
                            Die(1);
                        }
                    }
                    if (snakeP1[0].X == food.X && snakeP1[0].Y == food.Y)
                    {
                        Eat(snakeP1);
                    }
                    for (int j = 0; j < snakeP2.Count; j++)
                    {
                        if (snakeP1[i].X == snakeP2[j].X && snakeP1[i].Y == snakeP2[j].Y && snakeP1.Count < snakeP2.Count)
                        {
                            DieBySnake("snake1");
                        }
                    }



                }
                else
                {
                    snakeP1[i].X = snakeP1[i - 1].X;
                    snakeP1[i].Y = snakeP1[i - 1].Y;

                }
            }

        }

        private async Task MoveSnake2()
        {
            if (Input.KeyPress(Keys.D) && SettingsP2.P2Directions != DirectionsP2.A)
            {
                SettingsP2.P2Directions = DirectionsP2.D;
            }
            else if (Input.KeyPress(Keys.A) && SettingsP2.P2Directions != DirectionsP2.D)
            {
                SettingsP2.P2Directions = DirectionsP2.A;
            }
            else if (Input.KeyPress(Keys.W) && SettingsP2.P2Directions != DirectionsP2.S)
            {
                SettingsP2.P2Directions = DirectionsP2.W;
            }
            else if (Input.KeyPress(Keys.S) && SettingsP2.P2Directions != DirectionsP2.W)
            {
                SettingsP2.P2Directions = DirectionsP2.S;
            }

            for (int i = snakeP2.Count - 1; i >= 0; i--)
            {
                if (i == 0)
                {

                    switch (SettingsP2.P2Directions)
                    {
                        case DirectionsP2.A:
                            snakeP2[i].X--;
                            break;
                        case DirectionsP2.D:
                            snakeP2[i].X++;
                            break;
                        case DirectionsP2.W:
                            snakeP2[i].Y--;
                            break;
                        case DirectionsP2.S:
                            snakeP2[i].Y++;
                            break;
                    }

                    int maxXpos = pbCanvas.Size.Width / SettingsP1.Width;
                    int maxYpos = pbCanvas.Size.Height / SettingsP1.Height;

                    if (

                        snakeP2[i].X < 0 || snakeP2[i].Y < 0 ||
                        snakeP2[i].X > maxXpos || snakeP2[i].Y > maxYpos
                        )
                    {
                        Die(2);
                    }
                    for (int j = 1; j < snakeP2.Count; j++)
                    {
                        if (snakeP2[i].X == snakeP2[j].X && snakeP2[i].Y == snakeP2[j].Y)
                        {
                            Die(2);
                        }
                    }

                    if (snakeP2[0].X == food.X && snakeP2[0].Y == food.Y)
                    {
                        Eat(snakeP2);
                    }

                    for (int j = 0; j < snakeP1.Count; j++)
                    {

                        if (snakeP2[i].X == snakeP1[j].X && snakeP2[i].Y == snakeP1[j].Y && snakeP2.Count < snakeP1.Count)
                        {
                            DieBySnake("snake2");
                        }
                    }


                }
                else
                {
                    snakeP2[i].X = snakeP2[i - 1].X;
                    snakeP2[i].Y = snakeP2[i - 1].Y;
                }
            }
        }

        private void UpdateScreen(object sender, EventArgs e)
        {
            if (SettingsP1.GameOver == true || SettingsP2.GameOver == true)
            {
                if (Input.KeyPress(Keys.Enter))
                {
                    IsGameStarted = true;
                    StartGame();
                }

            }
            {
                MoveSnake1();
                MoveSnake2();

            }
            pbCanvas.Invalidate();
        }

        private void StartGame()
        {
            label3.Visible = false;
            new SettingsP1();
            new SettingsP2();
            snakeP1.Clear();
            snakeP2.Clear();
            SnakeSegment head = new SnakeSegment { X = 10, Y = 5 };
            SnakeSegment head2 = new SnakeSegment { X = 5, Y = 10 };
            snakeP1.Add(head);
            snakeP2.Add(head2);
            generateFood();
        }

        private void generateFood()
        {
            int maxXpos = pbCanvas.Size.Width / SettingsP1.Width;
            int maxYpos = pbCanvas.Size.Height / SettingsP1.Height;
            Random rnd = new Random(); food = new SnakeSegment { X = rnd.Next(0, maxXpos), Y = rnd.Next(0, maxYpos) };
        }

        private void Eat(List<SnakeSegment> snake)
        {
            SnakeSegment body = new SnakeSegment
            {
                X = snake[snakeP1.Count - 1].X,
                Y = snake[snakeP1.Count - 1].Y,

            };

            snake.Add(body);

            generateFood();

        }

        private void Die(int PlayerNo)
        {
            if (PlayerNo == 1)
            {
                SettingsP1.GameOver = true;
            }
            else if (PlayerNo == 2)
            {
                SettingsP2.GameOver = true;
            }

        }

        private void DieBySnake(string snake)
        {
            if (snake == "snake2")
            {
                SettingsP2.GameOver = true;
            }
            else
            {
                SettingsP1.GameOver = true;
            }

        }

        private void UpdateGraphics(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            if (IsGameStarted == false)
            {
                string game = "Are you ready? Please press enter to start.";
                label3.BackColor = Color.DarkRed;
                label3.Text = game;
                label3.Visible = true;
            }
            else if (SettingsP1.GameOver == false && SettingsP2.GameOver == false)
            {
                Brush snakeColour; Brush snakeColour2; for (int i = 0; i < snakeP1.Count; i++)
                {
                    snakeColour = Brushes.Blue;

                    canvas.FillEllipse(snakeColour, new Rectangle(snakeP1[i].X * SettingsP1.Width, snakeP1[i].Y * SettingsP1.Height, SettingsP1.Width, SettingsP1.Height));


                    canvas.FillEllipse(Brushes.Red, new Rectangle(food.X * SettingsP1.Width, food.Y * SettingsP1.Height, SettingsP1.Width, SettingsP1.Height));
                }
                for (int i = 0; i < snakeP2.Count; i++)
                {

                    snakeColour2 = Brushes.Green;


                    canvas.FillEllipse(snakeColour2, new Rectangle(snakeP2[i].X * SettingsP2.Width, snakeP2[i].Y * SettingsP2.Height, SettingsP2.Width, SettingsP2.Height));

                }

            }
            else
            {
                string gameOver;
                if (SettingsP2.GameOver == true && SettingsP1.GameOver == false)
                {

                    gameOver = "Game Over\n" + "PLAYER 1 WON" + "\n Press enter to Restart \n";
                    label3.BackColor = Color.Blue;


                }
                else if (SettingsP2.GameOver == true && SettingsP1.GameOver == true)
                {
                    gameOver = "Game Over\n" + "That's a tie" + "\n Press enter to Restart \n";
                    label3.BackColor = Color.Orange;

                }
                else
                {

                    gameOver = "Game Over\n" + "PLAYER 2 WON" + "\n Press enter to Restart \n";
                    label3.BackColor = Color.Green;


                }
                label3.Text = gameOver;
                label3.Visible = true;


            }

        }


        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            Input.changeState(e.KeyCode, true);
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            Input.changeState(e.KeyCode, false);
        }

    }
}