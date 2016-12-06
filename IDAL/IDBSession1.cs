﻿ 

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
	public partial interface IDBSession
    {

	
		IBackInputDal BackInputDal{get;set;}
	
		IBackOutputDal BackOutputDal{get;set;}
	
		IBusinessTypeDal BusinessTypeDal{get;set;}
	
		ICheckBillDal CheckBillDal{get;set;}
	
		ICheckDetailDal CheckDetailDal{get;set;}
	
		ICompanyDal CompanyDal{get;set;}
	
		ICostItemDal CostItemDal{get;set;}
	
		ICostUnitListDal CostUnitListDal{get;set;}
	
		ICostUnitListDetailDal CostUnitListDetailDal{get;set;}
	
		IDepartentDal DepartentDal{get;set;}
	
		IDIYBillDal DIYBillDal{get;set;}
	
		IGiveBillDal GiveBillDal{get;set;}
	
		IGoodItemDal GoodItemDal{get;set;}
	
		IInOutTypeDal InOutTypeDal{get;set;}
	
		IInWarehouseDal InWarehouseDal{get;set;}
	
		IItemLineDal ItemLineDal{get;set;}
	
		IItemUnitDal ItemUnitDal{get;set;}
	
		ILaborCostBillDal LaborCostBillDal{get;set;}
	
		ILineWayDal LineWayDal{get;set;}
	
		ILineWayAndDealerDal LineWayAndDealerDal{get;set;}
	
		ILoadGoodsTypeDal LoadGoodsTypeDal{get;set;}
	
		ILoadingAndLaborCostDetailDal LoadingAndLaborCostDetailDal{get;set;}
	
		ILoadingExpensesBillDal LoadingExpensesBillDal{get;set;}
	
		ILocationDal LocationDal{get;set;}
	
		ILocationChangeDal LocationChangeDal{get;set;}
	
		IMenuADal MenuADal{get;set;}
	
		IMenuBDal MenuBDal{get;set;}
	
		IOtherExpenseListDal OtherExpenseListDal{get;set;}
	
		IOtherExpenseListDetailDal OtherExpenseListDetailDal{get;set;}
	
		IOtherInputDal OtherInputDal{get;set;}
	
		IOtherOutputDal OtherOutputDal{get;set;}
	
		IReceiveFileDal ReceiveFileDal{get;set;}
	
		IRecordDal RecordDal{get;set;}
	
		ISendFileDal SendFileDal{get;set;}
	
		ITaskBillDal TaskBillDal{get;set;}
	
		ITransferBillDal TransferBillDal{get;set;}
	
		IUserDal UserDal{get;set;}
	
		IUserFrameworkDal UserFrameworkDal{get;set;}
	
		IUserRelationDal UserRelationDal{get;set;}
	
		IWarehouseDal WarehouseDal{get;set;}
	}	
}