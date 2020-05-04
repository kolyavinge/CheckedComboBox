using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CheckedComboBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public class SimpleItem
        {
            public string Name { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            var vm = new CheckedComboBoxViewModel<SimpleItem>
            {
                ItemsSource = new List<SimpleItem>
                {
                    new SimpleItem { Name = "1" },
                    new SimpleItem { Name = "2" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "3" },
                    new SimpleItem { Name = "111111111112345678901111111111123456789011111111111234567890111111111112345678901111111111123456789011111111111234567890111111111112345678901111111111123456789011111111111234567890111111111112345678901111111111123456789011111111111234567890111111111112345678901111111111123456789011111111111234567890" }
                }
            };

            //vm.SetDisplayMemberFunc(i => i.Name);

            vm.SelectedItemsChangeCompleted += (s, e) => MessageBox.Show("SelectedItemsChangeCompleted");

            this.checkedComboBoxView.DataContext = vm;
        }
    }
}
