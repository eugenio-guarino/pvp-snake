using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class GameScreen : Form
    {
        private List<SnakeSegment> Snake = new List<SnakeSegment>(); //creating a list array for the snake
        private List<SnakeSegment> Snake2 = new List<SnakeSegment>(); //creating a list array for the snake
        private SnakeSegment food = new SnakeSegment(); //creating a single Circle class called food
        private Boolean IsGameJustStarted { get; set; }
        public GameScreen()
        {
            InitializeComponent();

            new SettingsP1(); //linking the Settings Class to this Form+
            new SettingsP2(); //linking the Settings Class to this Form
            IsGameJustStarted = true;

            gameTimer.Interval = 1250 / SettingsP1.Speed;
            gameTimer.Tick += updateScreen;
            gameTimer.Start();


            startGame();
        }

        private void updateScreen(object sender, EventArgs e)
        {
            //this is the timers update screen function
            // each tick will run this function

            if (SettingsP1.GameOver == true || SettingsP2.GameOver == true)
            {
                //if is the Timers update screen function.
                //each tick will run this function

                if (Input.KeyPress(Keys.Enter))
                {

                    IsGameJustStarted = false;
                    startGame();
                }

            }
            else
            {
                //if the game is not over then the following commands will be executed 

                // below the actions will probe the keys being pressed by the player 
                //and more accordingly

                if (Input.KeyPress(Keys.Right) && SettingsP1.direction != DirectionsP1.Left)
                {
                    SettingsP1.direction = DirectionsP1.Right;
                }
                else if (Input.KeyPress(Keys.Left) && SettingsP1.direction != DirectionsP1.Right)
                {
                    SettingsP1.direction = DirectionsP1.Left;
                }
                else if (Input.KeyPress(Keys.Up) && SettingsP1.direction != DirectionsP1.Down)
                {
                    SettingsP1.direction = DirectionsP1.Up;
                }
                else if (Input.KeyPress(Keys.Down) && SettingsP1.direction != DirectionsP1.Up)
                {
                    SettingsP1.direction = DirectionsP1.Down;
                }

                if (Input.KeyPress(Keys.D) && SettingsP2.direction != DirectionsP2.A)
                {
                    SettingsP2.direction = DirectionsP2.D;
                }
                else if (Input.KeyPress(Keys.A) && SettingsP2.direction != DirectionsP2.D)
                {
                    SettingsP2.direction = DirectionsP2.A;
                }
                else if (Input.KeyPress(Keys.W) && SettingsP2.direction != DirectionsP2.S)
                {
                    SettingsP2.direction = DirectionsP2.W;
                }
                else if (Input.KeyPress(Keys.S) && SettingsP2.direction != DirectionsP2.W)
                {
                    SettingsP2.direction = DirectionsP2.S;
                }

                movePlayer(); //run move player function
            }
            pbCanvas.Invalidate(); //refresh the picture box and update the graphics on it

        }

        private void movePlayer()
        {

            // the main loop for the snake head and parts
            for (int i = Snake.Count - 1 ; i >= 0; i--)
            {
                // if the snake head is active
                if (i == 0)
                {
                    //move rest of the body according to which way the head is moving
                    switch (SettingsP1.direction)
                    {
                        case DirectionsP1.Left:
                            Snake[i].X--;
                            break;
                        case DirectionsP1.Right:
                            Snake[i].X++;
                            break;
                        case DirectionsP1.Up:
                            Snake[i].Y--;
                            break;
                        case DirectionsP1.Down:
                            Snake[i].Y++;
                            break;
                    }


                    //restrict the snake from living the canvas
                    int maxXpos = pbCanvas.Size.Width / SettingsP1.Width;
                    int maxYpos = pbCanvas.Size.Height / SettingsP1.Height;

                    if (
                        Snake[i].X < 0 || Snake[i].Y < 0 ||
                        Snake[i].X > maxXpos || Snake[i].Y > maxYpos

                        )
                    {
                        //end the game is snake either reaches edge of the canvas
                        die(1);
                    }
                    //detect collision with the body
                    //this loop will check if the snake had an collisione with other body parts
                    for (int j = 1; j < Snake.Count; j++)
                    {
                        if (Snake[i].X == Snake[j].X && Snake[i].Y == Snake[j].Y)
                        {
                            //if so we run the die function
                            die(1);
                        }
                    }
                    //detect collision between snake head and food
                    if (Snake[0].X == food.X && Snake[0].Y == food.Y)
                    {
                        //if so we run the eat function
                        eat(Snake);
                    }
                    //collision between the 2 snakes
                    for (int j = 0; j < Snake2.Count; j++)
                    {
                        if (Snake.Count > Snake2.Count && Snake.Count >= 5)
                        if (Snake[i].X == Snake2[j].X && Snake[i].Y == Snake2[j].Y)
                        {
                            //if so we run the die function
                            dieBySnake();
                        }
                    }



                }
                else
                {
                    //if there are no collision the we continue moving the snake and its parts
                    Snake[i].X = Snake[i - 1].X;
                    Snake[i].Y = Snake[i - 1].Y;

                }
            }

            for (int i = Snake2.Count - 1; i >= 0; i--)
            {
                // if the snake head is active
                if (i == 0)
                {

                    switch (SettingsP2.direction)
                    {
                        case DirectionsP2.A:
                            Snake2[i].X--;
                            break;
                        case DirectionsP2.D:
                            Snake2[i].X++;
                            break;
                        case DirectionsP2.W:
                            Snake2[i].Y--;
                            break;
                        case DirectionsP2.S:
                            Snake2[i].Y++;
                            break;
                    }

                    //restrict the snake from living the canvas
                    int maxXpos = pbCanvas.Size.Width / SettingsP1.Width;
                    int maxYpos = pbCanvas.Size.Height / SettingsP1.Height;

                    if (

                        Snake2[i].X < 0 || Snake2[i].Y < 0 ||
                        Snake2[i].X > maxXpos || Snake2[i].Y > maxYpos
                        )
                    {
                        //end the game is snake either reaches edge of the canvas
                        die(2);
                    }
                    //detect collision with the body
                    //this loop will check if the snake had an collisione with other body parts
                    for (int j = 1; j < Snake2.Count; j++)
                    {
                        if (Snake2[i].X == Snake2[j].X && Snake2[i].Y == Snake2[j].Y)
                        {
                            //if so we run the die function
                            die(2);
                        }
                    }

                    //detect collision between snake head and food
                    if (Snake2[0].X == food.X && Snake2[0].Y == food.Y)
                    {
                        eat(Snake2);
                    }
                    
                    //collision between the 2 snakes
                    for (int j = 0; j < Snake.Count; j++)
                    {
                        if (Snake2.Count > Snake.Count && Snake2.Count >= 5)
                        if (Snake2[i].X == Snake[j].X && Snake2[i].Y == Snake[j].Y)
                        {
                            //if so we run the die function
                            dieBySnake();
                        }
                    }


                }
                else
                {
                    //if there are no collision the we continue moving the snake and its parts
                    Snake2[i].X = Snake2[i - 1].X;
                    Snake2[i].Y = Snake2[i - 1].Y;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void startGame()
        {
            //this is the start game function

            label3.Visible = false; //set label 3 to invisible
            new SettingsP1(); // create a new istance of settings
            new SettingsP2(); // create a new istance of settings
            Snake.Clear(); //clear all snake parts
            Snake2.Clear(); //clear all snake parts
            SnakeSegment head = new SnakeSegment { X = 10, Y = 5 }; //create a new head for the snake
            SnakeSegment head2 = new SnakeSegment { X = 5, Y = 10 }; //create a new head for the snake
            Snake.Add(head); //add the gead to the snake array
            Snake2.Add(head2);
            label2.Text = SettingsP1.Score.ToString();
            generateFood();
        }

        private void generateFood()
        {
            int maxXpos = pbCanvas.Size.Width / SettingsP1.Width;
            //create a maximum X position int with half the size of the play area
            int maxYpos = pbCanvas.Size.Height / SettingsP1.Height;
            //create a maximum Y position int with half the size of the play area
            Random rnd = new Random(); //create a new random class
            food = new SnakeSegment { X = rnd.Next(0, maxXpos), Y = rnd.Next(0, maxYpos) };
            // create a new food with a random x and y
        }

        private void eat(List<SnakeSegment> Snake)
        {
            //add a segment to the body
            SnakeSegment body = new SnakeSegment
            {
                X = Snake[Snake.Count - 1].X,
                Y = Snake[Snake.Count - 1].Y,

            };

            Snake.Add(body);
            SettingsP1.Score += SettingsP1.Points;
            label2.Text = SettingsP1.Score.ToString();
            generateFood();

        }

        private void die(int PlayerNo)
        {
            //change the game over Boolean to true
            if (PlayerNo == 1)
            {
                SettingsP1.GameOver = true;
            }
            else if(PlayerNo == 2  )
            {
                SettingsP2.GameOver = true;
            }

        }

        private void dieBySnake()
        {
            //change the game over Boolean to true
            if (Snake.Count > Snake2.Count)
            {
                SettingsP2.GameOver = true;
            }
            else
            {
                SettingsP1.GameOver = true;
            }

        }

        private void keyIsDown(object sender, KeyEventArgs e)
        {
            //the key down event will trigger the change state from the Input Class
            Input.changeState(e.KeyCode, true);

        }

        private void keyIsUp(object sender, KeyEventArgs e)
        {
            //the key up event will trigger the change state from the Input class
            Input.changeState(e.KeyCode, false);
        }

        private void updateGraphics(object sender, PaintEventArgs e)
        {
            //this is where we will see the snake and its parts moving

            Graphics canvas = e.Graphics; // create a new graphic class called canvas
            if (IsGameJustStarted != false)
            {
                                
                

                string game = "PRESS ENTER TO START";
                label3.BackColor = Color.DarkRed;

                
                label3.Text = game;
                label3.Visible = true;
            }
            else if (SettingsP1.GameOver == false && SettingsP2.GameOver == false)
            {
                // if the game is not over then  we do the following

                Brush snakeColour; //create a new brush called snake colour

                Brush snakeColour2; //create a new brush called snake colour

                //run a loop to check the snake parts
                for (int i = 0; i < Snake.Count; i++)
                {
                    snakeColour = Brushes.Blue;

                    //draw snake body and head
                    canvas.FillEllipse(snakeColour, new Rectangle(Snake[i].X * SettingsP1.Width, Snake[i].Y * SettingsP1.Height, SettingsP1.Width, SettingsP1.Height));


                    //draw food
                    canvas.FillEllipse(Brushes.Red, new Rectangle(food.X * SettingsP1.Width, food.Y * SettingsP1.Height, SettingsP1.Width, SettingsP1.Height));
                }
                for (int i = 0; i < Snake2.Count; i++)
                {

                    snakeColour2 = Brushes.Green;


                    //draw snake body and head
                    canvas.FillEllipse(snakeColour2, new Rectangle(Snake2[i].X * SettingsP2.Width, Snake2[i].Y * SettingsP2.Height, SettingsP2.Width, SettingsP2.Height));

                }

            }
            else 
            {
                //this part will run the game is over
                //it will show the game over text and make the label 3 visible on the screen
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

        private void gameTimer_Tick(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
