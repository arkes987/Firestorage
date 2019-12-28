using Firestorage.Modules.Entry.Components;
using System.Windows;

namespace Firestorage
{
    public partial class EntryWindow : Window
    {
        public EntryWindow()
        {
            InitializeComponent();
            DataContext = new EntryViewModel();
        }
    }
}
