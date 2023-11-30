namespace Data.dao.movies; 

public interface IMovieDao {
    Task AddIfNotExists(int movieId);
}