using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Specialized;
using SniperManagerApp.Properties;
using iniFiles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.IO;
using System.Diagnostics;

namespace SniperManagerApp
{
    public partial class pathForm : Form
    {
        //public string tekkenDefaultSavesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"\\TekkenGame\\Saved\\SaveGames\\TEKKEN7\\");
        private void saveSettings()
        {
            IniFiles INI = new IniFiles(ConfigurationManager.AppSettings.Get("iniPath"));
            INI.Write("path", "savesPath", this.tbPath.Text);
            INI.Write("player", "id", this.cbPlayerID.Text);
        }
        
        public pathForm()
        {
            InitializeComponent();
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.saveSettings();
            this.Close();
        }

        private void paths_Load(object sender, EventArgs e)
        {
            IniFiles INI = new IniFiles(ConfigurationManager.AppSettings.Get("iniPath"));
            string currentSavesPath = "";
            if (INI.KeyExists("path", "savesPath")) { 
                currentSavesPath = INI.ReadINI("path", "savesPath");
                this.folderSavesDialog.SelectedPath = currentSavesPath;
            }
            else { currentSavesPath = Environment.CurrentDirectory; }
            this.tbPath.Text = currentSavesPath;
            this.folderSavesDialog.SelectedPath= currentSavesPath;

            string[] idsfolders = Directory.GetDirectories(INI.ReadINI("path", "tekkenDefaultSavepath"));
            int index = 0;
            foreach (string id in idsfolders)
            {
                idsfolders[index] = id.Split('\\').Last();
                index++;
            }
            this.cbPlayerID.Items.AddRange(idsfolders);

            if (!INI.KeyExists("player", "id"))
            {
                if (idsfolders.Count() > 1)
                {
                    this.gbPlayerID.Visible = true;
                }
                else
                {
                    this.cbPlayerID.SelectedText = idsfolders[0].Split('\\').Last();
                }
            }
            else 
            {
                this.gbPlayerID.Visible = true;
                this.cbPlayerID.SelectedIndex=this.cbPlayerID.FindString(INI.ReadINI("player", "id"));
            }
            
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.saveSettings();
        }

        private void btnPaths_Click(object sender, EventArgs e)
        {
            if (folderSavesDialog.ShowDialog() == DialogResult.OK)
            {
                this.tbPath.Text = folderSavesDialog.SelectedPath;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if(tbPath.Text.Length>0) Process.Start(tbPath.Text);
        }
    }
}
