using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using TeisterMask.Data.Models.Enums;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Task")]
    public class ImportTasksArrayDto
    {
        [XmlElement("Name")]
        [Required]
        [MinLength(2), MaxLength(40)]
        public string Name { get; set; }

        [XmlElement("OpenDate")]
        [Required]
        public DateTime OpenDate { get; set; }

        [XmlElement("DueDate")]
        [Required]
        public DateTime DueDate { get; set; }

        [XmlElement("ExecutionType")]
        [Required]
        public ExecutionType ExecutionType { get; set; }

        [XmlElement("LabelType")]
        [Required]
        public LabelType LabelType { get; set; }
    }
}