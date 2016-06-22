using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Seminar.DAL.DTO;
using Seminar.Model;

namespace Seminar.DAL.Repository
{
    public class CompanyRepository : RepositoryBase<Company>
    {
        public CompanyRepository(TravelWarrantManagerDbContext context)
            : base(context)
        {
        }

        public List<CompanyDTO> GetAllDTO(string q=null)
        {
            var list = this.DbContext.Companies.
                Where(p=> q== null || p.Name.Contains(q)).
                Select(p => new CompanyDTO()
                {
                    Name = p.Name,
                    Email = p.Email,
                    Address = p.Address,
                    City = p.City,
                    OIB = p.OIB
                }).ToList();
            return list;
        }

        public CompanyDTO GetCompany(int id)
        {
            var company = this.DbContext.Companies.Where(p => p.ID == id).
                Select(p => new CompanyDTO()
                {
                    Name = p.Name,
                    Email = p.Email,
                    Address = p.Address,
                    City = p.City,
                    OIB = p.OIB
                }).FirstOrDefault();
            return company;
        }
        public CompanyDTO GetDTO(int id)
        {
            var entity = DbContext.Companies
                .Include(p => p.Cars)
                .FirstOrDefault(p => p.ID == id);

            var dto = new CompanyDTO();
            dto.ID = entity.ID;
            dto.Name = entity.Name;
            dto.Address = entity.Address;
            dto.City = entity.City;
            dto.Email = entity.Email;
            dto.OIB = entity.OIB;
            //Mapiranje auta
            dto.Cars.AddRange(entity.Cars.Select(entityCar=> new CarDTO
            {
                ID = entityCar.ID,
                Name = entityCar.Name,
                FuelConsumption = entityCar.FuelConsumption,
                CompanyID = entityCar.CompanyID,
                Year = entityCar.Year,
                CompanyName = entityCar.Company.Name
            }));
            //mapiranje radnika
            dto.Employers.AddRange(entity.Employees.Select(entityEmployee => new EmployeeDTO
            {
                ID = entityEmployee.ID,
                Name = entityEmployee.Name,
                OIB = entityEmployee.OIB,
                CompanyID = entityEmployee.CompanyID,
                CompanyName = entityEmployee.Company.Name
            }));
            
            //mapiranje naloga
            dto.TravelWarrants.AddRange(entity.TravelWarrants.Select(entityTravelWarrant =>new TravelWarrantDTO
            {
                ID = entityTravelWarrant.ID,
                Relation = entityTravelWarrant.Relation,
                Date = entityTravelWarrant.Date,
                StartKilometer = entityTravelWarrant.StartKilometer,
                EndKilometer = entityTravelWarrant.EndKilometer,
                Kilometer = entityTravelWarrant.Kilometer,
                Description = entityTravelWarrant.Description,
                CarID = entityTravelWarrant.CarID,
                CarName = entityTravelWarrant.Car.Name,
                EmployeeID = entityTravelWarrant.EmployeeID,
                EmployeeName = entityTravelWarrant.Employee.Name,
                CompanyID = entityTravelWarrant.CompanyID,
                CompanyName = entityTravelWarrant.Company.Name
            }));
            foreach (var travelWarrant in entity.TravelWarrants)
            {
                dto.TotalKilometer += travelWarrant.Kilometer;
            }
            return dto;
        }

