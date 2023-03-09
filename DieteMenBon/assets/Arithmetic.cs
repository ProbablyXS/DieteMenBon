using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace DieteMenBon.assets
{
    class Arithmetic
    {
        public static string GeneratorGUID()
        {
            Guid generateGUID = Guid.NewGuid();
            string result = generateGUID.ToString();

            DATABASE.command = new MySqlCommand("select * from Patients", DATABASE.dbConnect);
            DATABASE.data = new MySqlDataAdapter(DATABASE.command);
            DataTable dt = new DataTable();
            DATABASE.data.Fill(dt);

            foreach (DataRow row in dt.Rows)
            {
                if (row.Field<string>("Guid") != result)
                {
                    return result.ToString();
                }

                result = generateGUID.ToString();
            }

            return result.ToString();
        }

        public static string GeneratorPatient_ID()
        {
            int result = 0;

            DATABASE.command = new MySqlCommand("select * from Patients", DATABASE.dbConnect);
            DATABASE.data = new MySqlDataAdapter(DATABASE.command);
            DataTable dt = new DataTable();
            DATABASE.data.Fill(dt);

            List<string> listId = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                listId.Add(row.Field<int>("Patient_Id").ToString());

                if (!listId.Contains(result.ToString()))
                {
                    return result.ToString();
                }
                else
                {
                    result += 1;
                }
            }

            return result.ToString();
        }

        public static int GeneratorTotalConsultation()
        {
            int result = 1;

            DATABASE.command = new MySqlCommand("select * from consultation", DATABASE.dbConnect);
            DATABASE.data = new MySqlDataAdapter(DATABASE.command);
            DataTable dt = new DataTable();
            DATABASE.data.Fill(dt);

            List<string> listId = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                listId.Add(row.Field<int>("Consultation_Id").ToString());

                if (!listId.Contains(result.ToString()))
                {
                    return result;
                }
                else
                {
                    result += 1;
                }
            }

            return result;
        }
    }
}
