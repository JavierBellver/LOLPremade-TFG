using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lolpremade.Models;
using lolpremade.Data;
using lolpremade.Utils;
using Bogus;
using lolpremade.Utils.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace lolpremade.Models
{
    public class DbInitializer
    {
        public static void Initialize(LolpremadeContext context)
        {
            context.Database.EnsureCreated();
            var faker = new Faker("en");

            if (!context.Users.Any())
            {
                string salt = CryptoUtils.GetSalt();
                UserPositionInTeam position = new UserPositionInTeam
                {
                    Role = "ADC",
                    isSubstitute = false,
                    TeamID = 1
                };
                UserPositionInTeam position2 = new UserPositionInTeam
                {
                    Role = "Support",
                    isSubstitute = false,
                    TeamID = 1
                };
                var users = new User[]
                {
                    new User{ Email="pedro@gmail.com",Username="paco",Salt=salt, Password=CryptoUtils.HashWithSHA256("pico",salt), PertainingTeam = 1, Country="", Language="",PlayRegion="Europe West", Rank="",Role="", Level="", PositionOnTeam = position  },
                    new User{ Email="another@gmail.com",Username="another",Salt=salt, Password=CryptoUtils.HashWithSHA256("pico",salt), PertainingTeam = 1, Country="", Language="",PlayRegion="", Rank="",Role="",Level="", PositionOnTeam = position2  },
                };
                foreach (User u in users)
                {
                    context.Users.Add(u);
                }
                context.SaveChanges();
                var users2 = new User[]
                {
                    new User{ Email="teamowner@gmail.com",Username="teamowner",Salt=salt, Password=CryptoUtils.HashWithSHA256("pico",salt), PertainingTeam = 0, Country="", Language="",PlayRegion="", Rank="",Role="",Level="" },
                    new User{ Email="inviteman1@gmail.com",Username="inviteman1",Salt=salt, Password=CryptoUtils.HashWithSHA256("pico",salt), PertainingTeam = 0, Country="", Language="",PlayRegion="", Rank="",Role="",Level=""  },
                    new User{ Email="inviteman2@gmail.com",Username="inviteman2",Salt=salt, Password=CryptoUtils.HashWithSHA256("pico",salt), PertainingTeam = 0, Country="", Language="",PlayRegion="", Rank="",Role="",Level=""  }
                };
                foreach (User u in users2)
                {
                    context.Users.Add(u);
                }
                context.SaveChanges();
                AddRandomUsers(salt, context);
            }
            if (!context.Teams.Any())
            {
                var teams = new Team[]
                {
                    new Team { Name="PepeTeam", Description="Pepe's team", teamAdmin = 1, Country="", Language="", PlayRegion="Europe West", TeamRank=""}
                };
                foreach (Team t in teams)
                {
                    context.Teams.Add(t);
                }
                context.SaveChanges();
                AddRandomTeams(context);
            }
            if (!context.TeamInvitations.Any())
            {
                var teamInitations = new TeamInvitation[]
                {
                    new TeamInvitation
                    {
                        SenderTeamId = 1,
                        ReceiverUserId = 1,
                        IssuedDate = new DateTime(),
                        Accepted = false
                    }
                };
                foreach (TeamInvitation t in teamInitations)
                {
                    context.TeamInvitations.Add(t);
                }
                context.SaveChanges();
            }
            if (!context.Tournaments.Any())
            {
                var tournaments = new Tournament[]
                {
                    new Tournament { Name = "MegaTournament", Description = "By the creators of SuperTournament arrives MegaTournament", Rank = "CHALLENGER", Language="Arabic", Country="Austria", OrganizerName = "SupaOrganizers", NumberOfParticipantTeams = 12, NumberOfCurrentParticipants = 0 },
                    new Tournament { Name = "SupaTournament", Description = "By the creators of SupaTournament arrives SupaTournament", Rank = "DIAMOND V", Language="Spanish", Country="Spain", OrganizerName = "MegOrgs", NumberOfParticipantTeams = 8, NumberOfCurrentParticipants = 0 },
                    new Tournament { Name = "YoYOTournament", Description = "The Yoyo Tournament for amateurs", Rank = "UNRANKED", Language="English", Country="Brazil", OrganizerName = "TheFound", NumberOfParticipantTeams = 4, NumberOfCurrentParticipants = 0 },
                    new Tournament { Name = "AmazingLeague", Description = "The most amazing teams play at amazing league", Rank = "CHALLENGER", Language="Esperanto", Country="Germany", OrganizerName = "Browncoat association", NumberOfParticipantTeams = 12, NumberOfCurrentParticipants = 12 }
                };
                foreach (Tournament t in tournaments)
                {
                    context.Tournaments.Add(t);
                }
                context.SaveChanges();
            }
            if (!context.UserOpinions.Any())
            {
                var opinions = new UserOpinion[]
                {
                    new UserOpinion
                    {
                        SenderUserId = 1,
                        ReceivingUserId = 2,
                        opinionDate = DateTime.Now,
                        punctuation = 4.0,
                        shortText = "Short comment text",
                        commentText = "Comentary Lorem Ipsom Ipsom Lorem Ipsum"
                    }
                };
                foreach (UserOpinion o in opinions)
                {
                    context.UserOpinions.Add(o);
                }
                context.SaveChanges();
            }
            if(!context.Messages.Any())
            {
                var messages = new Message[]
                {
                    new Message
                    {
                        SenderUserId = 1,
                        ReceivingUserId = 2,
                        Subject = "This is the subject",
                        MessageText = "This is the main text of the message",
                        MessageDate = DateTime.Now
                    }
                };
                foreach (Message m in messages)
                {
                    context.Messages.Add(m);
                }
                context.SaveChanges();
            }
            if(!context.TournamentParticipants.Any())
            {
                var participants = new TournamentParticipant[]
                {
                    new TournamentParticipant
                    {
                        TournamentId = 1,
                        TeamId = 2
                    }
                };
                foreach (TournamentParticipant p in participants)
                {
                    context.TournamentParticipants.Add(p);
                }
                context.SaveChanges();
            }
        }

        public static void TestInitiaize(LolpremadeContext context)
        {
            context.Database.EnsureCreated();
            var faker = new Faker("en");

            if (!context.Users.Any())
            {
                string salt = CryptoUtils.GetSalt();
                UserPositionInTeam position = new UserPositionInTeam
                {
                    Role = "ADC",
                    isSubstitute = false,
                    TeamID = 1
                };
                UserPositionInTeam position2 = new UserPositionInTeam
                {
                    Role = "Support",
                    isSubstitute = false,
                    TeamID = 1
                };
                var users = new User[]
                {
                    new User{ Email="pedro@gmail.com",Username="paco",Salt=salt, Password=CryptoUtils.HashWithSHA256("pico",salt), PertainingTeam = 1, Country="", Language="",PlayRegion="Europe West", Rank="",Role="", Level="", PositionOnTeam = position  },
                    new User{ Email="another@gmail.com",Username="another",Salt=salt, Password=CryptoUtils.HashWithSHA256("pico",salt), PertainingTeam = 1, Country="", Language="",PlayRegion="", Rank="",Role="",Level="", PositionOnTeam = position2  },
                };
                foreach (User u in users)
                {
                    context.Users.Add(u);
                }
                context.SaveChanges();
                var users2 = new User[]
                {
                    new User{ Email="teamowner@gmail.com",Username="teamowner",Salt=salt, Password=CryptoUtils.HashWithSHA256("pico",salt), PertainingTeam = 0, Country="", Language="",PlayRegion="", Rank="",Role="",Level="" },
                    new User{ Email="inviteman1@gmail.com",Username="inviteman1",Salt=salt, Password=CryptoUtils.HashWithSHA256("pico",salt), PertainingTeam = 0, Country="", Language="",PlayRegion="", Rank="",Role="",Level=""  },
                    new User{ Email="inviteman2@gmail.com",Username="inviteman2",Salt=salt, Password=CryptoUtils.HashWithSHA256("pico",salt), PertainingTeam = 0, Country="", Language="",PlayRegion="", Rank="",Role="",Level=""  }
                };
                foreach (User u in users2)
                {
                    context.Users.Add(u);
                }
                context.SaveChanges();
            }
            if (!context.Teams.Any())
            {
                var teams = new Team[]
                {
                    new Team { Name="PepeTeam", Description="Pepe's team", teamAdmin = 1, Country="", Language="", PlayRegion="Europe West", TeamRank=""},
                    new Team { Name="SupaTeam", Description="Tha Suppa Team", teamAdmin = 2, Country="", Language="", PlayRegion="Europe West", TeamRank="" }
                };
                foreach (Team t in teams)
                {
                    context.Teams.Add(t);
                }
                context.SaveChanges();
            }
            if (!context.TeamInvitations.Any())
            {
                var teamInitations = new TeamInvitation[]
                {
                    new TeamInvitation
                    {
                        SenderTeamId = 1,
                        ReceiverUserId = 1,
                        IssuedDate = new DateTime(),
                        Accepted = false
                    }
                };
                foreach (TeamInvitation t in teamInitations)
                {
                    context.TeamInvitations.Add(t);
                }
                context.SaveChanges();
            }
            if (!context.Tournaments.Any())
            {
                var tournaments = new Tournament[]
                {
                    new Tournament
                    {
                        Name = "MegaTournament",
                        Description = "By the creators of SuperTournament arrives MegaTournament",
                        OrganizerName = "TheMegaOrganizers",
                        NumberOfParticipantTeams = 12,
                        NumberOfCurrentParticipants = 0
                    }
                };
                foreach (Tournament t in tournaments)
                {
                    context.Tournaments.Add(t);
                }
                context.SaveChanges();
            }
            if (!context.UserOpinions.Any())
            {
                var opinions = new UserOpinion[]
                {
                    new UserOpinion
                    {
                        SenderUserId = 1,
                        ReceivingUserId = 2,
                        opinionDate = DateTime.Now,
                        punctuation = 4.0,
                        shortText = "Short comment text",
                        commentText = "Comentary Lorem Ipsom Ipsom Lorem Ipsum"
                    }
                };
                foreach (UserOpinion o in opinions)
                {
                    context.UserOpinions.Add(o);
                }
                context.SaveChanges();
            }
            if (!context.Messages.Any())
            {
                var messages = new Message[]
                {
                    new Message
                    {
                        SenderUserId = 1,
                        ReceivingUserId = 2,
                        Subject = "This is the subject",
                        MessageText = "This is the main text of the message",
                        MessageDate = DateTime.Now
                    }
                };
                foreach (Message m in messages)
                {
                    context.Messages.Add(m);
                }
                context.SaveChanges();
            }
            if (!context.TournamentParticipants.Any())
            {
                var participants = new TournamentParticipant[]
                {
                    new TournamentParticipant
                    {
                        TournamentId = 1,
                        TeamId = 2
                    }
                };
                foreach (TournamentParticipant p in participants)
                {
                    context.TournamentParticipants.Add(p);
                }
                context.SaveChanges();
            }
        }

        private static void AddRandomUsers(string salt, LolpremadeContext context)
        {
            var faker = new Faker("en");
            for (int i = 0; i < 1000; i++)
            {
                int Seed = (int)DateTime.Now.Ticks;
                var random = new Random(Seed);
                int randomLanguage = random.Next(Language.ListOfLanguages.Count);
                int randomCountry = random.Next(Country.ListOfCountries.Count);
                int randomRole = random.Next(Role.ListOfRoles.Count);
                int randomRank = random.Next(Rank.ListOfRanks.Count);
                int randomRegion = random.Next(PlayRegion.ListOfPlayRegions.Count);
                User _user = new User
                {
                    Email = faker.Internet.Email(),
                    Username = faker.Internet.UserName(),
                    Salt = salt,
                    Password = CryptoUtils.HashWithSHA256("pico", salt),
                    Country = Country.ListOfCountries[randomCountry].Value,
                    DateOfBirth = faker.Person.DateOfBirth,
                    Language = Language.ListOfLanguages[randomLanguage].Value,
                    Role = Role.ListOfRoles[randomRole].Value,
                    Rank = Rank.ListOfRanks[randomRank].Value,
                    Level = "30",
                    PlayRegion = PlayRegion.ListOfPlayRegions[randomRegion].Value
                };
                try
                {
                    context.Users.Add(_user);
                    context.SaveChanges();
                }
                catch(Exception e) when (e is InvalidOperationException || e is DbUpdateException)
                {
                    context.Users.Remove(_user);
                    i--;
                }
            }
        }

        private static void AddRandomTeams(LolpremadeContext context)
        {
            var faker = new Faker("en");
            for (int i = 0; i < 1000; i++)
            {
                int Seed = (int)DateTime.Now.Ticks;
                var random = new Random(Seed);
                int randomLanguage = random.Next(Language.ListOfLanguages.Count);
                int randomCountry = random.Next(Country.ListOfCountries.Count);
                int randomRank = random.Next(Rank.ListOfRanks.Count);
                int randomRegion = random.Next(PlayRegion.ListOfPlayRegions.Count);
                Team _team = new Team
                {
                    Name = TeamNamesGenerator.GetTeamName(),
                    Description = faker.Lorem.Paragraphs(),
                    Country = Country.ListOfCountries[randomCountry].Value,
                    Language = Language.ListOfLanguages[randomLanguage].Value,
                    TeamRank = Rank.ListOfRanks[randomRank].Value,
                    PlayRegion = PlayRegion.ListOfPlayRegions[randomRegion].Value
                };
                try
                {
                    context.Teams.Add(_team);
                    context.SaveChanges();
                }
                catch(Exception e) when (e is InvalidOperationException ||e is DbUpdateException)
                {
                    context.Teams.Remove(_team);
                    i--;
                }
            }
        }
    }
}
