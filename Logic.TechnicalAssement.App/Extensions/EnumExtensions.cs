using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace InvestmentManagement.Shared.Extensions
{
	public static class EnumExtensions
	{
		public static string GetDisplayName(this Enum value)
		{
			return value.GetType()?
							.GetMember(value.ToString())?
							.First()?
							.GetCustomAttribute<DisplayAttribute>()?
							.GetName();
		}
	}
}
