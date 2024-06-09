using Microsoft.AspNetCore.Mvc;
using PersuadeMate.Data;
using PersuadeMate.Data.Interfaces;

namespace PersuadeMate.Api.Controllers
{
    /// <summary>
    /// 利用可能な年代を取得するためのコントローラクラスです
    /// </summary>
    /// <param name="agesRepository"></param>
    [Route("api/[controller]")]
    [ApiController]
    public class AgesController(IAgesRepository agesRepository) : ControllerBase
    {
        /// <summary>
        /// システムで利用可能な年代をすべて取得します
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<Age>> GetAllAges()
        {
            var ages = agesRepository.GetAllAges();
            return Ok(ages);
        }
    }
}
