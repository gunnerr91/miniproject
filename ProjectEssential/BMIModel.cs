using ProjectEssential.EnumLib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEssential.BMIModel
{
    public class BMI
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Column("Name", Order = 1)]
        public string Name { get; set; }

        [Column("Height", Order = 2)]
        public int Height { get; set; }

        [Column("Weight", Order = 3)]
        public int Weight { get; set; }

        [Column("Unit", Order = 4)]
        public UnitTypes Unit { get; set; }

        [Column("BMI", Order = 5)]
        public double BMIValue { get; set; }
    }

    public class BMIDbContext: DbContext
    {
        public BMIDbContext()
            : base("BMIContext")
        {
        }

        public DbSet<BMI> BMI { get; set; }
    }

}
