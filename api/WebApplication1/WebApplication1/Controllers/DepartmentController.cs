using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            /*
            string query = @"
                select DepartmentId as ""DepartmentId"",
                        DepartmentName as ""DepartmentName""
                from Department
            ";*/

			string query = @"
                        SELECT 
                            ID AS ""ID"",
                            ""Name"",
                            ""Surname"",
                            ""Patron"",
                            ""Faculty"",
                            ""Specialty"",
                            ""Course"",
                            ""Group"",
                            ""City"",
                            ""PostalCode"",
                            ""Street"",
                            ""Phone"",
                            ""Email""
                        FROM Students;
                        ";

			DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult(table);
        }


		[HttpPost]
		public JsonResult Post(Student student)
		{
			string query = @"
        INSERT INTO Students (
            ""Name"",
            ""Surname"",
            ""Patron"",
            ""Faculty"",
            ""Specialty"",
            ""Course"",
            ""Group"",
            ""City"",
            ""PostalCode"",
            ""Street"",
            ""Phone"",
            ""Email""
        )
        VALUES (
            @Name,
            @Surname,
            @Patron,
            @Faculty,
            @Specialty,
            @Course,
            @Group,
            @City,
            @PostalCode,
            @Street,
            @Phone,
            @Email
        );
    ";

			DataTable table = new DataTable();
			string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
			NpgsqlDataReader myReader;
			using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
			{
				myCon.Open();
				using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
				{
					myCommand.Parameters.AddWithValue("@Name", student.Name);
					myCommand.Parameters.AddWithValue("@Surname", student.Surname);
					myCommand.Parameters.AddWithValue("@Patron", student.Patron);
					myCommand.Parameters.AddWithValue("@Faculty", student.Faculty);
					myCommand.Parameters.AddWithValue("@Specialty", student.Specialty);
					myCommand.Parameters.AddWithValue("@Course", student.Course);
					myCommand.Parameters.AddWithValue("@Group", student.Group);
					myCommand.Parameters.AddWithValue("@City", student.City);
					myCommand.Parameters.AddWithValue("@PostalCode", student.PostalCode);
					myCommand.Parameters.AddWithValue("@Street", student.Street);
					myCommand.Parameters.AddWithValue("@Phone", student.Phone);
					myCommand.Parameters.AddWithValue("@Email", student.Email);

					myReader = myCommand.ExecuteReader();
					table.Load(myReader);

					myReader.Close();
					myCon.Close();
				}
			}

			return new JsonResult("Added Successfully");
		}

		[HttpPut]
        public JsonResult Put(Student dep)
        {
			/*
            string query = @"
                update Department
                set DepartmentName = @DepartmentName
                where DepartmentId=@DepartmentId 
            ";*/

			string query = @"
                        UPDATE Students
                        SET
                            ""Name"" = @Name,
                            ""Surname"" = @Surname,
                            ""Patron"" = @Patron,
                            ""Faculty"" = @Faculty,
                            ""Specialty"" = @Specialty,
                            ""Course"" = @Course,
                            ""Group"" = @Group,
                            ""City"" = @City,
                            ""PostalCode"" = @PostalCode,
                            ""Street"" = @Street,
                            ""Phone"" = @Phone,
                            ""Email"" = @Email
                        WHERE
                            ""ID"" = @ID;
                        ";

			DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@Name", dep.Name);
                    myCommand.Parameters.AddWithValue("@Surname", dep.Surname);
					myCommand.Parameters.AddWithValue("@Patron", dep.Patron);
					myCommand.Parameters.AddWithValue("@Faculty", dep.Faculty);
					myCommand.Parameters.AddWithValue("@Specialty", dep.Specialty);
					myCommand.Parameters.AddWithValue("@Course", dep.Course);
					myCommand.Parameters.AddWithValue("@Group", dep.Group);
					myCommand.Parameters.AddWithValue("@City", dep.City);
					myCommand.Parameters.AddWithValue("@PostalCode", dep.PostalCode);
					myCommand.Parameters.AddWithValue("@Street", dep.Street);
					myCommand.Parameters.AddWithValue("@Phone", dep.Phone);
					myCommand.Parameters.AddWithValue("@Email", dep.Email);
					myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Updated Successfully");
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
			/*
            string query = @"
                delete from Department
                where DepartmentId=@DepartmentId 
            ";
            */

			string query = @"
                        DELETE FROM Students
                        WHERE ID = @ID;
                        ";

			DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("EmployeeAppCon");
            NpgsqlDataReader myReader;
            using (NpgsqlConnection myCon = new NpgsqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (NpgsqlCommand myCommand = new NpgsqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ID", id);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("Deleted Successfully");
        }
    }
}
