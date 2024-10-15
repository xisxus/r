using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiApplication1
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService()
        {
            _httpClient = new HttpClient();
            // Set your API Base Address (adjust based on the platform)
            _httpClient.BaseAddress = new Uri(DeviceInfo.Platform == DevicePlatform.Android
                ? "http://10.0.2.2:5143"
                : "http://localhost:5143");
        }

        // GET all products
        public async Task<List<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync("api/Products");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<List<Product>>(json);
                return data;
            }
            return null;
        }

        // GET product by ID
        public async Task<Product> GetProductAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/Products/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Product>(json);
            }
            return null;
        }

        // POST (create) a new product
        public async Task<bool> AddProductAsync(Product product)
        {
            var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/Products", content);
            return response.IsSuccessStatusCode;
        }

        // PUT (update) a product
        public async Task<bool> UpdateProductAsync(int id, Product product)
        {
            var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/Products/{id}", content);
            return response.IsSuccessStatusCode;
        }

        // DELETE a product
        public async Task<bool> DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Products/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
