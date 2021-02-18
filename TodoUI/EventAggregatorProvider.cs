using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;

namespace TodoUI
{
    public class EventAggregatorProvider
    {
        public EventAggregator TrackerEventAggregator { get; set; } = new EventAggregator();
    }
    
}
