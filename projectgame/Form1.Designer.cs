namespace ProjectGame
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            lblScore = new Label();
            btnstart = new Button();
            btnRestart = new Button();
            gameTimer = new System.Windows.Forms.Timer(components);
            hpBar = new ProgressBar();
            panelStartMenu = new Panel();
            btnEXIT = new Button();
            btnShop = new Button();
            lblMoneyMenu = new Label();
            label1 = new Label();
            lblWave = new Label();
            player = new PictureBox();
            panelShop = new Panel();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            lblShopMoney = new Label();
            btnBuyDamageUp = new Button();
            btnBuyHPBoost = new Button();
            btnBack = new Button();
            panelGameOver = new Panel();
            btnGO_Exit = new Button();
            btnGO_Restart = new Button();
            lblMoney = new Label();
            panelStartMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)player).BeginInit();
            panelShop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            panelGameOver.SuspendLayout();
            SuspendLayout();
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblScore.Location = new Point(126, 83);
            lblScore.Margin = new Padding(4, 0, 4, 0);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(125, 40);
            lblScore.TabIndex = 0;
            lblScore.Text = "Score : 0";
            lblScore.Click += lblScore_Click;
            // 
            // btnstart
            // 
            btnstart.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnstart.Location = new Point(631, 277);
            btnstart.Margin = new Padding(4, 5, 4, 5);
            btnstart.Name = "btnstart";
            btnstart.Size = new Size(160, 92);
            btnstart.TabIndex = 1;
            btnstart.Text = "Start";
            btnstart.UseVisualStyleBackColor = true;
            btnstart.Click += button1_Click;
            // 
            // btnRestart
            // 
            btnRestart.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRestart.Location = new Point(616, 448);
            btnRestart.Margin = new Padding(4, 5, 4, 5);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(160, 92);
            btnRestart.TabIndex = 2;
            btnRestart.Text = "Restart";
            btnRestart.UseVisualStyleBackColor = true;
            btnRestart.Click += btnRestart_Click;
            // 
            // gameTimer
            // 
            gameTimer.Enabled = true;
            gameTimer.Interval = 20;
            gameTimer.Tick += gameTimer_Tick;
            // 
            // hpBar
            // 
            hpBar.Location = new Point(1037, 83);
            hpBar.Margin = new Padding(4, 5, 4, 5);
            hpBar.Name = "hpBar";
            hpBar.Size = new Size(229, 42);
            hpBar.Style = ProgressBarStyle.Continuous;
            hpBar.TabIndex = 3;
            hpBar.Value = 100;
            // 
            // panelStartMenu
            // 
            panelStartMenu.BackColor = SystemColors.ActiveCaption;
            panelStartMenu.Controls.Add(btnstart);
            panelStartMenu.Controls.Add(btnEXIT);
            panelStartMenu.Controls.Add(btnShop);
            panelStartMenu.Controls.Add(lblMoneyMenu);
            panelStartMenu.Controls.Add(label1);
            panelStartMenu.Dock = DockStyle.Fill;
            panelStartMenu.Location = new Point(0, 0);
            panelStartMenu.Margin = new Padding(4, 5, 4, 5);
            panelStartMenu.Name = "panelStartMenu";
            panelStartMenu.Size = new Size(1377, 1027);
            panelStartMenu.TabIndex = 5;
            panelStartMenu.Paint += panelStartMenu_Paint;
            // 
            // btnEXIT
            // 
            btnEXIT.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEXIT.Location = new Point(631, 550);
            btnEXIT.Margin = new Padding(4, 5, 4, 5);
            btnEXIT.Name = "btnEXIT";
            btnEXIT.Size = new Size(160, 92);
            btnEXIT.TabIndex = 6;
            btnEXIT.Text = "EXIT";
            btnEXIT.UseVisualStyleBackColor = true;
            btnEXIT.Click += btnEXIT_Click;
            // 
            // btnShop
            // 
            btnShop.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnShop.Location = new Point(631, 418);
            btnShop.Margin = new Padding(4, 5, 4, 5);
            btnShop.Name = "btnShop";
            btnShop.Size = new Size(160, 92);
            btnShop.TabIndex = 5;
            btnShop.Text = "Shop";
            btnShop.UseVisualStyleBackColor = true;
            btnShop.Click += btnShop_Click;
            // 
            // lblMoneyMenu
            // 
            lblMoneyMenu.AutoSize = true;
            lblMoneyMenu.Font = new Font("Segoe UI", 26.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblMoneyMenu.Location = new Point(30, 47);
            lblMoneyMenu.Margin = new Padding(4, 0, 4, 0);
            lblMoneyMenu.Name = "lblMoneyMenu";
            lblMoneyMenu.Size = new Size(248, 71);
            lblMoneyMenu.TabIndex = 3;
            lblMoneyMenu.Text = "Money: 0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Impact", 36F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(524, 83);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(388, 87);
            label1.TabIndex = 2;
            label1.Text = "Space Wars";
            // 
            // lblWave
            // 
            lblWave.AutoSize = true;
            lblWave.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblWave.Location = new Point(544, 67);
            lblWave.Margin = new Padding(4, 0, 4, 0);
            lblWave.Name = "lblWave";
            lblWave.Size = new Size(107, 48);
            lblWave.TabIndex = 7;
            lblWave.Text = "Wave";
            // 
            // player
            // 
            player.BackColor = Color.White;
            player.BackgroundImage = Properties.Resources.download_removebg_preview;
            player.BackgroundImageLayout = ImageLayout.Stretch;
            player.Location = new Point(616, 810);
            player.Margin = new Padding(4, 5, 4, 5);
            player.Name = "player";
            player.Size = new Size(191, 217);
            player.SizeMode = PictureBoxSizeMode.StretchImage;
            player.TabIndex = 7;
            player.TabStop = false;
            // 
            // panelShop
            // 
            panelShop.BackColor = SystemColors.ActiveCaption;
            panelShop.Controls.Add(pictureBox1);
            panelShop.Controls.Add(pictureBox2);
            panelShop.Controls.Add(lblShopMoney);
            panelShop.Controls.Add(btnBuyDamageUp);
            panelShop.Controls.Add(btnBuyHPBoost);
            panelShop.Controls.Add(btnBack);
            panelShop.Dock = DockStyle.Fill;
            panelShop.Location = new Point(0, 0);
            panelShop.Margin = new Padding(4, 5, 4, 5);
            panelShop.Name = "panelShop";
            panelShop.Size = new Size(1377, 1027);
            panelShop.TabIndex = 4;
            panelShop.TabStop = true;
            panelShop.Visible = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = SystemColors.ActiveCaption;
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Location = new Point(480, 337);
            pictureBox1.Margin = new Padding(4, 5, 4, 5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(143, 83);
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackgroundImage = (Image)resources.GetObject("pictureBox2.BackgroundImage");
            pictureBox2.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox2.Location = new Point(788, 337);
            pictureBox2.Margin = new Padding(4, 5, 4, 5);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(143, 83);
            pictureBox2.TabIndex = 8;
            pictureBox2.TabStop = false;
            // 
            // lblShopMoney
            // 
            lblShopMoney.AutoSize = true;
            lblShopMoney.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblShopMoney.Location = new Point(1147, 57);
            lblShopMoney.Margin = new Padding(4, 0, 4, 0);
            lblShopMoney.Name = "lblShopMoney";
            lblShopMoney.Size = new Size(115, 48);
            lblShopMoney.TabIndex = 4;
            lblShopMoney.Text = "label2";
            // 
            // btnBuyDamageUp
            // 
            btnBuyDamageUp.Location = new Point(809, 453);
            btnBuyDamageUp.Margin = new Padding(4, 5, 4, 5);
            btnBuyDamageUp.Name = "btnBuyDamageUp";
            btnBuyDamageUp.Size = new Size(107, 38);
            btnBuyDamageUp.TabIndex = 3;
            btnBuyDamageUp.Text = "buy";
            btnBuyDamageUp.UseVisualStyleBackColor = true;
            btnBuyDamageUp.Click += btnBuyDamageUp_Click;
            // 
            // btnBuyHPBoost
            // 
            btnBuyHPBoost.Location = new Point(493, 453);
            btnBuyHPBoost.Margin = new Padding(4, 5, 4, 5);
            btnBuyHPBoost.Name = "btnBuyHPBoost";
            btnBuyHPBoost.Size = new Size(107, 38);
            btnBuyHPBoost.TabIndex = 2;
            btnBuyHPBoost.Text = "buy";
            btnBuyHPBoost.UseVisualStyleBackColor = true;
            btnBuyHPBoost.Click += btnBuyHPBoost_Click_1;
            // 
            // btnBack
            // 
            btnBack.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBack.Location = new Point(79, 75);
            btnBack.Margin = new Padding(4, 5, 4, 5);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(131, 77);
            btnBack.TabIndex = 1;
            btnBack.Text = "Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // panelGameOver
            // 
            panelGameOver.BackColor = SystemColors.ActiveCaption;
            panelGameOver.Controls.Add(btnGO_Exit);
            panelGameOver.Controls.Add(btnGO_Restart);
            panelGameOver.Dock = DockStyle.Fill;
            panelGameOver.Location = new Point(0, 0);
            panelGameOver.Margin = new Padding(4, 5, 4, 5);
            panelGameOver.Name = "panelGameOver";
            panelGameOver.Size = new Size(1377, 1027);
            panelGameOver.TabIndex = 5;
            // 
            // btnGO_Exit
            // 
            btnGO_Exit.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnGO_Exit.Location = new Point(827, 347);
            btnGO_Exit.Margin = new Padding(4, 5, 4, 5);
            btnGO_Exit.Name = "btnGO_Exit";
            btnGO_Exit.Size = new Size(201, 163);
            btnGO_Exit.TabIndex = 1;
            btnGO_Exit.Text = "EXIT";
            btnGO_Exit.UseVisualStyleBackColor = true;
            btnGO_Exit.Click += btnGO_Exit_Click;
            // 
            // btnGO_Restart
            // 
            btnGO_Restart.BackColor = Color.Transparent;
            btnGO_Restart.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnGO_Restart.Location = new Point(380, 347);
            btnGO_Restart.Margin = new Padding(4, 5, 4, 5);
            btnGO_Restart.Name = "btnGO_Restart";
            btnGO_Restart.Size = new Size(201, 163);
            btnGO_Restart.TabIndex = 0;
            btnGO_Restart.Text = "Restart";
            btnGO_Restart.UseVisualStyleBackColor = false;
            btnGO_Restart.Click += btnGO_Restart_Click;
            // 
            // lblMoney
            // 
            lblMoney.AutoSize = true;
            lblMoney.BackColor = Color.Transparent;
            lblMoney.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblMoney.ForeColor = Color.Yellow;
            lblMoney.Location = new Point(1217, 130);
            lblMoney.Margin = new Padding(4, 0, 4, 0);
            lblMoney.Name = "lblMoney";
            lblMoney.Size = new Size(146, 40);
            lblMoney.TabIndex = 6;
            lblMoney.Text = "Money: 0";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1377, 1027);
            Controls.Add(lblWave);
            Controls.Add(player);
            Controls.Add(lblMoney);
            Controls.Add(panelStartMenu);
            Controls.Add(panelShop);
            Controls.Add(panelGameOver);
            Controls.Add(hpBar);
            Controls.Add(btnRestart);
            Controls.Add(lblScore);
            Margin = new Padding(4, 5, 4, 5);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            panelStartMenu.ResumeLayout(false);
            panelStartMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)player).EndInit();
            panelShop.ResumeLayout(false);
            panelShop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            panelGameOver.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblScore;
        private Button btnstart;
        private Button btnRestart;
        private System.Windows.Forms.Timer gameTimer;
        private ProgressBar hpBar;
        private Panel panelStartMenu;
        private Label label1;
        private Label lblMoney;
        private Label lblMoneyMenu;
        private Button btnShop;
        private Panel panelShop;
        private Button btnEXIT;
        private Button btnBuyHPBoost;
        private Button btnBack;
        private Button btnBuyDamageUp;
        private Label lblShopMoney;
        private Panel panelGameOver;
        private Button btnGO_Exit;
        private Button btnGO_Restart;
        private Label lblWave;
        private PictureBox player;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
    }
}
