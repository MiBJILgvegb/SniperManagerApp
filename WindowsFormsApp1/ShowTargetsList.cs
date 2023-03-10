using iniFiles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web.UI.WebControls;
using System.IO;
using System.Net;

namespace SniperManagerApp
{
    public partial class ShowTargetsList : Form
    {
        private string[] toolTips= Array.Empty<string>();
        public ShowTargetsList()
        {
            InitializeComponent();
        }
        private void saveSettings()
        {
            int targetsCount = 0;
            IniFiles INI = new IniFiles(ConfigurationManager.AppSettings.Get("iniPath"));

            if (lbTargetsList.SelectedItems.Count > 0)
            {
                string games = "";
                foreach (string game in clbGames.CheckedItems)
                {
                    if (games.Length == 0) { games = game; }
                    else { games += $",{game}"; }
                }
                INI.Write($"ID{lID.Text}", "games", games);

                INI.Write($"ID{lID.Text}", "iswatching", cbIsWatching.Checked.ToString());
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.saveSettings();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.saveSettings();
        }
        private void FillclbGames(string games)
        {
            string[] gamesArr = games.Split(',');
            foreach (string game in Pointers.ALL_WATCHING_GAMES)
            {
                int ind= clbGames.Items.Add(game);
                if (gamesArr.Contains(game)) { clbGames.SetItemChecked(ind, true); }
            }
        }
        private void ShowTargetsList_Load(object sender, EventArgs e)
        {
            IniFiles INI = new IniFiles(ConfigurationManager.AppSettings.Get("iniPath"));

            int i = 1;
            string id="",platform="",game = "";

            while (INI.KeyExists("targetslist", $"target{i}"))
            {
                try
                {
                    id = INI.ReadINI("targetslist", $"target{i}");
                }
                catch { }

                string htmlString = SteamURL.GetSteamPageHtml(id);
                string name = SteamURL.ExtractNameFromSteamHtmlString(htmlString);

                lbTargetsList.Items.Add(name);
                Array.Resize(ref toolTips, lbTargetsList.Items.Count);
                toolTips[toolTips.Length - 1] = id;
                
                i++;
            }

            if (lbTargetsList.Items.Count > 0) { lbTargetsList.SelectedIndex = 0; }

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
            catch (Exception ex) { }
            if (pictureLink != "")
            {
                web.DownloadFile(pictureLink, picturePath);
                pbTargetIcon.Image = new Bitmap(picturePath);
            }
            else { pbTargetIcon.Image = (Image)Properties.Resources.ResourceManager.GetObject("na"); }


        }
        private void lbTargetsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(lbTargetsList.SelectedIndex != -1)
            {
                IniFiles INI = new IniFiles(ConfigurationManager.AppSettings.Get("iniPath"));

                string id = "", platform = "", games = "";
                bool iswatching = false;

                id = toolTips[lbTargetsList.SelectedIndex];
                string htmlString = SteamURL.GetSteamPageHtml(id);
                try
                {
                    platform = INI.ReadINI($"ID{id}", "platform");
                    games = INI.ReadINI($"ID{id}", "games");
                    iswatching = INI.ReadINI($"ID{id}", "iswatching").Equals("1");
                }
                catch { }

                lID.Text= id;
                lNickname.Text= lbTargetsList.SelectedItem.ToString();
                cbIsWatching.Checked = INI.ReadINI($"ID{lID.Text}", "iswatching").ToLower().Equals("true");
                UpdateTargetAvatar(htmlString);


                FillclbGames(games);

                gbTargetInfo.Visible= true;
            }
            else { gbTargetInfo.Visible = false; }
        }
    }
}
