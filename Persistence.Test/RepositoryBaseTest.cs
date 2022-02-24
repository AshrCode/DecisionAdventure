using Common;
using Domain;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Persistence.Test
{
    [TestClass]
    public class RepositoryBaseTest
    {
        // Mock instance of object.
        private Mock<ILogger<RepositoryBase<DecisionTree<DecisionData>>>> mockLogger;
        private RepositoryBase<DecisionTree<DecisionData>> repositoryBase;

        [TestInitialize]
        public void Setup()
        {
            mockLogger = new Mock<ILogger<RepositoryBase<DecisionTree<DecisionData>>>>();
            repositoryBase = new(mockLogger.Object);

            // Test seed data.
            string key = "Test_101";
            DecisionTree<DecisionData> decisionTree = new()
            {
                Root = new DecisionNode<DecisionData> { }
            };
            _ = repositoryBase.Save(decisionTree, key);
        }

        [TestMethod]
        public async Task Save_IfUniqueKeyIsProvidedShouldSaveSucessfully()
        {
            // Arrange
            string key = "Test_101_UniqueKey";
            DecisionTree<DecisionData> decisionTree = new()
            {
                Root = new DecisionNode<DecisionData> { }
            };

            // Act
            await repositoryBase.Save(decisionTree, key);
            var dTree = await repositoryBase.Get(key);

            // Assert
            Assert.IsNotNull(dTree);
        }

        [TestMethod]
        public async Task Save_IfDuplicateKeyIsProvidedShouldThrowException()
        {
            // Arrange
            string key = "Test_101";
            DecisionTree<DecisionData> decisionTree = new()
            {
                Root = new DecisionNode<DecisionData> { }
            };

            // Assert
            await Assert.ThrowsExceptionAsync<ApiException>(() => repositoryBase.Save(decisionTree, key));
        }

        [TestMethod]
        public async Task Update_IfCorrectKeyIsProvidedShouldUpdateSucessfully()
        {
            // Arrange
            string key = "Test_101";
            DecisionTree<DecisionData> decisionTree = new()
            {
                Root = new DecisionNode<DecisionData>
                {
                    Data = new DecisionData
                    {
                        Title = "Sample Title",
                        IsSelected = true
                    }
                }
            };

            // Act
            await repositoryBase.Update(decisionTree, key);
            var dTree = await repositoryBase.Get(key);

            // Assert
            Assert.AreEqual(true, dTree.Root.Data.IsSelected);
        }

        [TestMethod]
        public async Task Update_IfNewKeyIsProvidedShouldThrowException()
        {
            // Arrange
            string key = "Test_102_NewKey";
            DecisionTree<DecisionData> decisionTree = new()
            {
                Root = new DecisionNode<DecisionData>()
            };

            // Assert
            await Assert.ThrowsExceptionAsync<ApiException>(() => repositoryBase.Update(decisionTree, key));
        }
    }
}
