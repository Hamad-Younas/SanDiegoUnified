using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SanDiegoUnified.Models;
using SanDiegoUnified.Services;
using SanDiegoUnified.Components;
using Microsoft.Win32;

namespace SanDiegoUnified.Pages
{
    public partial class folderMigrate : Form
    {
        private Bitmap _bm;
        private int _process = -1;
        string regFullpath = @"HKEY_CURRENT_USER\Software\OneDriveMigrationTool";

        public folderMigrate()
        {
            InitializeComponent();
        }
        private void folderMigrate_Load(object sender, EventArgs e)
        {
            string imagePath = "Images/Page-1 (2).png";
            string imagePath2 = "Images/Page-1.png";
            tableLayoutPanel43.Visible = false;
            // registry
            Data.desktopData = ReadRegistryValue(regFullpath, "InvDesktop");
            Data.documentData = ReadRegistryValue(regFullpath, "InvDocuments");
            Data.picData = ReadRegistryValue(regFullpath, "InvPictures");
            Data.videoData = ReadRegistryValue(regFullpath, "InvVideos");
            Data.downloadData = ReadRegistryValue(regFullpath, "InvDownloads");
            Data.musicData = ReadRegistryValue(regFullpath, "InvMusic");
            Data.videoDatafile = ReadRegistryValue(regFullpath, "InvVideoFiles");
            Data.audiDatafile = ReadRegistryValue(regFullpath, "InvAudioFiles");
            Data.miscDatafile = ReadRegistryValue(regFullpath, "InvMiscFiles");


            RegistryValue.regDesValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusDesktop");
            RegistryValue.regDocValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusDocuments");
            RegistryValue.regPicValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusPictures");
            RegistryValue.regVidValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusVideos");
            RegistryValue.regDowValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusDownloads");
            RegistryValue.regMusValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusMusic");
            RegistryValue.regVidFilesValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusVideoFiles");
            RegistryValue.regAudFilesValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusAudioFiles");
            RegistryValue.regMisFilesValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusMiscFiles");


            DriveInfo cDrive = new DriveInfo("C");
            CDrive.availableC = FormatBytes(cDrive.TotalFreeSpace);
            CDrive.totalC = FormatBytes(cDrive.TotalSize);
            label21.Text = CDrive.availableC + " Available";


            int totalCapacity = int.Parse(CDrive.availableC.Split(' ')[0]);
            //90,00,000 bytes/second
            if (Data.desktopData != null)
            {
                string[] parts = Data.desktopData.Split(';');
                int gb = ConvertBytesToGB(long.Parse(parts[1]));
                Memory.desktopMemoryGB = gb;
                ProgressValue.desktopProgressbar = CalculateProgressBarValue(gb, totalCapacity);
                EstimateTime.desktopTime = CalculateTime(long.Parse(parts[1]));
                Items.desktopItems = int.Parse(parts[0]);

                int regValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusDesktop");
                RegistryValue.regDesValue = regValue;
                if (regValue == -1)
                {
                    panel1.BackgroundImage = Image.FromFile(imagePath);
                    panel1.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                    label3.Text = $"{parts[0]} items";
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusDesktop", 0);
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace + gb;
                    EstimateTime.time = EstimateTime.time + CalculateTime(long.Parse(parts[1]));
                    label4.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                }
                else if (regValue == 1 || regValue == 0 || regValue == 2)
                {
                    panel1.BackgroundImage = Image.FromFile(imagePath);
                    panel1.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                    label3.Text = $"{parts[0]} items";
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace + gb;
                    EstimateTime.time = EstimateTime.time + CalculateTime(long.Parse(parts[1]));
                    label4.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                }
                else if (regValue == 3)
                {
                    label3.Text = $"paused";
                    button1.Text = "Resume";
                    label4.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    EstimateTime.time = EstimateTime.time + CalculateTime(long.Parse(parts[1]));
                    panel1.BackgroundImage = Image.FromFile(imagePath);
                    panel1.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                }
                else if (regValue == 5)
                {
                    label3.Text = $"{parts[0]} items";
                    label4.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    panel1.BackgroundImage = Image.FromFile(imagePath2);
                    panel1.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                }
                else if (regValue == 4)
                {
                    label3.Text = $"{parts[0]} items";
                    label4.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    panel1.BackgroundImage = Image.FromFile(imagePath);
                    panel1.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                    panel1.Enabled = false;
                    AddLabelFinished(tableLayoutPanel37, Color.FromArgb(255, 0, 89, 154));
                }
                else
                {

                }
            }
            if (Data.documentData != null)
            {
                string[] parts = Data.documentData.Split(';');
                int gb = ConvertBytesToGB(long.Parse(parts[1]));
                Memory.documentMemoryGB = gb;
                ProgressValue.documentProgressbar = CalculateProgressBarValue(gb, totalCapacity);
                EstimateTime.documentTime = CalculateTime(long.Parse(parts[1]));
                Items.documentItems = int.Parse(parts[0]);

                int regValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusDocuments");
                RegistryValue.regDocValue = regValue;
                if (regValue == -1)
                {
                    panel5.BackgroundImage = Image.FromFile(imagePath);
                    panel5.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                    label7.Text = $"{parts[0]} items";
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusDocuments", 0);
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace + gb;
                    EstimateTime.time = EstimateTime.time + CalculateTime(long.Parse(parts[1]));
                    label6.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                }
                else if (regValue == 1 || regValue == 0 || regValue == 2)
                {
                    panel5.BackgroundImage = Image.FromFile(imagePath);
                    panel5.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                    label7.Text = $"{parts[0]} items";
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace + gb;
                    EstimateTime.time = EstimateTime.time + CalculateTime(long.Parse(parts[1]));
                    label6.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                }
                else if (regValue == 3)
                {
                    label7.Text = $"paused";
                    button1.Text = "Resume";
                    label6.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    EstimateTime.time = EstimateTime.time + CalculateTime(long.Parse(parts[1]));
                    panel5.BackgroundImage = Image.FromFile(imagePath);
                    panel5.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                }
                else if (regValue == 5)
                {
                    label7.Text = $"{parts[0]} items";
                    label6.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    panel5.BackgroundImage = Image.FromFile(imagePath2);
                    panel5.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                }
                else if (regValue == 4)
                {
                    label7.Text = $"{parts[0]} items";
                    label6.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    panel5.BackgroundImage = Image.FromFile(imagePath);
                    panel5.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                    panel5.Enabled = false;
                    AddLabelFinished(tableLayoutPanel9, Color.FromArgb(255, 226, 75, 38));
                }
                else
                {

                }
            }
            if (Data.picData != null)
            {
                string[] parts = Data.picData.Split(';');
                int gb = ConvertBytesToGB(long.Parse(parts[1]));
                Memory.pictureMemoryGB = gb;
                ProgressValue.picProgressbar = CalculateProgressBarValue(gb, totalCapacity);
                EstimateTime.pictureTime = CalculateTime(long.Parse(parts[1]));
                Items.pictureItems = int.Parse(parts[0]);

                int regValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusPictures");
                RegistryValue.regPicValue = regValue;
                if (regValue == -1)
                {
                    panel8.BackgroundImage = Image.FromFile(imagePath);
                    panel8.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                    label10.Text = $"{parts[0]} items";
                    label9.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusPictures", 0);
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace + gb;
                    EstimateTime.time = EstimateTime.time + CalculateTime(long.Parse(parts[1]));
                }
                else if (regValue == 1 || regValue == 0 || regValue == 2)
                {
                    panel8.BackgroundImage = Image.FromFile(imagePath);
                    panel8.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                    label10.Text = $"{parts[0]} items";
                    label9.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace + gb;
                    EstimateTime.time = EstimateTime.time + CalculateTime(long.Parse(parts[1]));
                    
                }
                else if (regValue == 3)
                {
                    label10.Text = $"paused";
                    button1.Text = "Resume";
                    label9.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    EstimateTime.time = EstimateTime.time + CalculateTime(long.Parse(parts[1]));
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace + gb;
                    panel8.BackgroundImage = Image.FromFile(imagePath);
                    panel8.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                }
                else if (regValue == 5)
                {
                    label10.Text = $"{parts[0]} items";
                    label9.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    panel8.BackgroundImage = Image.FromFile(imagePath2);
                    panel8.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                }
                else if (regValue == 4)
                {
                    label10.Text = $"{parts[0]} items";
                    label9.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    panel8.BackgroundImage = Image.FromFile(imagePath);
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace + gb;
                    panel8.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                    panel8.Enabled = false;
                    AddLabelFinished(tableLayoutPanel39, Color.FromArgb(255, 143, 175, 62));
                }
                else
                {

                }
            }
            if (Data.videoData != null)
            {
                string[] parts = Data.videoData.Split(';');
                int gb = ConvertBytesToGB(long.Parse(parts[1]));
                Memory.videoMemoryGB = gb;
                ProgressValue.videoProgressbar = CalculateProgressBarValue(gb, totalCapacity);
                EstimateTime.videoTime = CalculateTime(long.Parse(parts[1]));
                Items.videoItems = int.Parse(parts[0]);

                int regValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusVideos");
                RegistryValue.regVidValue = regValue;
                if (regValue == -1)
                {
                    panel11.BackgroundImage = Image.FromFile(imagePath2);
                    panel11.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                    label13.Text = $"{parts[0]} items";
                    label12.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusVideos", 0);
                }
                else if (regValue == 1 || regValue == 0)
                {
                    label13.Text = $"{parts[0]} items";
                    label12.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    panel11.BackgroundImage = Image.FromFile(imagePath2);
                    panel11.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                }
                else if (regValue == 2)
                {
                    panel11.BackgroundImage = Image.FromFile(imagePath);
                    panel11.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                    label13.Text = $"{parts[0]} items";
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace + gb;
                    EstimateTime.time = EstimateTime.time + CalculateTime(long.Parse(parts[1]));
                    label12.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                }
                else if (regValue == 3)
                {
                    label13.Text = "paused";
                    button1.Text = "Resume";
                    label12.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace + gb;
                    EstimateTime.time = EstimateTime.time + CalculateTime(long.Parse(parts[1]));
                    panel11.BackgroundImage = Image.FromFile(imagePath);
                    panel11.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                }
                else if (regValue == 5)
                {
                    label13.Text = $"{parts[0]} items";
                    label12.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    panel11.BackgroundImage = Image.FromFile(imagePath2);
                    panel11.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                }
                else if (regValue == 4)
                {
                    label13.Text = $"{parts[0]} items";
                    label12.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    panel11.BackgroundImage = Image.FromFile(imagePath);
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace + gb;
                    panel11.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                    panel11.Enabled = false;
                    AddLabelFinished(tableLayoutPanel14, Color.FromArgb(255, 164, 38, 114));
                }
                else
                {

                }
            }
            if (Data.downloadData != null)
            {
                string[] parts = Data.downloadData.Split(';');
                int gb = ConvertBytesToGB(long.Parse(parts[1]));
                Memory.downloadMemoryGB = gb;
                ProgressValue.downloadProgressbar = CalculateProgressBarValue(gb, totalCapacity);
                EstimateTime.downloadTime = CalculateTime(long.Parse(parts[1]));
                Items.downloadItems = int.Parse(parts[0]);

                int regValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusDownloads");
                RegistryValue.regDowValue = regValue;
                if (regValue == -1)
                {
                    panel17.BackgroundImage = Image.FromFile(imagePath2);
                    panel17.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                    label19.Text = $"{parts[0]} items";
                    label18.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusDownloads", 0);
                }
                else if (regValue == 1 || regValue == 0)
                {
                    label19.Text = $"{parts[0]} items";
                    label18.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    panel17.BackgroundImage = Image.FromFile(imagePath2);
                    panel17.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                }
                else if (regValue == 2)
                {
                    panel17.BackgroundImage = Image.FromFile(imagePath);
                    panel17.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                    label19.Text = $"{parts[0]} items";
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace + gb;
                    EstimateTime.time = EstimateTime.time + CalculateTime(long.Parse(parts[1]));
                    label18.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                }
                else if (regValue == 3)
                {
                    label19.Text = "paused";
                    button1.Text = "Resume";
                    label18.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace + gb;
                    EstimateTime.time = EstimateTime.time + CalculateTime(long.Parse(parts[1]));
                    panel17.BackgroundImage = Image.FromFile(imagePath);
                    panel17.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                }
                else if (regValue == 5)
                {
                    label19.Text = $"{parts[0]} items";
                    label18.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    panel17.BackgroundImage = Image.FromFile(imagePath2);
                    panel17.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                }
                else if (regValue == 4)
                {
                    label19.Text = $"{parts[0]} items";
                    label18.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    panel17.BackgroundImage = Image.FromFile(imagePath);
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace + gb;
                    panel17.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                    panel17.Enabled = false;
                    AddLabelFinished(tableLayoutPanel42, Color.FromArgb(255, 250, 175, 64));
                }
                else
                {

                }
            }
            if (Data.musicData != null)
            {
                string[] parts = Data.musicData.Split(';');
                int gb = ConvertBytesToGB(long.Parse(parts[1]));
                Memory.musicMemoryGB = gb;
                ProgressValue.musicProgressbar = CalculateProgressBarValue(gb, totalCapacity);
                EstimateTime.musicTime = CalculateTime(long.Parse(parts[1]));
                Items.musicItems = int.Parse(parts[0]);

                int regValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusMusic");
                RegistryValue.regMusValue = regValue;
                if (regValue == -1)
                {
                    label16.Text = $"{parts[0]} items";
                    panel14.BackgroundImage = Image.FromFile(imagePath2);
                    panel14.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                    label15.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusMusic", 0);
                }
                else if (regValue == 1 || regValue == 0)
                {
                    label16.Text = $"{parts[0]} items";
                    label15.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    panel14.BackgroundImage = Image.FromFile(imagePath2);
                    panel14.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                }
                else if (regValue == 2)
                {
                    panel14.BackgroundImage = Image.FromFile(imagePath);
                    panel14.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                    label16.Text = $"{parts[0]} items";
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace + gb;
                    EstimateTime.time = EstimateTime.time + CalculateTime(long.Parse(parts[1]));
                    label15.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                }
                else if (regValue == 3)
                {
                    label16.Text = "paused";
                    button1.Text = "Resume";
                    label15.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace + gb;
                    EstimateTime.time = EstimateTime.time + CalculateTime(long.Parse(parts[1]));
                    panel14.BackgroundImage = Image.FromFile(imagePath);
                    panel14.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                }
                else if (regValue == 5)
                {
                    label16.Text = $"{parts[0]} items";
                    label15.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    panel14.BackgroundImage = Image.FromFile(imagePath2);
                    panel14.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                }
                else if (regValue == 4)
                {
                    label16.Text = $"{parts[0]} items";
                    label15.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace + gb;
                    panel14.BackgroundImage = Image.FromFile(imagePath);
                    panel14.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                    panel14.Enabled = false;
                    AddLabelFinished(tableLayoutPanel40, Color.FromArgb(255, 156, 109, 255));
                }
                else
                {

                }
            }
            
            if (Data.videoDatafile != null)
            {
                string[] parts = Data.videoDatafile.Split(';');
                int gb = ConvertBytesToGB(long.Parse(parts[1]));
                Memory.videoFileMemoryGB = gb;
                int equalMemory = (int)Math.Ceiling(gb / totalCheckedFolder());
                Items.videoFileItems = int.Parse(parts[0]);
                label23.Text = $"{parts[0]} items";
                label24.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";
                EstimateTime.videoFileTime = CalculateTime(long.Parse(parts[1]));

                int regValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusVideoFiles");
                RegistryValue.regVidFilesValue = regValue;
                if (regValue == -1)
                {
                    panel20.BackgroundImage = Image.FromFile(imagePath);
                    panel20.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusVideoFiles", 0);
                }
                if (regValue == 0 || regValue == 1 || regValue == 2)
                {
                    panel20.BackgroundImage = Image.FromFile(imagePath);
                    panel20.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                }
                if (regValue == 5)
                {
                    panel20.BackgroundImage = Image.FromFile(imagePath2);
                    panel20.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                }
            }
            
            if (Data.audiDatafile != null)
            {
                string[] parts = Data.audiDatafile.Split(';');
                int gb = ConvertBytesToGB(long.Parse(parts[1]));
                Memory.audioFileMemoryGB = gb;
                int equalMemory = (int)Math.Ceiling(gb / totalCheckedFolder());
                EstimateTime.audioFileTime = CalculateTime(long.Parse(parts[1]));
                Items.audioFileItems = int.Parse(parts[0]);
                label31.Text = $"{parts[0]} items";
                label32.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";

                int regValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusAudioFiles");
                RegistryValue.regAudFilesValue = regValue;
                if (regValue == -1)
                {
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusAudioFiles", 0);
                    panel24.BackgroundImage = Image.FromFile(imagePath);
                    panel24.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                }
                if (regValue == 0 || regValue == 1 || regValue == 2)
                {
                    panel24.BackgroundImage = Image.FromFile(imagePath);
                    panel24.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                }
                if (regValue == 5)
                {
                    panel24.BackgroundImage = Image.FromFile(imagePath2);
                    panel24.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                }
            }

            if (Data.miscDatafile != null)
            {
                string[] parts = Data.miscDatafile.Split(';');
                int gb = ConvertBytesToGB(long.Parse(parts[1]));
                Memory.miscFileMemoryGB = gb;
                int equalMemory = (int)Math.Ceiling(gb / totalCheckedFolder());
                EstimateTime.miscFileTime = CalculateTime(long.Parse(parts[1]));
                Items.miscFileItems = int.Parse(parts[0]);
                label27.Text = $"{parts[0]} items";
                label28.Text = $"{gb}GB / {CalculateTime(long.Parse(parts[1]))} minutes";

                int regValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusMiscFiles");
                RegistryValue.regMisFilesValue = regValue;
                if (regValue == -1)
                {
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusMiscFiles", 0);
                    panel22.BackgroundImage = Image.FromFile(imagePath2);
                    panel22.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                }
                if (regValue == 0 || regValue == 1)
                {
                    panel22.BackgroundImage = Image.FromFile(imagePath2);
                    panel22.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                }
                if (regValue == 2)
                {
                    panel22.BackgroundImage = Image.FromFile(imagePath);
                    panel22.BackgroundImage.Tag = Path.GetFullPath(imagePath);
                }
                if (regValue == 5)
                {
                    panel22.BackgroundImage = Image.FromFile(imagePath2);
                    panel22.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                }
            }

            Mainform.MyForm.estimateTimeLabel();

            if (CDrive.occupaiedSpace >= int.Parse(CDrive.availableC.Split(' ')[0]))
            {
                button1.Enabled = false;
                tableLayoutPanel43.Visible = true;
                label21.ForeColor = Color.FromArgb(1, 226, 75, 38);
                label21.ForeColor = Color.FromArgb(1, 226, 75, 38);
                SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
            }
            else
            {
                button1.Enabled = true;
                tableLayoutPanel43.Visible = false;
                label21.ForeColor = Color.FromArgb(0, 89, 154);
                SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
            }

            PrivateFontCollection pfc = new PrivateFontCollection();
            pfc.AddFontFile("Fonts/Khula/Khula-Bold.ttf");
            if (pfc.Families.Length > 0)
            {
                Font customFont = new Font(pfc.Families[0], 24, FontStyle.Regular);
                label1.Font = customFont;
            }
        }

