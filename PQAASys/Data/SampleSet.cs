using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace PQAASys
{
    public partial class SampleSet
    {
        public SampleSet()
        {
            Requests = new HashSet<Request>();
        }
        [Key]
        public int sampleSetId { get; set; }
        [Required(ErrorMessage ="Необходимо указать количество образцов")]
        [Range(1,100)]
        public int numOfSamples { get; set; }
        [Required(ErrorMessage = "Необходимо указать тип продукта")]
        public string TypeOfProduct { get; set; }
        [Required(ErrorMessage = "Необходимо указать марку материала")]
        public string MaterialGrade { get; set; }
        [Required(ErrorMessage = "Необходимо указать маркировку образцов")]
        public string Marking { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
