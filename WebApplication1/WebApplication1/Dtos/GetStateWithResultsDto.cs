using System.Collections.Generic;

namespace WebApplication1.Dtos
{
    public class GetStateWithResultsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GetFormattedResultDto> Results { get; set; }
    }
}
