using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService
{
    public interface ITestAttributesService
    {
        TestAttributeEntity GetProductById(int TestID);
        IEnumerable<TestAttributeEntity> GetAllProducts();
        int CreateProduct(TestAttributeEntity TestEntity);
        bool UpdateProduct(int TestID, TestAttributeEntity productEntity);
        bool DeleteProduct(int TestID);
    }
}
