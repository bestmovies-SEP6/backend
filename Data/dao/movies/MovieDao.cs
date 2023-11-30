using System.Data;
using Entity;
using EntityFramework.Exceptions.Common;

namespace Data.dao.movies;

public class MovieDao : IMovieDao {
    private readonly DatabaseContext _databaseContext;

    public MovieDao(DatabaseContext databaseContext) {
        _databaseContext = databaseContext;
    }


    public async Task AddIfNotExists(int movieId) {
      MovieEntity? movieEntity =  await _databaseContext.Movies.FindAsync(movieId);
      if (movieEntity is not null) return;

      MovieEntity movieEntityToBeAdded = new MovieEntity() {
          Id = movieId
      };
      await _databaseContext.AddAsync(movieEntityToBeAdded);
      await _databaseContext.SaveChangesAsync();
    }
}