using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SanDiegoUnified.Components;
using Microsoft.Win32;
using SanDiegoUnified.Pages;
using SanDiegoUnified.Models;

namespace SanDiegoUnified
{
    public partial class Form1 : Form
    {
        string keyPath = @"HKEY_CURRENT_USER\Software\OneDriveMigrationTool";
        public Form1()
        {
            InitializeComponent();
            this.Size = new System.Drawing.Size(1000, 550);

            tableLayoutPanel6.Width = label3.Width;
            tableLayoutPanel6.Location = new System.Drawing.Point(panel2.Width-210,panel2.Location.Y - 20);
            tableLayoutPanel6.Hide();
            int des = ReadRegistryDwordValue(keyPath, "MigrationStatusDesktop");
            int doc = ReadRegistryDwordValue(keyPath, "MigrationStatusDocuments");
            int dow = ReadRegistryDwordValue(keyPath, "MigrationStatusDownloads");
            int mus = ReadRegistryDwordValue(keyPath, "MigrationStatusMusic");
            int pic = ReadRegistryDwordValue(keyPath, "MigrationStatusPictures");
            int vid = ReadRegistryDwordValue(keyPath, "MigrationStatusVideos");
            if (des > 1 || doc > 1 || mus > 1 || dow > 1 || pic > 1 || vid > 1)
            {
                openchildform(new folderMigrate());
                estimateTimeLabel();
                
            }
            else
            {
                openchildform(new InstructionsPage());
            }
        }

        // border
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Panel gradientPanel = (Panel)sender;

            Color color1 = ColorTranslator.FromHtml("#00599A");
            Color color2 = ColorTranslator.FromHtml("#E24B26");
            Color color3 = ColorTranslator.FromHtml("#8FAF3E");
            Color color4 = ColorTranslator.FromHtml("#A42672");
            Color color5 = ColorTranslator.FromHtml("#FAAF40");

            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(
                gradientPanel.ClientRectangle,
                color1,
                color2,
                LinearGradientMode.Horizontal);

            e.Graphics.FillRectangle(linearGradientBrush, gradientPanel.ClientRectangle);
        }

        private Form activeform = null;
        public void openchildform(Form ChildForm)
        {
            Thread.BeginThreadAffinity();
            if (activeform != null)
            {
                activeform.Close();
            }
            activeform = ChildForm;
            ChildForm.TopLevel = false;
            ChildForm.FormBorderStyle = FormBorderStyle.None;
            ChildForm.Dock = DockStyle.Fill;
            panel3.Controls.Add(ChildForm);
            panel3.Tag = ChildForm;
            ChildForm.BringToFront();
            ChildForm.Show();
        }
        public void openPage()
        {
            openchildform(new folderMigrate());
            estimateTimeLabel();
        }
        public void estimateTimeLabel() 
        {
            panel4.Controls.Clear();
            panel4.Controls.Remove(label3);
            Label lab = new Label();
            lab.Text = $"Folders to migrate / Est time {EstimateTime.time}min";
            lab.TextAlign = ContentAlignment.MiddleLeft;
            lab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(154)))));
            lab.Font = new Font("Microsoft JhengHei UI", 11); // Set the desired font size (12 is just an example, you can adjust it as needed)
            lab.AutoSize = true; // This ensures that the label automatically adjusts its size to fit the content
            panel4.Controls.Add(lab);
        }
        public void openPage2()
        {
            tableLayoutPanel6.Visible = false;
            panel4.Controls.Clear();
            panel4.Controls.Remove(label3);
            openchildform(new ReadcheckList());
        }
        public void openPage3()
        {
            tableLayoutPanel6.Visible = false;
            panel4.Controls.Add(label3);
            openchildform(new InstructionsPage());
        }
        public void openPage5()
        {
            tableLayoutPanel6.Visible = false;
            panel4.Controls.Clear();
            panel4.Controls.Remove(label3);
            openchildform(new FinishPage());
        }
        public void openPage6()
        {
            tableLayoutPanel6.Visible = false;
            panel4.Controls.Clear();
            panel4.Controls.Remove(label3);
            openchildform(new ErrorPage());
        }
        public void openPage7()
        {
            this.Close();
        }
        public void openPage4()
        {
            this.Close();
        }
        public void disableFirstPage()
        {
            label3.Enabled = false;
            tableLayoutPanel6.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void label3_Click(object sender, EventArgs e)
        {
            if (tableLayoutPanel6.Visible)
            {
                tableLayoutPanel6.Visible = false;
            }
            else
            {
                tableLayoutPanel6.Visible = true;
            }
        }
        private string GetFormattedDate(int nextDays)
        {
            DateTime currentDate = DateTime.Now;
            DateTime nextDay = currentDate.AddDays(nextDays);
            return nextDay.ToString("yyyyMMdd");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string date = GetFormattedDate(1);
            if (UpdateRegistryValue(keyPath, "ReminderDate", date))
            {
                tableLayoutPanel6.Visible = false;
                Components.CustomMessageBox mes = new Components.CustomMessageBox(1, 1);
                mes.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string date = GetFormattedDate(2);
            if (UpdateRegistryValue(keyPath, "ReminderDate", date))
            {
                tableLayoutPanel6.Visible = false;
                Components.CustomMessageBox mes = new Components.CustomMessageBox(2, 1);
                mes.Show();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string date = GetFormattedDate(3);
            if (UpdateRegistryValue(keyPath, "ReminderDate", date))
            {
                tableLayoutPanel6.Visible = false;
                Components.CustomMessageBox mes = new Components.CustomMessageBox(3, 1);
                mes.Show();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string date = GetFormattedDate(5);
            if (UpdateRegistryValue(keyPath, "ReminderDate", date))
            {
                tableLayoutPanel6.Visible = false;
                Components.CustomMessageBox mes = new Components.CustomMessageBox(5,1);
                mes.Show();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string date = GetFormattedDate(4);
            if (UpdateRegistryValue(keyPath, "ReminderDate", date))
            {
                tableLayoutPanel6.Visible = false;
                Components.CustomMessageBox mes = new Components.CustomMessageBox(4,1);
                mes.Show();
            }
        }
        private bool UpdateRegistryValue(string keyPath, string valueName, object newValue)
        {
            try
            {
                Registry.SetValue(keyPath, valueName, newValue);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private int ReadRegistryDwordValue(string keyPath, string valueName)
        {
            try
            {
                object value = Registry.GetValue(keyPath, valueName, null);
                if (value != null)
                {
                    if (value is int dwordValue)
                    {
                        return dwordValue;
                    }
                    else
                    {
                        return -1;
                    }
                }
                return -1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public void HidePanel()
        {
            tableLayoutPanel6.Hide();
        }
    }
}
