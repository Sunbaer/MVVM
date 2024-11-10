﻿using Common.NotifyPropertyChanged;
using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundBasedGameMVVM.ViewModels
{
    /// <summary>
    /// Base class for all view models
    /// </summary>
    public class ViewModelBase : NotifyPropertyChanged
    {
        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelBase"/> class.
        /// </summary>
        public ViewModelBase(IEventAggregator eventAggregator)
        {

            EventAggregator = eventAggregator;
        }
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        protected IEventAggregator EventAggregator { get; set; }
        #endregion
    }
}
