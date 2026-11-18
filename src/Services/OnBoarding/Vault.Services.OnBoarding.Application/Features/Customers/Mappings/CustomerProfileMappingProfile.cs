using AutoMapper;
using Vault.Services.OnBoarding.Application.Features.Customers.Models;
using Vault.Services.OnBoarding.Domain.Customer;

namespace Vault.Services.OnBoarding.Application.Features.Customers.Mappings;

internal sealed class CustomerProfileMappingProfile : Profile
{
    public CustomerProfileMappingProfile()
    {
        CreateMap<CustomerProfile, CustomerProfileDto>();
    }
}
