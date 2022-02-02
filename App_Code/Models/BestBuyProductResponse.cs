using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for BestBuyProductResponse
/// </summary>
public class BestBuyProductResponse
{
    public int from { get; set; }
    public int to { get; set; }
    public int currentPage { get; set; }
    public int total { get; set; }
    public int totalPages { get; set; }
    public List<Product> products { get; set; }
}