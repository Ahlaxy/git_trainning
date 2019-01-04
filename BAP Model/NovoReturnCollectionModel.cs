using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BAP_Model;
using BAP_WeChart;

namespace BAP_Model
{
    public class NovoReturnCollectionMode
    {
        public ResultModel resutlModel = new ResultModel();

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Task> TaskList = new List<Task>();//员工任务列表

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Project> ProjectListForDispatch = new List<Project>();//派工时的项目列表 

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Employee LoginReturnUserInfo = new Employee(); //登录成功返回的user信息  

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Employee> EmployeeListForDispatch = new List<Employee>();//派工时的员工列表

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<ProjectElement> ProjectElementList = new List<ProjectElement>();//派工时的项目元素列表

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string EmployeeSwipeCardAddedPrimaryKeyId = ""; //员工报工打卡时保存记录的主键Id 

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MenuPowers mp = new MenuPowers();  //用户菜单权限

        public NovoReturnCollectionMode()
        {
            this.TaskList = null;
            this.ProjectListForDispatch = null;
            this.LoginReturnUserInfo = null;
            this.EmployeeListForDispatch = null;
            this.ProjectElementList = null;
            this.EmployeeSwipeCardAddedPrimaryKeyId = null;
            this.mp = null;
        } 
    }
}


 