namespace CoCarroApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.panelChessBoard = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBoxAvatar = new System.Windows.Forms.PictureBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonLan = new System.Windows.Forms.Button();
            this.pictureBoxMask = new System.Windows.Forms.PictureBox();
            this.textBoxIPPlayer = new System.Windows.Forms.TextBox();
            this.progressBarCountDown = new System.Windows.Forms.ProgressBar();
            this.textBoxPlayerName = new System.Windows.Forms.TextBox();
            this.timerCountDown = new System.Windows.Forms.Timer(this.components);
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvatar)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMask)).BeginInit();
            this.SuspendLayout();
            // 
            // panelChessBoard
            // 
            this.panelChessBoard.BackColor = System.Drawing.SystemColors.Control;
            this.panelChessBoard.Location = new System.Drawing.Point(2, 3);
            this.panelChessBoard.Name = "panelChessBoard";
            this.panelChessBoard.Size = new System.Drawing.Size(601, 510);
            this.panelChessBoard.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.pictureBoxAvatar);
            this.panel2.Location = new System.Drawing.Point(620, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(269, 269);
            this.panel2.TabIndex = 1;
            // 
            // pictureBoxAvatar
            // 
            this.pictureBoxAvatar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxAvatar.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBoxAvatar.Image = global::CoCarroApp.Properties.Resources.co_caro_icon;
            this.pictureBoxAvatar.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxAvatar.Name = "pictureBoxAvatar";
            this.pictureBoxAvatar.Size = new System.Drawing.Size(269, 269);
            this.pictureBoxAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxAvatar.TabIndex = 0;
            this.pictureBoxAvatar.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.buttonLan);
            this.panel3.Controls.Add(this.pictureBoxMask);
            this.panel3.Controls.Add(this.textBoxIPPlayer);
            this.panel3.Controls.Add(this.progressBarCountDown);
            this.panel3.Controls.Add(this.textBoxPlayerName);
            this.panel3.Location = new System.Drawing.Point(620, 279);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(269, 201);
            this.panel3.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MV Boli", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 155);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 28);
            this.label1.TabIndex = 5;
            this.label1.Text = "5 in line to win";
            // 
            // buttonLan
            // 
            this.buttonLan.Location = new System.Drawing.Point(36, 108);
            this.buttonLan.Name = "buttonLan";
            this.buttonLan.Size = new System.Drawing.Size(89, 23);
            this.buttonLan.TabIndex = 4;
            this.buttonLan.Text = "Connect Lan";
            this.buttonLan.UseVisualStyleBackColor = true;
            // 
            // pictureBoxMask
            // 
            this.pictureBoxMask.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pictureBoxMask.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBoxMask.Location = new System.Drawing.Point(143, 15);
            this.pictureBoxMask.Name = "pictureBoxMask";
            this.pictureBoxMask.Size = new System.Drawing.Size(122, 127);
            this.pictureBoxMask.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxMask.TabIndex = 3;
            this.pictureBoxMask.TabStop = false;
            // 
            // textBoxIPPlayer
            // 
            this.textBoxIPPlayer.Location = new System.Drawing.Point(4, 72);
            this.textBoxIPPlayer.Name = "textBoxIPPlayer";
            this.textBoxIPPlayer.Size = new System.Drawing.Size(132, 20);
            this.textBoxIPPlayer.TabIndex = 2;
            this.textBoxIPPlayer.Text = "1203.40593.3";
            // 
            // progressBarCountDown
            // 
            this.progressBarCountDown.Location = new System.Drawing.Point(4, 42);
            this.progressBarCountDown.Name = "progressBarCountDown";
            this.progressBarCountDown.Size = new System.Drawing.Size(132, 23);
            this.progressBarCountDown.TabIndex = 1;
            // 
            // textBoxPlayerName
            // 
            this.textBoxPlayerName.Location = new System.Drawing.Point(4, 15);
            this.textBoxPlayerName.Name = "textBoxPlayerName";
            this.textBoxPlayerName.ReadOnly = true;
            this.textBoxPlayerName.Size = new System.Drawing.Size(132, 20);
            this.textBoxPlayerName.TabIndex = 0;
            // 
            // timerCountDown
            // 
            this.timerCountDown.Tick += new System.EventHandler(this.timerCountDown_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(897, 514);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panelChessBoard);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Game Carro Lan";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvatar)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMask)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Panel panelChessBoard;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBoxAvatar;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonLan;
        private System.Windows.Forms.PictureBox pictureBoxMask;
        private System.Windows.Forms.TextBox textBoxIPPlayer;
        private System.Windows.Forms.ProgressBar progressBarCountDown;
        private System.Windows.Forms.TextBox textBoxPlayerName;
        private System.Windows.Forms.Timer timerCountDown;
    }
}

