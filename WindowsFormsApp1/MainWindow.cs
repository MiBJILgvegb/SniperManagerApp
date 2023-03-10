using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Configuration;
using iniFiles;
using System.Diagnostics;
using Steamworks;
using System.Threading;
using System.Collections.Generic;
using System.Reflection;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Runtime.InteropServices;

namespace SniperManagerApp
{
    public partial class MainWindow : Form
    {
        public static MainWindow mainWindow;
        public static CharacterInfoForm characterInfoForm;
        //public static CommentWindow commentWindow;
        private static Thread mainThread;

        public static IntPtr tekkenWindowHandle;
        public static long userSteamId;
        public static long lastFoundSteamId = -1;
        public static bool isSteamIdFound = false; // helps keep track of  lastFoundSteamId
        public static string lastNameInPlayerlist;
        public static string lastFoundName = "";
        public static bool silentMode = true;
        public static bool fullLocation = false;
        public static bool isSteamAPI = true;
        private static bool mainThreadStop = false;

        public static long steamModulePointer;
        public static long tekkenModulePointer;
        public static long tekkenMemoryPointer;
        public static long tekkenPlayerNameMemoryPointer;
        public static long screenModePointer;
        public static long secondsRemainingMessagePointer;

        public static CallResult<LobbyMatchList_t> CallResultLobbyMatchList;
        public static List<PlayerLobbyInfo> ListOfPlayerLobbies;
        //public static PlayerLobbyInfo SelectedPlayer; //JoinLobby()
        public static int OnlineModeFilter;
        public static int OnlineModeCountFilter;

        private string tabPageTitle = "";
        private int globalDelay = 1;

        private string defaultSavesPath = Environment.CurrentDirectory;
        internal int lvilType = int.Parse(ConfigurationManager.AppSettings.Get("lvImageListType"));
        private bool T7Autostart=false;
        private bool T7RestartOnload = false;
        private bool T7CloseOnExit = false;
        private string T7DefaultSavepath;
        private string T7PlayerID;

        public MainWindow()
        {
            InitializeComponent();
            InitializeINIParameters();
            mainWindow = this;
        }
        //получаем настройки из ini-файла
        private void InitializeINIParameters()
        {
            IniFiles INI = new IniFiles(ConfigurationManager.AppSettings.Get("iniPath"));

            if (INI.KeyExists("path", "savesPath")) { defaultSavesPath = INI.ReadINI("path", "savesPath"); }

            //определяем размер отображаемых иконок
            if (INI.KeyExists("components", "lvImageListType"))
            {
                lvilType = int.Parse(INI.ReadINI("components", "lvImageListType"));
            }

            //определяем положения переключателей
            string isChecked = ConfigurationManager.AppSettings.Get("defaultTekkenAutostart").ToLower();
            if (INI.KeyExists("tekken7", "autostart"))
            {
                isChecked = INI.ReadINI("tekken7", "autostart").ToLower();
            }
            T7Autostart = isChecked.Equals("true");

            isChecked = ConfigurationManager.AppSettings.Get("defaultTekkenRestartOnload").ToLower();
            if (INI.KeyExists("tekken7", "restartonload"))
            {
                isChecked = INI.ReadINI("tekken7", "restartonload").ToLower();
            }
            T7RestartOnload = isChecked.Equals("true");

            isChecked = ConfigurationManager.AppSettings.Get("defaultTekkenCloseOnExit").ToLower();
            if (INI.KeyExists("tekken7", "closeonexit"))
            {
                isChecked = INI.ReadINI("tekken7", "closeonexit").ToLower();
            }
            T7CloseOnExit = isChecked.Equals("true");

            T7DefaultSavepath = INI.ReadINI("path", "tekkenDefaultSavepath");
            T7PlayerID = INI.ReadINI("player", "id");
        }
        //получаем изображение из ресурсов
        internal Image CreateImage(string image)
        {
            return (Image)Properties.Resources.ResourceManager.GetObject(image);
        }
        //создаем список изображений
        internal ImageList CreateImagesList(string[] imagesNames, int[] size)
        {
            ImageList imageList = new ImageList
            {
                ImageSize = new Size(size[0], size[1])
            };
            foreach (string imageName in imagesNames)
            {
                Image image = CreateImage(imageName);
                imageList.Images.Add(imageName, image);
            }

            return imageList;
        }
        internal ListViewItem[] CreateListViewItemList(string[] text)
        {
            ListViewItem[] listViewItems = new ListViewItem[text.Length];
            int index = 0;
            foreach (string t in text)
            {
                listViewItems[index] = new ListViewItem(" ") { ImageIndex = index, ToolTipText = t };
                index++;
            }

            return listViewItems;
        }
        //подготавливаем список изображений персонажей
        internal ImageList PrepareCharactersImagesList(string[] toolTips)
        {
            string[] characters = new string[toolTips.Length];
            int index = 0;
            foreach(string character in toolTips)
            {
                characters[index] = character.Replace('-', '_');
                index++;
            }
            return CreateImagesList(characters, new int[]{ int.Parse(ConfigurationManager.AppSettings.Get("lvilWidth")), int.Parse(ConfigurationManager.AppSettings.Get("lvilHeight")) });
        }

        //подготавливаем текстовый список персонажей
        internal ListViewItem[] PrepareCharactersList(string[] characters)
        {
            string[] charactersList = new string[characters.Length];
            int index = 0;
            foreach (string character in characters)
            {
                charactersList[index] = character.Replace('-', '_');
                index++;
            }

            return CreateListViewItemList(charactersList);
        }
       
