﻿using Microsoft.EntityFrameworkCore;

namespace LoCoMPro.Utils.Interfaces
{
    // Buscador genérico
    public interface IBuscador<out Tipo>
    {
        // Método genérico de búsqueda
        public IQueryable<Tipo> buscar();
    }
}
