using Application.Dtos.ShipPosition;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class ShipPositionService : GenericService<ShipPosition, ShipPositionDto>, IShipPositionService  
{
    public ShipPositionService(IShipPositionRepository repository, IMapper mapper) : base(repository, mapper)
    {
    }
}