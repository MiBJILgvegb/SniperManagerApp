using iniFiles;
using Steamworks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SniperManagerApp
{
    public partial class AddTargetsListForm : Form
    {
        public AddTargetsListForm()
        {
            InitializeComponent();
        }

        private void saveSettings()
        {
            int targetsCount=0;
            IniFiles INI = new IniFiles(ConfigurationManager.AppSettings.Get("iniPath"));

            if (INI.KeyExists("targetslist", "targetscount")) { targetsCount = int.Parse(INI.ReadINI("targetslist", "targetscount")); }


            targetsCount++;
            if (lTargetID.Text.Length > 0)
            {
                INI.Write("targetslist", $"target{targetsCount}", lTargetID.Text);
                INI.Write($"ID{lTargetID.Text}", "platform", "steam");

                if (clbGames.CheckedItems.Count > 0)
                {
                    string games = "";
                    foreach(string game in clbGames.CheckedItems)
                    {
                        if (games.Length == 0) { games = game; }
                        else { games += $",{game}"; }
                    }
                    INI.Write($"ID{lTargetID.Text}", "games", games);
                }
                INI.Write($"ID{lTargetID.Text}", "iswatching", "true");
            }
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.saveSettings();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.saveSettings();
            this.Close();
        }
        private void FillTargetInfo(string htmlString)
        {
            UpdateTargetNickname(htmlString);
            UpdateTargetAvatar(htmlString);
        }
        private void UpdateTargetNickname(string htmlString)
        {
            string name = SteamURL.ExtractNameFromSteamHtmlString(htmlString);

            lTargetNickname.Text = name;
        }
        private void UpdateTargetAvatar(string htmlString)
        {
            string picturePath = Path.GetTempPath() + "\\opponent.jpg";
            string pictureLink = "";
            using WebClient web = new WebClient();

            try
            {
                pictureLink = SteamURL.ExtractProfilePictureUrlFromSteamHtmlString(htmlString);
            }
            catch(Exception ex) { }
            if (pictureLink != "")
            {
                web.DownloadFile(pictureLink, picturePath);
                pbTargetIcon.Image = new Bitmap(picturePath);
            }
            else { pbTargetIcon.Image = (Image)Properties.Resources.ResourceManager.GetObject("na"); }

            
        }

        private void tbProfilePage_TextChanged(object sender, EventArgs e)
        {
            long id=0;

            if(rbtnSteam.Checked) { id = long.Parse(SteamURL.GetSteamIDFromURL(tbProfilePage.Text)); }

            //long steamId = long.Parse(SteamURL.GetSteamIDFromURL(tbProfilePage.Text));
            lTargetID.Text = id.ToString();

            string htmlString = SteamURL.GetSteamPageHtml(id);
            FillTargetInfo(htmlString);
        }
        private void FillclbGames()
        {
            foreach(string game in Pointers.ALL_WATCHING_GAMES)
            {
                clbGames.Items.Add(game);
            }
            //clbGames.SelectionMode = SelectionMode.MultiSimple; 
            clbGames.SetItemChecked(0, true);
        }
        private void AddTargetsListForm_Load(object sender, EventArgs e)
        {
            FillclbGames();
        }
    }
}
