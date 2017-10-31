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
    public void ClearAll_ClearsAllCateogryAssociationsFromDatabase_0()
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

  }
}
