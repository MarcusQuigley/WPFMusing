using Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace WPFMusing.ViewModels
{
    class MainWindowViewModel : BaseViewModel
    {
        private AsyncObservableCollection<string> _strings;// = new AsyncObservableCollection<string>();
        string _stringToAdd = string.Empty;
        RelayCommand _addStringCommand;

        public MainWindowViewModel()
        {
            LoadStrings();
            Debug.WriteLine("Ctr ThreadId: {0}", Thread.CurrentThread.ManagedThreadId);
        }

        void LoadStrings()
        {
            var strings = "If we add items to this collection out of the main thread"
                          .Split(' ');
             _strings = new AsyncObservableCollection<string>(strings);
        }

        public AsyncObservableCollection<string> Strings
        {
            get => _strings;
            set
            {
                _strings = value;
                OnPropertyChanged("Strings");
            }
        }
        public string StringToAdd
        {
            get => _stringToAdd;
            set
            {
                _stringToAdd = value;
                OnPropertyChanged("StringToAdd");
                AddStringCommand.RaiseExecuteChanged();
             }
        }

        public RelayCommand AddStringCommand
        {
            get
            {
                return _addStringCommand ?? (_addStringCommand = new RelayCommand(AddString, CanAdd));
            }
        }

        private void AddString(object arg)
        {
            Debug.WriteLine("ThreadId: {0}", Thread.CurrentThread.ManagedThreadId);
            Strings.Add(StringToAdd);
        }

        private bool CanAdd(object obj)
        {
            return StringToAdd != string.Empty;
        }
    }
}
