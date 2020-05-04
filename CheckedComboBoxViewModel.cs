using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CheckedComboBox
{
    public interface ICheckedComboBoxViewModel
    {
        void DropDownClosed();
    }

    public class CheckedComboBoxViewModel<T> : ICheckedComboBoxViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public CheckedComboBoxViewModel()
        {
            _comboboxItems = new List<CheckedComboBoxItem>();
        }

        private Func<T, string> _displayMemberFunc;
        public void SetDisplayMemberFunc(Func<T, string> displayMemberFunc)
        {
            _displayMemberFunc = displayMemberFunc;
            foreach (var i in _comboboxItems) i.DisplayMemberFunc = _displayMemberFunc;
        }

        private bool _isAllItemsSelected;
        public bool IsAllItemsSelected
        {
            get { return _isAllItemsSelected; }
            set
            {
                _isAllItemsSelected = value;
                RaisePropertyChanged("IsAllItemsSelected");
                IsAllItemsSelectedChange();
            }
        }

        public bool HasItems
        {
            get { return _comboboxItems.Any(); }
        }

        public IEnumerable<T> SelectedItems
        {
            get { return _comboboxItems.Where(i => i.IsSelected).Select(x => x.InnerObject); }
            set
            {
                var newSelectedItemsSet = new HashSet<T>(value ?? Enumerable.Empty<T>());
                foreach (var comboboxItem in _comboboxItems)
                {
                    comboboxItem.IsSelected = newSelectedItemsSet.Contains(comboboxItem.InnerObject);
                }
            }
        }

        public IEnumerable<T> ItemsSource
        {
            get { return _comboboxItems.Select(x => x.InnerObject); }
            set
            {
                foreach (var i in _comboboxItems) i.PropertyChanged -= OnItemPropertyChanged;
                _comboboxItems = (value ?? Enumerable.Empty<T>()).Select(i => new CheckedComboBoxItem(i, _displayMemberFunc)).ToList();
                foreach (var i in _comboboxItems) i.PropertyChanged += OnItemPropertyChanged;
                RaisePropertyChanged("ItemsSource");
                RaisePropertyChanged("HasItems");
            }
        }

        private List<CheckedComboBoxItem> _comboboxItems;
        public IEnumerable<CheckedComboBoxItem> ComboboxItems
        {
            get { return _comboboxItems; }
        }

        private bool _itemEnableSelectedChanged = true;
        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsSelected") return;
            if (!_itemEnableSelectedChanged) return;
            var selectedItem = (CheckedComboBoxItem)sender;
            _allItemsEnableSelectedChanged = false;
            if (selectedItem.IsSelected)
            {
                IsAllItemsSelected = _comboboxItems.All(i => i.IsSelected);
            }
            else
            {
                IsAllItemsSelected = false;
            }
            _allItemsEnableSelectedChanged = true;
            RaisePropertyChanged("SelectedItemsText");
        }

        private bool _allItemsEnableSelectedChanged = true;
        private void IsAllItemsSelectedChange()
        {
            if (!_allItemsEnableSelectedChanged) return;
            _itemEnableSelectedChanged = false;
            foreach (var item in _comboboxItems) item.IsSelected = _isAllItemsSelected;
            _itemEnableSelectedChanged = true;
            RaisePropertyChanged("SelectedItemsText");
        }

        public string SelectedItemsText
        {
            get
            {
                return _isAllItemsSelected ? "<все>" : String.Join(";", _comboboxItems.Where(i => i.IsSelected).Select(i => i.Name));
            }
        }

        public event EventHandler OnDropDownClosed;
        public void DropDownClosed()
        {
            if (OnDropDownClosed != null) OnDropDownClosed(this, EventArgs.Empty);
        }

        public class CheckedComboBoxItem : INotifyPropertyChanged
        {
            public T InnerObject { get; private set; }

            public CheckedComboBoxItem(T innerObject, Func<T, string> displayMemberFunc)
            {
                InnerObject = innerObject;
                DisplayMemberFunc = displayMemberFunc;
                _isSelected = false;
            }

            public Func<T, string> DisplayMemberFunc { get; set; }

            private bool _isSelected;

            public bool IsSelected
            {
                get { return _isSelected; }
                set
                {
                    _isSelected = value;
                    if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
                }
            }

            public string Name
            {
                get { return DisplayMemberFunc != null ? DisplayMemberFunc(InnerObject) : InnerObject.ToString(); }
            }

            public event PropertyChangedEventHandler PropertyChanged;
        }
    }
}
