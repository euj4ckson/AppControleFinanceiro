using AppControleFinanceiro.Views;

namespace AppControleFinanceiro
{
    public partial class App : Application
    {
        public App(TransactionList ListPage)
        {
            InitializeComponent();
            MainPage = new NavigationPage (ListPage);
        }

    }
}