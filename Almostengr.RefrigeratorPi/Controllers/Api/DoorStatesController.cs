using Almostengr.RefrigeratorPi.Features.Common.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Almostengr.RefrigeratorPi.Controllers.Api;

public class DoorStatesController : BaseAppApiController
{
    [HttpGet]
    public IActionResult Open()
    {
        return Ok(FridgeStateModel.IsDoorOpen);
    }
}