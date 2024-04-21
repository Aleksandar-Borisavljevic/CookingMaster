using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookingMaster.ViewModels
{
    [QueryProperty("Description", "IngredientDescription")]
    public partial class IngredientDescriptionViewModel : BaseViewModel
    {
        public IngredientDescriptionViewModel()
        {

        }

        [ObservableProperty]
        IngredientDescription description;
    }
}
