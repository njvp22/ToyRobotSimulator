using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Robo.UnitTests
{
    [TestClass]
    public class RoboMoveTest
    {
        private StringWriter stringWriter;
        private TextWriter originalOutput;

        [TestInitialize]
        public void TestInitialize()
        {
            // Redirect console output for verification
            stringWriter = new StringWriter();
            originalOutput = Console.Out;
            Console.SetOut(stringWriter);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Console.SetOut(originalOutput);
            stringWriter.Dispose();
        }

        // Helper method to get console output
        private string GetOutput()
        {
            return stringWriter.ToString().Trim();
        }

        [TestMethod]
        public void TestInitialState()
        {
            // Arrange
            var robo = new Robo();

            // Act
            robo.Report();

            // Assert - nothing should be output since robot isn't placed
            Assert.AreEqual("", GetOutput());
        }

        [TestMethod]
        public void TestPlaceValid()
        {
            // Arrange
            var robo = new Robo();

            // Act
            robo.Place(1, 2, Robo.Direction.NORTH);
            robo.Report();

            // Assert
            Assert.AreEqual("1,2,NORTH", GetOutput());
        }

        [TestMethod]
        public void TestPlaceInvalid()
        {
            // Arrange
            var robo = new Robo();

            // Act - Try to place outside the grid
            robo.Place(5, 5, Robo.Direction.NORTH);
            robo.Report();

            // Assert - Robot should not be placed
            Assert.AreEqual("", GetOutput());
        }

        [TestMethod]
        public void TestMoveValid()
        {
            // Arrange
            var robo = new Robo();
            robo.Place(1, 1, Robo.Direction.NORTH);

            // Act
            robo.Move();
            robo.Report();

            // Assert
            Assert.AreEqual("1,2,NORTH", GetOutput());
        }

        [TestMethod]
        public void TestMoveInvalid()
        {
            // Arrange
            var robo = new Robo();
            robo.Place(0, 0, Robo.Direction.SOUTH);

            // Act - Try to move off the grid
            robo.Move();
            robo.Report();

            // Assert - Position shouldn't change
            Assert.AreEqual("0,0,SOUTH", GetOutput());
        }

        [TestMethod]
        public void TestMoveWithoutPlacement()
        {
            // Arrange
            var robo = new Robo();

            // Act
            robo.Move();
            robo.Report();

            // Assert - Nothing should be output
            Assert.AreEqual("", GetOutput());
        }

        [TestMethod]
        public void TestLeftRotation()
        {
            // Arrange
            var robo = new Robo();
            robo.Place(1, 1, Robo.Direction.NORTH);

            // Act
            robo.Left();
            robo.Report();

            // Assert
            Assert.AreEqual("1,1,WEST", GetOutput());
        }

        [TestMethod]
        public void TestRightRotation()
        {
            // Arrange
            var robo = new Robo();
            robo.Place(1, 1, Robo.Direction.NORTH);

            // Act
            robo.Right();
            robo.Report();

            // Assert
            Assert.AreEqual("1,1,EAST", GetOutput());
        }

        [TestMethod]
        public void TestFullRotation()
        {
            // Arrange
            var robo = new Robo();
            robo.Place(1, 1, Robo.Direction.NORTH);

            // Act - Full rotation should return to original direction
            robo.Left();
            robo.Left();
            robo.Left();
            robo.Left();
            robo.Report();

            // Assert
            Assert.AreEqual("1,1,NORTH", GetOutput());
        }

        [TestMethod]
        public void TestComplexMovement()
        {
            // Arrange
            var robo = new Robo();
            robo.Place(1, 2, Robo.Direction.EAST);

            // Act
            robo.Move();
            robo.Move();
            robo.Left();
            robo.Move();
            robo.Report();

            // Assert
            Assert.AreEqual("3,3,NORTH", GetOutput());
        }

        [TestMethod]
        public void TestEdgeDetection()
        {
            // Arrange
            var robo = new Robo();
            robo.Place(4, 4, Robo.Direction.NORTH);

            // Act - Try to move beyond the north edge
            robo.Move();
            robo.Report();

            // Assert - Y should remain at 4
            Assert.AreEqual("4,4,NORTH", GetOutput());
        }

        [TestMethod]
        public void TestEdgeMovement()
        {
            // Arrange
            var robo = new Robo();
            robo.Place(0, 0, Robo.Direction.EAST);

            // Act - Move along the edge
            robo.Move();
            robo.Move();
            robo.Left();
            robo.Move();
            robo.Move();
            robo.Report();

            // Assert
            Assert.AreEqual("2,2,NORTH", GetOutput());
        }

        [TestMethod]
        public void TestIgnoreInvalidCommands()
        {
            // Arrange
            var robo = new Robo();

            // Act - Try commands before placing
            robo.Move();
            robo.Left();
            robo.Right();
            robo.Place(2, 3, Robo.Direction.WEST);
            robo.Report();

            // Assert - Only the valid commands should be processed
            Assert.AreEqual("2,3,WEST", GetOutput());
        }

        [TestMethod]
        public void TestMultiplePlacements()
        {
            // Arrange
            var robo = new Robo();
            robo.Place(1, 1, Robo.Direction.NORTH);

            // Act - Replace the robot
            robo.Move();
            robo.Place(3, 3, Robo.Direction.EAST);
            robo.Report();

            // Assert - Position should reflect the second placement
            Assert.AreEqual("3,3,EAST", GetOutput());
        }

        [TestMethod]
        public void TestInvalidPlacement()
        {
            // Arrange
            var robo = new Robo();
            robo.Place(1, 1, Robo.Direction.NORTH);

            // Act - Try invalid placement
            robo.Place(-1, 6, Robo.Direction.SOUTH);
            robo.Report();

            // Assert - Should remain at original position
            Assert.AreEqual("1,1,NORTH", GetOutput());
        }
    }


