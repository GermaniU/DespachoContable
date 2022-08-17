using Contracts;
using DespachoContable.Common;
using DespachoContable.Models;
using DespachoContable.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using static NuGet.Packaging.PackagingConstants;

namespace DespachoContable.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IServiceManager _serviceManager;

        public EmployeeController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager ?? throw new ArgumentNullException(nameof(serviceManager));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<EmployeeDTO> employees = await _serviceManager.EmployeeServices.GetAllAsync();

                List<EmployeeViewModel> employeeViewModels = GetEmployeeViewModel(employees);

                return View(employeeViewModels);

            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            try
            {
                var employeeDTO = await _serviceManager.EmployeeServices.GetByIdAsync(id);

                if (employeeDTO == null)
                {
                    return NotFound();
                }

                return View(employeeDTO);
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                IEnumerable<PositionDTO> positions = await _serviceManager.PositionService.GetAllAsync();

                ViewBag.PositionList = ToSelectList(positions, Guid.Empty);

                if (positions == null)
                {
                    return NotFound("First Register Positions");
                }

                return View();
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,ApellidoPaterno,ApellidoMaterno,FechaNacimiento,Genero,EstadoCivil,Rfc,Direccion,Email,Telefono,FechaAlta,FechaBaja,IdPuesto")] EmployeeForCreationDTO employeeDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    employeeDTO.TrimAllStrings();

                    await _serviceManager.EmployeeServices.CreateAsync(employeeDTO);

                    return RedirectToAction(nameof(Index));
                }
                return View(employeeDTO);
            }
            catch (Exception)
            {

                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var employeeDTO = await _serviceManager.EmployeeServices.GetByIdAsync(id);

                IEnumerable<PositionDTO> positions = await _serviceManager.PositionService.GetAllAsync();

                if (employeeDTO == null)
                {
                    return NotFound();
                }

                if (positions == null)
                {
                    return NotFound("First Register Positions");
                }

                EmployeeForUpdateViewModel employeeForUpdate = GetEmployeeForUpdateViewModel(employeeDTO);

                ViewBag.PositionList = ToSelectList(positions, employeeDTO.IdPuesto);

                return View(employeeForUpdate);
            }
            catch (Exception)
            {

                return View("Error");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EmployeeForUpdateViewModel EmployeeViewModel)
        {
            try
            {
                EmployeeForUpdateDTO employeeForUpdate = null;

                //Remove propeties not requiered
                ModelState.Remove("Rfc");
                ModelState.Remove("Genero");
                ModelState.Remove("Nombre");
                ModelState.Remove("ApellidoPaterno");
                ModelState.Remove("ApellidoMaterno");

                if (ModelState.IsValid)
                {
                    employeeForUpdate = GetEmployeeForUpdatel(EmployeeViewModel);

                    employeeForUpdate.TrimAllStrings();

                    await _serviceManager.EmployeeServices.UpdateAsync(id, employeeForUpdate);

                    return RedirectToAction(nameof(Index));
                }

                return View(employeeForUpdate);
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Unsubscribe(Guid id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                await _serviceManager.EmployeeServices.UnsubscribeAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Search(EmployeeFiltersViewModel employeeFiltersDTO)
        {
            try
            {

                if (!string.IsNullOrWhiteSpace(employeeFiltersDTO.Nombre)
                     || !string.IsNullOrWhiteSpace(employeeFiltersDTO.Rfc)
                     || !string.IsNullOrWhiteSpace(employeeFiltersDTO.IdStatus))
                {

                    employeeFiltersDTO.TrimAllStrings();

                    EmployeeFiltersDTO employeeFilters = GetEmployeeFiltertDto(employeeFiltersDTO);

                    IEnumerable<EmployeeDTO> employees = await _serviceManager.EmployeeServices.GetAllAsyncFiltered(employeeFilters);

                    List<EmployeeViewModel> employeeViewModels = GetEmployeeViewModel(employees);

                    return View("Index", employeeViewModels);
                }

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        private static EmployeeFiltersDTO GetEmployeeFiltertDto(EmployeeFiltersViewModel employeeFiltersDTO)
        {
            EmployeeFiltersDTO employeeFilters = new EmployeeFiltersDTO();

            employeeFilters.Nombre = employeeFiltersDTO.Nombre;
            employeeFilters.Rfc = employeeFiltersDTO.Rfc;

            if (!string.IsNullOrWhiteSpace(employeeFiltersDTO.IdStatus))
            {
                employeeFilters.lEmployeeUnsuscribed = bool.Parse(employeeFiltersDTO.IdStatus);
            }

            return employeeFilters;
        }

        private static List<EmployeeViewModel> GetEmployeeViewModel(IEnumerable<EmployeeDTO> employees)
        {
            List<EmployeeViewModel> employeeViewModels = new List<EmployeeViewModel>();

            foreach (var employee in employees)
            {
                EmployeeViewModel employeeViewModel = new EmployeeViewModel();

                employeeViewModel.NombreCompleto = $"{employee.Nombre} {employee.ApellidoPaterno} {employee.ApellidoMaterno}";
                employeeViewModel.Puesto = employee.Puesto;
                employeeViewModel.Email = employee.Email;
                employeeViewModel.FechaAlta = employee.FechaAlta;
                employeeViewModel.Rfc = employee.Rfc;
                employeeViewModel.Id = employee.Id;

                employeeViewModels.Add(employeeViewModel);


            }

            return employeeViewModels;
        }

        private SelectList ToSelectList(IEnumerable<PositionDTO> positions, Guid IdPuesto)
        {
            List<SelectListItem> list = new List<SelectListItem>();

            foreach (PositionDTO position in positions)
            {
                list.Add(new SelectListItem()
                {
                    Text = position.Nombre,
                    Value = position.IdPuesto.ToString(),
                    Selected = IdPuesto == position.IdPuesto ? true : false
                });
            }

            return new SelectList(list, "Value", "Text");
        }

        private static EmployeeForUpdateViewModel GetEmployeeForUpdateViewModel(EmployeeDTO employeeDTO)
        {
            var employeeForUpdate = new EmployeeForUpdateViewModel();

            employeeForUpdate.Id = employeeDTO.Id;
            employeeForUpdate.Email = employeeDTO.Email;
            employeeForUpdate.Telefono = employeeDTO.Telefono;
            employeeForUpdate.EstadoCivil = employeeDTO.EstadoCivil;
            employeeForUpdate.FechaBaja = employeeDTO.FechaBaja;
            employeeForUpdate.FechaAlta = employeeDTO.FechaAlta;
            employeeForUpdate.IdPuesto = employeeDTO.IdPuesto;
            employeeForUpdate.Direccion = employeeDTO.Direccion;
            employeeForUpdate.Nombre = employeeDTO.Nombre;
            employeeForUpdate.ApellidoMaterno = employeeDTO.ApellidoMaterno;
            employeeForUpdate.ApellidoPaterno = employeeDTO.ApellidoPaterno;
            employeeForUpdate.Rfc = employeeDTO.Rfc;
            employeeForUpdate.Genero = employeeDTO.Genero;
            employeeForUpdate.FechaNacimiento = employeeDTO.FechaNacimiento;

            return employeeForUpdate;
        }

        private static EmployeeForUpdateDTO GetEmployeeForUpdatel(EmployeeForUpdateViewModel EmployeeViewModel)
        {
            var employeeForUpdate = new EmployeeForUpdateDTO();

            employeeForUpdate.Email = EmployeeViewModel.Email;
            employeeForUpdate.Telefono = EmployeeViewModel.Telefono;
            employeeForUpdate.EstadoCivil = EmployeeViewModel.EstadoCivil;
            employeeForUpdate.FechaBaja = EmployeeViewModel.FechaBaja;
            employeeForUpdate.IdPuesto = EmployeeViewModel.IdPuesto;
            employeeForUpdate.Direccion = EmployeeViewModel.Direccion;


            return employeeForUpdate;
        }
    }
}
