using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static CoCarroApp.SocketData;

namespace CoCarroApp
{
    public partial class Form1 : Form
    {
        #region properties
        ChessBoardManager chessboard;
        ShocketManager socket;
        #endregion
        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false; //ko check nếu phần mềm khác chiếm tài nguyên để dùng
            chessboard = new ChessBoardManager(panelChessBoard,textBoxPlayerName,pictureBoxMask);
            socket = new ShocketManager();
            //truyền các tên biến của các đối tượng trong design vào new ChessBoardManager thì khi các biến đối tượng của ChessBoardManager
            //thay đổi thì các giá trị của các biến design cũng thay đổi theo.
            chessboard.EndedGame += Chessboard_EndedGame;  //ứng dụng event
            chessboard.PlayerMarked += Chessboard_playerMarked;//nếu ko có PlayerMarked += Chessboard_playerMarked thì PlayerMarked sẽ =null
            //chessboard.PlayerMarked -= Chessboard_playerMarked; //nếu dùng câu lệnh này thì PlayerMarked sẽ =null

            progressBarCountDown.Step = constant.count_down_step;
            progressBarCountDown.Maximum = constant.count_down_time;
            progressBarCountDown.Value = 0;

            timerCountDown.Interval = constant.count_down_interval;

            //chessboard.DrawChessBoard();
            newGame();
            //timerCountDown.Start();
        }
        void endgame()
        {
            timerCountDown.Stop();
            panelChessBoard.Enabled = false;
            undoToolStripMenuItem.Enabled = false;//kết thúc game rồi thì nút undo trong menu ko bấm đc nữa
            //MessageBox.Show("kết thúc game.");
        }

        private void Chessboard_playerMarked(object sender, ButtonClickEvent e)//sender là đối tượng gây ra sự kiện
        {
           
            timerCountDown.Start();
            panelChessBoard.Enabled = false;
            progressBarCountDown.Value = 0;
            //Console.WriteLine(sender.GetType().ToString());//để xem đối tượng nào được truyền vào.

            socket.Send(new SocketData((int)SocketCommand.SEND_POINT,"",e.ClickedPoint));
            undoToolStripMenuItem.Enabled = false;
            Listen();
        }

        private void Chessboard_EndedGame(object sender, EventArgs e)
        {
            //MessageBox.Show("chạy vào hàm chessboard-endedgame.");
            socket.Send(new SocketData((int)SocketCommand.END_GAME, "", new Point()));
            endgame();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timerCountDown_Tick(object sender, EventArgs e)//tich là sau 1 khoảng interval thì lại chạy hàm này
        {
            progressBarCountDown.PerformStep();/*mỗi lần câu lệnh này chạy thì thanh processbar sẽ tăng lên 1 chút,
            số lượng tăng tùy thuộc vào độ dài của thời gian mỗi lần tăng và tỉ lệ với khoảng từ minium đến maximum*/

            if (progressBarCountDown.Value >= progressBarCountDown.Maximum)
            {
                socket.Send(new SocketData((int)SocketCommand.TIME_OUT, "", new Point()));
                endgame();
            }
        }
        void newGame()
        {
            progressBarCountDown.Value = 0;
            timerCountDown.Stop();
            undoToolStripMenuItem.Enabled = true;
            chessboard.DrawChessBoard();
           
        }
        void undo()
        {
            chessboard.Undo();
            progressBarCountDown.Value = 0;
        }
        void quit()
        {
                 Application.Exit();
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            socket.Send(new SocketData((int)SocketCommand.NEW_GAME, "", new Point()));
            newGame();
            panelChessBoard.Enabled = true;//khi kết nối mạng lan, bên nào new game thì đc đánh.
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            socket.Send(new SocketData((int)SocketCommand.UNDO, "", new Point()));
            undo();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("bạn có muốn thoát game", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            { e.Cancel = true; }
            else
            {
                try
                {
                    socket.Send(new SocketData((int)SocketCommand.QUIT, "", new Point()));
                }
                catch
                {

                }
                
            }
        }

        private void buttonLan_Click(object sender, EventArgs e)
        {
            socket.myIP = textBoxIPPlayer.Text;
            if (!socket.ConnectServer())  //server
            {
                socket.isServer = true;
                panelChessBoard.Enabled = true;
                socket.CreateServer();

            }
            else  //client
            {
                socket.isServer = false;
                panelChessBoard.Enabled = false;
                Listen();
          
            }
             
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            textBoxIPPlayer.Text = socket.GetLocalIPv4(System.Net.NetworkInformation.NetworkInterfaceType.Wireless80211);
            if (string.IsNullOrEmpty(textBoxIPPlayer.Text))
                textBoxIPPlayer.Text = socket.GetLocalIPv4(System.Net.NetworkInformation.NetworkInterfaceType.Ethernet);
          
           // MessageBox.Show("ipla:" + textBoxIPPlayer.Text);
        }
        private void Listen()
        {
           
                Thread threadListen = new Thread(() => {
                    try { 
                        SocketData data = (SocketData)socket.Receive();
                        ProcessData(data);
                    }
                    catch
                    {

                    }
                });
                threadListen.IsBackground = true;
                threadListen.Start();
           
            
           // MessageBox.Show(data);
        }
        private void ProcessData(SocketData socketData)
        {
            switch (socketData.Command)
            {
                case (int)SocketCommand.NOTIFY:
                    MessageBox.Show(socketData.Message);
                    break;
                case (int)SocketCommand.NEW_GAME:
                    this.Invoke((MethodInvoker)(() => {
                      newGame();
                        panelChessBoard.Enabled = false;//tránh trường hợp new game cả 2 bên đều được đánh
                    }));
                    break;
                case (int)SocketCommand.QUIT:
                    timerCountDown.Stop();
                    MessageBox.Show("người chơi bên kia đã thoát.");
                    break;
                case (int)SocketCommand.END_GAME:
                    MessageBox.Show("người chơi bên kia đã thắng.");
                    break;
                case (int)SocketCommand.TIME_OUT:
                    MessageBox.Show("hết giờ.");
                    break;
                case (int)SocketCommand.SEND_POINT:
                    this.Invoke((MethodInvoker)(()=>{  //invoke thay đổi giao diện muốn chạy ngọt nên trong invoke.
                        progressBarCountDown.Value = 0;
                        panelChessBoard.Enabled = true; 
                        //lệnh trên lỗi cross-thread operation khi chạy các luồng khác nhau.sửa ở trên hàm tạo form1 là Control.CheckForIllegalCrossThreadCalls = false;
                        timerCountDown.Start(); //cái processbar nằm trong luồng khác nên mấy cái này đặt trong invoke
                        chessboard.OtherPlayerMark(socketData.Point);
                        undoToolStripMenuItem.Enabled = true;
                    }));
                   
                    break;
                case (int)SocketCommand.UNDO:
                    undo();
                    progressBarCountDown.Value = 0;
                    break;
            }
            Listen();
        }
    }
}
