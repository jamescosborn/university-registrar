using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;

namespace UniversityRegistrar.Models
{
  public class Student
  {
    public string Name {get; private set;}
    public string EnrollmentDate {get; private set;}
    public int Id {get; private set;}

    public Student(string name, string enrollment, int id = 0)
    {
      Name = name;
      EnrollmentDate = enrollment;
      Id = id;
    }
    public override bool Equals(System.Object otherStudent)
    {
      if (!(otherStudent is Student))
      {
          return false;
      }
      else
      {
          Student newStudent = (Student) otherStudent;
          bool idEquality = (this.Id == newStudent.Id);
          bool nameEquality = (this.Name == newStudent.Name);
          bool enrollEquality = (this.EnrollmentDate == newStudent.EnrollmentDate);
          return (idEquality && nameEquality && enrollEquality);
      }
    }

    public override int GetHashCode()
    {
        return this.Id.GetHashCode();
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO students (name, enrollment_date) VALUES (@name, @enrollment);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = Name;
      cmd.Parameters.Add(name);

      MySqlParameter enrollment = new MySqlParameter();
      enrollment.ParameterName = "@enrollment";
      enrollment.Value = EnrollmentDate;
      cmd.Parameters.Add(enrollment);

      cmd.ExecuteNonQuery();
      Id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Student> GetAll()
    {
      List<Student> roster = new List<Student>();

      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand();
      cmd.CommandText = @"SELECT * FROM students;";
      MySqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        int studentId = rdr.GetInt32(0);
        string studentName = rdr.GetString(1);
        string studentEnrollmentDate = rdr.GetString(2);
        Student newStudent = new Student(studentName, studentEnrollmentDate, studentId);
        roster.Add(newStudent);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return roster;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM students;";
      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
