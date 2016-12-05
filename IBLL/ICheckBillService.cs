using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL
{
    public partial interface ICheckBillService
    {
        string Save(CheckBill CheckBill, string userName);
        string Examine(Guid CheckBillId, string userName);
        string  GiveupExamine(Guid CheckBillId, string userName);

    }
}