        //------------------------------------------------------------------------------------------
        //заполняем фильтр персонажей
        private string[] PrepareCharacterTextList_Save()
        {
            string[] list = new string[Pointers.ALL_PLAYABLE_CHARACTERS.Length];
            int index = 0;
            foreach(string character in Pointers.ALL_PLAYABLE_CHARACTERS)
            {
                list[index]=character.Replace('-', ' ');
                index++;
            }

            return list;
        }
        private string[] PreapareCharacterTextList_Load()
        {
            string[] charactersList = Directory.GetDirectories(Path.Combine(defaultSavesPath,T7PlayerID) );

            if(charactersList.Length== 0) { return null; }

            string[] list = new string[charactersList.Length];
            int index = 0;
            foreach(string character in charactersList)
            {
                list[index]=new DirectoryInfo(character).Name;
                index++;
            }

            return list;
        }
        //заполняем список персонажей
        private void FillCharactersList_Save()
        {
            FormToGui.ClearCharactesList_Save();
            FormToGui.FillCharactersList_Save(PrepareCharactersList(Pointers.ALL_PLAYABLE_CHARACTERS), PrepareCharactersImagesList(Pointers.ALL_PLAYABLE_CHARACTERS), lvilType);
            FormToGui.SetCharactersListSelectIndex_Save(0);
        }
        private void FillCharacterFilter_Save()
        {
            FormToGui.ClearCharactersFilter_Save();
            FormToGui.FillCharactersFilter_Save(PrepareCharacterTextList_Save());
            FormToGui.SetCharactersFilterSelectIndex_Save(0);
        }
        private void FillCharacters_Save()
        {
            FillCharactersList_Save();
            FillCharacterFilter_Save();
        }
        private void FillCharactersList_Load()
        {
            string[] loadFilter = PreapareCharacterTextList_Load();
            if (loadFilter != null)
            {
                FormToGui.ClearCharactesList_Load();
                FormToGui.FillCharactersList_Load(PrepareCharactersList(loadFilter),PrepareCharactersImagesList(loadFilter), lvilType);
                FormToGui.SetCharactersListSelectIndex_Load(0);
            }
        }
        private void FillCharacterFilter_Load()
        {
            string[] loadFilter = PreapareCharacterTextList_Load();
            if (loadFilter != null)
            {
                FormToGui.ClearCharactersFilter_Load();
                FormToGui.FillCharactersFilter_Load(loadFilter);
                FormToGui.SetCharactersFilterSelectIndex_Load(0);
            }
            
        }
        private void FillCharacters_Load()
        {
            FillCharactersList_Load();
            FillCharacterFilter_Load();
        }
        //------------------------------------------------------------------------------------------

        //заполняем фильтр рангов
        private string[] PrepareRanksTextList_Load(string character)
        {
            string[] savedRanksList = Directory.GetDirectories(Path.Combine(defaultSavesPath,T7PlayerID, character));
            if (savedRanksList.Length == 0) { return null; }
            string[] ranksList = new string[savedRanksList.Length];

            int index = 0;
            foreach (string rank in savedRanksList)
            {
                ranksList[index] = Pointers.ALL_PLAYABLE_RANKS[int.Parse(new DirectoryInfo(rank).Name) - 1];
                index++;
            }
            return ranksList;
        }
        private ListViewItem[] PrepareRanksList(string[] ranks)
        {
            string[] ranksList = new string[ranks.Length];
            int index = 0;
            foreach (string rank in ranks)
            {
                if (Char.IsDigit(rank[0])) { ranksList[index] = "_" + rank; }
            }

            return CreateListViewItemList(ranksList);
        }
        private ImageList PrepareRanksImagesList(string[] toolTips)
        {
            string[] ranks = new string[Pointers.ALL_PLAYABLE_RANKS.Length];
            int index = 0;
            foreach (string rank in Pointers.ALL_PLAYABLE_RANKS)
            {
                ranks[index] = rank.Replace(' ', '_');
                if (Char.IsDigit(ranks[index][0])) { ranks[index] = "_" + ranks[index]; }
                index++;
            }
            return CreateImagesList(ranks, new int[] { int.Parse(ConfigurationManager.AppSettings.Get("lvilWidth")), int.Parse(ConfigurationManager.AppSettings.Get("lvilHeight")) });
        }
        private ImageList PrepareRanksImagesList_Load(string[] toolTips)
        {
            string[] ranks = new string[toolTips.Length];
            int index = 0;
            foreach (string rank in toolTips)
            {
                ranks[index] = rank.Replace(' ', '_');
                if (Char.IsDigit(ranks[index][0])) { ranks[index] = "_" + ranks[index]; }
                index++;
            }
            return CreateImagesList(ranks, new int[] { int.Parse(ConfigurationManager.AppSettings.Get("lvilWidth")), int.Parse(ConfigurationManager.AppSettings.Get("lvilHeight")) });
        }
        private void FillRanksFilter_Save()
        {
            FormToGui.ClearRanksFilter_Save();
            FormToGui.FillRanksFilter_Save(Pointers.ALL_PLAYABLE_RANKS);
            FormToGui.SetRanksFilterSelectIndex_Save(0);
        }
        
