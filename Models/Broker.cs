using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Microsoft.Extensions.Options;

namespace Supermarket.Models
{
    public class Broker
    {
        public readonly string _connectionString = "server=localhost;database=Supermarket;Trusted_Connection=True;";
        private readonly SqlDataAdapter adp = new SqlDataAdapter();

        private static Broker _instance;

        public Broker()
        {
        }

        public static Broker Instance()

        {
            if (_instance == null)
            {
                _instance = new Broker();
            }
            return _instance;
        }

        public DataSet Run(string query, string reff)
        {
            SqlConnection con = new SqlConnection(_connectionString);
            con.Open();
            SqlCommand comm = new SqlCommand(query, con);
            DataSet ds = new DataSet();
            try
            {
                adp.SelectCommand = comm;
                adp.Fill(ds, reff);
            }
            catch (Exception)
            {
                con.Close();
            }

            con.Close();
            return ds;
        }
    }
}