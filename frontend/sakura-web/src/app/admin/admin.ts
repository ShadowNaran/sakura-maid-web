import { Component, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AdminCategorias } from '../admin-categorias/admin-categorias';
import { AdminEventos } from '../admin-eventos/admin-eventos';
import { AdminNovedades } from '../admin-novedades/admin-novedades';
import { AdminProductos } from '../admin-productos/admin-productos';

type SeccionAdmin = 'productos' | 'categorias' | 'eventos' | 'novedades';

@Component({
  imports: [RouterLink, AdminProductos, AdminCategorias, AdminEventos, AdminNovedades],
  selector: 'app-admin',
  styleUrl: './admin.css',
  templateUrl: './admin.html',
})
export class Admin {
  seccion = signal<SeccionAdmin>('productos');

  cambiarSeccion(seccion: SeccionAdmin): void {
    this.seccion.set(seccion);
  }
}
