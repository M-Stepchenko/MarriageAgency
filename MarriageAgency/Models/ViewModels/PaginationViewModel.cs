namespace MarriageAgency.Models
{
    public class PaginationViewModel
    {
        public int PageNumber { get; private set; }
        public int TotalPages { get; private set; }

        public bool HasPreviousPage { get; private set; }
        public bool HasNextPage { get; private set; }
        public bool HasFirstPage { get; private set; }
        public bool HasLastPage { get; private set; }

        public List<int> Pages { get; private set; }

        public PaginationViewModel(int count, int pageNumber = 1, int pageSize = 20)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            
            if(TotalPages == 1)
            {
                HasPreviousPage = false;
                HasNextPage = false;
                HasFirstPage = false;
                HasLastPage = false;
                Pages = new List<int>();
            }
            else if(TotalPages < 11) 
            {
                HasPreviousPage = false;
                HasNextPage = false;
                HasFirstPage = false;
                HasLastPage = false;
                Pages = new List<int>();
                for(int i = 1; i <= TotalPages; i++)
                {
                    Pages.Add(i);
                }
            }
            else if(PageNumber < 4)
            {
                HasPreviousPage = pageNumber > 1;
                HasNextPage = true;
                HasFirstPage = false;
                HasLastPage = true;
                Pages = new List<int> { 1, 2, 3, 4, 5 };
            }
            else if (PageNumber > TotalPages - 3)
            {
                HasPreviousPage = true;
                HasNextPage = pageNumber < TotalPages;
                HasFirstPage = true;
                HasLastPage = false;
                Pages = new List<int> { TotalPages - 4, TotalPages - 3, TotalPages - 2, TotalPages - 1, TotalPages };
            }
            else
            {
                HasPreviousPage = true;
                HasNextPage = true;
                HasFirstPage = true;
                HasLastPage = true;
                Pages = new List<int> { PageNumber - 2, PageNumber - 1, PageNumber, PageNumber + 1, PageNumber + 2 };
            }
        }
    }
}
