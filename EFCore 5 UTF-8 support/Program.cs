using EFCore_5_UTF_8_support.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore_5_UTF_8_support
{
	static class Program
	{
		static async Task Main()
		{

			// change to your appropriate SQL 20219 instance 
			var sql2019instance = "data source=.\\SQLEXPRESS01;initial catalog=testutf8;integrated security=yes";
			var options = new DbContextOptionsBuilder<TestDBContext>().UseSqlServer(sql2019instance).Options;
			var context = new TestDBContext(options);
			await context.Database.EnsureCreatedAsync();

			MyTable myTable = new() { Param = "Как дела?" };
			context.MyTable.Add(myTable );
			await context.SaveChangesAsync();

			// read back
			var myTableCheck = await context.MyTable.OrderBy(o => o.Id).LastOrDefaultAsync();
			if (myTableCheck.Param != myTable.Param)
			{
				throw new Exception($"we found {myTableCheck.Param} but it should be {myTable.Param}");
			}


		}
	}
}
