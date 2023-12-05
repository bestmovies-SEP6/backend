using System.Text.Json.Serialization;
using ApiClient.util;
using Dto;

namespace ApiClient.api;

public class PeopleHttpClient : IPeopleClient
{
    private class PeopleResponseRoot
    {
        [JsonPropertyName("results")]
        public List<PeopleDto>? Results { get; set; }
    }

    private const string URL = "https://api.themoviedb.org/3";

    //Get a list of people ordered by popularity.
    public async Task<List<PeopleDto>>  GetListOfPopularPeople(int pageId) {
        PeopleResponseRoot popularPeoples = await HttpClientUtil.GetWithQuery<PeopleResponseRoot>($"{URL}/person/popular?language=en-US&page={pageId}");
        return ConvertPeople(popularPeoples);
    }

    public async Task<PersonDetailsDto> GetPersonDetailsById(int personId)
    {
        PersonDetailsDto personDetails = await HttpClientUtil.GetWithQuery<PersonDetailsDto>($"{URL}/person/{personId}?language=en-US&append_to_response=combined_credits");
        return ConvertPersonDetails(personDetails);    }

    private PersonDetailsDto ConvertPersonDetails(PersonDetailsDto personDetails)
    {
        personDetails.ProfileImage = $"https://image.tmdb.org/t/p/original{personDetails.ProfileImage}";
        switch (personDetails.Gender)
        {
            case 0:
                personDetails.GenderString = "Not set / not specified";
                break;
            case 1:
                personDetails.GenderString = "Female";
                break;
            case 2:
                personDetails.GenderString = "Male";
                break;
            case 3:
                personDetails.GenderString = "Non-binary";
                break;
            default:
                personDetails.GenderString = "Unknown";
                break;
        }
        if (personDetails.CombinedCredits?.Cast != null)
        {
            personDetails.CombinedCredits.Cast = personDetails.CombinedCredits.Cast
                .Select(m => new MovieOrSeriesDto
                {
                    Id = m.Id,
                    Title = !string.IsNullOrEmpty(m.Title) ? m.Title : m.OriginalName ?? "Title not available",
                    PosterPath = m.PosterPath != null ? $"https://image.tmdb.org/t/p/original{m.PosterPath}" : "Default poster path" 
                })
                .Take(10)
                .ToList();
        }
        if (personDetails.CombinedCredits?.Crew != null)
        {
            personDetails.CombinedCredits.Crew = personDetails.CombinedCredits.Crew
                .Select(m => new MovieOrSeriesDto
                {
                    Id = m.Id,
                    Title = !string.IsNullOrEmpty(m.Title) ? m.Title : m.OriginalName ?? "Title not available",
                    PosterPath = m.PosterPath != null ? $"https://image.tmdb.org/t/p/original{m.PosterPath}" : "Default poster path"
                })
                .Take(10)
                .ToList();
        }

        return personDetails;
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
                    knownFor.Title = knownFor.Title ?? knownFor.Name;
                    if (!string.IsNullOrWhiteSpace(knownFor.Title))
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