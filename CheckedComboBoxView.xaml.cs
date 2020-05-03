using System.Windows.Controls;

namespace CheckedComboBox
{
    public partial class CheckedComboBoxView : UserControl
    {
        public CheckedComboBoxView()
        {
            InitializeComponent();
        }

        private void ComboBox_DropDownClosed(object sender, System.EventArgs e)
        {
            var vm = DataContext as ICheckedComboBoxViewModel;
            if (vm != null)
            {
                vm.DropDownClosed();
            }
        }
    }
}
