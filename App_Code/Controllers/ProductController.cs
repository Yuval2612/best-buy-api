using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;


[RoutePrefix("api/product")]
[System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
public class ProductController : ApiController
{
    private static readonly NLog.Logger _log = NLog.LogManager.GetCurrentClassLogger();


    [HttpGet]
    [Route("{pageSize=pageSize}/{page=page}")]
    public async Task<IHttpActionResult> GetProducts(int pageSize, int page)
    {
        try
        {       
            var products = await BestBuy.Auth().GetProducts(pageSize, page);
            return Ok(products);
        }
        catch (Exception ex)
        {
            _log.Error(ex.ToString());
            return null;
        }
    }

    [HttpGet]
    [Route("{sku}")]
    public async Task<IHttpActionResult> GetProductBySku(int sku)
    {
        try
        {
            var product = await BestBuy.Auth().GetProduct(sku);
            return Ok(product);
        }
        catch (Exception ex)
        {
            _log.Error(ex.ToString());
            return null;
        }
    }
}
