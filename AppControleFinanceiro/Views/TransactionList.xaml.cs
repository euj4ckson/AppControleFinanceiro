using AppControleFinanceiro.Models;
using AppControleFinanceiro.Repositories;
using CommunityToolkit.Mvvm.Messaging;

namespace AppControleFinanceiro.Views;

public partial class TransactionList : ContentPage
{

    private ITransactionRepository _repository;
    public TransactionList(ITransactionRepository repository)
	{

        this._repository = repository;
        InitializeComponent();
        Reload();
        WeakReferenceMessenger.Default.Register<string>(this, (r, msg) =>
        {
            if (msg == "RecarregarTransactionList")
            {
                Reload();
            }
        });

    }

    private void Reload()
    {
        var items = _repository.GetAll();
        CollectionViewTransactions.ItemsSource = items;
        double income = items.Where(a => a.Type == Models.TransactionType.Income).Sum(a => a.Value);
        double Expense = items.Where(a => a.Type == Models.TransactionType.Expense).Sum(a => a.Value);
        double balance = income - Expense;
        LabelIncome.Text = income.ToString("C");
        LabelExpense.Text = Expense.ToString("C");
        LabelBalance.Text = balance.ToString("C");


    }

    private async void OnButtonClicked_to_TransactionAdd(object sender, EventArgs args)
    {
        try
        {
            var TransactionAdd = Application.Current.Handler.MauiContext.Services.GetService<TransactionAdd>();

            await Navigation.PushModalAsync(TransactionAdd);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.Message, "OK");
        }
    }

    private void TapGestureRecognizer_Tapped_To_TransactionEdit(object sender, TappedEventArgs e)
    {
        try 
        {
            var grid = (Grid)sender;
            var gestore = (TapGestureRecognizer)grid.GestureRecognizers[0];
            Transaction transaction = (Transaction)gestore.CommandParameter;
            var TransactionEdit = Handler.MauiContext.Services.GetService<TransactionEdit>();
            TransactionEdit.setTransactionData(transaction);
            Navigation.PushModalAsync(TransactionEdit);
        }
        catch (Exception ex)
        {
            DisplayAlert("Erro", ex.Message, "OK");
        }

        
    }


    private async void TapGestureRecognizerTapped_ToDelete(object sender, TappedEventArgs e)
    {
        await AnimationDeleteBorder((Border)sender, true);
       bool result =await App.Current.MainPage.DisplayAlert("Atenção", "Deseja Realizar a exclusão do registro?", "Sim","Não");
        if (result)
        {
            Transaction transaction = (Transaction)e.Parameter;
            _repository.Delete(transaction);
            Reload();
        }
        else
        {
            await AnimationDeleteBorder((Border)sender, false);

        }

    }
    private Color _borderOriginalBackgroudColor;
    private String _labelOriginalText;
    private async Task AnimationDeleteBorder(Border border,bool isDeleteAnimation)
    {
        var label = (Label)border.Content;


        if (isDeleteAnimation)
        {
            _borderOriginalBackgroudColor = border.BackgroundColor;
            _labelOriginalText = label.Text;
           await border.RotateYTo(90, 500);
            border.BackgroundColor = Colors.Red;
            label.TextColor = Colors.White;
            label.Text = "X";
            await border.RotateYTo(180, 500);
        }
        else
        {
            await border.RotateYTo(90, 500);
            label.TextColor = Colors.Black;
            label.Text = _labelOriginalText;
            border.BackgroundColor = _borderOriginalBackgroudColor;
            await border.RotateYTo(0, 500);



        }
    }
}