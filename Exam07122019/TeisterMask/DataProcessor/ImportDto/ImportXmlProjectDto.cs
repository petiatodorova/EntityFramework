using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace TeisterMask.DataProcessor.ImportDto
{
    [XmlType("Project")]
    public class ImportXmlProjectDto
    {
        [XmlElement("Name")]
        [Required]
        [MinLength(2), MaxLength(40)]
        public string Name { get; set; }

        [XmlElement("OpenDate")]
        [Required]
        public DateTime OpenDate { get; set; }

        [XmlElement("DueDate")]
        public DateTime? DueDate { get; set; }

        [XmlArray("Tasks")]
        public ImportTasksArrayDto[] Tasks { get; set; }
    }
}
