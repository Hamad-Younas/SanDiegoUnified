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

namespace SanDiegoUnified.Pages
{
    public partial class ReadcheckList : Form
    {
        public ReadcheckList()
        {
            InitializeComponent();
        }

        private void panel6_Click(object sender, EventArgs e)
        {
            Mainform.MyForm.openPage3();
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            Mainform.MyForm.openPage3();
        }
    }
}
