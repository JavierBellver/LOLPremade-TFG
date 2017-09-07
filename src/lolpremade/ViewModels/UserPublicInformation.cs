using lolpremade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lolpremade.ViewModels
{
    public class UserPublicInformation
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Rank { get; set; }
        public int PertainingTeam { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Country { get; set; }
        public string Language { get; set; }
        public string PlayRegion { get; set; }
        public string Level { get; set; }
        string userAvatar = "images/add_friend_article_banner.jpg";
        public string UserAvatar { get { return this.userAvatar; } set { this.userAvatar = value; } }

        public UserPublicInformation(User linkedUser)
        {
            ID = linkedUser.ID;
            Email = linkedUser.Email;
            Username = linkedUser.Username;
            Role = linkedUser.Role;
            Rank = linkedUser.Rank;
            PertainingTeam = linkedUser.PertainingTeam;
            DateOfBirth = linkedUser.DateOfBirth;
            Country = linkedUser.Country;
            Language = linkedUser.Language;
            PlayRegion = linkedUser.PlayRegion;
            Level = linkedUser.Level;
            UserAvatar = linkedUser.UserAvatar;
        }
    }
}
