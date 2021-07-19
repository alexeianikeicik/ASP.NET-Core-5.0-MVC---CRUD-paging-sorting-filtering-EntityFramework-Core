using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpensesByType.Models
{
    public class SortViewModel
    {
        public SortState ExpenseNameSort { get; private set; } // значение для сортировки по имени
        public SortState AmountSort { get; private set; }    // значение для сортировки по возрасту
        public SortState ExpenseTypeNameSort { get; private set; }   // значение для сортировки по компании
        public SortState Current { get; private set; }     // текущее значение сортировки

        public SortViewModel(SortState sortOrder)
        {
            ExpenseNameSort = sortOrder == SortState.ExpenseNameAsc ? SortState.ExpenseNameDesc : SortState.ExpenseNameAsc;
            AmountSort = sortOrder == SortState.AmountAsc ? SortState.AmountDesc : SortState.AmountAsc;
            ExpenseTypeNameSort = sortOrder == SortState.ExpenseTypeNameAsc ? SortState.ExpenseTypeNameDesc : SortState.ExpenseTypeNameAsc;
            Current = sortOrder;
        }
    }
}
