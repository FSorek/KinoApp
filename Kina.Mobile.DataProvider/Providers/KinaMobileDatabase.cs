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
			_database.DropTableAsync<Show>().Wait();
            _database.CreateTableAsync<Show>().Wait();
			_database.DropTableAsync<Movie>().Wait();
            _database.CreateTableAsync<Movie>().Wait();
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
		
		public Task<List<Show>> GetShowAsync()
        {
            return _database.Table<Show>().ToListAsync();
        }
		
		public Task<Show> GetShowAsync(int Id_Show)
        {
            return _database.Table<Show>().Where(s => s.Id_Show == Id_Show).FirstOrDefaultAsync();
			
        }
		
		public Task<int> SaveShowAsync(Show sh)
        {
            if (sh.Id_Show != 0)
            {
                return _database.UpdateAsync(sh);
            }
            else
            {
                return _database.InsertAsync(sh);
            }
        }

        public Task<int> DeleteShowAsync(Show sh)
        {
            return _database.DeleteAsync(sh);
        }
		
		public Task<List<Movie>> GetMovieAsync()
        {
            return _database.Table<Movie>().ToListAsync();
        }

        public Task<List<Movie>> GetMovieAsync(int Id_Movie, String Name)
        {
            return _database.QueryAsync<Movie>("SELECT * FROM Movie WHERE Id_Movie = ? OR Name = ? ", Id_Movie, Name);
        }
		
		public Task<Movie> GetMovieAsync(int Id)
        {
            return _database.Table<Movie>().Where(s => s.Id == Id).FirstOrDefaultAsync();
			
        }

        public Task<Movie> GetMovieAsync(String Name)
        {
            return _database.Table<Movie>().Where(s => s.Name == Name).FirstOrDefaultAsync();

        }
        public Task<int> SaveMovieAsync(Movie film)
        {
            if (film.Id != 0)
            {
                return _database.UpdateAsync(film);
            }
            else
            {
                return _database.InsertAsync(film);
            }
        }

        public Task<int> DeleteMovieAsync(Movie film)
        {
            return _database.DeleteAsync(film);
        }
    }
}
