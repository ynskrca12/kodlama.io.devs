using Application.Features.Teknologies.Dtos;
using Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Teknologies.Models
{
    public class TeknologyListModel : BasePageableModel
    {
        public IList<TeknologyListDto> Items { get; set; }
    }
}
