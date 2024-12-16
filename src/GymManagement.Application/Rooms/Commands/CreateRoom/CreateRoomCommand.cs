using ErrorOr;
using GymManagement.Domain.Rooms;
using MediatR;

namespace GymManagement.Api.Rooms.Commands.CreateRoom;

public record CreateRoomCommand(
    Guid GymId,
    string RoomName) : IRequest<ErrorOr<Room>>;