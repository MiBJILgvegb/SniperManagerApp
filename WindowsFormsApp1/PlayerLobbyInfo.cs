using Steamworks;
using System;
using System.Collections.Generic;

namespace SniperManagerApp
{
    public class PlayerLobbyInfo
    {
        public CSteamID LobbyId;
        public String Name;
        public long SteamId;
        public String Character;
        public String Rank;

        public PlayerLobbyInfo(CSteamID lobbyId, String name, long steamId, String character, String rank)
        {
            LobbyId = lobbyId;
            Name = name;
            SteamId = steamId;
            Character = character;
            Rank = rank;
        }

        static public void AddToList(List<PlayerLobbyInfo> theList, PlayerLobbyInfo newPlayer)
        {
            if (newPlayer.SteamId == 0) return;
            foreach (PlayerLobbyInfo player in theList)
            {
                if (player.SteamId == newPlayer.SteamId)
                {
                    player.Name = newPlayer.Name;
                    player.Character = newPlayer.Character;
                    player.Rank = newPlayer.Rank;
                    player.LobbyId = newPlayer.LobbyId;
                    return;
                }
            }
            theList.Add(newPlayer);
        }

        static public bool DoesListContainPlayer(List<PlayerLobbyInfo> theList, PlayerLobbyInfo newPlayer)
        {
            foreach (PlayerLobbyInfo player in theList)
            {
                if (player.SteamId == newPlayer.SteamId) return true;
            }
            return false;
        }

        static public String GetPlayerRank(List<PlayerLobbyInfo> theList, long steamId)
        {
            foreach (PlayerLobbyInfo player in theList)
            {
                if (player.SteamId == steamId) return player.Rank;
            }
            return "";
        }

        static public String GetPlayerCharacter(List<PlayerLobbyInfo> theList, long steamId)
        {
            foreach (PlayerLobbyInfo player in theList)
            {
                if (player.SteamId == steamId) return player.Character;
            }
            return "";
        }
    }
}