        private void FillRanksList_Save()
        {
            FormToGui.ClearRanksList_Save();
            FormToGui.FillRanksList_Save(PrepareRanksList(Pointers.ALL_PLAYABLE_RANKS), PrepareRanksImagesList(Pointers.ALL_PLAYABLE_RANKS), lvilType);
            FormToGui.SetRanksListSelectIndex_Save(0);
        }
        private void FillRanks_Save()
        {
            FillRanksList_Save();
            FillRanksFilter_Save();
        }

        private void FillRanksList_Load()
        {
            FormToGui.ClearRanksList_Load();
            FormToGui.FillRanksList_Load(PrepareRanksList(PrepareRanksTextList_Load(FormToGui.GetCharacterName_Load())), PrepareRanksImagesList_Load(PrepareRanksTextList_Load(FormToGui.GetCharacterName_Load())), lvilType);
            FormToGui.SetRanksListSelectIndex_Load(0);
        }
        private void FillRanksFilter_Load()
        {
            FormToGui.ClearRanksFilter_Load();
            FormToGui.FillRanksFilter_Load(PrepareRanksTextList_Load(FormToGui.GetCharacterName_Load()));
            FormToGui.SetRanksFilterSelectIndex_Load(0);
        }
        private void FillRanks_Load()
        {
            FillRanksList_Load();
            FillRanksFilter_Load();
        }

        //------------------------------------------------------------------------------------------

        private void TBT7Online_Write(string text)
        {
            /*tbT7Online.Invoke((MethodInvoker)delegate {
                //tbT7Online.Text+=text+"\r\n";
                tbT7Online.Text= text + "\r\n";
            });*/
        }
        private void TBT7Online_Write(List<PlayerLobbyInfo> theList)
        {
            String toBePrinted = "";
            int charactersPadding = 1 + Pointers.ALL_CHARACTERS.OrderByDescending(s => s.Length).First().Length;
            int rankPadding = 1 + Pointers.ALL_RANKS.OrderByDescending(s => s.Length).First().Length;
            theList = theList.OrderByDescending(x => Array.FindIndex(Pointers.ALL_RANKS, rank => rank == x.Rank)).ThenBy(x => x.Name).ToList();
            MainWindow.ListOfPlayerLobbies = theList;
            foreach (PlayerLobbyInfo player in theList)
            {
                String steamId = player.SteamId.ToString() + " ";
                String character = player.Character;
                String rank = player.Rank;
                String name = player.Name;
                String line = (steamId + character.PadRight(charactersPadding) + rank.PadRight(rankPadding) + name + "\r\n");
                toBePrinted += line;
            }
            //Gui.PrintToGuiPlayerList(toBePrinted);
            TBT7Online_Write(toBePrinted);

        }

        private void TBT7Online_Clear()
        {
            /*tbT7Online.Invoke((MethodInvoker)delegate {
                tbT7Online.Clear();
            });*/
        }

        private void StartMainThread()
        {
            mainThread = new Thread(() => RunMainThread());
            mainThread.IsBackground = true; // this makes sure the thread will be stopped after the gui is closed
            mainThread.Start();
        }
        private void RunMainThread()
        {
            if (!mainThreadStop)
            {
                InitEnemyList();
                InitGlobalVariables();
                LoadTargetProcess();
                if (isSteamAPI) { StartLobbyInfoThread(); }
                //EditTargetProcessLoop();
                
            }
            else { 
                //mainThread.Join();
                isSteamAPI = false;
                SteamworksAPI.Shutdown();
            }
        }
        private void TabControlFontChange()
        {
            string tpTitle = !tpT7Online.Text.Equals(tabPageTitle) ? tabPageTitle : "Searching...";
            //string tpTitle = "Searching...";
            //if(!tpT7Online.Text.Equals(tabPageTitle)) { tpTitle = tabPageTitle; }
            tpT7Online.Invoke(new Action(() => tpT7Online.Text = tpTitle));
        }
        private void InitEnemyList()
        {
            FormToGui.T7Online_ClearAll();
        }
        private void InitGlobalVariables()
        {
            MainWindow.CallResultLobbyMatchList = CallResult<LobbyMatchList_t>.Create(SteamworksAPI.MyCallbackLobbyMatchList);
            MainWindow.ListOfPlayerLobbies = new List<PlayerLobbyInfo>();
            //MainWindow.SelectedPlayer = null; // JoinLobby()
            MainWindow.OnlineModeFilter = LobbyListFilters.Ranked;
        }
        
