using AutoMapper;
using CloudDatabasesAssignment.Models.Entities;
using CloudDatabasesAssignment.Models.Requests;
using CloudDatabasesAssignment.Models.Responses;

namespace CloudDatabasesAssignment.Api.Context;

public class AutoMappingProfile : Profile
{
    public AutoMappingProfile()
    {
        CreateMap<CustomerRequest, Customer>();
        CreateMap<Customer, CustomerResponse>();
        
        CreateMap<ListingRequest, Listing>();
        CreateMap<Listing, ListingResponse>();
        
        CreateMap<MortgageInquiry, MortgageInquiryResponse>();
    }
}