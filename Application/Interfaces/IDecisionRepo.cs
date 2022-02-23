using Domain;
using System.Threading.Tasks;

namespace Application
{
    public interface IDecisionRepo<T>
    {
        Task<DecisionTree<T>> GetById(string key);

        Task SaveOrUpdate(DecisionTree<T> decisionTree, string key);
    }
}
