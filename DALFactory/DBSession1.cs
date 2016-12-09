 
using IDAL;
using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DALFactory
{
	public partial class DBSession : IDBSession
    {
	




		private IBackInputDal _BackInputDal;
        public IBackInputDal BackInputDal
        {
            get
            {
                if(_BackInputDal == null)
                {
                    _BackInputDal = AbstractFactory.CreateBackInputDal();
                }
                return _BackInputDal;
            }
            set { _BackInputDal = value; }
        }
	




		private IBackOutputDal _BackOutputDal;
        public IBackOutputDal BackOutputDal
        {
            get
            {
                if(_BackOutputDal == null)
                {
                    _BackOutputDal = AbstractFactory.CreateBackOutputDal();
                }
                return _BackOutputDal;
            }
            set { _BackOutputDal = value; }
        }
	




		private IBusinessTypeDal _BusinessTypeDal;
        public IBusinessTypeDal BusinessTypeDal
        {
            get
            {
                if(_BusinessTypeDal == null)
                {
                    _BusinessTypeDal = AbstractFactory.CreateBusinessTypeDal();
                }
                return _BusinessTypeDal;
            }
            set { _BusinessTypeDal = value; }
        }
	




		private ICheckBillDal _CheckBillDal;
        public ICheckBillDal CheckBillDal
        {
            get
            {
                if(_CheckBillDal == null)
                {
                    _CheckBillDal = AbstractFactory.CreateCheckBillDal();
                }
                return _CheckBillDal;
            }
            set { _CheckBillDal = value; }
        }
	




		private ICheckDetailDal _CheckDetailDal;
        public ICheckDetailDal CheckDetailDal
        {
            get
            {
                if(_CheckDetailDal == null)
                {
                    _CheckDetailDal = AbstractFactory.CreateCheckDetailDal();
                }
                return _CheckDetailDal;
            }
            set { _CheckDetailDal = value; }
        }
	




		private ICompanyDal _CompanyDal;
        public ICompanyDal CompanyDal
        {
            get
            {
                if(_CompanyDal == null)
                {
                    _CompanyDal = AbstractFactory.CreateCompanyDal();
                }
                return _CompanyDal;
            }
            set { _CompanyDal = value; }
        }
	




		private ICostItemDal _CostItemDal;
        public ICostItemDal CostItemDal
        {
            get
            {
                if(_CostItemDal == null)
                {
                    _CostItemDal = AbstractFactory.CreateCostItemDal();
                }
                return _CostItemDal;
            }
            set { _CostItemDal = value; }
        }
	




		private ICostUnitListDal _CostUnitListDal;
        public ICostUnitListDal CostUnitListDal
        {
            get
            {
                if(_CostUnitListDal == null)
                {
                    _CostUnitListDal = AbstractFactory.CreateCostUnitListDal();
                }
                return _CostUnitListDal;
            }
            set { _CostUnitListDal = value; }
        }
	




		private ICostUnitListDetailDal _CostUnitListDetailDal;
        public ICostUnitListDetailDal CostUnitListDetailDal
        {
            get
            {
                if(_CostUnitListDetailDal == null)
                {
                    _CostUnitListDetailDal = AbstractFactory.CreateCostUnitListDetailDal();
                }
                return _CostUnitListDetailDal;
            }
            set { _CostUnitListDetailDal = value; }
        }
	




		private IDepartentDal _DepartentDal;
        public IDepartentDal DepartentDal
        {
            get
            {
                if(_DepartentDal == null)
                {
                    _DepartentDal = AbstractFactory.CreateDepartentDal();
                }
                return _DepartentDal;
            }
            set { _DepartentDal = value; }
        }
	




		private IDIYBillDal _DIYBillDal;
        public IDIYBillDal DIYBillDal
        {
            get
            {
                if(_DIYBillDal == null)
                {
                    _DIYBillDal = AbstractFactory.CreateDIYBillDal();
                }
                return _DIYBillDal;
            }
            set { _DIYBillDal = value; }
        }
	




		private IGiveBillDal _GiveBillDal;
        public IGiveBillDal GiveBillDal
        {
            get
            {
                if(_GiveBillDal == null)
                {
                    _GiveBillDal = AbstractFactory.CreateGiveBillDal();
                }
                return _GiveBillDal;
            }
            set { _GiveBillDal = value; }
        }
	




		private IGoodItemDal _GoodItemDal;
        public IGoodItemDal GoodItemDal
        {
            get
            {
                if(_GoodItemDal == null)
                {
                    _GoodItemDal = AbstractFactory.CreateGoodItemDal();
                }
                return _GoodItemDal;
            }
            set { _GoodItemDal = value; }
        }
	




		private IInOutTypeDal _InOutTypeDal;
        public IInOutTypeDal InOutTypeDal
        {
            get
            {
                if(_InOutTypeDal == null)
                {
                    _InOutTypeDal = AbstractFactory.CreateInOutTypeDal();
                }
                return _InOutTypeDal;
            }
            set { _InOutTypeDal = value; }
        }
	




		private IInWarehouseDal _InWarehouseDal;
        public IInWarehouseDal InWarehouseDal
        {
            get
            {
                if(_InWarehouseDal == null)
                {
                    _InWarehouseDal = AbstractFactory.CreateInWarehouseDal();
                }
                return _InWarehouseDal;
            }
            set { _InWarehouseDal = value; }
        }
	




		private IItemLineDal _ItemLineDal;
        public IItemLineDal ItemLineDal
        {
            get
            {
                if(_ItemLineDal == null)
                {
                    _ItemLineDal = AbstractFactory.CreateItemLineDal();
                }
                return _ItemLineDal;
            }
            set { _ItemLineDal = value; }
        }
	




		private IItemUnitDal _ItemUnitDal;
        public IItemUnitDal ItemUnitDal
        {
            get
            {
                if(_ItemUnitDal == null)
                {
                    _ItemUnitDal = AbstractFactory.CreateItemUnitDal();
                }
                return _ItemUnitDal;
            }
            set { _ItemUnitDal = value; }
        }
	




		private ILaborCostBillDal _LaborCostBillDal;
        public ILaborCostBillDal LaborCostBillDal
        {
            get
            {
                if(_LaborCostBillDal == null)
                {
                    _LaborCostBillDal = AbstractFactory.CreateLaborCostBillDal();
                }
                return _LaborCostBillDal;
            }
            set { _LaborCostBillDal = value; }
        }
	




		private ILineWayDal _LineWayDal;
        public ILineWayDal LineWayDal
        {
            get
            {
                if(_LineWayDal == null)
                {
                    _LineWayDal = AbstractFactory.CreateLineWayDal();
                }
                return _LineWayDal;
            }
            set { _LineWayDal = value; }
        }
	




		private ILineWayAndDealerDal _LineWayAndDealerDal;
        public ILineWayAndDealerDal LineWayAndDealerDal
        {
            get
            {
                if(_LineWayAndDealerDal == null)
                {
                    _LineWayAndDealerDal = AbstractFactory.CreateLineWayAndDealerDal();
                }
                return _LineWayAndDealerDal;
            }
            set { _LineWayAndDealerDal = value; }
        }
	




		private ILoadGoodsTypeDal _LoadGoodsTypeDal;
        public ILoadGoodsTypeDal LoadGoodsTypeDal
        {
            get
            {
                if(_LoadGoodsTypeDal == null)
                {
                    _LoadGoodsTypeDal = AbstractFactory.CreateLoadGoodsTypeDal();
                }
                return _LoadGoodsTypeDal;
            }
            set { _LoadGoodsTypeDal = value; }
        }
	




		private ILoadingAndLaborCostDetailDal _LoadingAndLaborCostDetailDal;
        public ILoadingAndLaborCostDetailDal LoadingAndLaborCostDetailDal
        {
            get
            {
                if(_LoadingAndLaborCostDetailDal == null)
                {
                    _LoadingAndLaborCostDetailDal = AbstractFactory.CreateLoadingAndLaborCostDetailDal();
                }
                return _LoadingAndLaborCostDetailDal;
            }
            set { _LoadingAndLaborCostDetailDal = value; }
        }
	




		private ILoadingExpensesBillDal _LoadingExpensesBillDal;
        public ILoadingExpensesBillDal LoadingExpensesBillDal
        {
            get
            {
                if(_LoadingExpensesBillDal == null)
                {
                    _LoadingExpensesBillDal = AbstractFactory.CreateLoadingExpensesBillDal();
                }
                return _LoadingExpensesBillDal;
            }
            set { _LoadingExpensesBillDal = value; }
        }
	




		private ILocationDal _LocationDal;
        public ILocationDal LocationDal
        {
            get
            {
                if(_LocationDal == null)
                {
                    _LocationDal = AbstractFactory.CreateLocationDal();
                }
                return _LocationDal;
            }
            set { _LocationDal = value; }
        }
	




		private ILocationChangeDal _LocationChangeDal;
        public ILocationChangeDal LocationChangeDal
        {
            get
            {
                if(_LocationChangeDal == null)
                {
                    _LocationChangeDal = AbstractFactory.CreateLocationChangeDal();
                }
                return _LocationChangeDal;
            }
            set { _LocationChangeDal = value; }
        }
	




		private IMenuDal _MenuDal;
        public IMenuDal MenuDal
        {
            get
            {
                if(_MenuDal == null)
                {
                    _MenuDal = AbstractFactory.CreateMenuDal();
                }
                return _MenuDal;
            }
            set { _MenuDal = value; }
        }
	




		private IMenuADal _MenuADal;
        public IMenuADal MenuADal
        {
            get
            {
                if(_MenuADal == null)
                {
                    _MenuADal = AbstractFactory.CreateMenuADal();
                }
                return _MenuADal;
            }
            set { _MenuADal = value; }
        }
	




		private IMenuBDal _MenuBDal;
        public IMenuBDal MenuBDal
        {
            get
            {
                if(_MenuBDal == null)
                {
                    _MenuBDal = AbstractFactory.CreateMenuBDal();
                }
                return _MenuBDal;
            }
            set { _MenuBDal = value; }
        }
	




		private IOtherExpenseListDal _OtherExpenseListDal;
        public IOtherExpenseListDal OtherExpenseListDal
        {
            get
            {
                if(_OtherExpenseListDal == null)
                {
                    _OtherExpenseListDal = AbstractFactory.CreateOtherExpenseListDal();
                }
                return _OtherExpenseListDal;
            }
            set { _OtherExpenseListDal = value; }
        }
	




		private IOtherExpenseListDetailDal _OtherExpenseListDetailDal;
        public IOtherExpenseListDetailDal OtherExpenseListDetailDal
        {
            get
            {
                if(_OtherExpenseListDetailDal == null)
                {
                    _OtherExpenseListDetailDal = AbstractFactory.CreateOtherExpenseListDetailDal();
                }
                return _OtherExpenseListDetailDal;
            }
            set { _OtherExpenseListDetailDal = value; }
        }
	




		private IOtherInputDal _OtherInputDal;
        public IOtherInputDal OtherInputDal
        {
            get
            {
                if(_OtherInputDal == null)
                {
                    _OtherInputDal = AbstractFactory.CreateOtherInputDal();
                }
                return _OtherInputDal;
            }
            set { _OtherInputDal = value; }
        }
	




		private IOtherOutputDal _OtherOutputDal;
        public IOtherOutputDal OtherOutputDal
        {
            get
            {
                if(_OtherOutputDal == null)
                {
                    _OtherOutputDal = AbstractFactory.CreateOtherOutputDal();
                }
                return _OtherOutputDal;
            }
            set { _OtherOutputDal = value; }
        }
	




		private IReceiveFileDal _ReceiveFileDal;
        public IReceiveFileDal ReceiveFileDal
        {
            get
            {
                if(_ReceiveFileDal == null)
                {
                    _ReceiveFileDal = AbstractFactory.CreateReceiveFileDal();
                }
                return _ReceiveFileDal;
            }
            set { _ReceiveFileDal = value; }
        }
	




		private IRecordDal _RecordDal;
        public IRecordDal RecordDal
        {
            get
            {
                if(_RecordDal == null)
                {
                    _RecordDal = AbstractFactory.CreateRecordDal();
                }
                return _RecordDal;
            }
            set { _RecordDal = value; }
        }
	




		private ISendFileDal _SendFileDal;
        public ISendFileDal SendFileDal
        {
            get
            {
                if(_SendFileDal == null)
                {
                    _SendFileDal = AbstractFactory.CreateSendFileDal();
                }
                return _SendFileDal;
            }
            set { _SendFileDal = value; }
        }
	




		private ITaskBillDal _TaskBillDal;
        public ITaskBillDal TaskBillDal
        {
            get
            {
                if(_TaskBillDal == null)
                {
                    _TaskBillDal = AbstractFactory.CreateTaskBillDal();
                }
                return _TaskBillDal;
            }
            set { _TaskBillDal = value; }
        }
	




		private ITransferBillDal _TransferBillDal;
        public ITransferBillDal TransferBillDal
        {
            get
            {
                if(_TransferBillDal == null)
                {
                    _TransferBillDal = AbstractFactory.CreateTransferBillDal();
                }
                return _TransferBillDal;
            }
            set { _TransferBillDal = value; }
        }
	




		private IUserDal _UserDal;
        public IUserDal UserDal
        {
            get
            {
                if(_UserDal == null)
                {
                    _UserDal = AbstractFactory.CreateUserDal();
                }
                return _UserDal;
            }
            set { _UserDal = value; }
        }
	




		private IUserFrameworkDal _UserFrameworkDal;
        public IUserFrameworkDal UserFrameworkDal
        {
            get
            {
                if(_UserFrameworkDal == null)
                {
                    _UserFrameworkDal = AbstractFactory.CreateUserFrameworkDal();
                }
                return _UserFrameworkDal;
            }
            set { _UserFrameworkDal = value; }
        }
	




		private IUserRelationDal _UserRelationDal;
        public IUserRelationDal UserRelationDal
        {
            get
            {
                if(_UserRelationDal == null)
                {
                    _UserRelationDal = AbstractFactory.CreateUserRelationDal();
                }
                return _UserRelationDal;
            }
            set { _UserRelationDal = value; }
        }
	




		private IWarehouseDal _WarehouseDal;
        public IWarehouseDal WarehouseDal
        {
            get
            {
                if(_WarehouseDal == null)
                {
                    _WarehouseDal = AbstractFactory.CreateWarehouseDal();
                }
                return _WarehouseDal;
            }
            set { _WarehouseDal = value; }
        }
	}	
}