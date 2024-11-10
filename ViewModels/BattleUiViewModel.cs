using Data;
using Microsoft.Practices.Prism.Events;
using RoundBasedGameMVVM.Events;
using System;

namespace RoundBasedGameMVVM.ViewModels
{
    class BattleUiViewModel : ViewModelBase
    {
        /// <summary>the rounds in the curren playthroug </summary>
        public BattleRounds Rounds { get; set; }
        /// <summary>The stats of the playable character</summary>
        public Charakter Player { get; set; }
        /// <summary>The stats of the nonplayable character</summary>
        public Charakter Enemy { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BattleUiViewModel"/> class.
        /// </summary>
        public BattleUiViewModel(IEventAggregator eventAggregator):base(eventAggregator)

        {
            // subscribe to event
            this.EventAggregator.GetEvent<GetDuellistsEvent>().Subscribe(this.GetDuellists, ThreadOption.UIThread);
            // subscribe to event
            this.EventAggregator.GetEvent<GetRoundInformations>().Subscribe(this.GetRounds, ThreadOption.UIThread);
        
        }
        /// <summary>
        /// Get the Round informations
        /// </summary>
        /// <param name="obj"> is an object of the type BattleRound</param>
        private void GetRounds(BattleRounds obj)
        {
            /// <summary>the rounds in the curren playthroug </summary>
            Rounds = obj;
        }
        /// <summary>
        /// Get the duallist information
        /// </summary>
        /// <param name="duellists">is an object of the type Duellist</param>
        public void GetDuellists(Duellists duellists)
        {
            this.Player = duellists.Player;
            this.Enemy = duellists.Enemy;
            
        }
    }
}
