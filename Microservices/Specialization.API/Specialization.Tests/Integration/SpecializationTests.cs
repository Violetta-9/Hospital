using FluentAssertions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Services.API.Client.Abstraction;
using Services.API.Client.GeneratedClient;
using Specialization.API.Application.Command.CreateSpecialization;
using Specialization.API.Application.Command.UpdateSpecialization;
using Specialization.API.Application.Command.UpdateSpecializationStatus;
using Specialization.API.Application.Contracts.Incoming;
using Specialization.API.Application.Query.GetAllSpecialization;
using Specialization.API.Application.Query.GetSpecializationById;

namespace Specialization.Tests.Integration;

[TestFixture]
internal class SpecializationTests : IntegrationBaseTest
{
    [Test]
    public async Task CreateSpecialization_WithSuccessResponseFromService_ReturnSpecializationIdOnSuccess()
    {
        //Arrange
        var query = new CreateSpecializationCommand(new CreateSpecializationDTO
        {
            IsActive = true,
            ServicesId = new List<long> { 1, 2, 3 },
            Title = "New"
        });

        var serviceApiService = Services.GetRequiredService<IServiceApiProxy>();
        var mock = Mock.Get(serviceApiService);
        mock
            .Setup(ser => ser.SetSpecializationIdForServicesAsync(It.IsAny<long>(), It.IsAny<ICollection<long>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response { IsSuccess = true });

        //Act
        var response = await _mediator.Send(query);

        //Assert
        Assert.IsTrue(response > 0);
    }

    [Test]
    public void CreateSpecialization_WithBadResponseFromService_ThrowsException()
    {
        //Arrange
        var query = new CreateSpecializationCommand(new CreateSpecializationDTO
        {
            IsActive = true,
            ServicesId = new List<long> { 1, 2, 3 },
            Title = "New"
        });

        var serviceApiService = Services.GetRequiredService<IServiceApiProxy>();
        var mock = Mock.Get(serviceApiService);
        mock
            .Setup(ser => ser.SetSpecializationIdForServicesAsync(It.IsAny<long>(), It.IsAny<ICollection<long>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response { IsSuccess = false });

        //Assert
        var exception = Assert.ThrowsAsync<Exception>(async () =>
            await _mediator.Send(query));

        Assert.That(exception?.Message, Is.EqualTo("CreateSpecializationCommandHandler work bad"));
    }

    [Test]
    public void UpdateSpecialization_WithNoExistSpecialization_ReturnFalseResponse()
    {
        //Arrange
        var query = new UpdateSpecializationCommand(new UpdateSpecializationDTO
        {
            Id = 20,
            ServicesId = new List<long> { 1, 2, 3 },
            Title = "New"
        });

        var serviceApiService = Services.GetRequiredService<IServiceApiProxy>();
        var mock = Mock.Get(serviceApiService);
        mock
            .Setup(ser => ser.UpdateSpecializationIdForServicesAsync(It.IsAny<long>(), It.IsAny<ICollection<long>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response { IsSuccess = true });

        Assert.ThrowsAsync<ValidationException>(async () =>
            await _mediator.Send(query));
    }

    [Test]
    public async Task UpdateSpecialization_WithSuccessResponseFromService_ReturnSpecializationIdOnSuccess()
    {
        //Arrange
        var query = new UpdateSpecializationCommand(new UpdateSpecializationDTO
        {
            Id = 1,
            ServicesId = new List<long> { 1, 2, 3 },
            Title = "New"
        });

        var receiveUpdatedEntityQuery = new GetSpecializationByIdQuery(1);

        var serviceApiService = Services.GetRequiredService<IServiceApiProxy>();
        var mock = Mock.Get(serviceApiService);
        mock
            .Setup(ser => ser.UpdateSpecializationIdForServicesAsync(It.IsAny<long>(), It.IsAny<ICollection<long>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response { IsSuccess = true });

        //Act
        var response = await _mediator.Send(query);
        var checkResponse = await _mediator.Send(receiveUpdatedEntityQuery);

        //Assert
        Assert.IsTrue(response.IsSuccess);
        query.Should().BeEquivalentTo(checkResponse, options => options.ExcludingMissingMembers());
    }

    [Test]
    public async Task UpdateSpecializationStatus_ReturnSpecializationIdOnSuccess()
    {
        //Arrange
        var query = new UpdateSpecializationStatusCommand(new UpdateSpecializationStatusDTO
        {
            Id = 1,
            IsActive = false
        });
        var receiveUpdatedEntityQuery = new GetSpecializationByIdQuery(1);

        //Act
        var response = await _mediator.Send(query);
        var checkResponse = await _mediator.Send(receiveUpdatedEntityQuery);
        Assert.IsTrue(response.IsSuccess);
        query.Should().BeEquivalentTo(checkResponse, options => options.ExcludingMissingMembers());
    }

    [Test]
    public void UpdateSpecializationStatus_WithNoSpecializationFromRepository_ReturnBadResponse()
    {
        //Arrange
        var query = new UpdateSpecializationStatusCommand(new UpdateSpecializationStatusDTO
        {
            Id = 20,
            IsActive = true
        });
        //Assert
        Assert.ThrowsAsync<ValidationException>(async () =>
            await _mediator.Send(query));
    }

    [Test]
    public async Task GetAllSpecialization_ReturnSpecializationListArray()
    {
        //Arrange
        var query = new GetAllSpecializationQuery();

        //Act
        var response = await _mediator.Send(query);

        //Assert
        response.Should().NotBeNull();
        response.Should().NotContainNulls();
    }

    [Test]
    public async Task GetSpecializationByIdQuery_ReturnSpecializationOnSuccess()
    {
        //Arrange
        var query = new GetSpecializationByIdQuery(1);

        //Act
        var response = await _mediator.Send(query);

        //Assert
        response.Should().NotBeNull();
        response.Title.Should().NotBeNull();
        response.Services.Should().NotContainNulls();
    }

    [Test]
    public void GetSpecializationByIdQuery_WithNoExistingSpecialization_ThrowException()
    {
        //Arrange
        var query = new GetSpecializationByIdQuery(20);

        //Assert
        Assert.ThrowsAsync<ValidationException>(async () =>
            await _mediator.Send(query));
    }
}