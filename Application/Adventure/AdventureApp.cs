using Common;
using Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Application
{
    public class AdventureApp : IAdventureApp<string>
    {
        private readonly IDecisionRepo<string> _decisionRepo;
        private readonly ILogger _logger;

        public AdventureApp(IDecisionRepo<string> decisionRepo, ILogger<AdventureApp> logger)
        {
            _decisionRepo = decisionRepo;
            _logger = logger;
        }

        public async Task<DecisionTree<string>> GetDecisionTree(string key)
        {
            DecisionTree<string> decisionTree = await _decisionRepo.GetById(key);

            // Logs and raises error if no record was found.
            if (decisionTree is null)
            {
                var errMessage = $"Decision tree with the key {key} does not exist.";

                _logger.LogError(errMessage);
                throw new ApiException(ApiErrorCodes.BadRequest, errMessage);
            }

            return decisionTree;
        }

        public async Task<string> CreateDecisionTree(DecisionTree<string> decisionTree)
        {
            string key = Guid.NewGuid().ToString("N");

            await _decisionRepo.SaveOrUpdate(decisionTree, key);

            return key;
        }

        public async Task<DecisionTree<DecisionData>> GetUserDecision(DecisionTree<DecisionData> decisionTree)
        {
            DecisionTree<DecisionData> userDecisionTree = new()
            {
                Root = TraverseInPreorder(decisionTree.Root)
            };

            return userDecisionTree;
        }

        private DecisionNode<DecisionData> TraverseInPreorder(DecisionNode<DecisionData> node)
        {
            if (node is null)
                return null;

            if (node.Data.IsSelected)
                return node;

            TraverseInPreorder(node.Yes);
            TraverseInPreorder(node.No);

            return null;
        }
    }
}
