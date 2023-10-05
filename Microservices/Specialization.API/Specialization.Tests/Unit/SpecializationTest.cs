using Authorization.Data.Repository;
using AutoFixture;
using Moq;
using Services.API.Client.Abstraction;
using Services.API.Client.GeneratedClient;
using Specialization.API.Application.Command.CreateSpecialization;
using Specialization.API.Application.Command.UpdateSpecialization;
using Specialization.API.Application.Command.UpdateSpecializationStatus;
using Specialization.API.Application.Contracts.Incoming;
using SpecializationEntity = Authorization.Data_Domain.Models.Specialization;

namespace Specialization.Tests.Unit;

[TestFixture]
public class SpecializationTest
{
    [Test]
    public async Task CreateSpecialization_WithSuccessResponseFromService_ReturnSpecializationIdOnSuccess()
    {
        // Arrange
        var fixture = new Fixture();

        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        var expectedSpecializationId = 1;

        var specializationMockRepository = new Mock<ISpecializationRepository>();
        specializationMockRepository
            .Setup(repo => repo.InsertAsync(It.IsAny<SpecializationEntity>(),
                It.IsAny<CancellationToken>())).Callback<SpecializationEntity, CancellationToken>((entity, _) =>
            {
                entity.Id = expectedSpecializationId;
            }).ReturnsAsync((SpecializationEntity entity, CancellationToken _) => entity);
        specializationMockRepository.Setup(rep => rep.DeleteAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        var serviceMockApiProxy = new Mock<IServiceApiProxy>();
        serviceMockApiProxy
            .Setup(proxy => proxy.SetSpecializationIdForServicesAsync(It.IsAny<long>(),
                It.IsAny<ICollection<long>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response { IsSuccess = true });
        var request = new CreateSpecializationCommand(fixture.Create<CreateSpecializationDTO>());
        var handler =
            new CreateSpecializationCommandHandler(specializationMockRepository.Object, serviceMockApiProxy.Object);
        // Act
        var result = await handler.Handle(request, CancellationToken.None);
        // Assert
        specializationMockRepository.Verify(rep => rep.InsertAsync(It.Is<SpecializationEntity>(x =>
            x.Title == new SpecializationEntity
            {
                Title = request.CreateSpecializationDto.Title,
                IsActive = true
            }.Title), CancellationToken.None), Times.Once);

        serviceMockApiProxy.Verify(proxy => proxy.SetSpecializationIdForServicesAsync(
            It.Is<long>(x => x == expectedSpecializationId),
            It.Is<ICollection<long>>(x =>
                x == request.CreateSpecializationDto.ServicesId),
            CancellationToken.None), Times.Once);
        specializationMockRepository.Verify(
            rep => rep.DeleteAsync(It.Is<long>(x => x == expectedSpecializationId), CancellationToken.None),
            Times.Never);


        Assert.That(result, Is.EqualTo(expectedSpecializationId));
    }

    [Test]
    public void CreateSpecialization_WithBadResponseFromService_ThrowsException()
    {
        // Arrange
        var fixture = new Fixture();

        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        const long expectedSpecializationId = 1;

        var specializationMockRepository = new Mock<ISpecializationRepository>();
        specializationMockRepository
            .Setup(repo => repo.InsertAsync(It.IsAny<SpecializationEntity>(),
                It.IsAny<CancellationToken>())).Callback<SpecializationEntity, CancellationToken>((entity, _) =>
            {
                entity.Id = expectedSpecializationId;
            }).ReturnsAsync((SpecializationEntity entity, CancellationToken _) => entity);

        var serviceMockApiProxy = new Mock<IServiceApiProxy>();
        serviceMockApiProxy
            .Setup(proxy => proxy.SetSpecializationIdForServicesAsync(It.IsAny<long>(),
                It.IsAny<ICollection<long>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response { IsSuccess = false });
        var request = new CreateSpecializationCommand(fixture.Create<CreateSpecializationDTO>());
        var handler =
            new CreateSpecializationCommandHandler(specializationMockRepository.Object, serviceMockApiProxy.Object);
        // Act


        // Assert
        var exception = Assert.ThrowsAsync<Exception>(async () =>
            await handler.Handle(request, CancellationToken.None))!;

        Assert.That(exception.Message, Is.EqualTo("CreateSpecializationCommandHandler work bad"));

        specializationMockRepository.Verify(rep => rep.InsertAsync(It.Is<SpecializationEntity>(x =>
            x.Title == new SpecializationEntity
            {
                Title = request.CreateSpecializationDto.Title,
                IsActive = true
            }.Title), CancellationToken.None), Times.Once);

        serviceMockApiProxy.Verify(proxy => proxy.SetSpecializationIdForServicesAsync(
            It.Is<long>(x => x == expectedSpecializationId),
            It.Is<ICollection<long>>(x =>
                x == request.CreateSpecializationDto.ServicesId),
            CancellationToken.None), Times.Once);
        specializationMockRepository.Verify(rep => rep.DeleteAsync(It.Is<long>(x =>
            x == expectedSpecializationId), CancellationToken.None), Times.Once);
    }

    [Test]
    public async Task UpdateSpecialization_WithSuccessResponseFromService_ReturnSpecializationIdOnSuccess()
    {
        // Arrange
        var fixture = new Fixture();
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var expectedSpecialization = fixture.Build<SpecializationEntity>()
            .With(x => x.Id, 1).Create();

        var specializationMockRepository = new Mock<ISpecializationRepository>();
        specializationMockRepository.Setup(rep => rep.GetAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedSpecialization);

        var serviceMockApiProxy = new Mock<IServiceApiProxy>();
        serviceMockApiProxy
            .Setup(proxy => proxy.UpdateSpecializationIdForServicesAsync(It.IsAny<long>(),
                It.IsAny<ICollection<long>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response { IsSuccess = true });

        specializationMockRepository
            .Setup(repo => repo.UpdateAsync(It.IsAny<SpecializationEntity>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((SpecializationEntity entity, CancellationToken _) =>
            {
                expectedSpecialization.Title = entity.Title;
                return entity;
            });

        specializationMockRepository.Setup(rep => rep.DeleteAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var request =
            new UpdateSpecializationCommand(fixture.Build<UpdateSpecializationDTO>().With(x => x.Id, 1).Create());
        var handler =
            new UpdateSpecializationCommandHandler(serviceMockApiProxy.Object, specializationMockRepository.Object);
        // Act
        var result = await handler.Handle(request, CancellationToken.None);
        // Assert
        specializationMockRepository.Verify(rep => rep.GetAsync(It.Is<long>(x =>
            x == request.Id), CancellationToken.None), Times.Once);

        serviceMockApiProxy.Verify(proxy => proxy.UpdateSpecializationIdForServicesAsync(
            It.Is<long>(x => x == expectedSpecialization.Id),
            It.Is<ICollection<long>>(x =>
                x == request.ServicesId),
            CancellationToken.None), Times.Once);
        specializationMockRepository.Verify(
            rep => rep.DeleteAsync(It.Is<long>(x => x == expectedSpecialization.Id), CancellationToken.None),
            Times.Never);
        specializationMockRepository.Verify(rep => rep.UpdateAsync(It.Is<SpecializationEntity>(
            x => x.IsActive == expectedSpecialization.IsActive &&
                 x.Title == expectedSpecialization.Title &&
                 x.Id == expectedSpecialization.Id
        ), CancellationToken.None), Times.Once);


        Assert.IsTrue(result.IsSuccess);
        Assert.That(request.Id, Is.EqualTo(expectedSpecialization.Id));
        Assert.That(request.Title, Is.EqualTo(expectedSpecialization.Title));
    }

    [Test]
    public async Task UpdateSpecialization__ReturnSpecializationIdOnSuccess()
    {
        // Arrange
        var fixture = new Fixture();
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var specializationMockRepository = new Mock<ISpecializationRepository>();
        specializationMockRepository.Setup(rep => rep.GetAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(null! as SpecializationEntity);

        var serviceMockApiProxy = new Mock<IServiceApiProxy>();
        serviceMockApiProxy
            .Setup(proxy => proxy.UpdateSpecializationIdForServicesAsync(It.IsAny<long>(),
                It.IsAny<ICollection<long>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response { IsSuccess = true });

        specializationMockRepository
            .Setup(repo => repo.UpdateAsync(It.IsAny<SpecializationEntity>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((SpecializationEntity entity, CancellationToken _) => { return entity; });


        var request =
            new UpdateSpecializationCommand(fixture.Build<UpdateSpecializationDTO>().With(x => x.Id, 1).Create());
        var handler =
            new UpdateSpecializationCommandHandler(serviceMockApiProxy.Object, specializationMockRepository.Object);
        // Act
        var result = await handler.Handle(request, CancellationToken.None);
        // Assert
        specializationMockRepository.Verify(rep => rep.GetAsync(It.Is<long>(x =>
            x == request.Id), CancellationToken.None), Times.Once);

        serviceMockApiProxy.Verify(proxy => proxy.UpdateSpecializationIdForServicesAsync(
            It.IsAny<long>(),
            It.IsAny<ICollection<long>>(),
            CancellationToken.None), Times.Never);

        specializationMockRepository.Verify(
            rep => rep.UpdateAsync(It.IsAny<SpecializationEntity>(), CancellationToken.None), Times.Never);
        Assert.IsFalse(result.IsSuccess);
    }

    [Test]
    public void UpdateSpecialization_ReturnSpecializationIdOnSuccess()
    {
        // Arrange
        var fixture = new Fixture();
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var expectedSpecialization = fixture.Build<SpecializationEntity>()
            .With(x => x.Id, 1).Create();

        var specializationMockRepository = new Mock<ISpecializationRepository>();
        specializationMockRepository.Setup(rep => rep.GetAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedSpecialization);

        var serviceMockApiProxy = new Mock<IServiceApiProxy>();
        serviceMockApiProxy
            .Setup(proxy => proxy.UpdateSpecializationIdForServicesAsync(It.IsAny<long>(),
                It.IsAny<ICollection<long>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Response { IsSuccess = false });

        specializationMockRepository
            .Setup(repo => repo.UpdateAsync(It.IsAny<SpecializationEntity>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((SpecializationEntity entity, CancellationToken _) => { return entity; });


        var request =
            new UpdateSpecializationCommand(fixture.Build<UpdateSpecializationDTO>().With(x => x.Id, 1).Create());
        var handler =
            new UpdateSpecializationCommandHandler(serviceMockApiProxy.Object, specializationMockRepository.Object);

        // Assert

        var exception = Assert.ThrowsAsync<Exception>(async () =>
            await handler.Handle(request, CancellationToken.None))!;

        Assert.That(exception.Message, Is.EqualTo("UpdateSpecializationCommandHandler work bad"));

        specializationMockRepository.Verify(rep => rep.GetAsync(It.Is<long>(x =>
            x == request.Id), CancellationToken.None), Times.Once);

        serviceMockApiProxy.Verify(proxy => proxy.UpdateSpecializationIdForServicesAsync(
            It.Is<long>(x => x == expectedSpecialization.Id),
            It.Is<ICollection<long>>(x =>
                x == request.ServicesId),
            CancellationToken.None), Times.Once);

        specializationMockRepository.Verify(rep => rep.UpdateAsync(It.IsAny<SpecializationEntity>(
        ), CancellationToken.None), Times.Never);
    }

    [Test]
    public async Task UpdateSpecializationStatus_ReturnSpecializationIdOnSuccess()
    {
        // Arrange
        var fixture = new Fixture();
        fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList().ForEach(b => fixture.Behaviors.Remove(b));
        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        var expectedSpecialization = fixture.Build<SpecializationEntity>()
            .With(x => x.Id, 1)
            .Create();

        var specializationMockRepository = new Mock<ISpecializationRepository>();
        specializationMockRepository.Setup(rep => rep.GetAsync(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedSpecialization);

        specializationMockRepository
            .Setup(repo => repo.UpdateAsync(It.IsAny<SpecializationEntity>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync((SpecializationEntity entity, CancellationToken _) => entity);


        var request = new UpdateSpecializationStatusCommand(fixture.Build<UpdateSpecializationStatusDTO>()
            .With(x => x.Id, 1)
            .With(x => x.IsActive, false)
            .Create());
        var handler = new UpdateSpecializationStatusCommandHandler(specializationMockRepository.Object);

        // Assert
        var result = await handler.Handle(request, CancellationToken.None);
        specializationMockRepository.Verify(rep => rep.GetAsync(It.Is<long>(x =>
            x == request.UpdateSpecializationStatusDto.Id), CancellationToken.None), Times.Once);


        specializationMockRepository.Verify(rep => rep.UpdateAsync(It.Is<SpecializationEntity>(
            x => x.Id == expectedSpecialization.Id
        ), CancellationToken.None), Times.Once);

        Assert.IsTrue(result.IsSuccess);
        Assert.That(request.UpdateSpecializationStatusDto.Id, Is.EqualTo(expectedSpecialization.Id));
        Assert.That(request.UpdateSpecializationStatusDto.IsActive, Is.EqualTo(expectedSpecialization.IsActive));
    }
}