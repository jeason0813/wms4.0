 
using IBLL;
using DBModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
	
	public partial class BackInputService :BaseService<BackInput>,IBackInputService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.BackInputDal;
        }
    }   
	
	public partial class BackOutputService :BaseService<BackOutput>,IBackOutputService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.BackOutputDal;
        }
    }   
	
	public partial class BusinessTypeService :BaseService<BusinessType>,IBusinessTypeService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.BusinessTypeDal;
        }
    }   
	
	public partial class CheckBillService :BaseService<CheckBill>,ICheckBillService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.CheckBillDal;
        }
    }   
	
	public partial class CheckDetailService :BaseService<CheckDetail>,ICheckDetailService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.CheckDetailDal;
        }
    }   
	
	public partial class CompanyService :BaseService<Company>,ICompanyService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.CompanyDal;
        }
    }   
	
	public partial class CostItemService :BaseService<CostItem>,ICostItemService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.CostItemDal;
        }
    }   
	
	public partial class DepartentService :BaseService<Departent>,IDepartentService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.DepartentDal;
        }
    }   
	
	public partial class DIYBillService :BaseService<DIYBill>,IDIYBillService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.DIYBillDal;
        }
    }   
	
	public partial class GiveBillService :BaseService<GiveBill>,IGiveBillService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.GiveBillDal;
        }
    }   
	
	public partial class GoodItemService :BaseService<GoodItem>,IGoodItemService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.GoodItemDal;
        }
    }   
	
	public partial class InOutTypeService :BaseService<InOutType>,IInOutTypeService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.InOutTypeDal;
        }
    }   
	
	public partial class InWarehouseService :BaseService<InWarehouse>,IInWarehouseService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.InWarehouseDal;
        }
    }   
	
	public partial class ItemLineService :BaseService<ItemLine>,IItemLineService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.ItemLineDal;
        }
    }   
	
	public partial class ItemUnitService :BaseService<ItemUnit>,IItemUnitService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.ItemUnitDal;
        }
    }   
	
	public partial class LineWayService :BaseService<LineWay>,ILineWayService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.LineWayDal;
        }
    }   
	
	public partial class LoadGoodsTypeService :BaseService<LoadGoodsType>,ILoadGoodsTypeService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.LoadGoodsTypeDal;
        }
    }   
	
	public partial class LocationService :BaseService<Location>,ILocationService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.LocationDal;
        }
    }   
	
	public partial class LocationChangeService :BaseService<LocationChange>,ILocationChangeService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.LocationChangeDal;
        }
    }   
	
	public partial class MenuAService :BaseService<MenuA>,IMenuAService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.MenuADal;
        }
    }   
	
	public partial class MenuBService :BaseService<MenuB>,IMenuBService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.MenuBDal;
        }
    }   
	
	public partial class OtherInputService :BaseService<OtherInput>,IOtherInputService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.OtherInputDal;
        }
    }   
	
	public partial class OtherOutputService :BaseService<OtherOutput>,IOtherOutputService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.OtherOutputDal;
        }
    }   
	
	public partial class ReceiveFileService :BaseService<ReceiveFile>,IReceiveFileService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.ReceiveFileDal;
        }
    }   
	
	public partial class RecordService :BaseService<Record>,IRecordService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.RecordDal;
        }
    }   
	
	public partial class SendFileService :BaseService<SendFile>,ISendFileService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.SendFileDal;
        }
    }   
	
	public partial class TaskBillService :BaseService<TaskBill>,ITaskBillService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.TaskBillDal;
        }
    }   
	
	public partial class TransferBillService :BaseService<TransferBill>,ITransferBillService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.TransferBillDal;
        }
    }   
	
	public partial class UserService :BaseService<User>,IUserService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.UserDal;
        }
    }   
	
	public partial class UserFrameworkService :BaseService<UserFramework>,IUserFrameworkService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.UserFrameworkDal;
        }
    }   
	
	public partial class UserRelationService :BaseService<UserRelation>,IUserRelationService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.UserRelationDal;
        }
    }   
	
	public partial class WarehouseService :BaseService<Warehouse>,IWarehouseService
    {
    
		 public override void SetCurrentDal()
        {
            CurrentDal = this.CurrentDBSession.WarehouseDal;
        }
    }   
	
}