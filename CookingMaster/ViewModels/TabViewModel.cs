namespace CookingMaster.ViewModels
{
   public class TabViewModel : BaseViewModel
    {

        private bool _IsSelected;
        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                _IsSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        private string _TabUid;
        public string TabUid
        {
            get { return _TabUid; }
            set
            {
                _TabUid = value;
                OnPropertyChanged(nameof(TabUid));
            }
        }

        private string _TabTitle;
        public string TabTitle
        {
            get { return _TabTitle; }
            set
            {
                _TabTitle = value;
                OnPropertyChanged(nameof(TabTitle));
            }
        }

        public string IconPath { get; set; }
    }
}
