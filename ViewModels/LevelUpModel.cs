using Common.Command;
using Data;
using Microsoft.Practices.Prism.Events;
using RoundBasedGameMVVM.Events;
using RoundBasedGameMVVM.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RoundBasedGameMVVM.ViewModels
{
    public class LevelUpModel:ViewModelBase
    {

        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        private Duellists duellists;
        private BattleRounds rounds;
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="LevelUpModel"/> class.
        /// </summary>
        public LevelUpModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            //hookup Command
            this.HpUpCommand = new ActionCommand(this.HpUpExecute, this.HpUpCanExecute);
            //hookup Command
            this.DamageUP = new ActionCommand(this.DamageUpExcecute, this.DamgeUpCanExecute);
            //Subscribe to event
            this.EventAggregator.GetEvent<GetDuellistsEvent>().Subscribe(this.GetDuellist, ThreadOption.UIThread);
            //Subscribe to event
            this.EventAggregator.GetEvent<GetRoundInformations>().Subscribe(this.GetRoundInformation, ThreadOption.UIThread);
        }
        /// <summary>
        /// gets the current rounds information
        /// </summary>
        /// <param name="obj"></param>
        private void GetRoundInformation(BattleRounds obj)
        {
            this.rounds = obj;
        }



        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        public ICommand HpUpCommand{ get; private set; }
        public ICommand DamageUP { get; private set; }
        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------
        /// <summary>
        /// gets the duellists
        /// </summary>
        /// <param name="duellists"></param>
        private void GetDuellist(Duellists duellists)
        {
            this.duellists = duellists;
        }


        #endregion

        #region ------------------------- Commands ------------------------------------------------------------------------
        /// <summary>
        /// Increases the attack Damage of the playable Character.
        /// </summary>
        /// <param name="obj"></param>
        private void DamageUpExcecute(object obj)
        {
            this.duellists.Player.AttackDamage += 5;
            //creates a new instance of the Battle view
            BattleView battleView = new BattleView();
            BattleViewModel battleViewModel = new BattleViewModel(this.EventAggregator);
            battleView.DataContext = battleViewModel;
            //public all event which is needed for a new Round
            this.EventAggregator.GetEvent<SwapToGameEvent>().Publish(battleView);
            this.EventAggregator.GetEvent<GetRoundInformations>().Publish(this.rounds);
            this.EventAggregator.GetEvent<GetDuellistsEvent>().Publish(this.duellists);
        }

        private bool DamgeUpCanExecute(object arg)
        {
            return true;
        }

        private bool HpUpCanExecute(object arg)
        {
            return true;
        }
        /// <summary>
        /// Increase the Healthpoints of the playable character
        /// </summary>
        /// <param name="obj"></param>
        private void HpUpExecute(object obj)
        {
            this.duellists.Player.HealthPoints += 10;
            BattleView battleView = new BattleView();
            BattleViewModel battleViewModel = new BattleViewModel(this.EventAggregator);
            battleView.DataContext = battleViewModel;
           
            this.EventAggregator.GetEvent<SwapToGameEvent>().Publish(battleView);
            this.EventAggregator.GetEvent<GetRoundInformations>().Publish(this.rounds);
            this.EventAggregator.GetEvent<GetDuellistsEvent>().Publish(this.duellists);
        }
        #endregion
    }
}
