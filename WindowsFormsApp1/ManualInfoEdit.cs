using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SniperManagerApp
{
    internal class ManualInfoEdit
    {
        internal static byte[] characterInfo;
        internal static long playerSegmentAddress;
        internal static long characterInfoOffset;
        internal static Dictionary<string, int> characterData;
        private static long DictionaryGetIndex(Dictionary<long,long> dictionary, long value)
        {
            foreach(var kvp in dictionary) 
            {
                if(value==kvp.Value) return kvp.Key;
            }

            return -1;
        }
        private static long DictionaryGetIndexAsArray(Dictionary<uint, uint> dictionary, long value)
        {
            long index = 0;
            foreach(var kvp in dictionary) 
            {
                if (value == kvp.Value) return index;
                index++;
            }
            return -1;
        }
        private static long DictionaryGetValueAsArray(Dictionary<uint, uint> dictionary, uint index)
        {
            uint ind = 0;
            foreach(var kvp in dictionary)
            {
                if (index == ind) { return (long)kvp.Value; }
                ind++;
            }
            return -1;
        }
        internal static void WriteMemoryArray(long address, byte[] array)
        {
            int offset = 0;
            //for (int i = array.Length - 1; i >= 0; i--)
            foreach (int i in array)
            {
                ProcessMemory.WriteMemory<byte>(address + offset, i);
                offset++;
            }
        }
        //-----------------------------------------------------------------------------------------------
        private static long CalcCharacterInfoMemoryOffset(string character,long memoryPointer)
        {
            return memoryPointer + Pointers.CHARACTERINFO_MEMORY_BLOCK_OFFSET + Array.IndexOf(Pointers.ALL_CHARACTERS, character) * Pointers.CHARACTERINFO_MEMORY_BLOCK_SIZE;
        }
        private static byte[] GetCurrentCharacterInfo(string character)
        {
            characterInfoOffset = CalcCharacterInfoMemoryOffset(character,MainWindow.tekkenPlayerNameMemoryPointer);
            byte[] bytes = ProcessMemory.GetBytes(characterInfoOffset, Pointers.CHARACTERINFO_MEMORY_BLOCK_SIZE);
            //Array.Reverse(bytes);
            return bytes;
        }
        private static void PrepareCharacterData(byte[] characterInfo)
        {
            characterData = new Dictionary<string, int> { {"rank", GetCurrentCharacterRank(characterInfo) },{ "wins", GetCurrentCharacterWins(characterInfo) },{ "winstreak",GetCurrentCharacterWinstreak(characterInfo) },{ "elo", GetCurrentCharacterELO(characterInfo) } };
        }
        //-----------------------------------------------------------------------------------------------
        internal static void FillCharactersList()
        {
            FormToGui.ClearCharactersList_Manual();
            FormToGui.FillCharactersList_Manual(MainWindow.mainWindow.PrepareCharactersList(Pointers.ALL_PLAYABLE_CHARACTERS), MainWindow.mainWindow.PrepareCharactersImagesList(Pointers.ALL_PLAYABLE_CHARACTERS), MainWindow.mainWindow.lvilType);
        }
        internal static void PrintPlayerName()
        {
            FormToGui.ManualMemoryPlayerName_Print(ProcessMemory.ReadString(MainWindow.tekkenPlayerNameMemoryPointer));
        }
        internal static void CharacterInfoControls_Show()
        {
            FormToGui.ManualMemoryCharacterInfoControls_Show();
        }
        internal static void CharacterInfoControls_Hide()
        {
            FormToGui.ManualMemoryCharacterInfoControls_Hide();
        }
        internal static void ManualMemoryCharacterSubstitution_ControlsShow()
        {
            FormToGui.ManualMemorySubstitution_ListShow();
        }
        internal static void PrintPlayerSteamID() 
        {
            long steamModulePointer = ProcessMemory.GetModuleAddress(Pointers.STEAM_API_MODULE_EDITED_NAME);
            long userSteamIdAddress = ProcessMemory.GetDynamicAddress(steamModulePointer + Pointers.STEAM_ID_USER_STATIC_POINTER, Pointers.STEAM_ID_USER_POINTER_OFFSETS);
            FormToGui.ManualMemoryPlayerSteamID_Print(ProcessMemory.ReadMemory<long>(userSteamIdAddress));
        }
        private static int GetCurrentCharacterRank(byte[] characterInfo)
        {
            byte[] rank = new byte[4];
            Array.Copy(characterInfo, Pointers.CHARACTERINFO_MEMORY_OFFSETS["RANK"], rank, 0, 4);
            
            //return Array.IndexOf(Pointers.ALL_PLAYABLE_RANKS_MEMORY_IMPLIMENTATION, BitConverter.ToUInt32(rank, 0));
            return HaradaConverter.T7ToNormal(BitConverter.ToInt32(rank, 0));
        }
        private static void FillRankList(int rank)
        {
            FormToGui.ClearRanksList_CharacterInfo();
            FormToGui.FillRanksList_ManualInfoEdit(Pointers.ALL_PLAYABLE_RANKS);
            FormToGui.SetRanksListSelectIndex_ManualInfoEdit(rank-10);
            //int currentRank = GetCurrentCharacterRank(this.Text);
        }
        private static byte[] GetMatchHistory()
        {
            playerSegmentAddress = ProcessMemory.GetDynamicAddress(MainWindow.tekkenModulePointer+Pointers.PLAYER_NAME_STATIC_POINTER,Pointers.PLAYER_SEGMENT_POINTER_OFFSETS);
            long matchHistorySegmentStart = playerSegmentAddress + Pointers.PLAYERINFO_MEMORY_MATCHHISTORY_OFFSET;

            byte[] matchHistory=new byte[10];
            for(int i = 0; i< 10; i++)
            {
                matchHistory[i] = ProcessMemory.ReadMemory<byte>(matchHistorySegmentStart+4*i);
            }

            return matchHistory;
        }
        
        private static void FillWinstreakList()
        {
            //FormToGui.ManualMemoryCharacterWinstreak_Clear();
            //FormToGui.ManualMemoryCharacterWinstreak_Fill(PrepareDecimalsList());
        }
        private static int GetCurrentCharacterWinstreak(byte[] characterInfo)
        {
            byte[] winstreak = new byte[4];
            Array.Copy(characterInfo, Pointers.CHARACTERINFO_MEMORY_OFFSETS["WINSTREAK"], winstreak, 0, 4);

            return HaradaConverter.T7ToNormal(BitConverter.ToInt32(winstreak, 0));
        }
        private static void FillWinstreak(int winstreak)
        {
            Gui.Text(MainWindow.mainWindow.tbManualMemoryWinstreak,winstreak.ToString());
        }
        private static void FillMatchHistory() 
        {
            byte[] matchHistory= GetMatchHistory();
            FormToGui.ManualMemoryMatchHistory_SetHistory(matchHistory);
        }
        private static void FillELOLine(int currentElo)
        {
            Gui.Text(MainWindow.mainWindow.cbManualMemoryELOLine,currentElo.ToString());
        }
        private static int GetCurrentCharacterELO(byte[] characterInfo)
        {
            byte[] elo = new byte[4];
            Array.Copy(characterInfo, Pointers.CHARACTERINFO_MEMORY_OFFSETS["ELO"], elo, 0, 4);

            return HaradaConverter.T7ToNormal(BitConverter.ToInt32(elo, 0));
        }
        internal static void FillCurrentCharacterInfo()
        {
            string character = FormToGui.ManualMemoryGetSelectedCharacter();
            byte [] characterInfo = GetCurrentCharacterInfo(character);

            PrepareCharacterData(characterInfo);
            //FormToGui.ManualMemorySetGBTitle(man);

            FillRankList(characterData["rank"]);
            FillWinsCount(characterData["wins"]);
            FillWinstreak(characterData["winstreak"]);
            FillELOLine(characterData["elo"]);
            FillMatchHistory();

            FormToGui.ManualMemoryCharacterInfoControls_Show();
        }
        private static int GetCurrentCharacterWins(byte[] characterInfo)
        {
            byte[] wins = new byte[4];
            Array.Copy(characterInfo, Pointers.CHARACTERINFO_MEMORY_OFFSETS["RANKEDWINS"], wins, 0, 4);
            return HaradaConverter.T7ToNormal(BitConverter.ToInt32(wins, 0));
        }
      
        private static void FillWinsList()
        {
            //FormToGui.ManualMemoryCharacterWins_Clear();
            //FormToGui.ManualMemoryCharacterWins_Fill(PrepareDecimalsList());
        }
        private static void FillWinsCount(int wins)
        {
            Gui.Text(MainWindow.mainWindow.tbManualMemoryCurrentWins,wins.ToString());
        }
        private static uint GetNewRank() { return Pointers.ALL_PLAYABLE_RANKS_MEMORY_IMPLIMENTATION[FormToGui.ManualMemoryGetSelectedRank()]; }
        private static long GetNewWinsCount()
        {
            return HaradaConverter.T7ToHarada(Convert.ToUInt32(Gui.Text(MainWindow.mainWindow.tbManualMemoryCurrentWins)));
            
            /*uint value= (uint)DictionaryGetValueAsArray(Pointers.CHARACTERINFO_MEMORY_KNOWEDDECUMALS, (uint)FormToGui.ManualMemoryGetSelectedWinsCount());

            if(value>=0)
            {
                //return (long)value;
                return HaradaConverter.T7ToHarada(Gui.GetSelectedIndex(MainWindow.mainWindow.cbManualMemoryCurrentWins));
            }
            else { return -1; }*/
        }
        private static byte[] GetNewMatchHistory()
        {
            return FormToGui.ManualMemoryMatchHistory_GetNewHistory();
        }
        private static long GetNewWinstreak() 
        {
            return HaradaConverter.T7ToHarada(Convert.ToUInt32(Gui.Text(MainWindow.mainWindow.tbManualMemoryWinstreak)));

            /*uint value = (uint)DictionaryGetValueAsArray(Pointers.CHARACTERINFO_MEMORY_KNOWEDDECUMALS, (uint)FormToGui.ManualMemoryGetSelectedWinstreakCount());

            if (value >= 0)
            {
                //return (long)value;
                return Gui.GetSelectedIndex(MainWindow.mainWindow.cbManualMemoryWinstreak);
            }
            else { return -1; }*/
        }
        internal static void ApplyChanges()
        {
            uint newRank = GetNewRank();
            //uint newWinsCount = (uint)GetNewWinsCount();
            int newWinsCount = (int)GetNewWinsCount();

            int newWinstreak = (int)GetNewWinstreak();
            byte[] matchHistory = GetNewMatchHistory();
            long player_segment_start = ProcessMemory.GetDynamicAddress(MainWindow.tekkenModulePointer+ Pointers.PLAYER_NAME_STATIC_POINTER, Pointers.PLAYER_SEGMENT_POINTER_OFFSETS);

            Dictionary<int,uint> newInfo= new Dictionary<int,uint>();
            newInfo.Add(Pointers.CHARACTERINFO_MEMORY_OFFSETS["RANK"], newRank);
            newInfo.Add(Pointers.CHARACTERINFO_MEMORY_OFFSETS["RANKEDWINS"], (uint)newWinsCount);
            newInfo.Add(Pointers.CHARACTERINFO_MEMORY_OFFSETS["WINSTREAK"], (uint)newWinstreak);

            WriteNewCharacterInfo(characterInfoOffset,newInfo);
            WriteNewMatchHistory(player_segment_start, matchHistory);
            /*int offset = 0;
            foreach (byte match in matchHistory)
            {
                ProcessMemory.WriteMemory<byte>(player_segment_start+Pointers.PLAYERINFO_MEMORY_MATCHHISTORY_OFFSET + 4*offset,match);
                offset++;
            }*/
        }
        private static void WriteNewCharacterInfo(long address,Dictionary<int,uint> newInfo)
        {
            foreach(var kvp in newInfo)
            {
                ProcessMemory.WriteMemory<uint>(address + kvp.Key, kvp.Value);
            }
        }
        private static void WriteNewMatchHistory(long address, byte[] matchHistory)
        {
            int offset = 0;
            foreach (byte match in matchHistory)
            {
                ProcessMemory.WriteMemory<byte>(address + Pointers.PLAYERINFO_MEMORY_MATCHHISTORY_OFFSET + 4 * offset, match);
                offset++;
            }
        }
        internal static int GetSubstitutionCharacter()
        {
            return Array.IndexOf(Pointers.ALL_CHARACTERS,Pointers.ALL_PLAYABLE_CHARACTERS[FormToGui.ManualMemoryGetSelectedSubstitutionCharacter()]);

        }
        internal static void CharacterSubstitution()
        {
            int substitutionCharacterID=GetSubstitutionCharacter();
            long substitutionCharacterAddresses =ProcessMemory.GetDynamicAddress(MainWindow.tekkenModulePointer + Pointers.PLAYER_SUBSTITUTION_CHARACTER_POINTER, new int[] { Pointers.PLAYER_SUBSTITUTION_CHARACTER_OFFSET[0] });

            ProcessMemory.WriteMemory<int>(substitutionCharacterAddresses, substitutionCharacterID);
        }
    }
}
