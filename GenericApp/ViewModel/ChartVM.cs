using MpdBaileyTechnology.GenericApp.Model;
using MpdBaileyTechnology.Shared.WPF.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace MpdBaileyTechnology.GenericApp.ViewModel
{
    public class ChartVM : ViewModelBase
    {
        ObservableCollection<ChartSeries> _Series = new ObservableCollection<ChartSeries>();
        public ObservableCollection<ChartSeries> Series { get { return _Series; } }
        RollingSeries _RollingSeries;
        ChartSeries _ChartSeries;
        double count = 0;

        public ChartVM()
        {
            this.DisplayName = "Chart";
            _ChartSeries = new ChartSeries("Readings");
            LoadReadings();
            _RollingSeries = new RollingSeries(_ChartSeries, 1000, 60);
            _Series.Add(_RollingSeries.Rolled);
            MainVM.Instance.MainModel.Log.CollectionChanged += Log_CollectionChanged;

        }

        private void LoadReadings()
        {
            foreach (var item in MainVM.Instance.MainModel.Log)
            {
                _ChartSeries.AddPoint(count++, item.Temperature);
            }
        }
        void Log_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    _ChartSeries.AddPoint(count++, ((Reading)item).Temperature);
                    }
            }
            else if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
            {
                _ChartSeries.Points.Clear();
            }
        }
        protected override void CleanUp()
        {
            base.CleanUp();
            MainVM.Instance.MainModel.Log.CollectionChanged -= Log_CollectionChanged;
        }
    }
}
