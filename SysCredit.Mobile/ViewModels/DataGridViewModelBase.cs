namespace SysCredit.Mobile.ViewModels;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Xml.Linq;

[ObservableRecipient]
public partial class DataGridViewModelBase<T> : ObservableValidator
{
    public DataGridViewModelBase(ObservableCollection<T> Items)
    {
        m_Items = Items;
        m_SelectedItems = new ObservableCollection<T>();
        Messenger = WeakReferenceMessenger.Default;
    }

    [ObservableProperty]
    [NotifyPropertyChangedRecipients]
    private bool m_IsBusy;

    [ObservableProperty]
    [NotifyPropertyChangedRecipients]
    private ObservableCollection<T> m_SelectedItems;

    [ObservableProperty]
    [NotifyPropertyChangedRecipients]
    private ObservableCollection<T> m_Items;

    public int Row { get; set; }

    [RelayCommand]
    private void AddNew(T Item)
    {
        Items.Insert(Row, Item);
    }

    [RelayCommand]
    private void RemoveItem(T Item)
    {
        Items.Remove(Item);
    }

    [RelayCommand]
    private void RemoveSelected()
    {
        for (int Index = 0; Index < SelectedItems.Count; ++Index)
        {
            Items.Remove(SelectedItems[Index]);
        }
    }
}
