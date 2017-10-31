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

      //Student.ClearAll();
      List<Student> resultList = Student.GetAll();
      Console.WriteLine(resultList.Count+" : "+testList.Count);
      CollectionAssert.AreEqual(resultList,testList);
    }
  }
}
