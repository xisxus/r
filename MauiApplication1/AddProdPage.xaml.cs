namespace MauiApplication1;

public partial class AddProdPage : ContentPage
{
	private ProductService _productService;
	public AddProdPage()
	{
		InitializeComponent();
		_productService = new ProductService();
	}

    private async void OnSaveClicked(object sender, EventArgs e)
    {
		var prod = new Product()
		{
			name = NameEntry.Text,
			price = decimal.Parse(PriceEntry.Text),
			stock = int.Parse(StockEntry.Text)
        };

		await _productService.AddProductAsync(prod);
		await DisplayAlert("succ", "added", "ok");
		await Navigation.PushAsync(new MainPage());
    }
}