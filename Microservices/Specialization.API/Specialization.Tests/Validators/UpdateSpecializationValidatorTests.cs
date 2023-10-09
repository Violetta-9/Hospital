using Authorization.Data.Repository;
using FluentValidation.Results;
using FluentValidation.TestHelper;
using Moq;
using Specialization.API.Application.Command.UpdateSpecialization;
using Specialization.API.Application.Contracts.Incoming;
using Specialization.API.Application.Validator.Command;

namespace Specialization.Tests.Validators
{
    internal class UpdateSpecializationValidatorTests
    {
        private UpdateSpecializationValidator _validator;
        private Mock<ISpecializationRepository> _specializationRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _specializationRepositoryMock = new Mock<ISpecializationRepository>();
            _validator = new UpdateSpecializationValidator(_specializationRepositoryMock.Object);
        }

        [Test]
        public async Task Should_Have_Error_When_Id_Is_Empty()
        {
            // Arrange
            var command = new UpdateSpecializationCommand(new UpdateSpecializationDTO()
            {
                Title = "Test Title"
            });

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Id)
                .WithErrorMessage("Field Id cannot be empty");
        }

        [Test]
        public async Task Should_Have_Error_When_Specialization_Does_Not_Exist()
        {
            // Arrange
            // Arrange
            var command = new UpdateSpecializationCommand(new UpdateSpecializationDTO()
            {
                Id = 1,
                Title = "Test Title"
            });
            _specializationRepositoryMock.Setup(repo => repo.ExistsAsync(1, CancellationToken.None))
                .ReturnsAsync(false);

            // Act
            ValidationResult result = await _validator.ValidateAsync(command);

            // Assert
            Assert.That(result.Errors, Has.Some.Matches<ValidationFailure>(
                x => x.ErrorMessage == "Specialization by Id 1 not found"));

        }

        [Test]
        public async Task Should_Have_Error_When_Title_Is_Empty()
        {
            // Arrange
            var command = new UpdateSpecializationCommand(new UpdateSpecializationDTO()
            {
                Id = 1,
            });

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Title)
                .WithErrorMessage("Field Title cannot be empty");
           
        }
        [Test]
        public async Task Should_Not_Have_Error_When_Title_And_Id_Are_Valid()
        {
            // Arrange
            var command = new UpdateSpecializationCommand(new UpdateSpecializationDTO()
            {
                Id = 1,
                Title = "Test Title"
            });
            _specializationRepositoryMock.Setup(repo => repo.ExistsAsync(1, CancellationToken.None))
                .ReturnsAsync(true);

            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
