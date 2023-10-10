
using Specialization.API.Application.Command.CreateSpecialization;
using Specialization.API.Application.Contracts.Incoming;

namespace Specialization.Tests.Integration
{
    [TestFixture]
    internal class SpecializationTests: IntegrationBaseTest
    {
        [Test]
        public async Task CreateSpecialization_WithSuccessResponseFromService_ReturnSpecializationIdOnSuccess()
        {
            //Arrange
            var query = new CreateSpecializationCommand(new CreateSpecializationDTO()
            {
                IsActive = true,
                ServicesId = new List<long>(){1,2,3},
                Title="New"
            });
            //Act
            var response = await _mediator.Send(query);

            //Assert
            Assert.IsTrue(response>0);
        }
    }
}
