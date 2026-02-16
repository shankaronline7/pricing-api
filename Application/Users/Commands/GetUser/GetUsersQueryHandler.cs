using Application.DTOs.UserManagement;
using MediatR;
using Pricing.Application.Common.Interfaces;

public class GetUsersQueryHandler
    : IRequestHandler<GetUsersQuery, List<UserListDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUsersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<UserListDto>> Handle(
    GetUsersQuery request,
    CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UserRepository.GetAllAsync();

        return users
            .OrderBy(x => x.Id) 
            .Select(x => new UserListDto
            {
                Id = x.Id,
                Username = x.Username,
                Firstname = x.Firstname,
                Lastname = x.Lastname,
                EmailId = x.EmailId,
                Password = x.Password,
                Status = x.Status,
                CreatedDate = x.CreatedDate,
                CreatedBy = x.CreatedBy

            })
            .ToList();
    }

}

