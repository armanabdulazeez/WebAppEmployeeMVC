namespace WebAppEmpMVC.Models.Dto
{
    public class PaginatedResponseDto<T>
    {
        public bool FromCache {  get; set; }
        public int TotalCount {  get; set; }
        public int PageNumber {  get; set; }
        public int PageSize { get; set; }
        public List<T> Data {  get; set; }
    }
}
