# Project: Financial Control App (.NET MAUI)

## 🧾 Description
This repository contains the source code developed during the Udemy course **“.NET MAUI – Building a Financial Control App”**.  
The goal of this app is to help users manage their personal finances by registering transactions, categorizing income and expenses, filtering by date, and viewing monthly summaries and charts.  

The project uses:  
- .NET MAUI for cross-platform UI (Android, iOS, Windows),  
- A local embedded database (e.g., LiteDB) for data storage,  
- MVVM pattern to separate UI, business logic, and data layers.  

## 🚀 Main Features
- Add new transactions (income or expense) with date, amount, category, and description.  
- View a list of all registered transactions.  
- Filter transactions by period (e.g., last month, specific month).  
- Dashboard showing total income, total expenses, and current balance.  
- Indexed date field for faster queries (using `EnsureIndex(x => x.Date)` in the repository).  
- Support for multiple categories with category management features.  
- Responsive and intuitive interface designed for multiple platforms.  

## 🛠 Technologies Used
- Language: C#  
- Framework: .NET MAUI  
- Architecture Pattern: MVVM (Model-View-ViewModel)  
- Local embedded database: LiteDB (or another depending on implementation)  
- Dependency Injection for data services  
- Styling and theming for cross-platform UI  

## 🧩 Project Structure
/src
/AppControleFinanceiro
/Models ← Entity definitions (e.g., Transaction, Category)
/ViewModels ← ViewModels connecting UI and business logic
/Views ← Pages and UI components
/Services ← Data access services (e.g., TransactionRepository)
/Resources ← Images, styles, and themes
App.xaml ← Application configuration
MainPage.xaml ← Main page

bash
Copiar código

## 🔧 Installation & Local Setup
1. Clone this repository:  
   ```bash
   git clone https://github.com/your-username/FinancialControlApp.git
Open the solution in Visual Studio (version with .NET MAUI support).

Restore NuGet packages.

Check the database connection configuration (e.g., LiteDatabase(@"Filename=…")).

Set the startup project according to your target platform (Android, iOS, Windows).

Build and run the project. You can then start creating, filtering, and analyzing transactions.

📝 Notes & Tips
If you encounter an error like “The name 'collectionName' does not exist in the current context”, make sure that variable is declared in the same class where it’s used (e.g., in TransactionRepository).

Verify your .NET SDK and MAUI versions to ensure compatibility with your development environment.

When using LiteDB, make sure the database file can be created and written to in the specified path.

The course examples often use a variable such as collectionName = "transactions"; ensure it’s correctly defined within scope.

Before publishing or distributing the app, remove test data and update icons, splash screens, and permissions.

📚 References
Udemy Course: .NET MAUI – Building a Financial Control App

Official .NET MAUI Documentation: https://learn.microsoft.com/dotnet/maui/

LiteDB Documentation: https://www.litedb.org/
