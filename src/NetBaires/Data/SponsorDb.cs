using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetBaires.Data
{
    public class SponsorDb
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public string LogoUrlHigh { get; set; }
        public List<SponsorEvent> Events { get; set; }
    }
}