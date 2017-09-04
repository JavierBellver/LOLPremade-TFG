using lolpremade.Data;
using lolpremade.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lolpremade.DAL
{
    public class UnitOfWork
    {
        private bool disposed = false;
        private LolpremadeContext _context;
        private GenericRepository<User> userRepository;
        private GenericRepository<Team> teamRepository;
        private GenericRepository<TeamInvitation> teamInvitationsRepository;
        private GenericRepository<LOLUserInfo> lolUserInfoInfoRepository;
        private GenericRepository<UserPositionInTeam> userPositionInTeamRepository;
        private GenericRepository<Tournament> tournamentRepository;
        private GenericRepository<UserOpinion> usersOpinionsRepository;
        private GenericRepository<Message> messagesRepository;
        private GenericRepository<TournamentParticipant> tournamentParticipantRepository;

        public UnitOfWork(LolpremadeContext context)
        {
            _context = context;
        }

        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(_context);
                }
                return userRepository;
            }
        }

        public GenericRepository<Team> TeamRepository
        {
            get
            {
                if (this.teamRepository == null)
                {
                    this.teamRepository = new GenericRepository<Team>(_context);
                }
                return teamRepository;
            }
        }

        public GenericRepository<TeamInvitation> TeamInvitationsRepository
        {
            get
            {
                if (this.teamInvitationsRepository == null)
                {
                    this.teamInvitationsRepository = new GenericRepository<TeamInvitation>(_context);
                }
                return teamInvitationsRepository;
            }
        }

        public GenericRepository<LOLUserInfo> LOLUsersInfoRepository
        {
            get
            {
                if (this.lolUserInfoInfoRepository == null)
                {
                    this.lolUserInfoInfoRepository = new GenericRepository<LOLUserInfo>(_context);
                }
                return lolUserInfoInfoRepository;
            }
        }

        public GenericRepository<UserPositionInTeam> UserPositionInTeamRepository
        {
            get
            {
                if (this.userPositionInTeamRepository == null)
                {
                    this.userPositionInTeamRepository = new GenericRepository<UserPositionInTeam>(_context);
                }
                return userPositionInTeamRepository;
            }
        }

        public GenericRepository<Tournament> TournamentRepository
        {
            get
            {
                if (this.tournamentRepository == null)
                {
                    this.tournamentRepository = new GenericRepository<Tournament>(_context);
                }
                return tournamentRepository;
            }
        }

        public GenericRepository<UserOpinion> UsersOpinionsRepository
        {
            get
            {
                if (this.usersOpinionsRepository == null)
                {
                    this.usersOpinionsRepository = new GenericRepository<UserOpinion>(_context);
                }
                return usersOpinionsRepository;
            }
        }

        public GenericRepository<Message> MessagesRepository
        {
            get
            {
                if (this.messagesRepository == null)
                {
                    this.messagesRepository = new GenericRepository<Message>(_context);
                }
                return messagesRepository;
            }
        }

        public GenericRepository<TournamentParticipant> TournamentParticipantRepository
        {
            get
            {
                if (tournamentParticipantRepository == null)
                {
                    this.tournamentParticipantRepository = new GenericRepository<TournamentParticipant>(_context);
                }
                return tournamentParticipantRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