        private void LoadTargetProcess()
        {
            /*bool b_THandle, b_tWHandle;
            b_THandle= InitTekkenHandle();
            //b_tWHandle= InitTekkenWindowHandle();
            if (b_THandle & b_tWHandle)
            {
                InitModuleAdresses();
            }*/

            InitSteamProcess();

            InitSteamworksAPI(); 
            
            //Tekken.CleanAllProcessMessages();
            //Gui.PrintLineToGuiConsole("Program ready!");
        }
        private bool InitTekkenHandle()
        {
            int secondsDelay = 1; bool ret = false;
            //while (!ProcessMemory.Attach(Pointers.TEKKEN_EXE_NAME))
            if (!ProcessMemory.Attach(Pointers.TEKKEN_EXE_NAME))
            {
                TBT7Online_Write($"Tekken not found. Trying again in {secondsDelay} seconds...");
                //Gui.PrintLineToGuiConsole($"Tekken not found. Trying again in {secondsDelay} seconds...");
                Thread.Sleep(secondsDelay * 1000);
            }
            else
            {
                TBT7Online_Write($"Tekken found! (pid = {ProcessMemory.Process.Id})");
                ret= true;
            }
            //Gui.PrintLineToGuiConsole($"Tekken found! (pid = {ProcessMemory.Process.Id})");
            return ret;
        }
        private bool InitTekkenWindowHandle()
        {
            int secondsDelay = 1; bool ret=false;
            //while (null == (tekkenWindowHandle = ProcessWindow.GetWindowHandle(Pointers.TEKKEN_WINDOW_NAME))|| !ProcessWindow.IsWindow(tekkenWindowHandle))
            if ((null == (tekkenWindowHandle = ProcessWindow.GetWindowHandle(Pointers.TEKKEN_WINDOW_NAME))) || (!ProcessWindow.IsWindow(tekkenWindowHandle)))
            {
                TBT7Online_Write($"Searching for Tekken window. Trying again in {secondsDelay} seconds...");
                //Gui.PrintLineToGuiConsole($"Searching for Tekken window. Trying again in {secondsDelay} seconds...");
                Thread.Sleep(secondsDelay * 1000); // milliseconds
            }
            else
            {
                TBT7Online_Write($"Tekken window found! ({tekkenWindowHandle})");
                ret= true;
            }
            //Gui.PrintLineToGuiConsole($"Tekken window found! ({tekkenWindowHandle})");
            return ret;
        }
        private void InitSteamProcess()
        {
            while (Process.GetProcessesByName(Pointers.STEAM_EXE_NAME).Length == 0) 
            { 
                FormToGui.T7Online_Logs("Не найден запущенный Steam.");
                Thread.Sleep(globalDelay * 1000);
            }
            FormToGui.T7Online_Logs("Steam is loaded.");
        }
        public void InitModuleAdresses()
        {
            int secondsDelay = 2;
            steamModulePointer = 0;
            while (steamModulePointer == 0)
            {
                steamModulePointer = ProcessMemory.GetModuleAddress(Pointers.STEAM_API_MODULE_EDITED_NAME);
                if (steamModulePointer != 0)
                    continue;
                steamModulePointer = ProcessMemory.GetModuleAddress(Pointers.STEAM_API_MODULE_NAME);
                if (steamModulePointer != 0)
                    continue;
                TBT7Online_Write($"Getting the Tekken Steam module base address. Trying again in {secondsDelay} seconds...");
                //Gui.PrintLineToGuiConsole($"Getting the Tekken Steam module base address. Trying again in {secondsDelay} seconds...");
                Thread.Sleep(secondsDelay * 1000); // milliseconds
            }
            tekkenModulePointer = 0;
            while (tekkenModulePointer == 0)
            {
                tekkenModulePointer = ProcessMemory.GetModuleAddress(Pointers.TEKKEN_MODULE_NAME);
                if (tekkenModulePointer != 0)
                    continue;
                TBT7Online_Write($"Getting the Tekken (inside the Tekken process) module base address. Trying again in {secondsDelay} seconds...");
                //Gui.PrintLineToGuiConsole($"Getting the Tekken (inside the Tekken process) module base address. Trying again in {secondsDelay} seconds...");
                Thread.Sleep(secondsDelay * 1000); // milliseconds
            }
            long userSteamIdAddress = ProcessMemory.GetDynamicAddress(steamModulePointer + Pointers.STEAM_ID_USER_STATIC_POINTER, Pointers.STEAM_ID_USER_POINTER_OFFSETS);
            userSteamId = ProcessMemory.ReadMemory<long>(userSteamIdAddress);
            screenModePointer = ProcessMemory.GetDynamicAddress(tekkenModulePointer + Pointers.SCREEN_MODE_STATIC_POINTER, Pointers.SCREEN_MODE_POINTER_OFFSETS);
        }

