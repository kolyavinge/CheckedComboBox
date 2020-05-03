using System.Collections.Generic;
using System.Windows;

namespace CheckedComboBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var vm = new CheckedComboBoxViewModel<object>
            {
                Items = new List<CheckedComboBoxItem<object>>
                {
                    new CheckedComboBoxItem<object>(null) { Name="1", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="2", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = true },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="3", IsSelected = false },
                    new CheckedComboBoxItem<object>(null) { Name="111111111112345678901111111111123456789011111111111234567890111111111112345678901111111111123456789011111111111234567890111111111112345678901111111111123456789011111111111234567890111111111112345678901111111111123456789011111111111234567890111111111112345678901111111111123456789011111111111234567890", IsSelected = false },
                }
            };

            //vm.OnDropDownClosed += (s, e) => MessageBox.Show("1");

            this.checkedComboBoxView.DataContext = vm;
        }
    }
}
