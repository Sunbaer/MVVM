using Common.Command;
using Data;
using Microsoft.Practices.Prism.Events;
using RoundBasedGameMVVM.Events;
using System.Threading;
using System.Windows.Input;

namespace RoundBasedGameMVVM.ViewModels
{
    public class SpellViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        private bool enabled = true;
        private BattleRounds rounds;
        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                if (value != enabled)
                {
                    enabled = value;
                    this.OnPropertyChanged(nameof(enabled));
                }
            }
        }
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="SpellQuoteModel"/> class.
        /// </summary>
        public SpellViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            this.EventAggregator.GetEvent<GetDuellistsEvent>().Subscribe(this.GetDuellists, ThreadOption.UIThread);
            this.SendFighter = new ActionCommand(this.SetQuoteCommandExecute, this.SetQuoteCommandCanExecute);
            this.EventAggregator.GetEvent<GetRoundInformations>().Subscribe(this.GetRoundInformations, ThreadOption.UIThread);

        }


        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        public ICommand SendFighter { get; private set; }

        public Charakter Player { get; set; }
        public Charakter Enemy { get; set; }

        public Duellists duellists { get; set; }
        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------


        public void GetDuellists(Duellists duellists)
        {

            this.Player = duellists.Player;
            this.Enemy = duellists.Enemy;
            this.duellists = duellists;

        }
        private void GetRoundInformations(BattleRounds obj)
        {
            this.rounds = obj;
        }
        #endregion

        #region ------------------------- Commands ------------------------------------------------------------------------
        private bool SetQuoteCommandCanExecute(object parameter)
        {
            if (enabled)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void SetQuoteCommandExecute(object paramenter)
        {
            Enabled = false;
            Thread thread = new Thread(new ThreadStart(Worker));
            Thread thread1 = new Thread(new ThreadStart(Worker1));
            thread1.Start();
            thread.Start();
        }


        private void Worker1()
        {
            this.Player.Spell1(Enemy);
            this.duellists.SpellQuote = Player.SpellQuote;

        }
        private void Worker()
        {
            Thread thread = new Thread(new ThreadStart(Worker3));
            Thread.Sleep(2000);
            this.Enemy.Spell1(this.Player);
            this.duellists.SpellQuote = Enemy.SpellQuote;
            Thread.Sleep(2000);
            Enabled = true;
            thread.Start();



        }

        private void Worker3()
        {
            Thread thread = new Thread(new ThreadStart(Worker4));
            if (duellists.Player.HealthPoints <= 0)
            {

                this.duellists.SpellQuote = "You lost the battle but not the war!";
                thread.Start();
                rounds.BattleRound = 0;
                this.EventAggregator.GetEvent<GetRoundInformations>().Publish(rounds);

            }
            else if (duellists.Enemy.HealthPoints <= 0)
            {

                this.duellists.SpellQuote = "You won the battle!!";

                thread.Start();
                rounds.BattleRound = 0;
                rounds.ExternRound ++;
                this.EventAggregator.GetEvent<GetRoundInformations>().Publish(rounds);
            }
            else
            {
                rounds.BattleRound++;
                this.EventAggregator.GetEvent<GetRoundInformations>().Publish(rounds);
            }
            if (!thread.IsAlive)
            {
                thread.Start();
            }



        }

        private void Worker4()
        {

            Thread.Sleep(1000);
            Thread.Sleep(1000);
            this.duellists.SpellQuote = "";
            this.EventAggregator.GetEvent<GetDuellistsEvent>().Publish(duellists);
        }
        #endregion
    }
}