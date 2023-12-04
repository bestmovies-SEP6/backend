using System.Text.Json;
using ApiClient.apiMap;
using ApiClient.util;
using Dto;

namespace ApiClient.api;

public class PeopleHttpClient : IPeopleClient
{
    private const string URL = "https://api.themoviedb.org/3";

    //Get a list of people ordered by popularity.
    public async Task<List<PeopleDto>>  GetListOfPopularPeople(int pageId) {
        Console.WriteLine($"{URL}/person/popular?language=en-US&page={pageId}");
        PeopleResponseRoot popularPeoples = await HttpClientUtil.GetWithQuery<PeopleResponseRoot>($"{URL}/person/popular?language=en-US&page={pageId}");
        return ConvertPeople(popularPeoples);
    }

    private List<PeopleDto> ConvertPeople(PeopleResponseRoot responseRoot)
    {
        if (responseRoot.Results is null) {
            throw new Exception("Something went wrong while fetching popular people from tmdb api");
        }

        var filteredList = responseRoot.Results.Where(people => people.Name is not null).ToList();

        foreach (PeopleDto result in filteredList) 
        {
            result.ProfilePath = $"https://image.tmdb.org/t/p/original{result.ProfilePath}";   
            List<KnownForDto> movieTitles = new List<KnownForDto>();

            if (result.KnownForMovies != null)
            {
                foreach (var knownFor in result.KnownForMovies) 
                {
                    string title = knownFor.Title ?? knownFor.Name;
                    if (!string.IsNullOrWhiteSpace(title))
                    {
                        movieTitles.Add(knownFor);
                    }
                }
            }

            result.KnownForMovies = movieTitles;
        }

        return filteredList;
    }
}