        private void InitSteamworksAPI()
        {
            while (!SteamworksAPI.Init(Pointers.TEKKEN_STEAM_APP_ID))
            {
                FormToGui.T7Online_Logs("Error: failed to initialize steam api.");
                //Gui.PrintCannotContinueAndSleepForever();
                isSteamAPI = false;
                Thread.Sleep(globalDelay * 1000);
            }
            FormToGui.T7Online_Logs("SteamAPI is loaded.");
            isSteamAPI = true;
        }
        private void FillLobbyList()
        {
            FormToGui.T7Online_ClearAll();
            foreach (PlayerLobbyInfo enemy in ListOfPlayerLobbies)
            {
                FormToGui.T7Online_FillLobbyInfo(new Dictionary<string, string> { { "name", enemy.Name },{"rank" ,enemy.Rank},{"steamid" ,enemy.SteamId.ToString()},{ "character", enemy.Character } });
            }
        }
        private void StartLobbyInfoThread()
        {
            //Gui.InitOnlineModeComboBox();
            //Gui.PrintToGuiPlayerList("");
            FormToGui.T7Online_Logs("");
            Thread lobbyInfoThread = new Thread(() =>
            {
                while (!mainThreadStop)
                {
                    SteamworksAPI.SavePlayerLobbies(MainWindow.CallResultLobbyMatchList);

                    //int selectedIndex = FormToGui.T7Online_LobbySelected(); //MessageBox.Show(selectedIndex.ToString());
                    
                    FillLobbyList();
                    //if (-1 != selectedIndex) { FormToGui.T7Online_LobbyInfoSynchronizationSelection(selectedIndex); }
                    //TBT7Online_Write(ListOfPlayerLobbies);

                    //Gui.PrintPlayerLobbyInfoList(MainWindow.ListOfPlayerLobbies);
                    //Gui.RefreshPlayerLobbyInfoDropDownMenu(); // JoinLobby()
                    Thread.Sleep(3000);
                }
            }
            );
            lobbyInfoThread.IsBackground = true; // this makes sure the thread will be stopped after the gui is closed
            lobbyInfoThread.Start();
        }
        private void EditTargetProcessLoop()
        {
            bool areMessagesClean = true;
            int tapTitleSwitcher = 0;
            int delayWhileSearching = 1000 / 10; // in milliseconds, "10fps" (updates 10 times a sec)
            int delayWhileFighting = 2000; // 2 seconds, unused variable

            while (!mainThreadStop)
            {
                Thread.Sleep(delayWhileSearching);
                //Gui.UpdateGuiNextOpponent();
                if (Tekken.IsNewOpponentReceived())
                {
                    Tekken.CleanAllProcessMessages();
                    //Gui.CleanAllGuiMessages();
                    Tekken.HandleNewReceivedOpponent();
                    Tekken.DisplayOpponentInfoFromWeb(lastFoundSteamId);
                    areMessagesClean = false;
                }
                else if ((!areMessagesClean) && (!isSteamIdFound))
                {
                    Tekken.CleanAllProcessMessages();
                    //Gui.CleanAllGuiMessages();
                    areMessagesClean = true;
                }
                if (Tekken.IsNewFightAccepted())
                {
                    lastNameInPlayerlist = PlayerList.SaveNewOpponentInPlayerlist();
                }
                /*if (DidSomeoneCloseTekkenWindow())
                {
                    Gui.PrintLineToGuiConsole("Tekken window closed. (Can't find it anymore.)");
                    Gui.PrintCannotContinueAndCloseProgram();
                }*/
                if (tapTitleSwitcher == 10) { TabControlFontChange();tapTitleSwitcher = 0; } else { tapTitleSwitcher++; }
            }
            SteamworksAPI.Shutdown();
            mainThread.Abort();
        }

        /*private static BitmapImage InitImage(string filePath)
        {
            BitmapImage bitmapImage;
            using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
            {
                FileInfo fi = new FileInfo(filePath);
                byte[] bytes = reader.ReadBytes((int)fi.Length);
                reader.Close();

                bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(bytes);
                bitmapImage.EndInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                reader.Dispose();
            }
            return bitmapImage;
        }*/









        /*--------------------------------------------------------------------------------------------------------------------*/
        private void Form1_Load(object sender, EventArgs e)
        {

            ClientSize = new System.Drawing.Size(int.Parse(ConfigurationManager.AppSettings.Get("mainWindowWidth")), int.Parse(ConfigurationManager.AppSettings.Get("mainWindowHeight")));
            IniFiles INI = new IniFiles(ConfigurationManager.AppSettings.Get("iniPath"));
            if (!INI.KeyExists("path", "tekkenDefaultSavepath"))
            {
                INI.Write("path", "tekkenDefaultSavepath", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TekkenGame\\Saved\\SaveGames\\TEKKEN7\\"));
            }

            //Заполняем список персонажей на вкладке сохранений
            FillCharacters_Save();
            FillRanks_Save();

            //Заполняем предустановленные поля
            
            tsmiT7Autostart.Checked = T7Autostart;
            tsmiT7RestartOnload.Checked = T7RestartOnload;
            tsmiT7CloseOnExit.Checked = T7CloseOnExit;

            if (T7Autostart && (Process.GetProcessesByName(Pointers.TEKKEN_EXE_NAME).Count()>0)) { Process.Start(Pointers.STEAM_APP_START+ Pointers.TEKKEN_STEAM_APP_ID); }

        }

        private void bindLVtoCB(object e)
        {
            System.Windows.Forms.ListView sender = e as System.Windows.Forms.ListView;
            //int cb = 0;

            //bool fillRank = false;
            if (sender.SelectedItems.Count > 0)
            {
                if (sender.Name.Contains("haracter"))
                {
                    if (sender.Name.Contains("oad"))
                    {
                        FillRanks_Load();
                        FormToGui.SetCharactersFilterSelectIndex_Load(sender.SelectedItems[0].Index);
                    }
                }
                else if (sender.Name.Contains("ank"))
                {
                    if (sender.Name.Contains("oad"))
                    {
                        FormToGui.SetRanksFilterSelectIndex_Load(sender.SelectedItems[0].Index);
                    }
                    else if (sender.Name.Contains("ave"))
                    {
                        FormToGui.SetRanksFilterSelectIndex_Save(sender.SelectedItems[0].Index);
                    }
                }
            }
        }

        private void bindCBtoLV(object e)
        {
            System.Windows.Forms.ComboBox sender = e as System.Windows.Forms.ComboBox;
            int lv = 0;

            if (sender.SelectedItem != null)
            {
                if (sender.Name.Contains("haracter"))
                {
                    if (sender.Name.Contains("oad"))
                    {
                        lv = 1;
                        /*fillRank = true;*/
                    }
                }
                else if (sender.Name.Contains("ank"))
                {
                    if (sender.Name.Contains("oad"))
                    {
                        lv = 2;
                    }
                    else if (sender.Name.Contains("ave"))
                    {
                        lv = 3;
                    }
                }
            }

            switch (lv)
            {
                case 0: lvCharactersSave.Items[sender.SelectedIndex].Selected = true; break;
                case 1: lvCharactersLoad.Items[sender.SelectedIndex].Selected = true; break;
                case 2: lvRanksLoad.Items[sender.SelectedIndex].Selected = true; break;
                case 3: lvRanksSave.Items[sender.SelectedIndex].Selected = true; break;
            }

        }
        private void ToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void TabControl1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Control && ((e.KeyValue >= 49) && (e.KeyValue <= 59)))
            {
                // Выполнить нужное действие, например, открыть форму
                this.tabControl1.SelectedIndex = e.KeyValue - 49;
            }

        }

