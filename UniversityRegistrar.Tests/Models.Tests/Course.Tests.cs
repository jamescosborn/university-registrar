using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniversityRegistrar.Models;

namespace UniversityRegistrar.Models.Tests
{
  [TestClass]
  public class CourseTests : IDisposable
  {
    public CourseTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=university_registrar_test;";
    }
    public void Dispose()
    {
      Course.ClearAll();
    }
    [TestMethod]
    public void ClearAll_ClearsAllCoursesFromDatabase_0()
    {
      List<Course> testList = new List<Course>();
      Course courseA = new Course("History of Snakes", "HST100");
      Course courseB = new Course("History of Tacos", "HST101");
      Course courseC = new Course("History of History", "HST102");
      testList.Add(courseA);
      testList.Add(courseB);
      testList.Add(courseC);
      courseA.Save();
      courseB.Save();
      courseC.Save();

      Course.ClearAll();
      List<Course> resultList = Course.GetAll();

      Assert.AreEqual(true,resultList.Count==0);
    }
  }
}