        private double totalCheckedFolder()
        {
            double totalChecked = 0;
            if (RegistryValue.regDesValue == 2)
            {
                totalChecked++;
            }
            if (RegistryValue.regDocValue == 2)
            {
                totalChecked++;
            }
            if (RegistryValue.regPicValue == 2)
            {
                totalChecked++;
            }
            if (RegistryValue.regVidValue == 2)
            {
                totalChecked++;
            }
            if (RegistryValue.regDowValue == 2)
            {
                totalChecked++;
            }
            if (RegistryValue.regMusValue == 2)
            {
                totalChecked++;
            }
            return totalChecked;
        }
        private int CalculateTime(long dataSizeInBytes)
        {
            long speedInBytesPerSecond = 90000000;
            double timeInSeconds = (double)dataSizeInBytes / speedInBytesPerSecond;

            if (timeInSeconds < 60)
            {
                return 1;
            }
            else if (timeInSeconds >= 60)
            {
                double timeInMinutes = timeInSeconds / 60;
                return (int)Math.Ceiling(timeInMinutes);
            }

            return 0;
        }
        private int CalculateTimeInMinutesForGB(double dataSizeInGB)
        {
            double speedInGBPerSecond = 0.084; // Adjust this based on your specific speed in GB/s

            double timeInSeconds = dataSizeInGB / speedInGBPerSecond;

            if (timeInSeconds < 60)
            {
                return 1;
            }
            else if(timeInSeconds >= 60)
            {
                double timeInMinutes = timeInSeconds / 60;
                return (int)Math.Ceiling(timeInMinutes);
            }
            return 0;
        }

        private int ConvertBytesToGB(long bytes)
        {
            const double bytesInGB = 1024 * 1024 * 1024.0;
            return (int)Math.Ceiling(bytes / bytesInGB);
        }
        private int CalculateProgressBarValue(int parsedValue, int total)
        {
            int totalCapacity = total / 6;
            int progressBarValue = Math.Max(0, Math.Min(100, parsedValue * 100 / totalCapacity));
            return progressBarValue;
        }
        private static string FormatBytes(long bytes)
        {
            const int scale = 1024;
            string[] orders = { "GB", "MB", "KB", "Bytes" };
            long max = (long)Math.Pow(scale, orders.Length - 1);
            foreach (string order in orders)
            {
                if (bytes > max || order == "Bytes")
                    return string.Format("{0:##} {1}", Math.Round((decimal)bytes / max), order);

                max /= scale;
            }
            return "0 Bytes";
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            Panel gradientPanel = (Panel)sender;

            Color color1 = ColorTranslator.FromHtml("#000000");
            Color color2 = ColorTranslator.FromHtml("#000000");

            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(
                gradientPanel.ClientRectangle,
                color1,
                color2,
                LinearGradientMode.Horizontal);

            e.Graphics.FillRectangle(linearGradientBrush, gradientPanel.ClientRectangle);
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            Panel gradientPanel = (Panel)sender;

            Color color1 = ColorTranslator.FromHtml("#000000");
            Color color2 = ColorTranslator.FromHtml("#000000");

            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(
                gradientPanel.ClientRectangle,
                color1,
                color2,
                LinearGradientMode.Horizontal);

            e.Graphics.FillRectangle(linearGradientBrush, gradientPanel.ClientRectangle);
        }

