using Common.Command;
using Data;
using Microsoft.Practices.Prism.Events;
using RoundBasedGameMVVM.Events;
using RoundBasedGameMVVM.Views;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RoundBasedGameMVVM.ViewModels
{
    internal class GameOverViewModel:ViewModelBase
    {

        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        private GameStartView gameStartView;
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="GameOverViewModel"/> class.
        /// </summary>
        public GameOverViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            //hookup command
            this.NewRunCommand = new ActionCommand(this.NewRunExecute, this.NewRunCommandCanExecute);
            //Subscribe to Event
            this.EventAggregator.GetEvent<SwapToStartScreenEvent>().Subscribe(this.SwapToStartScreen, ThreadOption.UIThread);
            //hookup command
            this.BackToMainMenueCommand = new ActionCommand(this.BackToManueExecute, this.BackToMainMenueCanExecute);
        }

       


        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        //sets and gets NewRunCommand
        public ICommand NewRunCommand { get; private set; }
        //sets and gets BackToMainMenueCommand
        public ICommand BackToMainMenueCommand { get; private set; }

        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------
        /// <summary>
        /// override the gamestart view with another view
        /// </summary>
        /// <param name="gameStartView"></param>
        private void SwapToStartScreen(GameStartView gameStartView)
        {
            this.gameStartView = gameStartView;
        }
        /// <summary>
        /// take input of the program and out put from the Db
        /// </summary>
        /// <param name="character"></param>
        static void AskDb(out Charakter character)
        {
            //init variables 
            string dbName = "MVVM";
            // create connection string
            string connectionString = @"Data Source=MARTIN-LAPTOP\SQLEXPRESS;" + "Trusted_Connection = yes;" + $"database={dbName};" + "connection timeout =10";

            //create input query
            Console.WriteLine("Geben sie Die Datei ein ");
            string query = "Select * From CHARACTERS WHERE CharacterID = 2";
            character = new Fighter("dummy", 0, 0, 0);

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
        private bool NewRunCommandCanExecute(object parameter)
        {
            return true;
        }
        /// <summary>
        /// Init a new Run instance to start the game
        /// </summary>
        /// <param name="parameter"></param>
        private void NewRunExecute(object parameter)
        {

            BattleView battleView = new BattleView();
            BattleViewModel battleViewModel = new BattleViewModel(this.EventAggregator);
            battleView.DataContext = battleViewModel;
            Charakter character;
            BattleRounds round = new BattleRounds(0,0);
            AskDb(out character);
            this.EventAggregator.GetEvent<GetRoundInformations>().Publish(round);
            this.EventAggregator.GetEvent<GetCharakterInformationEvent>().Publish(character);
            this.EventAggregator.GetEvent<SwapToGameEvent>().Publish(battleView);
        }

        private bool BackToMainMenueCanExecute(object arg)
        {
            return true;
        }
        /// <summary>
        /// Command to return you to the main manue
        /// </summary>
        /// <param name="obj"></param>
        private void BackToManueExecute(object obj)
        {
            
            GameStartView gameStartView = new GameStartView();
            GameStartModel gameStartModel = new GameStartModel(this.EventAggregator);
            gameStartView.DataContext = gameStartModel;
            this.EventAggregator.GetEvent<SwapToStartScreenEvent>().Publish(gameStartView);
        }
        #endregion
    }
}
