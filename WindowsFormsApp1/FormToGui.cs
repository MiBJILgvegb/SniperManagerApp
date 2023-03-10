using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SniperManagerApp
{
    internal class FormToGui
    {
        internal static RadioButton[] radioButtons = { MainWindow.mainWindow.rbLastGames1, MainWindow.mainWindow.rbLastGames2, MainWindow.mainWindow.rbLastGames3, MainWindow.mainWindow.rbLastGames4, MainWindow.mainWindow.rbLastGames5, MainWindow.mainWindow.rbLastGames6, MainWindow.mainWindow.rbLastGames7, MainWindow.mainWindow.rbLastGames8, MainWindow.mainWindow.rbLastGames9, MainWindow.mainWindow.rbLastGames10 };
        internal static CheckBox[] checkBoxes = { MainWindow.mainWindow.chbLastGames1, MainWindow.mainWindow.chbLastGames2, MainWindow.mainWindow.chbLastGames3, MainWindow.mainWindow.chbLastGames4, MainWindow.mainWindow.chbLastGames5, MainWindow.mainWindow.chbLastGames6, MainWindow.mainWindow.chbLastGames7, MainWindow.mainWindow.chbLastGames8, MainWindow.mainWindow.chbLastGames9, MainWindow.mainWindow.chbLastGames10 };
        internal static PictureBox[] pictureBoxes = { MainWindow.mainWindow.pbLastGames1, MainWindow.mainWindow.pbLastGames2, MainWindow.mainWindow.pbLastGames3, MainWindow.mainWindow.pbLastGames4, MainWindow.mainWindow.pbLastGames5, MainWindow.mainWindow.pbLastGames6, MainWindow.mainWindow.pbLastGames7, MainWindow.mainWindow.pbLastGames8, MainWindow.mainWindow.pbLastGames9, MainWindow.mainWindow.pbLastGames10 };
        internal static Dictionary<string, ListBox> listBoxes = new Dictionary<string, ListBox> { { "steamid", MainWindow.mainWindow.lbEnemyListSteamID },{ "name",MainWindow.mainWindow.lbEnemyListName },{ "character", MainWindow.mainWindow.lbEnemyListCharacter },{ "rank", MainWindow.mainWindow.lbEnemyListRank } };
        
        //public static void PrintEnemyName(string enemy) { Gui.TextBox_SetText(MainWindow.mainWindow.tbEnemyListSteamID, enemy); }
        //=================================================================
        public static void ClearCharactesList_Save() { Gui.Clear(MainWindow.mainWindow.lvCharactersSave); }
        public static void ClearCharactesList_Load() { Gui.Clear(MainWindow.mainWindow.lvCharactersLoad); }
        //------------------------------------------------------------------
        public static void ClearRanksList_Save() { Gui.Clear(MainWindow.mainWindow.lvRanksSave); }
        public static void ClearRanksList_Load() { Gui.Clear(MainWindow.mainWindow.lvRanksLoad); }
        //=================================================================
        public static void ClearCharactersFilter_Save() { Gui.Clear(MainWindow.mainWindow.cbCharacterFilterSave); }
        public static void ClearCharactersFilter_Load() { Gui.Clear(MainWindow.mainWindow.cbCharacterFilterLoad); }
        //------------------------------------------------------------------
        public static void ClearRanksFilter_Save() { Gui.Clear(MainWindow.mainWindow.cbRankFilterSave); }
        public static void ClearRanksFilter_Load() { Gui.Clear(MainWindow.mainWindow.cbRankFilterLoad); }
        //------------------------------------------------------------------
        public static void ClearCharactersList_Manual() { Gui.Clear(MainWindow.mainWindow.lvManualMemoryCharactersList); }
        public static void ClearRanksList_CharacterInfo() { Gui.Clear(MainWindow.mainWindow.cbManualMemoryRankFilter); }
        public static void ClearWinstreakList_CharacterInfo() { Gui.Clear(MainWindow.mainWindow.cbManualMemoryWinstreak); }
        //==================================================================
        public static void FillCharactersFilter_Save(string[] cbItems) { Gui.Fill(MainWindow.mainWindow.cbCharacterFilterSave, cbItems); }
        public static void FillCharactersFilter_Load(string[] cbItems) { Gui.Fill(MainWindow.mainWindow.cbCharacterFilterLoad, cbItems); }
        //------------------------------------------------------------------
        public static void FillRanksFilter_Save(string[] cbItems) { Gui.Fill(MainWindow.mainWindow.cbRankFilterSave, cbItems); }
        public static void FillRanksFilter_Load(string[] cbItems) { Gui.Fill(MainWindow.mainWindow.cbRankFilterLoad, cbItems); }
        internal static void FillRanksList_ManualInfoEdit(string[] cbItems) { Gui.Fill(MainWindow.mainWindow.cbManualMemoryRankFilter, cbItems); }
        //=================================================================
        public static void FillCharactersList_Save(ListViewItem[] lvis,ImageList imageList,int lvilType)
        {
            Gui.ListView_AddImageList(MainWindow.mainWindow.lvCharactersSave,imageList, lvilType);
            Gui.Add(MainWindow.mainWindow.lvCharactersSave, lvis);
        }
        public static void FillCharactersList_Load(ListViewItem[] lvis,ImageList imageList,int lvilType)
        {
            Gui.ListView_AddImageList(MainWindow.mainWindow.lvCharactersLoad,imageList, lvilType);
            Gui.Add(MainWindow.mainWindow.lvCharactersLoad, lvis);
        }
        //------------------------------------------------------------------
        public static void FillRanksList_Save(ListViewItem[] lvis, ImageList imageList, int lvilType)
        {
            Gui.ListView_AddImageList(MainWindow.mainWindow.lvRanksSave, imageList, lvilType);
            Gui.Add(MainWindow.mainWindow.lvRanksSave, lvis);
        }
        public static void FillRanksList_Load(ListViewItem[] lvis, ImageList imageList, int lvilType)
        {
            Gui.ListView_AddImageList(MainWindow.mainWindow.lvRanksLoad, imageList, lvilType);
            Gui.Add(MainWindow.mainWindow.lvRanksLoad, lvis);
        }
        //------------------------------------------------------------------
        internal static void FillCharactersList_Manual(ListViewItem[] lvis, ImageList imageList, int lvilType)
        {
            Gui.ListView_AddImageList(MainWindow.mainWindow.lvManualMemoryCharactersList, imageList, lvilType);
            Gui.Add(MainWindow.mainWindow.lvManualMemoryCharactersList, lvis);
        }
        //==================================================================
        public static void SetCharactersListSelectIndex_Save(int index) { Gui.SelectIndex(MainWindow.mainWindow.lvCharactersSave, index); }
        public static void SetCharactersListSelectIndex_Load(int index) { Gui.SelectIndex(MainWindow.mainWindow.lvCharactersLoad, index); }
        //------------------------------------------------------------------
        public static void SetRanksListSelectIndex_Save(int index) { Gui.SelectIndex(MainWindow.mainWindow.lvRanksSave, index); }
        public static void SetRanksListSelectIndex_Load(int index) { Gui.SelectIndex(MainWindow.mainWindow.lvRanksLoad, index); }
        //=================================================================
        public static void SetCharactersFilterSelectIndex_Save(int index) { Gui.SelectIndex(MainWindow.mainWindow.cbCharacterFilterSave, index); }
        public static void SetCharactersFilterSelectIndex_Load(int index) { Gui.SelectIndex(MainWindow.mainWindow.cbCharacterFilterLoad, index); }
        //------------------------------------------------------------------
        public static void SetRanksFilterSelectIndex_Save(int index) { Gui.SelectIndex(MainWindow.mainWindow.cbRankFilterSave, index); }
        public static void SetRanksFilterSelectIndex_Load(int index) { Gui.SelectIndex(MainWindow.mainWindow.cbRankFilterLoad, index); }
        public static void SetRanksListSelectIndex_ManualInfoEdit(int index) { Gui.SelectIndex(MainWindow.mainWindow.cbManualMemoryRankFilter, index); }
        public static string GetCharacterName_Load() { return Gui.ListView_GetSelectedToolTip(MainWindow.mainWindow.lvCharactersLoad); }
        internal static void ManualMemoryPlayerName_Print(string name) { Gui.Text(MainWindow.mainWindow.tbPlayerName, name); }
        internal static void ManualMemoryCharacterInfoControls_Show() { Gui.VisibleON(MainWindow.mainWindow.gbCharacterInfo); }
        internal static void ManualMemoryCharacterInfoControls_Hide() { Gui.VisibleOFF(MainWindow.mainWindow.gbCharacterInfo); }
        internal static void ManualMemoryPlayerSteamID_Print(long steamid) { Gui.Text(MainWindow.mainWindow.tbSteamID,steamid.ToString()); }
        internal static bool ManualMemoryIsSelectedCharacter() { return Gui.ListView_IsSelectedItem(MainWindow.mainWindow.lvManualMemoryCharactersList); }
        internal static string ManualMemoryGetSelectedCharacter() { return Gui.ListView_GetSelectedToolTip(MainWindow.mainWindow.lvManualMemoryCharactersList); }
        internal static int ManualMemoryGetSelectedRank() { return Gui.GetSelectedIndex(MainWindow.mainWindow.cbManualMemoryRankFilter); }
        internal static int ManualMemoryGetSelectedWinsCount() { return Gui.GetSelectedIndex(MainWindow.mainWindow.cbManualMemoryCurrentWins); }
        internal static int ManualMemoryGetSelectedWinstreakCount() { return Gui.GetSelectedIndex(MainWindow.mainWindow.cbManualMemoryWinstreak); }
        internal static int ManualMemoryGetSelectedSubstitutionCharacter() { return Gui.GetSelectedIndex(MainWindow.mainWindow.cbCharacterSubstitutionList); }
        internal static void ManualMemoryCharacterWins_Clear() { Gui.Clear(MainWindow.mainWindow.cbManualMemoryCurrentWins); }
        internal static void ManualMemoryCharacterWinstreak_Clear() { Gui.Clear(MainWindow.mainWindow.cbManualMemoryWinstreak); }
        internal static void ManualMemoryCharacterWins_Fill(string[] wins) { Gui.Fill(MainWindow.mainWindow.cbManualMemoryCurrentWins, wins); }
        internal static void ManualMemoryCharacterWinstreak_Fill(string[] wins) { Gui.Fill(MainWindow.mainWindow.cbManualMemoryWinstreak, wins); }
        internal static void ManualMemoryCharacterWins_Select(int index) { Gui.SelectIndex(MainWindow.mainWindow.cbManualMemoryCurrentWins, index); }
        internal static void ManualMemoryCharacterWinstreak_Select(int index) { Gui.SelectIndex(MainWindow.mainWindow.cbManualMemoryWinstreak, index); }
        internal static void ManualMemoryCharacterEOLLine_Select(int index) { Gui.SelectIndex(MainWindow.mainWindow.cbManualMemoryELOLine,index); }
        internal static void ManualMemoryMatchHistory_SetHistory(byte[] matchHistory)
        {
            int index = 0;
            foreach(int match in matchHistory)
            {
                switch (match)
                {//1 - win, 2 - lose
                    case 0:
                        Gui.Uncheck(radioButtons[index]);
                        Gui.Disable(checkBoxes[index]);
                        Gui.PictureBox_SetPicture(pictureBoxes[index], (Image)Properties.Resources.ResourceManager.GetObject("none"));
                        break;
                    case 1:
                        Gui.Check(radioButtons[index]);
                        Gui.Enable(checkBoxes[index]);
                        Gui.Check(checkBoxes[index]);
                        Gui.PictureBox_SetPicture(pictureBoxes[index], (Image)Properties.Resources.ResourceManager.GetObject("win"));
                        break; 
                    case 2:
                        Gui.Check(radioButtons[index]);
                        Gui.Enable(checkBoxes[index]);
                        Gui.Uncheck(checkBoxes[index]);
                        Gui.PictureBox_SetPicture(pictureBoxes[index], (Image)Properties.Resources.ResourceManager.GetObject("lose"));
                        break;
                }
                index++;
            }
        }
        internal static void ManualMemoryMatchHistory_ChangeHistory(RadioButton radioButton)
        {
            int index=Array.IndexOf(radioButtons, radioButton);
            if (radioButton.Checked)
            {
                Gui.Enable(checkBoxes[index]);
                Gui.Uncheck(checkBoxes[index]);
                Gui.PictureBox_SetPicture(pictureBoxes[index], (Image)Properties.Resources.ResourceManager.GetObject("lose"));
            }
            else
            {
                Gui.Disable(checkBoxes[index]);
                Gui.Uncheck(checkBoxes[index]);
                Gui.PictureBox_SetPicture(pictureBoxes[index], (Image)Properties.Resources.ResourceManager.GetObject("none"));
            }
        }
        internal static void ManualMemoryMatchHistory_ChangeHistory(CheckBox checkBox)
        {
            int index = Array.IndexOf(checkBoxes, checkBox);
            if (checkBox.Checked) { Gui.PictureBox_SetPicture(pictureBoxes[index], (Image)Properties.Resources.ResourceManager.GetObject("win")); }
            else { Gui.PictureBox_SetPicture(pictureBoxes[index], (Image)Properties.Resources.ResourceManager.GetObject("lose")); }
        }
        internal static byte[] ManualMemoryMatchHistory_GetNewHistory()
        {
            byte[] newHistory=new byte[radioButtons.Length];
            int index = 0;
            foreach(RadioButton radioButton in radioButtons )
            {
                if (Gui.IsChecked(checkBoxes[index])) { newHistory[index] = 1; }
                else { newHistory[index] = 2; }
                if (!Gui.IsChecked(radioButton)) newHistory[index] = 0;
                index++;
            }

            return newHistory;
        }
        internal static void ManualMemorySubstitution_ListShow()
        {
            if (Gui.IsChecked(MainWindow.mainWindow.chbCharacterSubstitution))
            {
                Gui.Enable(MainWindow.mainWindow.cbCharacterSubstitutionList);
                Gui.Enable(MainWindow.mainWindow.bCharacterSubstitution);
            }
            else 
            { 
                Gui.Disable(MainWindow.mainWindow.cbCharacterSubstitutionList);
                Gui.Disable(MainWindow.mainWindow.bCharacterSubstitution);
            }
        }
        internal static void ManualMemorySubstitution_FillCharactersList(string[] characters) { Gui.Fill(MainWindow.mainWindow.cbCharacterSubstitutionList,characters); }
        internal static void ManualMemorySubstitution_ClearCharactersList() { Gui.Clear(MainWindow.mainWindow.cbCharacterSubstitutionList); }
        internal static void ManualMemoryWinstreak_Fill(string[] wins) { Gui.Fill(MainWindow.mainWindow.cbManualMemoryWinstreak, wins); }
        internal static string ManualMemoryWinstreak_Get() { return Gui.GetSelectedItem(MainWindow.mainWindow.cbManualMemoryWinstreak); }
        internal static void ManualMemoryTest_Show() { Gui.VisibleON(MainWindow.mainWindow.tbTest); }
        internal static void ManualMemoryTest_Text(string text) { Gui.TextAppend(MainWindow.mainWindow.tbTest,text); }
        //=================================================================
        internal static void T7Online_ClearAll()
        {
            foreach(var listBox in listBoxes)
            {
                Gui.ClearAsync(listBox.Value);
            }
        }
        internal static void T7Online_FillLobbyInfo(Dictionary<string, string> lobbyInfo)
        {
            foreach (var lobby in lobbyInfo)
            {
                Gui.FillAsync(listBoxes[lobby.Key], lobby.Value);
            }
        }
        internal static void T7Online_Logs(string log){ Gui.TextAsync(MainWindow.mainWindow.tbNewEmenyInfo,log); }
        internal static void T7Online_Logs(string[] logs)
        {
            foreach(string log in logs) { T7Online_Logs(log); }
        }
        internal static void T7Online_LobbyInfoSynchronizationSelection(int index)
        {
            foreach (var listBox in listBoxes)
            {
                Gui.SelectAsync(listBox.Value, index);
            }
        }
        internal static void T7Online_PageTitle(string title) { Gui.TitleAsync(MainWindow.mainWindow.tpT7Online,title); }
        internal static int T7Online_LobbySelected()
        {
            return Gui.GetSelectedIndexAsync(listBoxes["name"]);
        }
    }
}