        private void panel9_Paint(object sender, PaintEventArgs e)
        {
            Panel gradientPanel = (Panel)sender;

            Color color1 = ColorTranslator.FromHtml("#000000");
            Color color2 = ColorTranslator.FromHtml("#000000");

            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(
                gradientPanel.ClientRectangle,
                color1,
                color2,
                LinearGradientMode.Horizontal);

            e.Graphics.FillRectangle(linearGradientBrush, gradientPanel.ClientRectangle);
        }

        
        private void AddLabelWithNoneAnchor(TableLayoutPanel tableLayoutPanel,string text)
        {
            // Create a label
            tableLayoutPanel.Controls.Clear();
            tableLayoutPanel.Margin = new Padding(10, 10, 10, 10);
            if (tableLayoutPanel.ColumnCount > 0)
            {
                // Remove the last column
                tableLayoutPanel.ColumnStyles.RemoveAt(tableLayoutPanel.ColumnCount - 1);

                // Remove controls from the last column (optional)
                foreach (Control control in tableLayoutPanel.Controls)
                {
                    if (tableLayoutPanel.GetColumn(control) == tableLayoutPanel.ColumnCount)
                    {
                        tableLayoutPanel.Controls.Remove(control);
                    }
                }
            }
            Label label = new Label();
            label.Text = text;
            label.AutoSize = true;  
            label.BackColor = Color.Transparent;
            label.Anchor = AnchorStyles.None;

            // Add the label to the TableLayoutPanel
            tableLayoutPanel.Controls.Add(label);

            // Optionally, specify the cell in the TableLayoutPanel where you want to place the label
            tableLayoutPanel.SetCellPosition(label, new TableLayoutPanelCellPosition(0, 0));
        }
        private void AddLabelFinished(TableLayoutPanel tableLayoutPanel,Color color)
        {
            // Create a label
            tableLayoutPanel.Controls.Clear();
            tableLayoutPanel.Margin = new Padding(10, 10, 10, 10);
            if (tableLayoutPanel.ColumnCount > 0)
            {
                // Remove the last column
                tableLayoutPanel.ColumnStyles.RemoveAt(tableLayoutPanel.ColumnCount - 1);

                // Remove controls from the last column (optional)
                foreach (Control control in tableLayoutPanel.Controls)
                {
                    if (tableLayoutPanel.GetColumn(control) == tableLayoutPanel.ColumnCount)
                    {
                        tableLayoutPanel.Controls.Remove(control);
                    }
                }
            }
            Label label = new Label();
            label.ForeColor = Color.White;
            label.Text = "Finished";
            label.AutoSize = true;
            label.BackColor = Color.Transparent;
            label.Anchor = AnchorStyles.None;
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.BackColor = color;

            // Add the label to the TableLayoutPanel
            tableLayoutPanel.Controls.Add(label);

            // Optionally, specify the cell in the TableLayoutPanel where you want to place the label
            tableLayoutPanel.SetCellPosition(label, new TableLayoutPanelCellPosition(0, 0));
        }
        private void AddLabelFinished2(TableLayoutPanel tableLayoutPanel, Color color)
        {
            // Create a label
            tableLayoutPanel.Controls.Clear();
            tableLayoutPanel.Margin = new Padding(10, 10, 10, 10);
            Label label = new Label();
            label.ForeColor = Color.White;
            label.Text = "Finished";
            label.AutoSize = true;
            label.BackColor = Color.Transparent;
            label.Anchor = AnchorStyles.None;
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.BackColor = color;

            // Add the label to the TableLayoutPanel
            tableLayoutPanel.Controls.Add(label);

            // Optionally, specify the cell in the TableLayoutPanel where you want to place the label
            tableLayoutPanel.SetCellPosition(label, new TableLayoutPanelCellPosition(0, 0));
        }
        private void addLabelAtProgress()
        {
            // Create a label
            Label label = new Label();
            label.Text = " Do not use computer until process is completed";
            label.AutoSize = true;
            label.Font = new Font("Microsoft Sans Serif", 10, FontStyle.Regular);
            label.ForeColor = Color.FromArgb(1, 226, 75, 38);
            label.BackColor = Color.Transparent;
            label.Anchor = AnchorStyles.None;

            // Add the label to the TableLayoutPanel
            tableLayoutPanel2.Controls.Remove(tableLayoutPanel44);
            tableLayoutPanel2.Controls.Add(label,0,5);
        }
        private void RemoveControlsFromRowAndColumn(TableLayoutPanel tableLayoutPanel18)
        {
            int rowToRemove = 0;    // 0-based index, so 1 corresponds to the 2nd row
            int columnToRemove = 0; // 0-based index, so 3 corresponds to the 4th column

            // Get the control at the specified cell
            Control controlToRemove = tableLayoutPanel18.GetControlFromPosition(columnToRemove, rowToRemove);

            // Remove the control if it exists
            if (controlToRemove != null)
            {
                tableLayoutPanel18.Controls.Remove(controlToRemove);
                controlToRemove.Dispose(); // Optional: Dispose the control if needed
            }
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
            tableLayoutPanel18.Controls.Add(ChildForm,0,0);
            tableLayoutPanel18.Tag = ChildForm;
            ChildForm.BringToFront();
            ChildForm.Show();
        }
        private void desktopCopying()
        {
            string text = $"copying {Items.desktopItems} items  {Memory.desktopMemoryGB} GB / {EstimateTime.desktopTime} minutes";
            AddLabelWithNoneAnchor(tableLayoutPanel37, text);
            _bm = new Bitmap(tableLayoutPanel37.ClientSize.Width, tableLayoutPanel37.ClientSize.Height);
            tableLayoutPanel37.BackgroundImage = _bm;
            timer.Enabled = true;
            int dynamicTimeInMinutes = EstimateTime.desktopTime;
            int ticksTarget = 100;
            int millisecondsPerMinute = 60 * 1000;
            int interval = (int)((double)millisecondsPerMinute * dynamicTimeInMinutes / ticksTarget);
            timer.Interval = interval;
            using (Graphics graphics = Graphics.FromImage(_bm))
            {
                graphics.Clear(tableLayoutPanel37.BackColor);
            }
        }
        private void videoCopying()
        {
            string text = $"copying {Items.videoItems} items {Memory.videoMemoryGB} GB / {EstimateTime.videoTime} minutes";
            AddLabelWithNoneAnchor(tableLayoutPanel14,text);
            _bm = new Bitmap(tableLayoutPanel14.ClientSize.Width, tableLayoutPanel14.ClientSize.Height);
            tableLayoutPanel14.BackgroundImage = _bm;
            timer3.Enabled = true;
            int dynamicTimeInMinutes = EstimateTime.videoTime;
            int ticksTarget = 100;
            int millisecondsPerMinute = 60 * 1000;
            int interval = (int)((double)millisecondsPerMinute * dynamicTimeInMinutes / ticksTarget);
            timer3.Interval = interval;
            using (Graphics graphics = Graphics.FromImage(_bm))
            {
                graphics.Clear(tableLayoutPanel14.BackColor);
            }
        }
        private void downloadCopying()
        {
            string text = $"copying {Items.downloadItems} items  {Memory.downloadMemoryGB} GB / {EstimateTime.downloadTime} minutes";
            AddLabelWithNoneAnchor(tableLayoutPanel42,text);
            _bm = new Bitmap(tableLayoutPanel42.ClientSize.Width, tableLayoutPanel42.ClientSize.Height);
            tableLayoutPanel42.BackgroundImage = _bm;
            timer4.Enabled = true;
            int dynamicTimeInMinutes = EstimateTime.downloadTime;
            int ticksTarget = 100;
            int millisecondsPerMinute = 60 * 1000;
            int interval = (int)((double)millisecondsPerMinute * dynamicTimeInMinutes / ticksTarget);
            timer4.Interval = interval;
            using (Graphics graphics = Graphics.FromImage(_bm))
            {
                graphics.Clear(tableLayoutPanel42.BackColor);
            }
        }
        private void musicCopying()
        {
            string text = $"copying {Items.musicItems} items {Memory.musicMemoryGB} GB / {EstimateTime.musicTime} minutes";
            AddLabelWithNoneAnchor(tableLayoutPanel40,text);
            _bm = new Bitmap(tableLayoutPanel40.ClientSize.Width, tableLayoutPanel40.ClientSize.Height);
            tableLayoutPanel40.BackgroundImage = _bm;
            timer5.Enabled = true;
            int dynamicTimeInMinutes = EstimateTime.musicTime;
            int ticksTarget = 100;
            int millisecondsPerMinute = 60 * 1000;
            int interval = (int)((double)millisecondsPerMinute * dynamicTimeInMinutes / ticksTarget);
            timer5.Interval = interval;
            using (Graphics graphics = Graphics.FromImage(_bm))
            {
                graphics.Clear(tableLayoutPanel40.BackColor);
            }
        }
        private void documentCopying()
        {
            string text = $"copying {Items.documentItems} Items {Memory.documentMemoryGB} GB / {EstimateTime.documentTime} minutes";
            AddLabelWithNoneAnchor(tableLayoutPanel9, text);
            _bm = new Bitmap(tableLayoutPanel9.ClientSize.Width, tableLayoutPanel9.ClientSize.Height);
            tableLayoutPanel9.BackgroundImage = _bm;
            timer1.Enabled = true;
            int dynamicTimeInMinutes = EstimateTime.documentTime;
            int ticksTarget = 100;
            int millisecondsPerMinute = 60 * 1000;
            int interval = (int)((double)millisecondsPerMinute * dynamicTimeInMinutes / ticksTarget);
            timer1.Interval = interval;
            using (Graphics graphics = Graphics.FromImage(_bm))
            {
                graphics.Clear(tableLayoutPanel9.BackColor);
            }
        }

