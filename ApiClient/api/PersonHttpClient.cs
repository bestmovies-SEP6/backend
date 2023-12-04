using System.Text.Json.Serialization;
using ApiClient.util;
using Dto;

namespace ApiClient.api; 

public class PersonHttpClient : IPersonClient {

    private class GetPersonsByMovieIdResponse {

        [JsonPropertyName("cast")]
        public List<PersonDto> Cast { get; set; }
    }


    public async Task<List<PersonDto>> GetPersonsByMovieId(int movieId) {
        var response = await HttpClientUtil.Get<GetPersonsByMovieIdResponse>($"https://api.themoviedb.org/3/movie/{movieId}/credits");
        foreach (PersonDto personDto in response.Cast) {
            personDto.ProfileImage = $"https://image.tmdb.org/t/p/original{personDto.ProfileImage}";
        }

        return response.Cast;
    }
}