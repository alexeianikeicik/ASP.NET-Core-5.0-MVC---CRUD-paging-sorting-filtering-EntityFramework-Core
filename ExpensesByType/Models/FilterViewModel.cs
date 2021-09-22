using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ExpensesByType.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<ExpenseType> expense_type_names, int? ExpenseTypeId, string name)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            expense_type_names.Insert(0, new ExpenseType { Name = "All", Id = 0 });
            ExpenseTypes = new SelectList(expense_type_names, "Id", "Name", ExpenseTypeId);
            SelectedExpenseTypeId = ExpenseTypeId;
            SelectedName = name;
        }
        public SelectList ExpenseTypes { get; private set; } // список компаний
        public int? SelectedExpenseTypeId { get; private set; }   // выбранная компания
        public string SelectedName { get; private set; }    // введенное имя
    }
}
