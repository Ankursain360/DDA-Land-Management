using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Libraries.Model.Entity;
using Libraries.Service.IApplicationService;
using LeaseDetails.Models;
using Notification;
using Notification.Constants;
using Notification.OptionEnums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dto.Search;
using LeaseDetails.Filters;
using Core.Enum;
using Utility.Helper;

namespace LeaseDetails.Controllers
{
    public class ProceedingEvictionLetterController : BaseController
    {

        private readonly IProceedingEvictionLetterService _proceedingEvictionLetterService;

        public ProceedingEvictionLetterController(IProceedingEvictionLetterService proceedingEvictionLetterService)
        {
            _proceedingEvictionLetterService = proceedingEvictionLetterService;
        }
    }
}