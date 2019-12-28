using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace Firestorage.Libs
{
    public class NotifyObject : INotifyPropertyChanged, IDisposable
    {
        #region Event

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Event

        #region Methods

        public void Dispose()
        {
            if (PropertyChanged != null)
            {
                var delgates = PropertyChanged.GetInvocationList().ToList();
                foreach (var del in delgates)
                    PropertyChanged -= (PropertyChangedEventHandler)del;
            }

            IsDisposed = true;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            try
            {
                System.Threading.Interlocked.CompareExchange(ref PropertyChanged, null, null)?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        protected virtual void OnPropertyChanged(Expression<Func<object>> extension)
        {
            try
            {
                System.Threading.Interlocked.CompareExchange(ref PropertyChanged, null, null)?.Invoke(this, new PropertyChangedEventArgs(extension.GetPropertyName()));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        #endregion Methods

        #region Properties

        public bool IsDisposed { get; set; }

        #endregion Properties
    }
}
