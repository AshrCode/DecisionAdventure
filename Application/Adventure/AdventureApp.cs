using Common;
using Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Application
{
    public class AdventureApp : IAdventureApp<DecisionData>
    {
        private readonly IDecisionRepo<DecisionData> _decisionRepo;
        private readonly ILogger _logger;

        public AdventureApp(IDecisionRepo<DecisionData> decisionRepo, ILogger<AdventureApp> logger)
        {
            _decisionRepo = decisionRepo;
            _logger = logger;
        }

        public async Task<DecisionTree<DecisionData>> GetDecisionTree(string key)
        {
            DecisionTree<DecisionData> decisionTree = await _decisionRepo.GetById(key);

            // Logs and raises error if no record was found.
            if (decisionTree is null)
            {
                var errMessage = $"Decision tree with the key {key} does not exist.";

                _logger.LogError(errMessage);
                throw new ApiException(ApiErrorCodes.BadRequest, errMessage);
            }

            return decisionTree;
        }

        public async Task<string> AddDecisionTree(DecisionTree<DecisionData> decisionTree)
        {
            string key = Guid.NewGuid().ToString("N");

            // Add to storage
            await _decisionRepo.AddDecision(decisionTree, key);

            return key;
        }

        public async Task<string> UpdateDecisionTree(DecisionTree<DecisionData> decisionTree, string key = null)
        {
            // Update to storage
            await _decisionRepo.UpdateDecision(decisionTree, key);

            return key;
        }

        // Can be used to build a Decision tree that only contains the nodes based on user choices.
        #region Additional

        public Task<DecisionTree<DecisionData>> GetUserDecision(DecisionTree<DecisionData> decisionTree)
        {
            DecisionTree<DecisionData> userDecisionTree = new()
            {
                Root = TraverseInPreorder(decisionTree.Root)
            };

            return Task.FromResult(userDecisionTree);
        }

        private DecisionNode<DecisionData> TraverseInPreorder(DecisionNode<DecisionData> node)
        {
            if (node is null)
                return null;

            if (node.Data.IsSelected.HasValue
                && node.Data.IsSelected.Value)
                return node;

            TraverseInPreorder(node.Yes);
            TraverseInPreorder(node.No);

            return null;
        }

        #endregion
    }
}
