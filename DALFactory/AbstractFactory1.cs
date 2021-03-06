﻿ 

using IDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DALFactory
{
    public partial class AbstractFactory
    {
      
   
		
	    public static IBackInputDal CreateBackInputDal()
        {

		 string fullClassName = NameSpace + ".BackInputDal";
          return CreateInstance(fullClassName) as IBackInputDal;

        }
		
	    public static IBackOutputDal CreateBackOutputDal()
        {

		 string fullClassName = NameSpace + ".BackOutputDal";
          return CreateInstance(fullClassName) as IBackOutputDal;

        }
		
	    public static IBusinessTypeDal CreateBusinessTypeDal()
        {

		 string fullClassName = NameSpace + ".BusinessTypeDal";
          return CreateInstance(fullClassName) as IBusinessTypeDal;

        }
		
	    public static ICheckBillDal CreateCheckBillDal()
        {

		 string fullClassName = NameSpace + ".CheckBillDal";
          return CreateInstance(fullClassName) as ICheckBillDal;

        }
		
	    public static ICheckDetailDal CreateCheckDetailDal()
        {

		 string fullClassName = NameSpace + ".CheckDetailDal";
          return CreateInstance(fullClassName) as ICheckDetailDal;

        }
		
	    public static ICompanyDal CreateCompanyDal()
        {

		 string fullClassName = NameSpace + ".CompanyDal";
          return CreateInstance(fullClassName) as ICompanyDal;

        }
		
	    public static ICostItemDal CreateCostItemDal()
        {

		 string fullClassName = NameSpace + ".CostItemDal";
          return CreateInstance(fullClassName) as ICostItemDal;

        }
		
	    public static ICostTotalDal CreateCostTotalDal()
        {

		 string fullClassName = NameSpace + ".CostTotalDal";
          return CreateInstance(fullClassName) as ICostTotalDal;

        }
		
	    public static ICostUnitListDal CreateCostUnitListDal()
        {

		 string fullClassName = NameSpace + ".CostUnitListDal";
          return CreateInstance(fullClassName) as ICostUnitListDal;

        }
		
	    public static ICostUnitListDetailDal CreateCostUnitListDetailDal()
        {

		 string fullClassName = NameSpace + ".CostUnitListDetailDal";
          return CreateInstance(fullClassName) as ICostUnitListDetailDal;

        }
		
	    public static IDepartentDal CreateDepartentDal()
        {

		 string fullClassName = NameSpace + ".DepartentDal";
          return CreateInstance(fullClassName) as IDepartentDal;

        }
		
	    public static IDIYBillDal CreateDIYBillDal()
        {

		 string fullClassName = NameSpace + ".DIYBillDal";
          return CreateInstance(fullClassName) as IDIYBillDal;

        }
		
	    public static IErrorRecortDal CreateErrorRecortDal()
        {

		 string fullClassName = NameSpace + ".ErrorRecortDal";
          return CreateInstance(fullClassName) as IErrorRecortDal;

        }
		
	    public static IGiveBillDal CreateGiveBillDal()
        {

		 string fullClassName = NameSpace + ".GiveBillDal";
          return CreateInstance(fullClassName) as IGiveBillDal;

        }
		
	    public static IGoodItemDal CreateGoodItemDal()
        {

		 string fullClassName = NameSpace + ".GoodItemDal";
          return CreateInstance(fullClassName) as IGoodItemDal;

        }
		
	    public static IInOutRecordDetailDal CreateInOutRecordDetailDal()
        {

		 string fullClassName = NameSpace + ".InOutRecordDetailDal";
          return CreateInstance(fullClassName) as IInOutRecordDetailDal;

        }
		
	    public static IInOutTypeDal CreateInOutTypeDal()
        {

		 string fullClassName = NameSpace + ".InOutTypeDal";
          return CreateInstance(fullClassName) as IInOutTypeDal;

        }
		
	    public static IInWarehouseDal CreateInWarehouseDal()
        {

		 string fullClassName = NameSpace + ".InWarehouseDal";
          return CreateInstance(fullClassName) as IInWarehouseDal;

        }
		
	    public static IItemLineDal CreateItemLineDal()
        {

		 string fullClassName = NameSpace + ".ItemLineDal";
          return CreateInstance(fullClassName) as IItemLineDal;

        }
		
	    public static IItemUnitDal CreateItemUnitDal()
        {

		 string fullClassName = NameSpace + ".ItemUnitDal";
          return CreateInstance(fullClassName) as IItemUnitDal;

        }
		
	    public static ILaborCostBillDal CreateLaborCostBillDal()
        {

		 string fullClassName = NameSpace + ".LaborCostBillDal";
          return CreateInstance(fullClassName) as ILaborCostBillDal;

        }
		
	    public static ILineWayDal CreateLineWayDal()
        {

		 string fullClassName = NameSpace + ".LineWayDal";
          return CreateInstance(fullClassName) as ILineWayDal;

        }
		
	    public static ILineWayAndDealerDal CreateLineWayAndDealerDal()
        {

		 string fullClassName = NameSpace + ".LineWayAndDealerDal";
          return CreateInstance(fullClassName) as ILineWayAndDealerDal;

        }
		
	    public static ILoadGoodsTypeDal CreateLoadGoodsTypeDal()
        {

		 string fullClassName = NameSpace + ".LoadGoodsTypeDal";
          return CreateInstance(fullClassName) as ILoadGoodsTypeDal;

        }
		
	    public static ILoadingAndLaborCostDetailDal CreateLoadingAndLaborCostDetailDal()
        {

		 string fullClassName = NameSpace + ".LoadingAndLaborCostDetailDal";
          return CreateInstance(fullClassName) as ILoadingAndLaborCostDetailDal;

        }
		
	    public static ILoadingExpensesBillDal CreateLoadingExpensesBillDal()
        {

		 string fullClassName = NameSpace + ".LoadingExpensesBillDal";
          return CreateInstance(fullClassName) as ILoadingExpensesBillDal;

        }
		
	    public static ILocationDal CreateLocationDal()
        {

		 string fullClassName = NameSpace + ".LocationDal";
          return CreateInstance(fullClassName) as ILocationDal;

        }
		
	    public static ILocationChangeDal CreateLocationChangeDal()
        {

		 string fullClassName = NameSpace + ".LocationChangeDal";
          return CreateInstance(fullClassName) as ILocationChangeDal;

        }
		
	    public static IMenuDal CreateMenuDal()
        {

		 string fullClassName = NameSpace + ".MenuDal";
          return CreateInstance(fullClassName) as IMenuDal;

        }
		
	    public static IOtherExpenseListDal CreateOtherExpenseListDal()
        {

		 string fullClassName = NameSpace + ".OtherExpenseListDal";
          return CreateInstance(fullClassName) as IOtherExpenseListDal;

        }
		
	    public static IOtherExpenseListDetailDal CreateOtherExpenseListDetailDal()
        {

		 string fullClassName = NameSpace + ".OtherExpenseListDetailDal";
          return CreateInstance(fullClassName) as IOtherExpenseListDetailDal;

        }
		
	    public static IOtherInputDal CreateOtherInputDal()
        {

		 string fullClassName = NameSpace + ".OtherInputDal";
          return CreateInstance(fullClassName) as IOtherInputDal;

        }
		
	    public static IOtherOutputDal CreateOtherOutputDal()
        {

		 string fullClassName = NameSpace + ".OtherOutputDal";
          return CreateInstance(fullClassName) as IOtherOutputDal;

        }
		
	    public static IReceiveFileDal CreateReceiveFileDal()
        {

		 string fullClassName = NameSpace + ".ReceiveFileDal";
          return CreateInstance(fullClassName) as IReceiveFileDal;

        }
		
	    public static IRecordDal CreateRecordDal()
        {

		 string fullClassName = NameSpace + ".RecordDal";
          return CreateInstance(fullClassName) as IRecordDal;

        }
		
	    public static IRoleDal CreateRoleDal()
        {

		 string fullClassName = NameSpace + ".RoleDal";
          return CreateInstance(fullClassName) as IRoleDal;

        }
		
	    public static IRoleMenuRelationDal CreateRoleMenuRelationDal()
        {

		 string fullClassName = NameSpace + ".RoleMenuRelationDal";
          return CreateInstance(fullClassName) as IRoleMenuRelationDal;

        }
		
	    public static ISendFileDal CreateSendFileDal()
        {

		 string fullClassName = NameSpace + ".SendFileDal";
          return CreateInstance(fullClassName) as ISendFileDal;

        }
		
	    public static ITaskBillDal CreateTaskBillDal()
        {

		 string fullClassName = NameSpace + ".TaskBillDal";
          return CreateInstance(fullClassName) as ITaskBillDal;

        }
		
	    public static ITransferBillDal CreateTransferBillDal()
        {

		 string fullClassName = NameSpace + ".TransferBillDal";
          return CreateInstance(fullClassName) as ITransferBillDal;

        }
		
	    public static IUserDal CreateUserDal()
        {

		 string fullClassName = NameSpace + ".UserDal";
          return CreateInstance(fullClassName) as IUserDal;

        }
		
	    public static IUserFrameworkDal CreateUserFrameworkDal()
        {

		 string fullClassName = NameSpace + ".UserFrameworkDal";
          return CreateInstance(fullClassName) as IUserFrameworkDal;

        }
		
	    public static IUserRelationDal CreateUserRelationDal()
        {

		 string fullClassName = NameSpace + ".UserRelationDal";
          return CreateInstance(fullClassName) as IUserRelationDal;

        }
		
	    public static IUserRoleRelationDal CreateUserRoleRelationDal()
        {

		 string fullClassName = NameSpace + ".UserRoleRelationDal";
          return CreateInstance(fullClassName) as IUserRoleRelationDal;

        }
		
	    public static IWarehouseDal CreateWarehouseDal()
        {

		 string fullClassName = NameSpace + ".WarehouseDal";
          return CreateInstance(fullClassName) as IWarehouseDal;

        }
	}
	
}