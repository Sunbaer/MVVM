using Data;
using Microsoft.Practices.Prism.Events;
using RoundBasedGameMVVM.Events;
using RoundBasedGameMVVM.Views;
using System;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace RoundBasedGameMVVM.ViewModels
{
    internal class LoadingScreenViewModel : ViewModelBase
    {

        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------

        private BattleView battleView;
        private LevelUpView levelUpView;
        private BattleRounds rounds;
        private Duellists duellists;
        private int multplier;

        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="LoadingScreenView"/> class.
        /// </summary>
        public LoadingScreenViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            //Subscribe to Event
            this.EventAggregator.GetEvent<SwapToGameEvent>().Subscribe(this.GetBattleView, ThreadOption.UIThread);
            //Subscribe to Event
            this.EventAggregator.GetEvent<GetDuellistsEvent>().Subscribe(this.GetDuellists, ThreadOption.UIThread);
            //Subscribe to Event
            this.EventAggregator.GetEvent<GetRoundInformations>().Subscribe(this.GetRoundInformation, ThreadOption.UIThread);

        }
        /// <summary>
        /// gets the round information and use the value as stat multiplier
        /// </summary>
        /// <param name="obj"></param>
        private void GetRoundInformation(BattleRounds obj)
        {
            this.rounds = obj;
            multplier = this.rounds.ExternRound;
        }
        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        // is the Qoute wich shows up in the loaden screen
        public string Quote = "Loading please wait";
        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------
        /// <summary>
        /// gets the information of the curren Battleview
        /// </summary>
        /// <param name="battleView"></param>
        private void GetBattleView(BattleView battleView)
        {
            this.battleView = battleView;
        }
        /// <summary>
        /// gets the information of the Duellists
        /// </summary>
        /// <param name="duellists"></param>
        private void GetDuellists(Duellists duellists)
        {
            this.duellists = duellists;
            if (duellists.GameOver)
            {
                //switching to the gameOver screen if the playable character have no Healthpoints
                if (duellists.Player.HealthPoints <= 0)
                { 
                    GameOverView gameOverView = new GameOverView();
                    GameOverViewModel gameOverViewModel = new GameOverViewModel(this.EventAggregator);
                    gameOverView.DataContext = gameOverViewModel;

                    this.EventAggregator.GetEvent<SwapToGameOver>().Publish(gameOverView);
                    multplier = 1;
                }
                //switching to the levelup Window to increase the stats of the playable character
                else if (duellists.Enemy.HealthPoints <= 0)
                {

                    Thread thread = new Thread(new ThreadStart(SwapToGame));
                    LevelUpView levelUpView = new LevelUpView();
                    LevelUpModel levelUpModel = new LevelUpModel(this.EventAggregator);
                    levelUpView.DataContext = levelUpModel;
                    this.levelUpView = levelUpView;
                    duellists.GameOver = false;
                    Charakter enemy;
                    AskDb(out enemy);
                    thread.Start();
                    duellists.Enemy = enemy;
                    this.duellists = duellists;
                    this.EventAggregator.GetEvent<GetRoundInformations>().Publish(this.rounds);
                    this.EventAggregator.GetEvent<GetDuellistsEvent>().Publish(this.duellists);
                }

            }
        }
  
        private void SwapToGame()
        {
            Thread.Sleep(5000);
            this.EventAggregator.GetEvent<SwapToLevelUpScreenEvent>().Publish(this.levelUpView);
        }
        private void AskDb(out Charakter character)
        {
            //init variables 
            string dbName = "MVVM";
            // create connection string
            string connectionString = @"Data Source=MARTIN-LAPTOP\SQLEXPRESS;" + "Trusted_Connection = yes;" + $"database={dbName};" + "connection timeout =10";
            Random rnd = new Random();
            string n = rnd.Next(1, 4).ToString();
            multplier++;
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
                                character.AttackDamage =multplier*int.Parse(reader.GetValue(2).ToString());
                                character.HealthPoints = multplier * int.Parse(reader.GetValue(3).ToString());
                                character.Armor = multplier * int.Parse(reader.GetValue(4).ToString());
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
