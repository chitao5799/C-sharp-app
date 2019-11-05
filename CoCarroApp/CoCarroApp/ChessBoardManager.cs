using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoCarroApp
{
    public class ChessBoardManager
    {
        #region properties
        private Panel chessBoard;
        public Panel ChessBoard
        {
            get { return chessBoard; }
            set { chessBoard = value; } 
        }

        public List<player> Players { get => players; set => players = value; }
        public int IndexCurrentPlayer1 { get => IndexCurrentPlayer; set => IndexCurrentPlayer = value; }
        public TextBox NamePlayer { get => namePlayer; set => namePlayer = value; }
        public PictureBox MarkPlayer { get => markPlayer; set => markPlayer = value; }
        public List<List<Button>> Matric { get => matric; set => matric = value; }
        public Stack<PlayInfo> PlayTimeLine { get => playTimeLine; set => playTimeLine = value; }

        private List<player> players;
        private int IndexCurrentPlayer;
        private TextBox namePlayer;
        private PictureBox markPlayer;
        private List<List<Button>> matric;
        private event EventHandler<ButtonClickEvent> playerMarked;//playerMarked như kiểu biến hàm giống như delegate

        public event EventHandler<ButtonClickEvent> PlayerMarked //PlayerMarked cái này như kiểu queue(lúc đầu là null)
        {
            add  //add như là set
            {
                playerMarked += value; //value như là cái hàm ủy thác đưa vào cái PlayerMarked
            }
            remove  //remove như là get
            {
                playerMarked -= value;
            }
        }

        private event EventHandler endedGame;
        public event EventHandler EndedGame
        {
            add
            {
                endedGame += value;
            }
            remove
            {
                endedGame -= value;
            }
        }
        private Stack<PlayInfo> playTimeLine;
        #endregion

        #region initialize
        public ChessBoardManager(Panel chessBoard,TextBox namePlayer, PictureBox markPlayer)
        {
            this.chessBoard = chessBoard;//this.ChessBoard = chessBoard;-cũng được
            this.NamePlayer = namePlayer;
            this.MarkPlayer = markPlayer;
            this.Players = new List<player>()
            {
                new player("xyz",Image.FromFile(Application.StartupPath + "\\Resources\\x.png")),
                new player("obc",Image.FromFile(Application.StartupPath + "\\Resources\\o.png"))
            };

            
        }
        #endregion

        #region methods
        public void DrawChessBoard()
        {
            chessBoard.Enabled = true;
            ChessBoard.Controls.Clear();
            PlayTimeLine = new Stack<PlayInfo>();//khởi tạo ơ đây để khi new game thì stack sẽ khởi tạo lại
            IndexCurrentPlayer1 = 0;  //IndexCurrentPlayer1 là method khởi tạo của chỉ số người chơi.
            changePlayer();
            //MessageBox.Show("đã vẽ bàn cờ");
            //Button bt1 = new Button();
            //bt1.Text = "1";
            //panelChessBoard.Controls.Add(bt1);

            //Button bt2 = new Button() { Text = "2" ,Location =new Point(bt1.Location.X+bt1.Width,bt1.Location.Y)};

            //panelChessBoard.Controls.Add(bt2);
            Matric = new List<List<Button>>();

            Button oldButton = new Button() { Width = 0, Location = new Point(0, 0) };
            for (int i = 0; i < constant.Chess_Board_height; i++)
            {
                Matric.Add(new List<Button>());
                for (int j = 0; j < constant.Chess_Board_width; j++)
                {
                    Button bt = new Button()
                    {
                        Width = constant.Chess_Width,
                        Height = constant.Chess_height,
                        Location = new Point(oldButton.Location.X + oldButton.Width, oldButton.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch,
                        Tag =i.ToString()

                    };

                    bt.Click += bt_click;

                    ChessBoard.Controls.Add(bt);
                    Matric[i].Add(bt);
                    oldButton = bt;
                }

                oldButton.Location = new Point(0, oldButton.Location.Y + constant.Chess_height);
                oldButton.Height = 0;
                oldButton.Width = 0;

            }
        }

        private void bt_click(object sender, EventArgs e)
        {
            Button btn = sender as Button;//ép kiểu sender sang kiểu button,sender là button được nhấn
            if (btn.BackgroundImage != null)
                return;
            changeMark(btn);
            PlayTimeLine.Push(new PlayInfo( getChessPoint(btn),IndexCurrentPlayer));
            IndexCurrentPlayer1 = IndexCurrentPlayer1 == 1 ? 0 : 1;
            changePlayer();
            if (playerMarked != null)
                playerMarked(this, new ButtonClickEvent(getChessPoint(btn))); //khởi tạo event
            /*khi gọi PlayerMarked += Chessboard_playerMarked; bên file form1.cs thì playerMarked khác null và khi gọi
            playerMarked(this, new EventArgs()); tương ứng với button mới được click lần đầu thì phương 
            thức Chessboard_playerMarked bên form1.cs sẽ được gọi VÌ hàm tạo event này được đặt trong phương thức bt_click này*/

            if(isEndGame(btn))
            {
                EndGame();

            }         
        }
        public void OtherPlayerMark(Point point)
        {
            Button btn =Matric[point.Y][point.X];
            if (btn.BackgroundImage != null)
                return;
           // chessBoard.Enabled = true;
            changeMark(btn);
            PlayTimeLine.Push(new PlayInfo(getChessPoint(btn), IndexCurrentPlayer));
            IndexCurrentPlayer1 = IndexCurrentPlayer1 == 1 ? 0 : 1;
            changePlayer();
           
            if (isEndGame(btn))
            {
                EndGame();

            }
        }
        public bool Undo()
        {
            if (PlayTimeLine.Count <= 0)
                return false;
            bool undo1 = undo1Step();
            bool undo2=undo1Step();
            IndexCurrentPlayer1 = PlayTimeLine.Peek().IndexCurrentPlayer == 1 ? 0 : 1;
            return undo1 && undo2;//undo 2 lần vì 2 người chơi.
        }
        private bool undo1Step()
        {
            if (PlayTimeLine.Count <= 0)
                return false;
            PlayInfo oldPoint = PlayTimeLine.Pop();  //pop lấy ra luôn khỏi stack
            Button oldButt = Matric[oldPoint.Point.Y][oldPoint.Point.X];
            oldButt.BackgroundImage = null;
            //IndexCurrentPlayer1 = PlayTimeLine.Peek().IndexCurrentPlayer==1?0:1; //peek lấy ra xem nhưng trong stack vẫn còn
            if (PlayTimeLine.Count <= 0)
                IndexCurrentPlayer1 = 0;
            else
            {
                
            }

            changePlayer();
            return true;
        }
        public void EndGame()
        {
            //MessageBox.Show("kết thúc game.");
            if (endedGame != null)
                endedGame(this, new EventArgs());//sẽ chạy vào hàm Chessboard_EndedGame trong file form1.cs
        }

        private bool isEndGame(Button butt)
        {
            return isEndHorizontal(butt) || isEndVertical(butt) || isEndPrimerryDiagonal(butt) || isEndSubDiagonal(butt);
        }

        private Point getChessPoint(Button btn)
        {
            
            int vertical = Convert.ToInt32(btn.Tag);
            int horizontal = Matric[vertical].IndexOf(btn);
            Point point = new Point(horizontal,vertical);//point(x,y)
            return point;
        }
        private bool isEndHorizontal(Button butt)
        {
            Point point = getChessPoint(butt);
            int countLeft = 0;
            for(int i = point.X; i >= 0; i--)
            {
                if (Matric[point.Y][i].BackgroundImage == butt.BackgroundImage)
                {
                    countLeft++;

                }
                else break;
            }
            int countRight = 0;
            for (int i = point.X+1; i <constant.Chess_Board_width; i++)
            {
                if (Matric[point.Y][i].BackgroundImage == butt.BackgroundImage)
                {
                    countRight++;

                }
                else break;
            }
            return countLeft+countRight>=5;
        }
        private bool isEndVertical(Button butt)
        {
            Point point = getChessPoint(butt);
            int countTop = 0;
            for (int i = point.Y; i >= 0; i--)
            {
                if (Matric[i][point.X].BackgroundImage == butt.BackgroundImage)
                {
                    countTop++;

                }
                else break;
            }
            int countBotton = 0;
            for (int i = point.Y + 1; i < constant.Chess_Board_height; i++)
            {
                if (Matric[i][point.X].BackgroundImage == butt.BackgroundImage)
                {
                    countBotton++;

                }
                else break;
            }
            return countBotton + countTop >= 5;
            
        }
        private bool isEndPrimerryDiagonal(Button butt)
        {
            Point point = getChessPoint(butt);
            int countTop = 0;
            for (int i =0; i <=point.X; i++)
            {
                if (point.X - i < 0 || point.Y - i < 0)
                    break;
                if (Matric[point.Y-i][point.X-i].BackgroundImage == butt.BackgroundImage)
                {
                    countTop++;

                }
                else break;
            }
            int countBotton = 0;
            for (int i = 1; i <=constant.Chess_Board_width; i++)
            {
                if (point.Y + i >= constant.Chess_Board_height || point.X + i >= constant.Chess_Board_width)
                    break;
                if (Matric[point.Y+i][point.X+i].BackgroundImage == butt.BackgroundImage)
                {
                    countBotton++;

                }
                else break;
            }
            return countBotton + countTop >= 5;

        }
        private bool isEndSubDiagonal(Button butt)
        {
            Point point = getChessPoint(butt);
            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.X +i >constant.Chess_Board_width|| point.Y - i < 0)
                    break;
                if (Matric[point.Y - i][point.X + i].BackgroundImage == butt.BackgroundImage)
                {
                    countTop++;

                }
                else break;
            }
            int countBotton = 0;
            for (int i = 1; i <= constant.Chess_Board_width; i++)
            {
                if (point.Y + i >= constant.Chess_Board_height || point.X -i<0)
                    break;
                if (Matric[point.Y + i][point.X - i].BackgroundImage == butt.BackgroundImage)
                {
                    countBotton++;

                }
                else break;
            }
            return countBotton + countTop >= 5;
        }


        public void changeMark(Button btn)
        {
            btn.BackgroundImage = Players[IndexCurrentPlayer1].Mark;
            
        }
        public void changePlayer()
        {
            NamePlayer.Text = Players[IndexCurrentPlayer1].NamePlayer;
            MarkPlayer.Image = Players[IndexCurrentPlayer1].Mark;
        }
        #endregion

    }
    public class ButtonClickEvent : EventArgs
    {
        private Point clickedPoint;//lưu lại tọa độ vừa được click

        public Point ClickedPoint { get => clickedPoint; set => clickedPoint = value; }
        public ButtonClickEvent(Point point)
        {
            this.ClickedPoint = point;
        }
    }
}
