



















// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `AGSoftware`
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Data Source=GREG;Initial Catalog=AGSoftware;Integrated Security=True;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False`
//     Schema:                 ``
//     Include Views:          `False`



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace AGSoftware
{

	public partial class AGSoftwareDB : Database
	{
		public AGSoftwareDB() 
			: base("AGSoftware")
		{
			CommonConstruct();
		}

		public AGSoftwareDB(string connectionStringName) 
			: base(connectionStringName)
		{
			CommonConstruct();
		}
		
		partial void CommonConstruct();
		
		public interface IFactory
		{
			AGSoftwareDB GetInstance();
		}
		
		public static IFactory Factory { get; set; }
        public static AGSoftwareDB GetInstance()
        {
			if (_instance!=null)
				return _instance;
				
			if (Factory!=null)
				return Factory.GetInstance();
			else
				return new AGSoftwareDB();
        }

		[ThreadStatic] static AGSoftwareDB _instance;
		
		public override void OnBeginTransaction()
		{
			if (_instance==null)
				_instance=this;
		}
		
		public override void OnEndTransaction()
		{
			if (_instance==this)
				_instance=null;
		}
        

		public class Record<T> where T:new()
		{
			public static AGSoftwareDB repo { get { return AGSoftwareDB.GetInstance(); } }
			public bool IsNew() { return repo.IsNew(this); }
			public object Insert() { return repo.Insert(this); }

			public void Save() { repo.Save(this); }
			public int Update() { return repo.Update(this); }

			public int Update(IEnumerable<string> columns) { return repo.Update(this, columns); }
			public static int Update(string sql, params object[] args) { return repo.Update<T>(sql, args); }
			public static int Update(Sql sql) { return repo.Update<T>(sql); }
			public int Delete() { return repo.Delete(this); }
			public static int Delete(string sql, params object[] args) { return repo.Delete<T>(sql, args); }
			public static int Delete(Sql sql) { return repo.Delete<T>(sql); }
			public static int Delete(object primaryKey) { return repo.Delete<T>(primaryKey); }
			public static bool Exists(object primaryKey) { return repo.Exists<T>(primaryKey); }
			public static bool Exists(string sql, params object[] args) { return repo.Exists<T>(sql, args); }
			public static T SingleOrDefault(object primaryKey) { return repo.SingleOrDefault<T>(primaryKey); }
			public static T SingleOrDefault(string sql, params object[] args) { return repo.SingleOrDefault<T>(sql, args); }
			public static T SingleOrDefault(Sql sql) { return repo.SingleOrDefault<T>(sql); }
			public static T FirstOrDefault(string sql, params object[] args) { return repo.FirstOrDefault<T>(sql, args); }
			public static T FirstOrDefault(Sql sql) { return repo.FirstOrDefault<T>(sql); }
			public static T Single(object primaryKey) { return repo.Single<T>(primaryKey); }
			public static T Single(string sql, params object[] args) { return repo.Single<T>(sql, args); }
			public static T Single(Sql sql) { return repo.Single<T>(sql); }
			public static T First(string sql, params object[] args) { return repo.First<T>(sql, args); }
			public static T First(Sql sql) { return repo.First<T>(sql); }
			public static List<T> Fetch(string sql, params object[] args) { return repo.Fetch<T>(sql, args); }
			public static List<T> Fetch(Sql sql) { return repo.Fetch<T>(sql); }
			public static List<T> Fetch(long page, long itemsPerPage, string sql, params object[] args) { return repo.Fetch<T>(page, itemsPerPage, sql, args); }
			public static List<T> Fetch(long page, long itemsPerPage, Sql sql) { return repo.Fetch<T>(page, itemsPerPage, sql); }
			public static List<T> SkipTake(long skip, long take, string sql, params object[] args) { return repo.SkipTake<T>(skip, take, sql, args); }
			public static List<T> SkipTake(long skip, long take, Sql sql) { return repo.SkipTake<T>(skip, take, sql); }
			public static Page<T> Page(long page, long itemsPerPage, string sql, params object[] args) { return repo.Page<T>(page, itemsPerPage, sql, args); }
			public static Page<T> Page(long page, long itemsPerPage, Sql sql) { return repo.Page<T>(page, itemsPerPage, sql); }
			public static IEnumerable<T> Query(string sql, params object[] args) { return repo.Query<T>(sql, args); }
			public static IEnumerable<T> Query(Sql sql) { return repo.Query<T>(sql); }

		}

	}
	



    

	[TableName("dbo.StorytimePost")]



	[PrimaryKey("StorytimePostId")]



	[ExplicitColumns]
    public partial class StorytimePost : AGSoftwareDB.Record<StorytimePost>  
    {



		[Column] public int StorytimePostId { get; set; }





		[Column] public int UserId { get; set; }





		[Column] public string PostText { get; set; }





		[Column] public string ImagePath { get; set; }





		[Column] public int SentToUserId { get; set; }





		[Column] public DateTime DateCreated { get; set; }



	}

    

	[TableName("dbo.Storytime")]



	[PrimaryKey("StorytimeId")]



	[ExplicitColumns]
    public partial class Storytime : AGSoftwareDB.Record<Storytime>  
    {



		[Column] public int StorytimeId { get; set; }





		[Column] public string StorytimeTitle { get; set; }





		[Column] public DateTime DateCreated { get; set; }



	}

    

	[TableName("dbo.StorytimeGroup")]



	[PrimaryKey("StorytimeGroupId")]



	[ExplicitColumns]
    public partial class StorytimeGroup : AGSoftwareDB.Record<StorytimeGroup>  
    {



		[Column] public int StorytimeGroupId { get; set; }





		[Column] public int StorytimeId { get; set; }





		[Column] public int UserGroupId { get; set; }



	}

    

	[TableName("dbo.UserGroup")]



	[PrimaryKey("UserGroupId")]



	[ExplicitColumns]
    public partial class UserGroup : AGSoftwareDB.Record<UserGroup>  
    {



		[Column] public int UserGroupId { get; set; }





		[Column] public int UserId { get; set; }





		[Column] public string GroupName { get; set; }





		[Column] public DateTime DateCreated { get; set; }



	}

    

	[TableName("dbo.UserGroupUser")]



	[PrimaryKey("UserGroupUserId")]



	[ExplicitColumns]
    public partial class UserGroupUser : AGSoftwareDB.Record<UserGroupUser>  
    {



		[Column] public int UserGroupUserId { get; set; }





		[Column] public int UserId { get; set; }





		[Column] public int GroupId { get; set; }



	}

    

	[TableName("dbo.User")]



	[PrimaryKey("UserId")]



	[ExplicitColumns]
    public partial class User : AGSoftwareDB.Record<User>  
    {



		[Column] public int UserId { get; set; }





		[Column] public string FirstName { get; set; }





		[Column] public string LastName { get; set; }





		[Column] public string Username { get; set; }





		[Column] public string Password { get; set; }





		[Column] public string Email { get; set; }





		[Column] public DateTime DateCreated { get; set; }



	}


}



