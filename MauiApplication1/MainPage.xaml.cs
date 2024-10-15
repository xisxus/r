namespace MauiApplication1
{
    public partial class MainPage : ContentPage
    {
        private ProductService _productService;

        public MainPage()
        {
            InitializeComponent();
            _productService = new ProductService();
            LoadProduct();
        }

        private async void LoadProduct()
        {
           var prod = await _productService.GetProductsAsync();
            ProductListView.ItemsSource = prod;
        }

        private async void OnAddProductClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddProdPage());
        }

       

        private async void OnProductSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                var prod = (Product)e.SelectedItem;
                bool conf = await DisplayAlert("delete", "are u Sure", "yes", "no");
                if (conf)
                {
                    await _productService.DeleteProductAsync(prod.productId);
                    LoadProduct();
                }
            }
        }
    }

}
