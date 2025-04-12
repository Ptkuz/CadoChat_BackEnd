using CadoChat.Web.AspNetCore.WebResponse;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadoChat.Web.AspNetCore.MediatR
{
    public interface IRequestCommand<TResponse> : IRequest<TResponse>
        where TResponse : BaseResponse
    {

    }
}
