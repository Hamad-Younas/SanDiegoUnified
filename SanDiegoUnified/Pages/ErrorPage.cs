using Microsoft.Win32;
using SanDiegoUnified.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SanDiegoUnified.Pages
{
    public partial class ErrorPage : Form
    {
        string regFullpath = @"HKEY_CURRENT_USER\Software\OneDriveMigrationTool";
        public ErrorPage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Mainform.MyForm.openPage4();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            string url = ReadRegistryValue(regFullpath, "GetHelpURL");
            if (url != null)
            {
                Process.Start(url);
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
        private int error()
        {
            int val = ReadRegistryDwordValue(regFullpath, "ErrorCode");
            if (val != 0)
            {
                return val;
            }
            return -1;
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

        private void ErrorPage_Load(object sender, EventArgs e)
        {
            label3.Text = $"Error ({error()})";
        }
    }
}
