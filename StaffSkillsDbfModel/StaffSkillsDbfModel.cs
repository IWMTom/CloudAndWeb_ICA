namespace StaffSkillsDbfModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class StaffSkillsDbfModel : DbContext
    {
        public StaffSkillsDbfModel()
            : base("name=StaffSkillsDbfModel")
        {
        }

        public virtual DbSet<staffSkill> staffSkills { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
