using UKParliament.CodeTest.Web.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Reflection;

namespace UKParliament.CodeTest.Web.Controllers.Extensions
{
    public class ExceptionHandler
    {
        public T Execute<T>(MethodBase caller, Func<T> action, Func<T> exceptionAction)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                return exceptionAction();
            }
        }
    }
}