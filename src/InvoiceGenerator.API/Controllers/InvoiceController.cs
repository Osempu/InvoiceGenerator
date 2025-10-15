using System;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceGenerator.API.Controllers;

[ApiController]
[Route("api/invoices")]
public class InvoiceController : ControllerBase
{
    public InvoiceController()
    {

    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("✅ Invoices Controller Working Ok!");
    }
}
