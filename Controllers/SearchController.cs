using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kms.Data.Entities;
using kms.Data;
using kms.Models;
using kms.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using kms.Utils;

namespace kms.Controllers
{
    [KmsController, Authorize]
    public class SearchController : Controller
    {
        private int UserId { get { return int.Parse(User.Identity.Name); } }
        private readonly KMSDBContext _db;
        private readonly ISearchRepository _search;

        public SearchController(KMSDBContext context, ISearchRepository search) {
            this._db = context;
            this._search = search;
        }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] int? offset, [FromQuery] int? limit, [FromQuery] string query) {
            var response = await _search.CommonSearch(query, limit, offset);
            return Ok(response);
        }
    }
}