        private void picturesCopying()
        {
            string text = $"copying {Items.pictureItems} Items {Memory.pictureMemoryGB} GB / {EstimateTime.pictureTime} minutes";
            AddLabelWithNoneAnchor(tableLayoutPanel39, text);
            _bm = new Bitmap(tableLayoutPanel39.ClientSize.Width, tableLayoutPanel39.ClientSize.Height);
            tableLayoutPanel39.BackgroundImage = _bm;
            timer2.Enabled = true;
            int dynamicTimeInMinutes = EstimateTime.pictureTime;
            int ticksTarget = 100;
            int millisecondsPerMinute = 60 * 1000;
            int interval = (int)((double)millisecondsPerMinute * dynamicTimeInMinutes / ticksTarget);
            timer2.Interval = interval;
            using (Graphics graphics = Graphics.FromImage(_bm))
            {
                graphics.Clear(tableLayoutPanel39.BackColor);
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            const int max_progress = 100;
            _process++;
            tableLayoutPanel37.BeginInvoke(new Action(() =>
            {
                if (_process > 0)
                {
                    int value = ReadRegistryDwordValue(regFullpath, "MigrationStatusDesktop");
                    if (value == 4)
                    {
                        _process = 100;
                        _process = -1;
                        using (Graphics graphics = Graphics.FromImage(_bm))
                        {
                            graphics.Clear(tableLayoutPanel37.BackColor);
                        }
                        timer.Enabled = false;
                        UpdateRegistryValue(regFullpath, "MigrationStatusDesktop", 4);
                        AddLabelFinished2(tableLayoutPanel37, Color.FromArgb(255, 0, 89, 154));
                        if (MigrationPause.enable)
                        {
                            button1.Enabled = true;
                            label21.Visible = true;
                            button1.Text = "Resume";
                            tableLayoutPanel36.Visible = true;
                            if (RegistryValue.regDocValue == 2)
                            {
                                label7.Text = $"paused";
                                UpdateRegistryValue(regFullpath, "MigrationStatusDocuments", 3);
                            }
                            if (RegistryValue.regPicValue == 2)
                            {
                                label10.Text = $"paused";
                                UpdateRegistryValue(regFullpath, "MigrationStatusPictures", 3);
                            }
                            if (RegistryValue.regVidValue == 2)
                            {
                                label13.Text = $"paused";
                                UpdateRegistryValue(regFullpath, "MigrationStatusVideos", 3);
                            }
                            if (RegistryValue.regDowValue == 2)
                            {
                                label19.Text = $"paused";
                                UpdateRegistryValue(regFullpath, "MigrationStatusDownloads", 3);
                            }
                            if (RegistryValue.regMusValue == 2)
                            {
                                label16.Text = $"paused";
                                UpdateRegistryValue(regFullpath, "MigrationStatusMusic", 3);
                            }
                        }
                        else if (RegistryValue.regDocValue == 2 || RegistryValue.regDocValue == 3)
                        {
                            documentCopying();
                        }
                        else if (RegistryValue.regPicValue == 2 || RegistryValue.regPicValue == 3)
                        {
                            picturesCopying();
                        }
                        else if (RegistryValue.regVidValue == 2 || RegistryValue.regVidValue == 3)
                        {
                            videoCopying();
                        }
                        else if (RegistryValue.regDowValue == 2 || RegistryValue.regDowValue == 3)
                        {
                            downloadCopying();
                        }
                        else if (RegistryValue.regMusValue == 2 || RegistryValue.regMusValue == 3)
                        {
                            musicCopying();
                        }
                        else
                        {
                            Mainform.MyForm.openPage5();
                        }
                        return;
                    }
                }
            }));
            //if (_process >= max_progress)
            //{
            //    _process = -1;
            //    using (Graphics graphics = Graphics.FromImage(_bm))
            //    {
            //        graphics.Clear(tableLayoutPanel37.BackColor);
            //    }
            //    timer.Enabled = false;
            //    UpdateRegistryValue(regFullpath, "MigrationStatusDesktop", 4);
            //    AddLabelFinished2(tableLayoutPanel37, Color.FromArgb(255, 0, 89, 154));
            //    if (MigrationPause.enable)
            //    {
            //        button1.Enabled = true;
            //        label21.Visible = true;
            //        button1.Text = "Resume";
            //        tableLayoutPanel36.Visible = true;
            //        if (RegistryValue.regDocValue == 2)
            //        {
            //            label7.Text = $"paused";
            //            UpdateRegistryValue(regFullpath, "MigrationStatusDocuments", 3);
            //        }
            //        if (RegistryValue.regPicValue == 2)
            //        {
            //            label10.Text = $"paused";
            //            UpdateRegistryValue(regFullpath, "MigrationStatusPictures", 3);
            //        }
            //        if (RegistryValue.regVidValue == 2)
            //        {
            //            label13.Text = $"paused";
            //            UpdateRegistryValue(regFullpath, "MigrationStatusVideos", 3);
            //        }
            //        if (RegistryValue.regDowValue == 2)
            //        {
            //            label19.Text = $"paused";
            //            UpdateRegistryValue(regFullpath, "MigrationStatusDownloads", 3);
            //        }
            //        if (RegistryValue.regMusValue == 2)
            //        {
            //            label16.Text = $"paused";
            //            UpdateRegistryValue(regFullpath, "MigrationStatusMusic", 3);
            //        }
            //    }
            //    else if (RegistryValue.regDocValue == 2 || RegistryValue.regDocValue == 3)
            //    {
            //        documentCopying();
            //    }
            //    else if (RegistryValue.regPicValue == 2 || RegistryValue.regPicValue == 3)
            //    {
            //        picturesCopying();
            //    }
            //    else if (RegistryValue.regVidValue == 2 || RegistryValue.regVidValue == 3)
            //    {
            //        videoCopying();
            //    }
            //    else if (RegistryValue.regDowValue == 2 || RegistryValue.regDowValue == 3)
            //    {
            //        downloadCopying();
            //    }
            //    else if (RegistryValue.regMusValue == 2 || RegistryValue.regMusValue == 3)
            //    {
            //        musicCopying();
            //    }
            //    else
            //    {
            //        Mainform.MyForm.openPage5();
            //    }
            //    return;
            //}

            using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(255, 0, 89, 154)))
            {
                using (Graphics graphics = Graphics.FromImage(_bm))
                {
                    float wid = _bm.Width * _process / (max_progress - 5);
                    float hgt = _bm.Height;
                    RectangleF rect = new RectangleF(0, 0, wid, hgt);
                    graphics.FillRectangle(solidBrush, rect);
                }
            }
            tableLayoutPanel37.Refresh();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            const int max_progress = 100;
            _process++;
            tableLayoutPanel39.BeginInvoke(new Action(() =>
            {
                if (_process > 0)
                {
                    int value = ReadRegistryDwordValue(regFullpath, "MigrationStatusPictures");
                    if (value == 4)
                    {
                        _process = 100;
                        _process = -1;
                        //btnDownload.Text = "Download";
                        using (Graphics graphics = Graphics.FromImage(_bm))
                        {
                            graphics.Clear(tableLayoutPanel39.BackColor);
                        }
                        timer2.Enabled = false;
                        UpdateRegistryValue(regFullpath, "MigrationStatusPictures", 4);
                        AddLabelFinished2(tableLayoutPanel39, Color.FromArgb(255, 143, 175, 62));
                        if (MigrationPause.enable)
                        {
                            button1.Enabled = true;
                            label21.Visible = true;
                            button1.Text = "Resume";
                            tableLayoutPanel36.Visible = true;
                            if (RegistryValue.regVidValue == 2)
                            {
                                label13.Text = $"paused";
                                UpdateRegistryValue(regFullpath, "MigrationStatusVideos", 3);
                            }
                            if (RegistryValue.regDowValue == 2)
                            {
                                label19.Text = $"paused";
                                UpdateRegistryValue(regFullpath, "MigrationStatusDownloads", 3);
                            }
                            if (RegistryValue.regMusValue == 2)
                            {
                                label16.Text = $"paused";
                                UpdateRegistryValue(regFullpath, "MigrationStatusMusic", 3);
                            }
                        }
                        else if (RegistryValue.regVidValue == 2 || RegistryValue.regVidValue == 3)
                        {
                            videoCopying();
                        }
                        else if (RegistryValue.regDowValue == 2 || RegistryValue.regDowValue == 3)
                        {
                            downloadCopying();
                        }
                        else if (RegistryValue.regMusValue == 2 || RegistryValue.regMusValue == 3)
                        {
                            musicCopying();
                        }
                        else
                        {
                            Mainform.MyForm.openPage5();
                        }
                        return;
                    }
                }
            }));
            //if (_process >= max_progress)
            //{
            //    _process = -1;
            //    //btnDownload.Text = "Download";
            //    using (Graphics graphics = Graphics.FromImage(_bm))
            //    {
            //        graphics.Clear(tableLayoutPanel39.BackColor);
            //    }
            //    timer2.Enabled = false;
            //    UpdateRegistryValue(regFullpath, "MigrationStatusPictures", 4);
            //    AddLabelFinished2(tableLayoutPanel39, Color.FromArgb(255, 143, 175, 62));
            //    if (MigrationPause.enable)
            //    {
            //        button1.Enabled = true;
            //        label21.Visible = true;
            //        button1.Text = "Resume";
            //        tableLayoutPanel36.Visible = true;
            //        if (RegistryValue.regVidValue == 2)
            //        {
            //            label13.Text = $"paused";
            //            UpdateRegistryValue(regFullpath, "MigrationStatusVideos", 3);
            //        }
            //        if (RegistryValue.regDowValue == 2)
            //        {
            //            label19.Text = $"paused";
            //            UpdateRegistryValue(regFullpath, "MigrationStatusDownloads", 3);
            //        }
            //        if (RegistryValue.regMusValue == 2)
            //        {
            //            label16.Text = $"paused";
            //            UpdateRegistryValue(regFullpath, "MigrationStatusMusic", 3);
            //        }
            //    }
            //    else if (RegistryValue.regVidValue == 2 || RegistryValue.regVidValue == 3)
            //    {
            //        videoCopying();
            //    }
            //    else if (RegistryValue.regDowValue == 2 || RegistryValue.regDowValue == 3)
            //    {
            //        downloadCopying();
            //    }
            //    else if (RegistryValue.regMusValue == 2 || RegistryValue.regMusValue == 3)
            //    {
            //        musicCopying();
            //    }
            //    else
            //    {
            //        Mainform.MyForm.openPage5();
            //    }
            //    return;
            //}

