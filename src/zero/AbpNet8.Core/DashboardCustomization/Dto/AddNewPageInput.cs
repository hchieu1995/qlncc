using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbpNet8.DashboardCustomization.Dto
{
    public class AddNewPageInput
    {
        public string DashboardName { get; set; }

        public string Name { get; set; }

        public string Application { get; set; }
    }
    public class AddNewPageOutput
    {
        public string PageId { get; set; }
    }
    public class AddWidgetInput
    {
        public string WidgetId { get; set; }

        public string PageId { get; set; }

        public string DashboardName { get; set; }

        public byte Width { get; set; }

        public byte Height { get; set; }

        public string Application { get; set; }
    }
    public class DashboardOutput
    {
        public string Name { get; set; }

        public List<WidgetOutput> Widgets { get; set; }

        public DashboardOutput(string name, List<WidgetOutput> widgets)
        {
            Name = name;
            Widgets = widgets;
        }
    }
    public class DeletePageInput
    {
        public string Id { get; set; }

        public string DashboardName { get; set; }

        public string Application { get; set; }
    }
    public class GetDashboardInput
    {
        public string DashboardName { get; set; }

        public string Application { get; set; }
    }
    public class RenamePageInput
    {
        public string DashboardName { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Application { get; set; }
    }
    public class SavePageInput
    {
        public string DashboardName { get; set; }

        public string Application { get; set; }

        public List<Page> Pages { get; set; }
    }
    public class WidgetFilterOutput
    {
        public string Id { get; }

        public string Name { get; }

        public WidgetFilterOutput(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    public class WidgetOutput
    {
        public string Id { get; }

        public string Name { get; }

        public string Description { get; }

        public List<WidgetFilterOutput> Filters { get; set; }

        public WidgetOutput(string id, string name, string description, List<WidgetFilterOutput> filters = null)
        {
            Id = id;
            Name = name;
            Description = description;
            Filters = filters;
        }
    }
}
