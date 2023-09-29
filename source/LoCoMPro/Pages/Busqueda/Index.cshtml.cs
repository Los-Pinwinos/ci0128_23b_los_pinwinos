using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LoCoMPro.Models;


namespace LoCoMPro.Pages.Busqueda
{
    public class IndexVMModel : PageModel
    {
        private readonly LoCoMPro.Data.LoCoMProContext _context;
        private readonly IConfiguration _configuration;


        public IndexVMModel(LoCoMPro.Data.LoCoMProContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // Busquedas
        [BindProperty(SupportsGet = true)]
        public string? producto { get; set; }
       
        public void OnGet()
        {
            
        }
    }
}

