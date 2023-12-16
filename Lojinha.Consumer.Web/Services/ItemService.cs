using Lojinha.Consumer.Web.Models;
using Lojinha.Consumer.Web.Utils;

namespace Lojinha.Consumer.Web.Services;

public class ItemService : IItemService
{
    private readonly IHttpClientFactory _factory;

    private const string HTTP_CLIENT_NAME = "ItemAPI";

    public ItemService(IHttpClientFactory factory, IConfiguration configuration)
    {
        _factory = factory;
    }

    public async Task<ItemModel> Create(ItemModel vo)
    {
        var http = GetHttpClient();

        var res = await http.MakePostRequest($"items", vo);

        return await res.ConvertJsonToModel<ItemModel>();
    }

    public async Task Delete(long id)
    {
        var http = GetHttpClient();

        await http.MakeDeleteRequest($"items/{id}");
    }   

    public async Task<IEnumerable<ItemModel>> FindAll()
    {
        var http = GetHttpClient();

        var res = await http.MakeGetRequest("items");

        return await res.ConvertJsonToModel<List<ItemModel>>();
    }

    public async Task<ItemModel> FindById(long id)
    {
        var http = GetHttpClient();

        var res = await http.MakeGetRequest($"items/{id}");

        return await res.ConvertJsonToModel<ItemModel>();
    }

    public async Task<ItemModel> Update(long id, ItemModel vo)
    {
        var http = GetHttpClient();

        var res = await http.MakePutRequest($"items/{id}", vo);

        return await res.ConvertJsonToModel<ItemModel>();
    }

    private HttpClient GetHttpClient()
    {
        return _factory.CreateClient(HTTP_CLIENT_NAME);
    }
}