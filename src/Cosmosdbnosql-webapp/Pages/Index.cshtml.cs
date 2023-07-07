﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Cosmosdbnosql_webapp.Models;
using Cosmosdbnosql_webapp.Services;

namespace Cosmosdbnosql_webapp.Pages;

public class IndexPageModel : PageModel
{
    private readonly ICosmosService _cosmosService;

    public IEnumerable<Product>? Products { get; set; }

    public IndexPageModel(ICosmosService cosmosService)
    {
        _cosmosService = cosmosService;
    }

    public async Task OnGetAsync()
    {
        Products ??= await _cosmosService.RetrieveActiveProductsAsync();
    }
}