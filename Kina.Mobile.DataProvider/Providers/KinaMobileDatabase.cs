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
            var score = new UserScore();
            score.Id_Cinema = 1;
            score.Id_User = 1;
            score.Id_Movie = 1;
            score.Popcorn = 4;
            score.Screen = 4;
            score.Seat = 4;
            score.Sound = 4;
            _database.InsertAsync(score).Wait();
        }

        public Task<List<UserScore>> GetUserScoreAsync()
        {
            return _database.Table<UserScore>().ToListAsync();
        }

        public Task<List<UserScore>> GetUserScoreAsync(int Id_User, int Id_Cinema, int Id_Movie)
        {
            return _database.QueryAsync<UserScore>("SELECT * FROM UserScore WHERE Id_User = ? AND Id_Cinema = ? AND Id_Movie = ?", Id_User, Id_Cinema, Id_Movie);
        }

        public Task<List<UserScore>> GetUserScoreAsync(int Id_Cinema, int Id_Movie)
        {
            return _database.QueryAsync<UserScore>("SELECT * FROM UserScore WHERE Id_Cinema = ? AND Id_Movie = ?", Id_Cinema, Id_Movie);
        }

        public Task<UserScore> GetUserScoreAsync(int Id_UserScore)
        {
            return _database.Table<UserScore>().Where(s => s.Id_UserScore == Id_UserScore).FirstOrDefaultAsync();
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

        public Task<int> DeleteUserScoreAsync(UserScore score)
        {
            return _database.DeleteAsync(score);
        }
    }
}