        public void UpdateDTO(CompanyDTO model, string username)
        {
            
            
            var entity =
                this.DbContext.Companies
                    .Include(p => p.Cars)
                    .Include(p => p.Employees)
                    .FirstOrDefault(p => p.ID == model.ID);
            entity.Name = model.Name;
            entity.Address = model.Address;
            entity.City = model.City;
            entity.Email = model.Email;
            entity.OIB = model.OIB;
            /*auti*/
            if (model.Cars != null)
            {
                foreach (var entityCar in entity.Cars.ToList())
                {
                    var car=model.Cars.Where(p => p.ID > 0).ToList().Find(p => p.ID == entityCar.ID);
                    if (car != null)
                    {
                        entityCar.Name = car.Name;
                        entityCar.FuelConsumption = car.FuelConsumption;
                        entityCar.Year = car.Year;
                        var carRepo = new CarRepository(DbContext);
                        carRepo.Update(entityCar, username);
                    }
                    else
                    {
                        var carRepository = new CarRepository(DbContext);
                        carRepository.Delete(entityCar.ID);
                        entity.Cars.ToList().RemoveAll(p => p.ID == entityCar.ID);
                    }
                }
                foreach (var dtoCar in model.Cars.Where(p => p.ID == 0))
                {
                    var car = new Car();
                    car.Name = dtoCar.Name;
                    car.FuelConsumption = dtoCar.FuelConsumption;
                    car.Year = dtoCar.Year;
                    car.CompanyID = model.ID;
                    var carRepository = new CarRepository(DbContext);
                    carRepository.Add(car, username);
                }
            }
            else
            {
                if (entity.Cars != null)
                {
                    foreach (var entityCar in entity.Cars.ToList())
                    {
                        var carRepository = new CarRepository(DbContext);
                        carRepository.Delete(entityCar.ID);
                        entity.Cars.ToList().RemoveAll(p => p.ID == entityCar.ID);
                    }
                }
                
            }
            /*zaposlenici*/
            if (model.Employers !=null)
            {
                foreach (var entityEmployee in entity.Employees.ToList())
                {
                    var employee = model.Employers.Where(p => p.ID > 0).ToList().Find(p => p.ID == entityEmployee.ID);
                    if (employee != null)
                    {
                        entityEmployee.Name = employee.Name;
                        entityEmployee.OIB = employee.OIB;
                        var employeeRepository = new EmployeeRepository(DbContext);
                        employeeRepository.Update(entityEmployee, username);
                    }
                    else
                    {
                        var employeeRepository = new EmployeeRepository(DbContext);
                        employeeRepository.Delete(entityEmployee.ID);
                        entity.Employees.ToList().RemoveAll(p => p.ID == entityEmployee.ID);
                    }
                }
                foreach (var employeeDto in model.Employers.Where(p=>p.ID==0))
                {
                    var employee=new Employee();
                    employee.Name = employeeDto.Name;
                    employee.OIB = employeeDto.OIB;
                    employee.CompanyID = model.ID;
                    var empRepository=new EmployeeRepository(DbContext);
                    empRepository.Add(employee,username);
                }
            }
            else
            {
                if (entity.Employees != null)
                {
                    foreach (var entityEmployee in entity.Employees.ToList())
                    {
                        var employeeRepository = new EmployeeRepository(DbContext);
                        employeeRepository.Delete(entityEmployee.ID);
                        entity.Employees.ToList().RemoveAll(p => p.ID == entityEmployee.ID);
                    }
                }
                
            }
            
            base.Update(entity,username);
        }

        public void AddDTO(CompanyDTO model,string username)
        {
            var entity = new Company();
            entity.Name = model.Name;
            entity.Address = model.Address;
            entity.City = model.City;
            entity.Email = model.Email;
            entity.OIB = model.OIB;
            base.Add(entity, username);
            foreach (var carDto in model.Cars)
            {
                var car=new Car();
                car.Name = carDto.Name;
                car.FuelConsumption = carDto.FuelConsumption;
                car.Year = carDto.Year;
                car.CompanyID = entity.ID;
                var carRepository=new CarRepository(DbContext);
                carRepository.Add(car,username);
            }
            foreach (var employeeDto in model.Employers)
            {
                var employee=new Employee();
                employee.Name = employeeDto.Name;
                employee.OIB = employeeDto.OIB;
                employee.CompanyID = entity.ID;
                var employeeRepository=new EmployeeRepository(DbContext);
                employeeRepository.Add(employee,username);
            }
            
        }

    }
}
