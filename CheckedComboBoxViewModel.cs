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
        private readonly AllItemsCheckedComboBoxItem _allItems;

        private IEnumerable<CheckedComboBoxItem<T>> _items;

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
            _allItems = new AllItemsCheckedComboBoxItem();
            _allItems.PropertyChanged += AllItem_PropertyChanged;
            _items = Enumerable.Empty<CheckedComboBoxItem<T>>();
        }

        public CheckedComboBoxItem<T> AllItems
        {
            get { return _allItems; }
        }

        public bool HasItems
        {
            get { return _items.Any(); }
        }

        public IEnumerable<CheckedComboBoxItem<T>> SelectedItems
        {
            get { return _items.Where(i => i.IsSelected); }
        }

        public IEnumerable<CheckedComboBoxItem<T>> Items
        {
            get { return _items; }
            set
            {
                foreach (var i in _items) i.PropertyChanged -= OnItemPropertyChanged;
                if (value != null)
                {
                    _items = value;
                    foreach (var i in value) i.PropertyChanged += OnItemPropertyChanged;
                }
                else
                {
                    _items = Enumerable.Empty<CheckedComboBoxItem<T>>();
                }
                RaisePropertyChanged("Items");
                RaisePropertyChanged("HasItems");
            }
        }

        private bool _itemEnableSelectedChanged = true;
        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsSelected") return;
            if (!_itemEnableSelectedChanged) return;
            var selectedItem = (CheckedComboBoxItem<T>)sender;
            _allItemsEnableSelectedChanged = false;
            if (selectedItem.IsSelected)
            {
                _allItems.IsSelected = _items.All(i => i.IsSelected);
            }
            else
            {
                _allItems.IsSelected = false;
            }
            _allItemsEnableSelectedChanged = true;
            RaisePropertyChanged("SelectedItemsText");
        }

        private bool _allItemsEnableSelectedChanged = true;
        private void AllItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "IsSelected") return;
            if (!_allItemsEnableSelectedChanged) return;
            _itemEnableSelectedChanged = false;
            foreach (var item in _items) item.IsSelected = _allItems.IsSelected;
            _itemEnableSelectedChanged = true;
            RaisePropertyChanged("SelectedItemsText");
        }

        public string SelectedItemsText
        {
            get
            {
                return _allItems.IsSelected ? "<все>" : String.Join(";", Items.Where(i => i.IsSelected).Select(i => i.Name));
            }
        }

        public event EventHandler OnDropDownClosed;
        public void DropDownClosed()
        {
            if (OnDropDownClosed != null) OnDropDownClosed(this, EventArgs.Empty);
        }

        class AllItemsCheckedComboBoxItem : CheckedComboBoxItem<T>
        {
            public AllItemsCheckedComboBoxItem() : base(default(T))
            {
                Name = "<выделить все>";
            }
        }
    }

    public class CheckedComboBoxItem<T> : INotifyPropertyChanged
    {
        public T InnerObject { get; private set; }

        public CheckedComboBoxItem(T innerObject)
        {
            InnerObject = innerObject;
        }

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

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Name"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
