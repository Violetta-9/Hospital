using Authorization.Data.Repository;
using FluentValidation.Results;
using Moq;
using Specialization.API.Application.Query.GetSpecializationById;
using Specialization.API.Application.Validator.Query;

namespace Specialization.Tests.Validators
{
    internal class GetSpecializationByIdValidatorTests
    {
        private GetSpecializationByIdValidator _validator;
        private Mock<ISpecializationRepository> _specializationRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _specializationRepositoryMock = new Mock<ISpecializationRepository>();
            _validator = new GetSpecializationByIdValidator(_specializationRepositoryMock.Object);
        }

        [Test]
        public async Task Should_Have_Error_When_Id_Is_Empty()
        {
            // Arrange
            var command = new GetSpecializationByIdQuery(0);

            // Act
            ValidationResult result = await _validator.ValidateAsync(command);

            // Assert
            Assert.That(result.Errors, Has.Some.Matches<ValidationFailure>(
                x => x.ErrorMessage == "Field SpecializationId cannot be empty"));
        }

        [Test]
        public async Task Should_Have_Error_When_Specialization_Does_Not_Exist()
        {
            // Arrange
            var command = new GetSpecializationByIdQuery(1);
            
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
