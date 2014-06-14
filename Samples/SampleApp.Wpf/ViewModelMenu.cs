using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using StyleMVVM.ViewModel;

namespace Samples.Wpf
{
    public class ViewModelMenu : BaseViewModel
    {
        public ViewModelMenu(string text)
        {
            this.Text = text;
            this.Children = new ObservableCollection<ViewModelMenu>();
        }
        public ViewModelMenu(string text, IEnumerable<ViewModelMenu> childrenCollection)
        {
            this.Text = text;
            this.Children = new ObservableCollection<ViewModelMenu>(childrenCollection);
        }

        public ICommand Command { get; set; }
        public string Text { get; set; }
        public Uri IconUri { get; set; }

        public ObservableCollection<ViewModelMenu> Children { get; set; }
    }    
}