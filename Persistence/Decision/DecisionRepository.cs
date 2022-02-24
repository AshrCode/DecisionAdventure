using Application;
using Domain;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence
{
    public class DecisionRepository : RepositoryBase<DecisionTree<DecisionData>>, IDecisionRepo<DecisionData>
    {
        public DecisionRepository(ILogger<DecisionRepository> logger)
            : base(logger)
        {
            if (_storage.Count < 1)
            {
                // Seeding sample data.
                List<KeyValuePair<string, DecisionTree<DecisionData>>> items = GetSampleDecisionData();
                items.ForEach(x => _storage.Add(x.Key, x.Value));

                _logger.LogInformation("Sample decision tree data has been seeded.");
            }
        }

        public async Task<DecisionTree<DecisionData>> GetById(string key)
        {
            /*
             * Here, we can add additional repository level functionality in future if needed.
             */

            return await Get(key);
        }

        public async Task AddDecision(DecisionTree<DecisionData> decisionTree, string key)
        {
            /*
             * Here, we can add additional repository level functionality in future if needed.
             */

            await Save(decisionTree, key);
        }
        
        public async Task UpdateDecision(DecisionTree<DecisionData> decisionTree, string key)
        {
            /*
             * Here, we can add additional repository level functionality in future if needed.
             */

            await Update(decisionTree, key);
        }

        private static List<KeyValuePair<string, DecisionTree<DecisionData>>> GetSampleDecisionData()
        {
            List<KeyValuePair<string, DecisionTree<DecisionData>>> items = new();

            // Sample Data 1
            string sampleKey1 = "9A0FA5F7048544F2A7EFBF066F8AA9A9";
            DecisionTree<DecisionData> sampleDecisionTree1 = new()
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
            var sampleData1 = new KeyValuePair<string, DecisionTree<DecisionData>>(sampleKey1, sampleDecisionTree1);
            items.Add(sampleData1);

            // Sample Data 2
            string sampleKey2 = "TEST_Key";
            DecisionTree<DecisionData> sampleDecisionTree2 = new()
            {
                Root = new DecisionNode<DecisionData>
                {
                    Data = new DecisionData
                    {
                        Title = "Sample Title 2"
                    }
                }
            };
            var sampleData2 = new KeyValuePair<string, DecisionTree<DecisionData>>(sampleKey2, sampleDecisionTree2);
            items.Add(sampleData2);

            return items;
        }
    }
}
