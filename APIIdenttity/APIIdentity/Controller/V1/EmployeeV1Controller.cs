using Microsoft.AspNetCore.Mvc;
using APIIdentity.BusinessLayers.Contracts;
using APIIdentity.Models;

namespace APIIdentity.Controller.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EmployeeV1Controller : ControllerBase
    {
        private IEmployeeBusinessLayer _employeeBusinessLayer;
        private ILogger<EmployeeV1Controller> _logger;
        public EmployeeV1Controller(IEmployeeBusinessLayer employeeBusinessLayer, ILogger<EmployeeV1Controller> logger)
        {
            _employeeBusinessLayer = employeeBusinessLayer;
            _logger = logger;
            _logger.LogInformation("EmployeeV1Controller invoked");
        }

        /// <summary>
        /// Get list of all Emplyees
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetEmployees")]
        public ActionResult<List<Employee>> GetEmployees()
        {
            _logger.LogInformation("GetEmployees method started");
            try
            {
                return Ok(_employeeBusinessLayer.GetEmployees());
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get employee by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}", Name = "GetEmployeeById")]
        public ActionResult<Employee> GetEmployeeById(int Id)
        {
            _logger.LogInformation("GetEmployeeById method started");
            try
            {
                var employee = _employeeBusinessLayer.GetEmployee(Id);
                if (employee == null)
                {
                    _logger.LogInformation($"No data found for Id {Id}");
                    return NoContent();
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        /// <summary>
        /// Add a new employee
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost(Name = "AddEmployee")]
        public ActionResult<Employee> AddEmployee([FromBody] Employee employee)
        {
            _logger.LogInformation("AddEmployee method started");
            try
            {
                return StatusCode(StatusCodes.Status201Created, _employeeBusinessLayer.AddEmployee(employee));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Update an employee by Id
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut(Name = "UpdateEmployee")]
        public ActionResult<Employee> UpdateEmployee(int employeeId, Employee employee)
        {
            _logger.LogInformation("UpdateEmployee method started");
            try
            {
                return Ok(_employeeBusinessLayer.UpdateEmployee(employeeId, employee));
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Delete an employee by Id
        /// </summary>
        /// <param name="employeeId"></param>
        [HttpDelete(Name = "DeleteEmployee")]
        public ActionResult DeleteEmployee(int employeeId)
        {
            _logger.LogInformation("DeleteEmployee method started");
            try
            {
                _employeeBusinessLayer.DeleteEmployee(employeeId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
