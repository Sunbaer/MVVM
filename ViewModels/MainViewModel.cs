using Data;
using Microsoft.Practices.Prism.Events;
using RoundBasedGameMVVM.Events;
using RoundBasedGameMVVM.Views;
using System;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace RoundBasedGameMVVM.ViewModels
{
    /// <summary>
    /// Main ViewModel of the application
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region ------------------------- Fields, Constants, Delegates, Events ------------------------------
        /// <summary>
        /// View that is currently bound to the <see cref="ContentControl"/> left.
        /// </summary>
        private UserControl loadingScreenViewModel;
        private UserControl battleViewModel;
        private UserControl gameStartView;
        private UserControl gameOverView;
        private UserControl levelUpView;
        private UserControl characterSelectionView;
        private UserControl saveStateView;

        public UserControl SaveStateView
        {
            get
            {
                return this.saveStateView;
            }
            set
            {
                if (saveStateView != value)
                {
                    saveStateView = value;
                    this.OnPropertyChanged(nameof(SaveStateView));
                }
            }
        }




        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone -------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        public MainViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            //Subscribe to all which needet to start the game
            this.EventAggregator.GetEvent<SwapToGameEvent>().Subscribe(this.OnSwapToBattle, ThreadOption.UIThread);
            this.EventAggregator.GetEvent<SwapToStartScreenEvent>().Subscribe(this.OnSwapToScreen, ThreadOption.UIThread);
            this.EventAggregator.GetEvent<SwapToLoadingScreenEvent>().Subscribe(this.OnSwapToLoadingScreen, ThreadOption.UIThread);
            this.EventAggregator.GetEvent<SwapToGameOver>().Subscribe(this.OnSwapToGameOverScreen, ThreadOption.UIThread);
            this.EventAggregator.GetEvent<SwapToLevelUpScreenEvent>().Subscribe(this.OnSwaptoLevelUpScreen, ThreadOption.UIThread);
            this.EventAggregator.GetEvent<SwapToCharacterSelectionEvent>().Subscribe(this.OnSwapToCharacterSelection, ThreadOption.UIThread);
            this.EventAggregator.GetEvent<SwapToSaveStateView>().Subscribe(this.OnSwapToSaveStates, ThreadOption.UIThread);
            //init a new instanc of the gameStartView
            this.gameStartView = new GameStartView();
            GameStartModel gameStartViewModel = new GameStartModel(this.EventAggregator);
            gameStartView.DataContext = gameStartViewModel;
            BattleRounds battleRounds = new BattleRounds(0, 0);
            this.EventAggregator.GetEvent<GetRoundInformations>().Publish(battleRounds);
            AskDb();
        }

      




        #endregion

        #region ------------------------- Properties, Indexers ----------------------------------------------
        /// <summary>
        /// Gets the students view button command.
        /// </summary>
        public ICommand SpellViewCommand { get; private set; }

        /// <summary>
        /// Gets and sets the view that is currently bound to the <see cref="CharacterSelectionView"/> left.
        /// </summary>
        public UserControl CharacterSelectionView
        {
            get
            {
                return this.characterSelectionView;
            }
            set
            {
                if (this.characterSelectionView != value)
                {
                    this.characterSelectionView = value;
                    this.OnPropertyChanged(nameof(CharacterSelectionView));
                }
            }
        }
        /// <summary>
        /// Gets and sets the view that is currently bound to the <see cref="BattleViewModel"/> left.
        /// </summary>
        public UserControl BattleViewModel
        {
            get
            {
                return this.battleViewModel;
            }
            set
            {
                if (this.battleViewModel != value)
                {
                    this.battleViewModel = value;
                    this.OnPropertyChanged(nameof(this.BattleViewModel));
                }
            }
        }
        /// <summary>
        /// Gets and sets the view that is currently bound to the <see cref="LoadingScreenViewModel"/> left.
        /// </summary>
        public UserControl LoadingScreenViewModel
        {
            get
            {
                return loadingScreenViewModel;
            }
            set
            {
                if (loadingScreenViewModel != value)
                {
                    loadingScreenViewModel = value;
                    this.OnPropertyChanged(nameof(this.LoadingScreenViewModel));
                }
            }
        }
        /// <summary>
        /// Gets and sets the view that is currently bound to the <see cref="GameStartModel"/> left.
        /// </summary>
        public UserControl GameStartModel
        {
            get
            {
                return this.gameStartView;
            }
            set
            {
                if (this.gameStartView != value)
                {
                    this.gameStartView = value;
                    this.OnPropertyChanged(nameof(this.GameStartModel));
                }
            }
        }
        /// <summary>
        /// Gets and sets the view that is currently bound to the <see cref="GameOverView"/> left.
        /// </summary>

        public UserControl GameOverView
        {
            get
            {
                return this.GameOverView;
            }
            set
            {
                if (this.gameOverView != value)
                {
                    this.gameOverView = value;
                    this.OnPropertyChanged(nameof(this.GameOverView));
                }
            }
        }
        /// <summary>
        /// Gets and sets the view that is currently bound to the <see cref="LevelUpView"/> left.
        /// </summary>

        public UserControl LevelUpView
        {
            get
            {
                return this.levelUpView;
            }
            set
            {
                if (levelUpView != value)
                {
                    levelUpView = value;
                    this.OnPropertyChanged(nameof(this.LevelUpView));
                }
            }
        }


    


        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------
        static void AskDb()
        {
            //init variables 
            string dbName = "MVVM";
            // create connection string
            string connectionString = @"Data Source=MARTIN-LAPTOP\SQLEXPRESS;" + "Trusted_Connection = yes;" + $"database={dbName};" + "connection timeout =10";

            //create input query
            Console.WriteLine("Geben sie Die Datei ein ");
            string query = "Select * From CHARACTERS WHERE CharacterID = 1";
            Charakter character = new Fighter("dummy", 0, 0, 0);

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
        //overwrite the curren view with another view 
        private void OnSwapToSaveStates(SaveStatesView saveStateView)
        {
            this.GameStartModel = saveStateView;
        }
        //overwrite the curren view with another view 
        private void OnSwapToCharacterSelection(CharacterSelectionView characterSelectionView)
        {
            this.GameStartModel = characterSelectionView;
        }
        //overwrite the curren view with another view 
        private void OnSwaptoLevelUpScreen(LevelUpView levelUpScreenView)
        {
            this.GameStartModel = levelUpScreenView;
        }
        //overwrite the curren view with another view 
        private void OnSwapToLoadingScreen(LoadingScreenView loadingScreenView)
        {

            this.GameStartModel = loadingScreenView;
        }
        //overwrite the curren view with another view 
        private void OnSwapToScreen(GameStartView gameStartView)
        {
            this.GameStartModel = gameStartView;
        }
        //overwrite the curren view with another view 
        private void OnSwapToBattle(BattleView battleview)
        {
            this.GameStartModel = battleview;
        }

        private void OnSwapToGameOverScreen(GameOverView gameOverView)
        {
            this.GameStartModel = gameOverView;
        }
        #endregion
    }
}
