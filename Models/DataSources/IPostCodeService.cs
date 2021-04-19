using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClientAddressManager.Models
{
    public interface IPostCodeService
    {
        Task<Result<string>> GetPostCodeAsync(string address);
    }
}
