using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SoalJavab.DomainClasses;

namespace SoalJavab.Services.Contracts
{
    public interface ISoalRepository : IGenericRepository<Soal>
    {
        #region sync
        bool Follow(long UserID, long SoalID);
        IEnumerable<ApplicationUser> GetAllFollowers(long SoalID);
        IEnumerable<Soal> GetAllByzirreshteh(long ZirReshtehID);
        IEnumerable<Soal> GetAllByzirreshteh(long[] ZirReshtehID);
        IEnumerable<Soal> GetAllByTag(long TagId);
        IEnumerable<Soal> GetAllByUserID(long IdUser);
        IEnumerable<Soal> GetAllDeletedByUserID(long IdUser);
        Task<IEnumerable<Soal>> GetAllByUserIdAsync(long IdUser);
        long lastSoalToUserId(long Id);
        bool Insert(Soal entity, long[] tag, IList<ApplicationUser> user);
        bool Insert(Soal entity, long[] tag);
        bool DeletebyId(long Id);
        #endregion


        /// ASYNC PROCS

        #region async
        Task<bool> FollowAsync(long UserID, long SoalID);
        Task<IEnumerable<ApplicationUser>> GetAllFollowersAsync(long SoalID);
        Task<IEnumerable<Soal>> GetAllByzirreshtehAsync(long ZirReshtehID);
        Task<IEnumerable<Soal>> GetAllByzirreshtehAsync(long[] ZirReshtehID);
        Task<IEnumerable<Soal>> GetAllByTagAsync(long TagId);
        Task<IEnumerable<Soal>> GetAllByUserIDAsync(long UserID);
        Task<IEnumerable<Soal>> GetAllByUserIdAsyncAsync(long UserID);
        Task<long> LastSoalToUserIdAsync(long Id);
        Task<bool> InsertAsync(Soal entity, long[] tag, IList<ApplicationUser> user);
        Task<bool> InsertAsync(Soal entity, long[] tag);
        Task<bool> DeletebyIdAsync(long Id);
        #endregion


    }
}
