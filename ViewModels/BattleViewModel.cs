using Common.Command;
using Data;
using Microsoft.Practices.Prism.Events;
using RoundBasedGameMVVM.Events;
using RoundBasedGameMVVM.Views;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.TextFormatting;

namespace RoundBasedGameMVVM.ViewModels
{
    public class BattleViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events ------------------------------
        /// <summary>
        /// View that is currently bound to the <see cref="ContentControl"/> left.
        /// </summary>
        private UserControl spellView;
        private UserControl spellQuoteView;
        private UserControl battleUiView;
        private GameOverView gameOverView; 
        private LoadingScreenView loadingScreenView;
        private Duellists duellists;
        private BattleRounds round;
        private bool gameOver = false; 
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone -------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="BattleViewModel"/> class.
        /// </summary>
        public BattleViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {

           
            // Hookup commands to associated methods
            this.EventAggregator.GetEvent<GetCharakterInformationEvent>().Subscribe(this.GetCharacter, ThreadOption.UIThread);
            this.EventAggregator.GetEvent<GetDuellistsEvent>().Subscribe(this.GetDuellists, ThreadOption.UIThread);
            this.EventAggregator.GetEvent<GetRoundInformations>().Subscribe(this.GetRoundInformation, ThreadOption.UIThread);
            //init the SpellviewModel in this view
            this.spellView = new SpellView();
            SpellViewModel spellViewModel = new SpellViewModel(this.EventAggregator);
            spellView.DataContext = spellViewModel;
            //init the SpellQuoteModel in this view
            this.spellQuoteView = new SpellQuoteView();
            SpellQuoteModel spellQuoteModel = new SpellQuoteModel(this.EventAggregator);
            spellQuoteView.DataContext = spellQuoteModel;
            //init the battleUiModel in this view
            this.battleUiView = new BattleUiView();
            BattleUiViewModel battleUiModel = new BattleUiViewModel(this.EventAggregator);
            battleUiView.DataContext = battleUiModel;
            //init the loadingscreen Model in this view
            this.loadingScreenView = new LoadingScreenView();
            LoadingScreenViewModel loadingScreenViewModel = new LoadingScreenViewModel(this.EventAggregator);
            this.loadingScreenView.DataContext = loadingScreenViewModel;
            //init the gameoverView Model in this view
            this.gameOverView = new GameOverView();
            GameOverViewModel gameOverViewModel = new GameOverViewModel(this.EventAggregator);
            gameOverView.DataContext = gameOverViewModel;

        }

        private void GetRoundInformation(BattleRounds obj)
        {
            this.round = obj;
        }
        #endregion

        #region ------------------------- Properties, Indexers ----------------------------------------------
        
        public ICommand SpellViewCommand { get; private set; }

        /// <summary>
        /// Gets and sets the view that is currently bound to the <see cref="LoadingScreenView"/> left.
        /// </summary>

        public LoadingScreenView LoadingScreenView
        {
            get
            {
                return loadingScreenView;
            }
            set
            {
                if (loadingScreenView != value)
                {
                    loadingScreenView = value;
                    this.OnPropertyChanged(nameof(this.loadingScreenView));
                }
            }
        }

        /// <summary>
        /// Gets and sets the view that is currently bound to the <see cref="GameOverView/> left.
        /// </summary>
        public GameOverView GameOverView
        {
            get
            {
                return gameOverView;
            }
            set
            {
                if (gameOverView != value)
                {
                    gameOverView = value;
                    this.OnPropertyChanged(nameof(this.gameOverView));
                }
            }
        }

        /// <summary>
        /// Gets and sets the view that is currently bound to the <see cref="SpellView/> left.
        /// </summary>
        public UserControl SpellView
        {
            get
            {
                return this.spellView;
            }
            set
            {
                if (this.spellView != value)
                {
                    this.spellView = value;
                    this.OnPropertyChanged(nameof(this.spellView));
                }
            }
        }

