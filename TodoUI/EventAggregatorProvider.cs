﻿using System;
using System.Collections.Generic;
using System.Text;
using Caliburn.Micro;

namespace TodoUI
{
    /// <summary>
    /// Singleton for loosely-coupled application-wide event system.
    /// </summary>
    public class EventAggregatorProvider
    {
        public EventAggregator TrackerEventAggregator { get; set; }
        private static EventAggregatorProvider _instance;

        public static EventAggregatorProvider GetInstance()
        {
            if (_instance == null)
            {
                _instance = new EventAggregatorProvider();
            }

            return _instance;
        }

        private EventAggregatorProvider()
        {
            TrackerEventAggregator = new EventAggregator();
        }
    }
    
}
