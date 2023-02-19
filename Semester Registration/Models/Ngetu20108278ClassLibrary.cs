using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Ngetu20108278Library
{




    //Beginning Of Task 1
    public class ModuleDetails
    {

        private static string[] TheModuleDetails = new string[9];//(amanakmsd and laszlojanos95, 2020).
        public string this[int i]//(amanakmsd and laszlojanos95, 2020).
        {

            get { return TheModuleDetails[i]; }//(amanakmsd and laszlojanos95, 2020).
            set { TheModuleDetails[i] = value; }//(amanakmsd and laszlojanos95, 2020).


        }




        protected static int RemainingSelfStudyHours { get; set; }

        protected static int SelfStudyHours { get; set; }



    }


    public class Tables : ModuleDetails
    {
        public DataTable MainTable { get; set; }


        public void CreateTable()
        {

            MainDataTableDetails();

        }

        public void CalculateSelfStudyHours()
        {


            SelfStudyHours = (Convert.ToInt32(this[2]) * 10) / Convert.ToInt32(this[4]) - Convert.ToInt32(this[3]);


        }


        public void CalculateRemainingSelfStudyHours()
        {

            try
            {

                RemainingSelfStudyHours = (Convert.ToInt32(this[2]) * 10) / Convert.ToInt32(this[4]) - Convert.ToInt32(this[3]);

            }
            catch
            {




            }

        }

        public void CalculateRemainingSelfStudyHoursConditional()
        {


            if (this[6] == this[0])
            {

                RemainingSelfStudyHours = (Convert.ToInt32(this[2]) * 10) / Convert.ToInt32(this[4]) - Convert.ToInt32(this[3]) - int.Parse(this[5]);

            }





        }



        public DataTable MainDataTableDetails()
        {
            MainTable = new DataTable("MainTable");//(Syncfusion, 2021).
            MainTable.Columns.Add(new DataColumn("Code", typeof(string)));//(Syncfusion, 2021).

            MainTable.Columns.Add(new DataColumn("Name", typeof(string)));//(Syncfusion, 2021).

            MainTable.Columns.Add(new DataColumn("Credits", typeof(int)));//(Syncfusion, 2021).

            MainTable.Columns.Add(new DataColumn("Class Hours", typeof(int)));//(Syncfusion, 2021).

            MainTable.Columns.Add(new DataColumn("Required Self Study Hours Per Week", typeof(int)));//(Syncfusion, 2021).

            MainTable.Columns.Add(new DataColumn("Remaining Self Study Hours", typeof(int)));//(Syncfusion, 2021).




            DataRow UserInput = MainTable.NewRow();//(Behera, 2020).
            UserInput["Code"] = this[0];//(Behera, 2020).
            UserInput["Name"] = this[1];//(Behera, 2020).
            UserInput["Credits"] = Convert.ToInt32(this[2]);//(Behera, 2020).
            UserInput["Class Hours"] = Convert.ToInt32(this[3]);//(Behera, 2020).
            UserInput["Required Self Study Hours Per Week"] = SelfStudyHours;//(Behera, 2020).
            UserInput["Remaining Self Study Hours"] = RemainingSelfStudyHours;//(Behera, 2020).


            MainTable.Rows.Add(UserInput);//(Behera, 2020).

           

            return MainTable;//(Syncfusion, 2021).


        }

    }
    //End Of Task 1

    //Beginning Of Task 2(Revised)
    public class TheDatabases : Tables
    {



        public string LiveConnectionToDatabase = "";


        private static string[] DataEntries = new string[4];
        public string this[int g]
        {

            get { return DataEntries[g]; }
            set { DataEntries[g] = value; }


        }



        public void AddModulesToDatabase()
        {

            using (SqlConnection NewConnection = new SqlConnection(LiveConnectionToDatabase))//(Khan, 2019).
            {

                NewConnection.Open();//(B, s.a).
                MainDataTableDetails();

                using (SqlBulkCopy A = new SqlBulkCopy(NewConnection))//(MorganTechSpace, 2014)
                {

                    A.DestinationTableName = "AllModules";//(MorganTechSpace, 2014)

                    try
                    {
                        A.WriteToServer(MainTable);//(MorganTechSpace, 2014)


                    }
                    catch
                    {

                    }





                }
                NewConnection.Close();//(Artemiou, 2018).
            }





        }


        public void AddModulesToLoggedInUser()
        {

            using (SqlConnection NewConnection2 = new SqlConnection(LiveConnectionToDatabase))//(Khan, 2019).
            {

                NewConnection2.Open();//(B, s.a).
                MainDataTableDetails();

                MainTable.Columns.Add(new DataColumn("ModuleAssignedToUser", typeof(string)));

                foreach(DataRow bb in MainTable.Rows)
                {

                    bb["ModuleAssignedToUser"] = this[0];

                }
              
                

                using (SqlBulkCopy B = new SqlBulkCopy(NewConnection2))//(MorganTechSpace, 2014).
                {

                    B.DestinationTableName = "PersonalUserModules";//(MorganTechSpace, 2014).

                    try
                    {
                        B.WriteToServer(MainTable);//(MorganTechSpace, 2014).


                    }
                    catch
                    {

                    }





                }
                NewConnection2.Close();//(Artemiou, 2018).
            }





        }
        public void ReturnModulesToDatatable()
        {

            using (SqlConnection AnotherNewConnection = new SqlConnection(LiveConnectionToDatabase))//(Khan, 2019).
            {
                string sqlQueryForDatabase = "SELECT * FROM AllModules";//(B, s.a).

                AnotherNewConnection.Open();//(B, s.a).

                using (SqlCommand CopyDataToDatatable = new SqlCommand(sqlQueryForDatabase, AnotherNewConnection))//(Khan, 2019).
                {
                    CopyDataToDatatable.CommandType = CommandType.Text;//(Khan, 2019).


                    using (SqlDataAdapter ReadingDataFromDatatable = new SqlDataAdapter(CopyDataToDatatable))//(Khan, 2019).
                    {

                        MainDataTableDetails();

                        DataTable LK = new DataTable();

                        ReadingDataFromDatatable.Fill(LK);//(Khan, 2019).

                        foreach (DataRow mm in LK.Rows)
                        {

                            MainTable.Rows.Add(mm.ItemArray);
                            MainTable.AcceptChanges();



                        }







                    }

                    AnotherNewConnection.Close();//(Artemiou, 2018).
                }


            }
        }
        public void StudentRegistrationDetails()
        {
            using (SqlConnection AnotherConnection = new SqlConnection(LiveConnectionToDatabase))//(Khan, 2019).
            {

                SqlCommand CreateAStudent = new SqlCommand("SP_CreateANewStudent", AnotherConnection);//(Khan, 2019).
                CreateAStudent.CommandType = CommandType.StoredProcedure;//(Khan, 2019).

                SqlParameter UserName = new SqlParameter();//(Artemiou, 2018).
                UserName.ParameterName = "@StudentName";//(Artemiou, 2018).
                UserName.SqlDbType = System.Data.SqlDbType.VarChar;//(Artemiou, 2018).
                UserName.Value = this[0];//(Artemiou, 2018).

                SqlParameter UserPassword = new SqlParameter();//(Artemiou, 2018).
                UserPassword.ParameterName = "@StudentPassword";//(Artemiou, 2018).
                UserPassword.SqlDbType = System.Data.SqlDbType.Int;//(Artemiou, 2018).
                UserPassword.Value = this[1].GetHashCode();//(Diwan, 2019).

                CreateAStudent.Parameters.Add(UserName);//(Artemiou, 2018).
                CreateAStudent.Parameters.Add(UserPassword);//(Artemiou, 2018).

                AnotherConnection.Open();//(B, s.a).
                CreateAStudent.ExecuteNonQuery();//(MorganTechSpace, 2014).


                AnotherConnection.Close();//(Artemiou, 2018).

            }



        }

        public void StudentLogin()
        {

            using (SqlConnection ThirdConnection = new SqlConnection(LiveConnectionToDatabase))//(Khan, 2019).
            {

                SqlCommand LoginAsAStudent = new SqlCommand("SP_StudentLogin", ThirdConnection);//(Khan, 2019).
                LoginAsAStudent.CommandType = CommandType.StoredProcedure;//(Khan, 2019).

                SqlParameter LoginName = new SqlParameter();//(Artemiou, 2018).
                LoginName.ParameterName = "@StudentName";//(Artemiou, 2018).
                LoginName.SqlDbType = System.Data.SqlDbType.VarChar;//(Artemiou, 2018).
                LoginName.Value = this[0];//(Artemiou, 2018).

                SqlParameter LoginPassword = new SqlParameter();//(Artemiou, 2018).
                LoginPassword.ParameterName = "@StudentPassword";//(Artemiou, 2018).
                LoginPassword.SqlDbType = System.Data.SqlDbType.Int;//(Artemiou, 2018).
                LoginPassword.Value = this[1].GetHashCode();//(Diwan, 2019).

                LoginAsAStudent.Parameters.Add(LoginName);//(Artemiou, 2018).
                LoginAsAStudent.Parameters.Add(LoginPassword);//(Artemiou, 2018).

                ThirdConnection.Open();//(B, s.a).

                LoginAsAStudent.ExecuteNonQuery();//(MorganTechSpace, 2014).

                SqlDataAdapter InsertingDataIntoDataTable = new SqlDataAdapter(LoginAsAStudent);//(Khan, 2019).

                
                MainDataTableDetails();
                
                DataTable KP = new DataTable();


                InsertingDataIntoDataTable.Fill(KP);//(Khan, 2019).

                foreach (DataRow ss in KP.Rows)
                {

                   
                    MainTable.ImportRow(ss);
                    MainTable.AcceptChanges();
                 

                }
              

                ThirdConnection.Close();//(Artemiou, 2018).

            }




        }



        public void StudentLogout()
        {
            MainDataTableDetails().Clear();
        }

        public void UpdateSelfStudyHours()
        {
            using (SqlConnection FourthConnection = new SqlConnection(LiveConnectionToDatabase))//(Khan, 2019).
            {


                SqlCommand UpdateSelfStudyTime = new SqlCommand("SP_UpdateRemainingSelfStudyHours", FourthConnection);//(Khan, 2019).
                UpdateSelfStudyTime.CommandType = CommandType.StoredProcedure;//(Khan, 2019).

                SqlParameter ModuleCode = new SqlParameter();//(Artemiou, 2018).
                ModuleCode.ParameterName = "@ModuleCode";//(Artemiou, 2018).
                ModuleCode.SqlDbType = System.Data.SqlDbType.VarChar;//(Artemiou, 2018).
                ModuleCode.Value = this[2];//(Artemiou, 2018).

                SqlParameter NewSelfStudyHours = new SqlParameter();//(Artemiou, 2018).
                NewSelfStudyHours.ParameterName = "@RemainingSelfStudyHours";//(Artemiou, 2018).
                NewSelfStudyHours.SqlDbType = System.Data.SqlDbType.Int;//(Artemiou, 2018).
                NewSelfStudyHours.Value = int.Parse(this[3]);//(Artemiou, 2018).

                UpdateSelfStudyTime.Parameters.Add(ModuleCode);//(Artemiou, 2018).
                UpdateSelfStudyTime.Parameters.Add(NewSelfStudyHours);//(Artemiou, 2018).

                FourthConnection.Open();//(B, s.a).

                UpdateSelfStudyTime.ExecuteNonQuery();//(MorganTechSpace, 2014).
                //SqlDataAdapter InsertingDataIntoDataTable2 = new SqlDataAdapter(UpdateSelfStudyTime);//(Khan, 2019).


                //InsertingDataIntoDataTable2.Fill(MainDataTableDetails());//(Khan, 2019).


                FourthConnection.Close();//(Artemiou, 2018).

            }






        }
        public void UpdateSelfStudyHoursLoggedInUser()
        {
            using (SqlConnection FifthConnection = new SqlConnection(LiveConnectionToDatabase))//(Khan, 2019).
            {


                SqlCommand UpdateSelfStudyTimeForLoggedInUser = new SqlCommand("SP_UpdateRemainingSelfStudyHoursPersonal", FifthConnection);//(Khan, 2019).
                UpdateSelfStudyTimeForLoggedInUser.CommandType = CommandType.StoredProcedure;//(Khan, 2019).

                SqlParameter ModuleCode = new SqlParameter();//(Artemiou, 2018).
                ModuleCode.ParameterName = "@ModuleCode";//(Artemiou, 2018).
                ModuleCode.SqlDbType = System.Data.SqlDbType.VarChar;//(Artemiou, 2018).
                ModuleCode.Value = this[2];//(Artemiou, 2018).

                SqlParameter NewSelfStudyHours = new SqlParameter();//(Artemiou, 2018).
                NewSelfStudyHours.ParameterName = "@RemainingSelfStudyHours";//(Artemiou, 2018).
                NewSelfStudyHours.SqlDbType = System.Data.SqlDbType.Int;//(Artemiou, 2018).
                NewSelfStudyHours.Value = int.Parse(this[3]);//(Artemiou, 2018).

                UpdateSelfStudyTimeForLoggedInUser.Parameters.Add(ModuleCode);//(Artemiou, 2018).
                UpdateSelfStudyTimeForLoggedInUser.Parameters.Add(NewSelfStudyHours);//(Artemiou, 2018).

                FifthConnection.Open();//(B, s.a).

                UpdateSelfStudyTimeForLoggedInUser.ExecuteNonQuery();//(MorganTechSpace, 2014).
                //SqlDataAdapter InsertingDataIntoDataTable3 = new SqlDataAdapter(UpdateSelfStudyTimeForLoggedInUser);//(Khan, 2019).


                //InsertingDataIntoDataTable3.Fill(MainDataTableDetails());//(Khan, 2019).


                FifthConnection.Close();//(Artemiou, 2018).

            }






        }







    }
    //End Of Task 2 (Revised)

    //Beginning Of Task 3
    public class Classes : TheDatabases
    {
        
        public ModuleDetails ModuleDetails { get; set; }
        
        public Tables Tables { get; set; }
     
        public TheDatabases TheDatabases { get; set; }

      

        private static string[] ReminderDetails = new string[3];
        public string this[int e]
        {

            get { return ReminderDetails[e]; }
            set { ReminderDetails[e] = value; }


        }
       
        
        public void InstanciateAll()
        {
            ModuleDetails = new ModuleDetails();
            Tables = new Tables();
            TheDatabases = new TheDatabases();
        

        }

        public void AddReminderData()
        {

            using (SqlConnection SixthConnection = new SqlConnection(LiveConnectionToDatabase))
            {


                SqlCommand AddSelfStudyReminderData = new SqlCommand("SP_AddReminder", SixthConnection);
                AddSelfStudyReminderData.CommandType = CommandType.StoredProcedure;


                SqlParameter ModuleCode = new SqlParameter();
                ModuleCode.ParameterName = "@ModuleToStudy";
                ModuleCode.SqlDbType = System.Data.SqlDbType.VarChar;
                ModuleCode.Value = this[0];

                SqlParameter ModuleName = new SqlParameter();
                ModuleName.ParameterName = "@ModuleCodeOfModuleToStudy";
                ModuleName.SqlDbType = System.Data.SqlDbType.VarChar;
                ModuleName.Value = this[1];

                SqlParameter DateSetForSelfStudying = new SqlParameter();
                DateSetForSelfStudying.ParameterName = "@DateSetToStudy";
                DateSetForSelfStudying.SqlDbType = System.Data.SqlDbType.Date;
                DateSetForSelfStudying.Value = Convert.ToDateTime(this[2]);

                AddSelfStudyReminderData.Parameters.Add(ModuleCode);
                AddSelfStudyReminderData.Parameters.Add(ModuleName);
                AddSelfStudyReminderData.Parameters.Add(DateSetForSelfStudying);

                SixthConnection.Open();

                AddSelfStudyReminderData.ExecuteNonQuery();

                SixthConnection.Close();

            }




        }
        public void GetReminder()
        {
            using (SqlConnection SeventhConnection = new SqlConnection(LiveConnectionToDatabase))
            {
                SqlCommand GetAllSetReminders = new SqlCommand("SELECT * FROM Reminder", SeventhConnection);
                GetAllSetReminders.CommandType = CommandType.Text;


                SeventhConnection.Open();

                GetAllSetReminders.ExecuteNonQuery();

                SqlDataReader ObtainReminders = GetAllSetReminders.ExecuteReader();///(Triconsole, 2013).

                while (ObtainReminders.Read())///(Triconsole, 2013).
                {

                    this[0] = Convert.ToString(ObtainReminders["ModuleCodeOfModuleToStudy"].ToString());
                    this[1] = Convert.ToString(ObtainReminders["ModuleToStudy"].ToString());
                    this[2] = Convert.ToString(ObtainReminders["DateSetToStudy"].ToString());


                }

                ObtainReminders.Close();///(Triconsole, 2013).


                SeventhConnection.Close();

            }





        }

       


        }
}

