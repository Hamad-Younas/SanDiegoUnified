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
    public partial class CustomMessageBox : Form
    {
        int days = 0;
        int page = 0;
        public CustomMessageBox(int day,int page)
        {
            InitializeComponent();
            this.days = day;
            this.page = page;
        }

        private void MessageBox_Load(object sender, EventArgs e)
        {
            if (page == 1)
            {
                if (days == 1)
                {
                    label1.Text = $"We will remind you after {days} day";
                }
                else
                {
                    label1.Text = $"We will remind you after {days} days";
                }
            }
            else
            {
                label1.Text = $"We can pause after the current folder finishes";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            Mainform.MyForm.openPage7();
        }
    }
}
