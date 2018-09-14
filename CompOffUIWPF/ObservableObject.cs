using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompOffUIWPF
{
    public class ObservableObject<T> : INotifyPropertyChanged
    {
        private T myValue;
        private Action myAction;
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableObject()
        {
        }
        public ObservableObject(T value, Action actionToDo = null)
        {
            this.myValue = value;
            this.myAction = actionToDo;
        }

        public T Value
        {
            get
            {
                return this.myValue;
            }
            set
            {
                this.myValue = value;
                this.OnPropertyChanged(nameof(this.Value));
                if (this.myAction != null)
                {
                    myAction();
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                this.PropertyChanged(this, e);
            }
        }
    }
}
