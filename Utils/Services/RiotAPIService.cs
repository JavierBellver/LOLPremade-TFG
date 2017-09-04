using lolpremade.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace lolpremade.Utils.Services
{
    public class SummonerDTO
    {
        public string profileIconId { get; set; }
        public string name { get; set; }
        public string summonerLevel { get; set; }
        public string accountId { get; set; }
        public string id { get; set; }
        public long revisionDate { get; set; }
    }

    public class MiniSeriesDTO
    {
        public int wins { get; set; }
        public int loses { get; set; }
        public int target { get; set; }
        public string progress { get; set; }
    }

    public class LeagueListItemDTO
    {
        public string rank { get; set; }
        public bool hotStreak { get; set; }
        public MiniSeriesDTO miniSeries { get; set; }
        public int wins { get; set; }
        public bool veteran { get; set; }
        public int loses { get; set; }
        public string playerOrTeamId { get; set; }
        public string playerOrTeamName { get; set; }
        public bool inactive { get; set; }
        public bool freshBlood { get; set; }
        public int leaguePoints { get; set; }
    }
    

    public class LeagueListDTO
    {
        public string tier { get; set; }
        public string queue { get; set; }
        public string name { get; set; }
        public List<LeagueListItemDTO> entries { get; set; }
    }

    public class MasteryPageDTO
    {
        public bool current { get; set; }
        public string name { get; set; }
        public long id { get; set; }
    }
    public class MasteryPagesDTO
    {
        public string summonerId { get; set; }
        public List<MasteryPageDTO> pages { get; set; }
    }

    public interface IRiotAPIService
    {
        LOLUserInfo GetCompleteUserBySummonerName(string summonerName, string region);
        bool AuthorizeLOLAccount(string summonerName, string region, string masteryPage);
    }

    public class RiotAPIService : IRiotAPIService
    {
        private string RiotAPIKey;
        private Dictionary<string, string> regionToCode;

        public RiotAPIService()
        {
            // Insert here your own Riot API Key
	    // RiotAPIKey = "";
            regionToCode = new Dictionary<string, string>();
            regionToCode.Add("Brazil", "br1");
            regionToCode.Add("Europe Nordic & East", "eun1");
            regionToCode.Add("Europe West", "euw1");
            regionToCode.Add("Latin America North", "la1");
            regionToCode.Add("Latin America South", "la2");
            regionToCode.Add("Oceania", "oc1");
            regionToCode.Add("Russia", "ru");
            regionToCode.Add("Turkey", "tr1");
            regionToCode.Add("Japan", "jp1");
            regionToCode.Add("South East Asia", "pbe1");
            regionToCode.Add("Republic of Korea", "kr");
        }

        private string GetTierFromLeagueList(List<LeagueListDTO> list)
        {
            string tier = "";
            if(list.Where(l => l.queue== "RANKED_SOLO_5x5").Any())
            {
                tier = list.Where(l => l.queue == "RANKED_SOLO_5x5").First().tier;
            }
            return tier;
        }

        private string GetRankFromLeagueList(List<LeagueListDTO> list,string summonerId)
        {
            string rank = "";
            if(list.Where(l => l.queue == "RANKED_SOLO_5x5").Any())
            {
                LeagueListDTO league = list.Where(l => l.queue == "RANKED_SOLO_5x5").First();
                if(league.entries.Where(u => u.playerOrTeamId == summonerId).Any())
                {
                    rank = league.entries.Where(u => u.playerOrTeamId == summonerId).First().rank;
                }
            }
            return rank;
        }

        public string[] GetTierAndRank(string regionCode,string summonerId)
        {
            string[] rankAndTier = new string[2];
            string baseurl = "https://" + regionCode + ".api.riotgames.com";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("X-Riot-Token", RiotAPIKey);
            try
            {
                var response = client.GetStringAsync(baseurl + "/lol/league/v3/leagues/by-summoner/" + summonerId).Result;
                var receivedData = JsonConvert.DeserializeObject<List<LeagueListDTO>>(response);
                rankAndTier[0] = GetTierFromLeagueList(receivedData);
                rankAndTier[1] = GetRankFromLeagueList(receivedData, summonerId);
                return rankAndTier;
            }
            catch(AggregateException)
            {
                return new string[2];
            }
        }

        public LOLUserInfo GetBasicUserInfo(string regionCode, string summonerName)
        {
            LOLUserInfo _userInfo = new LOLUserInfo();
            string baseurl = "https://" + regionCode + ".api.riotgames.com";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("X-Riot-Token", RiotAPIKey);
            try
            {
                var response = client.GetStringAsync(baseurl + "/lol/summoner/v3/summoners/by-name/" + summonerName).Result;
                var receivedUser = JsonConvert.DeserializeObject<SummonerDTO>(response);
                _userInfo.Name = receivedUser.name;
                _userInfo.SummonerId = receivedUser.id;
                _userInfo.Summonerlvl = receivedUser.summonerLevel;
                _userInfo.AccountId = receivedUser.accountId;
                return _userInfo;
            }
            catch(AggregateException)
            {
                return _userInfo;
            }
        }

        public LOLUserInfo GetCompleteUserBySummonerName(string summonerName,string region)
        {
            string regionCode = regionToCode[region];
            LOLUserInfo _userInfo = GetBasicUserInfo(regionCode, summonerName);
            if(_userInfo != new LOLUserInfo())
            {
                string[] userTierRank = GetTierAndRank(regionCode, _userInfo.SummonerId);
                _userInfo.Rank = userTierRank[1];
                _userInfo.Tier = userTierRank[0];
            }
            return _userInfo;
        }

        public bool AuthorizeLOLAccount(string summonerName,string region,string masteryPage)
        {
            string baseurl = "https://" + regionToCode[region] + ".api.riotgames.com";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("X-Riot-Token", RiotAPIKey);
            try
            {
                var response = client.GetStringAsync(baseurl + "/lol/platform/v3/masteries/by-summoner/" + GetBasicUserInfo(regionToCode[region],summonerName).SummonerId).Result;
                var receivedUser = JsonConvert.DeserializeObject<MasteryPagesDTO>(response);
                if(receivedUser.pages.Where(p => p.name == masteryPage).Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (AggregateException)
            {
                return false;
            }
        }
    }
}
