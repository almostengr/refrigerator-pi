using Almostengr.Refrigerator.Features.Common.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Almostengr.Refrigerator.Controllers.Api;

public class DoorStatesController : BaseAppApiController
{
    [HttpGet]
    public IActionResult Open()
    {
        return Ok(FridgeStateModel.IsDoorOpen);
    }
}