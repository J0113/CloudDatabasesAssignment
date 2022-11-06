using AutoMapper;
using CloudDatabasesAssignment.Api.Services.Interfaces;
using CloudDatabasesAssignment.Models.Entities;
using CloudDatabasesAssignment.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CloudDatabasesAssignment.Api.Controllers;

[ApiController]
[Route("mortgages")]
public class MortgageController : ControllerBase
{

    private readonly IMortgageInquiryService _mortgageInquiryService;
    private readonly IMapper _mapper;

    public MortgageController(IMortgageInquiryService mortgageInquiryService, IMapper mapper)
    {
        _mortgageInquiryService = mortgageInquiryService;
        _mapper = mapper;
    }

    [HttpGet(Name = "Get All")]
    public async Task<IEnumerable<MortgageInquiryResponse>> GetMortgages()
    {
        var mortgages = await _mortgageInquiryService.GetAllAsync();
        return _mapper.Map<IEnumerable<MortgageInquiry>, IEnumerable<MortgageInquiryResponse>>(mortgages);
    }

    [HttpGet("{id}", Name = "Get By ID")]
    public async Task<MortgageInquiryResponse> GetMortgageById(Guid id)
    {
        var mortgage = await _mortgageInquiryService.GetByIdAsync(id);
        return _mapper.Map<MortgageInquiry, MortgageInquiryResponse>(mortgage);
    }

}