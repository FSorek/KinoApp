using DataModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kina.Mobile.DataProvider.Providers
{
    public class KinaMobileDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public KinaMobileDatabase(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.DropTableAsync<UserScore>().Wait();
            _database.CreateTableAsync<UserScore>().Wait();
            _database.DropTableAsync<Cinema>().Wait();
            _database.CreateTableAsync<Cinema>().Wait();
        }

        public Task<List<UserScore>> GetUserScoreAsync()
        {
            return _database.Table<UserScore>().ToListAsync();
        }

        public Task<List<UserScore>> GetUserScoreAsync(int Id_User, int Id_Cinema, string Id_Movie)
        {
            return _database.QueryAsync<UserScore>("SELECT * FROM UserScore WHERE Id_User = ? AND Id_Cinema = ? AND Id_Movie = ?", Id_User, Id_Cinema, Id_Movie);
        }

        public Task<List<UserScore>> GetUserScoreAsync(int Id_Cinema, string Id_Movie)
        {
            return _database.QueryAsync<UserScore>("SELECT * FROM UserScore WHERE Id_Cinema = ? AND Id_Movie = ?", Id_Cinema, Id_Movie);
        }

        public Task<UserScore> GetUserScoreAsync(int Id_UserScore)
        {
            return _database.Table<UserScore>().Where(s => s.Id_UserScore == Id_UserScore).FirstOrDefaultAsync();
        }

        public Task<Cinema> GetCinemaAsync(int Id_Cinema)
        {
            return _database.Table<Cinema>().Where(s => s.Id_Cinema == Id_Cinema).FirstOrDefaultAsync();
        }

        public Task<List<Cinema>> GetAllCinemaAsync()
        {
            return _database.QueryAsync<Cinema>("SELECT * FROM Cinema");
        }

        public Task<int> SaveUserScoreAsync(UserScore score)
        {
            if (score.Id_UserScore != 0)
            {
                return _database.UpdateAsync(score);
            }
            else
            {
                return _database.InsertAsync(score);
            }
        }

        public Task<int> SaveCinemaAsync(Cinema cinema)
        {
            if (cinema.Id_Cinema != 0)
            {
                return _database.UpdateAsync(cinema);
            }
            else
            {
                return _database.InsertAsync(cinema);
            }
        }

        public Task<int> DeleteUserScoreAsync(UserScore score)
        {
            return _database.DeleteAsync(score);
        }
    }
}
