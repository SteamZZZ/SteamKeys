using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using WebSteamParser.Models;
namespace WebSteamParser.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SteamController : ControllerBase
    {
        string _token = "STEAM-REALLY_PARSED-STAG-1304";
        private readonly u1286676_STEAM_PARSINGContext _context;
        public SteamController(u1286676_STEAM_PARSINGContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet]
        [Route("JsonData")]
        public async Task<IActionResult> GetJsonData(int start, int count,string token)
        {
            if (token == _token)
            {
                string result = "";
                string sql = "exec [dbo].[GET_JSON_LIST] @json OUTPUT, " + start + ", " + count;
                List<SqlParameter> parms = new List<SqlParameter>
            {
                // Create parameter(s)    
                new SqlParameter { ParameterName = "@json",Size =100000000, SqlDbType = System.Data.SqlDbType.NVarChar ,Direction= System.Data.ParameterDirection.Output}
            };

                var res = _context.Database.ExecuteSqlRaw(sql, parms);
                //var res=_context.Set<JsonRes>().FromSqlRaw(sql, parms.ToArray());
                //string JSONString = string.Empty;
                //JSONString = ;
                if (parms[0].Value != null)
                    return Ok(parms[0].Value.ToString());
                else
                    return NotFound("not-found");
            }
            else
                return BadRequest("Token is invalid");

        }

        [HttpGet]
        [Route("JsonDataByName")]
        public async Task<IActionResult> GetJsonDataByName(string name, string token)
        {
            if (token == _token)
            {
                string sql = "exec [dbo].[GET_JSON_LIST_BY_NAME] @json OUTPUT, @name" ;
                List<SqlParameter> parms = new List<SqlParameter>
            {
                // Create parameter(s)    
                new SqlParameter { ParameterName = "@json",Size =100000000, SqlDbType = System.Data.SqlDbType.NVarChar ,Direction= System.Data.ParameterDirection.Output},
                new SqlParameter { ParameterName = "@name",Size =200,Value=name, SqlDbType = System.Data.SqlDbType.NVarChar ,Direction= System.Data.ParameterDirection.Input},
            };

                var res = _context.Database.ExecuteSqlRaw(sql, parms);
                //var res=_context.Set<JsonRes>().FromSqlRaw(sql, parms.ToArray());
                //string JSONString = string.Empty;
                //JSONString = ;
                if (parms[0].Value != null)
                    return Ok(parms[0].Value.ToString());
                else
                    return NotFound("not-found");
            }
            else
                return BadRequest("Token is invalid");

        }

        [HttpGet]
        [Route("AddUser")]
        public async Task<IActionResult> AddUser(string login, string password,string token)
        {
            if (token == _token)
            {
                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                    return BadRequest("Invalid data");

                string sql = "exec [dbo].[ADD_USER] @login, @password, @id OUTPUT";
                List<SqlParameter> parms = new List<SqlParameter>
            {
                // Create parameter(s)    
                new SqlParameter { ParameterName = "@login",Size =100,Value=login, SqlDbType = System.Data.SqlDbType.NVarChar ,Direction= System.Data.ParameterDirection.Input},
                new SqlParameter { ParameterName = "@password",Size =100,Value=password, SqlDbType = System.Data.SqlDbType.NVarChar ,Direction= System.Data.ParameterDirection.Input},
                new SqlParameter { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int ,Direction= System.Data.ParameterDirection.Output},
            };

                var res = _context.Database.ExecuteSqlRaw(sql, parms);

                if ((int)parms[2].Value != -1)
                    return Ok((int)parms[2].Value);
                else
                    return BadRequest("User with this login already exists");
            }
            else
                return BadRequest("Token is invalid");
        }

        [HttpGet]
        [Route("VerifyUser")]
        public async Task<IActionResult> VerifyUser(string login, string password, string token)
        {
            if (token == _token)
            {
                if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                    return BadRequest("Invalid data");

                string sql = "exec [dbo].[VERIFY_USER] @login, @password, @id OUTPUT";
                List<SqlParameter> parms = new List<SqlParameter>
            {
                // Create parameter(s)    
                new SqlParameter { ParameterName = "@login",Size =100,Value=login, SqlDbType = System.Data.SqlDbType.NVarChar ,Direction= System.Data.ParameterDirection.Input},
                new SqlParameter { ParameterName = "@password",Size =100,Value=password, SqlDbType = System.Data.SqlDbType.NVarChar ,Direction= System.Data.ParameterDirection.Input},
                new SqlParameter { ParameterName = "@id", SqlDbType = System.Data.SqlDbType.Int ,Direction= System.Data.ParameterDirection.Output},
            };

                var res = _context.Database.ExecuteSqlRaw(sql, parms);

                if ((int)parms[2].Value == -1)
                    return BadRequest("Where is no user with such login");
                else if ((int)parms[2].Value == -2)
                    return BadRequest("Wrong password");
                else
                    return Ok((int)parms[2].Value);

            }
            else
                return BadRequest("Token is invalid");
        }

        [HttpGet]
        [Route("AddToFaforites")]
        public async Task<IActionResult> AddToFaforites(int user_id, int game_steam_id)
        {
            if (user_id<1 ||game_steam_id<1)
                return BadRequest("Invalid data");

            string sql = "exec [dbo].[ADD_TO_FAVORITES] @user_id, @game_id, @success OUTPUT";
            List<SqlParameter> parms = new List<SqlParameter>
            {
                // Create parameter(s)    
                new SqlParameter { ParameterName = "@user_id",Size =100,Value=user_id, SqlDbType = System.Data.SqlDbType.Int ,Direction= System.Data.ParameterDirection.Input},
                new SqlParameter { ParameterName = "@game_id",Size =100,Value=game_steam_id, SqlDbType = System.Data.SqlDbType.Int ,Direction= System.Data.ParameterDirection.Input},
                new SqlParameter { ParameterName = "@success", SqlDbType = System.Data.SqlDbType.Int ,Direction= System.Data.ParameterDirection.Output},
            };

            var res = _context.Database.ExecuteSqlRaw(sql, parms);

            switch ((int)parms[2].Value)
            {
                case 1:
                    return Ok("Added");
                case -1:
                    return Conflict("Already in favorites");
                case -2:
                    return BadRequest("There is no user or game with such id");
            }

            return BadRequest("Unknown exeption");
        }

        [HttpGet]
        [Route("RemoveFromFaforites")]
        public async Task<IActionResult> RemoveFromFaforites(int user_id, int game_steam_id)
        {
            if (user_id < 1 || game_steam_id < 1)
                return BadRequest("Invalid data");

            string sql = "exec [dbo].[REMOVE_FROM_FAVORITES] @user_id, @game_id";
            List<SqlParameter> parms = new List<SqlParameter>
            {
                // Create parameter(s)    
                new SqlParameter { ParameterName = "@user_id",Size =100,Value=user_id, SqlDbType = System.Data.SqlDbType.Int ,Direction= System.Data.ParameterDirection.Input},
                new SqlParameter { ParameterName = "@game_id",Size =100,Value=game_steam_id, SqlDbType = System.Data.SqlDbType.Int ,Direction= System.Data.ParameterDirection.Input}
                
            };

            var res = _context.Database.ExecuteSqlRaw(sql, parms);

            return Ok("Operation completed");
        }

        [HttpGet]
        [Route("GetFavorites")]
        public async Task<IActionResult> GetFavorites(int user_id)
        {
            if (user_id < 1 )
                return BadRequest("Invalid data");

            string sql = "exec [dbo].[GET_JSON_FAVORITES_LIST] @json OUTPUT, @user_id";
            List<SqlParameter> parms = new List<SqlParameter>
            {
                // Create parameter(s)    
                new SqlParameter { ParameterName = "@user_id",Size =100,Value=user_id, SqlDbType = System.Data.SqlDbType.Int ,Direction= System.Data.ParameterDirection.Input},
                new SqlParameter { ParameterName = "@json",Size =100000000, SqlDbType = System.Data.SqlDbType.NVarChar ,Direction= System.Data.ParameterDirection.Output},

            };

            var res = _context.Database.ExecuteSqlRaw(sql, parms);

            return Ok(parms[1].Value.ToString());
        }


    }
}