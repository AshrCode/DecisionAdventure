using Application;
using Domain;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence
{
    public class DecisionRepository : RepositoryBase<DecisionTree<DecisionData>>, IDecisionRepo<DecisionData>
    {
        //private static IDictionary<string, DecisionTree<DecisionData>> _bufferStorage = new ConcurrentDictionary<string, DecisionTree<DecisionData>>();

        public DecisionRepository(ILogger<DecisionRepository> logger)
            : base(logger)
        {
            if (_storage.Count < 1)
            {
                KeyValuePair<string, DecisionTree<DecisionData>> item = GetSampleDecisionData();
                _storage.Add(item);

                _logger.LogInformation("Sample decision tree data has been seeded.");
            }
        }

        public async Task<DecisionTree<DecisionData>> GetById(string key)
        {
            /*
             * Here, we can add additional db level functionality in future if needed.
             */

            return await Get(key);
        }

        // Update capability of the method can be avail in future.
        public async Task SaveOrUpdate(DecisionTree<DecisionData> decisionTree, string key)
        {
            /*
             * Here, we can add additional db level functionality in future if needed.
             */

            await Save(decisionTree, key);
        }

        private static KeyValuePair<string, DecisionTree<DecisionData>> GetSampleDecisionData()
        {
            string id = "9A0FA5F7048544F2A7EFBF066F8AA9A9";
            DecisionTree<DecisionData> decisionTree = new()
            {
                Root = new DecisionNode<DecisionData>
                {
                    // 1st Stage
                    Data = new()
                    {
                        Title = "Do I want an electric car?"
                    },
                    No = new DecisionNode<DecisionData>
                    {
                        // 2nd Stage
                        Data = new()
                        {
                            Title = "Maybe you want an apple?"
                        }
                    },
                    Yes = new DecisionNode<DecisionData>
                    {
                        // 2nd Stage
                        Data = new()
                        {
                            Title = "Do I desrve it?"
                        },
                        No = new DecisionNode<DecisionData>
                        {
                            // 3rd Stage
                            Data = new()
                            {
                                Title = "Is it a good car?"
                            },
                            No = new DecisionNode<DecisionData>
                            {
                                // Fourth Stage
                                Data = new()
                                {
                                    Title = "Wait till you find a wrost one."
                                }
                            },
                            Yes = new DecisionNode<DecisionData>
                            {
                                // Fourth Stage
                                Data = new()
                                {
                                    Title = "What are you waiting for? Go and get one."
                                }
                            }
                        },
                        Yes = new DecisionNode<DecisionData>
                        {
                            //3rd Stage
                            Data = new()
                            {
                                Title = "Are you sure?"
                            },
                            No = new DecisionNode<DecisionData>
                            {
                                // Fourth Stage
                                Data = new()
                                {
                                    Title = "Do jumping jack first."
                                }
                            },
                            Yes = new DecisionNode<DecisionData>
                            {
                                // Fourth Stage
                                Data = new()
                                {
                                    Title = "Get it."
                                }
                            }
                        }
                    }
                }
            };

            var item = new KeyValuePair<string, DecisionTree<DecisionData>>(id, decisionTree);

            return item;
        }
    }
}
