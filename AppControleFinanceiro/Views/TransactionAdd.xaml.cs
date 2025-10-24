using AppControleFinanceiro.Libraries.Utils.FixBugs;
using AppControleFinanceiro.Models;
using AppControleFinanceiro.Repositories;
using CommunityToolkit.Mvvm.Messaging;
using System.Text;

namespace AppControleFinanceiro.Views;

public partial class TransactionAdd : ContentPage
{
    private ITransactionRepository _repository;
    public TransactionAdd(ITransactionRepository repository)
	{
        _repository = repository;
        InitializeComponent();
		
	}

    private void TapGestureRecognizer_Tapped_ToClose(object sender, TappedEventArgs e)
    {
		Navigation.PopModalAsync();
    }

    private async void OnButtonClicked_Save(object sender, EventArgs e)
    {
        try
        {
            if (!ValidateInputs())
                return;

            SaveTransactionInDatabase();
            KeyboardBugs.HideKeyboardOnAndroid();
            WeakReferenceMessenger.Default.Send<string>("RecarregarTransactionList");
            await Navigation.PopModalAsync();


        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", ex.ToString(), "OK");
        }
    }


    private void SaveTransactionInDatabase()
    {
        Models.Transaction transaction = new Transaction()
        {
            Name = EntryName.Text,
            Value = Math.Abs(double.Parse(EntryValue.Text)),
            Date = DatePickerDate.Date,
            Type = RadioExpense.IsChecked ? TransactionType.Expense : TransactionType.Income
        };
        _repository.Add(transaction);
    }

    private bool ValidateInputs()
    {
        bool isValid = true;
        double result;
        StringBuilder sb = new StringBuilder();
        if (String.IsNullOrEmpty(EntryName.Text) || string.IsNullOrWhiteSpace(EntryName.Text))
        {
            sb.AppendLine("O campo 'Nome' deve ser preenchido!");
            isValid = false;
        }
        if ((String.IsNullOrEmpty(EntryValue.Text) || string.IsNullOrWhiteSpace(EntryValue.Text)))
        {
            sb.AppendLine("O campo 'Valor' deve ser preenchido!");
            isValid = false;
        }
        if (!String.IsNullOrEmpty(EntryValue.Text) && !double.TryParse(EntryValue.Text,out result))
        {
            sb.AppendLine("O campo 'Valor'  é invalido!");
            isValid = false;
        }
        if (isValid==false)
        {
            LabelError.Text = sb.ToString();
            LabelError.IsVisible = true;
        }
        return isValid;
    }
}