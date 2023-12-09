using Dto;
using Entity;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;

namespace Data.dao.favorites;

public class FavoritesDao : IFavoritesDao {
    private readonly DatabaseContext _databaseContext;

    public FavoritesDao(DatabaseContext databaseContext) {
        _databaseContext = databaseContext;
    }

    public async Task AddMovieToFavorites(string loggedInUser, int movieId) {
        try {
            FavoriteEntity favoriteEntity = new() {
                Username = loggedInUser,
                MovieId = movieId,
                FavoritedAt = DateTime.UtcNow
            };

            await _databaseContext.Favorites.AddAsync(favoriteEntity);
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception e) {
            if (e is UniqueConstraintException) {
                throw new Exception(ErrorMessages.MovieAlreadyInFavorites);
            }

            throw;
        }
    }

    public async Task<List<int>> GetAllFavoriteMovies(string username) {
        List<int> movieIds = await _databaseContext.Favorites
            .Where(wishListEntity => wishListEntity.Username == username)
            .Select(wishListEntity => wishListEntity.MovieId)
            .ToListAsync();

        return movieIds;
    }

    public async Task RemoveMovieFromFavorites(string loggedInUser, int movieId) {
        FavoriteEntity? favoriteEntity = await _databaseContext.Favorites.FirstOrDefaultAsync(entity =>
            entity.Username.Equals(loggedInUser) && entity.MovieId == movieId);

        if (favoriteEntity is null) {
            throw new Exception(ErrorMessages.MovieAlreadyInFavorites);
        }

        _databaseContext.Favorites.Remove(favoriteEntity);
        await _databaseContext.SaveChangesAsync();
    }
}