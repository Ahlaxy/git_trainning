/**  版本信息模板在安装目录下，可自行修改。
* Employees.cs
*
* 功 能： N/A
* 类 名： Employees
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2018/3/30 14:33:20   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace BAP_Model.Model
{
	/// <summary>
	/// Employees:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Employees
	{
		public Employees()
		{}
		#region Model
		private string _eid;
		private string _eshow;
		private string _ename;
		private bool _gender;
		private DateTime? _birthday;
		private int? _nation;
		private string _nativeplace;
		private string _raddress;
		private string _caddress;
		private string _postalcode;
		private int? _marriage;
		private int? _bloodgroup;
		private int? _stature;
		private int? _avoirdupois;
		private decimal? _eyesight;
		private int? _apparat;
		private string _identitycard;
		private string _nationality;
		private string _passport;
		private int? _health;
		private string _medicalrecord;
		private int? _drivinglicenceclass;
		private int? _qualifications;
		private int? _degree;
		private string _specialty;
		private string _university;
		private int? _levelcomputer;
		private int? _foreignlanguage;
		private int? _levellanguage;
		private string _religion;
		private string _telephone;
		private string _mobilephone;
		private string _emergencycontact;
		private string _officetel;
		private string _email;
		private string _strongsuit;
		private byte[] _photo;
		private string _remark;
		private string _did;
		private string _pid;
		private string _hid;
		private int? _titles;
		private int? _administrativelevel;
		private int? _employmentforms;
		private int? _team;
		private string _fixshift;
		private string _fixrest;
		private string _enno;
		private string _cardno;
		private string _password;
		private string _manuallaborno;
		private DateTime? _obtainemploymentdate;
		private DateTime? _accessiondate;
		private string _laborcontractno;
		private DateTime? _contractstartdate;
		private DateTime? _contractenddate;
		private DateTime? _contractsigningdate;
		private DateTime? _probationstartdate;
		private DateTime? _probationenddate;
		private int? _postsnature;
		private int? _wagescales;
		private decimal? _socialsecuritybase;
		private string _socialsecurityaccount;
		private decimal? _providentfundbase;
		private string _providentfundaccount;
		private int? _businessinsurance;
		private string _businessinsuranceaccount;
		private decimal? _businessinsurancebase;
		private int? _depositarybank;
		private string _bankaccount;
		private string _trpno;
		private DateTime? _trpdate;
		private int? _trpmonth;
		private int? _property;
		private string _operatoreid;
		private DateTime? _operatetime;
		private string _esurname;
		private bool _evip;
		private string _py;
		private int? _bandid;
		private int? _prop;
		private string _cmpid;
		private string _ex01;
		private string _ex02;
		private DateTime? _x;
		private DateTime? _y;
		private Guid _egid;
		private decimal? _ex1;
		private decimal? _ex2;
		private string _ex3;
		private string _ex4;
		private string _ex5;
		private string _ex6;
		private string _ex7;
		private string _ex8;
		private string _ex9;
		private string _ex10;
		private string _ex11;
		private string _ex12;
		private string _ex13;
		private decimal? _ex14;
		private decimal? _ex15;
		private int? _ex16;
		private int? _ex17;
		private DateTime? _ex18;
		private DateTime? _body;
		private string _wxopenid;
		private int? _wxsex;
		private string _wxnickname;
		private string _wxheadimg;
		/// <summary>
		/// 
		/// </summary>
		public string EID
		{
			set{ _eid=value;}
			get{return _eid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EShow
		{
			set{ _eshow=value;}
			get{return _eshow;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EName
		{
			set{ _ename=value;}
			get{return _ename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool Gender
		{
			set{ _gender=value;}
			get{return _gender;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Birthday
		{
			set{ _birthday=value;}
			get{return _birthday;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Nation
		{
			set{ _nation=value;}
			get{return _nation;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NativePlace
		{
			set{ _nativeplace=value;}
			get{return _nativeplace;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RAddress
		{
			set{ _raddress=value;}
			get{return _raddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CAddress
		{
			set{ _caddress=value;}
			get{return _caddress;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Postalcode
		{
			set{ _postalcode=value;}
			get{return _postalcode;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Marriage
		{
			set{ _marriage=value;}
			get{return _marriage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? BloodGroup
		{
			set{ _bloodgroup=value;}
			get{return _bloodgroup;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Stature
		{
			set{ _stature=value;}
			get{return _stature;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Avoirdupois
		{
			set{ _avoirdupois=value;}
			get{return _avoirdupois;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? Eyesight
		{
			set{ _eyesight=value;}
			get{return _eyesight;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Apparat
		{
			set{ _apparat=value;}
			get{return _apparat;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string IdentityCard
		{
			set{ _identitycard=value;}
			get{return _identitycard;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Nationality
		{
			set{ _nationality=value;}
			get{return _nationality;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Passport
		{
			set{ _passport=value;}
			get{return _passport;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Health
		{
			set{ _health=value;}
			get{return _health;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MedicalRecord
		{
			set{ _medicalrecord=value;}
			get{return _medicalrecord;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DrivingLicenceClass
		{
			set{ _drivinglicenceclass=value;}
			get{return _drivinglicenceclass;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Qualifications
		{
			set{ _qualifications=value;}
			get{return _qualifications;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Degree
		{
			set{ _degree=value;}
			get{return _degree;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Specialty
		{
			set{ _specialty=value;}
			get{return _specialty;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string University
		{
			set{ _university=value;}
			get{return _university;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? LevelComputer
		{
			set{ _levelcomputer=value;}
			get{return _levelcomputer;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? ForeignLanguage
		{
			set{ _foreignlanguage=value;}
			get{return _foreignlanguage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? LevelLanguage
		{
			set{ _levellanguage=value;}
			get{return _levellanguage;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Religion
		{
			set{ _religion=value;}
			get{return _religion;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Telephone
		{
			set{ _telephone=value;}
			get{return _telephone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string MobilePhone
		{
			set{ _mobilephone=value;}
			get{return _mobilephone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EmergencyContact
		{
			set{ _emergencycontact=value;}
			get{return _emergencycontact;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OfficeTel
		{
			set{ _officetel=value;}
			get{return _officetel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Email
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string StrongSuit
		{
			set{ _strongsuit=value;}
			get{return _strongsuit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public byte[] Photo
		{
			set{ _photo=value;}
			get{return _photo;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DID
		{
			set{ _did=value;}
			get{return _did;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PID
		{
			set{ _pid=value;}
			get{return _pid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string HID
		{
			set{ _hid=value;}
			get{return _hid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Titles
		{
			set{ _titles=value;}
			get{return _titles;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? AdministrativeLevel
		{
			set{ _administrativelevel=value;}
			get{return _administrativelevel;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? EmploymentForms
		{
			set{ _employmentforms=value;}
			get{return _employmentforms;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Team
		{
			set{ _team=value;}
			get{return _team;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FixShift
		{
			set{ _fixshift=value;}
			get{return _fixshift;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string FixRest
		{
			set{ _fixrest=value;}
			get{return _fixrest;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EnNo
		{
			set{ _enno=value;}
			get{return _enno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CardNo
		{
			set{ _cardno=value;}
			get{return _cardno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string Password
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ManualLaborNo
		{
			set{ _manuallaborno=value;}
			get{return _manuallaborno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ObtainEmploymentDate
		{
			set{ _obtainemploymentdate=value;}
			get{return _obtainemploymentdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? AccessionDate
		{
			set{ _accessiondate=value;}
			get{return _accessiondate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string LaborContractNo
		{
			set{ _laborcontractno=value;}
			get{return _laborcontractno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ContractStartDate
		{
			set{ _contractstartdate=value;}
			get{return _contractstartdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ContractEndDate
		{
			set{ _contractenddate=value;}
			get{return _contractenddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ContractSigningDate
		{
			set{ _contractsigningdate=value;}
			get{return _contractsigningdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ProbationStartDate
		{
			set{ _probationstartdate=value;}
			get{return _probationstartdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? ProbationEndDate
		{
			set{ _probationenddate=value;}
			get{return _probationenddate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? PostsNature
		{
			set{ _postsnature=value;}
			get{return _postsnature;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? WageScales
		{
			set{ _wagescales=value;}
			get{return _wagescales;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? SocialSecurityBase
		{
			set{ _socialsecuritybase=value;}
			get{return _socialsecuritybase;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SocialSecurityAccount
		{
			set{ _socialsecurityaccount=value;}
			get{return _socialsecurityaccount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ProvidentFundBase
		{
			set{ _providentfundbase=value;}
			get{return _providentfundbase;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ProvidentFundAccount
		{
			set{ _providentfundaccount=value;}
			get{return _providentfundaccount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? BusinessInsurance
		{
			set{ _businessinsurance=value;}
			get{return _businessinsurance;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BusinessInsuranceAccount
		{
			set{ _businessinsuranceaccount=value;}
			get{return _businessinsuranceaccount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? BusinessInsuranceBase
		{
			set{ _businessinsurancebase=value;}
			get{return _businessinsurancebase;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? DepositaryBank
		{
			set{ _depositarybank=value;}
			get{return _depositarybank;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string BankAccount
		{
			set{ _bankaccount=value;}
			get{return _bankaccount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TRPNo
		{
			set{ _trpno=value;}
			get{return _trpno;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? TRPDate
		{
			set{ _trpdate=value;}
			get{return _trpdate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? TRPMonth
		{
			set{ _trpmonth=value;}
			get{return _trpmonth;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Property
		{
			set{ _property=value;}
			get{return _property;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string OperatorEid
		{
			set{ _operatoreid=value;}
			get{return _operatoreid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? OperateTime
		{
			set{ _operatetime=value;}
			get{return _operatetime;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ESurname
		{
			set{ _esurname=value;}
			get{return _esurname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public bool EVip
		{
			set{ _evip=value;}
			get{return _evip;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PY
		{
			set{ _py=value;}
			get{return _py;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? BandId
		{
			set{ _bandid=value;}
			get{return _bandid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? Prop
		{
			set{ _prop=value;}
			get{return _prop;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CmpId
		{
			set{ _cmpid=value;}
			get{return _cmpid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EX01
		{
			set{ _ex01=value;}
			get{return _ex01;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EX02
		{
			set{ _ex02=value;}
			get{return _ex02;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? x
		{
			set{ _x=value;}
			get{return _x;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? y
		{
			set{ _y=value;}
			get{return _y;}
		}
		/// <summary>
		/// 
		/// </summary>
		public Guid EGID
		{
			set{ _egid=value;}
			get{return _egid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? EX1
		{
			set{ _ex1=value;}
			get{return _ex1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? EX2
		{
			set{ _ex2=value;}
			get{return _ex2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EX3
		{
			set{ _ex3=value;}
			get{return _ex3;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EX4
		{
			set{ _ex4=value;}
			get{return _ex4;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EX5
		{
			set{ _ex5=value;}
			get{return _ex5;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EX6
		{
			set{ _ex6=value;}
			get{return _ex6;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EX7
		{
			set{ _ex7=value;}
			get{return _ex7;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EX8
		{
			set{ _ex8=value;}
			get{return _ex8;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EX9
		{
			set{ _ex9=value;}
			get{return _ex9;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EX10
		{
			set{ _ex10=value;}
			get{return _ex10;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EX11
		{
			set{ _ex11=value;}
			get{return _ex11;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EX12
		{
			set{ _ex12=value;}
			get{return _ex12;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EX13
		{
			set{ _ex13=value;}
			get{return _ex13;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? EX14
		{
			set{ _ex14=value;}
			get{return _ex14;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? EX15
		{
			set{ _ex15=value;}
			get{return _ex15;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? EX16
		{
			set{ _ex16=value;}
			get{return _ex16;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? EX17
		{
			set{ _ex17=value;}
			get{return _ex17;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? EX18
		{
			set{ _ex18=value;}
			get{return _ex18;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? Body
		{
			set{ _body=value;}
			get{return _body;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string wxopenid
		{
			set{ _wxopenid=value;}
			get{return _wxopenid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? wxsex
		{
			set{ _wxsex=value;}
			get{return _wxsex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string wxnickname
		{
			set{ _wxnickname=value;}
			get{return _wxnickname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string wxheadimg
		{
			set{ _wxheadimg=value;}
			get{return _wxheadimg;}
		}
		#endregion Model

	}
}

