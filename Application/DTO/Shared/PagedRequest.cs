using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.DTO.Shared
{
    public class PagedRequest
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 25;
    }
}
