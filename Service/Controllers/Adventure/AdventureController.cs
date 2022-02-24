using Application;
using Common;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service
{
    [Route("[controller]")]
    [ApiController]
    public class AdventureController : BaseController
    {
        private readonly IAdventureApp<DecisionData> _adventureApp;

        public AdventureController(IAdventureApp<DecisionData> adventureApp, ILogger<AdventureController> logger)
            : base(logger)
        {
            _adventureApp = adventureApp;
        }

        /// <summary>
        /// Returns a decision-tree with the specified key.
        /// </summary>
        // GET <AdventureController>/C4C2E408FEC34AF2B29A4A385B5261A1
        [HttpGet("{key}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string key)
        {
            try
            {
                DecisionTree<DecisionData> decisionTree = await _adventureApp.GetDecisionTree(key);

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

        /// <summary>
        /// Creates an initial decision-tree.
        /// </summary>
        // POST <AdventureController>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post(DecisionTree<DecisionData> value)
        {
            try
            {
                string key = await _adventureApp.AddDecisionTree(value);

                // Response model mapping
                CreationResponseModel responseModel = new()
                {
                    Key = key
                };
                
                return Created(new Uri($"/adventure/{key}", UriKind.Relative), responseModel);
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

        /// <summary>
        /// Updates a decision-tree with the specified key and choices.
        /// </summary>
        // PUT <AdventureController>/C4C2E408FEC34AF2B29A4A385B5261A1
        [HttpPut("{key}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put([FromBody]DecisionTree<DecisionData> value, string key)
        {
            try
            {
                await _adventureApp.UpdateDecisionTree(value, key);

                return NoContent();
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
    }
}
