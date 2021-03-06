using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace Microsoft.eShopWeb.PublicApi.CatalogItemEndpoints;

public class ListPaged : EndpointBaseAsync
    .WithRequest<ListPagedCatalogItemRequest>
    .WithActionResult<ListPagedCatalogItemResponse>
{
    private readonly IRepository<CatalogItem> _itemRepository;
    private readonly IUriComposer _uriComposer;
    private readonly IMapper _mapper;
    private readonly ILogger<ListPaged> _logger;

   
    public ListPaged(IRepository<CatalogItem> itemRepository,
        IUriComposer uriComposer,
        IMapper mapper,
        ILogger<ListPaged> logger)
    {
        _logger = logger;
        _itemRepository = itemRepository;
        _uriComposer = uriComposer;
        _mapper = mapper;
    }

    [HttpGet("api/catalog-items")]
    [SwaggerOperation(
        Summary = "List Catalog Items (paged)",
        Description = "List Catalog Items (paged)",
        OperationId = "catalog-items.ListPaged",
        Tags = new[] { "CatalogItemEndpoints" })
    ]
    public override async Task<ActionResult<ListPagedCatalogItemResponse>> HandleAsync([FromQuery] ListPagedCatalogItemRequest request, CancellationToken cancellationToken) 
    {
        _logger.LogError("Strange error1");
        _logger.LogDebug("Strange error2");
        _logger.LogInformation("Paged request start!");
        var response = new ListPagedCatalogItemResponse(request.CorrelationId());

        var filterSpec = new CatalogFilterSpecification(request.CatalogBrandId, request.CatalogTypeId);
        int totalItems = await _itemRepository.CountAsync(filterSpec, cancellationToken);

        var pagedSpec = new CatalogFilterPaginatedSpecification(
            skip: request.PageIndex * request.PageSize,
            take: request.PageSize,
            brandId: request.CatalogBrandId,
            typeId: request.CatalogTypeId);

        var items = await _itemRepository.ListAsync(pagedSpec, cancellationToken);

        response.CatalogItems.AddRange(items.Select(_mapper.Map<CatalogItemDto>));
        foreach (CatalogItemDto item in response.CatalogItems)
        {
            item.PictureUri = _uriComposer.ComposePicUri(item.PictureUri);
        }

        if (request.PageSize > 0)
        {
            response.PageCount = int.Parse(Math.Ceiling((decimal)totalItems / request.PageSize).ToString());
        }
        else
        {
            response.PageCount = totalItems > 0 ? 1 : 0;
        }
        _logger.LogError("Number of returned list products: " + response.CatalogItems.Count);
        //return StatusCode(500, new Exception("Error i tyle").StackTrace);
        throw new Exception("Cannot move further");
        //throw new NotImplementedException("Cannot move further");
        //return BadRequest("Cannot move further");
        //return Ok(response);
    }
}
