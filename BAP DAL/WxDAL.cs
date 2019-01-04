using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using BAP_WeChart.Utility; 
using System.Data;
using System.Configuration;
using WSERP.Common;
using System.IO; 
using Wellshsoft.Net.Data;
using Wellshsoft.Net;
using WSERP.DataAccess;
using BAP_Model;
using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BAP_DAL
{
    public class WxDAL
    {  
        /// <summary>
        /// 保存派工
        /// </summary>
        /// <param name="ProjectId"></param>    
        /// <param name="ElementId"></param>   
        /// <param name="ResourceId"></param>   
        /// <param name="ResourceName"></param>
        /// <returns></returns>
        public string SaveDispatch(string ProjectId, string ElementId, string ResourceId, string ResourceName)
        {
            string Id = "";
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"  insert into ProjectResource  ( [ProjId],    [ElementId], [Rid],         [Rname],      CostPrice,JID,PID,IsExtenal,Rtype, Prop,   EX01, EX02, EX03, EX04, EX05, EX06, CmpId,  [EX16],      [EX17],     [EX18]      ) 
						                             values( @ProjectId,  @ElementId,  @ResourceId, @ResourceName,   0,        '',  '', 1,        1,     0,     '',  '',   '',   '',   '',   '',   '',      getdate(),  getdate(),  getdate()   ) 
						      select @@IDENTITY as Id ");
                SqlParameter[] sqlpara = new SqlParameter[] { 
                    new SqlParameter("@ProjectId",ProjectId) ,
                    new SqlParameter("@ElementId",ElementId) ,
                    new SqlParameter("@ResourceId",ResourceId) ,
                    new SqlParameter("@ResourceName",ResourceName)  
                }; 
                DataTable dt= DbHelperSQL.ExecuteDataTable(sb.ToString(), sqlpara); 
                if (dt != null && dt.Rows.Count > 0)
                {
                    Id = dt.Rows[0]["Id"].ToString();
                }
            }
            catch (Exception ex)
            {
                UtilityTools.WriteTxt("//Log//Novo SaveDispatch ", "  exception :" + ex.Message);
            }
            return Id;
        }

        /// <summary>
        /// 员工报工打卡
        /// </summary> 
        public bool EmployeeSwipeCard(string EID, string TaskCode, string SwipeCardType, string SwipeCardTime, string ProjectId, string Latitude, string Longitude, string StreetName, out string PkId)
        { 
            try
            { 
                StringBuilder sb = new StringBuilder();
                sb.Append(@"  
                              declare @IndicatorId varchar(20)
                              set @IndicatorId='1';
                              begin tran 
                              begin try 
                              insert into EmployeeSwipeCard  (  EID,   TaskCode,  SwipeCardType,  SwipeCardTime ,  ProjectId , Latitude  , Longitude , StreetName  ) 
						                             values  ( @EID,  @TaskCode, @SwipeCardType, @SwipeCardTime , @ProjectId ,@Latitude  ,@Longitude ,@StreetName  ) ;
                              update ProjectResource set ex12 = @SwipeCardType where ProjId=@ProjectId and ElementId=@TaskCode  and Rid=@EID ;
                              end try
                              begin catch 
                                 set @IndicatorId='0'
                                 if(@@trancount>0)   --全局变量@@trancount，事务开启此值+1，他用来判断是有开启事务
                                     rollback tran  
                              end catch 
                              if(@@trancount>0)
                              commit tran  
						      select @@IDENTITY as Id ,@IndicatorId as IndicatorId ");
                SqlParameter[] sqlpara = new SqlParameter[] { 
                    new SqlParameter("@EID",EID) , 
                    new SqlParameter("@TaskCode",TaskCode)   , 
                    new SqlParameter("@SwipeCardType",SwipeCardType)   , 
                    new SqlParameter("@SwipeCardTime",SwipeCardTime)   , 
                    new SqlParameter("@ProjectId",ProjectId)   , 
                    new SqlParameter("@Latitude",Latitude)   , 
                    new SqlParameter("@Longitude",Longitude)   , 
                    new SqlParameter("@StreetName",StreetName)    
                };
                DataTable dt = DbHelperSQL.ExecuteDataTable(sb.ToString(), sqlpara); 
                if (dt != null && dt.Rows.Count > 0)
                {
                    PkId = dt.Rows[0]["Id"].ToString();
                    string IndicatorId = dt.Rows[0]["IndicatorId"].ToString();
                    if (IndicatorId == "1")
                        return true;
                    else
                        return false;
                }
                else
                {
                    PkId = "";
                    return false;
                }
            }
            catch (Exception ex)
            {
                UtilityTools.WriteTxt("//Log//Novo EmployeeSwipeCard ", "  exception :" + ex.Message);
                PkId = "";
                return false;
            } 
        }
         
        public DataTable GetTestList(string statusId, string pageNumber, string pageSize)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"  With TemporaryTable as   
                               ( 
                                select notice.EmpRepairsCode,  notice.orderId, dept.deptName,notice.devicecode, notice.createdate,dispatch.dispatchfromdate,dispatch.EmpRepairsCode as DispatchEmpRepairsCode,notice.worktiem,
                                  Case  when (notice.BillStatus=10 ) then '未完成' 
                                        when notice.BillStatus=80   then '已完成'  
							          else
								        case when notice.ex16=0 then '待维修'
								        when notice.EX16=1 then '维修中'
							           end
							          end as '工单状态' ,
                                      notice.BillStatus as orderStatusId, 
                                      '0'  as    IsTakeOrderPersonFlag ,
                                      applicant.eshow as '报修人',
							          notice.EmpPhone as '报修人联系电话' ,
							          notice.maintainLocation as '报修地点',  
                                      notice.NoticeDescription as '报修描述'  , row_number()over( order by notice.createdate desc ) as rownum 
                                          from Hq_Maintainnotice notice
                                          left join Hq_Maintaindispatch dispatch on notice.OrderId=dispatch.NoticeCode
                                          left join Hq_defdept dept on dept.DeptCode=notice.deptcode 
                                          left join Employees applicant on applicant.EID= notice.EmpRepairsCode    where notice.BillStatus = @statusId ");
                sb.Append(@"   )   
                                select * from TemporaryTable where TemporaryTable.rownum BETWEEN ((" + pageNumber + "-1)*" + pageSize + "+1) AND (" + pageNumber + "*" + pageSize + ")  ");
             
                SqlParameter[] sqlpara = new SqlParameter[] {  new SqlParameter("@statusId",statusId) };

                DataTable dt = DbHelperSQL.ExecuteDataTable(sb.ToString(), sqlpara);
                return dt;
            }
            catch
            {
                return null;
            }
        }

        public DataTable GetTestDetails(string orderId)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"             select  Notice.OrderId as '工单编号' , 
                                                  Case   when (notice.BillStatus=10 ) then '未审核'  
                                                    when (notice.BillStatus=80 and (notice.Grade IS null OR notice.Grade='' ) ) then '已维修' 
                                                   when (notice.BillStatus=80 and (notice.Grade IS not null OR notice.Grade<>'' ) ) then '已评价' 
                                                   when notice.BillStatus=40  then '已撤回' 
                                                    when notice.BillStatus=10  then '未受理' 
                                                    when notice.BillStatus=20  then '已驳回' 
							                      else
								                    case when notice.ex16=0 then '待维修'
								                    when notice.EX16=1 then '维修中'
							                       end
							                      end as '工单状态',
                                                  notice.CreateDate as '创建时间',
                                                  applicant.EShow as '报修人',
                                                  department.DeptName as '所属科室',
                                                  notice.deptCode as orderDepartmentId, 
                                                  notice.EmpPhone as '报修人联系电话',
                                                  notice.DeviceCode as '设备码',
                                                  notice.maintainLocation as '报修地点',  
                                                  notice.NoticeDescription as '报修描述',
                                                  maintainer.EShow as '维修人',
                                                  maintainer.EID as '维修人工号',
                                                  maintainer.mobilePhone as '维修人员电话',
                                                  notice.ex18 as '派工日期',
                                                  notice.worktiem as  '完成时间',
                                                  notice.Remark as '备注',
                                                  notice.FinishRemark as '完成维修描述',
                                                  notice.EX4 as ApplicantRemark,
                                                  notice.grade as '评分',
                                                  notice.Evaluate as '评价'  
                                   from Hq_Maintainnotice notice
                              left join Hq_Maintaindispatch dispatch on notice.OrderId=dispatch.NoticeCode
                              left join Hq_defdept dept on dept.DeptCode=notice.deptcode 
                              left join Employees applicant on applicant.EID= notice.EmpRepairsCode
                              left join Employees maintainer on maintainer.EID=notice.EX13
                              left join Hq_defdept department on department.DeptCode=notice.DeptCode  
                               where Notice.OrderId= @orderId   order by notice.createdate desc  ");
                SqlParameter[] sqlpara = new SqlParameter[] { 
                    new SqlParameter("@orderId",orderId) 
                };

                DataTable dt = DbHelperSQL.ExecuteDataTable(sb.ToString(), sqlpara);
                return dt;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取物资申请详情
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public DataTable getGoodsAndMaterialsDetails(string orderid)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"  SELECT  APOrderId AS OrderId,Departments.Dname as DeptName ,Departments.DID as DeptId ,Employees.eshow as ApplicationMan,
                                      AssetApply.ex12 as ApplicationPhone,AssetApply.CreateDate as ApplicationDate, AssetApply.Remark , AssetApply.EX10 as ApprovalRemark , 
                                        Case when  AssetApply.BillStatus = 80 then '已完成'
                                             when  AssetApply.BillStatus = 60 then '已派工'
                                             when  AssetApply.BillStatus = 110 then '已过账'
                                             when  AssetApply.BillStatus = 40 then '已撤回'
                                             when  AssetApply.BillStatus = 10 then '未审批'
                                             when  AssetApply.BillStatus = 50 then '已批准'
                                             when  AssetApply.BillStatus = 20 then '已驳回'
                                             else  ''
                                             end as BillStatus 
                                FROM  AssetApply
                                left join Departments on Departments.DID =AssetApply.DID 
                                left join Employees on employees.eid= AssetApply.APEid
                                 where APOrderId =  @orderid ");
                SqlParameter[] sqlpara = new SqlParameter[] {  
                    new SqlParameter("@orderid",orderid) 
                };

                DataTable dt = AssetDBHelper.ExecuteDataTable(sb.ToString(), sqlpara);
                return dt;
            }
            catch
            { return null; }
        }

        /// <summary>
        /// 根据OrderId获取物资申请或归还物资列表
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public DataTable GetGoodsAndMaterialsMaterialsByOrderId(string orderid, int PType)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"    select MINTD.Mid,MINTD.Itemsummary,MINTD.BUnit,MINTD.Q, MtrlDetail.ImgUrl,MINTD.UP AS Price , 
                                     CONVERT(DECIMAL(18,2), COALESCE(C.CQ, 0.0)) AS CQ 
                                     from MINTD  
                                 left join  MtrlBase A  on A.Mid =MINTD.Mid
                                left join MtrlDetail on MINTD.MID=MtrlDetail.MID  
                                LEFT OUTER JOIN (
	                                        SELECT Mid,Whpkid,SUM(CQ) AS CQ, SUM(Price) Price FROM 
	                                        (
		                                        SELECT Mid, WhId AS Whpkid, SUM(CrntQ) AS CQ, AVG(PRICEMOVE) AS Price
		                                        FROM MtrlStockState
		                                        GROUP BY Mid, WhId
		                                        UNION ALL
		                                        SELECT Mid,Whpkid, SUM(CASE WHEN Ptype IN (60,70,80,100,110,7300) THEN Q*-1.0 ELSE Q END) AS CQ,  Price=0.0
		                                        FROM MINTD
		                                        WHERE BillStatus IN (10,15,20,50)
		                                        GROUP BY Mid, Whpkid
	                                        )M
	                                        GROUP BY Mid, Whpkid
                                        )C ON A.Mid=C.Mid  AND C.WhPkid = MINTD.WhPkid 
                                  where PID     =  @orderid  and Ptype=@Ptype ");
                SqlParameter[] sqlpara = new SqlParameter[] { new SqlParameter("@orderid", orderid), new SqlParameter("@Ptype", PType) };
                DataTable dt = AssetDBHelper.ExecuteDataTable(sb.ToString(), sqlpara);
                return dt;
            }
            catch
            { return null; }
        }


        public bool TestFinish(string orderId)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"    update Hq_Maintainnotice 
                                  set billstatus = 80  
                                  where orderId=@orderId ");
                SqlParameter[] sqlpara = new SqlParameter[] { 
                    new SqlParameter("@orderId",orderId) 
                };

                int i = DbHelperSQL.ExecuteNonQuery(sb.ToString(), sqlpara);

                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }
         

        /// <summary>
        /// 获取派工时的项目节点
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public DataTable getProjectElementByProjectId(string ProjectId,string EID)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@" select 
                             ( select COUNT(*) from  ProjectStructs b  where b.Pid= @ProjectId  and b.ParentElementId=a.ElementId and  b.ElementId<> b.ParentElementId  ) as SubItemsCount ,
                                (  select  COUNT(c.ElementId) 
			                          from  ProjectStructs c 
		                         left join ProjectActivity P1 on c.ElementId=P1.ActId 
		                         left join WBS w1 on  c.ElementId=w1.Wid 
		                         left join ProjectNetwork n1 on c.ElementId=n1.NId  
		                         where c.Pid= @ProjectId and c.ElementId=a.ElementId
				                           and (  exists (select 1 from ProjectActivity p2 where p2.ActId=p1.ActId and p2.OwnerEid = @EID ) 
					                           or exists (select 1 from WBS w2 where w2.WId=w1.WId and w2.OwnerEid='01001' ) 
					                           or exists (select 1 from ProjectNetwork n2 where n2.Nid=N1.Nid and N2.OwnerEid= @EID  )  
					                           ) ) as IsPersonInCharge ,*
                        from  ProjectStructs a where a.Pid = @ProjectId and a.ParentElementId = @ProjectId and a.ElementId<>a.ParentElementId ");
                SqlParameter[] sqlpara = new SqlParameter[] { 
                    new SqlParameter("@ProjectId",ProjectId)  , 
                    new SqlParameter("@EID",EID)  
                };

                DataTable dt = DbHelperSQL.ExecuteDataTable(sb.ToString(), sqlpara);
                return dt;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取派工时的项目节点的子节点
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public DataTable getSubElementIdByProjectIdAndElementId(string ProjectId, string ElementId,string EID)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"    select (  select COUNT(*) from  ProjectStructs b  where b.Pid= @ProjectId and b.ParentElementId=a.ElementId and  b.ElementId<> b.ParentElementId  ) as SubItemsCount ,
                                 (  select  COUNT(c.ElementId) 
			                          from  ProjectStructs c 
		                         left join ProjectActivity P1 on c.ElementId=P1.ActId 
		                         left join WBS w1 on  c.ElementId=w1.Wid 
		                         left join ProjectNetwork n1 on c.ElementId=n1.NId  
		                         where c.Pid=@ProjectId and c.ElementId=a.ElementId
				                           and (  exists (select 1 from ProjectActivity p2 where p2.ActId=p1.ActId and p2.OwnerEid = @EID ) 
					                           or exists (select 1 from WBS w2 where w2.WId=w1.WId and w2.OwnerEid = @EID ) 
					                           or exists (select 1 from ProjectNetwork n2 where n2.Nid=N1.Nid and N2.OwnerEid = @EID )  
					                           ) ) as IsPersonInCharge ,
                                          *
                                 from  ProjectStructs a where a.Pid=@ProjectId and a.ParentElementId=@ElementId and a.ElementId<>a.ParentElementId  ");
                SqlParameter[] sqlpara = new SqlParameter[] { 
                    new SqlParameter("@ProjectId",ProjectId), 
                    new SqlParameter("@ElementId",ElementId)  , 
                    new SqlParameter("@EID",EID)  
                };

                DataTable dt = DbHelperSQL.ExecuteDataTable(sb.ToString(), sqlpara);
                return dt;
            }
            catch
            { return null;}
        }

        /// <summary>
        /// Novo登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Novo_Login(string username, string password,out string  EID,out string  EShow)
        {
            try
            {
                string testDecode = Wellshsoft.Net.DataProtector.Decode(DataConvert.ToString<object>(password)); 
                string EncodePwd = Wellshsoft.Net.DataProtector.Encode(DataConvert.ToString<object>(password)); 
                StringBuilder sb = new StringBuilder();
                sb.Append(@"  SELECT top 1 [Users].EID,Users.Uid,Users.Pwd,employees.EShow
                                        FROM [Users] 
                                    left join Employees on Users.EID=Employees.EID 
                                where Uid=@username and Pwd=@EncodePwd 
                            ");
                SqlParameter[] sqlpara = new SqlParameter[] { 
                    new SqlParameter("@username", username) ,
                    new SqlParameter("@EncodePwd", EncodePwd) 
                };

                DataTable dt = DbHelperSQL.ExecuteDataTable(sb.ToString(), sqlpara);

                if (dt != null && dt.Rows.Count > 0)
                {
                    EID = dt.Rows[0]["EID"].ToString();
                    EShow = dt.Rows[0]["EShow"].ToString();
                    return true;
                }
                else
                {
                    EID = "";
                    EShow = "";
                    return false;
                }
            }
            catch
            { 
                EID = "";
                EShow = "";
                return false;
            }
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ApprovalCenter_Login(string username, string password, out string EID, out string EShow)
        {
            try
            {
                string testDecode = Wellshsoft.Net.DataProtector.Decode(DataConvert.ToString<object>(password));
                string EncodePwd = Wellshsoft.Net.DataProtector.Encode(DataConvert.ToString<object>(password));
                StringBuilder sb = new StringBuilder();
                sb.Append(@"  SELECT top 1 [Users].EID,Users.Uid,Users.Pwd,employees.EShow
                                        FROM [Users] 
                                    left join Employees on Users.EID=Employees.EID 
                                where Uid=@username and Pwd=@EncodePwd 
                            ");
                SqlParameter[] sqlpara = new SqlParameter[] { 
                    new SqlParameter("@username", username) ,
                    new SqlParameter("@EncodePwd", EncodePwd) 
                };

                DataTable dt = AssetDBHelper.ExecuteDataTable(sb.ToString(), sqlpara);

                if (dt != null && dt.Rows.Count > 0)
                {
                    EID = dt.Rows[0]["EID"].ToString();
                    EShow = dt.Rows[0]["EShow"].ToString();
                    return true;
                }
                else
                {
                    EID = "";
                    EShow = "";
                    return false;
                }
            }
            catch
            {
                EID = "";
                EShow = "";
                return false;
            }
        }

        /// <summary>
        /// 获取待审批列表
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="EID"></param> 
        /// <returns></returns>
        public DataTable getListForApproval(string pageNumber, string pageSize, string EID)
        { 
            try
            {
                string UID = GetUIDByEID(EID);
                StringBuilder sb = new StringBuilder();
                string WFMbidSqlStr = GetWFMbidSqlStr(); 
                sb.Append(@"  With TemporaryTable as   
                             ( 
                                    select * ,  row_number()over( order by a.ApplicationDate desc ) as rownum    from (  
                                   SELECT distinct APOrderId AS OrderId,Departments.Dname as DeptName,Employees.EShow as ApplicationMan,AssetApply.ex12 as ApplicationPhone,AssetApply.CreateDate as ApplicationDate, CONVERT(VARCHAR(8000),AssetApply.Remark ) as Remark  , 
                                        Case when  AssetApply.BillStatus = 10 then '未审批' 
                                             else  ''
                                             end as BillStatus   ,
                                       BillStatus as BillStatusId, '物资领用申请' as 'BillType' , '00000000-0000-0000-0000-000001500000' as 'WFMId' ,'' as 'IsApprover'
                                   FROM   AssetApply
                                left join Departments on Departments.DID =AssetApply.DID 
                                left join Employees on employees.eid= AssetApply.APEid  
                                left join WorkFlowPhasePostil A ON A.BillNo=AssetApply.APOrderId
								LEFT JOIN WFFlowPhase AS B ON A.WFMbid = B.WFMbid
                                where BillStatus = 10 " + WFMbidSqlStr + @" 

                                         union all 

                                SELECT distinct OrderId AS OrderId,Departments.Dname as DeptName,Employees.EShow as ApplicationMan,PurchApplication.ex12 as ApplicationPhone,PurchApplication.CreateDate as ApplicationDate, CONVERT(VARCHAR(8000),PurchApplication.Remark) as Remark   , 
                                        Case when  PurchApplication.BillStatus = 10 then '未审批'  
                                             else  ''
                                             end as BillStatus   ,
                                       BillStatus as BillStatusId,  '物资采购申请' as 'BillType'  , '00000000-0000-0000-0000-000000000970' as 'WFMId' ,'' as 'IsApprover'
                                   FROM   PurchApplication
                                left join Departments on Departments.DID =PurchApplication.DID 
                                left join Employees on employees.eid= PurchApplication.Eid  
                                left join WorkFlowPhasePostil A ON A.BillNo=PurchApplication.OrderId
								LEFT JOIN WFFlowPhase AS B ON A.WFMbid = B.WFMbid
                                where BillStatus =  10   " + WFMbidSqlStr + @"

                                         union all

                                SELECT distinct OrderId AS OrderId,'' as DeptName,Employees.EShow as ApplicationMan,PurchOrder.ex12 as ApplicationPhone,PurchOrder.CreateDate as ApplicationDate,  CONVERT(VARCHAR(8000),PurchOrder.Remark) as Remark    , 
                                        Case when  PurchOrder.BillStatus = 10 then '未审批'  
                                             else  ''
                                             end as BillStatus   ,
                                       BillStatus as BillStatusId,  '物资采购订单' as 'BillType'  , '00000000-0000-0000-0000-000000000900' as 'WFMId' ,'' as 'IsApprover'
                                   FROM   PurchOrder 
                                left join users on users.Uid=PurchOrder.Uid
                                left join Employees on employees.eid= users.Eid  
                                left join WorkFlowPhasePostil A ON A.BillNo=PurchOrder.OrderId
								LEFT JOIN WFFlowPhase AS B ON A.WFMbid = B.WFMbid
                                where BillStatus =  10  " + WFMbidSqlStr + @"  

                                ) as a   ");
                sb.Append(@"   )   
                                select * from TemporaryTable where TemporaryTable.rownum BETWEEN ((" + pageNumber + "-1)*" + pageSize + "+1) AND (" + pageNumber + "*" + pageSize + ")  ");
                SqlParameter[] sqlpara = new SqlParameter[] { new SqlParameter("@UID", UID) };
                DataTable dt = AssetDBHelper.ExecuteDataTable(sb.ToString(), sqlpara);
                return dt;
            }
            catch
            { return null; }
        }

        /// <summary>
        ///  获取物资采购订单详情 
        /// </summary> 
        public DataTable getPurchOrderDetails(string orderid)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"   SELECT   PurchOrder.OrderId as OrderId,
                                    '' as DeptId,'' as DeptName,
                                   Employees.EID, Employees.EShow as CreatorName,PurchOrder.BillStatus as BillStatusId,
                                    Case when  PurchOrder.BillStatus = 10 then '未审批'  
                                             else  ''
                                             end as BillStatus  , PurchOrder.Remark,PurchOrder.ex10 as ApprovalRemark,PurchOrder.CreateDate,
                                    TotalABD as TotalPriceTaxExclusive,
                                    TotalA  as TotalPriceTaxInclusive ,
                                    '' as ApplicationMan,'' as ApplicationPhone, CreateDate as ApplicationDate 
                                  FROM  PurchOrder          
                                  left join users on users.Uid=PurchOrder.UID
                                  left join Employees on Employees.EID = users.EID           
                                 where  OrderId  = @orderid ");
                SqlParameter[] sqlpara = new SqlParameter[] {  
                    new SqlParameter("@orderid",orderid) 
                };
                DataTable dt = AssetDBHelper.ExecuteDataTable(sb.ToString(), sqlpara);
                return dt;
            }
            catch
            { return null; }
        } 

        /// <summary>
        /// 获取物资退库详情
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public DataTable getGoodsAndMaterialsReturnDetails(string orderid)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"  SELECT  AROrderId AS OrderId,Departments.Dname as DeptName ,Departments.DID as DeptId ,Employees.eshow as ApplicationMan,AssetReturn.ex12 as ApplicationPhone,AssetReturn.CreateDate as ApplicationDate,AssetReturn.Remark ,AssetReturn.ex10 as ApprovalRemark , 
                                        Case when  AssetReturn.BillStatus = 80 then '已完成' 
                                             when  AssetReturn.BillStatus = 110 then '已过账'
                                             when  AssetReturn.BillStatus = 40 then '已撤回'
                                             when  AssetReturn.BillStatus = 10 then '未审批'
                                             when  AssetReturn.BillStatus = 50 then '已批准'
                                             when  AssetReturn.BillStatus = 20 then '已驳回'
                                             else  ''
                                             end as BillStatus 
                                FROM  AssetReturn
                                left join Departments on Departments.DID =AssetReturn.DID 
                                left join Employees on employees.eid= AssetReturn.APEid
                                 where AROrderId =  @orderid ");
                SqlParameter[] sqlpara = new SqlParameter[] { new SqlParameter("@orderid", orderid) };
                DataTable dt = AssetDBHelper.ExecuteDataTable(sb.ToString(), sqlpara);
                return dt;
            }
            catch
            { return null; }
        }

        /// <summary>
        /// 获取物资采购订单详情物资列表
        /// </summary> 
        public DataTable getMeterialDetailsForPurchOrder(string orderid)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"  select Mid,Q,ItemSummary, 
                               UnitSummary as Bunit, '' as Q, '' AS PRICE, '' as ImgUrl, '' as CQ , 
                                U as PriceTaxExclusive,AU as PriceTaxInclusive  from PurchOrderDetail  where  OrderId =  @orderid ");
                SqlParameter[] sqlpara = new SqlParameter[] {  
                    new SqlParameter("@orderid",orderid) 
                };
                DataTable dt = AssetDBHelper.ExecuteDataTable(sb.ToString(), sqlpara);
                return dt;
            }
            catch
            { return null; }
        }

        /// <summary>
        /// 获取物资采购申请详情物资列表
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public DataTable getMeterialDetailsForPurchApplication(string orderid)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"  select Mid,Q,ItemSummary,UnitSummary,
                               UnitSummary as Bunit, '' as Q, '' AS PRICE, '' as ImgUrl, '' as CQ 
                              from PurchApplicationDetail  where OrderId  =  @orderid ");
                SqlParameter[] sqlpara = new SqlParameter[] {  
                    new SqlParameter("@orderid",orderid) 
                };
                DataTable dt = AssetDBHelper.ExecuteDataTable(sb.ToString(), sqlpara);
                return dt;
            }
            catch
            { return null; }
        }

        /// <summary>
        ///  获取物资采购申请详情
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public DataTable getPurchApplicationDetails(string orderid)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"     SELECT   PurchApplication.OrderId as OrderId,
                                    Departments.DID as DeptId,Departments.Dname as DeptName, '' as ApplicationMan, '' as ApplicationPhone,
                                   Employees.EID, Employees.EShow as CreatorName,PurchApplication.BillStatus as BillStatusId,
                                    Case when  PurchApplication.BillStatus = 10 then '未审批'  
                                             else  ''
                                             end as BillStatus  ,
                                    PurchApplication.Remark,PurchApplication.ex10 as ApprovalRemark,PurchApplication.CreateDate  as ApplicationDate
                                  FROM  PurchApplication          
                                  left join users on users.Uid=PurchApplication.UID
                                  left join Employees on Employees.EID = users.EID  
                                  left join Departments on Departments.DID=PurchApplication.DID           
                                 where OrderId = @orderid ");
                SqlParameter[] sqlpara = new SqlParameter[] {  
                    new SqlParameter("@orderid",orderid) 
                };
                DataTable dt = AssetDBHelper.ExecuteDataTable(sb.ToString(), sqlpara);
                return dt;
            }
            catch
            { return null; }
        } 

        /// <summary>
        ///  获取固定资产转移详情
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public DataTable getFixedAssetTransferDetails(string orderid)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"  SELECT FixedAssetTurnDep.TDOrderId as OrderId,Employees.EID, Employees.EShow as CreatorName,
                               FixedAssetTurnDep.BillStatus,
                                Case when  FixedAssetTurnDep.BillStatus = 80 then '已完成' 
                                     when  FixedAssetTurnDep.BillStatus = 110 then '已过账'
                                     when  FixedAssetTurnDep.BillStatus = 40 then '已撤回'
                                     when  FixedAssetTurnDep.BillStatus = 10 then '未审批'
                                     when  FixedAssetTurnDep.BillStatus = 50 then '已批准'
                                     when  FixedAssetTurnDep.BillStatus = 20 then '已驳回'
                                     else  ''
                                     end as BillStatusString ,
                                FixedAssetTurnDep.Remark,FixedAssetTurnDep.EX08 as ApprovalRemark,FixedAssetTurnDep.CreateDate 
                                  FROM FixedAssetTurnDep 
                                left join users on users.Uid=FixedAssetTurnDep.Uid 
                                left join Employees on Employees.EID = users.EID
                              where TDOrderId    =  @orderid ");
                SqlParameter[] sqlpara = new SqlParameter[] { new SqlParameter("@orderid", orderid) };
                DataTable dt = AssetDBHelper.ExecuteDataTable(sb.ToString(), sqlpara);
                return dt;
            }
            catch
            { return null; }
        }



        /// <summary>
        /// 获取固定资产归还详情
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public DataTable getFixedAssetBackDetails(string orderid)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"      SELECT   AssetBack.OrderId as OrderId,
                                    Departments.DID as DeptCode,Departments.Dname as DeptName,
                                   Employees.EID, Employees.EShow as CreatorName,AssetBack.BillStatus, AssetBack.Remark, AssetBack.ex10 as ApprovalRemark,AssetBack.CreateDate 
                                  FROM  AssetBack          
                                  left join users on users.Uid=AssetBack.CreateUID
                                  left join Employees on Employees.EID = users.EID  
                                  left join Departments on Departments.DID=AssetBack.DID     
                              where OrderId = @orderid ");
                SqlParameter[] sqlpara = new SqlParameter[] {  
                    new SqlParameter("@orderid",orderid) 
                };
                DataTable dt = AssetDBHelper.ExecuteDataTable(sb.ToString(), sqlpara);
                return dt;
            }
            catch
            { return null; }
        }



        /// <summary>
        ///  获取固定资产领用详情 
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public DataTable getFixedAssetClaimDetails(string orderid)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"    SELECT   AssetClaim.OrderId as OrderId,
                                    Departments.DID as DeptCode,Departments.Dname as DeptName,
                                   Employees.EID, Employees.EShow as CreatorName,AssetClaim.BillStatus, AssetClaim.Remark,AssetClaim.ex10 as ApprovalRemark,AssetClaim.CreateDate 
                                  FROM  AssetClaim          
                                  left join users on users.Uid=AssetClaim.CreateUID
                                  left join Employees on Employees.EID = users.EID  
                                  left join Departments on Departments.DID=AssetClaim.DID        
                                 where OrderId = @orderid ");
                SqlParameter[] sqlpara = new SqlParameter[] {  
                    new SqlParameter("@orderid",orderid) 
                };
                DataTable dt = AssetDBHelper.ExecuteDataTable(sb.ToString(), sqlpara);
                return dt;
            }
            catch
            { return null; }
        }


        /// <summary>
        /// 获取已审批列表
        /// </summary>
        public DataTable getApprovedList(string pageNumber, string pageSize)
        {
            try
            { 
                StringBuilder sb = new StringBuilder();
                string WFMbidSqlStr = GetWFMbidSqlStr();
                sb.Append(@"  With TemporaryTable as   
                             ( 
                                    select * ,  row_number()over( order by a.ApplicationDate desc ) as rownum    from (  
                                   SELECT distinct APOrderId AS OrderId,Departments.Dname as DeptName,Employees.EShow as ApplicationMan,AssetApply.ex12 as ApplicationPhone,AssetApply.CreateDate as ApplicationDate, CONVERT(VARCHAR(8000),AssetApply.Remark ) as Remark  , 
                                        Case when  AssetApply.BillStatus = 50 then '已批准' 
                                             else  ''
                                             end as BillStatus   ,
                                       BillStatus as BillStatusId, '物资领用申请' as 'BillType' , '00000000-0000-0000-0000-000001500000' as 'WFMId' ,'' as 'IsApprover'
                                   FROM   AssetApply
                                left join Departments on Departments.DID =AssetApply.DID 
                                left join Employees on employees.eid= AssetApply.APEid  
                                left join WorkFlowPhasePostil A ON A.BillNo=AssetApply.APOrderId
								LEFT JOIN WFFlowPhase AS B ON A.WFMbid = B.WFMbid
                                where BillStatus = 50 

                                         union all    

                                SELECT distinct OrderId AS OrderId,Departments.Dname as DeptName,Employees.EShow as ApplicationMan,PurchApplication.ex12 as ApplicationPhone,PurchApplication.CreateDate as ApplicationDate, CONVERT(VARCHAR(8000),PurchApplication.Remark) as Remark   , 
                                        Case when  PurchApplication.BillStatus = 50 then '已批准' 
                                             else  ''
                                             end as BillStatus   ,
                                       BillStatus as BillStatusId,  '物资采购申请' as 'BillType'  , '00000000-0000-0000-0000-000000000970' as 'WFMId' ,'' as 'IsApprover'
                                   FROM   PurchApplication
                                left join Departments on Departments.DID =PurchApplication.DID 
                                left join Employees on employees.eid= PurchApplication.Eid  
                                left join WorkFlowPhasePostil A ON A.BillNo=PurchApplication.OrderId
								LEFT JOIN WFFlowPhase AS B ON A.WFMbid = B.WFMbid
                                where BillStatus =  50 

                                         union all

                                SELECT distinct OrderId AS OrderId,'' as DeptName,Employees.EShow as ApplicationMan,PurchOrder.ex12 as ApplicationPhone,PurchOrder.CreateDate as ApplicationDate,  CONVERT(VARCHAR(8000),PurchOrder.Remark) as Remark    , 
                                        Case when  PurchOrder.BillStatus =  50 then '已批准' 
                                             else  ''
                                             end as BillStatus   ,
                                       BillStatus as BillStatusId,  '物资采购订单' as 'BillType'  , '00000000-0000-0000-0000-000000000900' as 'WFMId' ,'' as 'IsApprover'
                                   FROM   PurchOrder 
                                left join users on users.Uid=PurchOrder.Uid
                                left join Employees on employees.eid= users.Eid  
                                left join WorkFlowPhasePostil A ON A.BillNo=PurchOrder.OrderId
								LEFT JOIN WFFlowPhase AS B ON A.WFMbid = B.WFMbid
                                where BillStatus =   50  
                                ) as a   ");
                sb.Append(@"   )   
                                select * from TemporaryTable where TemporaryTable.rownum BETWEEN ((" + pageNumber + "-1)*" + pageSize + "+1) AND (" + pageNumber + "*" + pageSize + ")  ");
                SqlParameter[] sqlpara = new SqlParameter[] {  };
                DataTable dt = AssetDBHelper.ExecuteDataTable(sb.ToString(), sqlpara);
                return dt;
            }
            catch
            { return null; }
        }

        /// <summary>
        /// 获取待审批列表总单据数量
        /// </summary>
        /// <param name="EID"></param>
        /// <returns></returns>
        public int getOrderListForApprovalTotalRow(string EID)
        {
            try
            {

                string UID = GetUIDByEID(EID);

                StringBuilder sb = new StringBuilder();
                string WFMbidSqlStr = GetWFMbidSqlStr();
                sb.Append(@"    select  count (1)  as totalRows       from (  
                                   SELECT distinct APOrderId AS OrderId,Departments.Dname as DeptName,Employees.EShow as ApplicationMan,AssetApply.ex12 as ApplicationPhone,AssetApply.CreateDate as ApplicationDate, CONVERT(VARCHAR(8000),AssetApply.Remark ) as Remark  , 
                                        Case when  AssetApply.BillStatus = 10 then '未审批' 
                                             else  ''
                                             end as BillStatus   ,
                                       BillStatus as BillStatusId, '物资领用申请' as 'BillType' , '00000000-0000-0000-0000-000001500000' as 'WFMId' ,'' as 'IsApprover'
                                   FROM   AssetApply
                                left join Departments on Departments.DID =AssetApply.DID 
                                left join Employees on employees.eid= AssetApply.APEid  
                                left join WorkFlowPhasePostil A ON A.BillNo=AssetApply.APOrderId
								LEFT JOIN WFFlowPhase AS B ON A.WFMbid = B.WFMbid
                                where BillStatus = 10 " + WFMbidSqlStr + @" 

                                         union all 

                                SELECT distinct OrderId AS OrderId,Departments.Dname as DeptName,Employees.EShow as ApplicationMan,PurchApplication.ex12 as ApplicationPhone,PurchApplication.CreateDate as ApplicationDate, CONVERT(VARCHAR(8000),PurchApplication.Remark) as Remark   , 
                                        Case when  PurchApplication.BillStatus = 10 then '未审批'  
                                             else  ''
                                             end as BillStatus   ,
                                       BillStatus as BillStatusId,  '物资采购申请' as 'BillType'  , '00000000-0000-0000-0000-000000000970' as 'WFMId' ,'' as 'IsApprover'
                                   FROM   PurchApplication
                                left join Departments on Departments.DID =PurchApplication.DID 
                                left join Employees on employees.eid= PurchApplication.Eid  
                                left join WorkFlowPhasePostil A ON A.BillNo=PurchApplication.OrderId
								LEFT JOIN WFFlowPhase AS B ON A.WFMbid = B.WFMbid
                                where BillStatus =  10   " + WFMbidSqlStr + @"

                                         union all

                                SELECT distinct OrderId AS OrderId,'' as DeptName,Employees.EShow as ApplicationMan,PurchOrder.ex12 as ApplicationPhone,PurchOrder.CreateDate as ApplicationDate,  CONVERT(VARCHAR(8000),PurchOrder.Remark) as Remark    , 
                                        Case when  PurchOrder.BillStatus = 10 then '未审批'  
                                             else  ''
                                             end as BillStatus   ,
                                       BillStatus as BillStatusId,  '物资采购订单' as 'BillType'  , '00000000-0000-0000-0000-000000000900' as 'WFMId' ,'' as 'IsApprover'
                                   FROM   PurchOrder 
                                left join users on users.Uid=PurchOrder.Uid
                                left join Employees on employees.eid= users.Eid  
                                left join WorkFlowPhasePostil A ON A.BillNo=PurchOrder.OrderId
								LEFT JOIN WFFlowPhase AS B ON A.WFMbid = B.WFMbid
                                where BillStatus =  10  " + WFMbidSqlStr + @"  

                                ) as a ");
                SqlParameter[] sqlpara = new SqlParameter[] { new SqlParameter("@UID", UID) };
                DataTable dt = AssetDBHelper.ExecuteDataTable(sb.ToString(), sqlpara);
                int rs = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    rs = int.Parse(dt.Rows[0]["totalRows"].ToString());
                }
                return rs;
            }
            catch
            { return 0; }
        }

        /// <summary>
        /// 更新批准或驳回原因备注
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="RejectReason"></param>
        /// <returns></returns>
        public bool UpdateRejectReason(string OrderId, string BillTypeString, string RejectReason)
        {
            try
            {
                StringBuilder sb = new StringBuilder();  
                if (BillTypeString == "物资领用申请")
                    sb.Append(" update AssetApply set EX10 =  @RejectReason where APOrderId = @OrderId"); 
                if (BillTypeString == "物资采购申请")
                    sb.Append(" update PurchApplication set EX10 = @RejectReason where OrderId = @OrderId");
                if (BillTypeString == "物资采购订单")
                    sb.Append(" update PurchOrder set EX10 = @RejectReason where OrderId = @OrderId"); 

                SqlParameter[] sqlpara = new SqlParameter[] { new SqlParameter("@RejectReason", RejectReason), new SqlParameter("@OrderId", OrderId) };
                int i = AssetDBHelper.ExecuteNonQuery(sb.ToString(), sqlpara);
                if (i > 0)
                    return true;
                else
                    return false;
            }
            catch
            { return false; }
        }


        private string GetWFMbidSqlStr()
        {
            return @" and A.WFMbid IN
												(   SELECT DISTINCT WFMbid 
															   FROM WFUser 
														  LEFT JOIN Users ON WFUser.UserName = Users.Uid
														  LEFT JOIN Employees ON Employees.EID=Users.EID
															  WHERE UserName=@UID AND UserName IN 
																				 (SELECT UID 
																					FROM WFAppUids 
																				   WHERE WFMID=WFUSER.WFMID 
																					 AND WFMBID=WFUSER.WFMBID 
																					 AND BILLNO=A.BILLNO) 
																					 AND 2=WFUser.Prop&2 
																					 AND ((AllowDept=1 AND Employees.DID IN (
																											  SELECT DID 
																												FROM Users , Employees 
																											  WHERE  Employees.EID=Users.EID AND Users.Uid=A.Initiator))
																											   OR (AllowDept=0 AND (
																													  ( SELECT DID FROM Users 
																															  LEFT JOIN Employees ON Employees.EID=Users.EID WHERE [Uid]=A.Initiator)  
																															  IN  (SELECT DID FROM WFFlowPhaseDept WHERE WFFlowPhaseDept.wfmbid=B.WFMbid  
																																			   AND WFFlowPhaseDept.WFMBUID 
																																			   IN(SELECT WFMBUID 
																																					FROM WFUSER 
																																					WHERE USERNAME=@UID))
																												OR ((SELECT COUNT(DID) FROM WFFlowPhaseDept 
																																	  WHERE WFFlowPhaseDept.wfmbid=B.WFMbid  
																																		AND WFFlowPhaseDept.WFMBUID IN(SELECT  WFMBUID FROM WFUSER WHERE USERNAME=@UID))=0))))) 
																																	   AND  A.EndStatus=0 AND A.EX8='0' AND  (a.EX5=0   )  ";
        }

        /// <summary>
        /// getSystemUserNameByEID
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public string getSystemUserNameByEID(string eid, out string password)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("  SELECT  Uid,pwd    FROM [Users] where EID = @Eid");
                SqlParameter[] sqlpara = new SqlParameter[] { 
                    new SqlParameter("@Eid",eid) 
                };

                DataTable dt = DbHelperSQL.ExecuteDataTable(sb.ToString(), sqlpara);
                if (dt != null && dt.Rows.Count > 0)
                {
                    password = dt.Rows[0]["pwd"].ToString();
                    return dt.Rows[0]["Uid"].ToString();
                }
                else
                {
                    password = "";

                    #region 查hqadmin pwd
                    StringBuilder sb2 = new StringBuilder();
                    sb2.Append("  SELECT  pwd    FROM [Users] where Uid ='hqadmin' ");
                    SqlParameter[] sqlpara2 = new SqlParameter[] {  
                    };

                    DataTable dt2 = DbHelperSQL.ExecuteDataTable(sb2.ToString(), sqlpara2);
                    if (dt2 != null && dt2.Rows.Count > 0)
                    {
                        password = dt2.Rows[0]["pwd"].ToString();
                    }
                    #endregion

                    return "hqadmin";
                }
            }
            catch
            {
                password = "";
                return "";
            }
        }

        public string GetUIDByEID(string EID)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"   select UID from Users where EID=@EID   ");
                SqlParameter[] sqlpara = new SqlParameter[] {  
                    new SqlParameter("@EID",EID) 
                };
                DataTable dt = DbHelperSQL.ExecuteDataTable(sb.ToString(), sqlpara);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["UID"].ToString();
                }

                return "";
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 检查是否还没有上工签到
        /// </summary>
        /// <param name="EID"></param>
        /// <param name="TaskCode"></param>
        /// <param name="SwipeCardType">打卡类型，1：上工签到，2：下工签退，3：完成任务</param>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public bool CheckIfNotAlreadyOnDutySwipeCard(string EID, string TaskCode, string SwipeCardType, string ProjectId)
        {
            try
            {
                if (SwipeCardType != "2")
                    return false;
                StringBuilder sb = new StringBuilder();
                sb.Append(@"  SELECT * FROM [EmployeeSwipeCard]  
                                where EID= @EID AND ProjectId=@ProjectId and TaskCode= @TaskCode and SwipeCardType='1'  ");
                SqlParameter[] sqlpara = new SqlParameter[] { 
                    new SqlParameter("@EID", EID) ,
                    new SqlParameter("@TaskCode", TaskCode)  , 
                    new SqlParameter("@ProjectId", ProjectId) 
                };
                DataTable dt = DbHelperSQL.ExecuteDataTable(sb.ToString(), sqlpara);
                if (dt != null && dt.Rows.Count > 0)
                { 
                    return false;
                }
                else
                { 
                    return true;
                }
            }
            catch
            { 
                return false;
            }
        }

        /// <summary>
        /// 检查是否已经下工签退过
        /// </summary>
        /// <param name="EID"></param>
        /// <param name="TaskCode"></param>
        /// <param name="SwipeCardType">打卡类型，1：上工签到，2：下工签退，3：完成任务</param>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public bool CheckIfAlreadyOffDutySwipeCard(string EID, string TaskCode,   string ProjectId)
        {
            try
            { 
                StringBuilder sb = new StringBuilder();
                sb.Append(@"  SELECT * FROM ProjectResource  where ElementId=@TaskCode AND Rid=@EID and EX12='2' and ProjId=@ProjectId    ");
                SqlParameter[] sqlpara = new SqlParameter[] { 
                    new SqlParameter("@EID", EID) ,
                    new SqlParameter("@TaskCode", TaskCode)  , 
                    new SqlParameter("@ProjectId", ProjectId) 
                };
                DataTable dt = DbHelperSQL.ExecuteDataTable(sb.ToString(), sqlpara);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 员工获取任务列表
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public DataTable getTaskList(string EID, string PageNumber, string PageSize)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@" With TemporaryTable as   
                              (  
                                  select s.TaskCode,s.TaskName ,s.UserName,s.ResponseStatus,s.ProjId , row_number()over( order by s.TaskCode  ) as rownum     from 
                                  (
                                      select distinct ProjectResource.elementId as TaskCode,
                                      ProjectStructs.ElementName as TaskName,
                                      ProjectResource.Rname as UserName, 
                                      case  when (ProjectResource.EX12 =0 OR ProjectResource.EX12 IS NULL ) then '未响应'
                                            when ProjectResource.EX12 = 1  then '上线'  
                                            when ProjectResource.EX12 = 2  then '下线'  
                                            when ProjectResource.EX12 = 3  then '完成'  
                                      end   as  ResponseStatus   , ProjectResource.ProjId    
                                     from ProjectResource 
                                     left join ProjectStructs on ProjectStructs.ElementId=ProjectResource.ElementId
                                     where Rid= @EID ) as s  ");
                sb.Append(@"   )   
                                select * from TemporaryTable where TemporaryTable.rownum BETWEEN ((" + PageNumber + "-1)*" + PageSize + "+1) AND (" + PageNumber + "*" + PageSize + ")  ");
                SqlParameter[] sqlpara = new SqlParameter[] { 
                    new SqlParameter("@EID",EID) 
                };
                DataTable dt_TodaysEquipments = DbHelperSQL.ExecuteDataTable(sb.ToString(), sqlpara);
                return dt_TodaysEquipments;
            }
            catch
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// 项目经理获取已派工的任务列表
        /// </summary>
        /// <param name="EID"></param>
        /// <returns></returns>
        public DataTable getDispatchedTaskListByPM(string EID , string PageNumber, string PageSize)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@" With TemporaryTable as   
                              (  
                                select s.ElementId,s.ElementName ,s.ProjId, row_number()over( order by s.ElementId  ) as rownum     from 
                                  (  
                                 select distinct ProjectStructs.ElementId,ProjectStructs.ElementName ,ProjectResource.ProjId      
                                  from ProjectResource
                             left join ProjectStructs on ProjectStructs.Pid=ProjectResource.ProjId and ProjectStructs.ElementId=ProjectResource.ElementId 
                             left join ProjectActivity P1 on ProjectStructs.ElementId=P1.ActId 
                             left join WBS w1 on  ProjectStructs.ElementId=w1.Wid 
                             left join ProjectNetwork n1 on ProjectStructs.ElementId = n1.NId 
                             where 1=1 and (  exists (select 1 from ProjectActivity p2 where p2.ActId=p1.ActId and p2.OwnerEid = @EID  ) 
			                               or exists (select 1 from WBS w2 where w2.WId=w1.WId and w2.OwnerEid = @EID  ) 
			                               or exists (select 1 from ProjectNetwork n2 where n2.Nid=N1.Nid and N2.OwnerEid = @EID  ) 
			                               )  ) as s  ");
                sb.Append(@"   )   
                                select * from TemporaryTable where TemporaryTable.rownum BETWEEN ((" + PageNumber + "-1)*" + PageSize + "+1) AND (" + PageNumber + "*" + PageSize + ")  ");
                SqlParameter[] sqlpara = new SqlParameter[] { 
                    new SqlParameter("@EID",EID) 
                };
                DataTable ResultData = DbHelperSQL.ExecuteDataTable(sb.ToString(), sqlpara);
                return ResultData;
            }
            catch
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// 项目经理获取已派工的任务详情
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="ElementId"></param>
        /// <returns></returns>
        public DataTable getDispatchedTaskDetailsByPM(string ProjectId, string ElementId)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@" select Rname, 
                                    case  when (ProjectResource.EX12 =0 OR ProjectResource.EX12 IS NULL ) then '未响应'
                                                                            when ProjectResource.EX12 = 1  then '上线'
                                                                            when ProjectResource.EX12 = 2  then '下线'
                                                                            when ProjectResource.EX12 = 3  then '完成'
                                                                      end   as  ResponseStatus 
                                         from ProjectResource 
                                        where ProjId=@ProjectId and ElementId=@ElementId ");
                SqlParameter[] sqlpara = new SqlParameter[] { 
                    new SqlParameter("@ProjectId",ProjectId) ,
                    new SqlParameter("@ElementId",ElementId) 
                };
                DataTable ResultData = DbHelperSQL.ExecuteDataTable(sb.ToString(), sqlpara);
                return ResultData;
            }
            catch
            {
                return new DataTable();
            }
        }
       
        #region Common Function
        /// <summary>
        /// 检查EID是否在系统Users表里
        /// </summary> 
        public bool CheckIfEID_In_SystemUsers_Table(string EID)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"   select * from Users where EID = @EID ");
                SqlParameter[] sqlpara = new SqlParameter[] { 
                    new SqlParameter("@EID",EID)  
                };

                DataTable dt = DbHelperSQL.ExecuteDataTable(sb.ToString(), sqlpara);
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取管理员EID
        /// </summary>
        /// <returns></returns>
        public string GetAdminEID()
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@"   select * from Users where UID='hqadmin'    ");
                SqlParameter[] sqlpara = new SqlParameter[] {  
                }; 
                DataTable dt = DbHelperSQL.ExecuteDataTable(sb.ToString(), sqlpara); 
                if (dt != null && dt.Rows.Count > 0)
                {
                    return dt.Rows[0]["EID"].ToString();
                } 
                return "";
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 检查菜单权限
        /// </summary>
        /// <param name="EID"></param>
        /// <param name="PowerId"></param>
        /// <returns></returns>
        public bool CheckEIDHasMenuPower(string EID, string PowerId)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(@" select [Users].*
                                FROM [Users] 
                                LEFT JOIN UserGroupRelation on UserGroupRelation.Uid= Users.Uid
                                left join UserGroupPowers on UserGroupPowers.UgId= UserGroupRelation.UgId
                                where UserGroupPowers.Pid=@PowerId  AND users.EID = @EID
                            ");
                SqlParameter[] sqlpara = new SqlParameter[] { 
                    new SqlParameter("@EID", EID) ,
                    new SqlParameter("@PowerId", PowerId) 
                };

                DataTable dt = DbHelperSQL.ExecuteDataTable(sb.ToString(), sqlpara);

                if (dt != null && dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取Uid和密码ByEID
        /// </summary>
        /// <param name="eid"></param>
        /// <returns></returns>
        public string getUidAndPwdByEID(string eid, out string password)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("  SELECT Uid,pwd FROM  [dbo].[Users] where EID = @Eid");
                SqlParameter[] sqlpara = new SqlParameter[] { 
                    new SqlParameter("@Eid",eid) 
                }; 
                DataTable dt = DbHelperSQL.ExecuteDataTable(sb.ToString(), sqlpara);
                if (dt != null && dt.Rows.Count > 0)
                {
                    password = dt.Rows[0]["pwd"].ToString();
                    return dt.Rows[0]["Uid"].ToString();
                }
                else
                {
                    password = ""; 
                    #region 查hqadmin pwd
                    StringBuilder sb2 = new StringBuilder();
                    sb2.Append("  SELECT pwd FROM [dbo].[Users] where Uid ='hqadmin' ");
                    SqlParameter[] sqlpara2 = new SqlParameter[] {  
                    }; 
                    DataTable dt2 = DbHelperSQL.ExecuteDataTable(sb2.ToString(), sqlpara2);
                    if (dt2 != null && dt2.Rows.Count > 0)
                    {
                        password = dt2.Rows[0]["pwd"].ToString();
                    }
                    #endregion 
                    return "hqadmin";
                }
            }
            catch
            {
                password = "";
                return "";
            }
        }
        #endregion
    }
}
