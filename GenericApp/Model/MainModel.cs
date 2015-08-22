using MpdBaileyTechnology.Shared.Threading;
using MpdBaileyTechnology.Shared.Utils;
using MpdBaileyTechnology.Shared.WPF.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MpdBaileyTechnology.GenericApp.Model
{
    public class MainModel : Disposer
    {
        public const int UPDATE_PERIOD = 1000;
        public string Status { get; set; }
        public ObservableCollectionEx<Reading> Log { get; private set; }
        public bool IsLogging { get; set; }
        public Connection Connection { get; private set; }

        private DataStream<Reading> _DataStream;

        public MainModel()
        {
            this.Status = "Ready";
            this.Connection = new Connection();
            this.Log = new ObservableCollectionEx<Reading>();
            _DataStream = new DataStream<Reading>(() => Connection.ReadingSource.GetReading(), UPDATE_PERIOD);
            _DataStream.DataReceived += _DataStream_DataReceived;
        }

        void _DataStream_DataReceived(object sender, EventArg<Reading> e)
        {
            Log.Add(e.Data);
        }

        public void Start()
        {
            _DataStream.Start();
            IsLogging = true;
        }

        public void Stop()
        {
            _DataStream.Stop();
            IsLogging = false;
        }

        protected override void CleanUpManagedResources()
        {
            base.CleanUpManagedResources();
            _DataStream.DataReceived -= _DataStream_DataReceived;
            Stop();

        }
    }
}