            using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(255, 143, 175, 62)))
            {
                using (Graphics graphics = Graphics.FromImage(_bm))
                {
                    float wid = _bm.Width * _process / (max_progress - 5);
                    float hgt = _bm.Height;
                    RectangleF rect = new RectangleF(0, 0, wid, hgt);
                    graphics.FillRectangle(solidBrush, rect);
                }
            }
            tableLayoutPanel39.Refresh();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            const int max_progress = 100;
            _process++;
            tableLayoutPanel9.BeginInvoke(new Action(() =>
            {
                if (_process > 0)
                {
                    int value = ReadRegistryDwordValue(regFullpath, "MigrationStatusDocuments");
                    if (value == 4)
                    {
                        _process = 100;
                        _process = -1;
                        //btnDownload.Text = "Download";
                        using (Graphics graphics = Graphics.FromImage(_bm))
                        {
                            graphics.Clear(tableLayoutPanel9.BackColor);
                        }
                        timer1.Enabled = false;
                        UpdateRegistryValue(regFullpath, "MigrationStatusDocuments", 4);
                        AddLabelFinished2(tableLayoutPanel9, Color.FromArgb(255, 226, 75, 38));
                        if (MigrationPause.enable)
                        {
                            button1.Enabled = true;
                            label21.Visible = true;
                            button1.Text = "Resume";
                            tableLayoutPanel36.Visible = true;
                            if (RegistryValue.regPicValue == 2)
                            {
                                label10.Text = $"paused";
                                UpdateRegistryValue(regFullpath, "MigrationStatusPictures", 3);
                            }
                            if (RegistryValue.regVidValue == 2)
                            {
                                label13.Text = $"paused";
                                UpdateRegistryValue(regFullpath, "MigrationStatusVideos", 3);
                            }
                            if (RegistryValue.regDowValue == 2)
                            {
                                label19.Text = $"paused";
                                UpdateRegistryValue(regFullpath, "MigrationStatusDownloads", 3);
                            }
                            if (RegistryValue.regMusValue == 2)
                            {
                                label16.Text = $"paused";
                                UpdateRegistryValue(regFullpath, "MigrationStatusMusic", 3);
                            }
                        }
                        else if (RegistryValue.regPicValue == 2 || RegistryValue.regPicValue == 3)
                        {
                            picturesCopying();
                        }
                        else if (RegistryValue.regVidValue == 2 || RegistryValue.regVidValue == 3)
                        {
                            videoCopying();
                        }
                        else if (RegistryValue.regDowValue == 2 || RegistryValue.regDowValue == 3)
                        {
                            downloadCopying();
                        }
                        else if (RegistryValue.regMusValue == 2 || RegistryValue.regMusValue == 3)
                        {
                            musicCopying();
                        }
                        else
                        {
                            Mainform.MyForm.openPage5();
                        }
                        return;

                    }
                }
            }));
            //if (_process >= max_progress)
            //{
            //    _process = -1;
            //    //btnDownload.Text = "Download";
            //    using (Graphics graphics = Graphics.FromImage(_bm))
            //    {
            //        graphics.Clear(tableLayoutPanel9.BackColor);
            //    }
            //    timer1.Enabled = false;
            //    UpdateRegistryValue(regFullpath, "MigrationStatusDocuments", 4);
            //    AddLabelFinished2(tableLayoutPanel9, Color.FromArgb(255, 226, 75, 38));
            //    if (MigrationPause.enable)
            //    {
            //        button1.Enabled = true;
            //        label21.Visible = true;
            //        button1.Text = "Resume";
            //        tableLayoutPanel36.Visible = true;
            //        if (RegistryValue.regPicValue == 2)
            //        {
            //            label10.Text = $"paused";
            //            UpdateRegistryValue(regFullpath, "MigrationStatusPictures", 3);
            //        }
            //        if (RegistryValue.regVidValue == 2)
            //        {
            //            label13.Text = $"paused";
            //            UpdateRegistryValue(regFullpath, "MigrationStatusVideos", 3);
            //        }
            //        if (RegistryValue.regDowValue == 2)
            //        {
            //            label19.Text = $"paused";
            //            UpdateRegistryValue(regFullpath, "MigrationStatusDownloads", 3);
            //        }
            //        if (RegistryValue.regMusValue == 2)
            //        {
            //            label16.Text = $"paused";
            //            UpdateRegistryValue(regFullpath, "MigrationStatusMusic", 3);
            //        }
            //    }
            //    else if (RegistryValue.regPicValue == 2 || RegistryValue.regPicValue == 3)
            //    {
            //        picturesCopying();
            //    }
            //    else if (RegistryValue.regVidValue == 2 || RegistryValue.regVidValue == 3)
            //    {
            //        videoCopying();
            //    }
            //    else if (RegistryValue.regDowValue == 2 || RegistryValue.regDowValue == 3)
            //    {
            //        downloadCopying();
            //    }
            //    else if (RegistryValue.regMusValue == 2 || RegistryValue.regMusValue == 3)
            //    {
            //        musicCopying();
            //    }
            //    else
            //    {
            //        Mainform.MyForm.openPage5();
            //    }
            //    return;
            //}

            using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(255, 226, 75, 38)))
            {
                using (Graphics graphics = Graphics.FromImage(_bm))
                {
                    float wid = _bm.Width * _process / (max_progress - 5);
                    float hgt = _bm.Height;
                    RectangleF rect = new RectangleF(0, 0, wid, hgt);
                    graphics.FillRectangle(solidBrush, rect);
                }
            }
            tableLayoutPanel9.Refresh();
        }
        private void timer5_Tick(object sender, EventArgs e)
        {
            const int max_progress = 100;
            _process++;
            tableLayoutPanel40.BeginInvoke(new Action(() =>
            {
                if (_process > 0)
                {
                    int value = ReadRegistryDwordValue(regFullpath, "MigrationStatusMusic");
                    if (value == 4)
                    {
                        _process = 100;
                        _process = -1;
                        //btnDownload.Text = "Download";
                        using (Graphics graphics = Graphics.FromImage(_bm))
                        {
                            graphics.Clear(tableLayoutPanel40.BackColor);
                        }
                        timer5.Enabled = false;
                        AddLabelFinished2(tableLayoutPanel40, Color.FromArgb(255, 156, 109, 255));
                        UpdateRegistryValue(regFullpath, "MigrationStatusMusic", 4);
                        if (MigrationPause.enable)
                        {
                            button1.Enabled = true;
                            label21.Visible = true;
                            button1.Text = "Resume";
                            tableLayoutPanel36.Visible = true;
                        }
                        Mainform.MyForm.openPage5();
                        return;
                    }
                }
            }));
            //if (_process >= max_progress)
            //{
            //    _process = -1;
            //    //btnDownload.Text = "Download";
            //    using (Graphics graphics = Graphics.FromImage(_bm))
            //    {
            //        graphics.Clear(tableLayoutPanel40.BackColor);
            //    }
            //    timer5.Enabled = false;
            //    AddLabelFinished2(tableLayoutPanel40, Color.FromArgb(255, 156, 109, 255));
            //    UpdateRegistryValue(regFullpath, "MigrationStatusMusic", 4);
            //    if (MigrationPause.enable)
            //    {
            //        button1.Enabled = true;
            //        label21.Visible = true;
            //        button1.Text = "Resume";
            //        tableLayoutPanel36.Visible = true;
            //    }
            //    Mainform.MyForm.openPage5();
            //    return;
            //}

            using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(255, 156, 109, 255)))
            {
                using (Graphics graphics = Graphics.FromImage(_bm))
                {
                    float wid = _bm.Width * _process / (max_progress - 5);
                    float hgt = _bm.Height;
                    RectangleF rect = new RectangleF(0, 0, wid, hgt);
                    graphics.FillRectangle(solidBrush, rect);
                }
            }
            tableLayoutPanel40.Refresh();
        }
        private void timer4_Tick(object sender, EventArgs e)
        {
            const int max_progress = 100;
            _process++;
            tableLayoutPanel42.BeginInvoke(new Action(() =>
            {
                if (_process > 0)
                {
                    int value = ReadRegistryDwordValue(regFullpath, "MigrationStatusDownloads");
                    if (value == 4)
                    {
                        _process = 100;
                        _process = -1;
                        //btnDownload.Text = "Download";
                        using (Graphics graphics = Graphics.FromImage(_bm))
                        {
                            graphics.Clear(tableLayoutPanel42.BackColor);
                        }
                        timer4.Enabled = false;
                        UpdateRegistryValue(regFullpath, "MigrationStatusDownloads", 4);
                        AddLabelFinished2(tableLayoutPanel42, Color.FromArgb(255, 250, 175, 64));
                        if (MigrationPause.enable)
                        {
                            button1.Enabled = true;
                            label21.Visible = true;
                            tableLayoutPanel36.Visible = true;
                            button1.Text = "Resume";
                            if (RegistryValue.regMusValue == 2)
                            {
                                label16.Text = $"paused";
                                UpdateRegistryValue(regFullpath, "MigrationStatusMusic", 3);
                            }
                        }
                        else if (RegistryValue.regMusValue == 2 || RegistryValue.regMusValue == 3)
                        {
                            musicCopying();
                        }
                        else
                        {
                            Mainform.MyForm.openPage5();
                        }
                        return;
                    }
                }
            }));
            //if (_process >= max_progress)
            //{
            //    _process = -1;
            //    //btnDownload.Text = "Download";
            //    using (Graphics graphics = Graphics.FromImage(_bm))
            //    {
            //        graphics.Clear(tableLayoutPanel42.BackColor);
            //    }
            //    timer4.Enabled = false;
            //    UpdateRegistryValue(regFullpath, "MigrationStatusDownloads", 4);
            //    AddLabelFinished2(tableLayoutPanel42, Color.FromArgb(255, 250, 175, 64));
            //    if (MigrationPause.enable)
            //    {
            //        button1.Enabled = true;
            //        label21.Visible = true;
            //        tableLayoutPanel36.Visible = true;
            //        button1.Text = "Resume";
            //        if (RegistryValue.regMusValue == 2)
            //        {
            //            label16.Text = $"paused";
            //            UpdateRegistryValue(regFullpath, "MigrationStatusMusic", 3);
            //        }
            //    }
            //    else if (RegistryValue.regMusValue == 2 || RegistryValue.regMusValue == 3)
            //    {
            //        musicCopying();
            //    }
            //    else
            //    {
            //        Mainform.MyForm.openPage5();
            //    }
            //    return;
            //}

            using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(255, 250, 175, 64)))
            {
                using (Graphics graphics = Graphics.FromImage(_bm))
                {
                    float wid = _bm.Width * _process / (max_progress - 5);
                    float hgt = _bm.Height;
                    RectangleF rect = new RectangleF(0, 0, wid, hgt);
                    graphics.FillRectangle(solidBrush, rect);
                }
            }
            tableLayoutPanel42.Refresh();
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            const int max_progress = 100;
            _process++;
            tableLayoutPanel14.BeginInvoke(new Action(() =>
            {
                if (_process > 0)
                {
                    int value = ReadRegistryDwordValue(regFullpath, "MigrationStatusVideos");
                    if (value == 4)
                    {
                        _process = 100;
                        _process = -1;
                        //btnDownload.Text = "Download";
                        using (Graphics graphics = Graphics.FromImage(_bm))
                        {
                            graphics.Clear(tableLayoutPanel14.BackColor);
                        }
                        timer3.Enabled = false;
                        UpdateRegistryValue(regFullpath, "MigrationStatusVideos", 4);
                        AddLabelFinished2(tableLayoutPanel14, Color.FromArgb(255, 164, 38, 114));
                        if (MigrationPause.enable)
                        {
                            button1.Enabled = true;
                            button1.Text = "Resume";
                            label21.Visible = true;
                            tableLayoutPanel36.Visible = true;
                            if (RegistryValue.regDowValue == 2)
                            {
                                label19.Text = $"paused";
                                UpdateRegistryValue(regFullpath, "MigrationStatusDownloads", 3);
                            }
                            if (RegistryValue.regMusValue == 2)
                            {
                                label16.Text = $"paused";
                                UpdateRegistryValue(regFullpath, "MigrationStatusMusic", 3);
                            }
                        }
                        else if (RegistryValue.regDowValue == 2 || RegistryValue.regDowValue == 3)
                        {
                            downloadCopying();
                        }
                        else if (RegistryValue.regMusValue == 2 || RegistryValue.regMusValue == 3)
                        {
                            musicCopying();
                        }
                        else
                        {
                            Mainform.MyForm.openPage5();
                        }
                        return;
                    }
                }
            }));
            //if (_process >= max_progress)
            //{
            //    _process = -1;
            //    //btnDownload.Text = "Download";
            //    using (Graphics graphics = Graphics.FromImage(_bm))
            //    {
            //        graphics.Clear(tableLayoutPanel14.BackColor);
            //    }
            //    timer3.Enabled = false;
            //    UpdateRegistryValue(regFullpath, "MigrationStatusVideos", 4);
            //    AddLabelFinished2(tableLayoutPanel14, Color.FromArgb(255, 164, 38, 114));
            //    if (MigrationPause.enable)
            //    {
            //        button1.Text = "Resume";
            //        label21.Visible = true;
            //        tableLayoutPanel36.Visible = true;
            //        if (RegistryValue.regDowValue == 2)
            //        {
            //            label19.Text = $"paused";
            //            UpdateRegistryValue(regFullpath, "MigrationStatusDownloads", 3);
            //        }
            //        if (RegistryValue.regMusValue == 2)
            //        {
            //            label16.Text = $"paused";
            //            UpdateRegistryValue(regFullpath, "MigrationStatusMusic", 3);
            //        }
            //    }
            //    else if (RegistryValue.regDowValue == 2 || RegistryValue.regDowValue == 3)
            //    {
            //        downloadCopying();
            //    }
            //    else if (RegistryValue.regMusValue == 2 || RegistryValue.regMusValue == 3)
            //    {
            //        musicCopying();
            //    }
            //    else
            //    {
            //        Mainform.MyForm.openPage5();
            //    }
            //    return;
            //}

            using (SolidBrush solidBrush = new SolidBrush(Color.FromArgb(255, 164, 38, 114)))
            {
                using (Graphics graphics = Graphics.FromImage(_bm))
                {
                    float wid = _bm.Width * _process / (max_progress - 5);
                    float hgt = _bm.Height;
                    RectangleF rect = new RectangleF(0, 0, wid, hgt);
                    graphics.FillRectangle(solidBrush, rect);
                }
            }
            tableLayoutPanel14.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int val = ReadRegistryDwordValue(regFullpath, "ErrorCode");
            if (val != 0)
            {
                Mainform.MyForm.openPage6();
                return;
            }
            string imagePath1 = "Images/Page-1 (2).png";
            string imagePath2 = "Images/Page-1.png";

            RegistryValue.regDesValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusDesktop");
            RegistryValue.regDocValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusDocuments");
            RegistryValue.regPicValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusPictures");
            RegistryValue.regVidValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusVideos");
            RegistryValue.regDowValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusDownloads");
            RegistryValue.regMusValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusMusic");
            RegistryValue.regVidFilesValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusVideoFiles");
            RegistryValue.regAudFilesValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusAudioFiles");
            RegistryValue.regMisFilesValue = ReadRegistryDwordValue(regFullpath, "MigrationStatusMiscFiles");

            if (RegistryValue.regDesValue == 0 || RegistryValue.regDesValue == 1)
            {
                if (panel1.BackgroundImage != null && Path.GetFullPath(imagePath1).Equals(Path.GetFullPath(panel1.BackgroundImage.Tag?.ToString())))
                {
                    RegistryValue.regDesValue = 2;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusDesktop", RegistryValue.regDesValue);
                }
                if (panel1.BackgroundImage != null && Path.GetFullPath(imagePath2).Equals(Path.GetFullPath(panel1.BackgroundImage.Tag?.ToString())))
                {
                    RegistryValue.regDesValue = 5;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusDesktop", RegistryValue.regDesValue);
                }
            }
            if (RegistryValue.regDocValue == 0 || RegistryValue.regDocValue == 1)
            {
                if (panel5.BackgroundImage != null && Path.GetFullPath(imagePath1).Equals(Path.GetFullPath(panel5.BackgroundImage.Tag?.ToString())))
                {
                    RegistryValue.regDocValue = 2;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusDocuments", RegistryValue.regDocValue);
                }
                if (panel5.BackgroundImage != null && Path.GetFullPath(imagePath2).Equals(Path.GetFullPath(panel5.BackgroundImage.Tag?.ToString())))
                {
                    RegistryValue.regDocValue = 5;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusDocuments", RegistryValue.regDocValue);
                }
            }
            if (RegistryValue.regPicValue == 0 || RegistryValue.regPicValue == 1)
            {
                if (panel8.BackgroundImage != null && Path.GetFullPath(imagePath1).Equals(Path.GetFullPath(panel8.BackgroundImage.Tag?.ToString())))
                {
                    RegistryValue.regPicValue = 2;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusPictures", RegistryValue.regPicValue);
                }
                if (panel8.BackgroundImage != null && Path.GetFullPath(imagePath2).Equals(Path.GetFullPath(panel8.BackgroundImage.Tag?.ToString())))
                {
                    RegistryValue.regPicValue = 5;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusPictures", RegistryValue.regPicValue);
                }
            }
            if (RegistryValue.regVidValue == 0 || RegistryValue.regVidValue == 1)
            {
                if (panel11.BackgroundImage != null && Path.GetFullPath(imagePath1).Equals(Path.GetFullPath(panel11.BackgroundImage.Tag?.ToString())))
                {
                    RegistryValue.regVidValue = 2;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusVideos", RegistryValue.regVidValue);
                }
                if (panel11.BackgroundImage != null && Path.GetFullPath(imagePath2).Equals(Path.GetFullPath(panel11.BackgroundImage.Tag?.ToString())))
                {
                    RegistryValue.regVidValue = 5;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusVideos", RegistryValue.regVidValue);
                }
            }
            if (RegistryValue.regDowValue == 0 || RegistryValue.regDowValue == 1)
            {
                if (panel17.BackgroundImage != null && Path.GetFullPath(imagePath1).Equals(Path.GetFullPath(panel17.BackgroundImage.Tag?.ToString())))
                {
                    RegistryValue.regDowValue = 2;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusDownloads", RegistryValue.regDowValue);
                }
                if (panel17.BackgroundImage != null && Path.GetFullPath(imagePath2).Equals(Path.GetFullPath(panel17.BackgroundImage.Tag?.ToString())))
                {
                    RegistryValue.regDowValue = 5;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusDownloads", RegistryValue.regDowValue);
                }
            }
            if (RegistryValue.regMusValue == 0 || RegistryValue.regMusValue == 1)
            {
                if (panel14.BackgroundImage != null && Path.GetFullPath(imagePath1).Equals(Path.GetFullPath(panel14.BackgroundImage.Tag?.ToString())))
                {
                    RegistryValue.regMusValue = 2;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusMusic", RegistryValue.regMusValue);
                }
                if (panel14.BackgroundImage != null && Path.GetFullPath(imagePath2).Equals(Path.GetFullPath(panel14.BackgroundImage.Tag?.ToString())))
                {
                    RegistryValue.regMusValue = 5;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusMusic", RegistryValue.regMusValue);
                }
            }
            if (RegistryValue.regVidFilesValue == 0 || RegistryValue.regVidFilesValue == 1)
            {
                if (panel20.BackgroundImage != null && Path.GetFullPath(imagePath1).Equals(Path.GetFullPath(panel20.BackgroundImage.Tag?.ToString())))
                {
                    RegistryValue.regVidFilesValue = 0;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusVideoFiles", RegistryValue.regVidFilesValue);
                }
                if (panel20.BackgroundImage != null && Path.GetFullPath(imagePath2).Equals(Path.GetFullPath(panel20.BackgroundImage.Tag?.ToString())))
                {
                    RegistryValue.regVidFilesValue = 5;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusVideoFiles", RegistryValue.regVidFilesValue);
                }
            }
            if (RegistryValue.regAudFilesValue == 0 || RegistryValue.regAudFilesValue == 1)
            {
                if (panel24.BackgroundImage != null && Path.GetFullPath(imagePath1).Equals(Path.GetFullPath(panel24.BackgroundImage.Tag?.ToString())))
                {
                    RegistryValue.regAudFilesValue = 0;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusAudioFiles", RegistryValue.regAudFilesValue);
                }
                if (panel24.BackgroundImage != null && Path.GetFullPath(imagePath2).Equals(Path.GetFullPath(panel24.BackgroundImage.Tag?.ToString())))
                {
                    RegistryValue.regAudFilesValue = 5;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusAudioFiles", RegistryValue.regAudFilesValue);
                }
            }
            if (RegistryValue.regMisFilesValue == 0 || RegistryValue.regMisFilesValue == 1)
            {
                if (panel22.BackgroundImage != null && Path.GetFullPath(imagePath1).Equals(Path.GetFullPath(panel22.BackgroundImage.Tag?.ToString())))
                {
                    RegistryValue.regMisFilesValue = 0;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusMiscFiles", RegistryValue.regMisFilesValue);
                }
                if (panel22.BackgroundImage != null && Path.GetFullPath(imagePath2).Equals(Path.GetFullPath(panel22.BackgroundImage.Tag?.ToString())))
                {
                    RegistryValue.regMisFilesValue = 5;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusMiscFiles", RegistryValue.regMisFilesValue);
                }
            }

            panel1.Enabled = false;
            panel5.Enabled = false;
            panel8.Enabled = false;
            panel11.Enabled = false;
            panel17.Enabled = false;
            panel14.Enabled = false;
            panel20.Enabled = false;
            panel24.Enabled = false;
            panel22.Enabled = false;
            if (button1.Text == "Pause")
            {
                button1.Enabled = false;
                button1.Text = "Pausing...";
                ConfirmMessageBox mes = new ConfirmMessageBox();
                mes.Show();
            }
            else if (button1.Text == "Resume")
            {
                MigrationPause.enable = false;
                button1.Text = "Pause";
                label21.Visible = false;
                tableLayoutPanel43.Visible = false;
                tableLayoutPanel36.Visible = false;
                RemoveControlsFromRowAndColumn(tableLayoutPanel18);
                openchildform(new Validationtextpanel());
                if (RegistryValue.regDesValue == 5)
                {
                    label3.Text = "Skipped";
                }
                if (RegistryValue.regDocValue == 5)
                {
                    label7.Text = "Skipped";
                }
                if (RegistryValue.regPicValue == 5)
                {
                    label10.Text = "Skipped";
                }
                if (RegistryValue.regVidValue == 5)
                {
                    label13.Text = "Skipped";
                }
                if (RegistryValue.regDowValue == 5)
                {
                    label19.Text = "Skipped";
                }
                if (RegistryValue.regMusValue == 5)
                {
                    label16.Text = "Skipped";
                }
                if (RegistryValue.regDesValue == 3)
                {
                    RegistryValue.regDesValue = 2;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusDesktop", RegistryValue.regDesValue);
                }
                if (RegistryValue.regDocValue == 3)
                {
                    RegistryValue.regDocValue = 2;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusDocuments", RegistryValue.regDocValue);
                }
                if (RegistryValue.regPicValue == 3)
                {
                    RegistryValue.regPicValue = 2;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusPictures", RegistryValue.regPicValue);
                }
                if (RegistryValue.regVidValue == 3)
                {
                    RegistryValue.regVidValue = 2;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusVideos", RegistryValue.regVidValue);
                }
                if (RegistryValue.regDowValue == 3)
                {
                    RegistryValue.regDowValue = 2;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusDownloads", RegistryValue.regDowValue);
                }
                if (RegistryValue.regMusValue == 3)
                {
                    RegistryValue.regMusValue = 2;
                    WriteRegistryDwordValue(regFullpath, "MigrationStatusMusic", RegistryValue.regMusValue);
                }
                if (RegistryValue.regDesValue == 2)
                {
                    desktopCopying();
                }
                else if (RegistryValue.regDocValue == 2)
                {
                    documentCopying();
                }
                else if (RegistryValue.regPicValue == 2)
                {
                    picturesCopying();
                }
                else if (RegistryValue.regVidValue == 2)
                {
                    videoCopying();
                }
                else if (RegistryValue.regDowValue == 2)
                {
                    downloadCopying();
                }
                else if (RegistryValue.regMusValue == 2)
                {
                    musicCopying();
                }
                else
                {
                    Mainform.MyForm.openPage5();
                }
            }
            else
            {
                button1.Text = "Pause";
                label21.Visible = false;
                tableLayoutPanel43.Visible = false;
                tableLayoutPanel36.Visible = false;
                addLabelAtProgress();
                RemoveControlsFromRowAndColumn(tableLayoutPanel18);
                openchildform(new Validationtextpanel());
                if (RegistryValue.regDesValue == 5)
                {
                    label3.Text = "Skipped";
                }
                if (RegistryValue.regDocValue == 5)
                {
                    label7.Text = "Skipped";
                }
                if (RegistryValue.regPicValue == 5)
                {
                    label10.Text = "Skipped";
                }
                if (RegistryValue.regVidValue == 5)
                {
                    label13.Text = "Skipped";
                }
                if (RegistryValue.regDowValue == 5)
                {
                    label19.Text = "Skipped";
                }
                if (RegistryValue.regMusValue == 5)
                {
                    label16.Text = "Skipped";
                }
                if (RegistryValue.regDesValue == 2)
                {
                    desktopCopying();
                }
                else if (RegistryValue.regDocValue == 2)
                {
                    documentCopying();
                }
                else if (RegistryValue.regPicValue == 2)
                {
                    picturesCopying();
                }
                else if (RegistryValue.regVidValue == 2)
                {
                    videoCopying();
                }
                else if (RegistryValue.regDowValue == 2)
                {
                    downloadCopying();
                }
                else if (RegistryValue.regMusValue == 2)
                {
                    musicCopying();
                }
                else
                {
                    Mainform.MyForm.openPage5();
                }
            }
        }
        private void panel1_Click(object sender, EventArgs e)
        {
            string imagePath1 = "Images/Page-1 (2).png";
            string imagePath2 = "Images/Page-1.png";

            if (panel1.BackgroundImage != null && Path.GetFullPath(imagePath1).Equals(Path.GetFullPath(panel1.BackgroundImage.Tag?.ToString())))
            {
                if (File.Exists(imagePath2))
                {
                    panel1.BackgroundImage = Image.FromFile(imagePath2);
                    panel1.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace - Memory.desktopMemoryGB;
                    EstimateTime.time = EstimateTime.time - EstimateTime.desktopTime;
                    Mainform.MyForm.estimateTimeLabel();
                    RegistryValue.regDesValue = 5;
                    if (CDrive.occupaiedSpace >= int.Parse(CDrive.availableC.Split(' ')[0]))
                    {
                        button1.Enabled = false;
                        tableLayoutPanel43.Visible = true;
                        label21.ForeColor = Color.FromArgb(1, 226, 75, 38);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                    else
                    {
                        button1.Enabled = true;
                        tableLayoutPanel43.Visible = false;
                        label21.ForeColor = Color.FromArgb(0, 89, 154);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                }
                else
                {
                    MessageBox.Show("Image file not found!");
                }
            }
            else
            {
                if (File.Exists(imagePath1))
                {
                    RegistryValue.regDesValue = 2;
                    panel1.BackgroundImage = Image.FromFile(imagePath1);
                    panel1.BackgroundImage.Tag = Path.GetFullPath(imagePath1);
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace + Memory.desktopMemoryGB;
                    EstimateTime.time = EstimateTime.time + EstimateTime.desktopTime;
                    Mainform.MyForm.estimateTimeLabel();
                    if (CDrive.occupaiedSpace >= int.Parse(CDrive.availableC.Split(' ')[0]))
                    {
                        button1.Enabled = false;
                        tableLayoutPanel43.Visible = true;
                        label21.ForeColor = Color.FromArgb(1, 226, 75, 38);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                    else
                    {
                        button1.Enabled = true;
                        tableLayoutPanel43.Visible = false;
                        label21.ForeColor = Color.FromArgb(0, 89, 154);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                }
                else
                {
                    MessageBox.Show("Image file not found!");
                }
            }
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            string imagePath1 = "Images/Page-1 (2).png";
            string imagePath2 = "Images/Page-1.png";

            if (panel5.BackgroundImage != null && Path.GetFullPath(imagePath1).Equals(Path.GetFullPath(panel5.BackgroundImage.Tag?.ToString())))
            {
                if (File.Exists(imagePath2))
                {
                    RegistryValue.regDocValue = 5;
                    panel5.BackgroundImage = Image.FromFile(imagePath2);
                    panel5.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace - Memory.documentMemoryGB;
                    EstimateTime.time = EstimateTime.time - EstimateTime.documentTime;
                    Mainform.MyForm.estimateTimeLabel();
                    if (CDrive.occupaiedSpace >= int.Parse(CDrive.availableC.Split(' ')[0]))
                    {
                        button1.Enabled = false;
                        tableLayoutPanel43.Visible = true;
                        label21.ForeColor = Color.FromArgb(1, 226, 75, 38);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                    else
                    {
                        button1.Enabled = true;
                        tableLayoutPanel43.Visible = false;
                        label21.ForeColor = Color.FromArgb(0, 89, 154);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                }
                else
                {
                    MessageBox.Show("Image file not found!");
                }
            }
            else
            {
                if (File.Exists(imagePath1))
                {
                    RegistryValue.regDocValue = 2;
                    panel5.BackgroundImage = Image.FromFile(imagePath1);
                    panel5.BackgroundImage.Tag = Path.GetFullPath(imagePath1);
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace + Memory.documentMemoryGB;
                    EstimateTime.time = EstimateTime.time + EstimateTime.documentTime;
                    Mainform.MyForm.estimateTimeLabel();
                    if (CDrive.occupaiedSpace >= int.Parse(CDrive.availableC.Split(' ')[0]))
                    {
                        button1.Enabled = false;
                        tableLayoutPanel43.Visible = true;
                        label21.ForeColor = Color.FromArgb(1, 226, 75, 38);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                    else
                    {
                        button1.Enabled = true;
                        tableLayoutPanel43.Visible = false;
                        label21.ForeColor = Color.FromArgb(0, 89, 154);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                }
                else
                {
                    MessageBox.Show("Image file not found!");
                }
            }
        }

        private void panel8_Click(object sender, EventArgs e)
        {
            string imagePath1 = "Images/Page-1 (2).png";
            string imagePath2 = "Images/Page-1.png";

            if (panel8.BackgroundImage != null && Path.GetFullPath(imagePath1).Equals(Path.GetFullPath(panel8.BackgroundImage.Tag?.ToString())))
            {
                if (File.Exists(imagePath2))
                {
                    panel8.BackgroundImage = Image.FromFile(imagePath2);
                    panel8.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                    RegistryValue.regPicValue = 5;
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace - Memory.pictureMemoryGB;
                    EstimateTime.time = EstimateTime.time - EstimateTime.pictureTime;
                    Mainform.MyForm.estimateTimeLabel();
                    if (CDrive.occupaiedSpace >= int.Parse(CDrive.availableC.Split(' ')[0]))
                    {
                        button1.Enabled = false;
                        tableLayoutPanel43.Visible = true;
                        label21.ForeColor = Color.FromArgb(1, 226, 75, 38);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                    else
                    {
                        button1.Enabled = true;
                        tableLayoutPanel43.Visible = false;
                        label21.ForeColor = Color.FromArgb(0, 89, 154);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                }
                else
                {
                    MessageBox.Show("Image file not found!");
                }
            }
            else
            {
                if (File.Exists(imagePath1))
                {
                    RegistryValue.regPicValue = 2;
                    panel8.BackgroundImage = Image.FromFile(imagePath1);
                    panel8.BackgroundImage.Tag = Path.GetFullPath(imagePath1);
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace + Memory.pictureMemoryGB;
                    EstimateTime.time = EstimateTime.time + EstimateTime.pictureTime;
                    Mainform.MyForm.estimateTimeLabel();
                    if (CDrive.occupaiedSpace >= int.Parse(CDrive.availableC.Split(' ')[0]))
                    {
                        button1.Enabled = false;
                        tableLayoutPanel43.Visible = true;
                        label21.ForeColor = Color.FromArgb(1, 226, 75, 38);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                    else
                    {
                        button1.Enabled = true;
                        tableLayoutPanel43.Visible = false;
                        label21.ForeColor = Color.FromArgb(0, 89, 154);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                }
                else
                {
                    MessageBox.Show("Image file not found!");
                }
            }
        }

        private void panel11_Click(object sender, EventArgs e)
        {
            string imagePath1 = "Images/Page-1 (2).png";
            string imagePath2 = "Images/Page-1.png";

            if (panel11.BackgroundImage != null && Path.GetFullPath(imagePath1).Equals(Path.GetFullPath(panel11.BackgroundImage.Tag?.ToString())))
            {
                if (File.Exists(imagePath2))
                {
                    panel11.BackgroundImage = Image.FromFile(imagePath2);
                    panel11.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                    RegistryValue.regVidValue = 5;
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace - Memory.videoMemoryGB;
                    EstimateTime.time = EstimateTime.time - EstimateTime.videoTime;
                    Mainform.MyForm.estimateTimeLabel();
                    if (CDrive.occupaiedSpace >= int.Parse(CDrive.availableC.Split(' ')[0]))
                    {
                        button1.Enabled = false;
                        tableLayoutPanel43.Visible = true;
                        label21.ForeColor = Color.FromArgb(1, 226, 75, 38);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                    else
                    {
                        button1.Enabled = true;
                        tableLayoutPanel43.Visible = false;
                        label21.ForeColor = Color.FromArgb(0, 89, 154);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                }
                else
                {
                    MessageBox.Show("Image file not found!");
                }
            }
            else
            {
                if (File.Exists(imagePath1))
                {
                    RegistryValue.regVidValue = 2;
                    panel11.BackgroundImage = Image.FromFile(imagePath1);
                    panel11.BackgroundImage.Tag = Path.GetFullPath(imagePath1);
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace + Memory.videoMemoryGB;
                    EstimateTime.time = EstimateTime.time + EstimateTime.videoTime;
                    Mainform.MyForm.estimateTimeLabel();
                    if (CDrive.occupaiedSpace >= int.Parse(CDrive.availableC.Split(' ')[0]))
                    {
                        button1.Enabled = false;
                        tableLayoutPanel43.Visible = true;
                        label21.ForeColor = Color.FromArgb(1, 226, 75, 38);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                    else
                    {
                        button1.Enabled = true;
                        tableLayoutPanel43.Visible = false;
                        label21.ForeColor = Color.FromArgb(0, 89, 154);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                }
                else
                {
                    MessageBox.Show("Image file not found!");
                }
            }
        }

        private void panel17_Click(object sender, EventArgs e)
        {
            string imagePath1 = "Images/Page-1 (2).png";
            string imagePath2 = "Images/Page-1.png";

            if (panel17.BackgroundImage != null && Path.GetFullPath(imagePath1).Equals(Path.GetFullPath(panel17.BackgroundImage.Tag?.ToString())))
            {
                if (File.Exists(imagePath2))
                {
                    RegistryValue.regDowValue = 5;
                    panel17.BackgroundImage = Image.FromFile(imagePath2);
                    panel17.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace - Memory.downloadMemoryGB;
                    EstimateTime.time = EstimateTime.time - EstimateTime.downloadTime;
                    Mainform.MyForm.estimateTimeLabel();
                    if (CDrive.occupaiedSpace >= int.Parse(CDrive.availableC.Split(' ')[0]))
                    {
                        button1.Enabled = false;
                        tableLayoutPanel43.Visible = true;
                        label21.ForeColor = Color.FromArgb(1, 226, 75, 38);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                    else
                    {
                        button1.Enabled = true;
                        tableLayoutPanel43.Visible = false;
                        label21.ForeColor = Color.FromArgb(0, 89, 154);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                }
                else
                {
                    MessageBox.Show("Image file not found!");
                }
                
            }
            else
            {
                if (File.Exists(imagePath1))
                {
                    RegistryValue.regDowValue = 2;
                    panel17.BackgroundImage = Image.FromFile(imagePath1);
                    panel17.BackgroundImage.Tag = Path.GetFullPath(imagePath1);
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace + Memory.downloadMemoryGB;
                    EstimateTime.time = EstimateTime.time + EstimateTime.downloadTime;
                    Mainform.MyForm.estimateTimeLabel();
                    if (CDrive.occupaiedSpace >= int.Parse(CDrive.availableC.Split(' ')[0]))
                    {
                        button1.Enabled = false;
                        tableLayoutPanel43.Visible = true;
                        label21.ForeColor = Color.FromArgb(1, 226, 75, 38);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                    else
                    {
                        button1.Enabled = true;
                        tableLayoutPanel43.Visible = false;
                        label21.ForeColor = Color.FromArgb(0, 89, 154);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                }
                else
                {
                    MessageBox.Show("Image file not found!");
                }
            }
        }

        private void panel14_Click(object sender, EventArgs e)
        {
            string imagePath1 = "Images/Page-1 (2).png";
            string imagePath2 = "Images/Page-1.png";

            if (panel14.BackgroundImage != null && Path.GetFullPath(imagePath1).Equals(Path.GetFullPath(panel14.BackgroundImage.Tag?.ToString())))
            {
                if (File.Exists(imagePath2))
                {
                    RegistryValue.regMusValue = 5;
                    panel14.BackgroundImage = Image.FromFile(imagePath2);
                    panel14.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace - Memory.musicMemoryGB;
                    EstimateTime.time = EstimateTime.time - EstimateTime.musicTime;
                    Mainform.MyForm.estimateTimeLabel();
                    if (CDrive.occupaiedSpace >= int.Parse(CDrive.availableC.Split(' ')[0]))
                    {
                        button1.Enabled = false;
                        tableLayoutPanel43.Visible = true;
                        label21.ForeColor = Color.FromArgb(1, 226, 75, 38);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                    else
                    {
                        button1.Enabled = true;
                        tableLayoutPanel43.Visible = false;
                        label21.ForeColor = Color.FromArgb(0, 89, 154);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                }
                else
                {
                    MessageBox.Show("Image file not found!");
                }
            }
            else
            {
                if (File.Exists(imagePath1))
                {
                    RegistryValue.regMusValue = 2;
                    panel14.BackgroundImage = Image.FromFile(imagePath1);
                    panel14.BackgroundImage.Tag = Path.GetFullPath(imagePath1);
                    CDrive.occupaiedSpace = CDrive.occupaiedSpace + Memory.musicMemoryGB;
                    EstimateTime.time = EstimateTime.time + EstimateTime.musicTime;
                    Mainform.MyForm.estimateTimeLabel();
                    if (CDrive.occupaiedSpace >= int.Parse(CDrive.availableC.Split(' ')[0]))
                    {
                        button1.Enabled = false;
                        tableLayoutPanel43.Visible = true;
                        label21.ForeColor = Color.FromArgb(1, 226, 75, 38);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                    else
                    {
                        button1.Enabled = true;
                        tableLayoutPanel43.Visible = false;
                        label21.ForeColor = Color.FromArgb(0, 89, 154);
                        SetColumnWidths(tableLayoutPanel44, Memory.desktopMemoryGB, Memory.documentMemoryGB, Memory.pictureMemoryGB, Memory.videoMemoryGB, Memory.downloadMemoryGB, Memory.musicMemoryGB);
                    }
                }
                else
                {
                    MessageBox.Show("Image file not found!");
                }
            }
        }

        private void panel20_Click(object sender, EventArgs e)
        {
            string imagePath1 = "Images/Page-1 (2).png";
            string imagePath2 = "Images/Page-1.png";

            if (panel20.BackgroundImage != null && Path.GetFullPath(imagePath1).Equals(Path.GetFullPath(panel20.BackgroundImage.Tag?.ToString())))
            {
                if (File.Exists(imagePath2))
                {
                    RegistryValue.regVidFilesValue = 5;
                    panel20.BackgroundImage = Image.FromFile(imagePath2);
                    panel20.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                }
                else
                {
                }
            }
            else
            {
                if (File.Exists(imagePath1))
                {
                    RegistryValue.regVidFilesValue = 0;
                    panel20.BackgroundImage = Image.FromFile(imagePath1);
                    panel20.BackgroundImage.Tag = Path.GetFullPath(imagePath1);
                }
                else
                {
                }
            }
        }

        private void panel24_Click(object sender, EventArgs e)
        {
            string imagePath1 = "Images/Page-1 (2).png";
            string imagePath2 = "Images/Page-1.png";

            if (panel24.BackgroundImage != null && Path.GetFullPath(imagePath1).Equals(Path.GetFullPath(panel24.BackgroundImage.Tag?.ToString())))
            {
                if (File.Exists(imagePath2))
                {
                    RegistryValue.regAudFilesValue = 5;
                    panel24.BackgroundImage = Image.FromFile(imagePath2);
                    panel24.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                }
                else
                {
                }
            }
            else
            {
                if (File.Exists(imagePath1))
                {
                    RegistryValue.regAudFilesValue = 0;
                    panel24.BackgroundImage = Image.FromFile(imagePath1);
                    panel24.BackgroundImage.Tag = Path.GetFullPath(imagePath1);
                }
                else
                {
                }
            }
        }

        private void panel22_Click(object sender, EventArgs e)
        {
            string imagePath1 = "Images/Page-1 (2).png";
            string imagePath2 = "Images/Page-1.png";

            if (panel22.BackgroundImage != null && Path.GetFullPath(imagePath1).Equals(Path.GetFullPath(panel22.BackgroundImage.Tag?.ToString())))
            {
                if (File.Exists(imagePath2))
                {
                    RegistryValue.regMisFilesValue = 5;
                    panel22.BackgroundImage = Image.FromFile(imagePath2);
                    panel22.BackgroundImage.Tag = Path.GetFullPath(imagePath2);
                }
                else
                {
                }
            }
            else
            {
                if (File.Exists(imagePath1))
                {
                    RegistryValue.regMisFilesValue = 0;
                    panel22.BackgroundImage = Image.FromFile(imagePath1);
                    panel22.BackgroundImage.Tag = Path.GetFullPath(imagePath1);
                }
                else
                {
                }
            }
        }

        private void label35_Click(object sender, EventArgs e)
        {
            Mainform.MyForm.openPage7();
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

        private void panel28_Click(object sender, EventArgs e)
        {

        }

        private void panel29_Click(object sender, EventArgs e)
        {

        }

        private void panel30_Click(object sender, EventArgs e)
        {

        }

        private void panel31_Click(object sender, EventArgs e)
        {

        }

        private void panel33_Click(object sender, EventArgs e)
        {

        }

        private void panel32_Click(object sender, EventArgs e)
        {

        }

        private void panel26_Click(object sender, EventArgs e)
        {
            Mainform.MyForm.openPage7();
        }
        private void SetColumnWidths(TableLayoutPanel tableLayoutPanel, int value1, int value2, int value3, int value4, int value5, int value6)
        {
            int totalValue = int.Parse(CDrive.availableC.Split(' ')[0]);
            int sumOfDynamicValues = 0;
            if (RegistryValue.regDesValue != 5)
            {
                sumOfDynamicValues = sumOfDynamicValues + value1;
            }
            if (RegistryValue.regDocValue != 5)
            {
                sumOfDynamicValues = sumOfDynamicValues + value2;
            }
            if (RegistryValue.regPicValue != 5)
            {
                sumOfDynamicValues = sumOfDynamicValues + value3;
            }
            if (RegistryValue.regVidValue == 2 || RegistryValue.regVidValue == 3 || RegistryValue.regVidValue == 4)
            {
                sumOfDynamicValues = sumOfDynamicValues + value4;
            }
            if (RegistryValue.regDowValue == 2 || RegistryValue.regDowValue == 3 || RegistryValue.regDowValue == 4)
            {
                sumOfDynamicValues = sumOfDynamicValues + value5;
            }
            if (RegistryValue.regMusValue == 2 || RegistryValue.regMusValue == 3 || RegistryValue.regMusValue == 4)
            {
                sumOfDynamicValues = sumOfDynamicValues + value6;
            }
            double percentage = 0.0;
            int value7 = 0; 
            percentage = (double)sumOfDynamicValues / totalValue * 100;
            if (sumOfDynamicValues > totalValue)
            {
                value7 = 0;
                percentage = (double)sumOfDynamicValues / totalValue * 100;
            }
            else
            {
                value7 = totalValue - sumOfDynamicValues;
                sumOfDynamicValues = sumOfDynamicValues + value7;
                percentage = (double)sumOfDynamicValues / totalValue * 100;
            }
            label21.Text = value7 + "GB Available";
            tableLayoutPanel.ColumnStyles[0].Width = 0;
            tableLayoutPanel.ColumnStyles[1].Width = 0;
            tableLayoutPanel.ColumnStyles[2].Width = 0;
            tableLayoutPanel.ColumnStyles[3].Width = 0;
            tableLayoutPanel.ColumnStyles[4].Width = 0;
            tableLayoutPanel.ColumnStyles[5].Width = 0;
            tableLayoutPanel.ColumnStyles[6].Width = 0;
            if (RegistryValue.regDesValue != 5)
            {
                tableLayoutPanel.ColumnStyles[0].Width = (float)(value1 / (double)sumOfDynamicValues * percentage);
            }
            if (RegistryValue.regDocValue != 5)
            {
                tableLayoutPanel.ColumnStyles[1].Width = (float)(value2 / (double)sumOfDynamicValues * percentage);
            }
            if (RegistryValue.regPicValue != 5)
            {
                tableLayoutPanel.ColumnStyles[2].Width = (float)(value3 / (double)sumOfDynamicValues * percentage);
            }
            if (RegistryValue.regVidValue == 2 || RegistryValue.regVidValue == 3 || RegistryValue.regVidValue == 4)
            {
                tableLayoutPanel.ColumnStyles[3].Width = (float)(value4 / (double)sumOfDynamicValues * percentage);
            }
            if (RegistryValue.regDowValue == 2 || RegistryValue.regDowValue == 3 || RegistryValue.regDowValue == 4)
            {
                tableLayoutPanel.ColumnStyles[4].Width = (float)(value5 / (double)sumOfDynamicValues * percentage);
            }
            if (RegistryValue.regMusValue == 2 || RegistryValue.regMusValue == 3 || RegistryValue.regMusValue == 4)
            {
                tableLayoutPanel.ColumnStyles[5].Width = (float)(value6 / (double)sumOfDynamicValues * percentage);
            }
            if (value7 > 0.0)
            {
                tableLayoutPanel.ColumnStyles[6].Width = (float)(value7 / (double)sumOfDynamicValues * percentage);
            }
            else
            {
                tableLayoutPanel.ColumnStyles[6].Width = 0;
            }
            tableLayoutPanel.CellPaint += (sender, e) =>
            {
                if (e.Column < 7)
                {
                    e.Graphics.FillRectangle(new SolidBrush(GetColumnColor(e.Column)), e.CellBounds);
                }
            };
        }
        private Color GetColumnColor(int columnIndex)
        {
            // Define your custom color logic based on the columnIndex
            switch (columnIndex)
            {
                case 0:
                    return Color.FromArgb(255, 0, 89, 154);
                case 1:
                    return Color.FromArgb(255, 226, 75, 38);
                case 2:
                    return Color.FromArgb(255, 143, 175, 62);
                case 3:
                    return Color.FromArgb(255, 164, 38, 114);
                case 4:
                    return Color.FromArgb(255, 250, 175, 64);
                case 5:
                    return Color.FromArgb(255, 156, 109, 255);
                case 6:
                    return Color.FromArgb(224, 224, 224);
                default:
                    return Color.Gray;
            }
        }
    }
}
