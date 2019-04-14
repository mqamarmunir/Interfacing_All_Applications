using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public static class StaticCache
    {
        public static List<mi_tinstruments> GetAllInstruments(bool BypassCache)
        {
            using (var _unitofWork = new UnitOfWork())
            {
                var lstInstruments = _unitofWork.InstrumentsRepository.GetAll().ToList();
                return lstInstruments;
            }
        }


    }
}
