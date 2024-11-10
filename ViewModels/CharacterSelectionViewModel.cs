using Common.Command;
using Data;
using Microsoft.Practices.Prism.Events;
using RoundBasedGameMVVM.Events;
using RoundBasedGameMVVM.Views;
using System;
using System.Windows.Input;

namespace RoundBasedGameMVVM.ViewModels
{
    public class CharacterSelectionViewModel : ViewModelBase
    {

        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------

        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Init the Constructer <see cref="CharacterSelectionView"/> class
        /// </summary>
        /// <param name="eventAggregator"></param>
        public CharacterSelectionViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            //hookup command
            this.CharacterSelectionCommand = new ActionCommand(this.Character1SelectionCommandExecute, this.Character1SelectionCommandCanExecute);
        }

      
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        /// <summary>
        /// get and sets the ICommand
        /// </summary>
        public ICommand CharacterSelectionCommand { get; set; }
        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------

        #endregion

        #region ------------------------- Commands ------------------------------------------------------------------------

        private bool Character1SelectionCommandCanExecute(object arg)
        {
            return true;
        }

        /// <summary>
        /// Creates a new Savestates with certain parameters
        /// </summary>
        /// <param name="obj"></param>
        private void Character1SelectionCommandExecute(object obj)
        {
            SaveStatesView saveStatesView = new SaveStatesView();
            SaveStateViewModel saveStateViewModel = new SaveStateViewModel(this.EventAggregator);
            saveStatesView.DataContext = saveStateViewModel;
            AccountInfo accountInfo = new AccountInfo(0, 1,0,true);
            this.EventAggregator.GetEvent<GetAccountInformationsEvent>().Publish(accountInfo);
            this.EventAggregator.GetEvent<SwapToSaveStateView>().Publish(saveStatesView);

        }
        #endregion
    }
}
