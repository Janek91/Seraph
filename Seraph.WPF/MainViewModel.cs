using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Seraph.WPF
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Handler> Handlers { get; }
        ListSortDirection m_direction = ListSortDirection.Ascending;

        public MainViewModel()
        {
            Handlers = new ObservableCollection<Handler>();
        }

        public void Close()
        {
            // Close selected handle
            foreach (Handler handler in Handlers)
            {
                if (handler.Selected)
                {
                    Command.Close(handler);
                }
            }
        }

        public void Handle(string filter)
        {
            Handlers.Clear();
            List<Handler> handlers = Command.Handle(filter).ToList();
            foreach (Handler h in handlers)
            {
                Handlers.Add(h);
            }
        }
    }
}