        private void ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            pathForm pathsForm = new pathForm();
            pathsForm.ShowDialog();
        }

        private void MenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string dest = Path.Combine(defaultSavesPath, T7PlayerID, lvCharactersSave.SelectedItems[0].ToolTipText, (lvRanksSave.SelectedItems[0].Index + 1).ToString());
            if (!Directory.Exists(dest))
            {
                try
                {
                    Directory.CreateDirectory(dest);
                }
                catch (IOException ee)
                {
                    MessageBox.Show("При попытке создать директорию возникла ошибка.\r\n" + ee.ToString());
                }
            }
            string sourcePath = Path.Combine(T7DefaultSavepath, T7PlayerID, ConfigurationManager.AppSettings.Get("tekkenMainSavefileName"));
            string destPath = Path.Combine(dest, ConfigurationManager.AppSettings.Get("tekkenMainSavefileName"));
            if (File.Exists(destPath))
            {
                DialogResult dialogResult = MessageBox.Show("Перезаписать существующее сохранение?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    File.Delete(destPath);
                    File.Copy(sourcePath, destPath);
                }
            }
            else { File.Copy(sourcePath, destPath); }

        }
        private void lvMouseUpHandler(object sender, MouseEventArgs e)
        {
            bindLVtoCB(sender);
        }
        private void lvKeyUpHandler(object sender, KeyEventArgs e)
        {

            if ((e.KeyValue == 40) || (e.KeyValue == 38))
            {
                bindLVtoCB(sender);
            }
        }

        private void cbMouseUpHandler(object sender)
        {
            bindCBtoLV(sender);
        }
        private void cbKeyUpHandler(object sender, KeyEventArgs e)
        {
            if ((e.KeyValue == 40) || (e.KeyValue == 38))
            {
                bindCBtoLV(sender);
            }
        }
        private void MainThreadStop()
        {
            mainThreadStop = true;
            if (tabPageTitle.Length > 0) { tpT7Online.Text = tabPageTitle; }
        }
        private void MainThreadInit()
        {
            mainThreadStop = false;
            tabPageTitle = tpT7Online.Text;
        }
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    MainThreadStop();
                    FillCharacterFilter_Save();
                    break;
                case 1:
                    MainThreadStop();
                    FillCharacters_Load();
                    FillRanks_Load();
                    break;
                case 3:
                    MainThreadInit();
                    StartMainThread();
                    break;
                case 4:
                    /*int index = 0;string str = "";
                    foreach(string rank in Pointers.ALL_PLAYABLE_RANKS)
                    {
                        str+="{ \""+ rank+"\", 0x" +Pointers.ALL_PLAYABLE_RANKS_MEMORY_IMPLIMENTATION[index++].ToString("X")+" },";
                    }
                    textBox1.Text = str;*/

                    GetPlayerInfo();
                    //ProcessMemory.Attach(Pointers.TEKKEN_EXE_NAME);
                    //tekkenModulePointer = ProcessMemory.GetModuleAddress(Pointers.TEKKEN_MODULE_NAME);
                    //tbT7StartMemoryPointer.Text = tekkenModulePointer.ToString();
                    //long x = BitConverter.ToInt64(new byte[] { 0x20, 0x30, 0x91, 0x98, 0xEA, 0x01, 0x00, 0x00 },0);
                    //byte[] x = BitConverter.GetBytes(0x1EA93E04964);
                    //tbT7StartMemoryPointer.Text= ProcessMemory.GetDynamicAddress(tekkenModulePointer + Pointers.PLAYER_NAME_STATIC_POINTER,Pointers.PLAYER_NAME_POINTER_OFFSETS).ToString();
                    break;
            }
        }
        private string GetPlayerSteamName()
        {
            long steamModulePointer = 0;
            steamModulePointer= ProcessMemory.GetModuleAddress(Pointers.STEAM_API_MODULE_EDITED_NAME);
            if (steamModulePointer==0) steamModulePointer= ProcessMemory.GetModuleAddress(Pointers.STEAM_API_MODULE_NAME);
            if (steamModulePointer == 0) return null;

            long userSteamIdAddress = ProcessMemory.GetDynamicAddress(steamModulePointer + Pointers.STEAM_ID_USER_STATIC_POINTER, Pointers.STEAM_ID_USER_POINTER_OFFSETS);
            long userSteamId = ProcessMemory.ReadMemory<long>(userSteamIdAddress);

            return null;
        }
        private void PrintPlayerSteamName(string steamname)
        {
            //FormToGui.ManualMemoryPlayerSteamNameShow(steamname);
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            string destPath = Path.Combine(T7DefaultSavepath,T7PlayerID, ConfigurationManager.AppSettings.Get("tekkenMainSavefileName"));
            string sourcePath = Path.Combine(Path.Combine(defaultSavesPath, T7PlayerID, lvCharactersLoad.SelectedItems[0].ToolTipText.ToString(), lvRanksLoad.SelectedItems[0].ToolTipText.ToString()), ConfigurationManager.AppSettings.Get("tekkenMainSavefileName"));
            try{File.Delete(destPath);}
            catch { MessageBox.Show("При удалении старой записи произошла ошибка"); }
            try { File.Copy(sourcePath, destPath); }
            catch { MessageBox.Show("При копировании записи произошла ошибка"); }

            if (tsmiT7RestartOnload.Checked)
            {
                foreach (var process in Process.GetProcessesByName(Pointers.TEKKEN_EXE_NAME))
                {
                    if (!process.CloseMainWindow()) MessageBox.Show("При попытке завершить процесс " + process.ProcessName + " возникли ошибки");
                }

                Process.Start(Pointers.STEAM_APP_START + Pointers.TEKKEN_STEAM_APP_ID);
            }
        }

        private void lvCharactersSave_MouseUp(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.ListView listView = sender as System.Windows.Forms.ListView;
            if (listView.SelectedItems.Count > 0)
            {
                lvMouseUpHandler(sender, e);
                cbRankFilterSave.SelectedIndex = 0;
                lvRanksSave.Items[0].Selected = true;
                lvRanksSave.EnsureVisible(0);
            }
        }

        private void lvCharactersSave_KeyUp(object sender, KeyEventArgs e)
        {
            System.Windows.Forms.ListView listView = sender as System.Windows.Forms.ListView;
            if (listView.SelectedItems.Count > 0)
            {
                lvKeyUpHandler(sender, e);
                cbRankFilterSave.SelectedIndex = 0;
                lvRanksSave.Items[0].Selected = true;
                lvRanksSave.EnsureVisible(0);
            }
        }

        private void lvCharactersLoad_MouseUp(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.ListView listView = sender as System.Windows.Forms.ListView;
            if (listView.SelectedItems.Count > 0)
            {
                lvMouseUpHandler(sender, e);
                cbRankFilterLoad.SelectedIndex = 0;
                lvRanksLoad.Items[0].Selected = true;
                lvRanksLoad.EnsureVisible(0);
            }
        }

        private void lvCharactersLoad_KeyUp(object sender, KeyEventArgs e)
        {
            System.Windows.Forms.ListView listView = sender as System.Windows.Forms.ListView;
            if (listView.SelectedItems.Count > 0)
            {
                lvKeyUpHandler(sender, e);
                cbRankFilterLoad.SelectedIndex = 0;
                lvRanksLoad.Items[0].Selected = true;
                lvRanksLoad.EnsureVisible(0);
            }

        }

        private void cbCharacterFilterSave_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cbMouseUpHandler(sender);

            lvCharactersSave.Items[cbCharacterFilterSave.SelectedIndex].Selected = true;
            lvCharactersSave.EnsureVisible(lvCharactersSave.SelectedItems[0].Index);

            cbRankFilterSave.SelectedIndex = 0;
            lvRanksSave.Items[cbRankFilterSave.SelectedIndex].Selected = true;
            lvRanksSave.EnsureVisible(cbRankFilterSave.SelectedIndex);
        }

        private void cbRankFilterSave_SelectionChangeCommitted(object sender, EventArgs e)
        {
            lvRanksSave.Items[cbRankFilterSave.SelectedIndex].Selected = true;
            lvRanksSave.EnsureVisible(cbRankFilterSave.SelectedIndex);
        }

        private void lvRanksLoad_MouseUp(object sender, MouseEventArgs e)
        {
            System.Windows.Forms.ListView listView = sender as System.Windows.Forms.ListView;
            if (listView.SelectedItems.Count > 0)
            {
                lvMouseUpHandler(sender, e);
            }
        }

        private void cbCharacterFilterLoad_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bindCBtoLV(sender);

            lvCharactersLoad.Items[cbCharacterFilterLoad.SelectedIndex].Selected = true;
            lvCharactersLoad.EnsureVisible(lvCharactersLoad.SelectedItems[0].Index);

            FillRanks_Load();

            cbRankFilterLoad.SelectedIndex = 0;
            lvRanksLoad.Items[cbRankFilterLoad.SelectedIndex].Selected = true;
            lvRanksLoad.EnsureVisible(cbRankFilterLoad.SelectedIndex);
        }

        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbRankFilterLoad_SelectionChangeCommitted(object sender, EventArgs e)
        {
            bindCBtoLV(sender);

            lvRanksLoad.Items[cbRankFilterLoad.SelectedIndex].Selected = true;
            lvRanksLoad.EnsureVisible(lvRanksLoad.SelectedItems[0].Index);
        }

        private void btnTextChange(object sender)
        {
            
        }

        private void lvCharactersSave_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnTextChange(sender);
        }

        private void lvRanksSave_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            btnTextChange(sender);
        }

        private void lvCharactersLoad_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            btnTextChange(sender);
        }

        private void lvRanksLoad_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnTextChange(sender);
        }

        private void tsmiT7Autostart_CheckedChanged(object sender, EventArgs e)
        {
            IniFiles INI = new IniFiles(ConfigurationManager.AppSettings.Get("iniPath"));

            INI.Write("tekken7","autostart", tsmiT7Autostart.Checked.ToString());
            T7Autostart = tsmiT7Autostart.Checked;
        }

        private void tsmiT7RestartOnload_CheckedChanged(object sender, EventArgs e)
        {
            IniFiles INI = new IniFiles(ConfigurationManager.AppSettings.Get("iniPath"));

            INI.Write("tekken7", "restartonload", tsmiT7RestartOnload.Checked.ToString());
            T7RestartOnload = tsmiT7RestartOnload.Checked;
        }

        private void tsmiT7CloseOnExit_CheckedChanged(object sender, EventArgs e)
        {
            IniFiles INI = new IniFiles(ConfigurationManager.AppSettings.Get("iniPath"));

            INI.Write("tekken7", "closeonexit", tsmiT7CloseOnExit.Checked.ToString());
            T7CloseOnExit = tsmiT7CloseOnExit.Checked;
        }

        private void mainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (T7CloseOnExit)
            {
                foreach (var process in Process.GetProcessesByName(Pointers.TEKKEN_EXE_NAME))
                {
                    if (!process.CloseMainWindow()) MessageBox.Show("При попытке завершить процесс " + process.ProcessName + " возникли ошибки");
                }
            }
        }

        private void btnT7Start_Click(object sender, EventArgs e)
        {
            Process.Start(Pointers.STEAM_APP_START+Pointers.TEKKEN_STEAM_APP_ID);
        }

        private void tsmiTargetListAdd_Click(object sender, EventArgs e)
        {
            AddTargetsListForm addTargetForm = new AddTargetsListForm();
            addTargetForm.ShowDialog();
        }

        private void tsmiShowTargetsList_Click(object sender, EventArgs e)
        {
            ShowTargetsList showTargetsList = new ShowTargetsList();
            showTargetsList.ShowDialog();
        }

        private void bT7MemoryGet_Click(object sender, EventArgs e)
        {
            ManualInfoEdit.ApplyPreset();
        }
       
        
        private void lvCharactersList_MouseUp(object sender, MouseEventArgs e)
        {
            if (FormToGui.ManualMemoryIsSelectedCharacter())
            {
                ManualInfoEdit.FillCurrentCharacterInfo();
            }
            else
            {
                ManualInfoEdit.CharacterInfoControls_Hide();
            }
        }
        
        private void GetPlayerInfo()
        {
            /*
            string[] strs = new string[] { "BD0FA4CC","7C1F48F1","FF3E908B","B7EB3EB8","D1AEFB5E","F276DD83","CDEDBA40","2B73BB39","DF3E90AB","25C2EDD0","56D77D26","F1AEFB7E","D276DDA3","ACDB75D8","C733B799","0FB7EB89","38F78194","B4751F04","54751FE4","98F78134","2499DBBC","79F24011","0B63BB09","A5D7ED45","6FABEBF5","53F5EE92","6748B742","587F817C","90D5F768","537BEE1C","FFE39056","535FEE38","7CF1481F","42FBE784","53F5EE92","AC957596","2E08DF23" };
            string[] newstrs = new string[strs.Length];
            int index = 0;

            foreach(string str in strs)
            {
                newstrs[index++] = str[6].ToString()+ str[7]+ str[4] + str[5] + str[2] + str[3] + str[0] + str[1];
            }
            foreach(string str in newstrs)
            {
                textBox1.Text += "0x"+str+',';
            }*/
            if (!ProcessMemory.Attach(Pointers.TEKKEN_EXE_NAME)) return;

            /*if(tekkenModulePointer==0)*/ tekkenModulePointer = ProcessMemory.GetModuleAddress(Pointers.TEKKEN_MODULE_NAME);
            /*if(tekkenPlayerNameMemoryPointer==0)*/ tekkenPlayerNameMemoryPointer = ProcessMemory.GetDynamicAddress(tekkenModulePointer + Pointers.PLAYER_NAME_STATIC_POINTER, Pointers.PLAYER_NAME_POINTER_OFFSETS);

            ManualInfoEdit.PrintPlayerName();
            ManualInfoEdit.PrintPlayerSteamID();

            ManualInfoEdit.FillCharactersList();
            
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            ManualInfoEdit.ApplyChanges();
        }

        private void bUpdatePlayerInfo_Click(object sender, EventArgs e)
        {
            GetPlayerInfo();
            Gui.Clear(tbTest);
        }

        private void rbLastGames_CheckedChanged(object sender, EventArgs e)
        {
            FormToGui.ManualMemoryMatchHistory_ChangeHistory(sender as RadioButton);
        }

        private void rbLastGames_MouseUp(object sender, MouseEventArgs e)
        {
            (sender as RadioButton).Checked = !(sender as RadioButton).Checked;
        }

        private void chbLastGames_CheckedChanged(object sender, EventArgs e)
        {
            FormToGui.ManualMemoryMatchHistory_ChangeHistory((sender as CheckBox));
        }

        private void chbCharacterSubstitution_CheckedChanged(object sender, EventArgs e)
        {
            FormToGui.ManualMemorySubstitution_ClearCharactersList();
            FormToGui.ManualMemorySubstitution_FillCharactersList(PrepareCharacterTextList_Save());
            FormToGui.ManualMemorySubstitution_ListShow();
        }

        private void bCharacterSubstitution_Click(object sender, EventArgs e)
        {
            ManualInfoEdit.CharacterSubstitution();
        }

        private void lbEnemyList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //(sender as ListBox).Items[(sender as ListBox).SelectedIndex].EnsureVisible();
        }

        private void lbEnemyList_MouseUp(object sender, MouseEventArgs e)
        {
            if((sender as ListBox).SelectedIndex>=0) FormToGui.T7Online_LobbyInfoSynchronizationSelection((sender as ListBox).SelectedIndex);
        }
    }
}
