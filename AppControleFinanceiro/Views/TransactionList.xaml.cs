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
        CollectionViewTransaction.ItemsSource = items;
        double income = items.Where(a => a.Type == Models.TransactionType.Income).Sum(a => a.Value);
        double Expense = items.Where(a => a.Type == Models.TransactionType.Expense).Sum(a => a.Value);
        double balance = income - Expense;
        LabelIncome.Text = income.ToString("C");
        LabelExpense.Text = Expense.ToString("C");
        LabelBalance.Text = balance.ToString("C");

    }

    private void OnButtonClicked_to_TransactionAdd(object sender, EventArgs args)
	{
        var TransactionAdd = Handler.MauiContext.Services.GetService<TransactionAdd>();
        Navigation.PushModalAsync(TransactionAdd);
    }
    private void OnButtonClicked_to_TransactionEdit(object sender, EventArgs e)
    {
        var TransactionEdit = Handler.MauiContext.Services.GetService<TransactionEdit>();
        Navigation.PushModalAsync(TransactionEdit);

    }
}