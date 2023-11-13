using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RubiconmpSolution.Business.Abstract;
using RubiconmpSolution.Entities.Concrete;
using RubiconmpSolution.WebApi.Models;

namespace RubiconmpSolution.WebApi.Controllers
{
    [Route("api/rectangles")]
    [ApiController]
    public class RectanglesController : ControllerBase
    {
        private readonly IRectangleService _rectangleService;

        public RectanglesController(IRectangleService rectangleService)
        {
            _rectangleService = rectangleService;
        }

        [HttpGet]
        [Authorize(Roles = "admin,user")]
        [Route("getRectangles")]
        public async Task<IActionResult> GetRectangles([FromBody]GetRectanglesModel model)
        {
            var result = await _rectangleService.GetRectanglesAsync(model.Coordinates);

            var x = new Dictionary<double, Rectangle[]>();
            foreach (var kvp in result.Data)
            {
                x.Add(kvp.Key.X, kvp.Value);
            }
            return Ok(x);
        }
    }
}
