using BMICalculator;
using ProjectEssential.BMIModel;
using ProjectEssential.EnumLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BMI.Controllers
{
    [RoutePrefix("webapi/bmi")]
    public class BMIApiController : ApiController
    {
        private BMIDbContext _db;


        public BMIApiController()
        {
            _db = new BMIDbContext();
        }

        public BMIApiController(BMIDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetData()
        {
            var data = _db.BMI
                .Select(m => new
                {
                    m.Name,
                    m.Height,
                    m.BMIValue,
                    m.Unit,
                    m.Weight
                })
                .AsEnumerable()
                .Select(m => new BMIViewModel
                {
                    BMIValue = m.BMIValue,
                    Height = m.Height,
                    Weight = m.Weight,
                    Name = m.Name,
                    Unit = GetUnitTypeFriendlyName(m.Unit)
                }).ToList();

            return Json(data);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult PostBMI(BMIViewModel model)
        {
            var _bmi = new ProjectEssential.BMIModel.BMI()
            {
                Weight = model.Weight,
                Height = model.Height,
                Name = model.Name,
            };
            if ((Int32)UnitTypes.InchesAndPound == int.Parse(model.Unit))
            {
                var weight = _bmi.Weight * .45;
                var height = _bmi.Height * .0254;
                _bmi.Unit = UnitTypes.InchesAndPound;
                _bmi.BMIValue = weight / (height * height);
            }
            else
            {
                var height = _bmi.Height * .01;
                _bmi.Unit = UnitTypes.CentimeterAndKilogram;
                _bmi.BMIValue = _bmi.Weight / (height * height);
            }

            using (var _db = new BMIDbContext())
            {
                _db.BMI.Add(_bmi);
                _db.SaveChanges();
            }

            return Ok(_bmi.BMIValue);
        }

        public string GetUnitTypeFriendlyName(UnitTypes type)
        {
            switch (type)
            {
                case UnitTypes.CentimeterAndKilogram:
                    return "Centimeter & KG";

                default:
                case UnitTypes.InchesAndPound:
                    return "Inches & Pound";

            }
        }
    }
}
