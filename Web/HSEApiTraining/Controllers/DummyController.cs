using Microsoft.AspNetCore.Mvc;

namespace HSEApiTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyController : Controller
    {
        // Объект типа IDummyService для работы контроллера.
        private readonly IDummyService _dummyService;

        public DummyController(IDummyService dummyService)
        {
            _dummyService = dummyService;
        }

        [HttpGet("generate/{number}")]
        public string DummyGenerator(int number)
        {
            return _dummyService.DummyGenerator(number);
        }
    }
}
