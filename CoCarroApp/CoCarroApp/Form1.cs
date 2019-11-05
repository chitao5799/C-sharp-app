using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoCarroApp
{
    public partial class Form1 : Form
    {
        #region properties
        ChessBoardManager chessboard;
        #endregion
        public Form1()
        {
            InitializeComponent();
            chessboard = new ChessBoardManager(panelChessBoard,textBoxPlayerName,pictureBoxMask);
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
            MessageBox.Show("kết thúc game.");
        }

        private void Chessboard_playerMarked(object sender, EventArgs e)//sender là đối tượng gây ra sự kiện
        {
           
            timerCountDown.Start();
            progressBarCountDown.Value = 0;
            //Console.WriteLine(sender.GetType().ToString());//để xem đối tượng nào được truyền vào.
           
            
        }

        private void Chessboard_EndedGame(object sender, EventArgs e)
        {
            //MessageBox.Show("chạy vào hàm chessboard-endedgame.");
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
        }
        void quit()
        {
                 Application.Exit();
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGame();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            undo();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("bạn có muốn thoát game", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
                e.Cancel = true;
        }
    }
}
