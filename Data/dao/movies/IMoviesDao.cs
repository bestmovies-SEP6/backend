namespace Data.dao.movies; 

public interface IMoviesDao {
    Task AddIfNotExists(int movieId);
}