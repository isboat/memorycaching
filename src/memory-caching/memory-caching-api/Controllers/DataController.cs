using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace memory_caching_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataService _dataService;

        public DataController(IDataService dataService)
        {
            _dataService = dataService;
        }

        // GET api/<DataController>/5
        [HttpGet("{key}")]
        public IActionResult Get(string key)
        {
            if(string.IsNullOrEmpty(key))
            {
                return BadRequest();
            }

            var data = _dataService.Get(key);

            return data == null ? NotFound() : new OkObjectResult(data);
        }

        // POST api/<DataController>
        [HttpPost]
        public IActionResult Post([FromBody] DataModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            _dataService.Upsert(model.Key, model.Value);

            return Ok();
        }

        // PUT api/<DataController>/5
        [HttpPut()]
        public IActionResult Put([FromBody] DataModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }

            _dataService.Upsert(model.Key, model.Value, TimeSpan.FromSeconds(20));

            return Ok();
        }

        // DELETE api/<DataController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return BadRequest();
            }

            _dataService.Delete(key);

            return Ok();
        }
    }
}
