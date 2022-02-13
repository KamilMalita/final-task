using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.eShopWeb.Web.Interfaces;

namespace Microsoft.eShopWeb.Web.Pages.Basket;

[Authorize]
public class SuccessModel : PageModel
{
    /*private readonly IBasketViewModelService _basketViewModelService;
    public SuccessModel(IBasketViewModelService basketViewModelService)
    {
        _basketViewModelService = basketViewModelService;
    }

    public BasketViewModel BasketModel { get; set; } = new BasketViewModel();
    public async Task OnGet()
    {
        BasketModel = await _basketViewModelService.CreateBasketForUser(GetOrSetBasketCookieAndUserName());
    }

    private string GetOrSetBasketCookieAndUserName()
    {
        string userName = null;

        if (Request.HttpContext.User.Identity.IsAuthenticated)
        {
            return Request.HttpContext.User.Identity.Name;
        }

        if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
        {
            userName = Request.Cookies[Constants.BASKET_COOKIENAME];

            if (!Request.HttpContext.User.Identity.IsAuthenticated)
            {
                if (!Guid.TryParse(userName, out var _))
                {
                    userName = null;
                }
            }
        }
        if (userName != null) return userName;

        userName = Guid.NewGuid().ToString();
        var cookieOptions = new CookieOptions { IsEssential = true };
        cookieOptions.Expires = DateTime.Today.AddYears(10);
        Response.Cookies.Append(Constants.BASKET_COOKIENAME, userName, cookieOptions);

        return userName;
    }*/
}
