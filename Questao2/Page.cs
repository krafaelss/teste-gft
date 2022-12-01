using Newtonsoft.Json;

namespace Questao2;
using JsonConstructorAttribute = Newtonsoft.Json.JsonConstructorAttribute;

public class Page
{
    [JsonProperty(PropertyName = "page")]
    public int PageNumber { get; set; }

    [JsonProperty(PropertyName = "total_pages")]
    public int TotalPages { get; set; }

    [JsonProperty(PropertyName = "data")]
    public List<FootballMatch> Matches { get; set; }


    //[JsonConstructor]
    //public PageList(IEnumerable<FootballMatch> data, int pageNumber, int totalPages)
    //{
    //    page = pageNumber;
    //    total_pages = totalPages;
    //    data = data.ToList();
    //}
    //public PagingHeader GetHeader()
    //{
    //    return new PagingHeader(this.Total, this.Page, this.PageSize, this.TotalPages);
    //}
}