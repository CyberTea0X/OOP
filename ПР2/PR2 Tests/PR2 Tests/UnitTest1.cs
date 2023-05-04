using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PR2_Tests
{
    using Task1;
    using Задание1;
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestFindGCDEuclid_TwoNumbers()
        {
            // Arrange
            int a = 12;
            int b = 18;
            int expected = 6;

            // Act
            int actual = Task1.FindGCDEuclid(a, b);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestFindGCDEuclid_MultipleNumbers()
        {
            // Arrange
            int[] numbers = { 12, 18, 24 };
            int expected = 6;

            // Act
            int actual = Task2.FindGCDEuclid(numbers);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestFindGCDEuclid_StringNumbers()
        {
            // Arrange
            string[] str_numbers = { "12", "18", "24" };
            int expected = 6;

            // Act
            int actual = Task2.FindGCDEuclid(str_numbers);

            // Assert
            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestFindGCDStein_TwoNumbers()
        {
            // Arrange
            int a = 12;
            int b = 18;
            int expected = 6;

            // Act
            int actual = Task3.FindGCDStein(a, b);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestFindGCDStein_MultipleNumbers()
        {
            // Arrange
            int[] numbers = { 12, 18, 24 };
            int expected = 6;

            // Act
            int actual = Task3.FindGCDStein(numbers);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestFindGCDStein_StringNumbers()
        {
            // Arrange
            string[] str_numbers = { "12", "18", "24" };
            int expected = 6;

            // Act
            int actual = Task3.FindGCDStein(str_numbers);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
