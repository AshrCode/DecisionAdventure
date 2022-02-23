using Application;
using Domain;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence
{
    public class DecisionRepository : RepositoryBase<DecisionTree<string>>, IDecisionRepo<string>
    {
        private static IDictionary<string, DecisionTree<string>> _bufferStorage = new ConcurrentDictionary<string, DecisionTree<string>>();

        public DecisionRepository(ILogger<DecisionRepository> logger)
            : base(logger)
        { 
            if(_storage.Count < 1)
            {
                KeyValuePair<string, DecisionTree<string>> item = GetSampleDecisionData();
                _storage.Add(item);

                _logger.LogInformation("Sample decision tree data has been seeded.");
            }
        }

        public async Task<DecisionTree<string>> GetById(string key)
        {
            /*
             * Here, we can add additional db level functionality in future if needed.
             */

            return await Get(key);
        }

        // Update capability of the method can be avail in future.
        public async Task SaveOrUpdate(DecisionTree<string> decisionTree, string key)
        {
            /*
             * Here, we can add additional db level functionality in future if needed.
             */

            await Save(decisionTree, key);
        }

        private static KeyValuePair<string, DecisionTree<string>> GetSampleDecisionData()
        {
            string id = "9A0FA5F7048544F2A7EFBF066F8AA9A9";
            DecisionTree<string> decisionTree = new()
            {
                Root = new DecisionNode<string>
                {
                    // 1st Stage
                    Data = "Do I want an electric car?",
                    No = new DecisionNode<string>
                    {
                        // 2nd Stage
                        Data = "Maybe you want an apple?"
                    },
                    Yes = new DecisionNode<string>
                    {
                        // 2nd Stage
                        Data = "Do I desrve it?",
                        No = new DecisionNode<string>
                        {
                            // 3rd Stage
                            Data = "Is it a good car?",
                            No = new DecisionNode<string>
                            {
                                // Fourth Stage
                                Data = "Wait till you find a wrost one."
                            },
                            Yes = new DecisionNode<string>
                            {
                                // Fourth Stage
                                Data = "What are you waiting for? Go and get one."
                            }
                        },
                        Yes = new DecisionNode<string>
                        {
                            //3rd Stage
                            Data = "Are you sure?",
                            No = new DecisionNode<string>
                            {
                                // Fourth Stage
                                Data = "Do jumping jack first."
                            },
                            Yes = new DecisionNode<string>
                            {
                                // Fourth Stage
                                Data = "Get it."
                            }
                        }
                    }
                }
            };

            var item = new KeyValuePair<string, DecisionTree<string>>(id, decisionTree);

            return item;
        }
    }
}
