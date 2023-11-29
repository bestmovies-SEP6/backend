using System.Text.Json;
using System.Text.Json.Nodes;
using ApiClient.apiMap;
using ApiClient.util;
using Dto;

namespace ApiClient.api;

public class MovieHttpClient : IMovieClient {
    public async Task<List<MovieDto>> GetNowPlaying() {
        MovieIdResponse jsonString =
            await HttpClientUtil.Get<MovieIdResponse>("https://api.themoviedb.org/3/movie/now_playing");

        throw new NotImplementedException();
    }
}