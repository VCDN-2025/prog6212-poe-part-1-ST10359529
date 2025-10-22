using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using St10359529_POE_Prog6212.Controllers;
using St10359529_POE_Prog6212.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace St10359529_POE_Prog6212.Tests
{
    [TestClass]
    public class Test
    {
        private LecturerController _lecturerController;
        private CoordinatorController _coordinatorController;

        [TestInitialize]
        public void Setup()
        {
            // Simple instantiation without dependencies
            _lecturerController = new LecturerController(null); // Null for IWebHostEnvironment
            _coordinatorController = new CoordinatorController();
        }

        [TestMethod]
        public void SubmitClaim_ValidInput_RedirectsToTrackStatus()
        {
            // Arrange
            var claim = new Claim
            {
                Name = "John",
                Surname = "Doe",
                ContactNumber = "1234567890",
                HoursWorked = 10,
                TotalAmount = 500
            };

            // Act
            var result = _lecturerController.SubmitClaim(claim, null) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("TrackStatus", result.ActionName);
            Assert.IsTrue(ClaimRepository.Claims.Count > 0);
        }

        [TestMethod]
        public void ApproveClaim_ValidId_UpdatesStatusToApproved()
        {
            // Arrange
            var claim = new Claim { Id = 1, Status = "Pending" };
            ClaimRepository.Claims.Add(claim);

            // Act
            var result = _coordinatorController.ApproveClaim(1) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("PendingClaims", result.ActionName);
            var updatedClaim = ClaimRepository.Claims.FirstOrDefault(c => c.Id == 1); // Fixed to Claims
            Assert.AreEqual("Approved", updatedClaim.Status);
        }

        [TestMethod]
        public void RejectClaim_ValidId_UpdatesStatusToRejected()
        {
            // Arrange
            var claim = new Claim { Id = 2, Status = "Pending" };
            ClaimRepository.Claims.Add(claim);

            // Act
            var result = _coordinatorController.RejectClaim(2) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("PendingClaims", result.ActionName);
            var updatedClaim = ClaimRepository.Claims.FirstOrDefault(c => c.Id == 2);
            Assert.AreEqual("Rejected", updatedClaim.Status);
        }

        [TestMethod]
        public void DeleteClaim_ValidId_MovesToDeletedClaims()
        {
            // Arrange
            var claim = new Claim { Id = 3, Status = "Pending" };
            ClaimRepository.Claims.Add(claim);

            // Act
            var result = _coordinatorController.DeleteClaim(3) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("PendingClaims", result.ActionName);
            Assert.IsFalse(ClaimRepository.Claims.Any(c => c.Id == 3));
            Assert.IsTrue(ClaimRepository.DeletedClaims.Any(c => c.Id == 3));
        }

        [TestMethod]
        public void Login_ValidCredentials_RedirectsToPendingClaims()
        {
            // Arrange
            var controller = new CoordinatorController();
            // Without mocking, test logic path
            var result = controller.Login("1234", "1234") as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("PendingClaims", result.ActionName);
            // Session check skipped due to no mocking
        }

        [TestMethod]
        public void Login_InvalidCredentials_ReturnsViewWithError()
        {
            // Arrange
            var controller = new CoordinatorController();
            // Without mocking, test logic path
            var result = controller.Login("wrong", "wrong") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.ViewData["Error"]);
            Assert.IsTrue(result.ViewData["Error"].ToString().Contains("Invalid"));
            // Session check skipped
        }
    }
}