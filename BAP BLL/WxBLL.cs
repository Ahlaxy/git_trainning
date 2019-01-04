using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BAP_Model.Model;
using WSERP.Common;
using WSERP.DataAccess;
using System.Data.SqlClient;   
using BAP_WeChart.Utility; 
using System.Data;
using System.Configuration;
using Wellshsoft.Net;
using System.IO;
using Wellshsoft.Net.Data;
using BAP_Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Net.Sockets;
using System.Net; 

namespace BAP_BLL
{
    public class WxBLL
    {
        BAP_DAL.WxDAL dal = new BAP_DAL.WxDAL();

        #region 审批中心 
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ApprovalCenter_Login(string username, string password, out string EID, out string EShow)
        {
            return dal.ApprovalCenter_Login(username, password, out EID, out EShow);
        }

        /// <summary>
        /// 更新批准或驳回原因备注
        /// </summary>
        /// <param name="OrderId"></param>
        /// <param name="RejectReason"></param>
        /// <returns></returns>
        public bool UpdateRejectReason(string OrderId, string BillTypeString, string RejectReason)
        {
            return dal.UpdateRejectReason(OrderId, BillTypeString, RejectReason);
        }

        /// <summary>
        /// 获取审批列表
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="EID"></param>
        /// <param name="BillStatusId">10 未审批， 50 已审批</param>
        /// <returns></returns>
        public DataTable getListForApproval(string pageNumber, string pageSize, string EID, string BillStatusId)
        {
            if (BillStatusId == "10")
            {
                return dal.getListForApproval(pageNumber, pageSize, EID);
            }
            else if (BillStatusId == "50")
            {
                return dal.getApprovedList(pageNumber, pageSize);
            }
            else
                return new DataTable();
        }

        public int getOrderListForApprovalTotalRow(string EID)
        { 
            return dal.getOrderListForApprovalTotalRow(  EID);
        }

