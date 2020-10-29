using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MedismartsAPI.Utilities
{
    public class HandleException : Attribute, IExceptionFilter
    {
        private IUtilityService _service;


        public HandleException(IUtilityService services)
        {
            _service = services;
        }
        public void OnException(ExceptionContext context)
        {


            bool isHandled = context.ExceptionHandled;

            if (!isHandled && context.Exception is Exception ex)
            {
              
                // Log To File
                _service.LogError(ex.Message);
               

           

                Response responseObject = new Response()
                {
                    //statusCode
                    responseData = null,
                    description = "an error has occured",
                    statusCode = "99",
                    message = "failed"
                };

                context.Result = new BadRequestObjectResult(responseObject);
               
            }
        }
    }
}
