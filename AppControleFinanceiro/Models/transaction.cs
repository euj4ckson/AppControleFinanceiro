using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppControleFinanceiro.Models
{
    public class Transaction
    {
        [BsonId]
        public required int Id { get; set; }

        public TransactionType  Type { get; set; }
        public required String Name { get; set; }
        public DateTimeOffset Date { get; set; }
        public Decimal Value { get; set; }
    }
    public enum TransactionType
    {
        Icome,
        Expenses
    }

}
