using SanDiegoUnified.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SanDiegoUnified.Components
{
    public partial class ConfirmMessageBox : Form
    {
        public ConfirmMessageBox()
        {
            InitializeComponent();
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MigrationPause.enable = true;
            this.Close();
            CustomMessageBox mes = new CustomMessageBox(0,0);
            mes.Show();
        }
    }
}
