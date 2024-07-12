using SanDiegoUnified.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Microsoft.Win32;
using SanDiegoUnified.Components;

namespace SanDiegoUnified.Pages
{
    public partial class InstructionsPage : Form
    {
        string regFilePath1 = "";
        string regFilePath2 = "";
        string regFullpath = @"HKEY_CURRENT_USER\Software\OneDriveMigrationTool";
        public InstructionsPage()
        {
            InitializeComponent();
        }

        private void InstructionsPage_Load(object sender, EventArgs e)
        {
            //regFilePath1 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Registry/OneDriveMigrationTool InvData.reg");
            //regFilePath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Registry/OneDriveMigrationTool InvData.reg");
            //if (!RegistryKeyExists(@"Software\OneDriveMigrationTool"))
            //{
            //    RunRegeditSilently(regFilePath1);
            //}

            RegistryValue.regDesValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusDesktop");
            RegistryValue.regDocValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusDocuments");
            RegistryValue.regPicValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusPictures");
            RegistryValue.regVidValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusVideos");
            RegistryValue.regDowValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusDownloads");
            RegistryValue.regMusValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusMusic");
            RegistryValue.regVidFilesValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusVideoFiles");
            RegistryValue.regAudFilesValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusAudioFiles");
            RegistryValue.regMisFilesValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusMiscFiles");

            if (RegistryValue.regDesValue == -1)
            {
                WriteRegistryDwordValue(regFullpath, "MigrationStatusDesktop", 0);
            }
            if (RegistryValue.regDocValue == -1)
            {
                WriteRegistryDwordValue(regFullpath, "MigrationStatusDocuments", 0);
            }
            if (RegistryValue.regPicValue == -1)
            {
                WriteRegistryDwordValue(regFullpath, "MigrationStatusPictures", 0);
            }
            if (RegistryValue.regVidValue == -1)
            {
                WriteRegistryDwordValue(regFullpath, "MigrationStatusVideos", 0);
            }
            if (RegistryValue.regDowValue == -1)
            {
                WriteRegistryDwordValue(regFullpath, "MigrationStatusDownloads", 0);
            }
            if (RegistryValue.regMusValue == -1)
            {
                WriteRegistryDwordValue(regFullpath, "MigrationStatusMusic", 0);
            }
            if (RegistryValue.regVidFilesValue == -1)
            {
                WriteRegistryDwordValue(regFullpath, "MigrationStatusVideoFiles", 0);
            }
            if (RegistryValue.regAudFilesValue == -1)
            {
                WriteRegistryDwordValue(regFullpath, "MigrationStatusAudioFiles", 0);
            }
            if (RegistryValue.regMisFilesValue == -1)
            {
                WriteRegistryDwordValue(regFullpath, "MigrationStatusMiscFiles", 0);
            }
            

            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile("Fonts/Khula/Khula-Bold.ttf");
            if (pfc.Families.Length > 0)
            {
                Font customFont = new Font(pfc.Families[0], 20, FontStyle.Regular);
                label2.Font = customFont;
                label3.Font = customFont;
                label4.Font = customFont;
                Font customFont1 = new Font(pfc.Families[0], 12, FontStyle.Regular);
                tableLayoutPanel3.Font = customFont1;
            }
            //diff. color
            Color color1 = ColorTranslator.FromHtml("#7D706C");  // Color for "Migrate your files"
            Color color2 = ColorTranslator.FromHtml("#00599A");  // Color for "to OneDrive"
            label2.ForeColor = color1;
            label2.Text = "Migrate your files";
            label3.ForeColor = color1;
            label3.Text = "to";
            label3.AutoSize = true;
            label4.ForeColor = color2;
            label4.Text = "Onedrive";

            //list text
            Color color3 = ColorTranslator.FromHtml("#00599A");  // Color for "Migrate your files"
            Color color4 = ColorTranslator.FromHtml("#7D706C");  // Color for "to OneDrive"
                                                                 // Assuming you have a RichTextBox named richTextBox1
            label5.ForeColor = color3;
            label5.Text = "Ease of Access: ";
            label6.ForeColor = color4;
            label6.Text = "Accessible from anywhere,";
            label7.ForeColor = color4;
            label7.Text = ("anytime, on any device, without the need for VPN.");

            label9.ForeColor = color3;
            label9.Text = ("Simplified Sharing: ");
            label8.ForeColor = color4;
            label8.Text = ("Effortless and instant file");
            label10.ForeColor = color4;
            label10.Text = ("sharing and collaboration.");

            label12.ForeColor = color3;
            label12.Text = ("Enhanced Security: ");
            label11.ForeColor = color4;
            label11.Text = ("Improved safety with");
            label13.ForeColor = color4;
            label13.Text = ("automatic backups and recovery options.");

            label15.ForeColor = color3;
            label15.Text = ("Work Offline: ");
            label14.ForeColor = color4;
            label14.Text = ("Edit documents offline and");
            label16.ForeColor = color4;
            label16.Text = ("changes automatically sync when back online.");

            label18.ForeColor = color3;
            label18.Text = ("Files on Demand: ");
            label17.ForeColor = color4;
            label17.Text = ("Smart technology that");
            label19.ForeColor = color4;
            label19.Text = ("offloads less frequently used large files to free up");
            label20.ForeColor = color4;
            label20.Text = ("space and enhance system performance.");
        }
        private bool WriteRegistryDwordValue(string keyPath, string valueName, int dwordValue)
        {
            try
            {
                Registry.SetValue(keyPath, valueName, dwordValue, RegistryValueKind.DWord);
                return true;
            }
            catch (Exception ex)
            {
                // Handle exceptions if necessary
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
        private void button1_Click(object sender, EventArgs e)
        {
            bool val = check();
            if (val)
            {
                Mainform.MyForm.HidePanel();
                Mainform.MyForm.openPage();
            }
            else
            {
                timer1.Enabled = true;
                label1.Enabled = false;
                button1.Enabled = false;
                Mainform.MyForm.disableFirstPage();
                panel9.Location = new System.Drawing.Point((tableLayoutPanel1.Width / 2) - (panel9.Width / 2), (tableLayoutPanel1.Height / 2) - (panel9.Height / 2));
                panel9.Visible = true;
            }
        }
        private bool check()
        {
            RegistryValue.regDesValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusDesktop");
            RegistryValue.regDocValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusDocuments");
            RegistryValue.regPicValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusPictures");
            RegistryValue.regVidValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusVideos");
            RegistryValue.regDowValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusDownloads");
            RegistryValue.regMusValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusMusic");
            RegistryValue.regVidFilesValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusVideoFiles");
            RegistryValue.regAudFilesValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusAudioFiles");
            RegistryValue.regMisFilesValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusMiscFiles");

            if (RegistryValue.regDesValue > 0)
            {
                if (RegistryValue.regDocValue > 0)
                {
                    if (RegistryValue.regPicValue > 0)
                    {
                        if (RegistryValue.regVidValue > 0)
                        {
                            if (RegistryValue.regDowValue > 0)
                            {
                                if (RegistryValue.regMusValue > 0)
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            string url = ReadRegistryValue(regFullpath, "LearnMoreURL");
            if (url != null)
            {
                Process.Start(url);
            }
            //Mainform.MyForm.openPage2();
        }
        private void RunRegeditSilently(string regFilePath)
        {
            try
            {
                if (File.Exists(regFilePath))
                {
                    Process regeditProcess = new Process();
                    regeditProcess.StartInfo.FileName = "regedit.exe";
                    regeditProcess.StartInfo.Arguments = $@"/s ""{regFilePath}"" /reg:64";
                    regeditProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    regeditProcess.Start();
                    regeditProcess.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error running regedit.exe: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool RegistryKeyExists(string keyPath)
        {
            try
            {
                RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(keyPath);
                return registryKey != null;
            }
            catch
            {
                return false;
            }
        }
        private string ReadRegistryValue(string keyPath, string valueName)
        {
            try
            {
                object value = Registry.GetValue(keyPath, valueName, null);
                if (value != null)
                {
                    return value.ToString();
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            bool val = check();
            if (val)
            {
                timer1.Enabled = false;
                Mainform.MyForm.HidePanel();
                Mainform.MyForm.openPage();
                panel9.Visible = false;
            }
            else
            {
                
            }
        }
    }
}