        /// <summary>
        /// Gets and sets the view that is currently bound to the <see cref="BattleUiView/> left.
        /// </summary>
        public UserControl BattleUiView
        {
            get
            {
                return this.battleUiView;
            }
            set
            {
                if (this.battleUiView != value)
                {
                    this.battleUiView = value;
                    this.OnPropertyChanged(nameof(this.battleUiView));
                }
            }
        }
        /// <summary>
        /// Gets and sets the view that is currently bound to the <see cref="SpellQuoteView"/> left.
        /// </summary>
        public UserControl SpellQuoteView
        {
            get
            {
                return this.spellQuoteView;
            }
            set
            {
                if (this.spellQuoteView != value)
                {
                    this.spellQuoteView = value;
                    this.OnPropertyChanged(nameof(this.spellQuoteView));
                }
            }
        }


        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------
        /// <summary>
        /// Get the Character Information.<see cref="AskDb(out Charakter)"/> Select with a random number the Enemytyp from the Db
        /// </summary>
        /// <param name="character"></param>
        public void GetCharacter(Charakter character)
        {
            //Start Properties of our duellists
            Charakter enemy;
            AskDb(out enemy);
            // Creates an new Instance of duellists
            Duellists duellists = new Duellists(character, enemy);
            this.duellists = duellists; 
            //publish duellists
            this.EventAggregator.GetEvent<GetDuellistsEvent>().Publish(duellists);

        }
        /// <summary>
        /// Get the information of the duellists
        /// </summary>
        /// <param name="duellists"></param>
        public void GetDuellists(Duellists duellists)
        {
            // if the playable character health >=0
            if (duellists.GameOver==false)
            {
                this.duellists = duellists;
                this.duellists.GameOver = false;
                // if the playable character health <=0
                if (this.duellists.Enemy.HealthPoints <= 0 || this.duellists.Player.HealthPoints <= 0)
                {
                    this.duellists.GameOver = true;
                    this.EventAggregator.GetEvent<GetRoundInformations>().Publish(this.round);
                    this.EventAggregator.GetEvent<GetDuellistsEvent>().Publish(this.duellists);
                    this.EventAggregator.GetEvent<SwapToLoadingScreenEvent>().Publish(this.loadingScreenView);
                    

                }
            }
        }
        /// <summary>
        /// take input of the program and out put from the Db
        /// </summary>
        /// <param name="character"></param>
        private void AskDb(out Charakter character)
        {
            //init variables 
            string dbName = "MVVM";
            // create connection string
            string connectionString = @"Data Source=MARTIN-LAPTOP\SQLEXPRESS;" + "Trusted_Connection = yes;" + $"database={dbName};" + "connection timeout =10";
            Random rnd = new Random();
            string n = rnd.Next(1, 4).ToString();
            string query = $"Select * From ENEMIES  WHERE EnemyID = {n}";
            character = new EnemyMelee("dummy", 0, 0, 0);

            // other codes

            // string query = "SELECT * FROM CPU";



            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {

                    try
                    {
                        //open sql connection
                        command.Connection.Open();

                        // execute query
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {

                                //Nr = int.Parse(reader.GetValue(0).ToString()),
                                //Gpu = reader.GetValue(1).ToString()
                                character.Name = reader.GetValue(1).ToString();
                                character.AttackDamage = int.Parse(reader.GetValue(2).ToString());
                                character.HealthPoints = int.Parse(reader.GetValue(3).ToString());
                                character.Armor = int.Parse(reader.GetValue(4).ToString());
                                character.ImageSource = reader.GetValue(5).ToString();
                            }


                            {

                            }
                            {

                            }

                            Console.ReadLine();
                        }
                    }
                    catch (SqlException ex)
                    {
                        // init stringbuilder
                        StringBuilder errorMassages = new StringBuilder();

                        //create error string 
                        for (int i = 0; i < ex.Errors.Count; i++)
                        {
                            errorMassages.AppendLine("Index #" + i);
                            errorMassages.AppendLine("Message: " + ex.Errors[i].Message);
                            errorMassages.AppendLine("LineNumber: " + ex.Errors[i].LineNumber);
                            errorMassages.AppendLine("Source: " + ex.Errors[i].Source);
                            errorMassages.AppendLine("Procedure: " + ex.Errors[i].Procedure);
                        }
                        //write error to console
                        Console.WriteLine(errorMassages.ToString());

                    }
                    catch (Exception ex)
                    {
                        //write error to console 
                        Console.WriteLine("Exeption: " + ex.Message);
                    }
                }
            }
        }
        #endregion

        #region ------------------------- Commands ------------------------------------------------------------------------
        #endregion
    }
}
