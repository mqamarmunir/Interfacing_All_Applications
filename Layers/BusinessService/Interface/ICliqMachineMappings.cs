using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService
{
    public interface ICliqMachineMappings
    {
        CliqMachineEntity GetById(int ID);
        IEnumerable<CliqMachineEntity> GetAll();

        IEnumerable<CliqMachineEntity> GetMany(Func<CliqMachineEntity, bool> where);
        long Create(CliqMachineEntity Entity);
        bool Update(int ID, CliqMachineEntity Entity);
        bool Delete(int ID);
    }
}
