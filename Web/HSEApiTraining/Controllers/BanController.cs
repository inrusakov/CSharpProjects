using HSEApiTraining.Models.Calculator;
using HSEApiTraining.Models.Customer;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace HSEApiTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanController : ControllerBase
    {
        private readonly IBanService _banService;

        public BanController(IBanService banService)
        {
            _banService = banService;
        }

        #region Post
        [HttpPost("Ban")]
        public AddBanResponse GetCustomers([FromQuery]AddBanRequest request)
        {
            return new AddBanResponse
            {
                Error = _banService.Ban(request)
            };
        }
        #endregion

        #region Get
        [HttpGet("GetAll")]
        public GetBannedResponse GetAll()
        {
            var result = _banService.GetAll();
            return new GetBannedResponse
            {
                BannedPhones = result.BannedPhones,
                Error = result.Error
            };
        }
        #endregion

        #region Delete
        [HttpDelete("{id}")]
        public DeleteBanResponse DeleteBanned(int id)
        {
            return new DeleteBanResponse
            {
                Error = _banService.DeleteBanned(id)
            };
        }

        [HttpDelete("All")]
        public DeleteBanResponse DeleteAll()
        {
            return new DeleteBanResponse
            {
                Error = _banService.DeleteAll()
            };
        }
        #endregion
    }
}
