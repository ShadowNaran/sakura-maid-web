import { Component, computed, inject, input, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { CAFE_CONFIG } from '../core/config/cafe.config';
import { ProductoCard } from '../producto-card/producto-card';
import { CatalogoEstadoService } from '../services/catalogo-estado.service';
import { ProductoService } from '../services/producto.service';

@Component({
  imports: [RouterLink, ProductoCard],
  selector: 'app-productos',
  styleUrl: './productos.css',
  templateUrl: './productos.html',
})
export class Productos {
  private readonly productoService = inject(ProductoService);
  private readonly catalogoEstado = inject(CatalogoEstadoService);
  private readonly router = inject(Router);

  categoriaId = input<string>();
  productos = this.catalogoEstado.productos;
  cargando = signal(true);
  error = signal('');
  readonly cafe = CAFE_CONFIG;

  productosFiltrados = computed(() => {
    const idCategoria = Number(this.categoriaId());
    if (!Number.isInteger(idCategoria) || idCategoria <= 0) {
      return this.productos();
    }
    return this.productos().filter((producto) => producto.categoriaId === idCategoria);
  });

  constructor() {
    this.productoService.obtenerProductos().subscribe({
      next: (productos) => {
        this.catalogoEstado.establecerProductos(productos);
        this.cargando.set(false);
      },
      error: () => {
        this.error.set('No se pudieron cargar los productos.');
        this.cargando.set(false);
      },
    });
  }

  abrirDetalle(id: number): void {
    this.router.navigate(['/productos', id]);
  }
}
