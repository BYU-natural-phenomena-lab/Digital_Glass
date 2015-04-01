using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Walle.Annotations;

namespace Walle.ViewModel
{
    /// <summary>
    /// Provides a common set of event handlers for all View Models to use. It also helps validate property names during debugging.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged, IDisposable
    {
        public virtual string DisplayName { get; protected set; }
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Implements property notification. This is a safe way to call the event.
        /// This is part of the magic that enables two-way binding to XAML apps.
        /// </summary>
        /// <param name="propertyName"></param>
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

        /// <summary>
        /// Verfies that the property name exists. This is useful for debugging XAML apps.
        /// </summary>
        /// <param name="propertyName"></param>
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {
            // Verify that the property name matches a real,  
            // public, instance property on this object.
            if (TypeDescriptor.GetProperties(this)[propertyName] == null)
            {
                string msg = "Invalid property name: " + propertyName;

                if (this.ThrowOnInvalidPropertyName)
                    throw new Exception(msg);
                else
                    Debug.Fail(msg);
            }
        }

        public void Dispose()
        {
            this.OnDispose();
        }

        protected virtual void OnDispose()
        {
        }

#if DEBUG
        /// <summary>
        /// Useful for ensuring that ViewModel objects are properly garbage collected.
        /// </summary>
        ~ViewModelBase()
        {
            string msg = string.Format("{0} ({1}) ({2}) Finalized", this.GetType().Name, this.DisplayName,
                this.GetHashCode());
            Debug.WriteLine(msg);
        }
#endif
    }
}