using GeoLocApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GeoLocApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeoLocationsController : ControllerBase
    {
        private readonly GeoLocApiContext _context;

        public GeoLocationsController(GeoLocApiContext context)
        {
            _context = context;
            //31.60.48.215
        }

        // GET: api/GeoLocations
        [HttpGet]
        public async Task<ActionResult<List<GeoLocation>>> GetGeoLocation()
        {
            if (_context.GeoLocations.Count() == 0)
            {
                return NotFound("No content in DB.");
            }
            var list = await _context.ListAsync();
            return Ok(list);
        }

        // GET: api/GeoLocations/134.201.250.155
        [HttpGet("{ip}")]
        public async Task<IActionResult> GetGeoLocation([FromRoute] string ip)
        {
            if (!IpValidated(ip))
            {
                string response = "Ip format not valid.";
                return BadRequest(response);
            }
            if (_context.GeoLocations.Count() == 0)
            {
                return NotFound("No data in DB at all.");
            }
            else if (!await _context.AlreadyExist(ip))
            {
                return NotFound("No such data in DB.");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var geoLocation = await _context.GeoLocations.AsNoTracking().SingleAsync(x => x.Ip.Equals(ip));

            return Ok(geoLocation);
        }

        // POST: api/GeoLocations
        [HttpPost]
        public async Task<IActionResult> PostGeoLocation([FromBody] InputModel inputModel)
        {
            if (!IpValidated(inputModel.ip))
            {
                string response = "Ip format not valid.";
                return BadRequest(response);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _context.AlreadyExist(inputModel.ip))
            {
                try
                {
                    CheckIP checkIP = new CheckIP(inputModel.ip);
                    GeoLocation geoLocation = checkIP.CreateGeoLoc();
                    if (!GeoLocValidation(geoLocation))
                    {
                        return BadRequest("This IP adress was not localized.");
                    }
                    else
                    {
                        _context.GeoLocations.Add(geoLocation);
                        await _context.SaveChangesAsync();

                        return Ok(geoLocation);
                    }
                }
                catch(WebException ex)
                {
                    string res = $"Web exception was catched.Check Internet connection.{Environment.NewLine}Error msg: {ex.Message}";
                    return NotFound(res);
                }
                catch(Exception ex)
                {
                    string res = $"Error: {ex.Message}";
                    return NotFound(ex.Message);
                }
            }
            else
            {
                string res = $"This Geo Location exist in database.You can see it by using GET: api/GeoLocations/{inputModel.ip}";
                return Ok(res);
            }
        }

        // DELETE: api/GeoLocations/31.60.48.215
        [HttpDelete("{ip}")]
        public async Task<IActionResult> DeleteGeoLocation([FromRoute] string ip)
        {
            if (!IpValidated(ip))
            {
                string response = "Ip format not valid.";
                return BadRequest(response);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await _context.AlreadyExist(ip))
            {
                return NotFound("No such data in DB.");
            }
            var geoLocation = await _context.GeoLocations.SingleAsync(x => x.Ip.Equals(ip));

            _context.GeoLocations.Remove(geoLocation);
            await _context.SaveChangesAsync();

            string res = $"Data with IP number: {geoLocation.Ip} was deleted succesfully.";
            return Ok(res);
        }

        private bool IpValidated(string ip)
        {
            if (string.IsNullOrWhiteSpace(ip))
            {
                return false;
            }
            string[] ipParts = ip.Split(".");
            if (ipParts.Length != 4)
            {
                return false;
            }

            foreach (string part in ipParts)
            {
                if (!byte.TryParse(part, out byte temporaryByte))
                {
                    return false;
                }
            }
            return true;
        }

        private bool GeoLocValidation(GeoLocation geoLocation)
        {
            if (string.IsNullOrEmpty(geoLocation.Continent))
            {
                return false;
            }
            return true;
        }
    }
}