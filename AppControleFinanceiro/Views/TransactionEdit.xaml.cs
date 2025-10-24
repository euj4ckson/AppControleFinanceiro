using AppControleFinanceiro.Libraries.Utils.FixBugs;
using AppControleFinanceiro.Models;
using AppControleFinanceiro.Repositories;
using CommunityToolkit.Mvvm.Messaging;
using System.Text;

namespace AppControleFinanceiro.Views;

public partial class TransactionEdit : ContentPage
{
    private ITransactionRepository _repository;
    private Transaction _transaction;

    public TransactionEdit(ITransactionRepository repository)
	{
        _repository = repository;
		InitializeComponent();
	}
    public void setTransactionData(Models.Transaction transaction)
    {
        _transaction = transaction;
        EntryName.Text = transaction.Name;
        EntryValue.Text = transaction.Value.ToString();
        DatePickerDate.Date = transaction.Date.DateTime;
        if (transaction.Type == Models.TransactionType.Expense)
        {
            RadioExpense.IsChecked = true;
        }
        else
        {
            RadioIncome.IsChecked = true;
        }
    }

    private void TapGestureRecognizer_Tapped_ToClose(object sender, TappedEventArgs e)
    {
        KeyboardBugs.HideKeyboardOnAndroid();
        Navigation.PopModalAsync();

    }


    private void OnButtonClicked_Save(object sender, EventArgs e)
    {

        if (ValidateInputs() == false)
        {
            return;
        }
        SaveTransactionInDatabase();
        KeyboardBugs.HideKeyboardOnAndroid();
        Navigation.PopModalAsync();
        WeakReferenceMessenger.Default.Send<string>("RecarregarTransactionList");

    }

    private void SaveTransactionInDatabase()
    {
        Models.Transaction transaction = new Transaction()
        {
            Id = _transaction.Id,
            Name = EntryName.Text,
            Value =Math.Abs(double.Parse(EntryValue.Text)),
            Date = DatePickerDate.Date,
            Type = RadioExpense.IsChecked ? TransactionType.Expense : TransactionType.Income
        };
        _repository.Update(transaction);
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
        if (!String.IsNullOrEmpty(EntryValue.Text) && !double.TryParse(EntryValue.Text, out result))
        {
            sb.AppendLine("O campo 'Valor' deve é invalido!");
            isValid = false;
        }
        if (isValid == false)
        {
            LabelError.Text = sb.ToString();
            LabelError.IsVisible = true;
        }
        return isValid;
    }
}