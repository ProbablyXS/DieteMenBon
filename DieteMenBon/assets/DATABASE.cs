using DieteMenBon.Properties;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DieteMenBon.assets
{
    class DATABASE
    {
        public static MySqlConnection dbConnect = new MySqlConnection("server=127.0.0.1; uid=; pwd=;database=DieteMenBon;charset=utf8;");
        public static MySqlCommand command;
        public static MySqlDataReader reader;
        public static MySqlDataAdapter data;

        public static bool tryToConnect = false;
        public static void RefreshHistoryPatient(ComboBox sender, int Id)
        {
            sender.Items.Clear();

            command = new MySqlCommand("select * from Patients", dbConnect);
            data = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            data.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                if (row.Field<int>("Patient_Id") == Id)
                {
                    sender.Items.Add(row.Field<DateTime>("History").ToString("dd/MM/yyyy"));
                }
            }

            List<string> list = new List<string>();
            foreach (var item in sender.Items)
            {
                list.Add(item.ToString());
            }
            sender.SelectedItem = list.Max();
        }

        public static void fixUTF8Character(DataGridView sender, int DataGridViewCell)
        {
            DataGridViewRow row = sender.Rows[DataGridViewCell];

            foreach (DataGridViewRow row1 in sender.Rows)
            {
                foreach (DataGridViewCell cell in row1.Cells)
                {
                    if (cell.Value == null) continue;

                    if (cell.Value.ToString().Contains("'"))
                    {
                        row.Cells[cell.ColumnIndex].Value = cell.Value.ToString().Replace("'", " ");
                    }
                }
            }
        }

        public static void GetHistoryPatient(Label Id, ComboBox History, TextBox Last_Name, TextBox First_Name, ComboBox Gender,
                    DateTimePicker Naissance, Label Age, TextBox Place_Of_Birth, TextBox Height, TextBox Phone, TextBox Email,
                    TextBox Address, TextBox Postal_Address, TextBox City, TextBox Job, TextBox Doctor, TextBox Referencing,
                    TextBox past, TextBox present, TextBox futur, TextBox pTel, TextBox Historique_poids,
                    TextBox Habitude_alim, TextBox Horaire_travail, TextBox ATCD_Fam, TextBox ATCD_Perso, TextBox Traitement,
                    TextBox Hormone, TextBox Diurese, TextBox Probleme_digest, TextBox Probleme_circu, TextBox Poids,
                    TextBox Poids_min, TextBox Poids_max, TextBox Poids_pal, TextBox Poids_obj, TextBox Gly,
                    TextBox Ch, TextBox Tsh, TextBox Acide_urique, TextBox Tg, TextBox Autre,
                    TextBox Aller, TextBox Intol, TextBox Tabac, TextBox Tca, TextBox Activite_phys,
                    TextBox Sommeil, TextBox Grignotag, CheckBox C_Eau_Plate, TextBox Eau_Plate, CheckBox C_Eau_Gaz, TextBox Eau_Gaz, CheckBox C_Cafe, TextBox Cafe,
                    CheckBox C_The_inf, TextBox The_inf, CheckBox C_Alcool, TextBox Alcool, CheckBox C_Boisson_suc, TextBox Boisson_suc, TextBox Aversion, TextBox Alim_fetish, TextBox Petit_dej,
                    TextBox Collation_matin, TextBox dejeuner, TextBox collation_apres_midi, TextBox diner, TextBox collation_de_nuit)
        {
            try
            {
                command = new MySqlCommand("select * from Patients where Patient_Id=@Patient_Id and History=@History", dbConnect);
                command.Parameters.AddWithValue("@Patient_Id", Convert.ToInt32(Id.Text));
                command.Parameters.AddWithValue("@History", Convert.ToDateTime(History.Text).ToString("yyyy/MM/dd"));

                reader = command.ExecuteReader();

                if (reader.Read())
                {
                    Last_Name.Text = reader.GetString("Perso_Last_Name").ToString();
                    First_Name.Text = reader.GetString("Perso_First_Name").ToString();
                    Gender.Text = reader.GetString("Perso_Gender").ToString();
                    Naissance.Text = Convert.ToDateTime(reader.GetString("Perso_Date_Of_Birth")).ToString("dd/MM/yyyy");
                    Age.Text = reader.GetString("Perso_Age").ToString();
                    Place_Of_Birth.Text = reader.GetString("Perso_Place_Of_Birth").ToString();
                    Height.Text = reader.GetString("Perso_Height").ToString();
                    Phone.Text = reader.GetString("Perso_Phone").ToString();
                    Email.Text = reader.GetString("Perso_Email").ToString();
                    Address.Text = reader.GetString("Perso_Address").ToString();
                    Postal_Address.Text = reader.GetString("Perso_Postal_Address").ToString();
                    City.Text = reader.GetString("Perso_City").ToString();
                    Job.Text = reader.GetString("Perso_Job").ToString();
                    Doctor.Text = reader.GetString("Perso_Doctor").ToString();
                    Referencing.Text = reader.GetString("Perso_Referencing").ToString();

                    past.Text = reader.GetString("Need_Past_Need").ToString();
                    present.Text = reader.GetString("Need_Present_Need").ToString();
                    futur.Text = reader.GetString("Need_Futur_Need").ToString();
                    pTel.Text = reader.GetString("Habit_Call_Info").ToString();
                    Historique_poids.Text = reader.GetString("Habit_Weight_History").ToString();
                    Habitude_alim.Text = reader.GetString("Habit_Food_Habit").ToString();
                    Horaire_travail.Text = reader.GetString("Habit_Work_Schedule").ToString();
                    ATCD_Fam.Text = reader.GetString("Patho_Familly_ATCD").ToString();
                    ATCD_Perso.Text = reader.GetString("Patho_PERSONAL_ATCD").ToString();
                    Traitement.Text = reader.GetString("Patho_Treatment").ToString();
                    Hormone.Text = reader.GetString("Physio_Hormonal").ToString();
                    Diurese.Text = reader.GetString("Physio_Urine_Color").ToString();
                    Probleme_digest.Text = reader.GetString("Physio_Digestion_problem").ToString();
                    Probleme_circu.Text = reader.GetString("Physio_Circulation_problems").ToString();
                    Poids.Text = reader.GetString("Weight_Weight").ToString();
                    Poids_min.Text = reader.GetString("Weight_Min_Weight").ToString();
                    Poids_max.Text = reader.GetString("Weight_Max_Weight").ToString();
                    Poids_pal.Text = reader.GetString("Weight_Weight_Balance").ToString();
                    Poids_obj.Text = reader.GetString("Weight_Objective_Weight").ToString();
                    Gly.Text = reader.GetString("Bio_Glycemie").ToString();
                    Ch.Text = reader.GetString("Bio_Ch").ToString();
                    Tsh.Text = reader.GetString("Bio_Tsh").ToString();
                    Acide_urique.Text = reader.GetString("Bio_Acide_Urique").ToString();
                    Tg.Text = reader.GetString("Bio_Tg").ToString();
                    Autre.Text = reader.GetString("Bio_Other").ToString();
                    Aller.Text = reader.GetString("Allergy_Allergy").ToString();
                    Intol.Text = reader.GetString("Allergy_Intolerance").ToString();
                    Tabac.Text = reader.GetString("Psycho_Tabac").ToString();
                    Tca.Text = reader.GetString("Psycho_Tca").ToString();
                    Activite_phys.Text = reader.GetString("Psycho_Activity").ToString();
                    Sommeil.Text = reader.GetString("Psycho_Sleep").ToString();
                    Grignotag.Text = reader.GetString("Psycho_Snacking").ToString();
                    Eau_Plate.Text = reader.GetString("Hydratation_Water").ToString();
                    Eau_Gaz.Text = reader.GetString("Hydratation_Gaz_Water").ToString();
                    Cafe.Text = reader.GetString("Hydratation_Coffee").ToString();
                    The_inf.Text = reader.GetString("Hydratation_Infusion_Thea").ToString();
                    Alcool.Text = reader.GetString("Hydratation_Alcohol").ToString();
                    Boisson_suc.Text = reader.GetString("Hydratation_Sweet_Drink").ToString();
                    Aversion.Text = reader.GetString("Aversion_Aversion").ToString();
                    Alim_fetish.Text = reader.GetString("Aversion_Fetish_Food").ToString();
                    Petit_dej.Text = reader.GetString("24H_Breakfast").ToString();
                    Collation_matin.Text = reader.GetString("24H_Morning_Snack").ToString();
                    dejeuner.Text = reader.GetString("24H_Lunch").ToString();
                    collation_apres_midi.Text = reader.GetString("24H_Afternoon_Snack").ToString();
                    diner.Text = reader.GetString("24H_Diner").ToString();
                    collation_de_nuit.Text = reader.GetString("24H_Night_Snack").ToString();

                    if (Eau_Plate.Text.Length > 0) C_Eau_Plate.Checked = true;
                    else
                    {
                        C_Eau_Plate.Checked = false;
                    }
                    if (Eau_Gaz.Text.Length > 0) C_Eau_Gaz.Checked = true;
                    else
                    {
                        C_Eau_Gaz.Checked = false;
                    }
                    if (Cafe.Text.Length > 0) C_Cafe.Checked = true;
                    else
                    {
                        C_Cafe.Checked = false;
                    }
                    if (The_inf.Text.Length > 0) C_The_inf.Checked = true;
                    else
                    {
                        C_The_inf.Checked = false;
                    }
                    if (Alcool.Text.Length > 0) C_Alcool.Checked = true;
                    else
                    {
                        C_Alcool.Checked = false;
                    }
                    if (Boisson_suc.Text.Length > 0) C_Boisson_suc.Checked = true;
                    else
                    {
                        C_Boisson_suc.Checked = false;
                    }
                }
            }


            catch { }

            try { reader.Close(); } catch { }

        }

        public static void SearchPatient(DataGridView sender, TextBox search, Label labelCount)
        {
            sender.Rows.Clear();

            if (search.TextLength == 0)
            {
                RefreshAllPatients(sender, labelCount);
                return;
            }

            command = new MySqlCommand("SELECT * FROM patients WHERE(patient_Id, History) IN (SELECT patient_Id, MAX(History) FROM patients GROUP BY patient_Id)", dbConnect);
            data = new MySqlDataAdapter(command);
            DataTable dt = new DataTable();
            data.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                if (row.Field<int>("Patient_Id").ToString().ToLower().StartsWith(search.Text.ToLower()) ||
                    row.Field<string>("Perso_Last_Name").ToString().ToLower().StartsWith(search.Text.ToLower()) ||
                    row.Field<string>("Perso_First_Name").ToString().ToLower().StartsWith(search.Text.ToLower()) ||
                    row.Field<DateTime>("Perso_Date_Of_Birth").ToString("dd/MM/yyyy").ToLower().StartsWith(search.Text.ToLower()) ||
                    row.Field<string>("Perso_Phone").ToString().ToLower().StartsWith(search.Text.ToLower()) ||
                    row.Field<string>("Perso_Email").ToString().ToLower().StartsWith(search.Text.ToLower()) ||
                    row.Field<string>("Perso_City").ToString().ToLower().StartsWith(search.Text.ToLower()) ||
                    row.Field<string>("Perso_Address").ToString().ToLower().StartsWith(search.Text.ToLower()))
                {

                    sender.Rows.Add(row.Field<int>("Patient_Id"), //ID
                                    row.Field<string>("Perso_Last_Name"), //NOM
                                    row.Field<string>("Perso_First_Name"), //PRENOM
                                    row.Field<DateTime>("Perso_Date_Of_Birth").ToString("dd/MM/yyyy"), //DATE DE NAISSANCE
                                    row.Field<string>("Perso_Phone"), //TELEPHONE
                                    row.Field<string>("Perso_Email"), //EMAIL
                                    row.Field<string>("Perso_City"), //VILLE
                                    row.Field<string>("Perso_Address")); //ADRESSE
                }

                labelCount.Text = "Total: " + sender.Rows.Count.ToString(); //TOTAL COUNT
            }
        }

        public static void AddNewConsultation(int Id)
        {
            int totalConsult = Arithmetic.GeneratorTotalConsultation();

            command = new MySqlCommand("insert into consultation " +
"(Patient_Id, Consultation_Id, Consultation_Date, Price, Payment_Means, Consultation_Comment, " +
"Food_plan_Change, Giving_Document, Solicitation, Weekly_Goals, " +
"Poids, Taille, Ventre, Hanche, Cuisse, Bras, IMC, Muscle, Hydratation, Graisse_Viscerale, Graisse_Sous_Cutanee) " +
 "values " +
"(@Patient_Id, @Consultation_Id, @Consultation_Date, @Price, @Payment_Means, @Consultation_Comment, " +
"@Food_plan_Change, @Giving_Document, @Solicitation, @Weekly_Goals, " +
"@Poids, @Taille, @Ventre, @Hanche, @Cuisse, @Bras, @IMC, @Muscle, @Hydratation, @Graisse_Viscerale, @Graisse_Sous_Cutanee)", dbConnect);

            command.Parameters.AddWithValue("@Patient_Id", Id);
            command.Parameters.AddWithValue("@Consultation_Id", Convert.ToInt32(totalConsult));
            command.Parameters.AddWithValue("@Consultation_Date", DateTime.Now.ToString("yyyy/MM/dd"));
            command.Parameters.AddWithValue("@Price", 0);
            command.Parameters.AddWithValue("@Payment_Means", "Espèce");
            command.Parameters.AddWithValue("@Consultation_Comment", "");
            command.Parameters.AddWithValue("@Food_plan_Change", "");
            command.Parameters.AddWithValue("@Giving_Document", "");
            command.Parameters.AddWithValue("@Solicitation", "");
            command.Parameters.AddWithValue("@Weekly_Goals", "");

            command.Parameters.AddWithValue("@Poids", "");
            command.Parameters.AddWithValue("@Taille", "");
            command.Parameters.AddWithValue("@Ventre", "");
            command.Parameters.AddWithValue("@Hanche", "");
            command.Parameters.AddWithValue("@Cuisse", "");
            command.Parameters.AddWithValue("@Bras", "");
            command.Parameters.AddWithValue("@IMC", "");
            command.Parameters.AddWithValue("@Muscle", "");
            command.Parameters.AddWithValue("@Hydratation", "");
            command.Parameters.AddWithValue("@Graisse_Viscerale", "");
            command.Parameters.AddWithValue("@Graisse_Sous_Cutanee", "");

            try
            {
                command.ExecuteNonQuery();
            }
            catch { MessageBox.Show("Une consultation à cette date est déjà ajouté", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information); }

        }

        public static void GetTotalMoneyCountForMonthAndYear(Label labelCountMonth, Label labelCountYear)
        {
            string month = DateTime.Now.ToString("MM").TrimStart('0').Trim();
            string year = DateTime.Now.ToString("yyyy");

            //MONTH
            try
            {
                double amount = 0;

                command = new MySqlCommand("SELECT * FROM consultation WHERE MONTH(Consultation_Date) = " + month + " AND YEAR(Consultation_Date) = " + year, dbConnect);
                data = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                data.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    amount += +row.Field<double>("Price"); //PRIX
                }

                if (labelCountMonth.Text != amount + "€".ToString())
                {
                    AnimationsForms.ChangeNumber(labelCountMonth, amount);
                }
            }
            catch { AnimationsForms.ChangeNumber(labelCountMonth, 0); }

            //YEAR
            try
            {
                double amount = 0;

                command = new MySqlCommand("SELECT * FROM consultation WHERE YEAR(Consultation_Date) = " + year, dbConnect);
                data = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                data.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    amount += +row.Field<double>("Price"); //PRIX
                }

                if (labelCountYear.Text != amount + "€".ToString())
                {
                    AnimationsForms.ChangeNumber(labelCountYear, amount);
                }
            }
            catch { AnimationsForms.ChangeNumber(labelCountMonth, 0); }
        }

        public static async Task LoginConnection(Form loginForm, TextBox login, TextBox password, Label labelInfo)
        {
            try
            {
                if (!tryToConnect)
                {
                    tryToConnect = true;

                    command = new MySqlCommand("SELECT login, password FROM login WHERE login=@login and password=@password", dbConnect);

                    command.Parameters.AddWithValue("@login", login.Text);
                    command.Parameters.AddWithValue("@password", password.Text);

                    MySqlDataReader dataReader = command.ExecuteReader();

                    if (dataReader.Read())
                    {
                        dataReader.Close();
                        FormMesPatients frm = new FormMesPatients(loginForm);

                        labelInfo.ForeColor = Settings.Default.defaultGreenBackColor;
                        labelInfo.Text = "Connexion réussie";
                        labelInfo.Visible = true;

                        await Task.Delay(1000);

                        frm.Show();
                        await AnimationsForms.OpenForm(frm);
                        await AnimationsForms.HideForm(loginForm);
                        labelInfo.Visible = false;
                    }
                    else
                    {
                        labelInfo.ForeColor = Color.DarkRed;
                        labelInfo.Text = "Connexion refusé";
                        labelInfo.Visible = true;

                        dataReader.Close();
                        tryToConnect = false;
                        return;
                    }
                }

                tryToConnect = false;
            }

            catch { tryToConnect = false; }
        }
        public static void RefreshAllPatients(DataGridView sender, Label labelCount)
        {
            try
            {
                sender.Rows.Clear();

                command = new MySqlCommand("SELECT * FROM patients WHERE(Patient_Id, History) IN (SELECT Patient_Id, MAX(History) FROM patients GROUP BY Patient_Id)", dbConnect);
                data = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                data.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    sender.Rows.Add(row.Field<int>("Patient_Id"), //ID
                       row.Field<string>("Perso_Last_Name"), //NOM
                       row.Field<string>("Perso_First_Name"), //PRENOM
                       row.Field<DateTime>("Perso_Date_Of_Birth").ToString("dd/MM/yyyy"), //DATE DE NAISSANCE
                       row.Field<string>("Perso_Phone"), //TELEPHONE
                       row.Field<string>("Perso_Email"), //EMAIL
                       row.Field<string>("Perso_City"), //VILLE
                       row.Field<string>("Perso_Address")); //ADRESSE

                    sender.Tag = Convert.ToString(row.Field<int>("Patient_Id")); //ADD TAG ID
                }

                labelCount.Text = "Total: " + dt.Rows.Count.ToString(); //TOTAL COUNT
            }
            catch { sender.Rows.Clear(); }
        }

        public static void RefreshAllConsultations(DataGridView sender, int Id, Label labelCount, Label labelPriceCount)
        {
            try
            {
                double prixCount = 0;

                sender.Rows.Clear();

                command = new MySqlCommand("select * from consultation where Patient_Id=@Patient_Id", dbConnect);
                command.Parameters.AddWithValue("@Patient_Id", Id);

                data = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                data.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    sender.Rows.Add(row.Field<int>("Consultation_Id"),
                       row.Field<DateTime>("Consultation_Date").ToString("dd/MM/yyyy"),
                       row.Field<double>("Price"),
                       row.Field<string>("Payment_Means"),
                       row.Field<string>("Consultation_Comment"),
                       row.Field<string>("Food_plan_Change"),
                       row.Field<string>("Giving_Document"),
                       row.Field<string>("Solicitation"),
                       row.Field<string>("Weekly_Goals"));

                    prixCount += +row.Field<double>("Price");

                    sender.Tag = Convert.ToString(row.Field<int>("Patient_Id")); //ADD TAG ID
                }

                labelCount.Text = "Total: " + dt.Rows.Count.ToString(); //TOTAL COUNT

                labelPriceCount.Text = "Prix Total: " + prixCount + "€"; //TOTAL PRIX COUNT

            }
            catch { }
        }

        public static void RefreshAllMensurations(DataGridView sender, int Id, Label labelCount, Label labelInfoIfZero)
        {
            try
            {
                sender.Rows.Clear();

                command = new MySqlCommand("select * from consultation where Patient_Id=@Patient_Id", dbConnect);
                command.Parameters.AddWithValue("@Patient_Id", Id);

                data = new MySqlDataAdapter(command);
                DataTable dt = new DataTable();
                data.Fill(dt);

                foreach (DataRow row in dt.Rows)
                {
                    sender.Rows.Add(row.Field<int>("Consultation_Id"),
                       row.Field<DateTime>("Consultation_Date").ToString("dd/MM/yyyy"),
                       row.Field<double>("Poids"),
                       row.Field<double>("Taille"),
                       row.Field<double>("Ventre"),
                       row.Field<double>("Hanche"),
                       row.Field<double>("Cuisse"),
                       row.Field<double>("Bras"),
                       row.Field<double>("IMC"),
                       row.Field<double>("Muscle"),
                       row.Field<double>("Hydratation"),
                       row.Field<double>("Graisse_Viscerale"),
                       row.Field<double>("Graisse_Sous_Cutanee"));

                    sender.Tag = Convert.ToString(row.Field<int>("Patient_Id")); //ADD TAG ID
                }

                labelCount.Text = "Total: " + dt.Rows.Count.ToString(); //TOTAL COUNT

                if (dt.Rows.Count == 0)
                {
                    labelInfoIfZero.Visible = true;
                }
                else
                {
                    labelInfoIfZero.Visible = false;
                }

            }
            catch { }
        }

        public static void DeleteConsultation(object selectedConsultationId)
        {
            command = new MySqlCommand("DELETE FROM consultation WHERE Consultation_Id = @Consultation_Id", dbConnect);
            command.Parameters.AddWithValue("@Consultation_Id", selectedConsultationId);
            command.ExecuteNonQuery();
        }

        public static void DeletePatient(object selectedId)
        {
            command = new MySqlCommand("DELETE FROM patients WHERE Patient_Id = @Patient_Id", dbConnect);
            command.Parameters.AddWithValue("@Patient_Id", selectedId);
            command.ExecuteNonQuery();

            command = new MySqlCommand("DELETE FROM consultation WHERE Patient_Id = @Patient_Id", dbConnect);
            command.Parameters.AddWithValue("@Patient_Id", selectedId);
            command.ExecuteNonQuery();
        }

        public static void UpdateConsultations(object OlderConsultationId,
            object NewConsultationId, object ConsultationDate, object Price,
            object Payment_Means, object Consultation_Comment, object Food_plan_Change,
            object Giving_Document, object Solicitation, object Weekly_Goals, int Id)
        {
            //try
            //{
                command = new MySqlCommand("update consultation set " +
"Consultation_Id='" + Convert.ToInt32(NewConsultationId) +
"', Consultation_Date='" + Convert.ToDateTime(ConsultationDate).ToString("yyyy/MM/dd") +
"', Price='" + Convert.ToDouble(Price) + "', Payment_Means='" + Payment_Means +
"', Consultation_Comment='" + @Consultation_Comment + "', Food_plan_Change='" + @Food_plan_Change +
"', Giving_Document='" + @Giving_Document + "', Solicitation='" + @Solicitation +
"', Weekly_Goals='" + @Weekly_Goals + "' where Patient_Id='" + Convert.ToInt32(Id) + "'" +
                                                      "and Consultation_Id='" + @OlderConsultationId + "'", dbConnect);

                command.ExecuteScalar();
            //}
            //catch
            //{
            //    MessageBox.Show("Une consultation du même genre est déjà existante merci de changer vos informations", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
        }

        public static void UpdateMensurations(object OlderConsultationId,
            object NewConsultationId, object ConsultationDate, object Poids,
            object Taille, object Ventre, object Hanche,
            object Cuisse, object Bras, object IMC, object Muscle, object Hydratation, object Graisse_Viscerale, object Graisse_Sous_Cutanee, int Id)
        {
            try
            {
                command = new MySqlCommand("update consultation set " +
"Consultation_Id='" + Convert.ToInt32(NewConsultationId) +
"', Consultation_Date='" + Convert.ToDateTime(ConsultationDate).ToString("yyyy/MM/dd") +
"', Poids='" + Poids + "', Taille='" + Taille +
"', Ventre='" + Ventre + "', Hanche='" + Hanche +
"', Cuisse='" + Cuisse + "', Bras='" + Bras +
"', IMC='" + IMC + "', Muscle='" + Muscle + "', Hydratation='" + Hydratation + "', Graisse_Viscerale='" + Graisse_Viscerale + "', Graisse_Sous_Cutanee='" + Graisse_Sous_Cutanee + "' where Patient_Id='" + Convert.ToInt32(Id) + "'" +
                                                      "and Consultation_Id='" + @OlderConsultationId + "'", dbConnect);

                command.ExecuteScalar();
            }
            catch
            {
                MessageBox.Show("Une consultation du même genre est déjà existante merci de changer vos informations", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static void UpdatePatient(Label Patient_Id, TextBox Last_Name, TextBox First_Name, ComboBox Gender,
DateTimePicker Naissance, Label Age, TextBox Place_Of_Birth, TextBox Height, TextBox Phone, TextBox Email,
TextBox Address, TextBox Postal_Address, TextBox City, TextBox Job, TextBox Doctor, TextBox Referencing,
TextBox past, TextBox present, TextBox futur, TextBox pTel, TextBox Historique_poids,
TextBox Habitude_alim, TextBox Horaire_travail, TextBox ATCD_Fam, TextBox ATCD_Perso, TextBox Traitement,
TextBox Hormone, TextBox Diurese, TextBox Probleme_digest, TextBox Probleme_circu, TextBox Poids,
TextBox Poids_min, TextBox Poids_max, TextBox Poids_pal, TextBox Poids_obj, TextBox Gly,
TextBox Ch, TextBox Tsh, TextBox Acide_urique, TextBox Tg, TextBox Autre,
TextBox Aller, TextBox Intol, TextBox Tabac, TextBox Tca, TextBox Activite_phys,
TextBox Sommeil, TextBox Grignotag, CheckBox C_Eau_Plate, TextBox Eau_Plate, CheckBox C_Eau_Gaz, TextBox Eau_Gaz, CheckBox C_Cafe, TextBox Cafe,
CheckBox C_The_inf, TextBox The_inf, CheckBox C_Alcool, TextBox Alcool, CheckBox C_Boisson_suc, TextBox Boisson_suc, TextBox Aversion, TextBox Alim_fetish, TextBox Petit_dej,
TextBox Collation_matin, TextBox dejeuner, TextBox collation_apres_midi, TextBox diner, TextBox collation_de_nuit)
        {
            //UPDATE

            if (!C_Eau_Plate.Checked) Eau_Plate.Text = "";
            if (!C_Eau_Gaz.Checked) Eau_Gaz.Text = "";
            if (!C_Cafe.Checked) Cafe.Text = "";
            if (!C_The_inf.Checked) The_inf.Text = "";
            if (!C_Alcool.Checked) Alcool.Text = "";
            if (!C_Boisson_suc.Checked) Boisson_suc.Text = "";

            command = new MySqlCommand("update patients set " +
    "Perso_Last_Name='" + @Last_Name.Text + "', Perso_First_Name='" + @First_Name.Text + "', Perso_Gender='" + Gender.Text + "', " +
    "Perso_Date_Of_Birth='" + Naissance.Value.ToString("yyyy/MM/dd") + "', Perso_Age='" + Age.Text + "', Perso_Place_Of_Birth='" + Place_Of_Birth.Text + "', Perso_Height='" + Height.Text + "', " +
    "Perso_Phone='" + Phone.Text + "', Perso_Email='" + Email.Text + "', Perso_Address='" + Address.Text + "', Perso_Postal_Address='" + Postal_Address.Text + "', Perso_City='" + City.Text + "', " +
    "Perso_Job='" + Job.Text + "', Perso_Doctor='" + Doctor.Text + "', Perso_Referencing='" + Referencing.Text + "', Need_Past_Need='" + past.Text + "', Need_Present_Need='" + present.Text + "', " +
    "Need_Futur_Need='" + futur.Text + "', Habit_Call_Info='" + pTel.Text + "', Habit_Weight_History='" + Historique_poids.Text + "', Habit_Food_Habit='" + Habitude_alim.Text + "', " +
    "Habit_Work_Schedule='" + Horaire_travail.Text + "', Patho_Familly_ATCD='" + ATCD_Fam.Text + "', Patho_PERSONAL_ATCD='" + ATCD_Perso.Text + "', Patho_Treatment='" + Traitement.Text + "', " +
    "Physio_Hormonal='" + Hormone.Text + "', Physio_Urine_Color='" + Diurese.Text + "', Physio_Digestion_problem='" + Probleme_digest.Text + "', " +
    "Physio_Circulation_problems='" + Probleme_circu.Text + "', Weight_Weight='" + Poids.Text + "', Weight_Min_Weight='" + Poids_min.Text + "', Weight_Max_Weight='" + Poids_max.Text + "', " +
    "Weight_Weight_Balance='" + Poids_pal.Text + "', Weight_Objective_Weight='" + Poids_obj.Text + "', Bio_Glycemie='" + Gly.Text + "', Bio_Ch='" + Ch.Text + "', Bio_Tsh='" + Tsh.Text + "', " +
    "Bio_Acide_Urique='" + Acide_urique.Text + "', Bio_Tg='" + Tg.Text + "', Bio_Other='" + Autre.Text + "', Allergy_Allergy='" + Aller.Text + "', Allergy_Intolerance='" + Intol.Text + "', " +
    "Psycho_Tabac='" + Tabac.Text + "', Psycho_Tca='" + Tca.Text + "', Psycho_Activity='" + Activite_phys.Text + "', Psycho_Sleep='" + Sommeil.Text + "', Psycho_Snacking='" + Grignotag.Text + "', " +
    "Hydratation_Water='" + Eau_Plate.Text + "', Hydratation_Gaz_Water='" + Eau_Gaz.Text + "', Hydratation_Coffee='" + Cafe.Text + "', " +
    "Hydratation_Infusion_Thea='" + The_inf.Text + "', Hydratation_Alcohol='" + Alcool.Text + "', Hydratation_Sweet_Drink='" + Boisson_suc.Text + "', Aversion_Aversion='" + Aversion.Text + "', " +
    "Aversion_Fetish_Food='" + Alim_fetish.Text + "', 24H_Breakfast='" + Petit_dej.Text + "', 24H_Morning_Snack='" + Collation_matin.Text + "', 24H_Lunch='" + dejeuner.Text + "', " +
    "24H_Afternoon_Snack='" + collation_apres_midi.Text + "', 24H_Diner='" + diner.Text + "', 24H_Night_Snack='" + collation_de_nuit.Text + "' where Patient_Id='" + Convert.ToInt32(Patient_Id.Text) + "'" +
                                                                  "and History='" + Convert.ToDateTime(DateTime.Now).ToString("yyyy/MM/dd") + "'", dbConnect);

            command.ExecuteScalar();
        }

        public static void ReplaceNewInformationsPatient(Label Patient_Id, ComboBox History, TextBox Last_Name, TextBox First_Name, ComboBox Gender,
    DateTimePicker Naissance, Label Age, TextBox Place_Of_Birth, TextBox Height, TextBox Phone, TextBox Email,
    TextBox Address, TextBox Postal_Address, TextBox City, TextBox Job, TextBox Doctor, TextBox Referencing,
    TextBox past, TextBox present, TextBox futur, TextBox pTel, TextBox Historique_poids,
    TextBox Habitude_alim, TextBox Horaire_travail, TextBox ATCD_Fam, TextBox ATCD_Perso, TextBox Traitement,
    TextBox Hormone, TextBox Diurese, TextBox Probleme_digest, TextBox Probleme_circu, TextBox Poids,
    TextBox Poids_min, TextBox Poids_max, TextBox Poids_pal, TextBox Poids_obj, TextBox Gly,
    TextBox Ch, TextBox Tsh, TextBox Acide_urique, TextBox Tg, TextBox Autre,
    TextBox Aller, TextBox Intol, TextBox Tabac, TextBox Tca, TextBox Activite_phys,
    TextBox Sommeil, TextBox Grignotag, CheckBox C_Eau_Plate, TextBox Eau_Plate, CheckBox C_Eau_Gaz, TextBox Eau_Gaz, CheckBox C_Cafe, TextBox Cafe,
    CheckBox C_The_inf, TextBox The_inf, CheckBox C_Alcool, TextBox Alcool, CheckBox C_Boisson_suc, TextBox Boisson_suc, TextBox Aversion, TextBox Alim_fetish, TextBox Petit_dej,
    TextBox Collation_matin, TextBox dejeuner, TextBox collation_apres_midi, TextBox diner, TextBox collation_de_nuit)
        {
            long unixTime = ((DateTimeOffset)Convert.ToDateTime(Convert.ToDateTime(History.Text).ToString("yyyy/MM/dd"))).ToUnixTimeSeconds();
            long current = ((DateTimeOffset)Convert.ToDateTime(Convert.ToDateTime(DateTime.Now).ToString("yyyy/MM/dd"))).ToUnixTimeSeconds();
            if (unixTime == current)
            {
                //UPDATE

                UpdatePatient(Patient_Id, Last_Name, First_Name, Gender,
    Naissance, Age, Place_Of_Birth, Height, Phone, Email,
    Address, Postal_Address, City, Job, Doctor, Referencing,
     past, present, futur, pTel, Historique_poids,
     Habitude_alim, Horaire_travail, ATCD_Fam, ATCD_Perso, Traitement,
     Hormone, Diurese, Probleme_digest, Probleme_circu, Poids,
     Poids_min, Poids_max, Poids_pal, Poids_obj, Gly,
     Ch, Tsh, Acide_urique, Tg, Autre,
     Aller, Intol, Tabac, Tca, Activite_phys,
     Sommeil, Grignotag, C_Eau_Plate, Eau_Plate, C_Eau_Gaz, Eau_Gaz, C_Cafe, Cafe,
     C_The_inf, The_inf, C_Alcool, Alcool, C_Boisson_suc, Boisson_suc, Aversion, Alim_fetish, Petit_dej,
     Collation_matin, dejeuner, collation_apres_midi, diner, collation_de_nuit);
            }
            else //NEW
            {
                DATABASE.AddNewPatient(Patient_Id, History, Last_Name, First_Name, Gender,
Naissance, Age, Place_Of_Birth, Height, Phone, Email,
Address, Postal_Address, City, Job, Doctor, Referencing,
 past, present, futur, pTel, Historique_poids,
 Habitude_alim, Horaire_travail, ATCD_Fam, ATCD_Perso, Traitement,
 Hormone, Diurese, Probleme_digest, Probleme_circu, Poids,
 Poids_min, Poids_max, Poids_pal, Poids_obj, Gly,
 Ch, Tsh, Acide_urique, Tg, Autre,
 Aller, Intol, Tabac, Tca, Activite_phys,
 Sommeil, Grignotag, C_Eau_Plate, Eau_Plate, C_Eau_Gaz, Eau_Gaz, C_Cafe, Cafe,
 C_The_inf, The_inf, C_Alcool, Alcool, C_Boisson_suc, Boisson_suc, Aversion, Alim_fetish, Petit_dej,
 Collation_matin, dejeuner, collation_apres_midi, diner, collation_de_nuit);
            }
        }

        public static void AddNewPatient(Label Patient_Id, ComboBox History, TextBox Last_Name, TextBox First_Name, ComboBox Gender,
                    DateTimePicker Naissance, Label Age, TextBox Place_Of_Birth, TextBox Height, TextBox Phone, TextBox Email,
                    TextBox Address, TextBox Postal_Address, TextBox City, TextBox Job, TextBox Doctor, TextBox Referencing,
                    TextBox past, TextBox present, TextBox futur, TextBox pTel, TextBox Historique_poids,
                    TextBox Habitude_alim, TextBox Horaire_travail, TextBox ATCD_Fam, TextBox ATCD_Perso, TextBox Traitement,
                    TextBox Hormone, TextBox Diurese, TextBox Probleme_digest, TextBox Probleme_circu, TextBox Poids,
                    TextBox Poids_min, TextBox Poids_max, TextBox Poids_pal, TextBox Poids_obj, TextBox Gly,
                    TextBox Ch, TextBox Tsh, TextBox Acide_urique, TextBox Tg, TextBox Autre,
                    TextBox Aller, TextBox Intol, TextBox Tabac, TextBox Tca, TextBox Activite_phys,
                    TextBox Sommeil, TextBox Grignotag, CheckBox C_Eau_Plate, TextBox Eau_Plate, CheckBox C_Eau_Gaz, TextBox Eau_Gaz, CheckBox C_Cafe, TextBox Cafe,
                    CheckBox C_The_inf, TextBox The_inf, CheckBox C_Alcool, TextBox Alcool, CheckBox C_Boisson_suc, TextBox Boisson_suc, TextBox Aversion, TextBox Alim_fetish, TextBox Petit_dej,
                    TextBox Collation_matin, TextBox dejeuner, TextBox collation_apres_midi, TextBox diner, TextBox collation_de_nuit)
        {
            if (!C_Eau_Plate.Checked) { Eau_Plate.Text = ""; }
            if (!C_Eau_Gaz.Checked) Eau_Gaz.Text = "";
            if (!C_Cafe.Checked) Cafe.Text = "";
            if (!C_The_inf.Checked) The_inf.Text = "";
            if (!C_Alcool.Checked) Alcool.Text = "";
            if (!C_Boisson_suc.Checked) Boisson_suc.Text = "";

            command = new MySqlCommand("insert into Patients " +
                "(Patient_Id, History, Perso_Last_Name, Perso_First_Name, Perso_Gender," +
                " Perso_Date_Of_Birth, Perso_Age, Perso_Place_Of_Birth, Perso_Height," +
                " Perso_Phone, Perso_Email, Perso_Address, Perso_Postal_Address, Perso_City," +
                " Perso_Job, Perso_Doctor, Perso_Referencing, Need_Past_Need, Need_Present_Need," +
                " Need_Futur_Need, Habit_Call_Info, Habit_Weight_History, Habit_Food_Habit," +
                " Habit_Work_Schedule, Patho_Familly_ATCD, Patho_PERSONAL_ATCD, Patho_Treatment," +
                " Physio_Hormonal, Physio_Urine_Color, Physio_Digestion_problem," +
                " Physio_Circulation_problems, Weight_Weight, Weight_Min_Weight, Weight_Max_Weight," +
                " Weight_Weight_Balance, Weight_Objective_Weight, Bio_Glycemie, Bio_Ch, Bio_Tsh," +
                " Bio_Acide_Urique, Bio_Tg, Bio_Other, Allergy_Allergy, Allergy_Intolerance," +
                " Psycho_Tabac, Psycho_Tca, Psycho_Activity, Psycho_Sleep, Psycho_Snacking," +
                " Hydratation_Water, Hydratation_Gaz_Water, Hydratation_Coffee," +
                " Hydratation_Infusion_Thea, Hydratation_Alcohol, Hydratation_Sweet_Drink, Aversion_Aversion," +
                " Aversion_Fetish_Food, 24H_Breakfast, 24H_Morning_Snack, 24H_Lunch," +
                " 24H_Afternoon_Snack, 24H_Diner, 24H_Night_Snack) " +
                 "values " +
                "(@Patient_Id, @History, @Perso_Last_Name, @Perso_First_Name, @Perso_Gender," +
                " @Perso_Date_Of_Birth, @Perso_Age, @Perso_Place_Of_Birth, @Perso_Height," +
                " @Perso_Phone, @Perso_Email, @Perso_Address, @Perso_Postal_Address, @Perso_City," +
                " @Perso_Job, @Perso_Doctor, @Perso_Referencing, @Need_Past_Need, @Need_Present_Need," +
                " @Need_Futur_Need, @Habit_Call_Info, @Habit_Weight_History, @Habit_Food_Habit," +
                " @Habit_Work_Schedule, @Patho_Familly_ATCD, @Patho_PERSONAL_ATCD, @Patho_Treatment," +
                " @Physio_Hormonal, @Physio_Urine_Color, @Physio_Digestion_problem," +
                " @Physio_Circulation_problems, @Weight_Weight, @Weight_Min_Weight, @Weight_Max_Weight," +
                " @Weight_Weight_Balance, @Weight_Objective_Weight, @Bio_Glycemie, @Bio_Ch, @Bio_Tsh," +
                " @Bio_Acide_Urique, @Bio_Tg, @Bio_Other, @Allergy_Allergy, @Allergy_Intolerance," +
                " @Psycho_Tabac, @Psycho_Tca, @Psycho_Activity, @Psycho_Sleep, @Psycho_Snacking," +
                " @Hydratation_Water, @Hydratation_Gaz_Water, @Hydratation_Coffee," +
                " @Hydratation_Infusion_Thea, @Hydratation_Alcohol, @Hydratation_Sweet_Drink," +
                " @Aversion_Aversion," +
                " @Aversion_Fetish_Food, @24H_Breakfast, @24H_Morning_Snack, @24H_Lunch," +
                " @24H_Afternoon_Snack, @24H_Diner, @24H_Night_Snack)", dbConnect);

            command.Parameters.AddWithValue("@Patient_Id", Convert.ToInt32(Patient_Id.Text));
            command.Parameters.AddWithValue("@History", DateTime.Now.Date);
            command.Parameters.AddWithValue("@Perso_Last_Name", Last_Name.Text);
            command.Parameters.AddWithValue("@Perso_First_Name", First_Name.Text);
            command.Parameters.AddWithValue("@Perso_Gender", Gender.Text);
            command.Parameters.AddWithValue("@Perso_Date_Of_Birth", Naissance.Value);
            command.Parameters.AddWithValue("@Perso_Age", Age.Text);
            command.Parameters.AddWithValue("@Perso_Place_Of_Birth", Place_Of_Birth.Text);
            command.Parameters.AddWithValue("@Perso_Height", Height.Text);
            command.Parameters.AddWithValue("@Perso_Phone", Phone.Text);
            command.Parameters.AddWithValue("@Perso_Email", Email.Text);
            command.Parameters.AddWithValue("@Perso_Address", Address.Text);
            command.Parameters.AddWithValue("@Perso_Postal_Address", Postal_Address.Text);
            command.Parameters.AddWithValue("@Perso_City", City.Text);
            command.Parameters.AddWithValue("@Perso_Job", Job.Text);
            command.Parameters.AddWithValue("@Perso_Doctor", Doctor.Text);
            command.Parameters.AddWithValue("@Perso_Referencing", Referencing.Text);

            command.Parameters.AddWithValue("@Hydratation_Water", Eau_Gaz.Text);
            command.Parameters.AddWithValue("@Hydratation_Gaz_Water", Eau_Gaz.Text);
            command.Parameters.AddWithValue("@Hydratation_Coffee", Cafe.Text);
            command.Parameters.AddWithValue("@Hydratation_Infusion_Thea", The_inf.Text);
            command.Parameters.AddWithValue("@Hydratation_Alcohol", Alcool.Text);
            command.Parameters.AddWithValue("@Hydratation_Sweet_Drink", Boisson_suc.Text);

            command.Parameters.AddWithValue("@Need_Past_Need", past.Text);
            command.Parameters.AddWithValue("@Need_Present_Need", present.Text);
            command.Parameters.AddWithValue("@Need_Futur_Need", futur.Text);
            command.Parameters.AddWithValue("@Habit_Call_Info", pTel.Text);
            command.Parameters.AddWithValue("@Habit_Weight_History", Historique_poids.Text);
            command.Parameters.AddWithValue("@Habit_Food_Habit", Habitude_alim.Text);
            command.Parameters.AddWithValue("@Habit_Work_Schedule", Horaire_travail.Text);
            command.Parameters.AddWithValue("@Patho_Familly_ATCD", ATCD_Fam.Text);
            command.Parameters.AddWithValue("@Patho_PERSONAL_ATCD", ATCD_Perso.Text);
            command.Parameters.AddWithValue("@Patho_Treatment", Traitement.Text);
            command.Parameters.AddWithValue("@Physio_Hormonal", Hormone.Text);
            command.Parameters.AddWithValue("@Physio_Urine_Color", Diurese.Text);
            command.Parameters.AddWithValue("@Physio_Digestion_problem", Probleme_digest.Text);
            command.Parameters.AddWithValue("@Physio_Circulation_problems", Probleme_circu.Text);
            command.Parameters.AddWithValue("@Weight_Weight", Poids.Text);
            command.Parameters.AddWithValue("@Weight_Min_Weight", Poids_min.Text);
            command.Parameters.AddWithValue("@Weight_Max_Weight", Poids_max.Text);
            command.Parameters.AddWithValue("@Weight_Weight_Balance", Poids_pal.Text);
            command.Parameters.AddWithValue("@Weight_Objective_Weight", Poids_obj.Text);
            command.Parameters.AddWithValue("@Bio_Glycemie", Gly.Text);
            command.Parameters.AddWithValue("@Bio_Ch", Ch.Text);
            command.Parameters.AddWithValue("@Bio_Tsh", Tsh.Text);
            command.Parameters.AddWithValue("@Bio_Acide_Urique", Acide_urique.Text);
            command.Parameters.AddWithValue("@Bio_Tg", Tg.Text);
            command.Parameters.AddWithValue("@Bio_Other", Autre.Text);
            command.Parameters.AddWithValue("@Allergy_Allergy", Aller.Text);
            command.Parameters.AddWithValue("@Allergy_Intolerance", Intol.Text);
            command.Parameters.AddWithValue("@Psycho_Tabac", Tabac.Text);
            command.Parameters.AddWithValue("@Psycho_Tca", Tca.Text);
            command.Parameters.AddWithValue("@Psycho_Activity", Activite_phys.Text);
            command.Parameters.AddWithValue("@Psycho_Sleep", Sommeil.Text);
            command.Parameters.AddWithValue("@Psycho_Snacking", Grignotag.Text);

            command.Parameters.AddWithValue("@Aversion_Aversion", Aversion.Text);
            command.Parameters.AddWithValue("@Aversion_Fetish_Food", Alim_fetish.Text);
            command.Parameters.AddWithValue("@24H_Breakfast", Petit_dej.Text);
            command.Parameters.AddWithValue("@24H_Morning_Snack", Collation_matin.Text);
            command.Parameters.AddWithValue("@24H_Lunch", dejeuner.Text);
            command.Parameters.AddWithValue("@24H_Afternoon_Snack", collation_apres_midi.Text);
            command.Parameters.AddWithValue("@24H_Diner", diner.Text);
            command.Parameters.AddWithValue("@24H_Night_Snack", collation_de_nuit.Text);

            try
            {
                command.ExecuteNonQuery();
            }
            catch
            {
                UpdatePatient(Patient_Id, Last_Name, First_Name, Gender,
Naissance, Age, Place_Of_Birth, Height, Phone, Email,
Address, Postal_Address, City, Job, Doctor, Referencing,
past, present, futur, pTel, Historique_poids,
Habitude_alim, Horaire_travail, ATCD_Fam, ATCD_Perso, Traitement,
Hormone, Diurese, Probleme_digest, Probleme_circu, Poids,
Poids_min, Poids_max, Poids_pal, Poids_obj, Gly,
Ch, Tsh, Acide_urique, Tg, Autre,
     Aller, Intol, Tabac, Tca, Activite_phys,
     Sommeil, Grignotag, C_Eau_Plate, Eau_Plate, C_Eau_Gaz, Eau_Gaz, C_Cafe, Cafe,
     C_The_inf, The_inf, C_Alcool, Alcool, C_Boisson_suc, Boisson_suc, Aversion, Alim_fetish, Petit_dej,
     Collation_matin, dejeuner, collation_apres_midi, diner, collation_de_nuit);
            }
        }
    }
}
