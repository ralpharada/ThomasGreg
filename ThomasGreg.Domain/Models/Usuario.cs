﻿using ThomasGreg.Domain.Interfaces;

namespace ThomasGreg.Domain.Models
{
    public partial class Usuario: IUsuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
        public bool Ativo { get; set; }  
    }
}
