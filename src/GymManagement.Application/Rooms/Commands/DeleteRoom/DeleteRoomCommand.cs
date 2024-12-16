using ErrorOr;

using MediatR;

namespace GymManagement.Api.Rooms.Commands.DeleteRoom;

public record DeleteRoomCommand(Guid GymId, Guid RoomId)
    : IRequest<ErrorOr<Deleted>>;