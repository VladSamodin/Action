using System;
using System.Text;
using System.Web.Mvc;

namespace MvcPL.Pagination
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
        PageInfo pageInfo, Func<int, string> pageUrl, String elementId)
        {
            TagBuilder ul = new TagBuilder("ul");
            ul.AddCssClass("pagination");

            int startPage = pageInfo.PageNumber > 3 ? pageInfo.PageNumber - 2 : 1;
            //if (pageInfo.PageNumber > 3)
            if (startPage != 1)
            {
                ul.InnerHtml += MakeListItem(1, pageUrl, elementId);
                ul.InnerHtml += MakeListItem(1, pageUrl, elementId, "disabled", "...");
            }

            int totalPages = pageInfo.TotalPages;
            int lastPage = pageInfo.PageNumber + 3 < totalPages ? pageInfo.PageNumber + 2 : totalPages;
            
            //for (int i = 1; i <= pageInfo.TotalPages; i++)
            for (int i = startPage; i <= lastPage; i++)
            {
                ul.InnerHtml += MakeListItem(i, pageUrl, elementId, i == pageInfo.PageNumber ? "active" : null);
            }

            if (lastPage != totalPages)
            {
                ul.InnerHtml += MakeListItem(totalPages, pageUrl, elementId, "disabled", "...");
                ul.InnerHtml += MakeListItem(totalPages, pageUrl, elementId);
            }

            return MvcHtmlString.Create(ul.ToString());
        }

        private static String MakeListItem(int pageNumber, Func<int, string> pageUrl, String elementId, String cssClass = null, String innderText = null)
        {
            innderText = innderText ?? pageNumber.ToString();
            //string linkText = pageUrl != null ? pageUrl(pageNumber) : "#";

            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", pageUrl(pageNumber));
            a.InnerHtml = innderText;

            a.Attributes.Add("data-ajax", "true");
            a.Attributes.Add("data-ajax-method", "GET");
            a.Attributes.Add("data-ajax-mode", "replace");
            a.Attributes.Add("data-ajax-update", "#" + elementId);

            TagBuilder li = new TagBuilder("li");
            li.InnerHtml = a.ToString();
            if (cssClass != null)
                li.AddCssClass(cssClass);
            return li.ToString();
        }
    }
}