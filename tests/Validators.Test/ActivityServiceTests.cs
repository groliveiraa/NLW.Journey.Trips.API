using Journey.Application.Services;
using Journey.Domain;
using Journey.Domain.Entities;
using Journey.Domain.Enums;
using Journey.Domain.Interfaces.Repositories;
using Journey.Exception.ExceptionsBase;
using Moq;

namespace Validators.Test;

public class ActivityServiceTests
{
    private readonly Mock<IActivityRepository> _activityRepositoryMock;
    private readonly Mock<ITripRepository> _tripRepositoryMock;
    private readonly ActivityService _activityService; 

    public ActivityServiceTests()
    {
        _activityRepositoryMock = new Mock<IActivityRepository>();
        _tripRepositoryMock = new Mock<ITripRepository>();
        _activityService = new ActivityService(_tripRepositoryMock.Object, _activityRepositoryMock.Object);
    }

    [Fact]
    public void UpdateActivityStatus_ExistingActivity_UpdatesStatusDone()
    {
        // Arrange
        var tripId = Guid.NewGuid();
        var activityId = Guid.NewGuid();
        var activity = new Activity
        {
            Id = activityId,
            TripId = tripId,
            Name = "Test",
            Status = ActivityStatus.Pending
        };

        _activityRepositoryMock.Setup(repo => repo.GetActivityByTripAndId(tripId, activityId)).Returns(activity);

        // Act
        _activityService.UpdateActivityStatus(tripId, activityId);

        // Assert
        Assert.Equal(ActivityStatus.Done, activity.Status);
        _activityRepositoryMock.Verify(repo => repo.Update(activity), Times.Once());
    }

    [Fact]
    public void UpdateActivityStatus_NonExistingActivity_ThrowsNotFoundException()
    {
        // Arrange
        var tripId = Guid.NewGuid();
        var activityId = Guid.NewGuid();

        _activityRepositoryMock.Setup(repo => repo.GetActivityByTripAndId(tripId, activityId)).Returns((Activity)null);

        // Act
        var exception = Assert.Throws<NotFoundException>(() => _activityService.UpdateActivityStatus(tripId, activityId));

        // Assert
        Assert.Equal("Activity not found.", exception.Message);
        _activityRepositoryMock.Verify(repo => repo.Update(It.IsAny<Activity>()), Times.Never);
    }
}