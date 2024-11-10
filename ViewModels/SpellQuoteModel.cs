using Data;
using Microsoft.Practices.Prism.Events;
using RoundBasedGameMVVM.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Annotations;
using System.Windows.Media.TextFormatting;

namespace RoundBasedGameMVVM.ViewModels
{
    public class SpellQuoteModel:ViewModelBase
    {
       
        public Duellists Duellists { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="SpellQuoteModel"/> class.
        /// </summary>
        public SpellQuoteModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            this.EventAggregator.GetEvent<GetDuellistsEvent>().Subscribe(this.GetDuellists, ThreadOption.UIThread);
            
        }

        public void GetDuellists(Duellists duellists)
        {
           
                this.Duellists = duellists;
           
        }
     
    }
    
}
