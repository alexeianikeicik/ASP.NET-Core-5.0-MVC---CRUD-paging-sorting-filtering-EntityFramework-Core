using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesByType.Models
{
    public enum SortState
    {
        ExpenseNameAsc,    // по имени по возрастанию
        ExpenseNameDesc,   // по имени по убыванию
        AmountAsc, // по возрасту по возрастанию
        AmountDesc,    // по возрасту по убыванию
        ExpenseTypeNameAsc, // по компании по возрастанию
        ExpenseTypeNameDesc // по компании по убыванию
    }
}
