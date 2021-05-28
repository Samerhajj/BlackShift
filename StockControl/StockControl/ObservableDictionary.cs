using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl
{
    public class ObservableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, System.Collections.Specialized.INotifyCollectionChanged, System.ComponentModel.INotifyPropertyChanged
    {
        // PropertyChangedEventHandler And NotifyCollectionChangedEventHandler 
        //Are two event handlers,
        //Each one of them is an EventHandeler that defines a delegate and an event
        //to notify all the components that have PropertyChanged or CollectionChanged included.

        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        public ObservableDictionary()
            :base() { }
        public new void Add(TKey key, TValue value)
        {
            base.Add(key, value);
            NotifyCollectionChanged();
        }
        public new bool Remove(TKey key)
        {
            bool result = base.Remove(key);
            NotifyCollectionChanged();
            return result;
        }
        public new int Count()
        {
            return base.Count;
        }
        public void ChangePlaces(TKey id1, TKey id2)
        {
            TValue temp = base[id1];
            base[id1] = base[id2];
            base[id2] = temp;
            NotifyCollectionChanged();
        }

        //Extra Functions
        private void NotifyCollectionChanged()
        {
            //Invoks the CollectionChanged event.
            CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
        private void NotifyPropertyChanged()
        {
            //Invoks the PropertyChanged event.
            PropertyChanged(this, new PropertyChangedEventArgs(""));
        }
    }
}

