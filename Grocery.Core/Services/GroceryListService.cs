using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;

namespace Grocery.Core.Services
{
    public class GroceryListService : IGroceryListService
    {
        private readonly IGroceryListRepository _groceryRepository;
        public GroceryListService(IGroceryListRepository groceryRepository)
        {
            _groceryRepository = groceryRepository;
        }
        public List<GroceryList> GetAll()
        {
            return _groceryRepository.GetAll();
        }
        public GroceryList Add(GroceryList item)
        {
            if (string.IsNullOrWhiteSpace(item.Name))
            {
                throw new ArgumentException("Naam is leeg", nameof(item.Name));
            }
            if (_groceryRepository.GetAll().Any(g => string.Equals(g.Name, item.Name, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException($"Lijst genaamd ({item.Name}) bestaat al");
            }
            return _groceryRepository.Add(item);
        }

        public GroceryList? Delete(GroceryList item)
        {
            throw new NotImplementedException();
        }

        public GroceryList? Get(int id)
        {
            return _groceryRepository.Get(id);
        }

        public GroceryList? Update(GroceryList item)
        {
            return _groceryRepository.Update(item);
        }
    }
}
