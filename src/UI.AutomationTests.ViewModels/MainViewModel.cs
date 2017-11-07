using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace UI.AutomationTests.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private string textA;
        private string textB;
        private string textC;
        private string status;

        public MainViewModel()
        {
            CalculateCommand = new RelayCommand((param) => CalculateExecute(param));
            ChangeStatusCommand = new RelayCommand((param) => ChangeStatusExecute(param));

            TestCollection = new ReadOnlyObservableCollection<string>(new ObservableCollection<string> { "item1", "item2", "item3" });
        }

        public ReadOnlyObservableCollection<string> TestCollection { get; }

        public string TextA
        {
            get
            {
                return this.textA;
            }
            set
            {
                if (value != this.textA)
                {
                    this.textA = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string TextB
        {
            get
            {
                return this.textB;
            }
            set
            {
                if (value != this.textB)
                {
                    this.textB = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string TextC
        {
            get
            {
                return this.textC;
            }
            set
            {
                if (value != this.textC)
                {
                    this.textC = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string Status
        {
            get
            {
                return this.status;
            }
            set
            {
                if (value != this.status)
                {
                    this.status = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public ICommand CalculateCommand { get; private set; }

        public ICommand ChangeStatusCommand { get; private set; }

        private void CalculateExecute(object param = null)
        {
            var a = string.IsNullOrEmpty(TextA) ? 0 : int.Parse(TextA);
            var b = string.IsNullOrEmpty(TextB) ? 0 : int.Parse(TextB);
            TextC = (a + b).ToString();
        }

        private void ChangeStatusExecute(object param = null)
        {
            this.Status = param?.ToString();
        }
    }
}
