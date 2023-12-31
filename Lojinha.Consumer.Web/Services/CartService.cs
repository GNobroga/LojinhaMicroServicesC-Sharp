
using Lojinha.Consumer.Web.Models;
using Lojinha.Consumer.Web.Services;
using Lojinha.Consumer.Web.Utils;

namespace Lojinha.Consumer.Web.Services;

public class CartService : ICartService
{
    private readonly HttpClient _httpClient;

    public CartService(IHttpClientFactory factory)
    {
        ArgumentNullException.ThrowIfNull(factory);
        _httpClient = factory.CreateClient("CartAPI");
    }

    public async Task<Cart> AddItemToCart(Cart cart, string userId)
    {
        var response = await _httpClient.MakePostRequest($"add-cart/{userId}", cart);
        return await response.ConvertJsonToModel<Cart>();
    }   

    public async Task<bool> ApplyCoupon(long cartDetailId, string couponCode)
    {
        await _httpClient.MakeGetRequest($"add-cupom/{cartDetailId}/{couponCode}");
        return true;
    }

    public async Task<bool> CheckoutCart(Cart cart)
    {
        await _httpClient.MakePostRequest("checkout", cart);

        return true;
    }

    public Task<bool> ClearCart(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<Cart> FindCartByUserId(string userId)
    {
        var response = await _httpClient.MakeGetRequest($"find-cart/{userId}");
        return await response.ConvertJsonToModel<Cart>();
    }

    public async Task<bool> RemoveCart(string userId)
    {
        var response = await _httpClient.MakeDeleteRequest($"delete-cart/{userId}");
        await response.ConvertJsonToModel<bool>();
        return true;
    }

    public async Task<bool> RemoveCoupon(long cartDetailId)
    {
       await _httpClient.MakeGetRequest($"remove-cupom/{cartDetailId}");
       return true;
    }

    public async Task<bool> RemoveItemFromCart(long cartDetailId)
    {
        var response = await _httpClient.MakeDeleteRequest($"remove-cart-detail/{cartDetailId}");
        return true;
    }

    public async Task<Cart> UpdateCart(Cart cart, string userId)
    {
        var response = await _httpClient.MakePutRequest($"update-cart/{userId}", cart);
        return await response.ConvertJsonToModel<Cart>();
    }
}