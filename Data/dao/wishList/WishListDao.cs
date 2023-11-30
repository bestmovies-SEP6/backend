using System.Data;
using Dto;
using Entity;
using EntityFramework.Exceptions.Common;
using Microsoft.EntityFrameworkCore;

namespace Data.dao.wishList;

public class WishListDao : IWishListDao {
    private readonly DatabaseContext _databaseContext;

    public WishListDao(DatabaseContext databaseContext) {
        _databaseContext = databaseContext;
    }

    public async Task AddMovieToWishList(string loggedInUser, int movieId) {
        try {
            // UserEntity? userEntity = await _databaseContext.Users.FindAsync(loggedInUser);
            // MovieEntity? movieEntity = await _databaseContext.Movies.FindAsync(movieId);

            WishListEntity wishListEntity = new WishListEntity() {
                Username = loggedInUser,
                MovieId = movieId,
                WishListedAt = DateTime.Now
            };

            await _databaseContext.WishLists.AddAsync(wishListEntity);
            await _databaseContext.SaveChangesAsync();
        }
        catch (Exception e) {
            if (e is UniqueConstraintException) {
                throw new Exception(ErrorMessages.MovieAlreadyInWishlist);
            }

            throw;
        }
    }

    public async Task<List<int>> GetAllWishListedMovies(string username) {
        List<int> movieIds = await _databaseContext.WishLists
            .Where(wishListEntity => wishListEntity.Username == username)
            .Select(wishListEntity => wishListEntity.MovieId)
            .ToListAsync();

        return movieIds;
    }
}