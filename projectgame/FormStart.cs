using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectGame
{
    public partial class FormStart : Form
    {
        public FormStart()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // พื้นหลังเมนู
            this.BackgroundImage = Image.FromFile("menu_bg.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void FormStart_Load(object sender, EventArgs e)
        {

        }
        private void btnPlay_Click_1(object sender, EventArgs e)
        {
            Form1 game = new Form1();
            game.Show();
            this.Hide();
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
