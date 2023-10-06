

using FluentValidation.TestHelper;
using Specialization.API.Application.Command.CreateSpecialization;
using Specialization.API.Application.Contracts.Incoming;
using Specialization.API.Application.Resources;
using Specialization.API.Application.Validator.Command;

namespace Specialization.Tests.Unit
{
    [TestFixture]
    public class SpecializationValidatorTests
    {
        private CreateSpecializationValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new CreateSpecializationValidator();
        }

        [Test]
        public void Should_Have_Error_When_Title_Is_Empty()
        {
            // Arrange
            var command = new CreateSpecializationCommand(new CreateSpecializationDTO
            {
                Title = string.Empty,
                IsActive = true,
                ServicesId = new List<long>(){1,2,3}
            });
          

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CreateSpecializationDto.Title)
                .WithErrorMessage(string.Format(Messages.NotEmptyField, "Title"));
        }

        [Test]
        public void Should_Have_Error_When_IsActive_Is_Empty()
        {
            // Arrange
            var command = new CreateSpecializationCommand(new CreateSpecializationDTO
            {
                Title = "SomeTitle", 
                IsActive = null is bool,
                ServicesId = new List<long>() { 1, 2, 3 }
            });

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CreateSpecializationDto.IsActive)
                .WithErrorMessage(string.Format(Messages.NotEmptyField, "IsActive"));
        }
        [Test]
        public void Should_Have_Error_When_ServicesId_Is_Null()
        {
            // Arrange
            var command = new CreateSpecializationCommand(new CreateSpecializationDTO
            {
                Title = "SomeTitle",
                IsActive =true,
                ServicesId = null
            });

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CreateSpecializationDto.ServicesId)
                .WithErrorMessage(string.Format(Messages.NotEmptyField, "ServicesId"));
        }

        [Test]
        public void Should_Have_Error_When_ServicesId_Is_Empty()
        {
            // Arrange
            var command = new CreateSpecializationCommand(new CreateSpecializationDTO
            {
                Title = "SomeTitle",
                IsActive = null is bool,
                ServicesId = new List<long>()
            });

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.CreateSpecializationDto.ServicesId)
                .WithErrorMessage(string.Format(Messages.NotEmptyField, "ServicesId"));
        }

        [Test]
        public void Should_Not_Have_Error_When_Title_And_IsActive_And_ServicesId_Are_Valid()
        {
            // Arrange
            var command = new CreateSpecializationCommand(new CreateSpecializationDTO
            {
                Title = "SomeTitle",
                IsActive = true,
                ServicesId = new List<long>() { 1, 2, 3 }
            });

            // Act
            var result = _validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

    }
}
