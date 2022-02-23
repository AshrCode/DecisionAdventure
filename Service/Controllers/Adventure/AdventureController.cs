using Application;
using Common;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service
{
    [Route("[controller]")]
    [ApiController]
    public class AdventureController : BaseController
    {
        private readonly IAdventureApp<string> _adventureApp;

        public AdventureController(IAdventureApp<string> adventureApp, ILogger<AdventureController> logger)
            : base(logger)
        {
            _adventureApp = adventureApp;
        }

        // GET: <AdventureController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET <AdventureController>/C4C2E408FEC34AF2B29A4A385B5261A1
        [HttpGet("{key}")]
        public async Task<IActionResult> Get(string key)
        {
            try
            {
                DecisionTree<string> decisionTree = await _adventureApp.GetDecisionTree(key);

                return Ok(decisionTree);
            }
            catch (ApiException aexp)
            {
                return HandleApiException(aexp);
            }
            catch (Exception ex)
            {
                return HandleApiException(ex);
            }
        }

        // POST <AdventureController>
        [HttpPost]
        public async Task<IActionResult> Post(DecisionTree<string> value)
        {
            try
            {
                string id = await _adventureApp.CreateDecisionTree(value);

                // Response model mapping
                CreationResponseModel responseModel = new()
                {
                    Key = id
                };
                
                return Created(new Uri($"/adventure/{id}", UriKind.Relative), responseModel);
            }
            catch (ApiException aexp)
            {
                return HandleApiException(aexp);
            }
            catch (Exception ex)
            {
                return HandleApiException(ex);
            }
        }

        // PUT api/<AdventureController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AdventureController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
