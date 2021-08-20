using BL.Repository;
using CandidateDL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace BL.Implementation
{
    public class CandidateImplementation : ICandidateRepository
    {
        private readonly string _connectionString;
        public CandidateImplementation(IConfiguration configuration)
        {
            _connectionString = configuration.GetSection("ConnectionString:dbConnection").Value;
        }
        public bool CreateCandidate(Candidate model)
        {
            using (SqlConnection con=new SqlConnection(_connectionString))
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                string commandText = @"usp_InsertData";
                SqlCommand cmd = new SqlCommand(commandText, con);
                cmd.Parameters.AddWithValue("@CandidateName", model.CandidateName);
                cmd.Parameters.AddWithValue("@FatherName", model.FatherName);
                cmd.Parameters.AddWithValue("@EmailId", model.EmailId);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                var response = cmd.ExecuteNonQuery();
                return response > 0;

            }
        }
        public bool DeleteCandidate(int Id)
        {
            string deleteCommand = @"Delete from Candidate where Id=@Id";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand(deleteCommand, con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", Id);

                var response = cmd.ExecuteNonQuery();
                return response > 0;
            }
        }

        public Candidate GetCandidateById(int Id)
        {
            var model = new Candidate();
            string selectCommand = @"Select Id,CandidateName,FatherName,EmailId from Candidate where Id=@Id";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand(selectCommand, con);
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@Id",Id);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.Id = Convert.ToInt32(reader["Id"]);
                        model.CandidateName = (reader["CandidateName"]).ToString();
                        model.FatherName = (reader["FatherName"]).ToString();
                        model.EmailId = (reader["EmailId"]).ToString();
                    }
                }
                return model;
            }
        }
        public IEnumerable<Candidate> GetCandidates()
        {
            var models = new List<Candidate>();
            string commandText = @"usp_GetCandidate";
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();
                SqlCommand cmd = new SqlCommand(commandText, con);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                using(var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var model = new Candidate();
                        model.Id = Convert.ToInt32(reader["Id"]);
                        model.CandidateName = (reader["CandidateName"]).ToString();
                        model.FatherName = (reader["FatherName"]).ToString();
                        model.EmailId = (reader["EmailId"]).ToString();

                        models.Add(model);
                    }
                }
            }
            return models;
        }
        public bool UpdateCandidate(Candidate model)
        {
            string commandText = @"Update Candidate set CandidateName=@CandidateName, 
                                FatherName=@FatherName, EmailId=@EmailId  where Id=@Id";

            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                if (con.State == System.Data.ConnectionState.Closed)
                    con.Open();

                SqlCommand cmd = new SqlCommand(commandText, con);
                cmd.Parameters.AddWithValue("@CandidateName", model.CandidateName);
                cmd.Parameters.AddWithValue("@FatherName", model.FatherName);
                cmd.Parameters.AddWithValue("@EmailId", model.EmailId);
                cmd.Parameters.AddWithValue("@Id", model.Id);
                cmd.CommandType = System.Data.CommandType.Text;
                var response = cmd.ExecuteNonQuery();
                return response > 0;
            }
        }
    }
}
