using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolitaireDomino
{
    public partial class FormSolitaireDomino : Form
    {
        Game game;

        public FormSolitaireDomino()
        {
            InitializeComponent();

            game = new Game(pictureBox.Size); // Инициализируем игру с размером экрана
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            labelWin.Visible = false;
            labelLoss.Visible = false;

            timer.Start(); // Запускаем таймер
            game.Start();  // Запускаем игру
        }
        
        private void pictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            game.Click(e.X, e.Y);

            switch (game.GetStatusGame)
            {
                case -1:   // Проигрышь
                    labelLoss.Visible = true;
                    break;
                case 0:
                    break;
                case 1:   // Выигрышь
                    labelWin.Visible = true;
                    break;
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            game.Tick();
            pictureBox.Image = game.Refresh();
        }
    }
}
