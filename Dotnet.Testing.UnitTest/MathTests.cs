using Dotnet.Testing.Web.Services;
using NUnit.Framework;

namespace Dotnet.Testing.UnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(new int[] { 12, 3 }, ExpectedResult = 15)]
        [TestCase(new int[] { 12, 2 }, ExpectedResult = 14)]
        [TestCase(new int[] { 12, 4 }, ExpectedResult = 16)]
        public int Test1(int[] args)
        {
            // Arrange
            MathService sut = new MathService();

            // Act
            return sut.Sum(args);
        }
    }
}