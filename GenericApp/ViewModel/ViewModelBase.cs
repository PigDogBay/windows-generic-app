using MpdBaileyTechnology.Shared.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MpdBaileyTechnology.GenericApp.ViewModel
{
    public class ViewModelBase : Notifier, IDisposable
    {
        /// <summary>
        /// Returns the user-friendly name of this object.
        /// </summary>
        public string DisplayName { get; set; }

        public string Filename { get; set; }
        public string Filter { get; protected set; }
        public bool CanSaveAs { get; protected set; }
        public bool CanSave { get; protected set; }

        public ViewModelBase()
        {
            CanSave = false;
            CanSaveAs = false;
            Filename = string.Empty;
            Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
        }

        public virtual void Save()
        {
        }

        /// <summary>
        /// Override this method to place any clean up code for managed resources
        /// 
        /// eg: serialPort.Dispose();
        /// </summary>
        protected virtual void CleanUp()
        {
        }

        #region Dispose Code
        // Track whether Dispose has been called.
        private bool disposed = false;

        ~ViewModelBase()
        {
            Dispose(false);
        }
        // Dispose(bool disposing) executes in two distinct scenarios.
        // If disposing equals true, the method has been called directly
        // or indirectly by a user's code. Managed and unmanaged resources
        // can be disposed.
        // If disposing equals false, the method has been called by the
        // runtime from inside the finalizer and you should not reference
        // other objects. Only unmanaged resources can be disposed.
        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                this.disposed = true;
                if (disposing)
                {
                    //dispose all managed resources
                    CleanUp();
                }
                //Clean up unmanaged resources

                //Disposal has been done
                disposed = true;
            }
        }
        //Implements IDisposable
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
