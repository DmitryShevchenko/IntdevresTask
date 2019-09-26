using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PcDataController : Controller
    {
        private readonly AppDbContext _db;

        public PcDataController(AppDbContext dbContext)
        {
            _db = dbContext;
        }

        [HttpPost("[action]")]
        public async Task Post([FromBody] DataModel model)
        {
            using (_db)
            {
                var dbDataModel = await _db.PcData.FirstOrDefaultAsync(x => x.PcName == model.PcName);

                if (dbDataModel != null)
                {
                    //Load related entity to context
                    await _db.Entry(dbDataModel).Collection(x => x.Users).LoadAsync();
                    await _db.Entry(dbDataModel).Reference(x => x.Manufacturer).LoadAsync();
                    //Update Model
                    dbDataModel.PcName = model.PcName;
                    dbDataModel.OS = model.OS;
                    dbDataModel.CpuLoad = model.CpuLoad;
                    dbDataModel.RamLoad = model.RamLoad;
                    dbDataModel.Manufacturer = model.Manufacturer;
                    dbDataModel.Users = model.Users;

                    _db.Update(dbDataModel);
                }
                else
                {
                    _db.PcData.Add(model);
                }

                await _db.SaveChangesAsync();
            }
        }
    }
}