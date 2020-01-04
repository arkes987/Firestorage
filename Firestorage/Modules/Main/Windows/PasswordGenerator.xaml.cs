using System.Windows;

namespace Firestorage.Modules.Main.Windows
{
    public partial class PasswordGenerator : Window
    {
        public PasswordGenerator(ref string input)
        {
            InitializeComponent();
            DataContext = new PasswordGeneratorViewModel(ref input);
        }
    }
}
