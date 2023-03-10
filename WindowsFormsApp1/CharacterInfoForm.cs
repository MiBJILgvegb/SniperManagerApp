using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SniperManagerApp
{
    public partial class CharacterInfoForm : Form
    {
        internal static CharacterInfoForm characterInfoForm;
        internal byte[] characterData;
        internal long characterInfoOffset;
        public CharacterInfoForm()
        {
            InitializeComponent();
            characterInfoForm = this;
            
        }
        private void SetFormTitle(string title)
        {
            this.Text=title;
        }
        private void FillCharacterInfo(byte[] characterInfo)
        {
            FillRankList(characterInfo);
            byte[] wins = GetCurrentWinsCounter(characterInfo);
        }
        private void CharacterInfoForm_Load(object sender, EventArgs e)
        {
            SetFormTitle(FormToGui.ManualMemoryGetSelectedCharacter());
            characterData = GetCurrentCharacterInfo(this.Text);
            FillCharacterInfo(characterData);
        }
        private long CalcCharacterRankMemoryOffset(string character)
        {
            return Pointers.CHARACTERINFO_MEMORY_BLOCK_OFFSET+Array.IndexOf(Pointers.ALL_CHARACTERS, character)*Pointers.CHARACTERINFO_MEMORY_BLOCK_SIZE+ Pointers.CHARACTERINFO_MEMORY_OFFSETS["RANK"];
        }
        private long CalcCharacterInfoMemoryOffset(string character)
        {
            return MainWindow.tekkenMemoryPointer + Pointers.CHARACTERINFO_MEMORY_BLOCK_OFFSET + Array.IndexOf(Pointers.ALL_CHARACTERS, character) * Pointers.CHARACTERINFO_MEMORY_BLOCK_SIZE;
        }
        private byte[] GetCurrentCharacterInfo(string character)
        {
            characterInfoOffset = CalcCharacterInfoMemoryOffset(character);
            byte[] bytes = ProcessMemory.GetBytes(characterInfoOffset, Pointers.CHARACTERINFO_MEMORY_BLOCK_SIZE);
            //Array.Reverse(bytes);
            return bytes;
        }
        private int GetCurrentCharacterRank(byte[] characterInfo)
        {
            byte[] rank=new byte[4];
            Array.Copy(characterInfo,Pointers.CHARACTERINFO_MEMORY_OFFSETS["RANK"], rank,0,4);
            Array.Reverse(rank);
            //return Array.IndexOf(Pointers.ALL_PLAYABLE_RANKS_MEMORY_IMPLIMENTATION,BitConverter.ToUInt32(rank, 0));
            return 0;
        }
        private byte[] GetCurrentWinsCounter(byte[] characterInfo)
        {
            byte[] wins=new byte[4];
            Array.Copy(characterInfo, Pointers.CHARACTERINFO_MEMORY_OFFSETS["RANKEDWINS"], wins, 0, 4);
            //Array.Reverse(wins);
            return wins;
            //return Array.IndexOf(Pointers.ALL_PLAYABLE_RANKS_MEMORY_IMPLIMENTATION, BitConverter.ToUInt32(rank, 0));
        }
        private void FillRankList(byte[] characterInfo)
        {
            FormToGui.ClearRanksList_CharacterInfo();
            FormToGui.FillRanksList_ManualInfoEdit(Pointers.ALL_PLAYABLE_RANKS);
            FormToGui.SetRanksListSelectIndex_ManualInfoEdit(GetCurrentCharacterRank(characterInfo));
            //int currentRank = GetCurrentCharacterRank(this.Text);
        }
        private void SaveInfo()
        {

        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            this.SaveInfo();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.SaveInfo();
            this.Close();
        }
        private void SetRank(long address,byte[] rank)
        {
            foreach(byte b in rank) 
            {
                ProcessMemory.WriteMemory<byte>(address,b);
                address++;
            }
        }
        private void ApplyPreset()
        {
            byte[] rank = new byte[4];
            Array.Copy(Pointers.CHARACTERS_PRESETS_JOSIE, Pointers.CHARACTERINFO_MEMORY_OFFSETS["RANK"], rank, 0, 4);
            //Array.Reverse(rank);
            SetRank(characterInfoOffset+ Pointers.CHARACTERINFO_MEMORY_OFFSETS["RANK"], rank);
        }
        private void bApplyPreset_Click(object sender, EventArgs e)
        {
            ApplyPreset();
        }
    }
}
