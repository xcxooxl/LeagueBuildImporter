namespace BuildImporter
{
    public class Session
    {
        public Action[][] actions { get; set; }
        public bool allowBattleBoost { get; set; }
        public bool allowDuplicatePicks { get; set; }
        public bool allowRerolling { get; set; }
        public bool allowSkinSelection { get; set; }
        public Bans bans { get; set; }
        public object[] benchChampionIds { get; set; }
        public bool benchEnabled { get; set; }
        public int boostableSkinCount { get; set; }
        public Chatdetails chatDetails { get; set; }
        public int counter { get; set; }
        public bool isSpectating { get; set; }
        public int localPlayerCellId { get; set; }
        public Myteam[] myTeam { get; set; }
        public int rerollsRemaining { get; set; }
        public Theirteam[] theirTeam { get; set; }
        public Timer timer { get; set; }
        public object[] trades { get; set; }
    }

    public class Bans
    {
        public object[] myTeamBans { get; set; }
        public int numBans { get; set; }
        public object[] theirTeamBans { get; set; }
    }

    public class Chatdetails
    {
        public string chatRoomName { get; set; }
        public object chatRoomPassword { get; set; }
    }

    public class Timer
    {
        public int adjustedTimeLeftInPhase { get; set; }
        public int adjustedTimeLeftInPhaseInSec { get; set; }
        public long internalNowInEpochMs { get; set; }
        public bool isInfinite { get; set; }
        public string phase { get; set; }
        public int timeLeftInPhase { get; set; }
        public int timeLeftInPhaseInSec { get; set; }
        public int totalTimeInPhase { get; set; }
    }

    public class Action
    {
        public int actorCellId { get; set; }
        public int championId { get; set; }
        public bool completed { get; set; }
        public int id { get; set; }
        public string type { get; set; }
    }

    public class Myteam
    {
        public string assignedPosition { get; set; }
        public int cellId { get; set; }
        public int championId { get; set; }
        public int championPickIntent { get; set; }
        public string playerType { get; set; }
        public int selectedSkinId { get; set; }
        public int spell1Id { get; set; }
        public int spell2Id { get; set; }
        public int summonerId { get; set; }
        public int team { get; set; }
        public int wardSkinId { get; set; }
    }

    public class Theirteam
    {
        public string assignedPosition { get; set; }
        public int cellId { get; set; }
        public int championId { get; set; }
        public int championPickIntent { get; set; }
        public string playerType { get; set; }
        public int selectedSkinId { get; set; }
        public int spell1Id { get; set; }
        public int spell2Id { get; set; }
        public int summonerId { get; set; }
        public int team { get; set; }
        public int wardSkinId { get; set; }
    }
}