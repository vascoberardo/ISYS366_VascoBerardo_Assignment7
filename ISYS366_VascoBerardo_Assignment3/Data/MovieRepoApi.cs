using ISYS366_VascoBerardo_Assignment3.Models;

namespace ISYS366_VascoBerardo_Assignment3.Data
{
    public class MovieRepoApi : IMovieRepo
    {
        private readonly HttpClient _client;

        public MovieRepoApi(HttpClient client)
        {
            _client = client;
        }

        public async Task<IEnumerable<Movie>> GetAllMoviesAsync()
        {
            try
            {
                var response = await _client.GetFromJsonAsync<ApiResponse<List<Movie>>>("api/movies");

                if (response?.Data != null)
                {
                    return response.Data;
                }

                return new List<Movie>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all movies: {ex.Message}");
                return new List<Movie>();
            }
        }

        public async Task<Movie?> GetByIdAsync(int id)
        {
            try
            {
                var response = await _client.GetFromJsonAsync<ApiResponse<Movie>>($"api/movies/{id}");

                if (response?.Data != null)
                {
                    return response.Data;
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting movie by id: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<string>> GetAllGenresAsync()
        {
            try
            {
                var response = await _client.GetFromJsonAsync<ApiResponse<List<string>>>("api/movies/genres/list");

                if (response?.Data != null)
                {
                    return response.Data;
                }

                return new List<string>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error getting all genres: {ex.Message}");
                return new List<string>();
            }
        }

        public async Task<IList<Movie>> SearchMoviesAsync(string? searchString, string? genre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchString))
                {
                    return new List<Movie>();
                }

                var response = await _client.GetFromJsonAsync<ApiResponse<List<Movie>>>($"api/movies/search/{searchString}");

                if (response?.Data != null)
                {
                    var movies = response.Data;

                    if (!string.IsNullOrWhiteSpace(genre))
                    {
                        movies = movies.Where(m => m.Genre == genre).ToList();
                    }

                    return movies;
                }

                return new List<Movie>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching movies: {ex.Message}");
                return new List<Movie>();
            }
        }

        public async Task AddMovieAsync(Movie movie)
        {
            try
            {
                var response = await _client.PostAsJsonAsync("api/movies", movie);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error adding movie: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding movie: {ex.Message}");
            }
        }

        public async Task UpdateMovieAsync(Movie movie)
        {
            try
            {
                var response = await _client.PutAsJsonAsync($"api/movies/{movie.Id}", movie);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error updating movie: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating movie: {ex.Message}");
            }
        }

        public async Task DeleteMovieAsync(int id)
        {
            try
            {
                var response = await _client.DeleteAsync($"api/movies/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error deleting movie: {response.StatusCode} - {errorContent}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting movie: {ex.Message}");
            }
        }
    }

    public class ApiResponse<T>
    {
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}