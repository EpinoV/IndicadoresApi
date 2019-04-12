using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SN.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IndicatorsController : ControllerBase
    {
        private readonly IOptions<Models.RegExFormats> RegExFormats;

        public IndicatorsController(IOptions<Models.RegExFormats> regExFormats)
        {
            RegExFormats = regExFormats;
        }

        [HttpGet("GetUF")]
        [Authorize]
        public ActionResult GetUF(string date)
        {
            Regex regEx = new Regex(RegExFormats.Value.DateFormat);
            
            if(!regEx.IsMatch(date))
            {
                return BadRequest("La fecha no tiene el formato correcto");
            }

            var dateRequest = DateTime.ParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);

            var indicator = new Application.UfIndicators(dateRequest);
            var result = indicator.Get();

            return Ok(result);

        }

    }
}