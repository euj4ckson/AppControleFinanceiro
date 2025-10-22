namespace AppControleFinanceiro.Views;

public partial class TransactionList : ContentPage
{
	public TransactionList()
	{
		InitializeComponent();
	}
	private void OnButtonClicked_to_TransactionAdd(object sender, EventArgs args)
	{
        if (App.Current != null)
            App.Current.MainPage = new TransactionAdd();
    }

    private void OnButtonClicked_to_TransactionEdit(object sender, EventArgs e)
    {
        if (App.Current != null)
            App.Current.MainPage = new TransactionEdit();
    }
}