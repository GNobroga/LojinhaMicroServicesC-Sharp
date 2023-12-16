namespace Lojinha.Consumer.Web.Utils;

public static class HttpClientExtensions
{
    public static async Task<T> ConvertJsonToModel<T>(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode) 
            throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

        var data = await response.Content.ReadFromJsonAsync<T>() ?? throw new ApplicationException("Unable to convert the object type to the specified data.");

        return data;
    }

    public static async Task<HttpResponseMessage> MakeGetRequest(this HttpClient http, string endpoint)
    {   

        var response = await http.GetAsync(endpoint);

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"An error occurred during the get request: {response.ReasonPhrase}");
    
        return response;
    }

    public static async Task<HttpResponseMessage> MakePostRequest<T>(this HttpClient http, string endpoint, T data)
    { 
        var response = await http.PostAsJsonAsync(endpoint, data);

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"An error occurred during the post request: {response.ReasonPhrase}");
    
        return response;
    }

    public static async Task<HttpResponseMessage> MakePutRequest<T>(this HttpClient http, string endpoint, T data)
    { 
        var response = await http.PutAsJsonAsync(endpoint, data);

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"An error occurred during the put request: {response.ReasonPhrase}");
    
        return response;
    }

    public static async Task<HttpResponseMessage> MakeDeleteRequest(this HttpClient http, string endpoint)
    { 
        var response = await http.DeleteAsync(endpoint);

        if (!response.IsSuccessStatusCode)
            throw new ApplicationException($"An error occurred during the put request: {response.ReasonPhrase}");
    
        return response;
    }

}