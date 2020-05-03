using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using test2.Helpers;
using test2.Models;
using test2.Models.Performances;

namespace test2.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PerformanceController : ControllerBase
    {
        private readonly IPerformanceHelper helper;
        private readonly UserManager<ApplicationUser> userManager;
        public PerformanceController(IPerformanceHelper helper, UserManager<ApplicationUser> _userManager)
        {
            this.helper = helper;
            this.userManager = _userManager;
        }

        [HttpPost]
        [Route("{page:int}/{count:int}/performances/{date?}/{nameSearch?}")]
        public async Task<PerformanceModel> GetPerformances(Int32 page, Int32 count, DateTime? date, String nameSearch)
        {
            PerformanceModel model = new PerformanceModel();
            model.Performances = await helper.GetPerformances(page, count, date, nameSearch);
            model.Total = await helper.GetTotal(date, nameSearch);
            return model;
        }

        [HttpPost]
        [Route("all")]
        public async Task<List<Performance>> GetAll()
        {
            return await helper.GetAll();
        }

        [HttpPost]
        [Route("getItem/{id}")]
        public async Task<Performance> GetItem(String id)
        {
            return await helper.GetPerformance(Guid.Parse(id));
        }

        [HttpPost]
        [Route("book/{id}/{timeId}/{count}")]
        [Authorize]
        public async Task BookPerformance(String id, String timeId, Int32 count)
        {
            var userId = userManager.GetUserId(User);
            await helper.Book(id, timeId, count, userId);
        }

        [HttpPost]
        [Route("edit")]
        [Authorize(Policy = "IsAdmin")]
        public async Task<Boolean> Edit([FromBody]Performance performance)
        {
            return await helper.Edit(performance);
        }
    }
}