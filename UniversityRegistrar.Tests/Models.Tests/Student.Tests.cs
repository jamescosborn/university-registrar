using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniversityRegistrar.Models;

namespace UniversityRegistrar.Models.Tests
{
  [TestClass]
  public class StudentTests : IDisposable
  {
    public StudentTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=university_registrar_test;";
    }

    public void Dispose()
    {
      Student.ClearAll();
    }

    [TestMethod]
    public void ClearAll_ClearsAllStudentsFromDatabase_0()
    {
      //Arrange
      List<Student> testList = new List<Student>();
      Student studentA = new Student("Alex","test");
      Student studentB = new Student("Bob","test");
      Student studentC = new Student("Charlie","test");
      testList.Add(studentA);
      testList.Add(studentB);
      testList.Add(studentC);
      studentA.Save();
      studentB.Save();
      studentC.Save();

      //Act
      Student.ClearAll();
      List<Student> resultList = Student.GetAll();
      //Test
      //Console.WriteLine(resultList.Count+" : "+testList.Count);
      Assert.AreEqual(true,resultList.Count==0);
    }

    [TestMethod]
    public void Save_SaveStudent()
    {
      //Arrange
      Student testStudent = new Student("test-name","test-date");
      //Act
      testStudent.Save();
      //Test
      // foreach(Student s in Student.GetAll())
      // {
      //   Console.WriteLine("* Student ID: "+s.Id+" *");
      //   Console.WriteLine("* Student Name: "+s.Name+" *");
      //   Console.WriteLine("* Student Enrollment Date: "+s.EnrollmentDate+" *");
      // }
      Assert.AreEqual(true,Student.GetAll().Count==1);
    }

    [TestMethod]
    public void Find_FindsStudentInDatabase_Category()
    {
      Student testStudent = new Student("Ronnie", "Nov. 1, 2004");
      testStudent.Save();

      Student foundStudent = Student.Find(1);
      // Console.WriteLine("* Student ID: "+ testStudent.Id +" *");
      Assert.AreEqual(testStudent, foundStudent);
    }

    [TestMethod]
    public void Delete_DeleteStudentInDatabase_0()
    {
      int newID = 1;
      Student testStudent = new Student("Ronnie", "Nov. 1, 2004");
      testStudent.Save();
      int numOfStudents = Student.GetAll().Count;

      Student foundStudent = Student.Find(newID);
      // Console.WriteLine("* Student ID: "+ testStudent.Id +" *");
      Student.Delete(newID);
      Assert.AreEqual(true, numOfStudents==Student.GetAll().Count);
    }


  }
}
