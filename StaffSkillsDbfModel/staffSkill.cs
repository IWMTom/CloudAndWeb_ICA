namespace StaffSkillsDbfModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("staffSkill")]
    public partial class staffSkill
    {
        public int staffSkillId { get; set; }

        [StringLength(50)]
        [Display(Name = "Staff Code")]
        public string staffCode { get; set; }

        [StringLength(50)]
        [Display(Name = "Skill Code")]
        public string skillCode { get; set; }

        public bool? active { get; set; }
    }
}
