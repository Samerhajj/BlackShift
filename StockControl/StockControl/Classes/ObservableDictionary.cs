using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl
{
    public class ObservableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, INotifyCollectionChanged
    {
        //NotifyCollectionChangedEventHandler 
        //Is An EventHandeler that defines a delegate and an event
        //to notify all the components that have CollectionChanged implemented.

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public ObservableDictionary()
            : base() { }
        public new void Add(TKey key, TValue value)
        {
            base.Add(key, value);
            NotifyCollectionChanged();
        }//Adds with notification
        public new bool Remove(TKey key)
        {
            bool result = base.Remove(key);
            NotifyCollectionChanged();
            return result;
        }//Removes with notification
        public new int Count
        {
            get { return base.Count; }
        }

        //Extra Functions
        private void NotifyCollectionChanged()
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }//Invoks the CollectionChanged event.
    }
}

