using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class GameScreen : Form
    {
        private List<SnakeSegment> snakeP1, snakeP2;
        private SnakeSegment food;
        private Boolean IsGameJustStarted { get; set; }
        public GameScreen()
        {
            InitializeComponent();

            snakeP1 = new List<SnakeSegment>();
            snakeP2 = new List<SnakeSegment>();

            food = new SnakeSegment(); //creating a single Circle class called food

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
            // allow commands to be executed as the game is still on
            else
            {
                if (Input.KeyPress(Keys.Right) && SettingsP1.p1Directions != DirectionsP1.Left)
                {
                    SettingsP1.p1Directions = DirectionsP1.Right;
                }
                else if (Input.KeyPress(Keys.Left) && SettingsP1.p1Directions != DirectionsP1.Right)
                {
                    SettingsP1.p1Directions = DirectionsP1.Left;
                }
                else if (Input.KeyPress(Keys.Up) && SettingsP1.p1Directions != DirectionsP1.Down)
                {
                    SettingsP1.p1Directions = DirectionsP1.Up;
                }
                else if (Input.KeyPress(Keys.Down) && SettingsP1.p1Directions != DirectionsP1.Up)
                {
                    SettingsP1.p1Directions = DirectionsP1.Down;
                }

                if (Input.KeyPress(Keys.D) && SettingsP2.p2Directions != DirectionsP2.A)
                {
                    SettingsP2.p2Directions = DirectionsP2.D;
                }
                else if (Input.KeyPress(Keys.A) && SettingsP2.p2Directions != DirectionsP2.D)
                {
                    SettingsP2.p2Directions = DirectionsP2.A;
                }
                else if (Input.KeyPress(Keys.W) && SettingsP2.p2Directions != DirectionsP2.S)
                {
                    SettingsP2.p2Directions = DirectionsP2.W;
                }
                else if (Input.KeyPress(Keys.S) && SettingsP2.p2Directions != DirectionsP2.W)
                {
                    SettingsP2.p2Directions = DirectionsP2.S;
                }

                movePlayer(); //run move player function
            }
            pbCanvas.Invalidate(); //refresh the picture box and update the graphics on it

        }

        private void movePlayer()
        {

            // the main loop for the snakeP1 head and parts
            for (int i = snakeP1.Count - 1 ; i >= 0; i--)
            {
                // if the snakeP1 head is active
                if (i == 0)
                {
                    //move rest of the body according to which way the head is moving
                    switch (SettingsP1.p1Directions)
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


                    //restrict the snakeP1 from living the canvas
                    int maxXpos = pbCanvas.Size.Width / SettingsP1.Width;
                    int maxYpos = pbCanvas.Size.Height / SettingsP1.Height;

                    if (
                        snakeP1[i].X < 0 || snakeP1[i].Y < 0 ||
                        snakeP1[i].X > maxXpos || snakeP1[i].Y > maxYpos

                        )
                    {
                        //end the game is snakeP1 either reaches edge of the canvas
                        die(1);
                    }
                    //detect collision with the body
                    //this loop will check if the snakeP1 had an collisione with other body parts
                    for (int j = 1; j < snakeP1.Count; j++)
                    {
                        if (snakeP1[i].X == snakeP1[j].X && snakeP1[i].Y == snakeP1[j].Y)
                        {
                            //if so we run the die function
                            die(1);
                        }
                    }
                    //detect collision between snakeP1 head and food
                    if (snakeP1[0].X == food.X && snakeP1[0].Y == food.Y)
                    {
                        //if so we run the eat function
                        eat(snakeP1);
                    }
                    //collision between the 2 snakes
                    for (int j = 0; j < snakeP2.Count; j++)
                    {
                        if (snakeP1.Count > snakeP2.Count && snakeP1.Count >= 5)
                        if (snakeP1[i].X == snakeP2[j].X && snakeP1[i].Y == snakeP2[j].Y)
                        {
                            //if so we run the die function
                            dieBySnake();
                        }
                    }



                }
                else
                {
                    //if there are no collision the we continue moving the snakeP1 and its parts
                    snakeP1[i].X = snakeP1[i - 1].X;
                    snakeP1[i].Y = snakeP1[i - 1].Y;

                }
            }

            for (int i = snakeP2.Count - 1; i >= 0; i--)
            {
                // if the snakeP1 head is active
                if (i == 0)
                {

                    switch (SettingsP2.p2Directions)
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

                    //restrict the snakeP1 from living the canvas
                    int maxXpos = pbCanvas.Size.Width / SettingsP1.Width;
                    int maxYpos = pbCanvas.Size.Height / SettingsP1.Height;

                    if (

                        snakeP2[i].X < 0 || snakeP2[i].Y < 0 ||
                        snakeP2[i].X > maxXpos || snakeP2[i].Y > maxYpos
                        )
                    {
                        //end the game is snakeP1 either reaches edge of the canvas
                        die(2);
                    }
                    //detect collision with the body
                    //this loop will check if the snakeP1 had an collisione with other body parts
                    for (int j = 1; j < snakeP2.Count; j++)
                    {
                        if (snakeP2[i].X == snakeP2[j].X && snakeP2[i].Y == snakeP2[j].Y)
                        {
                            //if so we run the die function
                            die(2);
                        }
                    }

                    //detect collision between snakeP1 head and food
                    if (snakeP2[0].X == food.X && snakeP2[0].Y == food.Y)
                    {
                        eat(snakeP2);
                    }
                    
                    //collision between the 2 snakes
                    for (int j = 0; j < snakeP1.Count; j++)
                    {
                        if (snakeP2.Count > snakeP1.Count && snakeP2.Count >= 5)
                        if (snakeP2[i].X == snakeP1[j].X && snakeP2[i].Y == snakeP1[j].Y)
                        {
                            //if so we run the die function
                            dieBySnake();
                        }
                    }


                }
                else
                {
                    //if there are no collision the we continue moving the snakeP1 and its parts
                    snakeP2[i].X = snakeP2[i - 1].X;
                    snakeP2[i].Y = snakeP2[i - 1].Y;
                }
            }
        }

        private void startGame()
        {
            //this is the start game function

            label3.Visible = false; //set label 3 to invisible
            new SettingsP1(); // create a new istance of settings
            new SettingsP2(); // create a new istance of settings
            snakeP1.Clear(); //clear all snakeP1 parts
            snakeP2.Clear(); //clear all snakeP1 parts
            SnakeSegment head = new SnakeSegment { X = 10, Y = 5 }; //create a new head for the snakeP1
            SnakeSegment head2 = new SnakeSegment { X = 5, Y = 10 }; //create a new head for the snakeP1
            snakeP1.Add(head); //add the gead to the snakeP1 array
            snakeP2.Add(head2);
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

        private void eat(List<SnakeSegment> snakeP1)
        {
            //add a segment to the body
            SnakeSegment body = new SnakeSegment
            {
                X = snakeP1[snakeP1.Count - 1].X,
                Y = snakeP1[snakeP1.Count - 1].Y,

            };

            snakeP1.Add(body);
            SettingsP1.Score += SettingsP1.Points;
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
            if (snakeP1.Count > snakeP2.Count)
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
            //this is where we will see the snakeP1 and its parts moving

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

                Brush snakeColour; //create a new brush called snakeP1 colour

                Brush snakeColour2; //create a new brush called snakeP1 colour

                //run a loop to check the snakeP1 parts
                for (int i = 0; i < snakeP1.Count; i++)
                {
                    snakeColour = Brushes.Blue;

                    //draw snakeP1 body and head
                    canvas.FillEllipse(snakeColour, new Rectangle(snakeP1[i].X * SettingsP1.Width, snakeP1[i].Y * SettingsP1.Height, SettingsP1.Width, SettingsP1.Height));


                    //draw food
                    canvas.FillEllipse(Brushes.Red, new Rectangle(food.X * SettingsP1.Width, food.Y * SettingsP1.Height, SettingsP1.Width, SettingsP1.Height));
                }
                for (int i = 0; i < snakeP2.Count; i++)
                {

                    snakeColour2 = Brushes.Green;


                    //draw snakeP1 body and head
                    canvas.FillEllipse(snakeColour2, new Rectangle(snakeP2[i].X * SettingsP2.Width, snakeP2[i].Y * SettingsP2.Height, SettingsP2.Width, SettingsP2.Height));

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
    }
}
