using Application.Abstractions.Messaging;
using Application.DTOs;
using Domain.Pets;
using SharedKernel;

namespace Application.Statics.GetPetTypes;
internal sealed class GetPetTypesQueryHandler : IQueryHandler<GetPetTypesQuery, List<StaticTypeDto>>
{
    public Task<Result<List<StaticTypeDto>>> Handle(GetPetTypesQuery query, CancellationToken cancellationToken)
    {
        List<StaticTypeDto> petTypeList = [];

        foreach (object petType in Enum.GetValues(typeof(PetType)))
        {
            petTypeList.Add(new StaticTypeDto((int)petType, ((Enum)petType).ToString(), EnumHelper.GetDisplayName((Enum)petType)));
        }

        return Task.FromResult(Result.Success(petTypeList));
    }
}