        public void CreateCI(ref ClientInfo CI, ref string uid, string EID, string DbConnectionString, string DatabaseName, string BAPLicense)
        {
            CI.Ptype = 10002;
            CI.CnStr = DbConnectionString;
            CI.MachineName = System.Environment.MachineName;
            CI.DataBaseName = DatabaseName;
            CI.Connecttype = 2;
            CI.Extension3 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).ToShortDateString();
            CI.Extension4 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1).ToShortDateString();
            CI.AccessDateTime = DateTime.Now;
            CI.Extension2 = BAPLicense;
            string pwd = "";
            CI.UserName = getSystemUserNameByEID(EID, out pwd);
            CI.Tag = pwd;
            CI.Tag = Wellshsoft.Net.DataProtector.Decode(DataConvert.ToString<object>(pwd));
            uid = CI.UserName;
            CheckUidVersion2(CI, uid, DbConnectionString, DatabaseName);
        }

        /// <summary>
        /// 重新登录Version2
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="Uid"></param>
        public void CheckUidVersion2(ClientInfo ci, string Uid, string ConnString, string DatabaseName)
        {
            ci.CnStr = System.Configuration.ConfigurationManager.ConnectionStrings["WSERPSysConnString"].ToString();
            ci.DataBaseName = System.Configuration.ConfigurationManager.AppSettings["WSERPSysDatabaseName"].ToString();

            using (AdoEx ado = new AdoEx("System.Data.SqlClient", ci.CnStr))
            {
                ci.UserName = Uid;
                //删除当前用户、客户端登录记录
                UtilityTools.WriteTxt("//Log//删除当前用户、客户端登录记录", "删除当前用户、客户端登录记录:" + ci.UserName + ", ci.MachineName:" + ci.MachineName + "  \r\n ci.CnStr:" + ci.CnStr);

                int i = ado.ExecuteNonQuery(CommandType.Text,
                            "DELETE FROM MPSHELP WHERE A=@A AND C=@C",
                            new SqlParameter[]{
                                new SqlParameter("@A",  Wellshsoft.Net.DataProtector.Encode(ci.UserName)),
                                new SqlParameter("@C",  Wellshsoft.Net.DataProtector.Encode(ci.MachineName))
                            });
                UtilityTools.WriteTxt("//Log//删除当前用户返回的i", "删除当前用户返回的i" + i);

            }
            try
            {
                ci.CnStr = ConnString;
                ci.DataBaseName = DatabaseName;
                UtilityTools.WriteTxt("//Log//重新登录", "重新登录 前  \r\n              ci.UserName:" + ci.UserName
                                                                            + " \r\n  ci.MachineName:" + ci.MachineName
                                                                            + " \r\n  ci.Tag:" + ci.Tag
                                                                            + " \r\n  ci.CnStr:" + ci.CnStr);
                //重新登录
                Security security = new WSERP.DataAccess.Security();
                //网站根目录需要配置连接数据库文件LocalNetDb.xml
                UsersData uid = security.Login(ci, ci.UserName, ci.MachineName);
                if (uid == null)
                {
                    UtilityTools.WriteTxt("//Log//重新登录", "重新登录 后 uid == null ");
                }
                else
                {
                    UtilityTools.WriteTxt("//Log//重新登录", "重新登录 后  \r\n Ename:" + uid.Ename + "EID:" + uid.EID);
                }
                string[] tags = (string[])uid.Tag;

                ci.AccessDateTime = DataConvert.ToDateTime<string>(DataProtector.Decode(tags[1]));
            }
            catch (Exception ex)
            {
                UtilityTools.WriteTxt("//Log//重新登录异常", "重新登录异常,网站根目录LocalNetDb.xml是否配置正确? ex.Message:" + ex.Message);
            }
        }


        public string getSystemUserNameByEID(string eid, out string pwd)
        {
            return dal.getSystemUserNameByEID(eid, out pwd);
        }


        #endregion

        #region 沃格
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool Novo_Login(string username, string password, out string EID, out string EShow)
        {
            return dal.Novo_Login(username, password,out EID,out EShow);
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
            return dal.CheckIfNotAlreadyOnDutySwipeCard(EID, TaskCode, SwipeCardType, ProjectId);
        }

        /// <summary>
        /// 检查是否已经下工签退过
        /// </summary>
        /// <param name="EID"></param>
        /// <param name="TaskCode"></param>
        /// <param name="SwipeCardType"></param>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public bool CheckIfAlreadyOffDutySwipeCard(string EID, string TaskCode,  string ProjectId)
        {
            return dal.CheckIfAlreadyOffDutySwipeCard(EID, TaskCode,   ProjectId);
        }

        /// <summary>
        /// 员工获取任务列表
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public DataTable getTaskList(string EID, string PageNumber, string PageSize)
        {
            return dal.getTaskList(EID, PageNumber, PageSize);
        }

        /// <summary>
        /// 员工报工打卡
        /// </summary>
        public bool EmployeeSwipeCard(string EID, string TaskCode, string SwipeCardType, string SwipeCardTime, string ProjectId, string Latitude, string Longitude, string StreetName, out string PkId)
        {
            return dal.EmployeeSwipeCard(EID, TaskCode, SwipeCardType, SwipeCardTime, ProjectId, Latitude, Longitude, StreetName, out PkId);
        }

        public DataTable GetTestList(string statusId, string pageNumber, string pageSize)
        {
            return dal.GetTestList(statusId,  pageNumber,  pageSize);
        }
         
        public bool TestFinish(string orderId)
        {
            return dal.TestFinish(orderId );
        }

        public DataTable GetTestDetails(string orderId)
        {
            return dal.GetTestDetails(orderId);
        }

        /// <summary>
        /// 获取物资领用申请详情
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public DataTable getGoodsAndMaterialsDetails(string orderid)
        {
            return dal.getGoodsAndMaterialsDetails(orderid);
        }

        /// <summary>
        /// 获取物资退库详情
        /// </summary>
        /// <param name="orderid"></param> 
        public DataTable getGoodsAndMaterialsReturnDetails(string orderid)
        {
            return dal.getGoodsAndMaterialsReturnDetails(orderid);
        }

        /// <summary>
        ///  获取物资采购申请详情
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public DataTable getPurchApplicationDetails(string orderid)
        {
            return dal.getPurchApplicationDetails(orderid);
        }

        /// <summary>
        /// 获取物资采购申请详情物资列表
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public DataTable getMeterialDetailsForPurchApplication(string orderid)
        {
            return dal.getMeterialDetailsForPurchApplication(orderid);
        }

        /// <summary>
        /// 获取物资采购订单详情物资列表
        /// </summary> 
        public DataTable getMeterialDetailsForPurchOrder(string orderid)
        {
            return dal.getMeterialDetailsForPurchOrder(orderid);
        }
        

        /// <summary>
        ///  获取物资采购订单详情 
        /// </summary> 
        public DataTable getPurchOrderDetails(string orderid)
        {
            return dal.getPurchOrderDetails(orderid);
        }

        /// <summary>
        ///  获取固定资产转移详情
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public DataTable getFixedAssetTransferDetails(string orderid)
        {
            return dal.getFixedAssetTransferDetails(orderid);
        }


        /// <summary>
        /// 获取固定资产归还详情
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public DataTable getFixedAssetBackDetails(string orderid)
        {
            return dal.getFixedAssetBackDetails(orderid);
        }

        /// <summary>
        ///  获取固定资产领用详情 
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public DataTable getFixedAssetClaimDetails(string orderid)
        {
            return dal.getFixedAssetClaimDetails(orderid);
        }

        /// <summary>
        /// 根据OrderId获取物资申请或归还物资列表
        /// </summary>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public DataTable GetGoodsAndMaterialsMaterialsByOrderId(string orderid, int PType)
        {
            return dal.GetGoodsAndMaterialsMaterialsByOrderId(orderid, PType);
        }

        /// <summary>
        /// 项目经理获取已派工的任务列表
        /// </summary>
        /// <param name="EID"></param>
        /// <returns></returns>
        public DataTable getDispatchedTaskListByPM(string EID  ,string PageNumber,string PageSize)
        {
            return dal.getDispatchedTaskListByPM(EID,PageNumber,PageSize);
        }

        /// <summary>
        /// 项目经理获取已派工的任务详情
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <param name="ElementId"></param>
        /// <returns></returns>
        public DataTable getDispatchedTaskDetailsByPM(string ProjectId, string ElementId)
        {
            return dal.getDispatchedTaskDetailsByPM(ProjectId,   ElementId);
        }

        /// <summary>
        /// 获取派工时的项目列表
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public DataTable getProjectListForDispatch(string EID, string Keyword)
        { 
            ProjectBuilderDAL ProjectDal = new ProjectBuilderDAL();

            string DbConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ToString();
            string BAPLicense = System.Configuration.ConfigurationManager.AppSettings["BAPLicense"].ToString();
            string DatabaseName = System.Configuration.ConfigurationManager.AppSettings["DatabaseName"].ToString();

            ClientInfo CI = new ClientInfo();
            CI.Ptype = 10002;
            CI.CnStr = DbConnectionString;
            CI.MachineName = System.Environment.MachineName;
            CI.DataBaseName = DatabaseName;
            CI.Connecttype = 2;
            CI.Extension3 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).ToShortDateString();
            CI.Extension4 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1).ToShortDateString();
            CI.AccessDateTime = DateTime.Now;
            CI.Extension2 = BAPLicense; 
            string uid = "";
            string pwd = "";
            CI.UserName = dal.getUidAndPwdByEID(EID, out pwd);
            CI.Tag = Wellshsoft.Net.DataProtector.Decode(DataConvert.ToString<object>(pwd)); 
            uid = CI.UserName; 
            CheckUid(CI, uid);

            return ProjectDal.SearchProjids(CI, Keyword, null, null); 
        }

        /// <summary>
        /// 获取派工时的项目节点
        /// </summary>
        /// <param name="ProjectId"></param>
        /// <returns></returns>
        public DataTable getProjectElementByProjectId( string ProjectId,string EID)
        {
            return dal.getProjectElementByProjectId(ProjectId,EID);
        }

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
            return dal.SaveDispatch(ProjectId, ElementId, ResourceId, ResourceName);
        }

        /// <summary>
        /// 获取派工时的项目节点的子节点
        /// </summary>
        /// <param name="Username"></param>
        /// <returns></returns>
        public DataTable getSubElementIdByProjectIdAndElementId(string ProjectId, string ElementId, string EID)
        {
            return dal.getSubElementIdByProjectIdAndElementId(ProjectId, ElementId, EID);
        }

        /// <summary>
        /// 获取派工时的员工列表
        /// </summary> 
        public DataTable getEmployeeListForDispatch(string EID , EntityWhere[] wheres)
        { 
            EmployeeDAL EmployeeDal = new EmployeeDAL();

            string DbConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ToString();
            string BAPLicense = System.Configuration.ConfigurationManager.AppSettings["BAPLicense"].ToString();
            string DatabaseName = System.Configuration.ConfigurationManager.AppSettings["DatabaseName"].ToString();

            ClientInfo CI = new ClientInfo();
            CI.Ptype = 10002;
            CI.CnStr = DbConnectionString;
            CI.MachineName = System.Environment.MachineName;
            CI.DataBaseName = DatabaseName;
            CI.Connecttype = 2;
            CI.Extension3 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).ToShortDateString();
            CI.Extension4 = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1).AddDays(-1).ToShortDateString();
            CI.AccessDateTime = DateTime.Now;
            CI.Extension2 = BAPLicense; 
            string uid = "";
            string pwd = "";
            CI.UserName = dal.getUidAndPwdByEID(EID, out pwd);
            CI.Tag = Wellshsoft.Net.DataProtector.Decode(DataConvert.ToString<object>(pwd)); 
            uid = CI.UserName; 
            CheckUid(CI, uid);

            return EmployeeDal.GetEmployeesByFilter(CI, wheres); 
        }

        #endregion 

        #region Common Function
        /// <summary>
        /// 获取管理员EID
        /// </summary>
        /// <returns></returns>
        public string GetAdminEID()
        {
            return dal.GetAdminEID();
        }
        /// <summary>
        /// 检查EID是否在系统Users表里
        /// </summary> 
        public bool CheckIfEID_In_SystemUsers_Table(string EID)
        {
            return dal.CheckIfEID_In_SystemUsers_Table(EID);
        }

        /// <summary>
        /// 检查用户是否有菜单权限
        /// </summary>
        /// <param name="EID"></param>
        /// <param name="PowerId"></param>
        /// <returns></returns>
        public bool CheckEIDHasMenuPower(string EID, string PowerId)
        {
            return dal.CheckEIDHasMenuPower(EID, PowerId);
        }

          /// <summary>
        /// 重新登录
        /// </summary>
        /// <param name="ci"></param>
        /// <param name="Uid"></param>
        public void CheckUid(ClientInfo ci, string Uid)
        {
            ci.CnStr = System.Configuration.ConfigurationManager.ConnectionStrings["WSERPSysConnString"].ToString();
            ci.DataBaseName = System.Configuration.ConfigurationManager.AppSettings["WSERPSysDatabaseName"].ToString();
            
            using (AdoEx ado = new AdoEx("System.Data.SqlClient", ci.CnStr))
            {
                ci.UserName = Uid;
                //删除当前用户、客户端登录记录
                UtilityTools.WriteTxt("//Log//删除当前用户、客户端登录记录", "删除当前用户、客户端登录记录:" + ci.UserName + ", ci.MachineName:" + ci.MachineName + "  \r\n ci.CnStr:" + ci.CnStr);
               
                int i = ado.ExecuteNonQuery(CommandType.Text,
                            "DELETE FROM MPSHELP WHERE A=@A AND C=@C",
                            new SqlParameter[]{
                                new SqlParameter("@A",  Wellshsoft.Net.DataProtector.Encode(ci.UserName)),
                                new SqlParameter("@C",  Wellshsoft.Net.DataProtector.Encode(ci.MachineName))
                            });
                UtilityTools.WriteTxt("//Log//删除当前用户返回的i", "删除当前用户返回的i" + i);
               
            } 
            try
            {
                ci.CnStr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnString"].ToString(); 
                ci.DataBaseName = System.Configuration.ConfigurationManager.AppSettings["DatabaseName"].ToString();
                UtilityTools.WriteTxt("//Log//重新登录", "重新登录 前  \r\n              ci.UserName:" + ci.UserName
                                                                            + " \r\n  ci.MachineName:" + ci.MachineName
                                                                            + " \r\n  ci.Tag:" + ci.Tag
                                                                            + " \r\n  ci.CnStr:" + ci.CnStr );
                //重新登录
                Security security = new WSERP.DataAccess.Security();
                //网站根目录需要配置连接数据库文件LocalNetDb.xml
                UsersData uid = security.Login(ci, ci.UserName, ci.MachineName);
                if (uid == null)
                {
                    UtilityTools.WriteTxt("//Log//重新登录", "重新登录 后 uid == null ");
                }
                else
                {
                    UtilityTools.WriteTxt("//Log//重新登录", "重新登录 后  \r\n Ename:" + uid.Ename + "EID:" + uid.EID);
                }
                string[] tags = (string[])uid.Tag;
               
                ci.AccessDateTime = DataConvert.ToDateTime<string>(DataProtector.Decode(tags[1]));
            }
            catch (Exception ex)
            {
                UtilityTools.WriteTxt("//Log//重新登录异常", "重新登录异常,网站根目录LocalNetDb.xml是否配置正确? ex.Message:" + ex.Message);
            }
        }
        #endregion

    }
}
