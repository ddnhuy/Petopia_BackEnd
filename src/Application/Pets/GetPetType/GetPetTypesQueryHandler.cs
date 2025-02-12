using Application.Abstractions.Messaging;
using Application.DTOs.Pet;
using Domain.Pets;
using SharedKernel;

namespace Application.Pets.GetPetType;
internal sealed class GetPetTypesQueryHandler : IQueryHandler<GetPetTypesQuery, List<PetTypeDto>>
{
    public Task<Result<List<PetTypeDto>>> Handle(GetPetTypesQuery query, CancellationToken cancellationToken)
    {
        List<PetTypeDto> petTypeList = [];

        foreach (object petType in Enum.GetValues(typeof(PetType)))
        {
            petTypeList.Add(new PetTypeDto((int)petType, ((Enum)petType).ToString(), EnumHelper.GetDisplayName((Enum)petType)));
        }

        return Task.FromResult(Result.Success(petTypeList));
    }
}
