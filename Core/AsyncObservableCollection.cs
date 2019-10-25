using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace Core
{
    public class AsyncObservableCollection<T> : ObservableCollection<T>
    {
        SynchronizationContext context = SynchronizationContext.Current;
        public AsyncObservableCollection(IEnumerable<T> list)
        : base(list)
        {
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (SynchronizationContext.Current == context)
            {
                RaiseCollectionChanged(e);
            }
            else
            {
                context.Send(RaiseCollectionChanged, e);
            }
        }

        private void RaiseCollectionChanged(object e)
        {
            base.OnCollectionChanged((NotifyCollectionChangedEventArgs)e);
        }

        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
           
            if (SynchronizationContext.Current == context)
            {
                RaiseCollectionChanged(e);
            }
            else
            {
                context.Send(RaisePropertyChanged, e);
            }
        }

        private void RaisePropertyChanged(object e)
        {
            base.OnPropertyChanged((PropertyChangedEventArgs)e);
        }
    }
}
