using Common.Command;
using Data;
using Microsoft.Practices.Prism.Events;
using RoundBasedGameMVVM.Events;
using RoundBasedGameMVVM.Views;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.TextFormatting;

namespace RoundBasedGameMVVM.ViewModels
{
    public class SaveStateViewModel:ViewModelBase
    {

        #region ------------------------- Fields, Constants, Delegates, Events --------------------------------------------
        private bool alreadInit = false;
        private AccountInfo accountinfo;
        #endregion

        #region ------------------------- Constructors, Destructors, Dispose, Clone ---------------------------------------
        /// <summary>
        /// Initializes a new instance of the <see cref="SaveStateViewModel"/> class.
        /// </summary>
        public SaveStateViewModel(IEventAggregator eventAggregator) : base(eventAggregator)
        {
            this.SaveSlot1 = new ActionCommand(this.SaveSlot1CommandExecute, this.SaveSlot1CommandCanExecute);
            this.EventAggregator.GetEvent<GetAccountInformationsEvent>().Subscribe(GetAccountInformation,ThreadOption.UIThread);
        }

        


        #endregion

        #region ------------------------- Properties, Indexers ------------------------------------------------------------
        public ICommand SaveSlot1 { get; set; }
        public ICommand SaveSlot2 { get; set; }
        public ICommand SaveSlot3 { get; set; }
        #endregion

        #region ------------------------- Private helper ------------------------------------------------------------------
        /// <summary>
        /// get the account information from the User
        /// </summary>
        /// <param name="obj"></param>
        private void GetAccountInformation(AccountInfo obj)
        {
            accountinfo = obj;
        }

        static void AskDb(out Charakter character, out BattleRounds rounds ,AccountInfo accountinfo)
        {
            //init variables 
            string dbName = "MVVM";
            // create connection string
            string connectionString = @"Data Source=MARTIN-LAPTOP\SQLEXPRESS;" + "Trusted_Connection = yes;" + $"database={dbName};" + "connection timeout =10";

            //create input query
            Console.WriteLine("Geben sie Die Datei ein ");
            string query = $"Select * From CHARACTERS WHERE CharacterID = {accountinfo.CharacterID}";
            rounds = new BattleRounds(0, 0);
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

            query = $"UPDATE GAMESAVES SET CharacterID = {accountinfo.CharacterID} WHERE SaveID = {accountinfo.AccountId}";


         

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
        private bool SaveSlot1CommandCanExecute(object arg)
        {
            return true;
        }

        private void SaveSlot1CommandExecute(object obj)
        {
            accountinfo.AccountId = 1;
            if (this.accountinfo.IsNew)
            {
                // hier kommt die Datenbank anbiendung für die Charakter verbindung
                BattleView battleView = new BattleView();
                BattleViewModel battleViewModel = new BattleViewModel(this.EventAggregator);
                battleView.DataContext = battleViewModel;
                BattleRounds rounds;
                Charakter character;
                AskDb(out character, out rounds ,this.accountinfo);
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
                    this.EventAggregator.GetEvent<GetRoundInformations>().Publish(rounds);
                    this.EventAggregator.GetEvent<GetCharakterInformationEvent>().Publish(character);
                }
                else
                {
                    this.EventAggregator.GetEvent<GetCharakterInformationEvent>().Publish(character);
                    this.EventAggregator.GetEvent<GetRoundInformations>().Publish(rounds);
                    this.EventAggregator.GetEvent<SwapToGameEvent>().Publish(battleView);
                }
            }
            else
            {

            }
        }
        #endregion

    }
}
