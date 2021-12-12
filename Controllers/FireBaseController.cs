using Microsoft.AspNetCore.Mvc;

namespace Minimal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FireBaseController : ControllerBase
    {
        public string FireBaseUid => User.FindFirst(claim => claim.Type == "user_id").Value;
    }
}
