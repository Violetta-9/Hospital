using Authorization.Data.Repository;
using FluentValidation.Results;
using Moq;
using Specialization.API.Application.Command.UpdateSpecializationStatus;
using Specialization.API.Application.Contracts.Incoming;
using Specialization.API.Application.Validator.Command;


namespace Specialization.Tests.Validators
{
    internal class UpdateSpecializationStatusValidatorTests
    {
        private UpdateSpecializationStatusValidator _validator;
        private Mock<ISpecializationRepository> _specializationRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _specializationRepositoryMock = new Mock<ISpecializationRepository>();
            _validator = new UpdateSpecializationStatusValidator(_specializationRepositoryMock.Object);
        }

        [Test]
        public async Task Should_Have_Error_When_Id_Is_Empty()
        {
            // Arrange
            var command = new UpdateSpecializationStatusCommand(new UpdateSpecializationStatusDTO());

            // Act
            ValidationResult result = await _validator.ValidateAsync(command);

            // Assert
            Assert.That(result.Errors, Has.Some.Matches<ValidationFailure>(
                x => x.ErrorMessage == "Field Id cannot be empty"));
        }

        [Test]
        public async Task Should_Have_Error_When_Specialization_Does_Not_Exist()
        {
            // Arrange
            var command = new UpdateSpecializationStatusCommand(new UpdateSpecializationStatusDTO
            {
                Id = 1
            });
            _specializationRepositoryMock.Setup(repo => repo.ExistsAsync(1, CancellationToken.None))
                .ReturnsAsync(false);

            // Act
            ValidationResult result = await _validator.ValidateAsync(command);

            // Assert
            Assert.That(result.Errors, Has.Some.Matches<ValidationFailure>(
                x => x.ErrorMessage == "Specialization by Id 1 not found"));
        }
    }
}
