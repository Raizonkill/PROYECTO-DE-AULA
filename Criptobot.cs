using System;
using System.Threading.Tasks;

class Program
{
    static bool exit = false;
    private static readonly string apiKey = "kQ8XSZzauiekT9xr8W9ZI7jcqXqxoUmcelQw6i7nPF9TAs6knwDydLj6ZtJ6vYMx";
    private static readonly string SecurityKey = "OzLU93RLa2LlUkYh7FyLGCEpoS0mAh692GVmCKZky7EtjX92eDXdHzNk9vEt1D6n";

    private static async Task Main(string[] args)
    {
        var _public_api = new CCXT.NET.Binance.Public.PublicApi();
        var _private_api = new CCXT.NET.Binance.Private.PrivateApi(apiKey, SecurityKey);

        var balance = await _private_api.FetchBalancesAsync();

        // primero miremos que trae el balance
        foreach (var item in balance.result)
        {
            if (item.free > 0 || item.used > 0)
            {
                Console.WriteLine(" Balance: " + item.currency + " " + item.free);
            }
        }
    }
}