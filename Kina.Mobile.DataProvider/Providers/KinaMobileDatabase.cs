using DataModel;
using Kina.Mobile.DataProvider.Models;
using SQLite;
using System;
using System.Collections.Generic;
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
            _database.DropTableAsync<Genre>().Wait();
            _database.CreateTableAsync<Genre>().Wait();

            InsertGenre("dramat", "drama");
            InsertGenre("thriller", "thriller");
            InsertGenre("komedia", "comedy");
            InsertGenre("animowany", "animation");
            InsertGenre("science-fiction", "sci-fi");
            InsertGenre("akcja", "action");
            InsertGenre("romans", "romance");
        }

        private void InsertGenre(string name, string engName)
        {
            Genre genre = new Genre
            {
                Name = name,
                EngName = engName
            };
            _database.InsertAsync(genre).Wait();
        }

        // User Score starts here
        public Task<List<UserScore>> GetUserScoreAsync()
        {
            return _database.Table<UserScore>().ToListAsync();
        }

        public Task<List<UserScore>> GetUserScoreAsync(int Id_User, long Id_Cinema, long Id_Movie)
        {
            return _database.QueryAsync<UserScore>("SELECT * FROM UserScore WHERE Id_User = ? AND Id_Cinema = ? AND Id_Movie = ?", Id_User, Id_Cinema, Id_Movie);
        }

        public Task<List<UserScore>> GetUserScoreAsync(long Id_Cinema, long Id_Movie)
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

        // Genre starts here
        public Task<List<Genre>> GetGenreAsync()
        {
            return _database.Table<Genre>().ToListAsync();
        }

        public Task<Genre> GetGenreAsync(int id)
        {
            return _database.Table<Genre>().Where(g => g.GenreID == id).FirstOrDefaultAsync();
        }

        public Task<Genre> GetGenreAsync(String name)
        {
            return _database.Table<Genre>().Where(g => g.Name == name).FirstOrDefaultAsync();
        }

        public Task<int> SaveGenreAsync(Genre genre)
        {
            if(genre.GenreID != 0)
            {
                return _database.UpdateAsync(genre);
            }
            else
            {
                return _database.InsertAsync(genre);
            }
        }

        public Task<int> DeleteGenreAsync(Genre genre)
        {
            return _database.DeleteAsync(genre);
        }
    }
}
