using Common.Command;
using Data;
using Microsoft.Practices.Prism.Events;
using RoundBasedGameMVVM.Events;
using RoundBasedGameMVVM.Views;
using System;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Input;

namespace RoundBasedGameMVVM.ViewModels
{
    internal class GameStartModel : ViewModelBase
    {


        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        private bool alreadInit = false;
        private BattleRounds rounds;
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="GameStartModel"/> class.
        /// </summary>
        public GameStartModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            //hookup Command
            this.GameStartCommand = new ActionCommand(this.StartGameCommandExecute, this.StartGameCommandCanExecute);
            //hookup Command
            this.NewGameCommand = new ActionCommand(this.NewGameCommandExecute, this.NewGameCommandCanExecute);
            //Subscribe to the event
            this.EventAggregator.GetEvent<GetRoundInformations>().Subscribe(this.GetRoundInformations, ThreadOption.UIThread);
        }


        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        public ICommand GameStartCommand { get; private set; }
        public ICommand NewGameCommand { get; set; }
        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------
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
        private bool StartGameCommandCanExecute(object parameter)
        {
            return true;
        }
        /// <summary>
        /// Creates a Character for the Round before beginning with the game
        /// </summary>
        /// <param name="parameter"></param>
        private void StartGameCommandExecute(object parameter)
        {
            // hier kommt die Datenbank anbiendung für die Charakter verbindung
            BattleView battleView = new BattleView();
            BattleViewModel battleViewModel = new BattleViewModel(this.EventAggregator);
            battleView.DataContext = battleViewModel;
            Charakter character;
            AskDb(out character);
            if (!alreadInit)
            {

                character.CharacterID = 1;
                switch (character.CharacterID)
                {
                    case 1:


                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;

                }
                alreadInit = true;
                this.EventAggregator.GetEvent<GetRoundInformations>().Publish(this.rounds);
                this.EventAggregator.GetEvent<GetCharakterInformationEvent>().Publish(character);
            }
            else
            {
                //Publish all event which is needed for a new game
                this.EventAggregator.GetEvent<GetCharakterInformationEvent>().Publish(character);
                this.EventAggregator.GetEvent<GetRoundInformations>().Publish(this.rounds);
                this.EventAggregator.GetEvent<SwapToGameEvent>().Publish(battleView);
            }


        }
        private bool NewGameCommandCanExecute(object arg)
        {
            return true;
        }
        /// <summary>
        /// change the curren View to the characterSelection View
        /// </summary>
        /// <param name="obj"></param>
        private void NewGameCommandExecute(object obj)
        {

            CharacterSelectionView characterSelectionView = new CharacterSelectionView();
            CharacterSelectionViewModel characterSelectionViewModel = new CharacterSelectionViewModel(this.EventAggregator);
            characterSelectionView.DataContext = characterSelectionViewModel;
            this.EventAggregator.GetEvent<SwapToCharacterSelectionEvent>().Publish(characterSelectionView);
        }
        /// <summary>
        /// gets the information of the rounds
        /// </summary>
        /// <param name="rounds"></param>
        private void GetRoundInformations(BattleRounds rounds)
        {
            this.rounds = rounds;
        }

    }
    #endregion
}

